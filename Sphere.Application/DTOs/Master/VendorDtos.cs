namespace Sphere.Application.DTOs.Master;

/// <summary>
/// Vendor info DTO.
/// </summary>
public class VendorInfoDto
{
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string VendorBizNumber { get; set; } = string.Empty;
    public string OnerName { get; set; } = string.Empty;
    public string VendorTellNumber { get; set; } = string.Empty;
    public string OathManager { get; set; } = string.Empty;
    public string OathManagerDept { get; set; } = string.Empty;
    public string OathManagerPhone { get; set; } = string.Empty;
    public string VendorAddress { get; set; } = string.Empty;
    public string CompanySignFilename { get; set; } = string.Empty;
    public byte[]? CompanySignContent { get; set; }
    public string YieldCalcType { get; set; } = string.Empty;
}

/// <summary>
/// Vendor query DTO.
/// </summary>
public class VendorQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? VendorId { get; set; }
}
