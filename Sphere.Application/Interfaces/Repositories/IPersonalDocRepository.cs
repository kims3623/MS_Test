using Sphere.Application.DTOs.User;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Personal document repository interface for stored procedure operations.
/// </summary>
public interface IPersonalDocRepository
{
    /// <summary>
    /// Gets personal document master list. (USP_SPC_PERSONAL_DOC_MST_SELECT)
    /// </summary>
    Task<IEnumerable<PersonalDocMasterDto>> GetMasterListAsync();

    /// <summary>
    /// Gets personal document details. (USP_SPC_PERSONAL_DOC_DETAIL_SELECT)
    /// </summary>
    Task<IEnumerable<PersonalDocDetailDto>> GetDetailsAsync(string personalDocItem);
}
