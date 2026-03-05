using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.TerminateSession;

/// <summary>
/// Command for terminating a session.
/// </summary>
public class TerminateSessionCommand : IRequest<Result<TerminateSessionResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public string TerminateUserId { get; set; } = string.Empty;
}
