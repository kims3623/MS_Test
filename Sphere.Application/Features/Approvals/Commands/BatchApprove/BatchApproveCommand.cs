using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Commands.BatchApprove;

/// <summary>
/// Command for batch approving multiple requests.
/// </summary>
public record BatchApproveCommand : IRequest<Result<BatchApproveResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public List<string> AprovIds { get; init; } = new();
    public string? Comment { get; init; }
}
