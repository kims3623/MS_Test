using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Queries.GetRawDataByGroup;

/// <summary>
/// Query to get raw data by group.
/// </summary>
public record GetRawDataByGroupQuery : IRequest<Result<RawDataGroupDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string GroupSpecSysId { get; init; } = string.Empty;
}
