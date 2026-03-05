using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.CreateMtrlClassMap;

/// <summary>
/// Command to create a new MtrlClassMap entry (class or subclass).
/// </summary>
public record CreateMtrlClassMapCommand : IRequest<Result<MtrlClassMapResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? ParentTreeId { get; init; }
    public string MtrlClassId { get; init; } = string.Empty;
    public string ClassType { get; init; } = string.Empty;
}
