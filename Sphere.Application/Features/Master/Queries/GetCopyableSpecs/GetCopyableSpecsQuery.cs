using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetCopyableSpecs;

/// <summary>
/// Query to retrieve copyable specifications for spec copy popup.
/// Returns specifications that can be copied based on vendor and filter criteria.
/// </summary>
public record GetCopyableSpecsQuery : IRequest<Result<SpecMasterListDto>>
{
    /// <summary>
    /// Division sequence (partition key).
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Vendor ID to filter specifications.
    /// </summary>
    public string? VendorId { get; init; }

    /// <summary>
    /// Material class ID to filter specifications.
    /// </summary>
    public string? MtrlClassId { get; init; }

    /// <summary>
    /// Search text for specification name or ID.
    /// </summary>
    public string? SearchText { get; init; }

    /// <summary>
    /// Only return active specifications (UseYn = 'Y').
    /// </summary>
    public bool ActiveOnly { get; init; } = true;
}
