using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Vendor repository interface for stored procedure operations.
/// </summary>
public interface IVendorRepository
{
    /// <summary>
    /// Gets vendor info. (USP_SPC_VENDOR_INFO_SELECT)
    /// </summary>
    Task<IEnumerable<VendorInfoDto>> GetVendorInfoAsync(VendorQueryDto query);

    /// <summary>
    /// Gets single vendor info by ID.
    /// </summary>
    Task<VendorInfoDto?> GetVendorByIdAsync(string divSeq, string vendorId);
}
