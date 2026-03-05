using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.DeleteMenu;

/// <summary>
/// Command for deleting a menu.
/// </summary>
public class DeleteMenuCommand : IRequest<Result<DeleteMenuResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}
