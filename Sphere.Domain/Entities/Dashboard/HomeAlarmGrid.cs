using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Dashboard;

/// <summary>
/// Home alarm grid entity for dashboard alarm list.
/// </summary>
public class HomeAlarmGrid : SphereEntity
{
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmType { get; set; } = string.Empty;
    public string AlmTypeName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public int Severity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? TriggeredDate { get; set; }
    public string AssignedUserName { get; set; } = string.Empty;
}
