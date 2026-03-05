using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.CreateDivision;

/// <summary>
/// Handler for CreateDivisionCommand.
/// </summary>
public class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand, Result<CreateDivisionResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<CreateDivisionCommandHandler> _logger;

    public CreateDivisionCommandHandler(
        ISystemRepository systemRepository,
        ILogger<CreateDivisionCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<CreateDivisionResponseDto>> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating division: DivCode={DivCode}", request.DivCode);

        try
        {
            var dto = new CreateDivisionRequestDto
            {
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
                CreateUserId = request.CreateUserId
            };

            var result = await _systemRepository.CreateDivisionAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<CreateDivisionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Division created successfully: {DivSeq}", result.DivSeq);

            return Result<CreateDivisionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating division: {DivCode}", request.DivCode);
            return Result<CreateDivisionResponseDto>.Failure($"사업부 생성 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
