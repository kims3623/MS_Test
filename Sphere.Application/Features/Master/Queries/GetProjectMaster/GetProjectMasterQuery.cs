using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetProjectMaster;

public record GetProjectMasterQuery : IRequest<Result<ProjectMasterListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? CustomerId { get; init; }
    public string? Status { get; init; }
    public string? UseYn { get; init; }
    public string? SearchText { get; init; }
}
