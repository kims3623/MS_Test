using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.SendAlarmNotification;

/// <summary>
/// Handler for SendAlarmNotificationCommand.
/// </summary>
public class SendAlarmNotificationCommandHandler : IRequestHandler<SendAlarmNotificationCommand, Result<SendAlarmNotificationResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<SendAlarmNotificationCommandHandler> _logger;

    public SendAlarmNotificationCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<SendAlarmNotificationCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<SendAlarmNotificationResponseDto>> Handle(SendAlarmNotificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Sending alarm notification for AlmSysId={AlmSysId} to {RecipientCount} recipients",
            request.AlmSysId, request.RecipientUserIds.Count);

        try
        {
            // Get alarm detail for notification content
            var alarmDetail = await _alarmRepository.GetDetailAsync(
                request.DivSeq,
                request.AlmSysId,
                cancellationToken);

            if (alarmDetail == null)
            {
                return Result<SendAlarmNotificationResponseDto>.Failure("알람을 찾을 수 없습니다.");
            }

            int sentCount = 0;
            int failedCount = 0;
            var failedRecipients = new List<string>();

            // In production, this would integrate with notification service
            // For now, simulate sending notifications
            foreach (var userId in request.RecipientUserIds)
            {
                try
                {
                    // Simulate notification sending
                    // In real implementation, call notification service here
                    _logger.LogDebug(
                        "Sending notification to user {UserId} for alarm {AlmSysId}",
                        userId, request.AlmSysId);

                    sentCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to send notification to user {UserId}", userId);
                    failedCount++;
                    failedRecipients.Add(userId);
                }
            }

            var response = new SendAlarmNotificationResponseDto
            {
                Result = failedCount == 0 ? "SUCCESS" : "PARTIAL",
                ResultMessage = failedCount == 0
                    ? $"{sentCount}명에게 알림을 발송했습니다."
                    : $"{sentCount}명 발송 성공, {failedCount}명 발송 실패",
                SentCount = sentCount,
                FailedCount = failedCount,
                FailedRecipients = failedRecipients
            };

            _logger.LogInformation(
                "Notification sent: AlmSysId={AlmSysId}, Sent={SentCount}, Failed={FailedCount}",
                request.AlmSysId, sentCount, failedCount);

            return Result<SendAlarmNotificationResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending alarm notification for {AlmSysId}", request.AlmSysId);
            return Result<SendAlarmNotificationResponseDto>.Failure($"알람 알림 발송 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
