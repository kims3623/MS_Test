using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetCodeMaster;

/// <summary>
/// Query to get code master list.
/// </summary>
public record GetCodeMasterQuery : IRequest<Result<CodeMasterListDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Code class ID filter.
    /// </summary>
    public string? CodeClassId { get; init; }

    /// <summary>
    /// Use Y/N filter.
    /// </summary>
    public string? UseYn { get; init; }

    /// <summary>
    /// Search text filter.
    /// </summary>
    public string? SearchText { get; init; }
}
