using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Queries.GetTempRawData;

/// <summary>
/// Query to get temporary raw data list.
/// </summary>
public record GetTempRawDataQuery : IRequest<Result<TempRawDataListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? SpecSysId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
}
