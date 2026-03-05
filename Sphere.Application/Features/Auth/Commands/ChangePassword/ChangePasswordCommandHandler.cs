using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ChangePassword;

/// <summary>
/// Handler for ChangePasswordCommand.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService,
        ILogger<ChangePasswordCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Password change attempt for user {UserId}", request.UserId);

        // 1. Find user (password is stored in UserInfo table)
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("Password change failed: User not found for {UserId}", request.UserId);
            return Result.Failure("User not found.");
        }

        // 2. Verify current password
        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash ?? string.Empty))
        {
            _logger.LogWarning("Password change failed: Invalid current password for {UserId}", request.UserId);
            return Result.Failure("Current password is incorrect.");
        }

        // 3. Hash new password
        var newPasswordHash = _passwordHasher.Hash(request.NewPassword);

        // 4. Update password
        user.PasswordHash = newPasswordHash;
        user.UpdateDate = _dateTimeService.Now;
        user.UpdateUserId = request.UserId;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Password changed successfully for user {UserId}", request.UserId);

        return Result.Success();
    }
}
