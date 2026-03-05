using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Handler for ResetPasswordCommand.
/// </summary>
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;

    public ResetPasswordCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService,
        ILogger<ResetPasswordCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Password reset request for user {UserId}", request.UserId);

        // 1. Find user by ID and email (password is stored in UserInfo table)
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.Email == request.Email && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("Password reset failed: User {UserId} not found or email mismatch", request.UserId);
            // Return success to prevent user enumeration
            return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse
            {
                Success = true,
                Message = "If the user exists and email matches, a password reset email will be sent."
            });
        }

        // 2. Generate temporary password and update UserInfo
        var tempPassword = GenerateTemporaryPassword();
        user.PasswordHash = _passwordHasher.Hash(tempPassword);
        user.UpdateDate = _dateTimeService.Now;

        // 3. Reset fail count and unlock account
        user.FailCount = 0;
        user.IsLocked = "N";

        await _context.SaveChangesAsync(cancellationToken);

        // Note: In production, send email with temporary password
        // For now, log it (should be replaced with email service)
        _logger.LogInformation("Password reset successful for user {UserId}. Temporary password generated.", request.UserId);

        return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse
        {
            Success = true,
            Message = "If the user exists and email matches, a password reset email will be sent."
        });
    }

    private static string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
