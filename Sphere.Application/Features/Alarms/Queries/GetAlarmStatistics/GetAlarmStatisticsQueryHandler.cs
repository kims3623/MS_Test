using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmStatistics;

/// <summary>
/// Handler for GetAlarmStatisticsQuery.
/// </summary>
public class GetAlarmStatisticsQueryHandler : IRequestHandler<GetAlarmStatisticsQuery, Result<AlarmStatisticsDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmStatisticsQueryHandler> _logger;

    public GetAlarmStatisticsQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmStatisticsQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmStatisticsDto>> Handle(GetAlarmStatisticsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm statistics for DivSeq={DivSeq}, StartDate={StartDate}, EndDate={EndDate}",
            request.DivSeq, request.StartDate, request.EndDate);

        try
        {
            var statistics = await _alarmRepository.GetStatisticsAsync(
                request.DivSeq,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            return Result<AlarmStatisticsDto>.Success(statistics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm statistics");
            return Result<AlarmStatisticsDto>.Failure($"알람 통계 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
