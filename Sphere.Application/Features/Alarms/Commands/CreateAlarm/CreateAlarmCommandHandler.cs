using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.CreateAlarm;

/// <summary>
/// Handler for CreateAlarmCommand.
/// </summary>
public class CreateAlarmCommandHandler : IRequestHandler<CreateAlarmCommand, Result<CreateAlarmResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<CreateAlarmCommandHandler> _logger;

    public CreateAlarmCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<CreateAlarmCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<CreateAlarmResponseDto>> Handle(CreateAlarmCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating alarm with title={Title}, AlmProcId={AlmProcId}, VendorId={VendorId}",
            request.Title, request.AlmProcId, request.VendorId);

        try
        {
            var createRequest = new CreateAlarmRequestDto
            {
                DivSeq = request.DivSeq,
                AlmProcId = request.AlmProcId,
                Title = request.Title,
                Contents = request.Contents,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                SpecSysId = request.SpecSysId,
                Severity = request.Severity,
                CreateUserId = request.CreateUserId
            };

            var result = await _alarmRepository.CreateAsync(createRequest, cancellationToken);

            if (result.Result != "OK" && result.Result != "SUCCESS")
            {
                _logger.LogWarning("Failed to create alarm: {Message}", result.ResultMessage);
                return Result<CreateAlarmResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Alarm created successfully: AlmSysId={AlmSysId}", result.AlmSysId);
            return Result<CreateAlarmResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating alarm");
            return Result<CreateAlarmResponseDto>.Failure($"알람 생성 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
