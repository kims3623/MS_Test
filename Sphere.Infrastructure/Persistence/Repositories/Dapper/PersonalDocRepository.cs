using System.Data;
using Sphere.Application.DTOs.User;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Personal document repository implementation using Dapper for stored procedures.
/// </summary>
public class PersonalDocRepository : DapperRepositoryBase, IPersonalDocRepository
{
    public PersonalDocRepository(IDbConnection connection) : base(connection) { }

    /// <inheritdoc />
    public async Task<IEnumerable<PersonalDocMasterDto>> GetMasterListAsync()
    {
        return await QueryAsync<PersonalDocMasterDto>("USP_SPC_PERSONAL_DOC_MST_SELECT");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PersonalDocDetailDto>> GetDetailsAsync(string personalDocItem)
    {
        return await QueryAsync<PersonalDocDetailDto>(
            "USP_SPC_PERSONAL_DOC_DETAIL_SELECT",
            new { personal_doc_item = personalDocItem });
    }
}
