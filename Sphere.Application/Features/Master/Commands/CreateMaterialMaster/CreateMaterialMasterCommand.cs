using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.CreateMaterialMaster;

public record CreateMaterialMasterCommand : IRequest<Result<MaterialMasterResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string MtrlId { get; init; } = string.Empty;
    public string MtrlName { get; init; } = string.Empty;
    public string MtrlClassId { get; init; } = string.Empty;
    public string MtrlClassGroupId { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string Unit { get; init; } = string.Empty;
    public string SpecId { get; init; } = string.Empty;
    public string UseYn { get; init; } = "Y";
    public string Description { get; init; } = string.Empty;
}
