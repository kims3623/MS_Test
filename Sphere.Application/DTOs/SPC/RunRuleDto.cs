namespace Sphere.Application.DTOs.SPC;

#region Run Rule DTOs

/// <summary>
/// Run rules configuration response DTO.
/// </summary>
public class RunRulesConfigDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public List<RunRuleDto> Rules { get; set; } = new();
    public string LastUpdatedBy { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; }
}

/// <summary>
/// Run rule definition DTO.
/// </summary>
public class RunRuleDto
{
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public string RuleNameK { get; set; } = string.Empty;
    public string RuleNameE { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Priority { get; set; }
    public RunRuleParametersDto Parameters { get; set; } = new();
}

/// <summary>
/// Run rule parameters DTO.
/// </summary>
public class RunRuleParametersDto
{
    public int? ConsecutivePoints { get; set; }
    public int? OutOfNPoints { get; set; }
    public decimal? SigmaLevel { get; set; }
    public string? Side { get; set; }
    public string? Direction { get; set; }
}

/// <summary>
/// Run rule check request DTO.
/// </summary>
public class RunRuleCheckRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public List<string>? RuleIds { get; set; }
}

/// <summary>
/// Run rule check response DTO.
/// </summary>
public class RunRuleCheckResponseDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public int TotalViolations { get; set; }
    public List<RunRuleViolationDto> Violations { get; set; } = new();
    public List<RunRuleSummaryDto> RuleSummary { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Run rule violation DTO.
/// </summary>
public class RunRuleViolationDto
{
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public List<int> AffectedPoints { get; set; } = new();
}

/// <summary>
/// Run rule summary DTO.
/// </summary>
public class RunRuleSummaryDto
{
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public int ViolationCount { get; set; }
    public decimal ViolationRate { get; set; }
    public string LastViolationDate { get; set; } = string.Empty;
}

#endregion

#region Western Electric Rules

/// <summary>
/// Western Electric run rules constants.
/// Standard 8 rules for control chart analysis.
/// </summary>
public static class WesternElectricRules
{
    /// <summary>Rule 1: One point beyond 3 sigma</summary>
    public const string Rule1_Beyond3Sigma = "WE1";

    /// <summary>Rule 2: Two of three consecutive points beyond 2 sigma</summary>
    public const string Rule2_TwoOfThreeBeyond2Sigma = "WE2";

    /// <summary>Rule 3: Four of five consecutive points beyond 1 sigma</summary>
    public const string Rule3_FourOfFiveBeyond1Sigma = "WE3";

    /// <summary>Rule 4: Eight consecutive points on one side of center</summary>
    public const string Rule4_EightOneSide = "WE4";

    /// <summary>Rule 5: Six consecutive points increasing or decreasing</summary>
    public const string Rule5_SixTrend = "WE5";

    /// <summary>Rule 6: Fourteen consecutive points alternating</summary>
    public const string Rule6_FourteenAlternating = "WE6";

    /// <summary>Rule 7: Fifteen consecutive points within 1 sigma</summary>
    public const string Rule7_FifteenWithin1Sigma = "WE7";

    /// <summary>Rule 8: Eight consecutive points beyond 1 sigma on both sides</summary>
    public const string Rule8_EightBeyond1SigmaBothSides = "WE8";
}

#endregion
