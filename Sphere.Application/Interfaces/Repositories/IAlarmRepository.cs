using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Alarm repository interface for stored procedure operations.
/// </summary>
public interface IAlarmRepository
{
    /// <summary>
    /// Gets alarm action users. (USP_SPC_ALARM_ACTION_USER_SELECT)
    /// </summary>
    Task<IEnumerable<AlarmActionUserDto>> GetActionUsersAsync(AlarmActionUserQueryDto query);

    /// <summary>
    /// Gets alarm issue first action info. (USP_SPC_ALARM_ISSUE_FIRST_ACTION)
    /// </summary>
    Task<AlarmIssueFirstActionDto?> GetIssueFirstActionAsync(string divSeq, string almSysId);

    /// <summary>
    /// Gets alarm actions for a specific alarm.
    /// </summary>
    Task<IEnumerable<AlarmActionDto>> GetAlarmActionsAsync(
        string divSeq,
        string alarmSysId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes/stops an alarm. (USP_SPC_SPH4020_STOP)
    /// </summary>
    Task<(bool Success, string? Message, string? NewStatus)> CloseAlarmAsync(
        string divSeq,
        string alarmSysId,
        string actionId,
        string stopReason,
        string userId,
        List<string>? customerIds,
        CancellationToken cancellationToken = default);

    #region B6 New Methods - Alarm List

    /// <summary>
    /// Gets alarm list with filter and pagination.
    /// </summary>
    Task<AlarmListResponseDto> GetListAsync(
        AlarmListFilterDto filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets alarm detail by ID.
    /// </summary>
    Task<AlarmDetailDto?> GetDetailAsync(
        string divSeq,
        string almSysId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new alarm.
    /// </summary>
    Task<CreateAlarmResponseDto> CreateAsync(
        CreateAlarmRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an alarm.
    /// </summary>
    Task<UpdateAlarmResponseDto> UpdateAsync(
        UpdateAlarmRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion

    #region B6 New Methods - Alarm History

    /// <summary>
    /// Gets alarm history timeline.
    /// </summary>
    Task<AlarmHistoryResponseDto> GetHistoryAsync(
        AlarmHistoryQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets alarm statistics.
    /// </summary>
    Task<AlarmStatisticsDto> GetStatisticsAsync(
        string divSeq,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets alarm trend data.
    /// </summary>
    Task<AlarmTrendResponseDto> GetTrendAsync(
        AlarmTrendQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region B6 New Methods - Alarm Attachments

    /// <summary>
    /// Gets alarm attachments.
    /// </summary>
    Task<AlarmAttachmentListResponseDto> GetAttachmentsAsync(
        AlarmAttachmentQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads an attachment to alarm.
    /// </summary>
    Task<UploadAlarmAttachmentResponseDto> UploadAttachmentAsync(
        UploadAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an alarm attachment.
    /// </summary>
    Task<DeleteAlarmAttachmentResponseDto> DeleteAttachmentAsync(
        DeleteAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads an alarm attachment.
    /// </summary>
    Task<DownloadAlarmAttachmentResponseDto?> DownloadAttachmentAsync(
        DownloadAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion

    #region B6 New Methods - Alarm Actions

    /// <summary>
    /// Executes an alarm action.
    /// </summary>
    Task<ExecuteAlarmActionResponseDto> ExecuteActionAsync(
        ExecuteAlarmActionRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Merges multiple alarms into one.
    /// </summary>
    Task<MergeAlarmResponseDto> MergeAlarmsAsync(
        MergeAlarmRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion
}
