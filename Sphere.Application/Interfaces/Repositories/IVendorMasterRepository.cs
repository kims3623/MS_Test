using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Vendor master repository interface for stored procedure operations.
/// </summary>
public interface IVendorMasterRepository
{
    /// <summary>
    /// Gets vendor master list. (USP_SPC_VENDOR_INFO_SELECT)
    /// </summary>
    Task<IEnumerable<VendorMasterDto>> GetVendorMasterListAsync(
        string divSeq,
        VendorMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single vendor master by ID. (USP_SPC_VENDOR_INFO_SELECT)
    /// </summary>
    Task<VendorMasterDto?> GetVendorMasterByIdAsync(
        string divSeq,
        string vendorId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new vendor master. (USP_SPC_VENDOR_INFO_SAVE)
    /// </summary>
    Task<VendorMasterResultDto> CreateVendorMasterAsync(
        string divSeq,
        CreateVendorMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default);
}
