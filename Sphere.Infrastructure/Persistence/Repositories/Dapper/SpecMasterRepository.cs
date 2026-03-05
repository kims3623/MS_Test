using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

public class SpecMasterRepository : DapperRepositoryBase, ISpecMasterRepository
{
    private readonly ILogger<SpecMasterRepository> _logger;

    public SpecMasterRepository(
        IDbConnection connection,
        ILogger<SpecMasterRepository> logger) : base(connection)
    {
        _logger = logger;
    }

    /// <remarks>
    /// DB USP: USP_SPC_SPEC_SELECT
    /// DB params: @P_Lang_Type, @P_div_seq, @P_vendor_id(MAX), @P_mtrl_class_id(MAX),
    ///            @P_act_prod_id(MAX), @P_project_id(MAX), @P_mtrl_id(MAX), @P_step_id(MAX),
    ///            @P_item_id(MAX), @P_measm_id(MAX), @P_use_yn
    /// </remarks>
    public async Task<IEnumerable<SpecMasterDto>> GetSpecMasterListAsync(
        string divSeq, SpecMasterFilterDto? filter = null, CancellationToken cancellationToken = default)
    {
        return await QueryAsync<SpecMasterDto>("USP_SPC_SPEC_SELECT", new
        {
            Lang_Type = "ko-KR",
            div_seq = divSeq,
            vendor_id = filter?.VendorId,
            mtrl_class_id = filter?.MtrlClassId,
            act_prod_id = (string?)null,
            project_id = (string?)null,
            mtrl_id = (string?)null,
            step_id = (string?)null,
            item_id = (string?)null,
            measm_id = (string?)null,
            use_yn = filter?.UseYn
        });
    }

    public async Task<IEnumerable<SpecDetailDto>> GetSpecDetailAsync(
        string divSeq, string specSysId, CancellationToken cancellationToken = default)
    {
        return await QueryAsync<SpecDetailDto>("USP_SPC_SPEC_DTL_SELECT", new { div_seq = divSeq, spec_sys_id = specSysId });
    }

    /// <summary>
    /// Create spec master via USP_SPC_SPEC_INSERT with TVP parameters.
    /// </summary>
    public async Task<SpecMasterResultDto> CreateSpecMasterAsync(
        string divSeq, CreateSpecMasterDto dto, string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mstTable = BuildMstDataTable(divSeq, dto.SpecId, dto.MtrlClassId, dto.VendorId,
                dto.UseYn ?? "Y", dto.Description, userId, isCreate: true);
            var dtlTable = CreateDtlSchema();

            var sqlConn = GetSqlConnection();
            if (sqlConn.State != ConnectionState.Open)
                await sqlConn.OpenAsync(cancellationToken);

            using var cmd = sqlConn.CreateCommand();
            cmd.CommandText = "USP_SPC_SPEC_INSERT";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@P_MST_DATA", SqlDbType.Structured)
            {
                TypeName = "SPC_SPEC_MST_TYPE",
                Value = mstTable
            });
            cmd.Parameters.Add(new SqlParameter("@P_DTL_DATA", SqlDbType.Structured)
            {
                TypeName = "SPC_SPEC_DTL_TYPE",
                Value = dtlTable
            });

            await cmd.ExecuteNonQueryAsync(cancellationToken);

            _logger.LogInformation("Created spec master: DivSeq={DivSeq}, SpecId={SpecId}", divSeq, dto.SpecId);

            return new SpecMasterResultDto
            {
                Success = true,
                Message = "Spec master created successfully.",
                SpecSysId = dto.SpecId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create spec master {SpecId}", dto.SpecId);
            return new SpecMasterResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// Update spec master via USP_SPC_SPEC_UPDATE with TVP + OUTPUT parameters.
    /// </summary>
    public async Task<SpecMasterResultDto> UpdateSpecMasterAsync(
        string divSeq, string specSysId, UpdateSpecMasterDto dto, string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mstTable = BuildMstDataTable(divSeq, specSysId, "", "",
                dto.UseYn ?? "Y", dto.Description, userId, isCreate: false);
            var dtlTable = CreateDtlSchema();

            var sqlConn = GetSqlConnection();
            if (sqlConn.State != ConnectionState.Open)
                await sqlConn.OpenAsync(cancellationToken);

            using var cmd = sqlConn.CreateCommand();
            cmd.CommandText = "USP_SPC_SPEC_UPDATE";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@P_MST_DATA", SqlDbType.Structured)
            {
                TypeName = "SPC_SPEC_MST_TYPE",
                Value = mstTable
            });
            cmd.Parameters.Add(new SqlParameter("@P_DTL_DATA", SqlDbType.Structured)
            {
                TypeName = "SPC_SPEC_DTL_TYPE",
                Value = dtlTable
            });

            var isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(isSuccessParam);

            await cmd.ExecuteNonQueryAsync(cancellationToken);

            var success = isSuccessParam.Value is true or 1;

            _logger.LogInformation("Updated spec master: DivSeq={DivSeq}, SpecSysId={SpecSysId}, Success={Success}",
                divSeq, specSysId, success);

            return new SpecMasterResultDto
            {
                Success = success,
                Message = success ? "Spec master updated successfully." : "Failed to update spec master.",
                SpecSysId = specSysId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update spec master {SpecSysId}", specSysId);
            return new SpecMasterResultDto
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

    private static DataTable BuildMstDataTable(
        string divSeq, string specSysId, string mtrlClassId, string vendorId,
        string useYn, string? description, string userId, bool isCreate)
    {
        var table = CreateMstSchema();
        var now = DateTime.Now;

        table.Rows.Add(
            divSeq,                                         // div_seq
            specSysId,                                      // spec_sys_id
            mtrlClassId,                                    // mtrl_class_id
            vendorId,                                       // vendor_id
            DBNull.Value,                                   // mtrl_id
            DBNull.Value,                                   // step_id
            DBNull.Value,                                   // item_id
            DBNull.Value,                                   // measm_id
            "1",                                            // spl_cnt
            DBNull.Value,                                   // eqp_id
            DBNull.Value,                                   // stage_id
            DBNull.Value,                                   // line
            DBNull.Value,                                   // mold_cnt
            DBNull.Value,                                   // cavity
            DBNull.Value,                                   // frequency
            DBNull.Value,                                   // needle
            DBNull.Value,                                   // angle
            DBNull.Value,                                   // statistics
            DBNull.Value,                                   // remarks
            DBNull.Value,                                   // spcl_id
            DBNull.Value,                                   // sub_spcl_id
            DBNull.Value,                                   // optn_id
            DBNull.Value,                                   // sub_optn_id
            DBNull.Value,                                   // aprov_id
            useYn,                                          // use_yn
            DBNull.Value,                                   // acti_name
            DBNull.Value,                                   // origin_acti_name
            DBNull.Value,                                   // reason_code
            (object?)description ?? DBNull.Value,           // description
            isCreate ? userId : (object)DBNull.Value,       // create_user_id
            isCreate ? now : (object)DBNull.Value,          // create_date
            userId,                                         // update_user_id
            now                                             // update_date
        );

        return table;
    }

    private static DataTable CreateMstSchema()
    {
        var table = new DataTable();
        table.Columns.Add("div_seq", typeof(string));
        table.Columns.Add("spec_sys_id", typeof(string));
        table.Columns.Add("mtrl_class_id", typeof(string));
        table.Columns.Add("vendor_id", typeof(string));
        table.Columns.Add("mtrl_id", typeof(string));
        table.Columns.Add("step_id", typeof(string));
        table.Columns.Add("item_id", typeof(string));
        table.Columns.Add("measm_id", typeof(string));
        table.Columns.Add("spl_cnt", typeof(string));
        table.Columns.Add("eqp_id", typeof(string));
        table.Columns.Add("stage_id", typeof(string));
        table.Columns.Add("line", typeof(string));
        table.Columns.Add("mold_cnt", typeof(string));
        table.Columns.Add("cavity", typeof(string));
        table.Columns.Add("frequency", typeof(string));
        table.Columns.Add("needle", typeof(string));
        table.Columns.Add("angle", typeof(string));
        table.Columns.Add("statistics", typeof(string));
        table.Columns.Add("remarks", typeof(string));
        table.Columns.Add("spcl_id", typeof(string));
        table.Columns.Add("sub_spcl_id", typeof(string));
        table.Columns.Add("optn_id", typeof(string));
        table.Columns.Add("sub_optn_id", typeof(string));
        table.Columns.Add("aprov_id", typeof(string));
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

    private static DataTable CreateDtlSchema()
    {
        var table = new DataTable();
        table.Columns.Add("div_seq", typeof(string));
        table.Columns.Add("spec_sys_id", typeof(string));
        table.Columns.Add("cntln_type_id", typeof(string));
        table.Columns.Add("spec_type_id", typeof(string));
        table.Columns.Add("ul_value", typeof(string));
        table.Columns.Add("cl_value", typeof(string));
        table.Columns.Add("ll_value", typeof(string));
        table.Columns.Add("target_str", typeof(string));
        table.Columns.Add("spec_ver", typeof(int));
        table.Columns.Add("cl_up_id", typeof(string));
        table.Columns.Add("cl_calc_type", typeof(string));
        table.Columns.Add("cl_calc_value", typeof(string));
        table.Columns.Add("min_data_qty", typeof(string));
        table.Columns.Add("ref_period", typeof(string));
        table.Columns.Add("ref_cl_type", typeof(string));
        table.Columns.Add("ref_cl_value", typeof(string));
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
