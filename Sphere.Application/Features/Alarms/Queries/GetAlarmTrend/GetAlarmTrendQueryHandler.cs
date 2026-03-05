using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmTrend;

/// <summary>
/// Handler for GetAlarmTrendQuery.
/// </summary>
public class GetAlarmTrendQueryHandler : IRequestHandler<GetAlarmTrendQuery, Result<AlarmTrendResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmTrendQueryHandler> _logger;

    public GetAlarmTrendQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmTrendQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmTrendResponseDto>> Handle(GetAlarmTrendQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm trend for DivSeq={DivSeq}, StartDate={StartDate}, EndDate={EndDate}, GroupBy={GroupBy}",
            request.DivSeq, request.StartDate, request.EndDate, request.GroupBy);

        try
        {
            var trendQuery = new AlarmTrendQueryDto
            {
                DivSeq = request.DivSeq,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                GroupBy = request.GroupBy
            };

            var trendData = await _alarmRepository.GetTrendAsync(trendQuery, cancellationToken);

            return Result<AlarmTrendResponseDto>.Success(trendData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm trend data");
            return Result<AlarmTrendResponseDto>.Failure($"알람 추세 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
