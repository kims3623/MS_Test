using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmAttachments;

/// <summary>
/// Query to get alarm attachments.
/// </summary>
public record GetAlarmAttachmentsQuery : IRequest<Result<AlarmAttachmentListResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
}
