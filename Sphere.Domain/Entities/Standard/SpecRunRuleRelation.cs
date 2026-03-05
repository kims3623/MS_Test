using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Specification run rule relation entity for mapping specs to run rules.
/// Maps to SPC_SPEC_RUN_RULE_REL table.
/// </summary>
public class SpecRunRuleRelation : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Run rule identifier (PK)
    /// </summary>
    public string RunRuleId { get; set; } = string.Empty;

    /// <summary>
    /// Run rule name
    /// </summary>
    public string RunRuleName { get; set; } = string.Empty;

    /// <summary>
    /// Active flag for this rule on this spec
    /// </summary>
    public string ActiveYn { get; set; } = "Y";

    /// <summary>
    /// Priority sequence
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Action to take when rule triggers
    /// </summary>
    public string TriggerAction { get; set; } = string.Empty;

    /// <summary>
    /// Notification flag
    /// </summary>
    public string NotifyYn { get; set; } = "Y";
}
