using System.Data;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

public class DefaultRuleRepository : DapperRepositoryBase, IDefaultRuleRepository
{
    public DefaultRuleRepository(IDbConnection connection) : base(connection) { }

    /// <remarks>
    /// DB USP: USP_SPC_DEFAULT_MNG_RULE_SELECT
    /// DB params: @P_Lang_Type, @P_div_seq, @P_stat_type_id(MAX), @P_vendor_id(MAX),
    ///            @P_mtrl_class_id(MAX), @P_step_id(MAX), @P_item_id(MAX), @P_measm_id(MAX), @P_use_yn
    /// </remarks>
    public async Task<IEnumerable<DefaultRuleDto>> GetDefaultRuleListAsync(
        string divSeq, DefaultRuleFilterDto? filter = null, CancellationToken cancellationToken = default)
    {
        return await QueryAsync<DefaultRuleDto>("USP_SPC_DEFAULT_MNG_RULE_SELECT", new
        {
            Lang_Type = "ko-KR",
            div_seq = divSeq,
            stat_type_id = (string?)null,
            vendor_id = (string?)null,
            mtrl_class_id = (string?)null,
            step_id = (string?)null,
            item_id = (string?)null,
            measm_id = (string?)null,
            use_yn = filter?.UseYn
        });
    }
}
