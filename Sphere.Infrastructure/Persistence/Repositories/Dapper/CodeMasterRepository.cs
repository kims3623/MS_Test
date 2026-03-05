using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.DTOs.Lookup;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Dapper implementation of ICodeMasterRepository.
/// Provides CRUD operations via stored procedures (TVP) and Lookup queries via raw SQL.
/// </summary>
public class CodeMasterRepository : DapperRepositoryBase, ICodeMasterRepository
{
    private readonly ILogger<CodeMasterRepository> _logger;
    private readonly ILocaleService _localeService;
    #region SQL Queries

    /// <summary>
    /// SQL query for retrieving lookup data from SPC_CODE_MST by code_class_id.
    /// Uses language-based name selection via CASE expression.
    /// </summary>
    private const string GetLookupByClassIdSql = @"
SELECT
    div_seq AS DivSeq,
    code_id AS Id,
    ISNULL(code_alias, '') AS Alias,
    CASE @language
        WHEN 'ko-KR' THEN ISNULL(code_name_k, '')
        WHEN 'en-US' THEN ISNULL(code_name_e, ISNULL(code_name_k, ''))
        WHEN 'zh-CN' THEN ISNULL(code_name_c, ISNULL(code_name_k, ''))
        WHEN 'vi-VN' THEN ISNULL(code_name_v, ISNULL(code_name_k, ''))
        ELSE ISNULL(code_name_k, '')
    END AS Name,
    ISNULL(code_name_k, '') AS NameK,
    ISNULL(code_name_e, '') AS NameE,
    ISNULL(code_name_c, '') AS NameC,
    ISNULL(code_name_v, '') AS NameV,
    ISNULL(dsp_seq, 0) AS DisplaySeq,
    ISNULL(code_opt, '') AS CodeOpt
FROM SPC_CODE_MST WITH (NOLOCK)
WHERE div_seq = @divSeq
  AND code_class_id = @codeClassId
  AND (@activeOnly = 0 OR use_yn = 'Y')
ORDER BY dsp_seq, code_id";

    /// <summary>
    /// SQL query for retrieving a single code by composite key.
    /// </summary>
    private const string GetByIdSql = @"
SELECT
    div_seq AS DivSeq,
    code_id AS Id,
    ISNULL(code_alias, '') AS Alias,
    CASE @language
        WHEN 'ko-KR' THEN ISNULL(code_name_k, '')
        WHEN 'en-US' THEN ISNULL(code_name_e, ISNULL(code_name_k, ''))
        WHEN 'zh-CN' THEN ISNULL(code_name_c, ISNULL(code_name_k, ''))
        WHEN 'vi-VN' THEN ISNULL(code_name_v, ISNULL(code_name_k, ''))
        ELSE ISNULL(code_name_k, '')
    END AS Name,
    ISNULL(code_name_k, '') AS NameK,
    ISNULL(code_name_e, '') AS NameE,
    ISNULL(code_name_c, '') AS NameC,
    ISNULL(code_name_v, '') AS NameV,
    ISNULL(dsp_seq, 0) AS DisplaySeq,
    ISNULL(code_opt, '') AS CodeOpt
FROM SPC_CODE_MST WITH (NOLOCK)
WHERE div_seq = @divSeq
  AND code_class_id = @codeClassId
  AND code_id = @codeId";

    #endregion

    public CodeMasterRepository(
        IDbConnection connection,
        ILogger<CodeMasterRepository> logger,
        ILocaleService localeService) : base(connection)
    {
        _logger = logger;
        _localeService = localeService;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CodeMasterDto>> GetCodeMasterListAsync(
        string divSeq,
        CodeMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync<CodeMasterDto>(
            "USP_SPC_CODE_MST_SELECT",
            new
            {
                div_seq = divSeq,
                code_class_id = filter?.CodeClassId ?? string.Empty,
                code_id = string.Empty,
                code_name = filter?.SearchText ?? string.Empty,
                use_yn = filter?.UseYn ?? string.Empty
            });
    }

    /// <inheritdoc />
    public async Task<CodeMasterDto?> GetCodeMasterByIdAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        CancellationToken cancellationToken = default)
    {
        return await QueryFirstOrDefaultAsync<CodeMasterDto>(
            "USP_SPC_CODE_MST_SELECT",
            new
            {
                div_seq = divSeq,
                code_class_id = codeClassId,
                code_id = codeId,
                code_name = string.Empty,
                use_yn = string.Empty
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// USP_SPC_CODE_MST_INSERT requires TVP: @P_Data SPC_CODE_MST_TYPE READONLY
    /// </remarks>
    public async Task<CodeMasterResultDto> CreateCodeMasterAsync(
        string divSeq,
        CreateCodeMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var table = BuildCodeMstDataTable(
                divSeq, dto.CodeId, dto.CodeClassId, dto.CodeAlias,
                dto.CodeNameK, dto.CodeNameE, dto.CodeNameC, dto.CodeNameV,
                dto.DisplaySeq, dto.CodeOpt, dto.UseYn, dto.Description,
                userId, userId, "USP_SPC_CODE_MST_INSERT");

            await ExecuteTvpCommandAsync("USP_SPC_CODE_MST_INSERT", table, cancellationToken);

            _logger.LogInformation("Created code master: DivSeq={DivSeq}, CodeId={CodeId}, ClassId={ClassId}",
                divSeq, dto.CodeId, dto.CodeClassId);

            return new CodeMasterResultDto
            {
                Success = true,
                Message = "Code master created successfully.",
                CodeId = dto.CodeId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create code master {CodeId}", dto.CodeId);
            return new CodeMasterResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// USP_SPC_CODE_MST_UPDATE requires TVP: @P_Data SPC_CODE_MST_TYPE READONLY
    /// </remarks>
    public async Task<CodeMasterResultDto> UpdateCodeMasterAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        UpdateCodeMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var table = BuildCodeMstDataTable(
                divSeq, codeId, codeClassId, dto.CodeAlias,
                dto.CodeNameK, dto.CodeNameE, dto.CodeNameC, dto.CodeNameV,
                dto.DisplaySeq, dto.CodeOpt, dto.UseYn, dto.Description,
                userId, userId, "USP_SPC_CODE_MST_UPDATE");

            await ExecuteTvpCommandAsync("USP_SPC_CODE_MST_UPDATE", table, cancellationToken);

            _logger.LogInformation("Updated code master: DivSeq={DivSeq}, CodeId={CodeId}", divSeq, codeId);

            return new CodeMasterResultDto
            {
                Success = true,
                Message = "Code master updated successfully.",
                CodeId = codeId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update code master {CodeId}", codeId);
            return new CodeMasterResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// USP_SPC_CODE_MST_DELETE requires TVP: @P_Data SPC_CODE_MST_TYPE READONLY
    /// </remarks>
    public async Task<CodeMasterResultDto> DeleteCodeMasterAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var table = BuildCodeMstDataTable(
                divSeq, codeId, codeClassId, "",
                "", "", "", "",
                0, "", "Y", "",
                userId, userId, "USP_SPC_CODE_MST_DELETE");

            await ExecuteTvpCommandAsync("USP_SPC_CODE_MST_DELETE", table, cancellationToken);

            _logger.LogInformation("Deleted code master: DivSeq={DivSeq}, CodeId={CodeId}", divSeq, codeId);

            return new CodeMasterResultDto
            {
                Success = true,
                Message = "Code master deleted successfully.",
                CodeId = codeId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete code master {CodeId}", codeId);
            return new CodeMasterResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    #region 공통 조회 메서드 (2개)

    /// <inheritdoc />
    public async Task<IEnumerable<CodeMasterLookupDto>> GetLookupByClassIdAsync(
        string divSeq,
        string codeClassId,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        language ??= _localeService.CurrentLocale;

        var parameters = new
        {
            divSeq,
            codeClassId,
            language,
            activeOnly = activeOnly ? 1 : 0
        };

        return await _connection.QueryAsync<CodeMasterLookupDto>(
            GetLookupByClassIdSql,
            parameters,
            commandType: CommandType.Text);
    }

    /// <inheritdoc />
    public async Task<CodeMasterLookupDto?> GetByIdAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        string? language = null,
        CancellationToken cancellationToken = default)
    {
        language ??= _localeService.CurrentLocale;

        var parameters = new
        {
            divSeq,
            codeClassId,
            codeId,
            language
        };

        return await _connection.QueryFirstOrDefaultAsync<CodeMasterLookupDto>(
            GetByIdSql,
            parameters,
            commandType: CommandType.Text);
    }

    #endregion

    #region 타입별 조회 메서드 (18개)

    /// <inheritdoc />
    public async Task<IEnumerable<ActProdLookupDto>> GetActProdsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "ACT_PROD", language, activeOnly, cancellationToken);
        return MapToTypedDto<ActProdLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<StepLookupDto>> GetStepsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "STEP", language, activeOnly, cancellationToken);
        return MapToTypedDto<StepLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ItemLookupDto>> GetItemsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "ITEM", language, activeOnly, cancellationToken);
        return MapToTypedDto<ItemLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MeasmLookupDto>> GetMeasmsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "MEASM", language, activeOnly, cancellationToken);
        return MapToTypedDto<MeasmLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EqpLookupDto>> GetEqpsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "EQP", language, activeOnly, cancellationToken);
        return MapToTypedDto<EqpLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LineLookupDto>> GetLinesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "LINE", language, activeOnly, cancellationToken);
        return MapToTypedDto<LineLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ShiftLookupDto>> GetShiftsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "SHIFT", language, activeOnly, cancellationToken);
        return MapToTypedDto<ShiftLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<StageLookupDto>> GetStagesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "STAGE", language, activeOnly, cancellationToken);
        return MapToTypedDto<StageLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FrequencyLookupDto>> GetFrequenciesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "FREQUENCY", language, activeOnly, cancellationToken);
        return MapToTypedDto<FrequencyLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CustLookupDto>> GetCustsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "VENDOR", language, activeOnly, cancellationToken);
        return MapToTypedDto<CustLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EndUserLookupDto>> GetEndUsersAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "END_USER", language, activeOnly, cancellationToken);
        return MapToTypedDto<EndUserLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RunRuleLookupDto>> GetRunRulesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "RUNRULE", language, activeOnly, cancellationToken);
        return MapToTypedDto<RunRuleLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MenuLookupDto>> GetMenusAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "MENU", language, activeOnly, cancellationToken);
        return MapToTypedDto<MenuLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PicTypeLookupDto>> GetPicTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "PIC_TYPE", language, activeOnly, cancellationToken);
        return MapToTypedDto<PicTypeLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<StatTypeLookupDto>> GetStatTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "STAT_TYPE", language, activeOnly, cancellationToken);
        return MapToTypedDto<StatTypeLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CntlnTypeLookupDto>> GetCntlnTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "CNTLN_TYPE", language, activeOnly, cancellationToken);
        return MapToTypedDto<CntlnTypeLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SpecTypeLookupDto>> GetSpecTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "SPEC_TYPE", language, activeOnly, cancellationToken);
        return MapToTypedDto<SpecTypeLookupDto>(results);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<DataTypeLookupDto>> GetDataTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var results = await GetLookupByClassIdAsync(divSeq, "DATA_TYPE", language, activeOnly, cancellationToken);
        return MapToTypedDto<DataTypeLookupDto>(results);
    }

    #endregion

    #region Private Helper Methods

    /// <summary>
    /// Maps CodeMasterLookupDto to typed DTO by copying base properties.
    /// The typed DTOs use computed properties based on CodeOpt.
    /// </summary>
    private static IEnumerable<T> MapToTypedDto<T>(IEnumerable<CodeMasterLookupDto> source)
        where T : CodeMasterLookupDto, new()
    {
        return source.Select(x => new T
        {
            DivSeq = x.DivSeq,
            Id = x.Id,
            Alias = x.Alias,
            Name = x.Name,
            NameK = x.NameK,
            NameE = x.NameE,
            NameC = x.NameC,
            NameV = x.NameV,
            DisplaySeq = x.DisplaySeq,
            CodeOpt = x.CodeOpt
        });
    }

    #endregion

    #region TVP Helpers

    private SqlConnection GetSqlConnection()
    {
        if (_connection is SqlConnection sqlConn) return sqlConn;
        throw new InvalidOperationException("Connection must be Microsoft.Data.SqlClient.SqlConnection for TVP support");
    }

    private async Task ExecuteTvpCommandAsync(string spName, DataTable table, CancellationToken cancellationToken)
    {
        var sqlConn = GetSqlConnection();
        if (sqlConn.State != ConnectionState.Open)
            await sqlConn.OpenAsync(cancellationToken);

        using var cmd = sqlConn.CreateCommand();
        cmd.CommandText = spName;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@P_Data", SqlDbType.Structured)
        {
            TypeName = "SPC_CODE_MST_TYPE",
            Value = table
        });

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    /// <summary>
    /// Builds DataTable matching SPC_CODE_MST_TYPE TVP schema (19 columns).
    /// </summary>
    private static DataTable BuildCodeMstDataTable(
        string divSeq, string codeId, string codeClassId, string codeAlias,
        string codeNameK, string codeNameE, string codeNameC, string codeNameV,
        int dspSeq, string codeOpt, string useYn, string description,
        string createUserId, string updateUserId, string actiName)
    {
        var table = CreateCodeMstSchema();
        var now = DateTime.Now;

        table.Rows.Add(
            divSeq,                                         // div_seq
            codeId,                                         // code_id
            (object?)codeAlias ?? DBNull.Value,             // code_alias
            codeClassId,                                    // code_class_id
            (object?)codeNameK ?? DBNull.Value,             // code_name_k
            (object?)codeNameE ?? DBNull.Value,             // code_name_e
            (object?)codeNameC ?? DBNull.Value,             // code_name_c
            (object?)codeNameV ?? DBNull.Value,             // code_name_v
            dspSeq,                                         // dsp_seq
            (object?)codeOpt ?? DBNull.Value,               // code_opt
            useYn,                                          // use_yn
            actiName,                                       // acti_name
            actiName,                                       // origin_acti_name
            DBNull.Value,                                   // reason_code
            (object?)description ?? DBNull.Value,           // description
            createUserId,                                   // create_user_id
            now,                                            // create_date
            updateUserId,                                   // update_user_id
            now                                             // update_date
        );

        return table;
    }

    /// <summary>
    /// Creates DataTable schema matching SPC_CODE_MST_TYPE (19 columns).
    /// </summary>
    private static DataTable CreateCodeMstSchema()
    {
        var table = new DataTable();
        table.Columns.Add("div_seq", typeof(string));
        table.Columns.Add("code_id", typeof(string));
        table.Columns.Add("code_alias", typeof(string));
        table.Columns.Add("code_class_id", typeof(string));
        table.Columns.Add("code_name_k", typeof(string));
        table.Columns.Add("code_name_e", typeof(string));
        table.Columns.Add("code_name_c", typeof(string));
        table.Columns.Add("code_name_v", typeof(string));
        table.Columns.Add("dsp_seq", typeof(int));
        table.Columns.Add("code_opt", typeof(string));
        table.Columns.Add("use_yn", typeof(string));
        table.Columns.Add("acti_name", typeof(string));
        table.Columns.Add("origin_acti_name", typeof(string));
        table.Columns.Add("reason_code", typeof(string));
        table.Columns.Add("description", typeof(string));
        table.Columns.Add("create_user_id", typeof(string));
        table.Columns.Add("create_date", typeof(DateTime));
        table.Columns.Add("update_user_id", typeof(string));
        table.Columns.Add("update_date", typeof(DateTime));
        return table;
    }

    #endregion
}
