using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.DeleteCodeMaster;

/// <summary>
/// Command to delete a code master.
/// </summary>
public record DeleteCodeMasterCommand : IRequest<Result<CodeMasterResultDto>>
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
    /// Code class ID.
    /// </summary>
    public string CodeClassId { get; init; } = string.Empty;

    /// <summary>
    /// Code ID.
    /// </summary>
    public string CodeId { get; init; } = string.Empty;
}
