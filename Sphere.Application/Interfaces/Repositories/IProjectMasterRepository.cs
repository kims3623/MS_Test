using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

public interface IProjectMasterRepository
{
    Task<IEnumerable<ProjectMasterDto>> GetProjectMasterListAsync(
        string divSeq,
        ProjectMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default);
}
