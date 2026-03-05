using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Validation rule entity for data validation rules.
/// </summary>
public class ValidationRule : SphereEntity
{
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string RuleType { get; set; } = string.Empty;
    public string RuleExpression { get; set; } = string.Empty;
    public string ErrorMessageK { get; set; } = string.Empty;
    public string ErrorMessageE { get; set; } = string.Empty;
    public int Priority { get; set; }
}
