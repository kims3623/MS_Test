using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.CreateUser;

/// <summary>
/// Handler for CreateUserCommand.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        ISystemRepository systemRepository,
        ILogger<CreateUserCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<CreateUserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating user: DivSeq={DivSeq}, UserId={UserId}",
            request.DivSeq, request.UserId);

        try
        {
            var dto = new CreateUserRequestDto
            {
                DivSeq = request.DivSeq,
                UserId = request.UserId,
                UserName = request.UserName,
                UserNameE = request.UserNameE,
                Email = request.Email,
                Phone = request.Phone,
                Mobile = request.Mobile,
                DeptCode = request.DeptCode,
                PositionCode = request.PositionCode,
                RoleCode = request.RoleCode,
                UserType = request.UserType,
                VendorId = request.VendorId,
                Language = request.Language,
                Timezone = request.Timezone,
                InitialPassword = request.InitialPassword,
                GroupIds = request.GroupIds,
                CreateUserId = request.CreateUserId
            };

            var result = await _systemRepository.CreateUserAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<CreateUserResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("User created successfully: {UserId}", request.UserId);

            return Result<CreateUserResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user: {UserId}", request.UserId);
            return Result<CreateUserResponseDto>.Failure($"사용자 생성 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
