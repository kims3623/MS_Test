namespace Sphere.Application.DTOs.Favorite;

/// <summary>
/// 즐겨찾기 작업 결과 DTO
/// </summary>
/// <remarks>
/// 즐겨찾기 생성, 수정, 삭제 작업의 결과를 담는 DTO입니다.
/// </remarks>
public class FavoriteResultDto
{
    /// <summary>
    /// 성공 여부
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 결과 메시지
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 처리된 메뉴 ID (선택, 작업 대상 식별용)
    /// </summary>
    public string? MenuId { get; set; }
}
