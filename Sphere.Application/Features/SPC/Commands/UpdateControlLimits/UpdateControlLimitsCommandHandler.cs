using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Commands.UpdateControlLimits;

/// <summary>
/// Handler for UpdateControlLimitsCommand.
/// </summary>
public class UpdateControlLimitsCommandHandler : IRequestHandler<UpdateControlLimitsCommand, Result<bool>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<UpdateControlLimitsCommandHandler> _logger;

    public UpdateControlLimitsCommandHandler(
        ISPCRepository repository,
        ILogger<UpdateControlLimitsCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(
        UpdateControlLimitsCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating control limits for SpecSysId {SpecSysId} by User {UserId}",
            request.SpecSysId, request.UserId);

        try
        {
            var updateDto = new ControlLimitsUpdateDto
            {
                SpecSysId = request.SpecSysId,
                ChartType = request.ChartType,
                Ucl = request.Ucl,
                Cl = request.Cl,
                Lcl = request.Lcl,
                Usl = request.Usl,
                Lsl = request.Lsl,
                Target = request.Target,
                Reason = request.Reason
            };

            var success = await _repository.UpdateControlLimitsAsync(updateDto, request.UserId, cancellationToken);

            if (!success)
            {
                return Result<bool>.Failure("Failed to update control limits.");
            }

            _logger.LogInformation("Successfully updated control limits for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating control limits for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<bool>.Failure("Failed to update control limits.");
        }
    }
}
