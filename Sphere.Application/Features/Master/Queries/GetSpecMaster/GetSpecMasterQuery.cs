using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetSpecMaster;

public record GetSpecMasterQuery : IRequest<Result<SpecMasterListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string? Status { get; init; }
    public string? UseYn { get; init; }
    public string? SearchText { get; init; }
}
