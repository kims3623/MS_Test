using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.UpdateUser;

/// <summary>
/// Handler for UpdateUserCommand.
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        ISystemRepository systemRepository,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UpdateUserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating user: DivSeq={DivSeq}, UserId={UserId}",
            request.DivSeq, request.UserId);

        try
        {
            var dto = new UpdateUserRequestDto
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
                IsActive = request.IsActive,
                GroupIds = request.GroupIds,
                UpdateUserId = request.UpdateUserId
            };

            var result = await _systemRepository.UpdateUserAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<UpdateUserResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("User updated successfully: {UserId}", request.UserId);

            return Result<UpdateUserResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user: {UserId}", request.UserId);
            return Result<UpdateUserResponseDto>.Failure($"사용자 수정 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
