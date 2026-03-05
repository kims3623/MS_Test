using System.Text.Json.Serialization;

namespace Sphere.Application.DTOs.Master;

#region Spec Master DTOs

public class SpecMasterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string SpecVersion { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string MeasmId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
    public string CreateUser { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }

    // Dapper column aliases: SP returns spec_ver → maps to SpecVer, we redirect to SpecVersion
    [JsonIgnore] public string SpecVer { get => SpecVersion; set => SpecVersion = value; }
    // SP returns create_user_id → maps to CreateUserId, we redirect to CreateUser
    [JsonIgnore] public string CreateUserId { get => CreateUser; set => CreateUser = value; }
}

public class SpecMasterListDto
{
    public List<SpecMasterDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class SpecMasterFilterDto
{
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? Status { get; set; }
    public string? UseYn { get; set; }
    public string? SearchText { get; set; }
}

public class SpecDetailDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public decimal? LowerLimit { get; set; }
    public decimal? UpperLimit { get; set; }
    public decimal? TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int DisplaySeq { get; set; }
}

public class CreateSpecMasterDto
{
    public string SpecId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string SpecVersion { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

public class UpdateSpecMasterDto
{
    public string SpecName { get; set; } = string.Empty;
    public string SpecVersion { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

public class SpecMasterResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? SpecSysId { get; set; }
}

#endregion
