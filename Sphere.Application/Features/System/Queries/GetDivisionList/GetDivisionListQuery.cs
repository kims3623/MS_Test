using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetDivisionList;

/// <summary>
/// Query to get division list with filter and pagination.
/// </summary>
public record GetDivisionListQuery : IRequest<Result<DivisionListResponseDto>>
{
    public string? DivSeq { get; init; }
    public string? DivName { get; init; }
    public string? IsActive { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
