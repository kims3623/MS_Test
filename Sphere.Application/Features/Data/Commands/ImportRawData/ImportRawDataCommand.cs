using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Features.Data.Commands.ImportRawData;

/// <summary>
/// Command to import raw data from external source.
/// </summary>
public record ImportRawDataCommand : IRequest<Result<ImportRawDataResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public List<ImportRawDataRowDto> Rows { get; init; } = new();
}
