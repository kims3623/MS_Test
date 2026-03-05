using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Commands.UploadAlarmAttachment;

/// <summary>
/// Command to upload an attachment to an alarm.
/// </summary>
public record UploadAlarmAttachmentCommand : IRequest<Result<UploadAlarmAttachmentResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string OriginalFileName { get; init; } = string.Empty;
    public long FileSize { get; init; }
    public string FileType { get; init; } = string.Empty;
    public string MimeType { get; init; } = string.Empty;
    public byte[] FileContent { get; init; } = Array.Empty<byte>();
    public string CreateUserId { get; init; } = string.Empty;
}
