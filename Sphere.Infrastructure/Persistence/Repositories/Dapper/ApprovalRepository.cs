using System.Data;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Approval repository implementation using Dapper for stored procedures.
/// </summary>
public class ApprovalRepository : DapperRepositoryBase, IApprovalRepository
{
    public ApprovalRepository(IDbConnection connection) : base(connection) { }

    /// <inheritdoc />
    public async Task<IEnumerable<ApprovalListDto>> GetListAsync(ApprovalFilterDto filter)
    {
        return await QueryAsync<ApprovalListDto>(
            "USP_SPC_APROV_LIST_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = filter.DivSeq,
                StartDate = filter.StartDate ?? string.Empty,
                EndDate = filter.EndDate ?? string.Empty,
                User_Id = filter.UserId ?? string.Empty,
                Aprov_Type = string.Empty,
                Chg_Type = string.Empty,
                Aprov_Id = string.Empty,
                Input_Aprov_Id = string.Empty,
                Aprov_State = filter.AprovState ?? string.Empty,
                Aprov_Title = string.Empty,
                Chg_Type_Id = filter.ChgTypeId ?? string.Empty
            });
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApprovalDefaultUserDto>> GetDefaultUsersAsync(
        string divSeq, string? chgTypeId = null, string? vendorId = null, string? mtrlClassId = null)
    {
        return await QueryAsync<ApprovalDefaultUserDto>(
            "USP_SPC_APROV_DEFAULT_USER_SELECT",
            new
            {
                div_seq = divSeq,
                chg_type_id = chgTypeId,
                vendor_id = vendorId,
                mtrl_class_id = mtrlClassId
            });
    }

    /// <inheritdoc />
    public async Task<ApprovalInsertResultDto> InsertAsync(ApprovalCreateDto dto)
    {
        var result = await QueryFirstOrDefaultAsync<ApprovalInsertResultDto>(
            "USP_SPC_APROV_LIST_INSERT",
            new
            {
                div_seq = dto.DivSeq,
                chg_type_id = dto.ChgTypeId,
                aprov_action_id = dto.AprovActionId,
                writer = dto.Writer,
                title = dto.Title,
                contents = dto.Contents,
                user_list = dto.UserList,
                batch_id = dto.BatchId,
                alm_sys_id = dto.AlmSysId,
                alm_action_id = dto.AlmActionId,
                create_user_id = dto.CreateUserId
            });

        return result ?? new ApprovalInsertResultDto { Result = "FAIL", ResultMessage = "No result returned" };
    }

    /// <inheritdoc />
    public async Task<ApprovalDetailDto?> GetDetailAsync(string divSeq, string aprovId)
    {
        var sql = @"
            SELECT al.aprov_id, al.chg_type_id,
                ct.code_name_k AS chg_type_name,
                al.aprov_action_id,
                aa.code_name_k AS aprov_action_name,
                aas.code_name_k AS aprov_action_state_name,
                al.user_list, al.update_user_id,
                CONVERT(VARCHAR(20), al.update_date, 120) AS update_date,
                al.description
            FROM SPC_APROV_LIST al
            LEFT JOIN SPC_CODE_MST ct
                ON al.div_seq = ct.div_seq AND al.chg_type_id = ct.code_id AND ct.code_class_id = 'CHG_TYPE'
            LEFT JOIN SPC_CODE_MST aa
                ON al.div_seq = aa.div_seq AND al.aprov_action_id = aa.code_id AND aa.code_class_id = 'APROV_ACTION'
            LEFT JOIN SPC_CODE_MST aas
                ON al.div_seq = aas.div_seq AND al.aprov_action_state = aas.code_id AND aas.code_class_id = 'APROV_ACTION_STATE'
            WHERE al.div_seq = @div_seq AND al.aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailDto>(sql, new
        {
            div_seq = divSeq,
            aprov_id = aprovId
        });
    }

    /// <inheritdoc />
    public async Task<ApprovalDetailButtonDto?> GetDetailButtonAsync(string divSeq, string aprovId, string userId)
    {
        var sql = @"
            SELECT
                CASE WHEN writer = @user_id AND aprov_state = 'PENDING' THEN 'Y' ELSE 'N' END AS cbutton_flag,
                CASE WHEN user_list LIKE '%' + @user_id + '%' AND aprov_state = 'PENDING' THEN 'Y' ELSE 'N' END AS arbutton_flag
            FROM SPC_APROV_LIST
            WHERE div_seq = @div_seq AND aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailButtonDto>(sql, new
        {
            div_seq = divSeq,
            aprov_id = aprovId,
            user_id = userId
        });
    }

    /// <inheritdoc />
    public async Task<ApprovalDetailContentDto?> GetDetailContentAsync(string divSeq, string aprovId)
    {
        var sql = @"
            SELECT title, contents
            FROM SPC_APROV_LIST
            WHERE div_seq = @div_seq AND aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailContentDto>(sql, new
        {
            div_seq = divSeq,
            aprov_id = aprovId
        });
    }

    #region B6 New Methods - Approval Actions

    /// <summary>
    /// Executes approval action via unified USP_SPC_APROV_LIST_UPDATE.
    /// DB params: @P_div_seq, @P_Aprrov_id, @P_Aprrov_action_id, @P_Action, @P_update_user_id, @P_description
    /// NOTE: DB uses 'Aprrov' spelling (double 'r').
    /// </summary>
    private async Task<ApprovalActionResponseDto> ExecuteApprovalActionAsync(
        string divSeq,
        string aprovId,
        string aprovActionId,
        string action,
        string userId,
        string? description)
    {
        var result = await QueryFirstOrDefaultAsync<ApprovalActionResponseDto>(
            "USP_SPC_APROV_LIST_UPDATE",
            new
            {
                div_seq = divSeq,
                Aprrov_id = aprovId,
                Aprrov_action_id = aprovActionId,
                Action = action,
                update_user_id = userId,
                description = description ?? string.Empty
            });

        return result ?? new ApprovalActionResponseDto { Result = "SUCCESS", AprovId = aprovId };
    }

    /// <inheritdoc />
    public async Task<ApprovalActionResponseDto> ApproveAsync(
        ApproveRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq,
            request.AprovId,
            string.Empty,
            "APRROV",
            request.UserId,
            request.Comment);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState = "APPROVED";
            result.NewStateName = "승인됨";
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ApprovalActionResponseDto> RejectAsync(
        RejectRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq,
            request.AprovId,
            string.Empty,
            "REJECT",
            request.UserId,
            request.Description);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState = "REJECTED";
            result.NewStateName = "반려됨";
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ApprovalActionResponseDto> CancelAsync(
        CancelApprovalRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq,
            request.AprovId,
            string.Empty,
            "CANCEL",
            request.UserId,
            request.Reason);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState = "CANCELLED";
            result.NewStateName = "취소됨";
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ApprovalListResponseDto> GetListWithPaginationAsync(
        ApprovalFilterDto filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var items = await QueryAsync<ApprovalListDto>(
            "USP_SPC_APROV_LIST_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = filter.DivSeq,
                StartDate = filter.StartDate ?? string.Empty,
                EndDate = filter.EndDate ?? string.Empty,
                User_Id = filter.UserId ?? string.Empty,
                Aprov_Type = string.Empty,
                Chg_Type = string.Empty,
                Aprov_Id = string.Empty,
                Input_Aprov_Id = string.Empty,
                Aprov_State = filter.AprovState ?? string.Empty,
                Aprov_Title = string.Empty,
                Chg_Type_Id = filter.ChgTypeId ?? string.Empty
            });

        var itemList = items.ToList();

        return new ApprovalListResponseDto
        {
            Items = itemList,
            TotalCount = itemList.Count,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    #endregion
}
