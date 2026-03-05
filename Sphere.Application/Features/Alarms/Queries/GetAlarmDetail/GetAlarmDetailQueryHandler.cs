using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmDetail;

/// <summary>
/// Handler for GetAlarmDetailQuery.
/// </summary>
public class GetAlarmDetailQueryHandler : IRequestHandler<GetAlarmDetailQuery, Result<AlarmDetailDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmDetailQueryHandler> _logger;

    public GetAlarmDetailQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmDetailQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmDetailDto>> Handle(GetAlarmDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm detail for AlmSysId={AlmSysId}",
            request.AlmSysId);

        try
        {
            var alarm = await _alarmRepository.GetDetailAsync(
                request.DivSeq,
                request.AlmSysId,
                cancellationToken);

            if (alarm == null)
            {
                _logger.LogWarning("Alarm not found: {AlmSysId}", request.AlmSysId);
                return Result<AlarmDetailDto>.Failure("알람을 찾을 수 없습니다.");
            }

            // Load actions for the alarm
            var actions = await _alarmRepository.GetAlarmActionsAsync(
                request.DivSeq,
                request.AlmSysId,
                cancellationToken);
            alarm.Actions = actions.ToList();

            // Load history count
            var history = await _alarmRepository.GetHistoryAsync(
                new AlarmHistoryQueryDto { DivSeq = request.DivSeq, AlmSysId = request.AlmSysId },
                cancellationToken);
            alarm.HistoryCount = history.TotalCount;

            // Load attachment count
            var attachments = await _alarmRepository.GetAttachmentsAsync(
                new AlarmAttachmentQueryDto { DivSeq = request.DivSeq, AlmSysId = request.AlmSysId },
                cancellationToken);
            alarm.AttachmentCount = attachments.TotalCount;

            return Result<AlarmDetailDto>.Success(alarm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm detail for {AlmSysId}", request.AlmSysId);
            return Result<AlarmDetailDto>.Failure($"알람 상세 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
