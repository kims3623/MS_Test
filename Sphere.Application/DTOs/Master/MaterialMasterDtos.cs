namespace Sphere.Application.DTOs.Master;

#region Material Master DTOs

/// <summary>
/// Material master DTO (matches USP_SPC_MTRL_MST_SELECT output columns).
/// </summary>
public class MaterialMasterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string ActProdName { get; set; } = string.Empty;
    public string EndUserId { get; set; } = string.Empty;
    public string EndUserName { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string UnitBizYn { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
    public string CustIds { get; set; } = string.Empty;
    public string CustNames { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
    public DateTime? UpdateDate { get; set; }
}

/// <summary>
/// Material master list response DTO.
/// </summary>
public class MaterialMasterListDto
{
    public List<MaterialMasterDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Material master filter DTO.
/// </summary>
public class MaterialMasterFilterDto
{
    public string? MtrlClassId { get; set; }
    public string? VendorId { get; set; }
    public string? UseYn { get; set; }
}

/// <summary>
/// Create material master request DTO.
/// </summary>
public class CreateMaterialMasterDto
{
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassGroupId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string SpecId { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Material master operation result DTO.
/// </summary>
public class MaterialMasterResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? MtrlId { get; set; }
}

#endregion
