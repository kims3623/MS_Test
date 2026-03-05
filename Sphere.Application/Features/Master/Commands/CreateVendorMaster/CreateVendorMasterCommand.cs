using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.CreateVendorMaster;

public record CreateVendorMasterCommand : IRequest<Result<VendorMasterResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string VendorName { get; init; } = string.Empty;
    public string VendorType { get; init; } = string.Empty;
    public string VendorCode { get; init; } = string.Empty;
    public string ContactPerson { get; init; } = string.Empty;
    public string ContactEmail { get; init; } = string.Empty;
    public string ContactPhone { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string UseYn { get; init; } = "Y";
    public string Description { get; init; } = string.Empty;
}
