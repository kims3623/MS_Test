namespace Sphere.Application.DTOs.System;

#region Approval Config DTOs

/// <summary>
/// Approval config filter DTO.
/// </summary>
public class ApprovalConfigFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? ApprovalType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Approval config item DTO.
/// </summary>
public class ApprovalConfigItemDto
{
    public string ApprovalConfigId { get; set; } = string.Empty;
    public string ApprovalType { get; set; } = string.Empty;
    public string ApprovalTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinApproverCount { get; set; }
    public int MaxApproverCount { get; set; }
    public string ApprovalMethod { get; set; } = string.Empty;
    public string ApprovalMethodName { get; set; } = string.Empty;
    public string RequireSequential { get; set; } = string.Empty;
    public string AllowSelfApproval { get; set; } = string.Empty;
    public string AllowDelegation { get; set; } = string.Empty;
    public int TimeoutDays { get; set; }
    public string AutoApproveOnTimeout { get; set; } = string.Empty;
    public string SendNotification { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Approval config response DTO.
/// </summary>
public class ApprovalConfigResponseDto
{
    public List<ApprovalConfigItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// Approval config detail DTO.
/// </summary>
public class ApprovalConfigDetailDto
{
    public string ApprovalConfigId { get; set; } = string.Empty;
    public string ApprovalType { get; set; } = string.Empty;
    public string ApprovalTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinApproverCount { get; set; }
    public int MaxApproverCount { get; set; }
    public string ApprovalMethod { get; set; } = string.Empty;
    public string ApprovalMethodName { get; set; } = string.Empty;
    public string RequireSequential { get; set; } = string.Empty;
    public string AllowSelfApproval { get; set; } = string.Empty;
    public string AllowDelegation { get; set; } = string.Empty;
    public int TimeoutDays { get; set; }
    public string AutoApproveOnTimeout { get; set; } = string.Empty;
    public string SendNotification { get; set; } = string.Empty;
    public string NotificationTemplate { get; set; } = string.Empty;
    public List<ApprovalStepDto> Steps { get; set; } = new();
    public string IsActive { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Approval step DTO.
/// </summary>
public class ApprovalStepDto
{
    public int StepOrder { get; set; }
    public string StepName { get; set; } = string.Empty;
    public string ApproverType { get; set; } = string.Empty;
    public string ApproverTypeName { get; set; } = string.Empty;
    public string ApproverId { get; set; } = string.Empty;
    public string ApproverName { get; set; } = string.Empty;
    public string IsRequired { get; set; } = string.Empty;
    public string CanDelegate { get; set; } = string.Empty;
}

/// <summary>
/// Update approval config request DTO.
/// </summary>
public class UpdateApprovalConfigRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string ApprovalConfigId { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? MinApproverCount { get; set; }
    public int? MaxApproverCount { get; set; }
    public string? ApprovalMethod { get; set; }
    public string? RequireSequential { get; set; }
    public string? AllowSelfApproval { get; set; }
    public string? AllowDelegation { get; set; }
    public int? TimeoutDays { get; set; }
    public string? AutoApproveOnTimeout { get; set; }
    public string? SendNotification { get; set; }
    public string? NotificationTemplate { get; set; }
    public List<ApprovalStepDto>? Steps { get; set; }
    public string? IsActive { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update approval config response DTO.
/// </summary>
public class UpdateApprovalConfigResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

#endregion

#region Approval User Relation DTOs

/// <summary>
/// Approval user relation filter DTO.
/// </summary>
public class ApprovalUserRelationFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? ApproverUserId { get; set; }
    public string? RelationType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

/// <summary>
/// Approval user relation item DTO.
/// </summary>
public class ApprovalUserRelationItemDto
{
    public string RelationId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserDeptName { get; set; } = string.Empty;
    public string ApproverUserId { get; set; } = string.Empty;
    public string ApproverUserName { get; set; } = string.Empty;
    public string ApproverDeptName { get; set; } = string.Empty;
    public string RelationType { get; set; } = string.Empty;
    public string RelationTypeName { get; set; } = string.Empty;
    public int Priority { get; set; }
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidTo { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Approval user relation response DTO.
/// </summary>
public class ApprovalUserRelationResponseDto
{
    public List<ApprovalUserRelationItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// Create approval user relation request DTO.
/// </summary>
public class CreateApprovalUserRelationRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ApproverUserId { get; set; } = string.Empty;
    public string RelationType { get; set; } = string.Empty;
    public int Priority { get; set; } = 1;
    public string? ValidFrom { get; set; }
    public string? ValidTo { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Create approval user relation response DTO.
/// </summary>
public class CreateApprovalUserRelationResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string RelationId { get; set; } = string.Empty;
}

/// <summary>
/// Update approval user relation request DTO.
/// </summary>
public class UpdateApprovalUserRelationRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RelationId { get; set; } = string.Empty;
    public string? ApproverUserId { get; set; }
    public string? RelationType { get; set; }
    public int? Priority { get; set; }
    public string? ValidFrom { get; set; }
    public string? ValidTo { get; set; }
    public string? IsActive { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update approval user relation response DTO.
/// </summary>
public class UpdateApprovalUserRelationResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

/// <summary>
/// Delete approval user relation request DTO.
/// </summary>
public class DeleteApprovalUserRelationRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RelationId { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete approval user relation response DTO.
/// </summary>
public class DeleteApprovalUserRelationResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

/// <summary>
/// User approvers DTO - for getting approvers of a specific user.
/// </summary>
public class UserApproversDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public List<ApproverInfoDto> Approvers { get; set; } = new();
}

/// <summary>
/// Approver info DTO.
/// </summary>
public class ApproverInfoDto
{
    public string ApproverUserId { get; set; } = string.Empty;
    public string ApproverUserName { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string PositionName { get; set; } = string.Empty;
    public string RelationType { get; set; } = string.Empty;
    public string RelationTypeName { get; set; } = string.Empty;
    public int Priority { get; set; }
}

#endregion

#region Approval Management DTOs (for SPH5113)

/// <summary>
/// Pending approval filter DTO.
/// </summary>
public class PendingApprovalFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? ApprovalType { get; set; }
    public string? RequesterId { get; set; }
    public string? Status { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Pending approval item DTO.
/// </summary>
public class PendingApprovalItemDto
{
    public string ApprovalId { get; set; } = string.Empty;
    public string ApprovalNo { get; set; } = string.Empty;
    public string ApprovalType { get; set; } = string.Empty;
    public string ApprovalTypeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RequesterId { get; set; } = string.Empty;
    public string RequesterName { get; set; } = string.Empty;
    public string RequesterDeptName { get; set; } = string.Empty;
    public string RequestDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public int CurrentStep { get; set; }
    public int TotalSteps { get; set; }
    public string DueDate { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string PriorityName { get; set; } = string.Empty;
}

/// <summary>
/// Pending approval response DTO.
/// </summary>
public class PendingApprovalResponseDto
{
    public List<PendingApprovalItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PendingCount { get; set; }
    public int OverdueCount { get; set; }
}

/// <summary>
/// Approval detail for management DTO.
/// </summary>
public class ApprovalManagementDetailDto
{
    public string ApprovalId { get; set; } = string.Empty;
    public string ApprovalNo { get; set; } = string.Empty;
    public string ApprovalType { get; set; } = string.Empty;
    public string ApprovalTypeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RequesterId { get; set; } = string.Empty;
    public string RequesterName { get; set; } = string.Empty;
    public string RequesterDeptName { get; set; } = string.Empty;
    public string RequestDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public int CurrentStep { get; set; }
    public int TotalSteps { get; set; }
    public string DueDate { get; set; } = string.Empty;
    public string CompletedDate { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public string TargetId { get; set; } = string.Empty;
    public List<ApprovalHistoryItemDto> History { get; set; } = new();
    public List<ApprovalStepStatusDto> Steps { get; set; } = new();
}

/// <summary>
/// Approval history item DTO.
/// </summary>
public class ApprovalHistoryItemDto
{
    public int StepOrder { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string ActionTypeName { get; set; } = string.Empty;
    public string ActionUserId { get; set; } = string.Empty;
    public string ActionUserName { get; set; } = string.Empty;
    public string ActionDate { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}

/// <summary>
/// Approval step status DTO.
/// </summary>
public class ApprovalStepStatusDto
{
    public int StepOrder { get; set; }
    public string StepName { get; set; } = string.Empty;
    public string ApproverUserId { get; set; } = string.Empty;
    public string ApproverUserName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string ActionDate { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}

#endregion
