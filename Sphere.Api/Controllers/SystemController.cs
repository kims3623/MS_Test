using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.System;
using Sphere.Application.Features.System.Commands.CreateUser;
using Sphere.Application.Features.System.Commands.UpdateUser;
using Sphere.Application.Features.System.Commands.DeleteUser;
using Sphere.Application.Features.System.Commands.UpdateSecurityPolicy;
using Sphere.Application.Features.System.Commands.CreateDivision;
using Sphere.Application.Features.System.Commands.UpdateDivision;
using Sphere.Application.Features.System.Commands.UpdateSystemConfig;
using Sphere.Application.Features.System.Commands.CreateRole;
using Sphere.Application.Features.System.Commands.UpdateRole;
using Sphere.Application.Features.System.Commands.DeleteRole;
using Sphere.Application.Features.System.Commands.TerminateSession;
using Sphere.Application.Features.System.Commands.CreateMenu;
using Sphere.Application.Features.System.Commands.UpdateMenu;
using Sphere.Application.Features.System.Commands.DeleteMenu;
using Sphere.Application.Features.System.Queries.GetUserList;
using Sphere.Application.Features.System.Queries.GetSecurityPolicy;
using Sphere.Application.Features.System.Queries.GetAuditLogs;
using Sphere.Application.Features.System.Queries.GetDivisionList;
using Sphere.Application.Features.System.Queries.GetSystemConfig;
using Sphere.Application.Features.System.Queries.GetLoginHistory;
using Sphere.Application.Features.System.Queries.GetActiveSessions;
using Sphere.Application.Features.System.Queries.GetRoleList;
using Sphere.Application.Features.System.Queries.GetMenuTree;
using Sphere.Application.Features.System.Queries.GetUserGroups;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for system management operations (B7 - e_System module).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize]
public class SystemController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<SystemController> _logger;

    public SystemController(ISender mediator, ILogger<SystemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #region User Management (SPH5010, SPH5011, SPH5012)

    /// <summary>
    /// Gets user list with filter and pagination.
    /// </summary>
    /// <param name="userId">User ID filter.</param>
    /// <param name="userName">User name filter.</param>
    /// <param name="deptCode">Department code filter.</param>
    /// <param name="roleCode">Role code filter.</param>
    /// <param name="userType">User type filter.</param>
    /// <param name="isActive">Active status filter (Y/N).</param>
    /// <param name="vendorId">Vendor ID filter.</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 20).</param>
    /// <returns>Paginated user list.</returns>
    [HttpGet("users")]
    [ProducesResponseType(typeof(UserListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserList(
        [FromQuery] string? userId = null,
        [FromQuery] string? userName = null,
        [FromQuery] string? deptCode = null,
        [FromQuery] string? roleCode = null,
        [FromQuery] string? userType = null,
        [FromQuery] string? isActive = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetUserListQuery
        {
            DivSeq = divSeq,
            UserId = userId,
            UserName = userName,
            DeptCode = deptCode,
            RoleCode = roleCode,
            UserType = userType,
            IsActive = isActive,
            VendorId = vendorId,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "User List Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">Create user request.</param>
    /// <returns>Create user result.</returns>
    [HttpPost("users")]
    [ProducesResponseType(typeof(CreateUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new CreateUserCommand
        {
            DivSeq = divSeq,
            UserId = request.UserId,
            UserName = request.UserName,
            UserNameE = request.UserNameE ?? string.Empty,
            Email = request.Email,
            Phone = request.Phone ?? string.Empty,
            Mobile = request.Mobile ?? string.Empty,
            DeptCode = request.DeptCode,
            PositionCode = request.PositionCode ?? string.Empty,
            RoleCode = request.RoleCode,
            UserType = request.UserType,
            VendorId = request.VendorId,
            Language = request.Language ?? "ko-KR",
            Timezone = request.Timezone ?? "Asia/Seoul",
            InitialPassword = request.InitialPassword,
            GroupIds = request.GroupIds,
            CreateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Create User Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="request">Update user request.</param>
    /// <returns>Update user result.</returns>
    [HttpPut("users/{id}")]
    [ProducesResponseType(typeof(UpdateUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateUserCommand
        {
            DivSeq = divSeq,
            UserId = id,
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
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update User Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Delete user result.</returns>
    [HttpDelete("users/{id}")]
    [ProducesResponseType(typeof(DeleteUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new DeleteUserCommand
        {
            DivSeq = divSeq,
            UserId = id,
            DeleteUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Delete User Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Security Policy (SPH5050)

    /// <summary>
    /// Gets security policy settings.
    /// </summary>
    /// <returns>Security policy configuration.</returns>
    [HttpGet("security-policy")]
    [ProducesResponseType(typeof(SecurityPolicyDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSecurityPolicy()
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetSecurityPolicyQuery
        {
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Security Policy Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates security policy settings.
    /// </summary>
    /// <param name="request">Update security policy request.</param>
    /// <returns>Update result.</returns>
    [HttpPut("security-policy")]
    [ProducesResponseType(typeof(UpdateSecurityPolicyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSecurityPolicy([FromBody] UpdateSecurityPolicyRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateSecurityPolicyCommand
        {
            DivSeq = divSeq,
            MinPasswordLength = request.MinPasswordLength,
            MaxPasswordLength = request.MaxPasswordLength,
            RequireUppercase = request.RequireUppercase,
            RequireLowercase = request.RequireLowercase,
            RequireDigit = request.RequireDigit,
            RequireSpecialChar = request.RequireSpecialChar,
            PasswordHistoryCount = request.PasswordHistoryCount,
            PasswordExpiryDays = request.PasswordExpiryDays,
            PasswordWarningDays = request.PasswordWarningDays,
            MaxLoginAttempts = request.MaxLoginAttempts,
            LockoutDurationMinutes = request.LockoutDurationMinutes,
            SessionTimeoutMinutes = request.SessionTimeoutMinutes,
            AllowMultipleSessions = request.AllowMultipleSessions,
            MaxConcurrentSessions = request.MaxConcurrentSessions,
            RequireOtpForLogin = request.RequireOtpForLogin,
            RequireOtpForSensitiveOps = request.RequireOtpForSensitiveOps,
            OtpValidityMinutes = request.OtpValidityMinutes,
            OtpMaxAttempts = request.OtpMaxAttempts,
            EnableIpWhitelist = request.EnableIpWhitelist,
            EnableIpBlacklist = request.EnableIpBlacklist,
            IpWhitelist = request.IpWhitelist,
            IpBlacklist = request.IpBlacklist,
            EnableAuditLog = request.EnableAuditLog,
            AuditLogRetentionDays = request.AuditLogRetentionDays,
            LogSensitiveDataAccess = request.LogSensitiveDataAccess,
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update Security Policy Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Audit Logs (SPH5070)

    /// <summary>
    /// Gets audit logs with filter and pagination.
    /// </summary>
    /// <param name="startDate">Start date filter.</param>
    /// <param name="endDate">End date filter.</param>
    /// <param name="userId">User ID filter.</param>
    /// <param name="actionType">Action type filter.</param>
    /// <param name="targetType">Target type filter.</param>
    /// <param name="targetId">Target ID filter.</param>
    /// <param name="ipAddress">IP address filter.</param>
    /// <param name="keyword">Keyword search.</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 50).</param>
    /// <returns>Paginated audit log list.</returns>
    [HttpGet("audit-logs")]
    [ProducesResponseType(typeof(AuditLogResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? userId = null,
        [FromQuery] string? actionType = null,
        [FromQuery] string? targetType = null,
        [FromQuery] string? targetId = null,
        [FromQuery] string? ipAddress = null,
        [FromQuery] string? keyword = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAuditLogsQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            UserId = userId,
            ActionType = actionType,
            TargetType = targetType,
            TargetId = targetId,
            IpAddress = ipAddress,
            Keyword = keyword,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Audit Logs Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Division Management (SPH5120)

    /// <summary>
    /// Gets division list with filter and pagination.
    /// </summary>
    /// <param name="divSeq">Division sequence filter.</param>
    /// <param name="divName">Division name filter.</param>
    /// <param name="isActive">Active status filter (Y/N).</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 20).</param>
    /// <returns>Paginated division list.</returns>
    [HttpGet("divisions")]
    [ProducesResponseType(typeof(DivisionListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDivisionList(
        [FromQuery] string? divSeq = null,
        [FromQuery] string? divName = null,
        [FromQuery] string? isActive = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetDivisionListQuery
        {
            DivSeq = divSeq,
            DivName = divName,
            IsActive = isActive,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Division List Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new division.
    /// </summary>
    /// <param name="request">Create division request.</param>
    /// <returns>Create division result.</returns>
    [HttpPost("divisions")]
    [ProducesResponseType(typeof(CreateDivisionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionRequestBody request)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new CreateDivisionCommand
        {
            DivCode = request.DivCode,
            DivName = request.DivName,
            DivNameE = request.DivNameE ?? string.Empty,
            DivType = request.DivType,
            ParentDivSeq = request.ParentDivSeq,
            Description = request.Description,
            Address = request.Address,
            Phone = request.Phone,
            Fax = request.Fax,
            Email = request.Email,
            SortOrder = request.SortOrder,
            CreateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Create Division Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing division.
    /// </summary>
    /// <param name="id">Division sequence.</param>
    /// <param name="request">Update division request.</param>
    /// <returns>Update division result.</returns>
    [HttpPut("divisions/{id}")]
    [ProducesResponseType(typeof(UpdateDivisionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDivision(string id, [FromBody] UpdateDivisionRequestBody request)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateDivisionCommand
        {
            DivSeq = id,
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
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update Division Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Role Management (SPH5030, SPH5031)

    /// <summary>
    /// Gets role list with filter and pagination.
    /// </summary>
    [HttpGet("roles")]
    [ProducesResponseType(typeof(RoleListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleList(
        [FromQuery] string? roleCode = null,
        [FromQuery] string? roleName = null,
        [FromQuery] string? roleType = null,
        [FromQuery] string? isActive = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetRoleListQuery
        {
            DivSeq = divSeq,
            RoleCode = roleCode,
            RoleName = roleName,
            RoleType = roleType,
            IsActive = isActive,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Role List Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost("roles")]
    [ProducesResponseType(typeof(CreateRoleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new CreateRoleCommand
        {
            DivSeq = divSeq,
            RoleCode = request.RoleCode,
            RoleName = request.RoleName,
            RoleNameE = request.RoleNameE ?? string.Empty,
            RoleType = request.RoleType,
            Description = request.Description,
            Level = request.Level,
            PermissionCodes = request.PermissionCodes,
            MenuPermissions = request.MenuPermissions,
            CreateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Create Role Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    [HttpPut("roles/{id}")]
    [ProducesResponseType(typeof(UpdateRoleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateRoleCommand
        {
            DivSeq = divSeq,
            RoleCode = id,
            RoleName = request.RoleName,
            RoleNameE = request.RoleNameE,
            RoleType = request.RoleType,
            Description = request.Description,
            Level = request.Level,
            IsActive = request.IsActive,
            PermissionCodes = request.PermissionCodes,
            MenuPermissions = request.MenuPermissions,
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update Role Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a role.
    /// </summary>
    [HttpDelete("roles/{id}")]
    [ProducesResponseType(typeof(DeleteRoleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new DeleteRoleCommand
        {
            DivSeq = divSeq,
            RoleCode = id,
            DeleteUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Delete Role Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Login History (SPH5060)

    /// <summary>
    /// Gets login history with filter and pagination.
    /// </summary>
    [HttpGet("login-history")]
    [ProducesResponseType(typeof(LoginHistoryResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoginHistory(
        [FromQuery] string? userId = null,
        [FromQuery] string? userName = null,
        [FromQuery] string? loginResult = null,
        [FromQuery] string? ipAddress = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetLoginHistoryQuery
        {
            DivSeq = divSeq,
            UserId = userId,
            UserName = userName,
            LoginResult = loginResult,
            IpAddress = ipAddress,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Login History Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Session Management (SPH5080)

    /// <summary>
    /// Gets active sessions with filter and pagination.
    /// </summary>
    [HttpGet("sessions")]
    [ProducesResponseType(typeof(ActiveSessionResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveSessions(
        [FromQuery] string? userId = null,
        [FromQuery] string? ipAddress = null,
        [FromQuery] string? sessionStatus = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetActiveSessionsQuery
        {
            DivSeq = divSeq,
            UserId = userId,
            IpAddress = ipAddress,
            SessionStatus = sessionStatus,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Sessions Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Terminates a session.
    /// </summary>
    [HttpDelete("sessions/{id}")]
    [ProducesResponseType(typeof(TerminateSessionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TerminateSession(string id, [FromQuery] string? reason = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new TerminateSessionCommand
        {
            DivSeq = divSeq,
            SessionId = id,
            Reason = reason,
            TerminateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Terminate Session Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Menu Management (SPH5100)

    /// <summary>
    /// Gets menu tree structure.
    /// </summary>
    [HttpGet("menus/tree")]
    [ProducesResponseType(typeof(MenuTreeResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMenuTree(
        [FromQuery] string? parentMenuId = null,
        [FromQuery] string? menuType = null,
        [FromQuery] string? isActive = null,
        [FromQuery] string language = "ko-KR")
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetMenuTreeQuery
        {
            DivSeq = divSeq,
            ParentMenuId = parentMenuId,
            MenuType = menuType,
            IsActive = isActive,
            Language = language
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Menu Tree Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    [HttpPost("menus")]
    [ProducesResponseType(typeof(CreateMenuResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new CreateMenuCommand
        {
            DivSeq = divSeq,
            MenuId = request.MenuId,
            MenuName = request.MenuName,
            MenuNameE = request.MenuNameE ?? string.Empty,
            ParentMenuId = request.ParentMenuId,
            MenuType = request.MenuType,
            IconClass = request.IconClass,
            ScreenId = request.ScreenId,
            Url = request.Url,
            Description = request.Description,
            SortOrder = request.SortOrder,
            IsVisible = request.IsVisible ?? "Y",
            OpenNewWindow = request.OpenNewWindow ?? "N",
            CreateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Create Menu Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing menu.
    /// </summary>
    [HttpPut("menus/{id}")]
    [ProducesResponseType(typeof(UpdateMenuResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMenu(string id, [FromBody] UpdateMenuRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateMenuCommand
        {
            DivSeq = divSeq,
            MenuId = id,
            MenuName = request.MenuName,
            MenuNameE = request.MenuNameE,
            ParentMenuId = request.ParentMenuId,
            MenuType = request.MenuType,
            IconClass = request.IconClass,
            ScreenId = request.ScreenId,
            Url = request.Url,
            Description = request.Description,
            SortOrder = request.SortOrder,
            IsActive = request.IsActive,
            IsVisible = request.IsVisible,
            OpenNewWindow = request.OpenNewWindow,
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update Menu Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a menu.
    /// </summary>
    [HttpDelete("menus/{id}")]
    [ProducesResponseType(typeof(DeleteMenuResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMenu(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new DeleteMenuCommand
        {
            DivSeq = divSeq,
            MenuId = id,
            DeleteUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Delete Menu Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region User Groups (SPH5012)

    /// <summary>
    /// Gets user groups with filter and pagination.
    /// </summary>
    [HttpGet("user-groups")]
    [ProducesResponseType(typeof(UserGroupListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserGroups(
        [FromQuery] string? groupId = null,
        [FromQuery] string? groupName = null,
        [FromQuery] string? groupType = null,
        [FromQuery] string? isActive = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetUserGroupsQuery
        {
            DivSeq = divSeq,
            GroupId = groupId,
            GroupName = groupName,
            GroupType = groupType,
            IsActive = isActive,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "User Groups Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region System Config (SPH5130)

    /// <summary>
    /// Gets system configuration.
    /// </summary>
    /// <returns>System configuration.</returns>
    [HttpGet("config")]
    [ProducesResponseType(typeof(SystemConfigDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSystemConfig()
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetSystemConfigQuery
        {
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "System Config Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates system configuration.
    /// </summary>
    /// <param name="request">Update system config request.</param>
    /// <returns>Update result.</returns>
    [HttpPut("config")]
    [ProducesResponseType(typeof(UpdateSystemConfigResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSystemConfig([FromBody] UpdateSystemConfigRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateSystemConfigCommand
        {
            DivSeq = divSeq,
            Items = request.Items,
            UpdateUserId = currentUserId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update System Config Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion
}

#region Request Body Classes

/// <summary>
/// Request body for creating a user.
/// </summary>
public class CreateUserRequestBody
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? UserNameE { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string DeptCode { get; set; } = string.Empty;
    public string? PositionCode { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string? Language { get; set; }
    public string? Timezone { get; set; }
    public string InitialPassword { get; set; } = string.Empty;
    public List<string>? GroupIds { get; set; }
}

/// <summary>
/// Request body for updating a user.
/// </summary>
public class UpdateUserRequestBody
{
    public string? UserName { get; set; }
    public string? UserNameE { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? DeptCode { get; set; }
    public string? PositionCode { get; set; }
    public string? RoleCode { get; set; }
    public string? UserType { get; set; }
    public string? VendorId { get; set; }
    public string? Language { get; set; }
    public string? Timezone { get; set; }
    public string? IsActive { get; set; }
    public List<string>? GroupIds { get; set; }
}

/// <summary>
/// Request body for updating security policy.
/// </summary>
public class UpdateSecurityPolicyRequestBody
{
    public int? MinPasswordLength { get; set; }
    public int? MaxPasswordLength { get; set; }
    public string? RequireUppercase { get; set; }
    public string? RequireLowercase { get; set; }
    public string? RequireDigit { get; set; }
    public string? RequireSpecialChar { get; set; }
    public int? PasswordHistoryCount { get; set; }
    public int? PasswordExpiryDays { get; set; }
    public int? PasswordWarningDays { get; set; }
    public int? MaxLoginAttempts { get; set; }
    public int? LockoutDurationMinutes { get; set; }
    public int? SessionTimeoutMinutes { get; set; }
    public string? AllowMultipleSessions { get; set; }
    public int? MaxConcurrentSessions { get; set; }
    public string? RequireOtpForLogin { get; set; }
    public string? RequireOtpForSensitiveOps { get; set; }
    public int? OtpValidityMinutes { get; set; }
    public int? OtpMaxAttempts { get; set; }
    public string? EnableIpWhitelist { get; set; }
    public string? EnableIpBlacklist { get; set; }
    public List<string>? IpWhitelist { get; set; }
    public List<string>? IpBlacklist { get; set; }
    public string? EnableAuditLog { get; set; }
    public int? AuditLogRetentionDays { get; set; }
    public string? LogSensitiveDataAccess { get; set; }
}

/// <summary>
/// Request body for creating a division.
/// </summary>
public class CreateDivisionRequestBody
{
    public string DivCode { get; set; } = string.Empty;
    public string DivName { get; set; } = string.Empty;
    public string? DivNameE { get; set; }
    public string DivType { get; set; } = string.Empty;
    public string? ParentDivSeq { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public int SortOrder { get; set; }
}

/// <summary>
/// Request body for updating a division.
/// </summary>
public class UpdateDivisionRequestBody
{
    public string? DivCode { get; set; }
    public string? DivName { get; set; }
    public string? DivNameE { get; set; }
    public string? DivType { get; set; }
    public string? ParentDivSeq { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public int? SortOrder { get; set; }
    public string? IsActive { get; set; }
}

/// <summary>
/// Request body for updating system config.
/// </summary>
public class UpdateSystemConfigRequestBody
{
    public List<SystemConfigUpdateItemDto> Items { get; set; } = new();
}

/// <summary>
/// Request body for creating a role.
/// </summary>
public class CreateRoleRequestBody
{
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string? RoleNameE { get; set; }
    public string RoleType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Level { get; set; }
    public List<string>? PermissionCodes { get; set; }
    public List<RoleMenuPermissionDto>? MenuPermissions { get; set; }
}

/// <summary>
/// Request body for updating a role.
/// </summary>
public class UpdateRoleRequestBody
{
    public string? RoleName { get; set; }
    public string? RoleNameE { get; set; }
    public string? RoleType { get; set; }
    public string? Description { get; set; }
    public int? Level { get; set; }
    public string? IsActive { get; set; }
    public List<string>? PermissionCodes { get; set; }
    public List<RoleMenuPermissionDto>? MenuPermissions { get; set; }
}

/// <summary>
/// Request body for creating a menu.
/// </summary>
public class CreateMenuRequestBody
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string? MenuNameE { get; set; }
    public string? ParentMenuId { get; set; }
    public string MenuType { get; set; } = string.Empty;
    public string? IconClass { get; set; }
    public string? ScreenId { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public string? IsVisible { get; set; }
    public string? OpenNewWindow { get; set; }
}

/// <summary>
/// Request body for updating a menu.
/// </summary>
public class UpdateMenuRequestBody
{
    public string? MenuName { get; set; }
    public string? MenuNameE { get; set; }
    public string? ParentMenuId { get; set; }
    public string? MenuType { get; set; }
    public string? IconClass { get; set; }
    public string? ScreenId { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public string? IsActive { get; set; }
    public string? IsVisible { get; set; }
    public string? OpenNewWindow { get; set; }
}

#endregion
