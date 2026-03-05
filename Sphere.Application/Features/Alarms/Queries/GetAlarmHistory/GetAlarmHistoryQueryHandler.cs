using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmHistory;

/// <summary>
/// Handler for GetAlarmHistoryQuery.
/// </summary>
public class GetAlarmHistoryQueryHandler : IRequestHandler<GetAlarmHistoryQuery, Result<AlarmHistoryResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmHistoryQueryHandler> _logger;

    public GetAlarmHistoryQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmHistoryQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmHistoryResponseDto>> Handle(GetAlarmHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm history for AlmSysId={AlmSysId}",
            request.AlmSysId);

        try
        {
            var query = new AlarmHistoryQueryDto
            {
                DivSeq = request.DivSeq,
                AlmSysId = request.AlmSysId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _alarmRepository.GetHistoryAsync(query, cancellationToken);

            _logger.LogInformation("Found {Count} history items", result.TotalCount);

            return Result<AlarmHistoryResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm history for {AlmSysId}", request.AlmSysId);
            return Result<AlarmHistoryResponseDto>.Failure($"알람 이력 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
