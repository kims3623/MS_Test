using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm rule entity for defining alarm trigger rules.
/// </summary>
public class AlarmRule : SphereEntity
{
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public string RuleType { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public decimal? ThresholdValue { get; set; }
    public string Operator { get; set; } = string.Empty;
    public int Severity { get; set; }
    public string NotifyUsers { get; set; } = string.Empty;
    public string NotifyType { get; set; } = "EMAIL";
    public string Description { get; set; } = string.Empty;
}
