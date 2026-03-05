using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Material master repository interface for stored procedure operations.
/// </summary>
public interface IMaterialMasterRepository
{
    /// <summary>
    /// Gets material master list. (USP_SPC_MTRL_MST_SELECT)
    /// </summary>
    Task<IEnumerable<MaterialMasterDto>> GetMaterialMasterListAsync(
        string divSeq,
        MaterialMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single material master by ID. (USP_SPC_MTRL_MST_SELECT)
    /// </summary>
    Task<MaterialMasterDto?> GetMaterialMasterByIdAsync(
        string divSeq,
        string mtrlId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new material master. (USP_SPC_MTRL_MST_INSERT)
    /// </summary>
    Task<MaterialMasterResultDto> CreateMaterialMasterAsync(
        string divSeq,
        CreateMaterialMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default);
}
