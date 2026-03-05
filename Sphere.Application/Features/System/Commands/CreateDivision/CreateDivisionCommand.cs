using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateDivision;

/// <summary>
/// Command to create a new division.
/// </summary>
public record CreateDivisionCommand : IRequest<Result<CreateDivisionResponseDto>>
{
    public string DivCode { get; init; } = string.Empty;
    public string DivName { get; init; } = string.Empty;
    public string DivNameE { get; init; } = string.Empty;
    public string DivType { get; init; } = string.Empty;
    public string? ParentDivSeq { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? Fax { get; init; }
    public string? Email { get; init; }
    public int SortOrder { get; init; }
    public string CreateUserId { get; init; } = string.Empty;
}
