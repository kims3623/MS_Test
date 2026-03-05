using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.CreateVendorMaster;

public class CreateVendorMasterCommandHandler : IRequestHandler<CreateVendorMasterCommand, Result<VendorMasterResultDto>>
{
    private readonly IVendorMasterRepository _repository;
    private readonly ILogger<CreateVendorMasterCommandHandler> _logger;

    public CreateVendorMasterCommandHandler(
        IVendorMasterRepository repository,
        ILogger<CreateVendorMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<VendorMasterResultDto>> Handle(CreateVendorMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating vendor master: DivSeq {DivSeq}, VendorId {VendorId}", request.DivSeq, request.VendorId);

        try
        {
            var dto = new CreateVendorMasterDto
            {
                VendorId = request.VendorId,
                VendorName = request.VendorName,
                VendorType = request.VendorType,
                VendorCode = request.VendorCode,
                ContactPerson = request.ContactPerson,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Address = request.Address,
                Country = request.Country,
                UseYn = request.UseYn,
                Description = request.Description
            };

            var result = await _repository.CreateVendorMasterAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<VendorMasterResultDto>.Failure(result.Message);
            }

            return Result<VendorMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating vendor master: DivSeq {DivSeq}, VendorId {VendorId}", request.DivSeq, request.VendorId);
            return Result<VendorMasterResultDto>.Failure("Failed to create vendor master.");
        }
    }
}
