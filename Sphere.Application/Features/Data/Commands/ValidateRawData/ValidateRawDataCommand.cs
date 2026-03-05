using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.ValidateRawData;

/// <summary>
/// Command to validate raw data.
/// </summary>
public record ValidateRawDataCommand : IRequest<Result<ValidationResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}
