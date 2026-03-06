namespace Sphere.Application.DTOs.Approval;

/// <summary>
/// Approval list query result DTO.
/// </summary>
public class ApprovalListDto
{
    public string AprovId { get; set; } = string.Empty;
    public string ChgTypeName { get; set; } = string.Empty;
    public string AprovActionId { get; set; } = string.Empty;
    public string AprovActionName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string AprovStateName { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
}

/// <summary>
/// Approval filter DTO.
/// </summary>
public class ApprovalFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? AprovState { get; set; }
    public string? ChgTypeId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}

/// <summary>
/// Approval default user DTO.
/// </summary>
public class ApprovalDefaultUserDto
{
    public string DivSeq { get; set; } = string.Empty;
    public int Seq { get; set; }
    public string ChgTypeId { get; set; } = string.Empty;
    public string AprovActionId { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public string UserList { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string ActiName { get; set; } = string.Empty;
    public string OriginActiName { get; set; } = string.Empty;
    public string ReasonCode { get; set; } = string.Empty;
    public string ParallGroup { get; set; } = string.Empty;
}

/// <summary>
/// Approval insert result DTO.
/// </summary>
public class ApprovalInsertResultDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string NotiFlag { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public string ChgTypeId { get; set; } = string.Empty;
    public string AprovActionId { get; set; } = string.Empty;
    public string AprovState { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string UserList { get; set; } = string.Empty;
    public string BatchId { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
}

/// <summary>
/// Approval create DTO.
/// </summary>
public class ApprovalCreateDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string ChgTypeId { get; set; } = string.Empty;
    public string AprovActionId { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string UserList { get; set; } = string.Empty;
    public string? BatchId { get; set; }
    public string? AlmSysId { get; set; }
    public string? AlmActionId { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Approval detail DTO.
/// </summary>
public class ApprovalDetailDto
{
    public string AprovId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string ChgTypeId { get; set; } = string.Empty;
    public string ChgTypeName { get; set; } = string.Empty;
    public string AprovActionId { get; set; } = string.Empty;
    public string AprovActionName { get; set; } = string.Empty;
    public string AprovActionStateName { get; set; } = string.Empty;
    public string UserList { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public string AprovState { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
}

/// <summary>
/// Approval detail button DTO.
/// Maps to USP_SPC_APROV_LIST_SELECT_DETAIL_BUTTON output.
/// </summary>
public class ApprovalDetailButtonDto
{
    public string CButtonFlag { get; set; } = "N";
    public string ARButtonFlag { get; set; } = "N";

    /// <summary>
    /// Can cancel - only for writer and pending state.
    /// </summary>
    public bool CanCancel => CButtonFlag == "Y";

    /// <summary>
    /// Can approve or reject - for approvers and pending state.
    /// </summary>
    public bool CanApproveReject => ARButtonFlag == "Y";
}

/// <summary>
/// Approval detail content DTO.
/// </summary>
public class ApprovalDetailContentDto
{
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
}

/// <summary>
/// Approval content detail DTO for ApporvContentPopup.
/// </summary>
public class ApprovalContentDto
{
    public string AprovId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string ChgTypeId { get; set; } = string.Empty;
    public string ChgTypeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string Requestor { get; set; } = string.Empty;
    public string RequestorName { get; set; } = string.Empty;
    public string RequestDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public List<ApprovalHistoryItemDto> ApprovalHistory { get; set; } = new();
    public List<AttachmentInfoDto>? Attachments { get; set; }
}

/// <summary>
/// Approval history item DTO.
/// </summary>
public class ApprovalHistoryItemDto
{
    public int Seq { get; set; }
    public string ActionDate { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public string? Comment { get; set; }
}

/// <summary>
/// Attachment info DTO.
/// </summary>
public class AttachmentInfoDto
{
    public string FileId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileUrl { get; set; } = string.Empty;
}

/// <summary>
/// Approve request DTO.
/// </summary>
public class ApproveRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Comment { get; set; }
}

/// <summary>
/// Reject request DTO.
/// </summary>
public class RejectRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Cancel request DTO.
/// </summary>
public class CancelApprovalRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Reason { get; set; }
}

/// <summary>
/// Approval action response DTO.
/// </summary>
public class ApprovalActionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string NewState { get; set; } = string.Empty;
    public string NewStateName { get; set; } = string.Empty;
}

/// <summary>
/// Approval default user query parameters.
/// </summary>
public class ApprovalDefaultUserQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? ChgTypeId { get; set; }
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
}

/// <summary>
/// Approval list response with pagination.
/// </summary>
public class ApprovalListResponseDto
{
    public List<ApprovalListDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

/// <summary>
/// Approval detail list response (for batch detail retrieval).
/// </summary>
public class ApprovalDetailListResponseDto
{
    public string AprovId { get; set; } = string.Empty;
    public List<ApprovalDetailListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Individual item in approval detail list.
/// </summary>
public class ApprovalDetailListItemDto
{
    public string AprovId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string ApproverId { get; set; } = string.Empty;
    public string ActionDate { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}

/// <summary>
/// Batch approve response DTO.
/// </summary>
public class BatchApproveResponseDto
{
    public int SuccessCount { get; set; }
    public int FailCount { get; set; }
    public List<string> Errors { get; set; } = new();
}
