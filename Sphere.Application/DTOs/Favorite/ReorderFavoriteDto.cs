using System.ComponentModel.DataAnnotations;

namespace Sphere.Application.DTOs.Favorite;

/// <summary>
/// 즐겨찾기 일괄 순서 변경 요청 DTO
/// </summary>
/// <remarks>
/// PUT /api/v1/favorites/reorder 엔드포인트에서 사용됩니다.
/// 여러 즐겨찾기의 표시 순서를 한 번에 변경할 때 사용합니다.
/// </remarks>
public class ReorderFavoriteDto
{
    /// <summary>
    /// 메뉴 ID (필수)
    /// </summary>
    /// <example>MENU001</example>
    [Required(ErrorMessage = "메뉴 ID는 필수입니다.")]
    [StringLength(50, ErrorMessage = "메뉴 ID는 50자를 초과할 수 없습니다.")]
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// 표시 순서 (필수, 0 이상)
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "표시 순서는 필수입니다.")]
    [Range(0, int.MaxValue, ErrorMessage = "표시 순서는 0 이상이어야 합니다.")]
    public int DisplaySeq { get; set; }
}
