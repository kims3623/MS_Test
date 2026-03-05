namespace Sphere.Application.DTOs.Favorite;

/// <summary>
/// 즐겨찾기 생성 요청 DTO
/// </summary>
/// <remarks>
/// POST /api/v1/favorites 엔드포인트에서 사용됩니다.
/// MenuId는 필수이며, DisplaySeq는 선택적입니다.
/// </remarks>
public class CreateFavoriteDto
{
    /// <summary>
    /// 메뉴 ID (필수)
    /// </summary>
    [global::System.ComponentModel.DataAnnotations.Required(ErrorMessage = "메뉴 ID는 필수입니다.")]
    [global::System.ComponentModel.DataAnnotations.StringLength(40, ErrorMessage = "메뉴 ID는 최대 40자입니다.")]
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// 표시 순서 (선택, 미지정 시 마지막 순서로 추가)
    /// </summary>
    public int? DisplaySeq { get; set; }
}
