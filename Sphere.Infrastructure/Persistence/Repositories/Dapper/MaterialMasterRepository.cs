using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Dapper implementation of IMaterialMasterRepository.
/// Uses TVP (Table-Valued Parameter) for INSERT/UPDATE via stored procedures.
/// </summary>
public class MaterialMasterRepository : DapperRepositoryBase, IMaterialMasterRepository
{
    private readonly ILogger<MaterialMasterRepository> _logger;

    public MaterialMasterRepository(
        IDbConnection connection,
        ILogger<MaterialMasterRepository> logger) : base(connection)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_MTRL_MST_SELECT
    /// DB params: @P_Lang_Type VARCHAR(10), @P_div_seq VARCHAR(40),
    ///   @P_vendor_id VARCHAR(4000)='', @P_mtrl_id VARCHAR(4000)='',
    ///   @P_mtrl_class_id VARCHAR(4000)='', @P_act_prod_id VARCHAR(4000)='',
    ///   @P_cust_id VARCHAR(4000)='', @P_end_user_id VARCHAR(4000)='',
    ///   @P_project_id VARCHAR(4000)='', @P_use_yn VARCHAR(10)=''
    /// </remarks>
    public async Task<IEnumerable<MaterialMasterDto>> GetMaterialMasterListAsync(
        string divSeq,
        MaterialMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync<MaterialMasterDto>(
            "USP_SPC_MTRL_MST_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                vendor_id = filter?.VendorId ?? "",
                mtrl_id = "",
                mtrl_class_id = filter?.MtrlClassId ?? "",
                act_prod_id = "",
                cust_id = "",
                end_user_id = "",
                project_id = "",
                use_yn = filter?.UseYn ?? ""
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_MTRL_MST_SELECT (filtered by mtrl_id)
    /// </remarks>
    public async Task<MaterialMasterDto?> GetMaterialMasterByIdAsync(
        string divSeq,
        string mtrlId,
        CancellationToken cancellationToken = default)
    {
        return await QueryFirstOrDefaultAsync<MaterialMasterDto>(
            "USP_SPC_MTRL_MST_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                mtrl_id = mtrlId
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// USP_SPC_MTRL_MST_INSERT requires TVP: @P_Data SPC_MTRL_MST_CUST_TYPE READONLY
    /// </remarks>
    public async Task<MaterialMasterResultDto> CreateMaterialMasterAsync(
        string divSeq,
        CreateMaterialMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var table = BuildMtrlMstCustDataTable(
                divSeq, dto.MtrlId, dto.MtrlClassId, dto.VendorId,
                "", "", "", dto.UseYn, dto.Description,
                userId, userId, "USP_SPC_MTRL_MST_INSERT", "");

            await ExecuteTvpCommandAsync("USP_SPC_MTRL_MST_INSERT", table, cancellationToken);

            _logger.LogInformation("Created material master: DivSeq={DivSeq}, MtrlId={MtrlId}",
                divSeq, dto.MtrlId);

            return new MaterialMasterResultDto
            {
                Success = true,
                Message = "Material master created successfully.",
                MtrlId = dto.MtrlId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create material master {MtrlId}", dto.MtrlId);
            return new MaterialMasterResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

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
            TypeName = "SPC_MTRL_MST_CUST_TYPE",
            Value = table
        });

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    /// <summary>
    /// Builds DataTable matching SPC_MTRL_MST_CUST_TYPE TVP schema (16 columns).
    /// </summary>
    private static DataTable BuildMtrlMstCustDataTable(
        string divSeq, string mtrlId, string mtrlClassId, string vendorId,
        string actProdId, string endUserId, string projectId,
        string useYn, string? description,
        string createUserId, string updateUserId, string actiName, string custIds)
    {
        var table = CreateMtrlMstCustSchema();

        table.Rows.Add(
            divSeq,                                         // div_seq
            mtrlId,                                         // mtrl_id
            mtrlClassId,                                    // mtrl_class_id
            vendorId,                                       // vendor_id
            actProdId,                                      // act_prod_id
            (object?)endUserId ?? DBNull.Value,             // end_user_id
            projectId,                                      // project_id
            "N",                                            // unit_biz_yn
            useYn,                                          // use_yn
            actiName,                                       // acti_name
            actiName,                                       // origin_acti_name
            DBNull.Value,                                   // reason_code
            (object?)description ?? DBNull.Value,           // description
            createUserId,                                   // create_user_id
            updateUserId,                                   // update_user_id
            custIds                                         // cust_ids (NOT NULL)
        );

        return table;
    }

    /// <summary>
    /// Creates DataTable schema matching SPC_MTRL_MST_CUST_TYPE (16 columns).
    /// </summary>
    private static DataTable CreateMtrlMstCustSchema()
    {
        var table = new DataTable();
        table.Columns.Add("div_seq", typeof(string));
        table.Columns.Add("mtrl_id", typeof(string));
        table.Columns.Add("mtrl_class_id", typeof(string));
        table.Columns.Add("vendor_id", typeof(string));
        table.Columns.Add("act_prod_id", typeof(string));
        table.Columns.Add("end_user_id", typeof(string));
        table.Columns.Add("project_id", typeof(string));
        table.Columns.Add("unit_biz_yn", typeof(string));
        table.Columns.Add("use_yn", typeof(string));
        table.Columns.Add("acti_name", typeof(string));
        table.Columns.Add("origin_acti_name", typeof(string));
        table.Columns.Add("reason_code", typeof(string));
        table.Columns.Add("description", typeof(string));
        table.Columns.Add("create_user_id", typeof(string));
        table.Columns.Add("update_user_id", typeof(string));
        table.Columns.Add("cust_ids", typeof(string));
        return table;
    }

    #endregion
}
