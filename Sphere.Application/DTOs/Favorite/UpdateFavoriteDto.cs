namespace Sphere.Application.DTOs.Favorite;

/// <summary>
/// 즐겨찾기 순서 변경 요청 DTO
/// </summary>
/// <remarks>
/// PUT /api/v1/favorites/{menuId} 엔드포인트에서 사용됩니다.
/// 표시 순서는 0 이상의 정수여야 합니다.
/// </remarks>
public class UpdateFavoriteDto
{
    /// <summary>
    /// 표시 순서 (필수, 0 이상)
    /// </summary>
    [global::System.ComponentModel.DataAnnotations.Required(ErrorMessage = "표시 순서는 필수입니다.")]
    [global::System.ComponentModel.DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "표시 순서는 0 이상이어야 합니다.")]
    public int DisplaySeq { get; set; }
}
