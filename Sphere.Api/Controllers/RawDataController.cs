using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Features.Data.Queries.GetRawDataList;
using Sphere.Application.Features.Data.Queries.GetTempRawData;
using Sphere.Application.Features.Data.Queries.GetConfirmDate;
using Sphere.Application.Features.Data.Queries.GetRawDataByGroup;
using Sphere.Application.Features.Data.Commands.CreateRawData;
using Sphere.Application.Features.Data.Commands.UpdateRawData;
using Sphere.Application.Features.Data.Commands.DeleteRawData;
using Sphere.Application.Features.Data.Commands.SaveTempRawData;
using Sphere.Application.Features.Data.Commands.ConfirmRawData;
using Sphere.Application.Features.Data.Commands.ImportRawData;
using Sphere.Application.Features.Data.Commands.ValidateRawData;
using Sphere.Application.Features.Data.Commands.UpdateConfirmDate;
using Sphere.Application.Features.Data.Commands.BatchSaveRawData;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Raw data controller for data entry management.
/// </summary>
[ApiController]
[Route("api/v1/rawdata")]
[Authorize]
[Produces("application/json")]
public class RawDataController : ControllerBase
{
    private readonly ISender _mediator;

    public RawDataController(ISender mediator) => _mediator = mediator;

    private string DivSeq => User.FindFirstValue("div_seq") ?? "OPT001";
    private string UserId => User.FindFirstValue("user_id") ?? string.Empty;

    #region Basic CRUD

    /// <summary>
    /// Gets raw data list with filtering and pagination.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(RawDataListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRawDataList(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? projectId = null,
        [FromQuery] string? actProdId = null,
        [FromQuery] string? stepId = null,
        [FromQuery] string? itemId = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] string? specSysId = null,
        [FromQuery] string? approvalYn = null,
        [FromQuery] string? searchText = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        var result = await _mediator.Send(new GetRawDataListQuery
        {
            DivSeq = DivSeq,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            ProjectId = projectId,
            ActProdId = actProdId,
            StepId = stepId,
            ItemId = itemId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            SpecSysId = specSysId,
            ApprovalYn = approvalYn,
            SearchText = searchText,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Creates a new raw data entry.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRawData([FromBody] CreateRawDataRequest request)
    {
        var result = await _mediator.Send(new CreateRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            WorkDate = request.WorkDate,
            Shift = request.Shift,
            LotName = request.LotName,
            Frequency = request.Frequency,
            RawDataValue = request.RawDataValue,
            InputQty = request.InputQty,
            DefectQty = request.DefectQty
        });

        return result.Succeeded
            ? CreatedAtAction(nameof(GetRawDataList), result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Updates an existing raw data entry.
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRawData(long id, [FromBody] UpdateRawDataRequest request)
    {
        var result = await _mediator.Send(new UpdateRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            RawDataId = id,
            SpecSysId = request.SpecSysId,
            WorkDate = request.WorkDate,
            Shift = request.Shift,
            LotName = request.LotName,
            RawDataValue = request.RawDataValue,
            InputQty = request.InputQty,
            DefectQty = request.DefectQty
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Deletes a raw data entry.
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRawData(
        long id,
        [FromQuery] string specSysId,
        [FromQuery] string workDate,
        [FromQuery] string shift = "")
    {
        var result = await _mediator.Send(new DeleteRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            RawDataId = id,
            SpecSysId = specSysId,
            WorkDate = workDate,
            Shift = shift
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Temp Raw Data

    /// <summary>
    /// Gets temporary raw data list.
    /// </summary>
    [HttpGet("temp")]
    [ProducesResponseType(typeof(TempRawDataListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTempRawData(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? specSysId = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null)
    {
        var result = await _mediator.Send(new GetTempRawDataQuery
        {
            DivSeq = DivSeq,
            VendorId = vendorId,
            SpecSysId = specSysId,
            StartDate = startDate,
            EndDate = endDate
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Saves temporary raw data (draft).
    /// </summary>
    [HttpPost("temp")]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveTempRawData([FromBody] SaveTempRawDataRequest request)
    {
        var result = await _mediator.Send(new SaveTempRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            WorkDate = request.WorkDate,
            Shift = request.Shift,
            LotName = request.LotName,
            RawDataValue = request.RawDataValue,
            InputQty = request.InputQty,
            DefectQty = request.DefectQty
        });

        return result.Succeeded
            ? CreatedAtAction(nameof(GetTempRawData), result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Confirm

    /// <summary>
    /// Confirms raw data (finalize).
    /// </summary>
    [HttpPost("confirm")]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmRawData([FromBody] ConfirmRawDataRequest request)
    {
        var result = await _mediator.Send(new ConfirmRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            WorkDate = request.WorkDate
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Gets confirm date list.
    /// </summary>
    [HttpGet("confirm-dates")]
    [ProducesResponseType(typeof(ConfirmDateListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetConfirmDates(
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] string? statTypeId = null)
    {
        var result = await _mediator.Send(new GetConfirmDateQuery
        {
            DivSeq = DivSeq,
            MtrlClassId = mtrlClassId,
            VendorId = vendorId,
            StatTypeId = statTypeId
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Updates confirm date.
    /// </summary>
    [HttpPut("confirm-dates")]
    [ProducesResponseType(typeof(RawDataOperationResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateConfirmDate([FromBody] UpdateConfirmDateRequest request)
    {
        var result = await _mediator.Send(new UpdateConfirmDateCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            MtrlClassId = request.MtrlClassId,
            VendorId = request.VendorId,
            StatTypeId = request.StatTypeId,
            ConfirmDate = request.ConfirmDate
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Group

    /// <summary>
    /// Gets raw data by group.
    /// </summary>
    [HttpGet("groups/{groupSpecSysId}")]
    [ProducesResponseType(typeof(RawDataGroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRawDataByGroup(string groupSpecSysId)
    {
        var result = await _mediator.Send(new GetRawDataByGroupQuery
        {
            DivSeq = DivSeq,
            GroupSpecSysId = groupSpecSysId
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Import/Export

    /// <summary>
    /// Imports raw data from external source (Excel/CSV).
    /// </summary>
    [HttpPost("import")]
    [ProducesResponseType(typeof(ImportRawDataResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportRawData([FromBody] ImportRawDataRequest request)
    {
        var result = await _mediator.Send(new ImportRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            Rows = request.Rows
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Exports raw data to external format (Excel/CSV).
    /// </summary>
    [HttpGet("export")]
    [ProducesResponseType(typeof(RawDataListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExportRawData(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? specSysId = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null)
    {
        var result = await _mediator.Send(new GetRawDataListQuery
        {
            DivSeq = DivSeq,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            SpecSysId = specSysId,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = 1,
            PageSize = 10000
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Validation

    /// <summary>
    /// Validates raw data against spec rules.
    /// </summary>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateRawData([FromBody] ValidateRawDataRequest request)
    {
        var result = await _mediator.Send(new ValidateRawDataCommand
        {
            DivSeq = DivSeq,
            SpecSysId = request.SpecSysId,
            WorkDate = request.WorkDate,
            RawDataValue = request.RawDataValue,
            InputQty = request.InputQty,
            DefectQty = request.DefectQty
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion

    #region Batch

    /// <summary>
    /// Batch saves raw data (insert, update, delete in single transaction).
    /// </summary>
    [HttpPost("batch")]
    [ProducesResponseType(typeof(BatchSaveResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BatchSaveRawData([FromBody] BatchSaveRawDataRequest request)
    {
        var result = await _mediator.Send(new BatchSaveRawDataCommand
        {
            DivSeq = DivSeq,
            UserId = UserId,
            SpecSysId = request.SpecSysId,
            Rows = request.Rows
        });

        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    #endregion
}

#region Request DTOs

public record CreateRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public string Shift { get; init; } = string.Empty;
    public string LotName { get; init; } = string.Empty;
    public string? Frequency { get; init; }
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}

public record UpdateRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public string Shift { get; init; } = string.Empty;
    public string LotName { get; init; } = string.Empty;
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}

public record SaveTempRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public string Shift { get; init; } = string.Empty;
    public string LotName { get; init; } = string.Empty;
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}

public record ConfirmRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
}

public record UpdateConfirmDateRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string MtrlClassId { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string StatTypeId { get; init; } = string.Empty;
    public string ConfirmDate { get; init; } = string.Empty;
}

public record ImportRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public List<ImportRawDataRowDto> Rows { get; init; } = new();
}

public record ValidateRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string WorkDate { get; init; } = string.Empty;
    public decimal? RawDataValue { get; init; }
    public int InputQty { get; init; }
    public int DefectQty { get; init; }
}

public record BatchSaveRawDataRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public List<BatchRawDataRowDto> Rows { get; init; } = new();
}

#endregion
