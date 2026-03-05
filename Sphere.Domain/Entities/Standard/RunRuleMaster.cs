using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Run rule master entity for SPC control chart rules.
/// Maps to SPC_RUN_RULE_MST table.
/// </summary>
public class RunRuleMaster : SphereEntity
{
    /// <summary>
    /// Run rule identifier (PK)
    /// </summary>
    public string RunRuleId { get; set; } = string.Empty;

    /// <summary>
    /// Run rule name
    /// </summary>
    public string RunRuleName { get; set; } = string.Empty;

    /// <summary>
    /// Run rule name in Korean
    /// </summary>
    public string RunRuleNameK { get; set; } = string.Empty;

    /// <summary>
    /// Run rule name in English
    /// </summary>
    public string RunRuleNameE { get; set; } = string.Empty;

    /// <summary>
    /// Run rule type (WESTERN_ELECTRIC, NELSON, etc.)
    /// </summary>
    public string RunRuleType { get; set; } = string.Empty;

    /// <summary>
    /// Rule number (1-8 for Western Electric)
    /// </summary>
    public int RuleNo { get; set; }

    /// <summary>
    /// Points required to trigger
    /// </summary>
    public int PointsRequired { get; set; }

    /// <summary>
    /// Zone affected (A, B, C, or combination)
    /// </summary>
    public string Zone { get; set; } = string.Empty;

    /// <summary>
    /// Rule condition description
    /// </summary>
    public string Condition { get; set; } = string.Empty;

    /// <summary>
    /// Severity level (1=Low, 2=Medium, 3=High)
    /// </summary>
    public int Severity { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
