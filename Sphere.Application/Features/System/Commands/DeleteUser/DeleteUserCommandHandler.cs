using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.DeleteUser;

/// <summary>
/// Handler for DeleteUserCommand.
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<DeleteUserResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(
        ISystemRepository systemRepository,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<DeleteUserResponseDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting user: DivSeq={DivSeq}, UserId={UserId}",
            request.DivSeq, request.UserId);

        try
        {
            var dto = new DeleteUserRequestDto
            {
                DivSeq = request.DivSeq,
                UserId = request.UserId,
                DeleteUserId = request.DeleteUserId
            };

            var result = await _systemRepository.DeleteUserAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<DeleteUserResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("User deleted successfully: {UserId}", request.UserId);

            return Result<DeleteUserResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user: {UserId}", request.UserId);
            return Result<DeleteUserResponseDto>.Failure($"사용자 삭제 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
