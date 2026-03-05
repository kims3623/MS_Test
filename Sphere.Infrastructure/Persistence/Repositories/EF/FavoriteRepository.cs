using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Favorite;
using Sphere.Application.Interfaces.Repositories;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Repositories.EF;

/// <summary>
/// EF Core implementation of IFavoriteRepository.
/// Manages user favorites (SPC_USER_FAVORITE table) with menu information from VW_SPC_MENU.
/// </summary>
public class FavoriteRepository : IFavoriteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FavoriteRepository> _logger;

    public FavoriteRepository(
        ApplicationDbContext context,
        ILogger<FavoriteRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FavoriteDto>> GetUserFavoritesAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Getting user favorites for DivSeq: {DivSeq}, UserId: {UserId}",
            divSeq, userId);

        try
        {
            // Query UserFavorite with JOIN to Menu for menu information
            // Fallback: if SPC_MENU table doesn't exist, return favorites without menu info
            try
            {
                var query = from uf in _context.UserFavorites
                            join m in _context.Menus
                                on new { uf.DivSeq, uf.MenuId } equals new { m.DivSeq, m.MenuId }
                                into menuJoin
                            from menu in menuJoin.DefaultIfEmpty()
                            where uf.DivSeq == divSeq
                                  && uf.UserId == userId
                                  && uf.UseYn == "Y"
                            orderby uf.DspSeq
                            select new FavoriteDto
                            {
                                TableSysId = uf.TableSysId,
                                DivSeq = uf.DivSeq,
                                UserId = uf.UserId,
                                MenuId = uf.MenuId,
                                MenuName = menu != null ? menu.MenuName : string.Empty,
                                MenuUrl = menu != null ? menu.MenuUrl : string.Empty,
                                DisplaySeq = uf.DspSeq
                            };

                var result = await query.AsNoTracking().ToListAsync(cancellationToken);

                _logger.LogDebug(
                    "Found {Count} favorites for UserId: {UserId}",
                    result.Count, userId);

                return result;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                // SPC_MENU table may not exist - fallback to favorites without menu info
                _logger.LogWarning("SPC_MENU table not available, returning favorites without menu info");

                var fallbackQuery = _context.UserFavorites
                    .Where(uf => uf.DivSeq == divSeq && uf.UserId == userId && uf.UseYn == "Y")
                    .OrderBy(uf => uf.DspSeq)
                    .Select(uf => new FavoriteDto
                    {
                        TableSysId = uf.TableSysId,
                        DivSeq = uf.DivSeq,
                        UserId = uf.UserId,
                        MenuId = uf.MenuId,
                        MenuName = uf.MenuId,
                        MenuUrl = string.Empty,
                        DisplaySeq = uf.DspSeq
                    });

                return await fallbackQuery.AsNoTracking().ToListAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting user favorites for DivSeq: {DivSeq}, UserId: {UserId}",
                divSeq, userId);
            return new List<FavoriteDto>();
        }
    }

    /// <inheritdoc />
    public async Task<FavoriteDto?> GetFavoriteByIdAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Getting favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
            divSeq, userId, menuId);

        try
        {
            try
            {
                var query = from uf in _context.UserFavorites
                            join m in _context.Menus
                                on new { uf.DivSeq, uf.MenuId } equals new { m.DivSeq, m.MenuId }
                                into menuJoin
                            from menu in menuJoin.DefaultIfEmpty()
                            where uf.DivSeq == divSeq
                                  && uf.UserId == userId
                                  && uf.MenuId == menuId
                                  && uf.UseYn == "Y"
                            select new FavoriteDto
                            {
                                TableSysId = uf.TableSysId,
                                DivSeq = uf.DivSeq,
                                UserId = uf.UserId,
                                MenuId = uf.MenuId,
                                MenuName = menu != null ? menu.MenuName : string.Empty,
                                MenuUrl = menu != null ? menu.MenuUrl : string.Empty,
                                DisplaySeq = uf.DspSeq
                            };

                return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                _logger.LogWarning("SPC_MENU table not available, returning favorite without menu info");
                return await _context.UserFavorites
                    .Where(uf => uf.DivSeq == divSeq && uf.UserId == userId && uf.MenuId == menuId && uf.UseYn == "Y")
                    .Select(uf => new FavoriteDto
                    {
                        TableSysId = uf.TableSysId,
                        DivSeq = uf.DivSeq,
                        UserId = uf.UserId,
                        MenuId = uf.MenuId,
                        MenuName = uf.MenuId,
                        MenuUrl = string.Empty,
                        DisplaySeq = uf.DspSeq
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
                divSeq, userId, menuId);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<FavoriteResultDto> AddFavoriteAsync(
        string divSeq,
        string userId,
        CreateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Adding favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
            divSeq, userId, dto.MenuId);

        try
        {
            // Check if already exists (including soft-deleted)
            var existing = await _context.UserFavorites
                .FirstOrDefaultAsync(uf =>
                    uf.DivSeq == divSeq &&
                    uf.UserId == userId &&
                    uf.MenuId == dto.MenuId,
                    cancellationToken);

            if (existing != null)
            {
                if (existing.UseYn == "Y")
                {
                    return new FavoriteResultDto
                    {
                        Success = false,
                        Message = "이미 즐겨찾기에 추가된 메뉴입니다.",
                        MenuId = dto.MenuId
                    };
                }

                // Reactivate soft-deleted favorite
                existing.UseYn = "Y";
                existing.DspSeq = dto.DisplaySeq ?? await GetNextDisplaySeqAsync(divSeq, userId, cancellationToken);
                existing.UpdateDate = DateTime.UtcNow;
                existing.UpdateUserId = userId;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Reactivated favorite for UserId: {UserId}, MenuId: {MenuId}",
                    userId, dto.MenuId);

                return new FavoriteResultDto
                {
                    Success = true,
                    Message = "즐겨찾기가 성공적으로 추가되었습니다.",
                    MenuId = dto.MenuId
                };
            }

            // Calculate display sequence if not provided
            var displaySeq = dto.DisplaySeq ?? await GetNextDisplaySeqAsync(divSeq, userId, cancellationToken);

            // Create new favorite
            var favorite = new UserFavorite
            {
                DivSeq = divSeq,
                UserId = userId,
                MenuId = dto.MenuId,
                DspSeq = displaySeq,
                AddedDate = DateTime.UtcNow,
                UseYn = "Y",
                CreateUserId = userId,
                CreateDate = DateTime.UtcNow
            };

            await _context.UserFavorites.AddAsync(favorite, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Added favorite for UserId: {UserId}, MenuId: {MenuId}, DisplaySeq: {DisplaySeq}",
                userId, dto.MenuId, displaySeq);

            return new FavoriteResultDto
            {
                Success = true,
                Message = "즐겨찾기가 성공적으로 추가되었습니다.",
                MenuId = dto.MenuId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error adding favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
                divSeq, userId, dto.MenuId);

            return new FavoriteResultDto
            {
                Success = false,
                Message = $"즐겨찾기 추가 중 오류가 발생했습니다: {ex.Message}",
                MenuId = dto.MenuId
            };
        }
    }

    /// <inheritdoc />
    public async Task<FavoriteResultDto> UpdateFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        UpdateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Updating favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}, NewDisplaySeq: {DisplaySeq}",
            divSeq, userId, menuId, dto.DisplaySeq);

        try
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf =>
                    uf.DivSeq == divSeq &&
                    uf.UserId == userId &&
                    uf.MenuId == menuId &&
                    uf.UseYn == "Y",
                    cancellationToken);

            if (favorite == null)
            {
                return new FavoriteResultDto
                {
                    Success = false,
                    Message = "즐겨찾기를 찾을 수 없습니다.",
                    MenuId = menuId
                };
            }

            favorite.DspSeq = dto.DisplaySeq;
            favorite.UpdateDate = DateTime.UtcNow;
            favorite.UpdateUserId = userId;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Updated favorite display sequence for UserId: {UserId}, MenuId: {MenuId}, NewDisplaySeq: {DisplaySeq}",
                userId, menuId, dto.DisplaySeq);

            return new FavoriteResultDto
            {
                Success = true,
                Message = "즐겨찾기 순서가 성공적으로 변경되었습니다.",
                MenuId = menuId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error updating favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
                divSeq, userId, menuId);

            return new FavoriteResultDto
            {
                Success = false,
                Message = $"즐겨찾기 수정 중 오류가 발생했습니다: {ex.Message}",
                MenuId = menuId
            };
        }
    }

    /// <inheritdoc />
    public async Task<FavoriteResultDto> RemoveFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug(
            "Removing favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
            divSeq, userId, menuId);

        try
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf =>
                    uf.DivSeq == divSeq &&
                    uf.UserId == userId &&
                    uf.MenuId == menuId &&
                    uf.UseYn == "Y",
                    cancellationToken);

            if (favorite == null)
            {
                return new FavoriteResultDto
                {
                    Success = false,
                    Message = "즐겨찾기를 찾을 수 없습니다.",
                    MenuId = menuId
                };
            }

            // Soft delete: set use_yn = 'N'
            favorite.UseYn = "N";
            favorite.UpdateDate = DateTime.UtcNow;
            favorite.UpdateUserId = userId;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Removed (soft-deleted) favorite for UserId: {UserId}, MenuId: {MenuId}",
                userId, menuId);

            return new FavoriteResultDto
            {
                Success = true,
                Message = "즐겨찾기가 성공적으로 삭제되었습니다.",
                MenuId = menuId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error removing favorite for DivSeq: {DivSeq}, UserId: {UserId}, MenuId: {MenuId}",
                divSeq, userId, menuId);

            return new FavoriteResultDto
            {
                Success = false,
                Message = $"즐겨찾기 삭제 중 오류가 발생했습니다: {ex.Message}",
                MenuId = menuId
            };
        }
    }

    /// <summary>
    /// Gets the next display sequence for a user's favorites.
    /// </summary>
    private async Task<int> GetNextDisplaySeqAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken)
    {
        var maxSeq = await _context.UserFavorites
            .Where(uf => uf.DivSeq == divSeq && uf.UserId == userId && uf.UseYn == "Y")
            .MaxAsync(uf => (int?)uf.DspSeq, cancellationToken);

        return (maxSeq ?? 0) + 1;
    }
}
