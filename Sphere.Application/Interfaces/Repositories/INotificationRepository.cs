using Sphere.Application.DTOs.Notification;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// 알림 Repository 인터페이스
/// </summary>
/// <remarks>
/// SPC_NOTIFY_LIST 테이블에 대한 CRUD 작업을 정의합니다.
/// 알림 발송, 조회, 상태 관리 기능을 제공합니다.
/// </remarks>
public interface INotificationRepository
{
    /// <summary>
    /// 사용자의 알림 목록 조회 (최근 N일)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="days">조회 기간 (일), 기본값: 7일</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>알림 목록</returns>
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(
        string divSeq,
        string userId,
        int days = 7,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 미발송 알림 목록 조회
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>미발송 알림 목록 (send_yn = 'N')</returns>
    Task<IEnumerable<NotificationDto>> GetPendingNotificationsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 알림 발송 상태 업데이트
    /// </summary>
    /// <param name="tableSysId">행 식별자</param>
    /// <param name="success">발송 성공 여부</param>
    /// <param name="errorCode">오류 코드 (실패 시)</param>
    /// <param name="errorMsg">오류 메시지 (실패 시)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>업데이트 성공 여부</returns>
    Task<bool> UpdateNotificationStatusAsync(
        long tableSysId,
        bool success,
        string? errorCode = null,
        string? errorMsg = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 알림 생성
    /// </summary>
    /// <param name="notification">알림 정보</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>생성된 알림의 table_sys_id</returns>
    Task<long> CreateNotificationAsync(
        NotificationDto notification,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 알림 읽음 처리 (use_yn = 'R')
    /// </summary>
    /// <param name="tableSysId">행 식별자</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>처리 성공 여부</returns>
    Task<bool> MarkAsReadAsync(
        long tableSysId,
        CancellationToken cancellationToken = default);
}
