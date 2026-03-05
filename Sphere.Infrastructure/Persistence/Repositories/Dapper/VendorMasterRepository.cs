using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Dapper implementation of IVendorMasterRepository.
/// </summary>
public class VendorMasterRepository : DapperRepositoryBase, IVendorMasterRepository
{
    public VendorMasterRepository(IDbConnection connection) : base(connection)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_VENDOR_INFO_SELECT
    /// DB params: @P_Lang_Type VARCHAR(10), @P_div_seq VARCHAR(40), @P_vendor_id VARCHAR(40)
    /// </remarks>
    public async Task<IEnumerable<VendorMasterDto>> GetVendorMasterListAsync(
        string divSeq,
        VendorMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync<VendorMasterDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                vendor_id = filter?.VendorId
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_VENDOR_INFO_SELECT
    /// DB params: @P_Lang_Type VARCHAR(10), @P_div_seq VARCHAR(40), @P_vendor_id VARCHAR(40)
    /// </remarks>
    public async Task<VendorMasterDto?> GetVendorMasterByIdAsync(
        string divSeq,
        string vendorId,
        CancellationToken cancellationToken = default)
    {
        return await QueryFirstOrDefaultAsync<VendorMasterDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                vendor_id = vendorId
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_VENDOR_INFO_SAVE
    /// DB params: @P_div_seq, @P_vendor_id, @P_update_user_id, @P_vendor_biz_number,
    ///            @P_oner_name, @P_vendor_tellnumber, @P_oath_manager, @P_oath_manager_dept,
    ///            @P_oath_manager_phone, @P_vendor_address, @P_company_sign_filename,
    ///            @P_company_sign_content, @P_yield_calc_type
    /// </remarks>
    public async Task<VendorMasterResultDto> CreateVendorMasterAsync(
        string divSeq,
        CreateVendorMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var result = await QueryFirstOrDefaultAsync<VendorMasterResultDto>(
            "USP_SPC_VENDOR_INFO_SAVE",
            new
            {
                div_seq = divSeq,
                vendor_id = dto.VendorId,
                update_user_id = userId,
                vendor_biz_number = (string?)null,
                oner_name = dto.ContactPerson,
                vendor_tellnumber = dto.ContactPhone,
                oath_manager = (string?)null,
                oath_manager_dept = (string?)null,
                oath_manager_phone = (string?)null,
                vendor_address = dto.Address,
                company_sign_filename = (string?)null,
                company_sign_content = (string?)null,
                yield_calc_type = (string?)null
            });

        return result ?? new VendorMasterResultDto
        {
            Success = true,
            Message = "Vendor master created successfully.",
            VendorId = dto.VendorId
        };
    }
}
