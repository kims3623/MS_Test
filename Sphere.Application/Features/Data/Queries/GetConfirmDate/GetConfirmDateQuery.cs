using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Queries.GetConfirmDate;

/// <summary>
/// Query to get confirm date list.
/// SP USP_SPC_CONFIRM_DATE_SELECT expects: @P_div_seq, @P_mtrl_class_id, @P_vendor_id, @P_stat_type_id
/// </summary>
public record GetConfirmDateQuery : IRequest<Result<ConfirmDateListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? MtrlClassId { get; init; }
    public string? VendorId { get; init; }
    public string? StatTypeId { get; init; }
}
