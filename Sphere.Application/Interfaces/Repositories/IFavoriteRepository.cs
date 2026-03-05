using Sphere.Application.DTOs.Favorite;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// 즐겨찾기 Repository 인터페이스
/// </summary>
/// <remarks>
/// SPC_USER_FAVORITE 테이블에 대한 CRUD 작업을 정의합니다.
/// 사용자별 즐겨찾기 메뉴 관리 기능을 제공합니다.
/// </remarks>
public interface IFavoriteRepository
{
    /// <summary>
    /// 사용자의 즐겨찾기 목록 조회 (표시 순서대로 정렬)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>즐겨찾기 목록 (표시 순서대로 정렬)</returns>
    Task<IEnumerable<FavoriteDto>> GetUserFavoritesAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 특정 즐겨찾기 조회
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="menuId">메뉴 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>즐겨찾기 정보 (없으면 null)</returns>
    Task<FavoriteDto?> GetFavoriteByIdAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 즐겨찾기 추가
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="dto">생성 요청 DTO</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>작업 결과</returns>
    Task<FavoriteResultDto> AddFavoriteAsync(
        string divSeq,
        string userId,
        CreateFavoriteDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 즐겨찾기 순서 변경
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="menuId">메뉴 ID</param>
    /// <param name="dto">수정 요청 DTO</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>작업 결과</returns>
    Task<FavoriteResultDto> UpdateFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        UpdateFavoriteDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 즐겨찾기 삭제 (논리 삭제: use_yn = 'N')
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="userId">사용자 ID</param>
    /// <param name="menuId">메뉴 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>작업 결과</returns>
    Task<FavoriteResultDto> RemoveFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default);
}
