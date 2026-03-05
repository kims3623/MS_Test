using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Default management rule entity for managing default business rules.
/// Maps to SPC_DEFAULT_MGMT_RULE table.
/// </summary>
public class DefaultManagementRule : SphereEntity
{
    /// <summary>
    /// Rule identifier (PK)
    /// </summary>
    public string RuleId { get; set; } = string.Empty;

    /// <summary>
    /// Rule type
    /// </summary>
    public string RuleType { get; set; } = string.Empty;

    /// <summary>
    /// Rule name
    /// </summary>
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// Rule name in Korean
    /// </summary>
    public string RuleNameK { get; set; } = string.Empty;

    /// <summary>
    /// Rule name in English
    /// </summary>
    public string RuleNameE { get; set; } = string.Empty;

    /// <summary>
    /// Target entity type
    /// </summary>
    public string TargetEntity { get; set; } = string.Empty;

    /// <summary>
    /// Condition expression
    /// </summary>
    public string Condition { get; set; } = string.Empty;

    /// <summary>
    /// Action to take when triggered
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Priority level
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Effective from date
    /// </summary>
    public DateTime? EffectiveFrom { get; set; }

    /// <summary>
    /// Effective to date
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
