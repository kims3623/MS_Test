using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetVendorMaster;

/// <summary>
/// Query to get vendor master list.
/// </summary>
public record GetVendorMasterQuery : IRequest<Result<VendorMasterListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorType { get; init; }
    public string? UseYn { get; init; }
    public string? ApprovalStatus { get; init; }
    public string? SearchText { get; init; }
}
