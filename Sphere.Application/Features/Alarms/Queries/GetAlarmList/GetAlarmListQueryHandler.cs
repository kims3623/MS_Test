using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmList;

/// <summary>
/// Handler for GetAlarmListQuery.
/// </summary>
public class GetAlarmListQueryHandler : IRequestHandler<GetAlarmListQuery, Result<AlarmListResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmListQueryHandler> _logger;

    public GetAlarmListQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmListQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmListResponseDto>> Handle(GetAlarmListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm list for DivSeq={DivSeq}, Page={Page}",
            request.DivSeq, request.PageNumber);

        try
        {
            var filter = new AlarmListFilterDto
            {
                DivSeq = request.DivSeq,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                AlmActionId = request.AlmActionId,
                AlmProcYn = request.AlmProcYn,
                StopYn = request.StopYn,
                UserId = request.UserId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _alarmRepository.GetListAsync(filter, cancellationToken);

            _logger.LogInformation("Found {Count} alarms", result.TotalCount);

            return Result<AlarmListResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm list");
            return Result<AlarmListResponseDto>.Failure($"알람 목록 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
