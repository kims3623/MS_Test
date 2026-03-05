using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// Home issue grid entity for dashboard issue grid.
/// </summary>
public class HomeIssueGrid : SphereEntity
{
    public string VendorType { get; set; } = string.Empty;
    public string VendorTypeName { get; set; } = string.Empty;
    public string StatTypeId { get; set; } = string.Empty;
    public string StatTypeName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassGroupId { get; set; } = string.Empty;
    public string MtrlClassGroupName { get; set; } = string.Empty;
    public int DiffDay { get; set; }
    public int Count { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string SeverityName { get; set; } = string.Empty;
}
