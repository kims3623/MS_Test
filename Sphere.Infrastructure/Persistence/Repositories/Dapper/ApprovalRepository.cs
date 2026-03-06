using System.Data;
using Dapper;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// 결재 리포지토리 - Dapper(SP + raw SQL) 구현체
/// </summary>
public class ApprovalRepository : DapperRepositoryBase, IApprovalRepository
{
    public ApprovalRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<ApprovalListDto>> GetListAsync(ApprovalFilterDto filter)
    {
        return await QueryAsync<ApprovalListDto>(
            "USP_SPC_APROV_LIST_SELECT",
            new
            {
                Lang_Type      = "ko-KR",
                div_seq        = filter.DivSeq,
                StartDate      = filter.StartDate ?? string.Empty,
                EndDate        = filter.EndDate ?? string.Empty,
                User_Id        = filter.UserId ?? string.Empty,
                Aprov_Type     = string.Empty,
                Chg_Type       = string.Empty,
                Aprov_Id       = string.Empty,
                Input_Aprov_Id = string.Empty,
                Aprov_State    = filter.AprovState ?? string.Empty,
                Aprov_Title    = string.Empty,
                Chg_Type_Id    = filter.ChgTypeId ?? string.Empty
            });
    }

    public async Task<IEnumerable<ApprovalDefaultUserDto>> GetDefaultUsersAsync(
        string divSeq, string? chgTypeId = null, string? vendorId = null, string? mtrlClassId = null)
    {
        return await QueryAsync<ApprovalDefaultUserDto>(
            "USP_SPC_APROV_DEFAULT_USER_SELECT",
            new
            {
                div_seq      = divSeq,
                chg_type_id  = chgTypeId,
                vendor_id    = vendorId,
                mtrl_class_id = mtrlClassId
            });
    }

    public async Task<ApprovalInsertResultDto> InsertAsync(ApprovalCreateDto dto)
    {
        var result = await QueryFirstOrDefaultAsync<ApprovalInsertResultDto>(
            "USP_SPC_APROV_LIST_INSERT",
            new
            {
                div_seq          = dto.DivSeq,
                chg_type_id      = dto.ChgTypeId,
                aprov_action_id  = dto.AprovActionId,
                writer           = dto.Writer,
                title            = dto.Title,
                contents         = dto.Contents,
                user_list        = dto.UserList,
                batch_id         = dto.BatchId,
                alm_sys_id       = dto.AlmSysId,
                alm_action_id    = dto.AlmActionId,
                create_user_id   = dto.CreateUserId
            });

        return result ?? new ApprovalInsertResultDto { Result = "FAIL", ResultMessage = "No result returned" };
    }

    public async Task<ApprovalDetailDto?> GetDetailAsync(string divSeq, string aprovId)
    {
        const string sql = @"
            SELECT al.aprov_id, al.div_seq, al.chg_type_id,
                ct.code_name_k   AS chg_type_name,
                al.aprov_action_id,
                aa.code_name_k   AS aprov_action_name,
                aas.code_name_k  AS aprov_action_state_name,
                al.user_list, al.update_user_id,
                CONVERT(VARCHAR(20), al.update_date, 120) AS update_date,
                al.description,
                al.title, al.contents, al.writer, al.aprov_state,
                CONVERT(VARCHAR(20), al.create_date, 120) AS create_date
            FROM SPC_APROV_LIST al
            LEFT JOIN SPC_CODE_MST ct
                ON al.div_seq = ct.div_seq AND al.chg_type_id = ct.code_id AND ct.code_class_id = 'CHG_TYPE'
            LEFT JOIN SPC_CODE_MST aa
                ON al.div_seq = aa.div_seq AND al.aprov_action_id = aa.code_id AND aa.code_class_id = 'APROV_ACTION'
            LEFT JOIN SPC_CODE_MST aas
                ON al.div_seq = aas.div_seq AND al.aprov_action_state = aas.code_id AND aas.code_class_id = 'APROV_ACTION_STATE'
            WHERE al.div_seq = @div_seq AND al.aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailDto>(sql, new { div_seq = divSeq, aprov_id = aprovId });
    }

    public async Task<ApprovalDetailButtonDto?> GetDetailButtonAsync(string divSeq, string aprovId, string userId)
    {
        const string sql = @"
            SELECT
                CASE WHEN writer = @user_id AND aprov_state = 'PENDING' THEN 'Y' ELSE 'N' END AS cbutton_flag,
                CASE WHEN user_list LIKE '%' + @user_id + '%' AND aprov_state = 'PENDING' THEN 'Y' ELSE 'N' END AS arbutton_flag
            FROM SPC_APROV_LIST
            WHERE div_seq = @div_seq AND aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailButtonDto>(sql, new { div_seq = divSeq, aprov_id = aprovId, user_id = userId });
    }

    public async Task<ApprovalDetailContentDto?> GetDetailContentAsync(string divSeq, string aprovId)
    {
        const string sql = @"
            SELECT title, contents
            FROM SPC_APROV_LIST
            WHERE div_seq = @div_seq AND aprov_id = @aprov_id";

        return await QueryFirstOrDefaultRawSqlAsync<ApprovalDetailContentDto>(sql, new { div_seq = divSeq, aprov_id = aprovId });
    }

    #region 결재 액션 (USP_SPC_APROV_LIST_UPDATE)

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
                div_seq         = divSeq,
                Aprrov_id       = aprovId,
                Aprrov_action_id = aprovActionId,
                Action          = action,
                update_user_id  = userId,
                description     = description ?? string.Empty
            });

        return result ?? new ApprovalActionResponseDto { Result = "SUCCESS", AprovId = aprovId };
    }

    public async Task<ApprovalActionResponseDto> ApproveAsync(
        ApproveRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq, request.AprovId, string.Empty, "APRROV", request.UserId, request.Comment);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState     = "APPROVED";
            result.NewStateName = "승인됨";
        }
        return result;
    }

    public async Task<ApprovalActionResponseDto> RejectAsync(
        RejectRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq, request.AprovId, string.Empty, "REJECT", request.UserId, request.Description);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState     = "REJECTED";
            result.NewStateName = "반려됨";
        }
        return result;
    }

    public async Task<ApprovalActionResponseDto> CancelAsync(
        CancelApprovalRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await ExecuteApprovalActionAsync(
            request.DivSeq, request.AprovId, string.Empty, "CANCEL", request.UserId, request.Reason);

        if (string.IsNullOrEmpty(result.NewState))
        {
            result.NewState     = "CANCELLED";
            result.NewStateName = "취소됨";
        }
        return result;
    }

    public async Task<ApprovalListResponseDto> GetListWithPaginationAsync(
        ApprovalFilterDto filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var items = (await QueryAsync<ApprovalListDto>(
            "USP_SPC_APROV_LIST_SELECT",
            new
            {
                Lang_Type      = "ko-KR",
                div_seq        = filter.DivSeq,
                StartDate      = filter.StartDate ?? string.Empty,
                EndDate        = filter.EndDate ?? string.Empty,
                User_Id        = filter.UserId ?? string.Empty,
                Aprov_Type     = string.Empty,
                Chg_Type       = string.Empty,
                Aprov_Id       = string.Empty,
                Input_Aprov_Id = string.Empty,
                Aprov_State    = filter.AprovState ?? string.Empty,
                Aprov_Title    = string.Empty,
                Chg_Type_Id    = filter.ChgTypeId ?? string.Empty
            })).ToList();

        return new ApprovalListResponseDto
        {
            Items      = items,
            TotalCount = items.Count,
            PageNumber = pageNumber,
            PageSize   = pageSize
        };
    }

    #endregion

    #region 직접 SQL (EF 대체용)

    public async Task<int> UpdateApprovalStateAsync(
        string divSeq,
        string aprovId,
        string newState,
        string updateUserId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_APROV_LIST
            SET APROV_STATE    = @NewState,
                UPDATE_USER_ID = @UpdateUserId,
                UPDATE_DATE    = GETDATE()
            WHERE DIV_SEQ  = @DivSeq
              AND APROV_ID = @AprovId";

        return await _connection.ExecuteAsync(
            new CommandDefinition(sql,
                new { DivSeq = divSeq, AprovId = aprovId, NewState = newState, UpdateUserId = updateUserId },
                cancellationToken: cancellationToken));
    }

    public async Task InsertApprovalHistoryAsync(
        string divSeq,
        string aprovId,
        string action,
        string approverId,
        DateTime actionDate,
        string? comment,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO SPC_APROV_HIST
                (DIV_SEQ, APROV_ID, ACTION, APPROVER_ID, ACTION_DATE, COMMENT, CREATE_USER_ID, CREATE_DATE)
            VALUES
                (@DivSeq, @AprovId, @Action, @ApproverId, @ActionDate, @Comment, @ApproverId, GETDATE())";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql,
                new { DivSeq = divSeq, AprovId = aprovId, Action = action, ApproverId = approverId, ActionDate = actionDate, Comment = comment ?? string.Empty },
                cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<ApprovalHistoryItemDto>> GetApprovalHistoriesAsync(
        string divSeq,
        string aprovId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                HIST_SEQ     AS Seq,
                APPROVER_ID  AS UserId,
                APPROVER_NAME AS UserName,
                ACTION       AS Action,
                CONVERT(VARCHAR(20), ACTION_DATE, 120) AS ActionDate,
                COMMENT      AS Comment
            FROM SPC_APROV_HIST
            WHERE DIV_SEQ  = @DivSeq
              AND APROV_ID = @AprovId
            ORDER BY HIST_SEQ";

        return await _connection.QueryAsync<ApprovalHistoryItemDto>(
            new CommandDefinition(sql, new { DivSeq = divSeq, AprovId = aprovId }, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<AttachmentInfoDto>> GetApprovalAttachmentsAsync(
        string aprovId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                ATTACHMENT_ID    AS FileId,
                ORIGINAL_FILE_NAME AS FileName,
                FILE_SIZE        AS FileSize,
                FILE_PATH        AS FileUrl
            FROM SPC_APROV_ATTACHMENT
            WHERE APROV_ID = @AprovId
              AND USE_YN   = 'Y'
            ORDER BY UPLOAD_DATE";

        return await _connection.QueryAsync<AttachmentInfoDto>(
            new CommandDefinition(sql, new { AprovId = aprovId }, cancellationToken: cancellationToken));
    }

    #endregion
}
