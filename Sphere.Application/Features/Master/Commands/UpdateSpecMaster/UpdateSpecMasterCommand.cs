using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.UpdateSpecMaster;

/// <summary>
/// Command to update an existing spec master.
/// </summary>
public record UpdateSpecMasterCommand : IRequest<Result<SpecMasterResultDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// User ID from claims.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Spec system ID (unique identifier).
    /// </summary>
    public string SpecSysId { get; init; } = string.Empty;

    /// <summary>
    /// Spec name.
    /// </summary>
    public string SpecName { get; init; } = string.Empty;

    /// <summary>
    /// Spec version.
    /// </summary>
    public string SpecVersion { get; init; } = string.Empty;

    /// <summary>
    /// Status.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Use Y/N flag.
    /// </summary>
    public string UseYn { get; init; } = "Y";

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; init; } = string.Empty;
}
