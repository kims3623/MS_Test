using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetYieldSpecData;

/// <summary>
/// Query to get yield spec data.
/// </summary>
public record GetYieldSpecDataQuery : IRequest<Result<YieldSpecDataDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
}
