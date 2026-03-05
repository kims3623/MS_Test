namespace Sphere.Application.DTOs.Favorite;

/// <summary>
/// 즐겨찾기 조회 결과 DTO
/// </summary>
/// <remarks>
/// SPC_USER_FAVORITE 테이블 + VW_SPC_MENU 조인 결과를 담는 DTO입니다.
/// 사용자의 즐겨찾기 메뉴 목록 조회 시 사용됩니다.
/// </remarks>
public class FavoriteDto
{
    /// <summary>
    /// 행 식별자 (자동 증가)
    /// </summary>
    public long TableSysId { get; set; }

    /// <summary>
    /// 사업부 코드
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 사용자 ID
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 메뉴 ID
    /// </summary>
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// 메뉴명 (VW_SPC_MENU JOIN 결과)
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 메뉴 URL (VW_SPC_MENU JOIN 결과)
    /// </summary>
    public string MenuUrl { get; set; } = string.Empty;

    /// <summary>
    /// 표시 순서
    /// </summary>
    public int DisplaySeq { get; set; }
}
