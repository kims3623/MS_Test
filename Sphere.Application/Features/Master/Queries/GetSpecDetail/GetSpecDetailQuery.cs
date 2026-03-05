using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetSpecDetail;

/// <summary>
/// Query to get spec detail by spec system ID.
/// </summary>
public record GetSpecDetailQuery : IRequest<Result<SpecDetailListDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Spec system ID.
    /// </summary>
    public string SpecSysId { get; init; } = string.Empty;
}

/// <summary>
/// DTO for spec detail list response.
/// </summary>
public class SpecDetailListDto
{
    public List<SpecDetailDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}
