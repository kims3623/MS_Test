using System.Data;
using Dapper;
using Sphere.Application.DTOs.Favorite;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// 즐겨찾기 리포지토리 - Dapper raw SQL 구현체
/// 테이블: SPC_USER_FAVORITE (JOIN SPC_MENU)
/// </summary>
public class FavoriteRepository : DapperRepositoryBase, IFavoriteRepository
{
    public FavoriteRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<FavoriteDto>> GetUserFavoritesAsync(
        string divSeq,
        string userId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                uf.div_seq      AS DivSeq,
                uf.user_id      AS UserId,
                uf.menu_id      AS MenuId,
                uf.dsp_seq      AS DisplaySeq,
                ISNULL(m.menu_name, '')  AS MenuName,
                ISNULL(m.menu_url,  '')  AS MenuUrl
            FROM SPC_USER_FAVORITE uf
            LEFT JOIN SPC_MENU m
                ON uf.div_seq = m.div_seq AND uf.menu_id = m.menu_id
            WHERE uf.div_seq = @DivSeq
              AND uf.user_id = @UserId
              AND uf.use_yn  = 'Y'
            ORDER BY uf.dsp_seq";

        return await _connection.QueryAsync<FavoriteDto>(
            new CommandDefinition(sql, new { DivSeq = divSeq, UserId = userId }, cancellationToken: cancellationToken));
    }

    public async Task<FavoriteDto?> GetFavoriteByIdAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                uf.div_seq      AS DivSeq,
                uf.user_id      AS UserId,
                uf.menu_id      AS MenuId,
                uf.dsp_seq      AS DisplaySeq,
                ISNULL(m.menu_name, '')  AS MenuName,
                ISNULL(m.menu_url,  '')  AS MenuUrl
            FROM SPC_USER_FAVORITE uf
            LEFT JOIN SPC_MENU m
                ON uf.div_seq = m.div_seq AND uf.menu_id = m.menu_id
            WHERE uf.div_seq = @DivSeq
              AND uf.user_id = @UserId
              AND uf.menu_id = @MenuId
              AND uf.use_yn  = 'Y'";

        return await _connection.QueryFirstOrDefaultAsync<FavoriteDto>(
            new CommandDefinition(sql, new { DivSeq = divSeq, UserId = userId, MenuId = menuId }, cancellationToken: cancellationToken));
    }

    public async Task<FavoriteResultDto> AddFavoriteAsync(
        string divSeq,
        string userId,
        CreateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        // Check existing (including soft-deleted)
        const string checkSql = @"
            SELECT use_yn, dsp_seq
            FROM SPC_USER_FAVORITE
            WHERE div_seq = @DivSeq AND user_id = @UserId AND menu_id = @MenuId";

        var existing = await _connection.QueryFirstOrDefaultAsync<(string UseYn, int DspSeq)>(
            new CommandDefinition(checkSql, new { DivSeq = divSeq, UserId = userId, MenuId = dto.MenuId }, cancellationToken: cancellationToken));

        if (existing != default && existing.UseYn == "Y")
        {
            return new FavoriteResultDto
            {
                Success = false,
                Message = "이미 즐겨찾기에 추가된 메뉴입니다.",
                MenuId = dto.MenuId
            };
        }

        var displaySeq = dto.DisplaySeq ?? await GetNextDisplaySeqAsync(divSeq, userId, cancellationToken);

        if (existing != default)
        {
            // Reactivate soft-deleted
            const string reactivateSql = @"
                UPDATE SPC_USER_FAVORITE
                SET use_yn         = 'Y',
                    dsp_seq        = @DspSeq,
                    update_user_id = @UserId,
                    update_date    = GETDATE()
                WHERE div_seq = @DivSeq AND user_id = @UserId AND menu_id = @MenuId";

            await _connection.ExecuteAsync(
                new CommandDefinition(reactivateSql, new { DivSeq = divSeq, UserId = userId, MenuId = dto.MenuId, DspSeq = displaySeq }, cancellationToken: cancellationToken));
        }
        else
        {
            const string insertSql = @"
                INSERT INTO SPC_USER_FAVORITE
                    (div_seq, user_id, menu_id, dsp_seq, use_yn, added_date, create_user_id, create_date)
                VALUES
                    (@DivSeq, @UserId, @MenuId, @DspSeq, 'Y', GETDATE(), @UserId, GETDATE())";

            await _connection.ExecuteAsync(
                new CommandDefinition(insertSql, new { DivSeq = divSeq, UserId = userId, MenuId = dto.MenuId, DspSeq = displaySeq }, cancellationToken: cancellationToken));
        }

        return new FavoriteResultDto
        {
            Success = true,
            Message = "즐겨찾기가 성공적으로 추가되었습니다.",
            MenuId = dto.MenuId
        };
    }

    public async Task<FavoriteResultDto> UpdateFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        UpdateFavoriteDto dto,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_FAVORITE
            SET dsp_seq        = @DspSeq,
                update_user_id = @UserId,
                update_date    = GETDATE()
            WHERE div_seq = @DivSeq
              AND user_id = @UserId
              AND menu_id = @MenuId
              AND use_yn  = 'Y'";

        var affected = await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { DivSeq = divSeq, UserId = userId, MenuId = menuId, DspSeq = dto.DisplaySeq }, cancellationToken: cancellationToken));

        if (affected == 0)
        {
            return new FavoriteResultDto
            {
                Success = false,
                Message = "즐겨찾기를 찾을 수 없습니다.",
                MenuId = menuId
            };
        }

        return new FavoriteResultDto
        {
            Success = true,
            Message = "즐겨찾기 순서가 성공적으로 변경되었습니다.",
            MenuId = menuId
        };
    }

    public async Task<FavoriteResultDto> RemoveFavoriteAsync(
        string divSeq,
        string userId,
        string menuId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_USER_FAVORITE
            SET use_yn         = 'N',
                update_user_id = @UserId,
                update_date    = GETDATE()
            WHERE div_seq = @DivSeq
              AND user_id = @UserId
              AND menu_id = @MenuId
              AND use_yn  = 'Y'";

        var affected = await _connection.ExecuteAsync(
            new CommandDefinition(sql, new { DivSeq = divSeq, UserId = userId, MenuId = menuId }, cancellationToken: cancellationToken));

        if (affected == 0)
        {
            return new FavoriteResultDto
            {
                Success = false,
                Message = "즐겨찾기를 찾을 수 없습니다.",
                MenuId = menuId
            };
        }

        return new FavoriteResultDto
        {
            Success = true,
            Message = "즐겨찾기가 성공적으로 삭제되었습니다.",
            MenuId = menuId
        };
    }

    private async Task<int> GetNextDisplaySeqAsync(string divSeq, string userId, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT ISNULL(MAX(dsp_seq), 0) + 1
            FROM SPC_USER_FAVORITE
            WHERE div_seq = @DivSeq AND user_id = @UserId AND use_yn = 'Y'";

        return await _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, new { DivSeq = divSeq, UserId = userId }, cancellationToken: cancellationToken));
    }
}
