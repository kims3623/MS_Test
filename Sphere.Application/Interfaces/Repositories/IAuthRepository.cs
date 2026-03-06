using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Auth 리포지토리 인터페이스 - 인증/인가 SP 및 직접 SQL 작업
/// </summary>
public interface IAuthRepository
{
    #region 사용자 조회

    /// <summary>
    /// 로그인용 전체 사용자 정보 조회 (USP_SPC_LOGIN_USER_SELECT 또는 raw SQL)
    /// </summary>
    Task<LoginUserInfoDto?> GetLoginUserInfoAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 사용자 상세 정보 조회 (UserInfoDto 반환)
    /// </summary>
    Task<UserInfoDto?> GetUserInfoByIdAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 로그인 인증용 사용자 데이터 조회 (PasswordHash, FailCount, IsLocked 포함)
    /// </summary>
    Task<UserAuthDto?> GetUserForAuthAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 비밀번호 해시 검증
    /// </summary>
    Task<bool> ValidatePasswordAsync(
        string userId,
        string passwordHash,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 역할별 권한 코드 목록 조회
    /// </summary>
    Task<IEnumerable<string>> GetRolePermissionsAsync(
        string divSeq,
        string roleCode,
        CancellationToken cancellationToken = default);

    #endregion

    #region 사용자 업데이트

    /// <summary>
    /// 로그인 실패 카운트 및 잠금 상태 업데이트
    /// </summary>
    Task UpdateLoginFailAsync(
        string userId,
        string divSeq,
        int failCount,
        string isLocked,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 로그인 성공: 실패 카운트 초기화 + 마지막 로그인 시간 갱신
    /// </summary>
    Task UpdateLoginSuccessAsync(
        string userId,
        string divSeq,
        DateTime loginTime,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 마지막 로그인 시간 갱신 (OTP 인증 후 사용)
    /// </summary>
    Task UpdateLastLoginAsync(
        string userId,
        DateTime loginTime,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 비밀번호 변경
    /// </summary>
    Task UpdatePasswordHashAsync(
        string userId,
        string divSeq,
        string newPasswordHash,
        string updateUserId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 비밀번호 초기화: 새 해시 설정 + 잠금 해제 + 실패 카운트 초기화
    /// </summary>
    Task ResetAndUnlockUserAsync(
        string userId,
        string divSeq,
        string newPasswordHash,
        CancellationToken cancellationToken = default);

    #endregion

    #region 세션

    /// <summary>
    /// 활성 세션 조회
    /// </summary>
    Task<UserSessionDto?> GetActiveSessionAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 세션 활성 상태 업데이트
    /// </summary>
    Task UpdateSessionActiveAsync(
        string userId,
        string divSeq,
        string isActive,
        CancellationToken cancellationToken = default);

    #endregion

    #region 권한 필터

    /// <summary>
    /// 권한 필터 드롭다운 조회 (USP_SPC_AUTHORITY_FILTER_SELECT)
    /// </summary>
    Task<IEnumerable<AuthorityFilterResultDto>> GetAuthorityFiltersAsync(
        AuthorityFilterRequestDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region Oath 관리

    Task<IEnumerable<OathMasterDto>> GetOathMasterListAsync(
        OathMasterFilterDto filter,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<OathLoginDto>> GetOathForLoginAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default);

    Task<OathLoginLinkDto?> GetOathLoginLinkAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<OathHistoryDto>> GetOathHistoryAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default);

    Task<OathDocumentDto?> GetOathDocumentAsync(
        string divSeq,
        string oathDocId,
        CancellationToken cancellationToken = default);

    #endregion
}
