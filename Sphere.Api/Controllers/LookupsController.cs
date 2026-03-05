using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.Common.Constants;
using Sphere.Application.DTOs.Lookup;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Api.Controllers;

/// <summary>
/// Lookups controller for code master lookup APIs.
/// Provides 19 endpoints for various lookup data retrieval.
/// </summary>
[ApiController]
[Route("api/v1/lookups")]
[Authorize]
[Produces("application/json")]
public class LookupsController : ControllerBase
{
    private readonly ICodeMasterRepository _repository;
    private readonly ILogger<LookupsController> _logger;

    public LookupsController(
        ICodeMasterRepository repository,
        ILogger<LookupsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Gets the division sequence from the current user's claims.
    /// </summary>
    private string GetDivSeq() => User.FindFirst("div_seq")?.Value ?? "001";

    /// <summary>
    /// Gets the language from the Accept-Language header.
    /// </summary>
    private string GetLanguage() => SupportedLocales.Resolve(Request.Headers["Accept-Language"].FirstOrDefault());

    #region Common Lookup (1 endpoint)

    /// <summary>
    /// Gets lookup data by code class ID.
    /// </summary>
    /// <param name="codeClassId">Code class ID (e.g., ACT_PROD, STEP, ITEM).</param>
    /// <param name="lang">Language code (ko-KR, en-US, zh-CN, vi-VN). Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data. Defaults to true.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of lookup data for the specified code class.</returns>
    [HttpGet("{codeClassId}")]
    [ProducesResponseType(typeof(IEnumerable<CodeMasterLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetLookupByClassId(
        string codeClassId,
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var divSeq = GetDivSeq();
        var language = lang ?? GetLanguage();

        _logger.LogDebug("Getting lookup data for class {CodeClassId}, divSeq: {DivSeq}, lang: {Language}",
            codeClassId, divSeq, language);

        var result = await _repository.GetLookupByClassIdAsync(divSeq, codeClassId, language, activeOnly, cancellationToken);
        return Ok(result);
    }

    #endregion

    #region Type-specific Lookups (18 endpoints)

    /// <summary>
    /// Gets product type lookup data (ACT_PROD).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of product types.</returns>
    [HttpGet("act-prods")]
    [ProducesResponseType(typeof(IEnumerable<ActProdLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetActProds(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetActProdsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets process step lookup data (STEP).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of process steps.</returns>
    [HttpGet("steps")]
    [ProducesResponseType(typeof(IEnumerable<StepLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSteps(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetStepsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets measurement item lookup data (ITEM).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of measurement items.</returns>
    [HttpGet("items")]
    [ProducesResponseType(typeof(IEnumerable<ItemLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetItems(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetItemsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets measurement method lookup data (MEASM).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of measurement methods.</returns>
    [HttpGet("measms")]
    [ProducesResponseType(typeof(IEnumerable<MeasmLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMeasms(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetMeasmsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets equipment lookup data (EQP).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of equipment.</returns>
    [HttpGet("eqps")]
    [ProducesResponseType(typeof(IEnumerable<EqpLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetEqps(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetEqpsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets line lookup data (LINE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of lines.</returns>
    [HttpGet("lines")]
    [ProducesResponseType(typeof(IEnumerable<LineLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetLines(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetLinesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets shift lookup data (SHIFT).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of shifts.</returns>
    [HttpGet("shifts")]
    [ProducesResponseType(typeof(IEnumerable<ShiftLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetShifts(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetShiftsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets stage lookup data (STAGE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of stages.</returns>
    [HttpGet("stages")]
    [ProducesResponseType(typeof(IEnumerable<StageLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetStages(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetStagesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets frequency lookup data (FREQUENCY).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of frequencies.</returns>
    [HttpGet("frequencies")]
    [ProducesResponseType(typeof(IEnumerable<FrequencyLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFrequencies(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetFrequenciesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets customer lookup data (VENDOR).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of customers.</returns>
    [HttpGet("custs")]
    [ProducesResponseType(typeof(IEnumerable<CustLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCusts(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetCustsAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets end user lookup data (END_USER).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of end users.</returns>
    [HttpGet("end-users")]
    [ProducesResponseType(typeof(IEnumerable<EndUserLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetEndUsers(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetEndUsersAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets run rule lookup data (RUNRULE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of run rules.</returns>
    [HttpGet("runrules")]
    [ProducesResponseType(typeof(IEnumerable<RunRuleLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetRunRules(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetRunRulesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets menu lookup data (MENU).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of menus.</returns>
    [HttpGet("menus")]
    [ProducesResponseType(typeof(IEnumerable<MenuLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMenus(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetMenusAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets chart type lookup data (PIC_TYPE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of chart types.</returns>
    [HttpGet("pic-types")]
    [ProducesResponseType(typeof(IEnumerable<PicTypeLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPicTypes(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetPicTypesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets statistics type lookup data (STAT_TYPE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of statistics types.</returns>
    [HttpGet("stat-types")]
    [ProducesResponseType(typeof(IEnumerable<StatTypeLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetStatTypes(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetStatTypesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets control line type lookup data (CNTLN_TYPE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of control line types.</returns>
    [HttpGet("cntln-types")]
    [ProducesResponseType(typeof(IEnumerable<CntlnTypeLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCntlnTypes(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetCntlnTypesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets specification type lookup data (SPEC_TYPE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of specification types.</returns>
    [HttpGet("spec-types")]
    [ProducesResponseType(typeof(IEnumerable<SpecTypeLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSpecTypes(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetSpecTypesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets data type lookup data (DATA_TYPE).
    /// </summary>
    /// <param name="lang">Language code. Defaults to Accept-Language header.</param>
    /// <param name="activeOnly">If true, returns only active data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of data types.</returns>
    [HttpGet("data-types")]
    [ProducesResponseType(typeof(IEnumerable<DataTypeLookupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDataTypes(
        [FromQuery] string? lang = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetDataTypesAsync(GetDivSeq(), lang ?? GetLanguage(), activeOnly, cancellationToken);
        return Ok(result);
    }

    #endregion
}
