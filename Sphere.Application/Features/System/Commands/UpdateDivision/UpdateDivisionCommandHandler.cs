using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.UpdateDivision;

/// <summary>
/// Handler for UpdateDivisionCommand.
/// </summary>
public class UpdateDivisionCommandHandler : IRequestHandler<UpdateDivisionCommand, Result<UpdateDivisionResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<UpdateDivisionCommandHandler> _logger;

    public UpdateDivisionCommandHandler(
        ISystemRepository systemRepository,
        ILogger<UpdateDivisionCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UpdateDivisionResponseDto>> Handle(UpdateDivisionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating division: DivSeq={DivSeq}", request.DivSeq);

        try
        {
            var dto = new UpdateDivisionRequestDto
            {
                DivSeq = request.DivSeq,
                DivCode = request.DivCode,
                DivName = request.DivName,
                DivNameE = request.DivNameE,
                DivType = request.DivType,
                ParentDivSeq = request.ParentDivSeq,
                Description = request.Description,
                Address = request.Address,
                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email,
                SortOrder = request.SortOrder,
                IsActive = request.IsActive,
                UpdateUserId = request.UpdateUserId
            };

            var result = await _systemRepository.UpdateDivisionAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<UpdateDivisionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Division updated successfully: {DivSeq}", request.DivSeq);

            return Result<UpdateDivisionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating division: {DivSeq}", request.DivSeq);
            return Result<UpdateDivisionResponseDto>.Failure($"사업부 수정 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
