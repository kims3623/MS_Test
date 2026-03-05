using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Commands.UpdateCodeMaster;

/// <summary>
/// Command to update an existing code master.
/// </summary>
public record UpdateCodeMasterCommand : IRequest<Result<CodeMasterResultDto>>
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
    /// Code class ID (from route).
    /// </summary>
    public string CodeClassId { get; init; } = string.Empty;

    /// <summary>
    /// Code ID (from route).
    /// </summary>
    public string CodeId { get; init; } = string.Empty;

    /// <summary>
    /// Code alias.
    /// </summary>
    public string CodeAlias { get; init; } = string.Empty;

    /// <summary>
    /// Code name in Korean.
    /// </summary>
    public string CodeNameK { get; init; } = string.Empty;

    /// <summary>
    /// Code name in English.
    /// </summary>
    public string CodeNameE { get; init; } = string.Empty;

    /// <summary>
    /// Code name in Chinese.
    /// </summary>
    public string CodeNameC { get; init; } = string.Empty;

    /// <summary>
    /// Code name in Vietnamese.
    /// </summary>
    public string CodeNameV { get; init; } = string.Empty;

    /// <summary>
    /// Display sequence.
    /// </summary>
    public int DisplaySeq { get; init; }

    /// <summary>
    /// Code option.
    /// </summary>
    public string CodeOpt { get; init; } = string.Empty;

    /// <summary>
    /// Use Y/N flag.
    /// </summary>
    public string UseYn { get; init; } = "Y";

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; init; } = string.Empty;
}
