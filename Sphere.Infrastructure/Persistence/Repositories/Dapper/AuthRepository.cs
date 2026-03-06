using System.Data;
using Dapper;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Auth 리포지토리 - Dapper(SP + raw SQL) 구현체
/// </summary>
public class AuthRepository : DapperRepositoryBase, IAuthRepository
{
    public AuthRepository(IDbConnection connection) : base(connection) { }

    #region 사용자 조회

    public async Task<LoginUserInfoDto?> GetLoginUserInfoAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                u.USER_ID        AS UserId,
                u.USER_NAME      AS UserName,
                u.EMAIL          AS Email,
                u.PASSWORD_HASH  AS PasswordHash,
                u.LAST_LOGIN_DATE AS LastLoginDate,
                u.DIV_SEQ        AS DivSeq,
                d.DIV_NAME       AS DivName,
                u.DEPT_ID        AS DeptId,
                dept.DEPT_NAME   AS DeptName,
                u.POSITION_ID    AS PositionId,
                pos.POSITION_NAME AS PositionName,
                u.VENDOR_ID      AS VendorId,
                v.VENDOR_NAME    AS VendorName,
                v.VENDOR_TYPE    AS VendorType,
                CASE WHEN u.AUTHORITY_LEVEL >= 9 THEN 1 ELSE 0 END AS IsAdmin,
                u.ROLE_ID        AS RoleId,
                r.ROLE_NAME      AS RoleName,
                COALESCE(u.LOCALE, 'ko-KR')          AS Locale,
                COALESCE(u.TIMEZONE, 'Asia/Seoul')    AS TimeZone,
                COALESCE(u.DATE_FORMAT, 'yyyy-MM-dd') AS DateFormat,
                COALESCE(u.NUMBER_FORMAT, '#,##0.##') AS NumberFormat
            FROM SPC_USER_INFO u
            LEFT JOIN SPC_DIVISION_INFO d    ON u.DIV_SEQ = d.DIV_SEQ
            LEFT JOIN SPC_DEPT_INFO dept     ON u.DEPT_ID = dept.DEPT_ID    AND u.DIV_SEQ = dept.DIV_SEQ
            LEFT JOIN SPC_POSITION_INFO pos  ON u.POSITION_ID = pos.POSITION_ID AND u.DIV_SEQ = pos.DIV_SEQ
            LEFT JOIN SPC_VENDOR_INFO v      ON u.VENDOR_ID = v.VENDOR_ID   AND u.DIV_SEQ = v.DIV_SEQ
            LEFT JOIN SPC_ROLE_INFO r        ON u.ROLE_ID = r.ROLE_ID       AND u.DIV_SEQ = r.DIV_SEQ
            WHERE u.USER_ID = @UserId
              AND u.DIV_SEQ = @DivSeq
              AND u.USE_YN = 'Y'";

        return await _connection.QueryFirstOrDefaultAsync<LoginUserInfoDto>(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq }, cancellationToken: cancellationToken));
    }

    public async Task<UserInfoDto?> GetUserInfoByIdAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                u.USER_ID          AS UserId,
                u.USER_NAME        AS UserName,
                u.USER_NAME_K      AS UserNameK,
                u.USER_NAME_E      AS UserNameE,
                u.EMAIL            AS Email,
                u.DIV_SEQ          AS DivSeq,
                u.DEPT_ID          AS DeptId,
                dept.DEPT_NAME     AS DeptName,
                u.POSITION_ID      AS PositionId,
                pos.POSITION_NAME  AS PositionName,
                u.VENDOR_ID        AS VendorId,
                u.ROLE_ID          AS RoleId,
                u.USER_GROUP_ID    AS UserGroupId,
                u.AUTHORITY_LEVEL  AS AuthorityLevel,
                u.PHONE_NUMBER     AS PhoneNumber,
                u.MOBILE_NUMBER    AS MobileNumber,
                u.EXTENSION        AS Extension,
                u.USE_YN           AS UseYn,
                u.CREATE_DATE      AS CreateDate,
                u.UPDATE_DATE      AS UpdateDate
            FROM SPC_USER_INFO u
            LEFT JOIN SPC_DEPT_INFO dept    ON u.DEPT_ID = dept.DEPT_ID       AND u.DIV_SEQ = dept.DIV_SEQ
            LEFT JOIN SPC_POSITION_INFO pos ON u.POSITION_ID = pos.POSITION_ID AND u.DIV_SEQ = pos.DIV_SEQ
            WHERE u.USER_ID = @UserId
              AND u.DIV_SEQ = @DivSeq";

        return await _connection.QueryFirstOrDefaultAsync<UserInfoDto>(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq }, cancellationToken: cancellationToken));
    }

    public async Task<UserAuthDto?> GetUserForAuthAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                u.USER_ID        AS UserId,
                u.DIV_SEQ        AS DivSeq,
                u.PASSWORD_HASH  AS PasswordHash,
                u.FAIL_COUNT     AS FailCount,
                u.IS_LOCKED      AS IsLocked,
                u.USE_YN         AS UseYn,
                u.LAST_LOGIN_DATE AS LastLoginDate,
                u.EMAIL          AS Email,
                u.USER_NAME      AS UserName,
                u.DEPT_ID        AS DeptId,
                dept.DEPT_NAME   AS DeptName,
                u.ROLE_ID        AS RoleId,
                r.ROLE_NAME      AS RoleName,
                u.LOCALE         AS Locale,
                u.TIMEZONE       AS Timezone
            FROM SPC_USER_INFO u
            LEFT JOIN SPC_DEPT_INFO dept ON u.DEPT_ID = dept.DEPT_ID AND u.DIV_SEQ = dept.DIV_SEQ
            LEFT JOIN SPC_ROLE_INFO r    ON u.ROLE_ID = r.ROLE_ID    AND u.DIV_SEQ = r.DIV_SEQ
            WHERE u.USER_ID = @UserId
              AND u.DIV_SEQ = @DivSeq";

        return await _connection.QueryFirstOrDefaultAsync<UserAuthDto>(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq }, cancellationToken: cancellationToken));
    }

    public async Task<bool> ValidatePasswordAsync(
        string userId,
        string passwordHash,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT COUNT(1)
            FROM SPC_USER_INFO
            WHERE USER_ID = @UserId
              AND PASSWORD_HASH = @PasswordHash
              AND USE_YN = 'Y'";

        var count = await _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, new { UserId = userId, PasswordHash = passwordHash }, cancellationToken: cancellationToken));
        return count > 0;
    }

    public async Task<IEnumerable<string>> GetRolePermissionsAsync(
        string divSeq,
        string roleCode,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT PERMISSION_CODE
            FROM SPC_ROLE_PERMISSION
            WHERE DIV_SEQ = @DivSeq
              AND ROLE_CODE = @RoleCode
              AND USE_YN = 'Y'";

        return await _connection.QueryAsync<string>(
            new CommandDefinition(sql, new { DivSeq = divSeq, RoleCode = roleCode }, cancellationToken: cancellationToken));
    }

    #endregion

    #region 사용자 업데이트

    public async Task UpdateLoginFailAsync(
        string userId,
        string divSeq,
        int failCount,
        string isLocked,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_INFO
            SET FAIL_COUNT  = @FailCount,
                IS_LOCKED   = @IsLocked,
                UPDATE_DATE = GETDATE()
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq, FailCount = failCount, IsLocked = isLocked }, cancellationToken: cancellationToken));
    }

    public async Task UpdateLoginSuccessAsync(
        string userId,
        string divSeq,
        DateTime loginTime,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_INFO
            SET FAIL_COUNT       = 0,
                IS_LOCKED        = 'N',
                LAST_LOGIN_DATE  = @LoginTime,
                UPDATE_DATE      = @LoginTime
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq, LoginTime = loginTime }, cancellationToken: cancellationToken));
    }

    public async Task UpdateLastLoginAsync(
        string userId,
        DateTime loginTime,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_INFO
            SET LAST_LOGIN_DATE = @LoginTime,
                UPDATE_DATE     = @LoginTime
            WHERE USER_ID = @UserId";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, LoginTime = loginTime }, cancellationToken: cancellationToken));
    }

    public async Task UpdatePasswordHashAsync(
        string userId,
        string divSeq,
        string newPasswordHash,
        string updateUserId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_INFO
            SET PASSWORD_HASH  = @NewPasswordHash,
                UPDATE_USER_ID = @UpdateUserId,
                UPDATE_DATE    = GETDATE()
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq, NewPasswordHash = newPasswordHash, UpdateUserId = updateUserId }, cancellationToken: cancellationToken));
    }

    public async Task ResetAndUnlockUserAsync(
        string userId,
        string divSeq,
        string newPasswordHash,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_INFO
            SET PASSWORD_HASH = @NewPasswordHash,
                FAIL_COUNT    = 0,
                IS_LOCKED     = 'N',
                UPDATE_DATE   = GETDATE()
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq, NewPasswordHash = newPasswordHash }, cancellationToken: cancellationToken));
    }

    #endregion

    #region 세션

    public async Task<UserSessionDto?> GetActiveSessionAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT USER_ID  AS UserId,
                   DIV_SEQ  AS DivSeq,
                   IS_ACTIVE AS IsActive
            FROM SPC_USER_SESSION
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq
              AND IS_ACTIVE = 'Y'";

        return await _connection.QueryFirstOrDefaultAsync<UserSessionDto>(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq }, cancellationToken: cancellationToken));
    }

    public async Task UpdateSessionActiveAsync(
        string userId,
        string divSeq,
        string isActive,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_SESSION
            SET IS_ACTIVE   = @IsActive,
                UPDATE_DATE = GETDATE()
            WHERE USER_ID = @UserId
              AND DIV_SEQ  = @DivSeq
              AND IS_ACTIVE = 'Y'";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { UserId = userId, DivSeq = divSeq, IsActive = isActive }, cancellationToken: cancellationToken));
    }

    #endregion

    #region 권한 필터

    public async Task<IEnumerable<AuthorityFilterResultDto>> GetAuthorityFiltersAsync(
        AuthorityFilterRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", query.Language ?? "ko-KR");
        parameters.Add("P_div_seq", query.DivSeq);
        parameters.Add("P_login_vendor", query.UserId ?? string.Empty);
        parameters.Add("P_filter_type", query.CodeClassId ?? string.Empty);
        parameters.Add("P_vendor_list", string.Empty);
        parameters.Add("P_mtrlclass_list", string.Empty);

        return await _connection.QueryAsync<AuthorityFilterResultDto>(
            new CommandDefinition("USP_SPC_AUTHORITY_FILTER_SELECT", parameters,
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    #endregion

    #region Oath 관리

    public async Task<IEnumerable<OathMasterDto>> GetOathMasterListAsync(
        OathMasterFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", "ko-KR");
        parameters.Add("P_div_seq", filter.DivSeq);
        parameters.Add("P_start_date", string.Empty);
        parameters.Add("P_end_date", string.Empty);
        parameters.Add("P_req_vendor_id", filter.RequestVendor ?? string.Empty);
        parameters.Add("P_accept_vendor_id", filter.AcceptVendor ?? string.Empty);
        parameters.Add("P_doc_type", filter.OathDocId ?? string.Empty);
        parameters.Add("P_action_id", string.Empty);
        parameters.Add("P_oath_id", string.Empty);
        parameters.Add("P_search_option", filter.CompleteOath ?? string.Empty);

        return await _connection.QueryAsync<OathMasterDto>(
            new CommandDefinition("USP_SPC_OATH_MST_SELECT", parameters,
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<OathLoginDto>> GetOathForLoginAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", "ko-KR");
        parameters.Add("P_div_seq", divSeq);
        parameters.Add("P_vendor_id", userId);
        parameters.Add("P_sysmanager_flag", string.Empty);

        return await _connection.QueryAsync<OathLoginDto>(
            new CommandDefinition("USP_SPC_OATH_MST_SELECT_LOGIN", parameters,
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    public async Task<OathLoginLinkDto?> GetOathLoginLinkAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                CONVERT(VARCHAR(20), om.create_date, 120) AS start_date,
                om.expire_time   AS end_date,
                om.request_vendor AS req_vendor,
                om.accept_vendor,
                om.oath_doc_id   AS oath_doc_type,
                om.oath_action_id
            FROM SPC_OATH_MST om
            WHERE om.div_seq = @div_seq AND om.oath_id = @oath_id";

        return await _connection.QueryFirstOrDefaultAsync<OathLoginLinkDto>(
            new CommandDefinition(sql, new { div_seq = divSeq, oath_id = oathId }, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<OathHistoryDto>> GetOathHistoryAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_Lang_Type", "ko-KR");
        parameters.Add("P_div_seq", divSeq);
        parameters.Add("P_oath_id", oathId);

        return await _connection.QueryAsync<OathHistoryDto>(
            new CommandDefinition("USP_SPC_OATH_MST_HIST_SELECT", parameters,
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    public async Task<OathDocumentDto?> GetOathDocumentAsync(
        string divSeq,
        string oathDocId,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("P_div_seq", divSeq);
        parameters.Add("P_oath_id", oathDocId);

        return await _connection.QueryFirstOrDefaultAsync<OathDocumentDto>(
            new CommandDefinition("USP_SPC_OATH_DOC_SELECT", parameters,
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    #endregion
}
