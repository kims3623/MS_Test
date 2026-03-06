using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// 프로젝트 마스터 리포지토리 - Dapper SP 구현체
/// SP: USP_SPC_PROJECT_MST_SELECT
/// 테이블: SPC_PROJECT
/// </summary>
public class ProjectMasterRepository : DapperRepositoryBase, IProjectMasterRepository
{
    public ProjectMasterRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<ProjectMasterDto>> GetProjectMasterListAsync(
        string divSeq,
        ProjectMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        filter ??= new ProjectMasterFilterDto();

        // USP_SPC_PROJECT_MST_SELECT: @P_div_seq, @P_use_yn, @P_search_text
        return await QueryAsync<ProjectMasterDto>(
            "USP_SPC_PROJECT_MST_SELECT",
            new
            {
                div_seq     = divSeq,
                use_yn      = filter.UseYn ?? string.Empty,
                search_text = filter.SearchText ?? string.Empty
            });
    }
}
