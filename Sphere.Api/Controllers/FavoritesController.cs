using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Favorite;
using Sphere.Application.Interfaces.Repositories;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for user favorites management operations.
/// </summary>
/// <remarks>
/// 사용자별 즐겨찾기 메뉴 관리 기능을 제공합니다.
/// 즐겨찾기 조회, 추가, 순서 변경, 삭제 등의 작업을 수행합니다.
/// </remarks>
[ApiController]
[Route("api/v1/favorites")]
[Produces("application/json")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteRepository _repository;
    private readonly ILogger<FavoritesController> _logger;

    public FavoritesController(
        IFavoriteRepository repository,
        ILogger<FavoritesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// JWT 클레임에서 사업부 코드(div_seq)를 추출합니다.
    /// </summary>
    /// <returns>사업부 코드 (기본값: "001")</returns>
    private string GetDivSeq() => User.FindFirst("div_seq")?.Value ?? "OPT001";

    /// <summary>
    /// JWT 클레임에서 사용자 ID를 추출합니다.
    /// </summary>
    /// <returns>사용자 ID</returns>
    private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    /// <summary>
    /// 내 즐겨찾기 목록을 조회합니다.
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>즐겨찾기 목록 (표시 순서대로 정렬)</returns>
    /// <response code="200">조회 성공</response>
    /// <response code="401">인증 실패</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FavoriteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMyFavorites(CancellationToken cancellationToken = default)
    {
        var divSeq = GetDivSeq();
        var userId = GetUserId();

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 목록 조회 요청 (사업부: {DivSeq})",
            userId, divSeq);

        var result = await _repository.GetUserFavoritesAsync(divSeq, userId, cancellationToken);

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 {Count}개 조회 완료",
            userId, result.Count());

        return Ok(result);
    }

    /// <summary>
    /// 새로운 즐겨찾기를 추가합니다.
    /// </summary>
    /// <param name="dto">즐겨찾기 추가 요청 정보</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>추가 결과</returns>
    /// <response code="201">추가 성공</response>
    /// <response code="400">유효성 검증 실패</response>
    /// <response code="401">인증 실패</response>
    /// <response code="409">이미 존재하는 즐겨찾기</response>
    [HttpPost]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddFavorite(
        [FromBody] CreateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var divSeq = GetDivSeq();
        var userId = GetUserId();

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 추가 요청 - 메뉴: {MenuId} (사업부: {DivSeq})",
            userId, dto.MenuId, divSeq);

        var result = await _repository.AddFavoriteAsync(divSeq, userId, dto, cancellationToken);

        if (!result.Success)
        {
            if (result.Message?.Contains("이미 존재") == true)
            {
                _logger.LogWarning(
                    "사용자 {UserId}의 즐겨찾기 추가 실패 - 중복: {MenuId}",
                    userId, dto.MenuId);
                return Conflict(result);
            }

            _logger.LogWarning(
                "사용자 {UserId}의 즐겨찾기 추가 실패: {Message}",
                userId, result.Message);
            return BadRequest(result);
        }

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 추가 성공 - 메뉴: {MenuId}",
            userId, dto.MenuId);

        return CreatedAtAction(nameof(GetMyFavorites), result);
    }

    /// <summary>
    /// 즐겨찾기 순서를 변경합니다.
    /// </summary>
    /// <param name="menuId">메뉴 ID</param>
    /// <param name="dto">순서 변경 요청 정보</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>변경 결과</returns>
    /// <response code="200">변경 성공</response>
    /// <response code="400">유효성 검증 실패</response>
    /// <response code="401">인증 실패</response>
    /// <response code="404">즐겨찾기를 찾을 수 없음</response>
    [HttpPut("{menuId}")]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFavorite(
        string menuId,
        [FromBody] UpdateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var divSeq = GetDivSeq();
        var userId = GetUserId();

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 순서 변경 요청 - 메뉴: {MenuId}, 순서: {DisplaySeq} (사업부: {DivSeq})",
            userId, menuId, dto.DisplaySeq, divSeq);

        var result = await _repository.UpdateFavoriteAsync(divSeq, userId, menuId, dto, cancellationToken);

        if (!result.Success)
        {
            _logger.LogWarning(
                "사용자 {UserId}의 즐겨찾기 순서 변경 실패 - 메뉴: {MenuId}, 사유: {Message}",
                userId, menuId, result.Message);
            return NotFound(result);
        }

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 순서 변경 성공 - 메뉴: {MenuId}",
            userId, menuId);

        return Ok(result);
    }

    /// <summary>
    /// 즐겨찾기를 삭제합니다.
    /// </summary>
    /// <param name="menuId">메뉴 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>삭제 결과</returns>
    /// <response code="200">삭제 성공</response>
    /// <response code="401">인증 실패</response>
    /// <response code="404">즐겨찾기를 찾을 수 없음</response>
    [HttpDelete("{menuId}")]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(FavoriteResultDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFavorite(
        string menuId,
        CancellationToken cancellationToken = default)
    {
        var divSeq = GetDivSeq();
        var userId = GetUserId();

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 삭제 요청 - 메뉴: {MenuId} (사업부: {DivSeq})",
            userId, menuId, divSeq);

        var result = await _repository.RemoveFavoriteAsync(divSeq, userId, menuId, cancellationToken);

        if (!result.Success)
        {
            _logger.LogWarning(
                "사용자 {UserId}의 즐겨찾기 삭제 실패 - 메뉴: {MenuId}, 사유: {Message}",
                userId, menuId, result.Message);
            return NotFound(result);
        }

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 삭제 성공 - 메뉴: {MenuId}",
            userId, menuId);

        return Ok(result);
    }

    /// <summary>
    /// 즐겨찾기 순서를 일괄 변경합니다.
    /// </summary>
    /// <param name="items">순서 변경 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>변경 결과</returns>
    /// <response code="200">변경 성공</response>
    /// <response code="400">유효성 검증 실패</response>
    /// <response code="401">인증 실패</response>
    [HttpPut("reorder")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ReorderFavorites(
        [FromBody] List<ReorderFavoriteDto> items,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (items == null || items.Count == 0)
        {
            return BadRequest(new { Success = false, Message = "순서 변경 항목이 없습니다." });
        }

        var divSeq = GetDivSeq();
        var userId = GetUserId();

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 일괄 순서 변경 요청 - {Count}개 항목 (사업부: {DivSeq})",
            userId, items.Count, divSeq);

        var failedItems = new List<string>();

        foreach (var item in items)
        {
            var result = await _repository.UpdateFavoriteAsync(
                divSeq,
                userId,
                item.MenuId,
                new UpdateFavoriteDto { DisplaySeq = item.DisplaySeq },
                cancellationToken);

            if (!result.Success)
            {
                failedItems.Add(item.MenuId);
                _logger.LogWarning(
                    "사용자 {UserId}의 즐겨찾기 순서 변경 실패 - 메뉴: {MenuId}",
                    userId, item.MenuId);
            }
        }

        if (failedItems.Count > 0)
        {
            _logger.LogWarning(
                "사용자 {UserId}의 즐겨찾기 일괄 순서 변경 부분 실패 - {FailedCount}/{TotalCount}개",
                userId, failedItems.Count, items.Count);

            return Ok(new
            {
                Success = true,
                Message = $"일부 항목({failedItems.Count}개)의 순서 변경에 실패했습니다.",
                FailedMenuIds = failedItems
            });
        }

        _logger.LogInformation(
            "사용자 {UserId}의 즐겨찾기 일괄 순서 변경 성공 - {Count}개 항목",
            userId, items.Count);

        return Ok(new { Success = true, Message = "순서가 변경되었습니다." });
    }
}
