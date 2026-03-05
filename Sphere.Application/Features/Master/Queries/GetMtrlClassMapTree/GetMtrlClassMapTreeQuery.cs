using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetMtrlClassMapTree;

/// <summary>
/// Query to get MtrlClassMap tree data.
/// </summary>
public record GetMtrlClassMapTreeQuery : IRequest<Result<List<MtrlClassMapTreeDto>>>
{
    public string DivSeq { get; init; } = string.Empty;
}
