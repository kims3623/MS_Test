using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Features.Auth.Commands.ChangePassword;
using Sphere.Application.Features.Auth.Commands.GetOtpCode;
using Sphere.Application.Features.Auth.Commands.Login;
using Sphere.Application.Features.Auth.Commands.Logout;
using Sphere.Application.Features.Auth.Commands.RefreshToken;
using Sphere.Application.Features.Auth.Commands.ResendOtp;
using Sphere.Application.Features.Auth.Commands.ResetPassword;
using Sphere.Application.Features.Auth.Commands.ValidateCredentials;
using Sphere.Application.Features.Auth.Commands.VerifyOtp;
using Sphere.Application.Features.Auth.Queries.GetCurrentUser;
using Sphere.Application.Features.Auth.Queries.GetLoginHistory;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Authentication controller for login, logout, and token management.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ISender mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user and returns tokens.
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <returns>Login response with tokens.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var command = new LoginCommand
        {
            UserId = request.UserId,
            Password = request.Password,
            DivSeq = request.DivSeq
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Authentication Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>No content on success.</returns>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto? request = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new LogoutCommand
        {
            UserId = userId,
            DivSeq = divSeq,
            RefreshToken = request?.RefreshToken
        };

        await _mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Refreshes authentication tokens.
    /// </summary>
    /// <param name="request">Refresh token request.</param>
    /// <returns>New tokens.</returns>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var command = new RefreshTokenCommand
        {
            RefreshToken = request.RefreshToken
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Token Refresh Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets the current authenticated user's profile.
    /// </summary>
    /// <returns>User profile.</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetCurrentUserQuery
        {
            UserId = userId,
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Validates the current session.
    /// </summary>
    /// <returns>Session validation status.</returns>
    [HttpGet("session")]
    [Authorize]
    [ProducesResponseType(typeof(SessionValidationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public IActionResult ValidateSession()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var divSeq = User.FindFirstValue("div_seq");
        var expiration = User.FindFirstValue("exp");

        return Ok(new SessionValidationDto
        {
            IsValid = true,
            UserId = userId ?? string.Empty,
            DivSeq = divSeq ?? string.Empty,
            ExpiresAt = expiration != null
                ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration)).UtcDateTime
                : DateTime.UtcNow
        });
    }

    /// <summary>
    /// Changes the current user's password.
    /// </summary>
    /// <param name="request">Password change request.</param>
    /// <returns>No content on success.</returns>
    [HttpPut("password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new ChangePasswordCommand
        {
            UserId = userId,
            DivSeq = divSeq,
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword,
            ConfirmPassword = request.ConfirmPassword
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Password Change Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Verifies OTP code after login.
    /// </summary>
    /// <param name="request">OTP verification request.</param>
    /// <returns>Login response with tokens on success.</returns>
    [HttpPost("otp/verify")]
    [AllowAnonymous]
    [EnableRateLimiting("otp")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto request)
    {
        var command = new VerifyOtpCommand
        {
            OtpSessionId = request.OtpSessionId,
            OtpCode = request.OtpCode,
            UserId = request.OtpSessionId, // Will be extracted from session
            DivSeq = string.Empty // Will be extracted from session
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "OTP Verification Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Resends OTP code to user.
    /// </summary>
    /// <param name="request">Resend OTP request.</param>
    /// <returns>Resend result with remaining time.</returns>
    [HttpPost("otp/resend")]
    [AllowAnonymous]
    [EnableRateLimiting("otp")]
    [ProducesResponseType(typeof(ResendOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequestDto request)
    {
        var command = new ResendOtpCommand
        {
            OtpSessionId = request.OtpSessionId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "OTP Resend Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Generates and sends OTP code to user.
    /// </summary>
    /// <param name="request">OTP generation request.</param>
    /// <returns>OTP session information.</returns>
    [HttpPost("otp")]
    [AllowAnonymous]
    [EnableRateLimiting("otp")]
    [ProducesResponseType(typeof(GetOtpCodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> GetOtpCode([FromBody] GetOtpCodeRequestDto request)
    {
        var command = new GetOtpCodeCommand
        {
            UserId = request.UserId,
            DivSeq = request.DivSeq
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "OTP Generation Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Resets user password.
    /// </summary>
    /// <param name="request">Password reset request.</param>
    /// <returns>Reset result.</returns>
    [HttpPost("password/reset")]
    [AllowAnonymous]
    [EnableRateLimiting("password-reset")]
    [ProducesResponseType(typeof(ResetPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        var command = new ResetPasswordCommand
        {
            UserId = request.UserId,
            Email = request.Email,
            DivSeq = request.DivSeq
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Password Reset Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets login history for current user.
    /// </summary>
    /// <param name="startDate">Optional start date filter.</param>
    /// <param name="endDate">Optional end date filter.</param>
    /// <param name="pageNumber">Page number (default 1).</param>
    /// <param name="pageSize">Page size (default 20).</param>
    /// <returns>Login history list.</returns>
    [HttpGet("history")]
    [Authorize]
    [ProducesResponseType(typeof(GetLoginHistoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetLoginHistory(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetLoginHistoryQuery
        {
            UserId = userId,
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to Get Login History",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Validates user credentials without logging in.
    /// </summary>
    /// <param name="request">Credentials to validate.</param>
    /// <returns>Validation result.</returns>
    [HttpPost("validate")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ValidateCredentialsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateCredentials([FromBody] ValidateCredentialsRequestDto request)
    {
        var command = new ValidateCredentialsCommand
        {
            UserId = request.UserId,
            Password = request.Password,
            DivSeq = request.DivSeq
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }
}

/// <summary>
/// OTP generation request DTO.
/// </summary>
public class GetOtpCodeRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Password reset request DTO.
/// </summary>
public class ResetPasswordRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Validate credentials request DTO.
/// </summary>
public class ValidateCredentialsRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Logout request DTO.
/// </summary>
public class LogoutRequestDto
{
    public string? RefreshToken { get; set; }
}

/// <summary>
/// Session validation response DTO.
/// </summary>
public class SessionValidationDto
{
    public bool IsValid { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
