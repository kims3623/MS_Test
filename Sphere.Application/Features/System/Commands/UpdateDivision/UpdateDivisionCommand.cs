using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateDivision;

/// <summary>
/// Command to update an existing division.
/// </summary>
public record UpdateDivisionCommand : IRequest<Result<UpdateDivisionResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? DivCode { get; init; }
    public string? DivName { get; init; }
    public string? DivNameE { get; init; }
    public string? DivType { get; init; }
    public string? ParentDivSeq { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? Fax { get; init; }
    public string? Email { get; init; }
    public int? SortOrder { get; init; }
    public string? IsActive { get; init; }
    public string UpdateUserId { get; init; } = string.Empty;
}
