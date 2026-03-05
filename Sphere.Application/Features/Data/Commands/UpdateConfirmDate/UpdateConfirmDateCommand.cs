using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.UpdateConfirmDate;

/// <summary>
/// Command to update confirm date.
/// </summary>
public record UpdateConfirmDateCommand : IRequest<Result<RawDataOperationResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string MtrlClassId { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string StatTypeId { get; init; } = string.Empty;
    public string ConfirmDate { get; init; } = string.Empty;
}
