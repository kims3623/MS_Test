using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Approval repository interface for stored procedure operations.
/// </summary>
public interface IApprovalRepository
{
    /// <summary>
    /// Gets approval list by filter. (USP_SPC_APROV_LIST_SELECT)
    /// </summary>
    Task<IEnumerable<ApprovalListDto>> GetListAsync(ApprovalFilterDto filter);

    /// <summary>
    /// Gets default approval users. (USP_SPC_APROV_DEFAULT_USER_SELECT)
    /// </summary>
    Task<IEnumerable<ApprovalDefaultUserDto>> GetDefaultUsersAsync(
        string divSeq, string? chgTypeId = null, string? vendorId = null, string? mtrlClassId = null);

    /// <summary>
    /// Inserts new approval. (USP_SPC_APROV_LIST_INSERT)
    /// </summary>
    Task<ApprovalInsertResultDto> InsertAsync(ApprovalCreateDto dto);

    /// <summary>
    /// Gets approval detail. (TODO [P3]: No DB USP - use USP_SPC_APROV_LIST_SELECT)
    /// </summary>
    Task<ApprovalDetailDto?> GetDetailAsync(string divSeq, string apovId);

    /// <summary>
    /// Gets approval detail button flags. (TODO [P3]: No DB USP - auth-based button logic)
    /// </summary>
    Task<ApprovalDetailButtonDto?> GetDetailButtonAsync(string divSeq, string aprovId, string userId);

    /// <summary>
    /// Gets approval detail content. (TODO [P3]: No DB USP - use USP_SPC_APROV_LIST_SELECT)
    /// </summary>
    Task<ApprovalDetailContentDto?> GetDetailContentAsync(string divSeq, string aprovId);

    #region B6 New Methods - Approval Actions

    /// <summary>
    /// Approves a pending approval request.
    /// </summary>
    Task<ApprovalActionResponseDto> ApproveAsync(
        ApproveRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Rejects a pending approval request.
    /// </summary>
    Task<ApprovalActionResponseDto> RejectAsync(
        RejectRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a pending approval request (by writer only).
    /// </summary>
    Task<ApprovalActionResponseDto> CancelAsync(
        CancelApprovalRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets approval list with pagination support.
    /// </summary>
    Task<ApprovalListResponseDto> GetListWithPaginationAsync(
        ApprovalFilterDto filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    #endregion
}
