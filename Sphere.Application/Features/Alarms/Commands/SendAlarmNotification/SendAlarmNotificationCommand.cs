using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Alarms.Commands.SendAlarmNotification;

/// <summary>
/// Command to send alarm notification to users.
/// </summary>
public record SendAlarmNotificationCommand : IRequest<Result<SendAlarmNotificationResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public List<string> RecipientUserIds { get; init; } = new();
    public string? Subject { get; init; }
    public string? Message { get; init; }
    public bool SendEmail { get; init; } = true;
    public bool SendPush { get; init; } = false;
    public string SenderUserId { get; init; } = string.Empty;
}

/// <summary>
/// Response DTO for send alarm notification.
/// </summary>
public class SendAlarmNotificationResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public int SentCount { get; set; }
    public int FailedCount { get; set; }
    public List<string> FailedRecipients { get; set; } = new();
}
