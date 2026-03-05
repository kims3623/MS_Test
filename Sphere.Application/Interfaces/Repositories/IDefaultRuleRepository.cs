using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

public interface IDefaultRuleRepository
{
    Task<IEnumerable<DefaultRuleDto>> GetDefaultRuleListAsync(
        string divSeq, DefaultRuleFilterDto? filter = null, CancellationToken cancellationToken = default);
}
