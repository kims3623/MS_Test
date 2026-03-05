using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Queries.GetRawDataList;

/// <summary>
/// Query to get raw data list with filtering and pagination.
/// </summary>
public record GetRawDataListQuery : IRequest<Result<RawDataListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string? ProjectId { get; init; }
    public string? ActProdId { get; init; }
    public string? StepId { get; init; }
    public string? ItemId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public string? SpecSysId { get; init; }
    public string? ApprovalYn { get; init; }
    public string? SearchText { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}
