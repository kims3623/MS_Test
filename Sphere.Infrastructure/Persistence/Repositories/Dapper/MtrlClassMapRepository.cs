using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Dapper implementation of IMtrlClassMapRepository.
/// Uses raw SQL for SELECT (no SP exists) and stored procedures for INSERT/UPDATE.
/// </summary>
public class MtrlClassMapRepository : DapperRepositoryBase, IMtrlClassMapRepository
{
    private readonly ILogger<MtrlClassMapRepository> _logger;

    public MtrlClassMapRepository(
        IDbConnection connection,
        ILogger<MtrlClassMapRepository> logger) : base(connection)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    /// <remarks>
    /// No SP exists for SELECT. Uses raw SQL joining SPC_MTRL_CLASS_MAP + SPC_CODE_MST.
    /// Dapper MatchNamesWithUnderscores handles snake_case→PascalCase mapping.
    /// </remarks>
    public async Task<IEnumerable<MtrlClassMapTreeDto>> GetTreeAsync(
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                mcm.tree_id         AS TreeId,
                mcm.tree_parent_id  AS TreeParentId,
                ISNULL(cm.code_name_k, mcm.mtrl_class_id) AS MtrlClassName,
                mcm.mtrl_class_id   AS MtrlClassId,
                mcm.parent_class_id AS ParentClassId,
                mcm.use_yn          AS UseYn,
                mcm.map_level       AS MapLevel
            FROM SPC_MTRL_CLASS_MAP mcm
            LEFT JOIN SPC_CODE_MST cm
                ON mcm.div_seq = cm.div_seq
                AND mcm.mtrl_class_id = cm.code_id
                AND cm.code_class_id = 'MTRL_CLASS'
            WHERE mcm.div_seq = @DivSeq
            ORDER BY mcm.map_level, mcm.tree_id
        ";

        return await _connection.QueryAsync<MtrlClassMapTreeDto>(
            sql,
            new { DivSeq = divSeq },
            commandType: CommandType.Text);
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_MTRL_CLASS_MAP_INSERT
    /// </remarks>
    public async Task<MtrlClassMapResultDto> CreateAsync(
        string divSeq,
        CreateMtrlClassMapDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ExecuteAsync(
                "USP_SPC_MTRL_CLASS_MAP_INSERT",
                new
                {
                    div_seq = divSeq,
                    parent_tree_id = dto.ParentTreeId ?? "",
                    mtrl_class_id = dto.MtrlClassId,
                    class_type = dto.ClassType,
                    user_id = userId
                });

            _logger.LogInformation(
                "Created MtrlClassMap: DivSeq={DivSeq}, MtrlClassId={MtrlClassId}, ClassType={ClassType}",
                divSeq, dto.MtrlClassId, dto.ClassType);

            return new MtrlClassMapResultDto
            {
                Success = true,
                Message = "Created successfully."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to create MtrlClassMap: DivSeq={DivSeq}, MtrlClassId={MtrlClassId}",
                divSeq, dto.MtrlClassId);

            return new MtrlClassMapResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_MTRL_CLASS_MAP_UPDATE
    /// </remarks>
    public async Task<MtrlClassMapResultDto> UpdateAsync(
        string divSeq,
        UpdateMtrlClassMapDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ExecuteAsync(
                "USP_SPC_MTRL_CLASS_MAP_UPDATE",
                new
                {
                    div_seq = divSeq,
                    tree_id = dto.TreeId,
                    use_yn = dto.UseYn,
                    user_id = userId
                });

            _logger.LogInformation(
                "Updated MtrlClassMap: DivSeq={DivSeq}, TreeId={TreeId}, UseYn={UseYn}",
                divSeq, dto.TreeId, dto.UseYn);

            return new MtrlClassMapResultDto
            {
                Success = true,
                Message = "Updated successfully.",
                TreeId = dto.TreeId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to update MtrlClassMap: DivSeq={DivSeq}, TreeId={TreeId}",
                divSeq, dto.TreeId);

            return new MtrlClassMapResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}
