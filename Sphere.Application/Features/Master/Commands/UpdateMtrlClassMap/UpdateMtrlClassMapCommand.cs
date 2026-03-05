using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.UpdateMtrlClassMap;

/// <summary>
/// Command to update an existing MtrlClassMap entry (useYn, etc.).
/// </summary>
public record UpdateMtrlClassMapCommand : IRequest<Result<MtrlClassMapResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string TreeId { get; init; } = string.Empty;
    public string UseYn { get; init; } = string.Empty;
}
