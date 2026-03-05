using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.UpdateSystemConfig;

/// <summary>
/// Handler for UpdateSystemConfigCommand.
/// </summary>
public class UpdateSystemConfigCommandHandler : IRequestHandler<UpdateSystemConfigCommand, Result<UpdateSystemConfigResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<UpdateSystemConfigCommandHandler> _logger;

    public UpdateSystemConfigCommandHandler(
        ISystemRepository systemRepository,
        ILogger<UpdateSystemConfigCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UpdateSystemConfigResponseDto>> Handle(UpdateSystemConfigCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating system config: DivSeq={DivSeq}, Items={ItemCount}",
            request.DivSeq, request.Items.Count);

        try
        {
            var dto = new UpdateSystemConfigRequestDto
            {
                DivSeq = request.DivSeq,
                Items = request.Items,
                UpdateUserId = request.UpdateUserId
            };

            var result = await _systemRepository.UpdateSystemConfigAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<UpdateSystemConfigResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("System config updated successfully: {UpdatedCount} items", result.UpdatedCount);

            return Result<UpdateSystemConfigResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating system config");
            return Result<UpdateSystemConfigResponseDto>.Failure($"시스템 설정 수정 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
