using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

public interface ISpecMasterRepository
{
    Task<IEnumerable<SpecMasterDto>> GetSpecMasterListAsync(
        string divSeq, SpecMasterFilterDto? filter = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<SpecDetailDto>> GetSpecDetailAsync(
        string divSeq, string specSysId, CancellationToken cancellationToken = default);

    Task<SpecMasterResultDto> CreateSpecMasterAsync(
        string divSeq, CreateSpecMasterDto dto, string userId, CancellationToken cancellationToken = default);

    Task<SpecMasterResultDto> UpdateSpecMasterAsync(
        string divSeq, string specSysId, UpdateSpecMasterDto dto, string userId, CancellationToken cancellationToken = default);
}
