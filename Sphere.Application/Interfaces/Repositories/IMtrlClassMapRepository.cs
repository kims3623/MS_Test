using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// MtrlClassMap repository interface for material class map tree operations.
/// </summary>
public interface IMtrlClassMapRepository
{
    /// <summary>
    /// Gets MtrlClassMap tree data (raw SQL join of SPC_MTRL_CLASS_MAP + SPC_CODE_MST).
    /// </summary>
    Task<IEnumerable<MtrlClassMapTreeDto>> GetTreeAsync(
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new class or subclass. (USP_SPC_MTRL_CLASS_MAP_INSERT)
    /// </summary>
    Task<MtrlClassMapResultDto> CreateAsync(
        string divSeq,
        CreateMtrlClassMapDto dto,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing class (useYn, etc.). (USP_SPC_MTRL_CLASS_MAP_UPDATE)
    /// </summary>
    Task<MtrlClassMapResultDto> UpdateAsync(
        string divSeq,
        UpdateMtrlClassMapDto dto,
        string userId,
        CancellationToken cancellationToken = default);
}
