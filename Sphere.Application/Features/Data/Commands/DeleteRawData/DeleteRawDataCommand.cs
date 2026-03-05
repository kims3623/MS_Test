using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.DeleteRawData;

/// <summary>
/// Command to delete raw data.
/// </summary>
public record DeleteRawDataCommand : IRequest<Result<RawDataOperationResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public long RawDataId { get; init; }
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public string Shift { get; init; } = string.Empty;
}
