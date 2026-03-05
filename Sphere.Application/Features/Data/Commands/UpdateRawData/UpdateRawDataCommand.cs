using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.UpdateRawData;

/// <summary>
/// Command to update raw data.
/// </summary>
public record UpdateRawDataCommand : IRequest<Result<RawDataOperationResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public long RawDataId { get; init; }
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public string Shift { get; init; } = string.Empty;
    public string LotName { get; init; } = string.Empty;
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}
