using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// 결재 리포지토리 인터페이스
/// </summary>
public interface IApprovalRepository
{
    /// <summary>
    /// 결재 목록 조회 (USP_SPC_APROV_LIST_SELECT)
    /// </summary>
    Task<IEnumerable<ApprovalListDto>> GetListAsync(ApprovalFilterDto filter);

    /// <summary>
    /// 기본 결재자 조회 (USP_SPC_APROV_DEFAULT_USER_SELECT)
    /// </summary>
    Task<IEnumerable<ApprovalDefaultUserDto>> GetDefaultUsersAsync(
        string divSeq, string? chgTypeId = null, string? vendorId = null, string? mtrlClassId = null);

    /// <summary>
    /// 결재 등록 (USP_SPC_APROV_LIST_INSERT)
    /// </summary>
    Task<ApprovalInsertResultDto> InsertAsync(ApprovalCreateDto dto);

    /// <summary>
    /// 결재 단건 조회 (raw SQL)
    /// </summary>
    Task<ApprovalDetailDto?> GetDetailAsync(string divSeq, string apovId);

    /// <summary>
    /// 결재 버튼 플래그 조회 (raw SQL)
    /// </summary>
    Task<ApprovalDetailButtonDto?> GetDetailButtonAsync(string divSeq, string aprovId, string userId);

    /// <summary>
    /// 결재 내용 조회 (raw SQL)
    /// </summary>
    Task<ApprovalDetailContentDto?> GetDetailContentAsync(string divSeq, string aprovId);

    /// <summary>
    /// 승인 처리 (USP_SPC_APROV_LIST_UPDATE)
    /// </summary>
    Task<ApprovalActionResponseDto> ApproveAsync(
        ApproveRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 반려 처리 (USP_SPC_APROV_LIST_UPDATE)
    /// </summary>
    Task<ApprovalActionResponseDto> RejectAsync(
        RejectRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 취소 처리 (USP_SPC_APROV_LIST_UPDATE)
    /// </summary>
    Task<ApprovalActionResponseDto> CancelAsync(
        CancelApprovalRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 페이징 결재 목록 조회
    /// </summary>
    Task<ApprovalListResponseDto> GetListWithPaginationAsync(
        ApprovalFilterDto filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 결재 상태 직접 업데이트 (raw SQL) - 배치 승인 등 EF 대체
    /// </summary>
    Task<int> UpdateApprovalStateAsync(
        string divSeq,
        string aprovId,
        string newState,
        string updateUserId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 결재 이력 추가 (raw SQL)
    /// </summary>
    Task InsertApprovalHistoryAsync(
        string divSeq,
        string aprovId,
        string action,
        string approverId,
        DateTime actionDate,
        string? comment,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 결재 이력 목록 조회 (raw SQL)
    /// </summary>
    Task<IEnumerable<ApprovalHistoryItemDto>> GetApprovalHistoriesAsync(
        string divSeq,
        string aprovId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 결재 첨부파일 목록 조회 (raw SQL)
    /// </summary>
    Task<IEnumerable<AttachmentInfoDto>> GetApprovalAttachmentsAsync(
        string aprovId,
        CancellationToken cancellationToken = default);
}
