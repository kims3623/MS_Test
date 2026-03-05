using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Notification;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Dapper implementation of INotificationRepository.
/// Manages notifications from SPC_NOTIFY_LIST table.
/// </summary>
public class NotificationRepository : DapperRepositoryBase, INotificationRepository
{
    private readonly ILogger<NotificationRepository> _logger;

    public NotificationRepository(
        IDbConnection connection,
        ILogger<NotificationRepository> logger)
        : base(connection)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(
        string divSeq,
        string userId,
        int days = 7,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Getting notifications for DivSeq: {DivSeq}, UserId: {UserId}, Days: {Days}",
            divSeq, userId, days);

        try
        {
            var sql = @"
                SELECT
                    table_sys_id AS TableSysId,
                    div_seq AS DivSeq,
                    module_id AS ModuleId,
                    noti_type_id AS NotiTypeId,
                    alm_sys_id AS AlmSysId,
                    alm_action_id AS AlmActionId,
                    aprov_id AS AprovId,
                    aprov_action_id AS AprovActionId,
                    receiver AS Receiver,
                    title AS Title,
                    contents AS Contents,
                    send_yn AS SendYn,
                    error_yn AS ErrorYn,
                    error_code AS ErrorCode,
                    error_msg AS ErrorMsg,
                    use_yn AS UseYn,
                    acti_name AS ActiName,
                    origin_acti_name AS OriginActiName,
                    reason_code AS ReasonCode,
                    description AS Description,
                    create_user_id AS CreateUserId,
                    create_date AS CreateDate,
                    update_user_id AS UpdateUserId,
                    update_date AS UpdateDate
                FROM SPC_NOTIFY_LIST
                WHERE div_seq = @DivSeq
                  AND (receiver LIKE '%' + @UserId + '%' OR create_user_id = @UserId)
                  AND create_date >= DATEADD(DAY, -@Days, GETDATE())
                  AND use_yn = 'Y'
                ORDER BY create_date DESC";

            var result = await _connection.QueryAsync<NotificationDto>(
                sql,
                new { DivSeq = divSeq, UserId = userId, Days = days });

            _logger.LogDebug(
                "Found {Count} notifications for UserId: {UserId}",
                result.Count(), userId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting notifications for DivSeq: {DivSeq}, UserId: {UserId}",
                divSeq, userId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<NotificationDto>> GetPendingNotificationsAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting pending notifications (send_yn = 'N')");

        try
        {
            var sql = @"
                SELECT
                    table_sys_id AS TableSysId,
                    div_seq AS DivSeq,
                    module_id AS ModuleId,
                    noti_type_id AS NotiTypeId,
                    alm_sys_id AS AlmSysId,
                    alm_action_id AS AlmActionId,
                    aprov_id AS AprovId,
                    aprov_action_id AS AprovActionId,
                    receiver AS Receiver,
                    title AS Title,
                    contents AS Contents,
                    send_yn AS SendYn,
                    error_yn AS ErrorYn,
                    error_code AS ErrorCode,
                    error_msg AS ErrorMsg,
                    use_yn AS UseYn,
                    acti_name AS ActiName,
                    origin_acti_name AS OriginActiName,
                    reason_code AS ReasonCode,
                    description AS Description,
                    create_user_id AS CreateUserId,
                    create_date AS CreateDate,
                    update_user_id AS UpdateUserId,
                    update_date AS UpdateDate
                FROM SPC_NOTIFY_LIST
                WHERE send_yn = 'N'
                  AND error_yn = 'N'
                  AND use_yn = 'Y'
                ORDER BY create_date ASC";

            var result = await _connection.QueryAsync<NotificationDto>(sql);

            _logger.LogDebug("Found {Count} pending notifications", result.Count());

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting pending notifications");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdateNotificationStatusAsync(
        long tableSysId,
        bool success,
        string? errorCode = null,
        string? errorMsg = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Updating notification status for TableSysId: {TableSysId}, Success: {Success}",
            tableSysId, success);

        try
        {
            var sql = @"
                UPDATE SPC_NOTIFY_LIST
                SET send_yn = @SendYn,
                    error_yn = @ErrorYn,
                    error_code = @ErrorCode,
                    error_msg = @ErrorMsg,
                    update_date = GETDATE()
                WHERE table_sys_id = @TableSysId";

            var affected = await _connection.ExecuteAsync(sql, new
            {
                TableSysId = tableSysId,
                SendYn = success ? "Y" : "N",
                ErrorYn = success ? "N" : "Y",
                ErrorCode = errorCode ?? string.Empty,
                ErrorMsg = errorMsg ?? string.Empty
            });

            if (affected > 0)
            {
                _logger.LogInformation(
                    "Updated notification status for TableSysId: {TableSysId}, Success: {Success}",
                    tableSysId, success);
            }
            else
            {
                _logger.LogWarning(
                    "No notification found with TableSysId: {TableSysId}",
                    tableSysId);
            }

            return affected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error updating notification status for TableSysId: {TableSysId}",
                tableSysId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<long> CreateNotificationAsync(
        NotificationDto notification,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Creating notification for Receiver: {Receiver}, Title: {Title}",
            notification.Receiver, notification.Title);

        try
        {
            var sql = @"
                INSERT INTO SPC_NOTIFY_LIST (
                    div_seq,
                    module_id,
                    noti_type_id,
                    alm_sys_id,
                    alm_action_id,
                    aprov_id,
                    aprov_action_id,
                    receiver,
                    title,
                    contents,
                    send_yn,
                    error_yn,
                    error_code,
                    error_msg,
                    use_yn,
                    acti_name,
                    origin_acti_name,
                    reason_code,
                    description,
                    create_user_id,
                    create_date,
                    update_user_id,
                    update_date
                )
                OUTPUT INSERTED.table_sys_id
                VALUES (
                    @DivSeq,
                    @ModuleId,
                    @NotiTypeId,
                    @AlmSysId,
                    @AlmActionId,
                    @AprovId,
                    @AprovActionId,
                    @Receiver,
                    @Title,
                    @Contents,
                    @SendYn,
                    @ErrorYn,
                    @ErrorCode,
                    @ErrorMsg,
                    @UseYn,
                    @ActiName,
                    @OriginActiName,
                    @ReasonCode,
                    @Description,
                    @CreateUserId,
                    GETDATE(),
                    @UpdateUserId,
                    GETDATE()
                )";

            var tableSysId = await _connection.ExecuteScalarAsync<long>(sql, new
            {
                notification.DivSeq,
                notification.ModuleId,
                notification.NotiTypeId,
                notification.AlmSysId,
                notification.AlmActionId,
                notification.AprovId,
                notification.AprovActionId,
                notification.Receiver,
                notification.Title,
                notification.Contents,
                notification.SendYn,
                notification.ErrorYn,
                notification.ErrorCode,
                notification.ErrorMsg,
                notification.UseYn,
                notification.ActiName,
                notification.OriginActiName,
                notification.ReasonCode,
                notification.Description,
                notification.CreateUserId,
                notification.UpdateUserId
            });

            _logger.LogInformation(
                "Created notification with TableSysId: {TableSysId} for Receiver: {Receiver}",
                tableSysId, notification.Receiver);

            return tableSysId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating notification for Receiver: {Receiver}",
                notification.Receiver);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> MarkAsReadAsync(
        long tableSysId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Marking notification as read for TableSysId: {TableSysId}",
            tableSysId);

        try
        {
            var sql = @"
                UPDATE SPC_NOTIFY_LIST
                SET use_yn = 'R',
                    update_date = GETDATE()
                WHERE table_sys_id = @TableSysId
                  AND use_yn = 'Y'";

            var affected = await _connection.ExecuteAsync(sql, new { TableSysId = tableSysId });

            if (affected > 0)
            {
                _logger.LogInformation(
                    "Marked notification as read for TableSysId: {TableSysId}",
                    tableSysId);
            }
            else
            {
                _logger.LogWarning(
                    "No active notification found with TableSysId: {TableSysId}",
                    tableSysId);
            }

            return affected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error marking notification as read for TableSysId: {TableSysId}",
                tableSysId);
            throw;
        }
    }
}
