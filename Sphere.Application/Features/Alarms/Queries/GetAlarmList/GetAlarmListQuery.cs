using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmList;

/// <summary>
/// Query to get alarm list with filter and pagination.
/// </summary>
public record GetAlarmListQuery : IRequest<Result<AlarmListResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string? AlmActionId { get; init; }
    public string? AlmProcYn { get; init; }
    public string? StopYn { get; init; }
    public string? UserId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
