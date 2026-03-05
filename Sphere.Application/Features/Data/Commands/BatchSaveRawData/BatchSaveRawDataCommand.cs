using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.BatchSaveRawData;

/// <summary>
/// Command to batch save raw data (insert, update, delete).
/// </summary>
public record BatchSaveRawDataCommand : IRequest<Result<BatchSaveResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public List<BatchRawDataRowDto> Rows { get; init; } = new();
}
