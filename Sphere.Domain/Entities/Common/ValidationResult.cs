using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Validation result entity for data validation results.
/// </summary>
public class ValidationResultEntity : SphereEntity
{
    public string IsValid { get; set; } = "Y";
    public string ErrorCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
}
