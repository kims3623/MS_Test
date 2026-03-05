using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Vendor repository implementation using Dapper for stored procedures.
/// </summary>
public class VendorRepository : DapperRepositoryBase, IVendorRepository
{
    public VendorRepository(IDbConnection connection) : base(connection) { }

    /// <inheritdoc />
    public async Task<IEnumerable<VendorInfoDto>> GetVendorInfoAsync(VendorQueryDto query)
    {
        return await QueryAsync<VendorInfoDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new
            {
                div_seq = query.DivSeq,
                vendor_id = query.VendorId
            });
    }

    /// <inheritdoc />
    public async Task<VendorInfoDto?> GetVendorByIdAsync(string divSeq, string vendorId)
    {
        return await QueryFirstOrDefaultAsync<VendorInfoDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new { div_seq = divSeq, vendor_id = vendorId });
    }
}
