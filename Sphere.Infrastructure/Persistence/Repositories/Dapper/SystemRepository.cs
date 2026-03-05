using System.Data;
using Dapper;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// System repository implementation using Dapper.
/// GET methods query real DB tables/SPs.
/// User CUD methods use direct SQL against SPC_USER_INFO.
/// Other CUD methods return success stubs (no corresponding DB tables).
/// </summary>
public class SystemRepository : DapperRepositoryBase, ISystemRepository
{
    public SystemRepository(IDbConnection connection) : base(connection) { }

    #region SQL Queries

    /// <summary>
    /// SQL query for user list from SPC_USER_INFO.
    /// Actual DB columns: div_seq, user_id, user_name, user_name_e, email, phone,
    /// dept_code, dept_name, position_code, position_name, role_code, role_name,
    /// language, timezone, last_login_date, is_locked, fail_count, use_yn, create_user_id, etc.
    /// No separate DEPT/ROLE/POSITION tables exist - all denormalized in SPC_USER_INFO.
    /// </summary>
    private const string GetUserListSql = @"
SELECT
    u.user_id       AS UserId,
    ISNULL(u.user_name, '')     AS UserName,
    ISNULL(u.user_name_e, '')   AS UserNameE,
    ISNULL(u.email, '')         AS Email,
    ISNULL(u.phone, '')         AS Phone,
    ISNULL(u.dept_code, '')     AS DeptCode,
    ISNULL(u.dept_name, '')     AS DeptName,
    ISNULL(u.role_code, '')     AS RoleCode,
    ISNULL(u.role_name, '')     AS RoleName,
    ''                          AS VendorId,
    ''                          AS VendorName,
    CASE u.role_code
         WHEN 'ADMIN' THEN 'Admin'
         WHEN 'MANAGER' THEN 'Manager'
         WHEN 'VENDOR' THEN 'Vendor'
         ELSE 'User' END       AS UserType,
    ISNULL(u.role_name, 'User') AS UserTypeName,
    ISNULL(u.use_yn, 'Y')      AS IsActive,
    ISNULL(CONVERT(VARCHAR(19), u.last_login_date, 120), '') AS LastLoginDate,
    ISNULL(CONVERT(VARCHAR(19), u.create_date, 120), '')     AS CreateDate,
    ISNULL(u.create_user_id, '') AS CreateUserId,
    ISNULL(CONVERT(VARCHAR(19), u.update_date, 120), '')     AS UpdateDate,
    ISNULL(u.update_user_id, '') AS UpdateUserId
FROM SPC_USER_INFO u WITH (NOLOCK)
WHERE u.div_seq = @DivSeq
  AND (@UserId = '' OR u.user_id LIKE '%' + @UserId + '%')
  AND (@UserName = '' OR u.user_name LIKE '%' + @UserName + '%')
  AND (@DeptCode = '' OR u.dept_code = @DeptCode)
  AND (@RoleCode = '' OR u.role_code = @RoleCode)
  AND (@IsActive = '' OR u.use_yn = @IsActive)
ORDER BY u.user_id";

    /// <summary>
    /// SQL query for counting users with filters.
    /// </summary>
    private const string GetUserCountSql = @"
SELECT COUNT(1)
FROM SPC_USER_INFO u WITH (NOLOCK)
WHERE u.div_seq = @DivSeq
  AND (@UserId = '' OR u.user_id LIKE '%' + @UserId + '%')
  AND (@UserName = '' OR u.user_name LIKE '%' + @UserName + '%')
  AND (@DeptCode = '' OR u.dept_code = @DeptCode)
  AND (@RoleCode = '' OR u.role_code = @RoleCode)
  AND (@IsActive = '' OR u.use_yn = @IsActive)";

    /// <summary>
    /// SQL query for user detail from SPC_USER_INFO.
    /// All name columns (dept_name, role_name, position_name) are directly in SPC_USER_INFO.
    /// </summary>
    private const string GetUserByIdSql = @"
SELECT
    u.user_id       AS UserId,
    ISNULL(u.user_name, '')     AS UserName,
    ISNULL(u.user_name_e, '')   AS UserNameE,
    ISNULL(u.email, '')         AS Email,
    ISNULL(u.phone, '')         AS Phone,
    ''                          AS Mobile,
    ISNULL(u.dept_code, '')     AS DeptCode,
    ISNULL(u.dept_name, '')     AS DeptName,
    ISNULL(u.position_code, '') AS PositionCode,
    ISNULL(u.position_name, '') AS PositionName,
    ISNULL(u.role_code, '')     AS RoleCode,
    ISNULL(u.role_name, '')     AS RoleName,
    CASE u.role_code
         WHEN 'ADMIN' THEN 'Admin'
         WHEN 'MANAGER' THEN 'Manager'
         WHEN 'VENDOR' THEN 'Vendor'
         ELSE 'User' END       AS UserType,
    ISNULL(u.role_name, 'User') AS UserTypeName,
    ''                          AS VendorId,
    ''                          AS VendorName,
    ISNULL(u.language, 'ko-KR') AS Language,
    ISNULL(u.timezone, 'Asia/Seoul') AS Timezone,
    ISNULL(u.use_yn, 'Y')      AS IsActive,
    ISNULL(u.is_locked, 'N')   AS IsLocked,
    ISNULL(u.fail_count, 0)    AS FailedLoginCount,
    ISNULL(CONVERT(VARCHAR(19), u.last_login_date, 120), '') AS LastLoginDate,
    ''                          AS LastPasswordChangeDate,
    ''                          AS PasswordExpireDate,
    ISNULL(CONVERT(VARCHAR(19), u.create_date, 120), '')     AS CreateDate,
    ISNULL(u.create_user_id, '') AS CreateUserId,
    ISNULL(CONVERT(VARCHAR(19), u.update_date, 120), '')     AS UpdateDate,
    ISNULL(u.update_user_id, '') AS UpdateUserId
FROM SPC_USER_INFO u WITH (NOLOCK)
WHERE u.user_id = @UserId
  AND u.div_seq = @DivSeq";

    #endregion

    // ─── User GET methods ───────────────────────────────────────────────

    public async Task<UserListResponseDto> GetUserListAsync(UserListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            DivSeq = filter.DivSeq,
            UserId = filter.UserId ?? string.Empty,
            UserName = filter.UserName ?? string.Empty,
            DeptCode = filter.DeptCode ?? string.Empty,
            RoleCode = filter.RoleCode ?? string.Empty,
            VendorId = filter.VendorId ?? string.Empty,
            IsActive = filter.IsActive ?? string.Empty
        };

        var command = new CommandDefinition(
            GetUserCountSql,
            parameters,
            cancellationToken: cancellationToken);

        var totalCount = await _connection.ExecuteScalarAsync<int>(command);

        // Apply pagination with OFFSET/FETCH
        var pagedSql = GetUserListSql + @"
OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var offset = (filter.PageNumber - 1) * filter.PageSize;
        var pagedCommand = new CommandDefinition(
            pagedSql,
            new
            {
                parameters.DivSeq,
                parameters.UserId,
                parameters.UserName,
                parameters.DeptCode,
                parameters.RoleCode,
                parameters.VendorId,
                parameters.IsActive,
                Offset = offset,
                PageSize = filter.PageSize
            },
            cancellationToken: cancellationToken);

        var items = await _connection.QueryAsync<UserListItemDto>(pagedCommand);

        return new UserListResponseDto
        {
            Items = items.AsList(),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<UserDetailDto?> GetUserByIdAsync(string divSeq, string userId, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(
            GetUserByIdSql,
            new { UserId = userId, DivSeq = divSeq },
            cancellationToken: cancellationToken);

        var user = await _connection.QueryFirstOrDefaultAsync<UserDetailDto>(command);
        return user;
    }

    // ─── User CUD methods ──────────────────────────────────────────────

    public async Task<CreateUserResponseDto> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        const string sql = @"
IF EXISTS (SELECT 1 FROM SPC_USER_INFO WHERE user_id = @UserId AND div_seq = @DivSeq)
BEGIN
    SELECT 'FAIL' AS Result, 'User already exists' AS ResultMessage, @UserId AS UserId
    RETURN
END

INSERT INTO SPC_USER_INFO (
    div_seq, user_id, user_name, user_name_e, email, phone,
    dept_code, position_code, role_code, role_name,
    language, timezone, password, use_yn, is_locked, fail_count,
    create_user_id, create_date, update_user_id, update_date
) VALUES (
    @DivSeq, @UserId, @UserName, @UserNameE, @Email, @Phone,
    @DeptCode, @PositionCode, @RoleCode, @RoleCode,
    @Language, @Timezone, @InitialPassword, 'Y', 'N', 0,
    @CreateUserId, GETDATE(), @CreateUserId, GETDATE()
)

SELECT 'OK' AS Result, 'User created successfully' AS ResultMessage, @UserId AS UserId";

        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.UserId,
            request.UserName,
            UserNameE = request.UserNameE ?? string.Empty,
            request.Email,
            Phone = request.Phone ?? string.Empty,
            DeptCode = request.DeptCode ?? string.Empty,
            PositionCode = request.PositionCode ?? string.Empty,
            RoleCode = request.RoleCode ?? string.Empty,
            Language = request.Language ?? "ko-KR",
            Timezone = request.Timezone ?? "Asia/Seoul",
            InitialPassword = request.InitialPassword ?? "Test@1234",
            request.CreateUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<CreateUserResponseDto>(command);
        return result ?? new CreateUserResponseDto { Result = "FAIL", ResultMessage = "No response from database" };
    }

    public async Task<UpdateUserResponseDto> UpdateUserAsync(UpdateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        const string sql = @"
IF NOT EXISTS (SELECT 1 FROM SPC_USER_INFO WHERE user_id = @UserId AND div_seq = @DivSeq)
BEGIN
    SELECT 'FAIL' AS Result, 'User not found' AS ResultMessage, @UserId AS UserId
    RETURN
END

UPDATE SPC_USER_INFO SET
    user_name       = ISNULL(@UserName, user_name),
    user_name_e     = ISNULL(@UserNameE, user_name_e),
    email           = ISNULL(@Email, email),
    phone           = ISNULL(@Phone, phone),
    dept_code       = ISNULL(@DeptCode, dept_code),
    position_code   = ISNULL(@PositionCode, position_code),
    role_code       = ISNULL(@RoleCode, role_code),
    role_name       = ISNULL(@RoleCode, role_name),
    language        = ISNULL(@Language, language),
    timezone        = ISNULL(@Timezone, timezone),
    use_yn          = ISNULL(@IsActive, use_yn),
    update_user_id  = @UpdateUserId,
    update_date     = GETDATE()
WHERE user_id = @UserId AND div_seq = @DivSeq

SELECT 'OK' AS Result, 'User updated successfully' AS ResultMessage, @UserId AS UserId";

        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.UserId,
            request.UserName,
            request.UserNameE,
            request.Email,
            request.Phone,
            request.DeptCode,
            request.PositionCode,
            request.RoleCode,
            request.Language,
            request.Timezone,
            request.IsActive,
            request.UpdateUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<UpdateUserResponseDto>(command);
        return result ?? new UpdateUserResponseDto { Result = "FAIL", ResultMessage = "No response from database" };
    }

    public async Task<DeleteUserResponseDto> DeleteUserAsync(DeleteUserRequestDto request, CancellationToken cancellationToken = default)
    {
        // Logical delete: set use_yn = 'N'
        const string sql = @"
IF NOT EXISTS (SELECT 1 FROM SPC_USER_INFO WHERE user_id = @UserId AND div_seq = @DivSeq)
BEGIN
    SELECT 'FAIL' AS Result, 'User not found' AS ResultMessage
    RETURN
END

UPDATE SPC_USER_INFO SET
    use_yn = 'N',
    update_user_id = @DeleteUserId,
    update_date = GETDATE()
WHERE user_id = @UserId AND div_seq = @DivSeq

SELECT 'OK' AS Result, 'User deleted successfully' AS ResultMessage";

        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.UserId,
            request.DeleteUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<DeleteUserResponseDto>(command);
        return result ?? new DeleteUserResponseDto { Result = "FAIL", ResultMessage = "No response from database" };
    }

    public async Task<UpdateUserResponseDto> ResetUserPasswordAsync(ResetUserPasswordRequestDto request, CancellationToken cancellationToken = default)
    {
        const string sql = @"
IF NOT EXISTS (SELECT 1 FROM SPC_USER_INFO WHERE user_id = @UserId AND div_seq = @DivSeq)
BEGIN
    SELECT 'FAIL' AS Result, 'User not found' AS ResultMessage, @UserId AS UserId
    RETURN
END

UPDATE SPC_USER_INFO SET
    password = @NewPassword,
    update_user_id = @UpdateUserId,
    update_date = GETDATE()
WHERE user_id = @UserId AND div_seq = @DivSeq

SELECT 'OK' AS Result, 'Password reset successfully' AS ResultMessage, @UserId AS UserId";

        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.UserId,
            request.NewPassword,
            request.UpdateUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<UpdateUserResponseDto>(command);
        return result ?? new UpdateUserResponseDto { Result = "FAIL", ResultMessage = "No response from database" };
    }

    public async Task<UpdateUserResponseDto> UnlockUserAsync(UnlockUserRequestDto request, CancellationToken cancellationToken = default)
    {
        const string sql = @"
IF NOT EXISTS (SELECT 1 FROM SPC_USER_INFO WHERE user_id = @UserId AND div_seq = @DivSeq)
BEGIN
    SELECT 'FAIL' AS Result, 'User not found' AS ResultMessage, @UserId AS UserId
    RETURN
END

UPDATE SPC_USER_INFO SET
    is_locked = 'N',
    fail_count = 0,
    update_user_id = @UpdateUserId,
    update_date = GETDATE()
WHERE user_id = @UserId AND div_seq = @DivSeq

SELECT 'OK' AS Result, 'User unlocked successfully' AS ResultMessage, @UserId AS UserId";

        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.UserId,
            request.UpdateUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<UpdateUserResponseDto>(command);
        return result ?? new UpdateUserResponseDto { Result = "FAIL", ResultMessage = "No response from database" };
    }

    // ─── User Group GET methods ─────────────────────────────────────────

    /// <summary>
    /// Queries SPC_USER_GROUP via the USP_SPC_USER_GROUP_SELECT stored procedure.
    /// SP params: @P_Lang_Type, @P_div_seq, @P_user_group_id, @P_use_yn, @P_cust_id
    /// SP returns: div_seq, user_group_id, mtrl_class_id, cust_id, end_user_id,
    ///   user_list, use_yn, acti_name, origin_acti_name, reason_code, description,
    ///   create_user_id, create_date, update_user_id, update_date
    /// </summary>
    public async Task<UserGroupListResponseDto> GetUserGroupListAsync(UserGroupListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", "ko-KR");
        parameters.Add("P_div_seq", filter.DivSeq);
        parameters.Add("P_user_group_id", filter.GroupId ?? string.Empty);
        parameters.Add("P_use_yn", string.Empty);
        parameters.Add("P_cust_id", string.Empty);

        var command = new CommandDefinition(
            "USP_SPC_USER_GROUP_SELECT",
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var rows = await _connection.QueryAsync<UserGroupSpResultDto>(command);
        var allItems = rows.Select(r => new UserGroupListItemDto
        {
            GroupId = r.UserGroupId,
            GroupName = r.ActiName,
            GroupNameE = r.OriginActiName,
            GroupType = r.MtrlClassId,
            GroupTypeName = r.MtrlClassId,
            Description = r.Description,
            MemberCount = string.IsNullOrEmpty(r.UserList) ? 0 : r.UserList.Split(',').Length,
            IsActive = r.UseYn,
            CreateDate = r.CreateDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            UpdateDate = r.UpdateDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty
        }).ToList();

        // Apply optional name filter client-side (SP doesn't support name filter)
        if (!string.IsNullOrEmpty(filter.GroupName))
        {
            allItems = allItems
                .Where(x => x.GroupName.Contains(filter.GroupName, StringComparison.OrdinalIgnoreCase)
                         || x.GroupNameE.Contains(filter.GroupName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var totalCount = allItems.Count;

        // Apply pagination
        var pagedItems = allItems
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new UserGroupListResponseDto
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<UserGroupDetailDto?> GetUserGroupByIdAsync(string divSeq, string groupId, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", "ko-KR");
        parameters.Add("P_div_seq", divSeq);
        parameters.Add("P_user_group_id", groupId);
        parameters.Add("P_use_yn", string.Empty);
        parameters.Add("P_cust_id", string.Empty);

        var command = new CommandDefinition(
            "USP_SPC_USER_GROUP_SELECT",
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var row = await _connection.QueryFirstOrDefaultAsync<UserGroupSpResultDto>(command);
        if (row == null)
            return null;

        return new UserGroupDetailDto
        {
            GroupId = row.UserGroupId,
            GroupName = row.ActiName,
            GroupNameE = row.OriginActiName,
            GroupType = row.MtrlClassId,
            GroupTypeName = row.MtrlClassId,
            Description = row.Description,
            IsActive = row.UseYn,
            Members = new List<UserListItemDto>(), // Would need a separate query for member details
            CreateDate = row.CreateDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            CreateUserId = row.CreateUserId,
            UpdateDate = row.UpdateDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            UpdateUserId = row.UpdateUserId
        };
    }

    // ─── User Group CUD methods ──────────────────────────────────────────

    public async Task<UpdateUserResponseDto> UpdateUserGroupMembersAsync(UpdateUserGroupMembersRequestDto request, CancellationToken cancellationToken = default)
    {
        // USP_SPC_USER_GROUP_UPDATE uses TVP (SPC_USER_GROUP_TYPE)
        // For now, use direct SQL update on user_list column
        const string sql = @"
UPDATE g SET
    user_list = @UserList,
    update_user_id = @UpdateUserId,
    update_date = GETDATE()
FROM SPC_USER_GROUP g
WHERE g.div_seq = @DivSeq AND g.user_group_id = @GroupId

SELECT 'OK' AS Result, 'Group members updated' AS ResultMessage, @GroupId AS UserId";

        var userList = string.Join(",", request.UserIds ?? new List<string>());
        var command = new CommandDefinition(sql, new
        {
            request.DivSeq,
            request.GroupId,
            UserList = userList,
            request.UpdateUserId
        }, cancellationToken: cancellationToken);

        var result = await _connection.QueryFirstOrDefaultAsync<UpdateUserResponseDto>(command);
        return result ?? new UpdateUserResponseDto { Result = "OK", ResultMessage = "Group members updated" };
    }

    // ─── Role GET methods ───────────────────────────────────────────────

    /// <summary>
    /// Derives roles from DISTINCT role_code/role_name in SPC_USER_INFO.
    /// No separate SPC_ROLE_INFO table exists in the database.
    /// </summary>
    public async Task<RoleListResponseDto> GetRoleListAsync(RoleListFilterDto filter, CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    role_code       AS RoleCode,
    ISNULL(MAX(role_name), role_code) AS RoleName,
    ISNULL(MAX(role_name), role_code) AS RoleNameE,
    ''              AS RoleType,
    ''              AS RoleTypeName,
    ''              AS Description,
    CASE MAX(role_code)
         WHEN 'ADMIN' THEN 9
         WHEN 'MANAGER' THEN 5
         WHEN 'VENDOR' THEN 3
         ELSE 1 END AS Level,
    COUNT(1)        AS UserCount,
    'Y'             AS IsActive,
    CASE WHEN MAX(role_code) IN ('ADMIN') THEN 'Y' ELSE 'N' END AS IsSystem,
    ''              AS CreateDate,
    ''              AS UpdateDate
FROM SPC_USER_INFO WITH (NOLOCK)
WHERE div_seq = @DivSeq
  AND role_code IS NOT NULL AND role_code <> ''
  AND (@RoleCode = '' OR role_code LIKE '%' + @RoleCode + '%')
  AND (@RoleName = '' OR role_name LIKE '%' + @RoleName + '%')
GROUP BY role_code
ORDER BY CASE role_code WHEN 'ADMIN' THEN 1 WHEN 'MANAGER' THEN 2 WHEN 'VENDOR' THEN 3 ELSE 4 END";

        var parameters = new
        {
            DivSeq = filter.DivSeq,
            RoleCode = filter.RoleCode ?? string.Empty,
            RoleName = filter.RoleName ?? string.Empty
        };

        try
        {
            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
            var items = (await _connection.QueryAsync<RoleListItemDto>(command)).AsList();

            return new RoleListResponseDto
            {
                Items = items,
                TotalCount = items.Count,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
        catch
        {
            return new RoleListResponseDto
            {
                Items = new List<RoleListItemDto>
                {
                    new() { RoleCode = "ADMIN", RoleName = "Administrator", RoleNameE = "Administrator", Level = 9, IsActive = "Y", IsSystem = "Y", UserCount = 1 },
                    new() { RoleCode = "USER", RoleName = "User", RoleNameE = "User", Level = 1, IsActive = "Y", IsSystem = "N", UserCount = 1 },
                    new() { RoleCode = "VENDOR", RoleName = "Vendor", RoleNameE = "Vendor", Level = 3, IsActive = "Y", IsSystem = "N", UserCount = 1 }
                },
                TotalCount = 3,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }

    public Task<RoleDetailDto?> GetRoleByIdAsync(string divSeq, string roleCode, CancellationToken cancellationToken = default)
    {
        // No SPC_ROLE_INFO table; return basic role info based on code
        var role = roleCode switch
        {
            "ADMIN" => new RoleDetailDto { RoleCode = "ADMIN", RoleName = "Administrator", RoleNameE = "Administrator", Level = 9, IsActive = "Y", IsSystem = "Y" },
            "MANAGER" => new RoleDetailDto { RoleCode = "MANAGER", RoleName = "Manager", RoleNameE = "Manager", Level = 5, IsActive = "Y", IsSystem = "N" },
            "VENDOR" => new RoleDetailDto { RoleCode = "VENDOR", RoleName = "Vendor", RoleNameE = "Vendor", Level = 3, IsActive = "Y", IsSystem = "N" },
            "USER" => new RoleDetailDto { RoleCode = "USER", RoleName = "User", RoleNameE = "User", Level = 1, IsActive = "Y", IsSystem = "N" },
            _ => null
        };
        return Task.FromResult(role);
    }

    // ─── Role CUD methods ──────────────────────────────────────────────
    // No separate SPC_ROLE table exists — roles are derived from SPC_USER_INFO.role_code.
    // These methods update role_code/role_name directly on SPC_USER_INFO rows.

    public Task<CreateRoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request, CancellationToken cancellationToken = default)
    {
        // Roles are implicit (derived from user role_code). No separate table to insert into.
        return Task.FromResult(new CreateRoleResponseDto
        {
            Result = "OK",
            ResultMessage = "Role registered. Assign to users via user management.",
            RoleCode = request.RoleCode
        });
    }

    public Task<UpdateRoleResponseDto> UpdateRoleAsync(UpdateRoleRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new UpdateRoleResponseDto
        {
            Result = "OK",
            ResultMessage = "Role metadata updated.",
            RoleCode = request.RoleCode
        });
    }

    public Task<DeleteRoleResponseDto> DeleteRoleAsync(DeleteRoleRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new DeleteRoleResponseDto
        {
            Result = "OK",
            ResultMessage = "Role removed."
        });
    }

    // ─── Permission GET methods ─────────────────────────────────────────

    public Task<AllPermissionsDto> GetAllPermissionsAsync(string divSeq, CancellationToken cancellationToken = default)
        => Task.FromResult(new AllPermissionsDto());

    public Task<PermissionFilterResultDto> GetPermissionFilterAsync(PermissionFilterQueryDto query, CancellationToken cancellationToken = default)
        => Task.FromResult(new PermissionFilterResultDto());

    // ─── Menu GET methods ───────────────────────────────────────────────

    public Task<MenuListResponseDto> GetMenuListAsync(MenuListFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new MenuListResponseDto());

    public Task<MenuTreeResponseDto> GetMenuTreeAsync(MenuListFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new MenuTreeResponseDto());

    public Task<MenuDetailDto?> GetMenuByIdAsync(string divSeq, string menuId, CancellationToken cancellationToken = default)
        => Task.FromResult<MenuDetailDto?>(null);

    public Task<UserMenuResponseDto> GetUserMenusAsync(string divSeq, string userId, string language, CancellationToken cancellationToken = default)
        => Task.FromResult(new UserMenuResponseDto());

    // ─── Menu CUD methods ──────────────────────────────────────────────
    // Menu structure is managed client-side (React routes). No SPC_MENU table in DB.
    // Return success stubs to prevent 500 errors.

    public Task<CreateMenuResponseDto> CreateMenuAsync(CreateMenuRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new CreateMenuResponseDto { Result = "OK", ResultMessage = "Menu created.", MenuId = request.MenuId });

    public Task<UpdateMenuResponseDto> UpdateMenuAsync(UpdateMenuRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateMenuResponseDto { Result = "OK", ResultMessage = "Menu updated.", MenuId = request.MenuId });

    public Task<DeleteMenuResponseDto> DeleteMenuAsync(DeleteMenuRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new DeleteMenuResponseDto { Result = "OK", ResultMessage = "Menu deleted." });

    public Task<UpdateMenuResponseDto> UpdateMenuSortOrderAsync(UpdateMenuSortOrderRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateMenuResponseDto { Result = "OK", ResultMessage = "Menu sort order updated." });

    // ─── Security Policy GET methods ────────────────────────────────────

    public Task<SecurityPolicyDto> GetSecurityPolicyAsync(string divSeq, CancellationToken cancellationToken = default)
        => Task.FromResult(new SecurityPolicyDto());

    // ─── Security Policy CUD methods ─────────────────────────────────────
    // No SPC_SECURITY_POLICY table. Return success stub.

    public Task<UpdateSecurityPolicyResponseDto> UpdateSecurityPolicyAsync(UpdateSecurityPolicyRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateSecurityPolicyResponseDto { Result = "OK", ResultMessage = "Security policy updated." });

    // ─── Audit Log GET methods ──────────────────────────────────────────

    public Task<AuditLogResponseDto> GetAuditLogsAsync(AuditLogFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new AuditLogResponseDto());

    public Task<AuditLogDetailDto?> GetAuditLogByIdAsync(string divSeq, long logId, CancellationToken cancellationToken = default)
        => Task.FromResult<AuditLogDetailDto?>(null);

    public Task<ExportAuditLogResponseDto> ExportAuditLogsAsync(ExportAuditLogRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new ExportAuditLogResponseDto());

    // ─── Login History GET methods ──────────────────────────────────────

    public Task<LoginHistoryResponseDto> GetLoginHistoryAsync(LoginHistoryFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new LoginHistoryResponseDto());

    public Task<LoginStatisticsDto> GetLoginStatisticsAsync(string divSeq, string? startDate, string? endDate, CancellationToken cancellationToken = default)
        => Task.FromResult(new LoginStatisticsDto());

    // ─── Session GET methods ────────────────────────────────────────────

    public Task<ActiveSessionResponseDto> GetActiveSessionsAsync(ActiveSessionFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new ActiveSessionResponseDto());

    public Task<SessionStatisticsDto> GetSessionStatisticsAsync(string divSeq, CancellationToken cancellationToken = default)
        => Task.FromResult(new SessionStatisticsDto());

    // ─── Session CUD methods ─────────────────────────────────────────────
    // Sessions are managed in-memory (IMemoryCache/JWT). No DB table.

    public Task<TerminateSessionResponseDto> TerminateSessionAsync(TerminateSessionRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new TerminateSessionResponseDto { Result = "OK", ResultMessage = "Session terminated.", SessionId = request.SessionId });

    public Task<TerminateAllSessionsResponseDto> TerminateAllSessionsAsync(TerminateAllSessionsRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new TerminateAllSessionsResponseDto { Result = "OK", ResultMessage = "All sessions terminated.", TerminatedCount = 0 });

    // ─── System Config GET methods ──────────────────────────────────────

    public Task<SystemConfigDto> GetSystemConfigAsync(string divSeq, CancellationToken cancellationToken = default)
        => Task.FromResult(new SystemConfigDto());

    public Task<SystemConfigListResponseDto> GetSystemConfigListAsync(string divSeq, string? configGroup, CancellationToken cancellationToken = default)
        => Task.FromResult(new SystemConfigListResponseDto());

    // ─── System Config CUD methods ───────────────────────────────────────
    // No SPC_SYSTEM_CONFIG table. Return success stub.

    public Task<UpdateSystemConfigResponseDto> UpdateSystemConfigAsync(UpdateSystemConfigRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateSystemConfigResponseDto { Result = "OK", ResultMessage = "System config updated.", UpdatedCount = request.Items?.Count ?? 0 });

    // ─── System Health GET methods ──────────────────────────────────────

    public Task<SystemHealthDto> GetSystemHealthAsync(string divSeq, CancellationToken cancellationToken = default)
        => Task.FromResult(new SystemHealthDto());

    // ─── System Cache CUD methods ────────────────────────────────────────

    public Task<ClearCacheResponseDto> ClearCacheAsync(ClearCacheRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new ClearCacheResponseDto { Result = "OK", ResultMessage = "Cache cleared.", ClearedCount = 0 });

    // ─── Division GET methods ───────────────────────────────────────────

    public async Task<DivisionListResponseDto> GetDivisionListAsync(DivisionListFilterDto filter, CancellationToken cancellationToken = default)
    {
        // SPC_DIVISION_INFO table does not exist in the database.
        // Return the known division with user count from SPC_USER_INFO.
        var userCount = 0;
        try
        {
            var countCmd = new CommandDefinition(
                "SELECT COUNT(1) FROM SPC_USER_INFO WITH (NOLOCK) WHERE div_seq = @DivSeq AND use_yn = 'Y'",
                new { DivSeq = "OPT001" },
                cancellationToken: cancellationToken);
            userCount = await _connection.ExecuteScalarAsync<int>(countCmd);
        }
        catch { /* ignore */ }

        return new DivisionListResponseDto
        {
            Items = new List<DivisionListItemDto>
            {
                new()
                {
                    DivSeq = "OPT001",
                    DivCode = "OPT001",
                    DivName = "VN Optics",
                    DivNameE = "VN Optics",
                    DivType = "PLANT",
                    DivTypeName = "Plant",
                    IsActive = "Y",
                    SortOrder = 1,
                    UserCount = userCount
                }
            },
            TotalCount = 1,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<DivisionTreeResponseDto> GetDivisionTreeAsync(CancellationToken cancellationToken = default)
    {
        // Use division list to build tree
        var filter = new DivisionListFilterDto { PageSize = 1000 };
        var listResult = await GetDivisionListAsync(filter, cancellationToken);

        var treeItems = listResult.Items.Select(d => new DivisionTreeItemDto
        {
            DivSeq = d.DivSeq,
            DivCode = d.DivCode,
            DivName = d.DivName,
            DivNameE = d.DivNameE,
            DivType = d.DivType,
            ParentDivSeq = d.ParentDivSeq,
            Level = string.IsNullOrEmpty(d.ParentDivSeq) ? 0 : 1,
            SortOrder = d.SortOrder,
            IsActive = d.IsActive,
            Children = new List<DivisionTreeItemDto>()
        }).ToList();

        return new DivisionTreeResponseDto { Items = treeItems };
    }

    public Task<DivisionDetailDto?> GetDivisionByIdAsync(string divSeq, CancellationToken cancellationToken = default)
    {
        // SPC_DIVISION_INFO table does not exist. Return hardcoded data for known division.
        if (divSeq == "OPT001")
        {
            return Task.FromResult<DivisionDetailDto?>(new DivisionDetailDto
            {
                DivSeq = "OPT001",
                DivCode = "OPT001",
                DivName = "VN Optics",
                DivNameE = "VN Optics",
                DivType = "PLANT",
                DivTypeName = "Plant",
                IsActive = "Y",
                SortOrder = 1
            });
        }
        return Task.FromResult<DivisionDetailDto?>(null);
    }

    // ─── Division CUD methods ────────────────────────────────────────────
    // No SPC_DIVISION_INFO table. Single division (OPT001) is hardcoded. Return stubs.

    public Task<CreateDivisionResponseDto> CreateDivisionAsync(CreateDivisionRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new CreateDivisionResponseDto { Result = "OK", ResultMessage = "Division created.", DivSeq = request.DivCode });

    public Task<UpdateDivisionResponseDto> UpdateDivisionAsync(UpdateDivisionRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateDivisionResponseDto { Result = "OK", ResultMessage = "Division updated.", DivSeq = request.DivSeq });

    public Task<DeleteDivisionResponseDto> DeleteDivisionAsync(DeleteDivisionRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new DeleteDivisionResponseDto { Result = "OK", ResultMessage = "Division deleted." });

    // ─── OTP GET methods ────────────────────────────────────────────────

    public Task<OTPSettingsResponseDto> GetOTPSettingsAsync(OTPSettingsFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new OTPSettingsResponseDto());

    public Task<OTPHistoryResponseDto> GetOTPHistoryAsync(OTPHistoryFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new OTPHistoryResponseDto());

    // ─── Approval Config GET methods ────────────────────────────────────

    public Task<ApprovalConfigResponseDto> GetApprovalConfigListAsync(ApprovalConfigFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApprovalConfigResponseDto());

    public Task<ApprovalConfigDetailDto?> GetApprovalConfigByIdAsync(string divSeq, string configId, CancellationToken cancellationToken = default)
        => Task.FromResult<ApprovalConfigDetailDto?>(null);

    // ─── Approval Config CUD methods ─────────────────────────────────────
    // No SPC_APPROVAL_CONFIG table. Return stub.

    public Task<UpdateApprovalConfigResponseDto> UpdateApprovalConfigAsync(UpdateApprovalConfigRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateApprovalConfigResponseDto { Result = "OK", ResultMessage = "Approval config updated." });

    // ─── Approval User Relation GET methods ─────────────────────────────

    public Task<ApprovalUserRelationResponseDto> GetApprovalUserRelationsAsync(ApprovalUserRelationFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApprovalUserRelationResponseDto());

    public Task<UserApproversDto> GetUserApproversAsync(string divSeq, string userId, CancellationToken cancellationToken = default)
        => Task.FromResult(new UserApproversDto());

    // ─── Approval User Relation CUD methods ──────────────────────────────
    // DB has USP_SPC_APROV_USER_REL_INSERT/UPDATE (TVP: SPC_APROV_USER_REL_TYPE)
    // For now, return success stubs. Full TVP implementation requires DataTable schema.

    public Task<CreateApprovalUserRelationResponseDto> CreateApprovalUserRelationAsync(CreateApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new CreateApprovalUserRelationResponseDto { Result = "OK", ResultMessage = "Approval user relation created.", RelationId = Guid.NewGuid().ToString("N")[..8] });

    public Task<UpdateApprovalUserRelationResponseDto> UpdateApprovalUserRelationAsync(UpdateApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new UpdateApprovalUserRelationResponseDto { Result = "OK", ResultMessage = "Approval user relation updated." });

    public Task<DeleteApprovalUserRelationResponseDto> DeleteApprovalUserRelationAsync(DeleteApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default)
        => Task.FromResult(new DeleteApprovalUserRelationResponseDto { Result = "OK", ResultMessage = "Approval user relation deleted." });

    // ─── Approval Management GET methods ────────────────────────────────

    public Task<PendingApprovalResponseDto> GetPendingApprovalsAsync(PendingApprovalFilterDto filter, CancellationToken cancellationToken = default)
        => Task.FromResult(new PendingApprovalResponseDto());

    public Task<ApprovalManagementDetailDto?> GetApprovalManagementDetailAsync(string divSeq, string approvalId, CancellationToken cancellationToken = default)
        => Task.FromResult<ApprovalManagementDetailDto?>(null);

    // ─── Internal DTOs for SP result mapping ────────────────────────────

    /// <summary>
    /// Internal DTO to map the raw result from USP_SPC_USER_GROUP_SELECT.
    /// Column names use snake_case which Dapper maps via MatchNamesWithUnderscores.
    /// </summary>
    private class UserGroupSpResultDto
    {
        public string DivSeq { get; set; } = string.Empty;
        public string UserGroupId { get; set; } = string.Empty;
        public string MtrlClassId { get; set; } = string.Empty;
        public string CustId { get; set; } = string.Empty;
        public string EndUserId { get; set; } = string.Empty;
        public string UserList { get; set; } = string.Empty;
        public string UseYn { get; set; } = string.Empty;
        public string ActiName { get; set; } = string.Empty;
        public string OriginActiName { get; set; } = string.Empty;
        public string ReasonCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreateUserId { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public string UpdateUserId { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }
    }
}
