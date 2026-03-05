using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetMaterialMaster;

/// <summary>
/// Query to get material master list.
/// </summary>
public record GetMaterialMasterQuery : IRequest<Result<MaterialMasterListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? MtrlClassId { get; init; }
    public string? VendorId { get; init; }
    public string? UseYn { get; init; }
}
