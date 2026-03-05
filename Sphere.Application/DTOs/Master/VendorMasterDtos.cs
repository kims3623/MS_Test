namespace Sphere.Application.DTOs.Master;

#region Vendor Master DTOs

/// <summary>
/// Vendor master DTO (matches VendorMstRec entity).
/// </summary>
public class VendorMasterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string VendorType { get; set; } = string.Empty;
    public string VendorTypeName { get; set; } = string.Empty;
    public string VendorCode { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string ApprovalStatus { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreateUser { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public string UpdateUser { get; set; } = string.Empty;
    public DateTime? UpdateDate { get; set; }
}

/// <summary>
/// Vendor master list response DTO.
/// </summary>
public class VendorMasterListDto
{
    public List<VendorMasterDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Vendor master filter DTO.
/// </summary>
public class VendorMasterFilterDto
{
    public string? VendorId { get; set; }
    public string? VendorType { get; set; }
    public string? UseYn { get; set; }
    public string? ApprovalStatus { get; set; }
    public string? SearchText { get; set; }
}

/// <summary>
/// Create vendor master request DTO.
/// </summary>
public class CreateVendorMasterDto
{
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string VendorType { get; set; } = string.Empty;
    public string VendorCode { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Vendor master operation result DTO.
/// </summary>
public class VendorMasterResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? VendorId { get; set; }
}

#endregion
