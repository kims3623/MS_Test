using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.CreateSpecMaster;

/// <summary>
/// Command to create a new spec master.
/// </summary>
public record CreateSpecMasterCommand : IRequest<Result<SpecMasterResultDto>>
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
    /// Spec ID.
    /// </summary>
    public string SpecId { get; init; } = string.Empty;

    /// <summary>
    /// Spec name.
    /// </summary>
    public string SpecName { get; init; } = string.Empty;

    /// <summary>
    /// Spec version.
    /// </summary>
    public string SpecVersion { get; init; } = string.Empty;

    /// <summary>
    /// Vendor ID.
    /// </summary>
    public string VendorId { get; init; } = string.Empty;

    /// <summary>
    /// Material class ID.
    /// </summary>
    public string MtrlClassId { get; init; } = string.Empty;

    /// <summary>
    /// Use Y/N flag.
    /// </summary>
    public string UseYn { get; init; } = "Y";

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; init; } = string.Empty;
}
