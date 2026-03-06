using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// 협력사 리포지토리 - Dapper SP 구현체
/// SP: USP_SPC_VENDOR_INFO_SELECT
/// 테이블: SPC_VENDOR_INFO
/// </summary>
public class VendorRepository : DapperRepositoryBase, IVendorRepository
{
    public VendorRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<VendorInfoDto>> GetVendorInfoAsync(VendorQueryDto query)
    {
        // USP_SPC_VENDOR_INFO_SELECT: @P_div_seq, @P_vendor_id
        return await QueryAsync<VendorInfoDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new
            {
                div_seq   = query.DivSeq,
                vendor_id = query.VendorId ?? string.Empty
            });
    }

    public async Task<VendorInfoDto?> GetVendorByIdAsync(string divSeq, string vendorId)
    {
        return await QueryFirstOrDefaultAsync<VendorInfoDto>(
            "USP_SPC_VENDOR_INFO_SELECT",
            new
            {
                div_seq   = divSeq,
                vendor_id = vendorId
            });
    }
}
