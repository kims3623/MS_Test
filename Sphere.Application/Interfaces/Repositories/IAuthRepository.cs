using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Auth repository interface for authentication and authorization stored procedure operations.
/// </summary>
public interface IAuthRepository
{
    #region User Authentication Methods

    /// <summary>
    /// Gets login user information for authentication.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Login user info DTO or null if not found</returns>
    Task<LoginUserInfoDto?> GetLoginUserInfoAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user information by user ID and division.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User info DTO or null if not found</returns>
    Task<UserInfoDto?> GetUserInfoByIdAsync(
        string userId,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates user password against stored hash.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="passwordHash">Password hash to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if password is valid, false otherwise</returns>
    Task<bool> ValidatePasswordAsync(
        string userId,
        string passwordHash,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the last login timestamp for a user.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="loginTime">Login timestamp</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateLastLoginAsync(
        string userId,
        DateTime loginTime,
        CancellationToken cancellationToken = default);

    #endregion

    #region Authority Filter Methods

    /// <summary>
    /// Gets authority filter dropdown data. (USP_SPC_AUTHORITY_FILTER_SELECT)
    /// </summary>
    /// <param name="query">Authority filter query parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of authority filter results</returns>
    Task<IEnumerable<AuthorityFilterResultDto>> GetAuthorityFiltersAsync(
        AuthorityFilterRequestDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region Oath Management Methods

    /// <summary>
    /// Gets oath master list. (USP_SPC_OATH_MST_SELECT)
    /// </summary>
    /// <param name="filter">Oath master filter parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of oath master records</returns>
    Task<IEnumerable<OathMasterDto>> GetOathMasterListAsync(
        OathMasterFilterDto filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets oath info for login. (USP_SPC_OATH_MST_SELECT_LOGIN)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="userId">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of oath login records</returns>
    Task<IEnumerable<OathLoginDto>> GetOathForLoginAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets oath login link info. (TODO [P3]: No DB USP - use USP_SPC_OATH_MST_SELECT_LOGIN)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="oathId">Oath ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Oath login link DTO or null if not found</returns>
    Task<OathLoginLinkDto?> GetOathLoginLinkAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets oath history. (USP_SPC_OATH_MST_HIST_SELECT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="oathId">Oath ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of oath history records</returns>
    Task<IEnumerable<OathHistoryDto>> GetOathHistoryAsync(
        string divSeq,
        string oathId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets oath document. (USP_SPC_OATH_DOC_SELECT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="oathDocId">Oath document ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Oath document DTO or null if not found</returns>
    Task<OathDocumentDto?> GetOathDocumentAsync(
        string divSeq,
        string oathDocId,
        CancellationToken cancellationToken = default);

    #endregion
}
