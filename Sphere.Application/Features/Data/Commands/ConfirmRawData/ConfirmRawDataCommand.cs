using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.ConfirmRawData;

/// <summary>
/// Command to confirm raw data.
/// </summary>
public record ConfirmRawDataCommand : IRequest<Result<RawDataOperationResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
}
