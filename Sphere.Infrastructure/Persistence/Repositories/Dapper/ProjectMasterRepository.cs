using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

public class ProjectMasterRepository : DapperRepositoryBase, IProjectMasterRepository
{
    public ProjectMasterRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<ProjectMasterDto>> GetProjectMasterListAsync(
        string divSeq, ProjectMasterFilterDto? filter = null, CancellationToken cancellationToken = default)
    {
        // TODO [P2]: DB에 USP_SPC_PROJECT_FILTER_SELECT 없음 (Project 테이블 자체 DB에 없음). EF Core로 전환 필요.
        await Task.CompletedTask;
        return new List<ProjectMasterDto>();
    }
}
