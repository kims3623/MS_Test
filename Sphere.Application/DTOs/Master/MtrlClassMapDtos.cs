namespace Sphere.Application.DTOs.Master;

#region MtrlClassMap DTOs

/// <summary>
/// MtrlClassMap tree node DTO (from SPC_MTRL_CLASS_MAP + SPC_CODE_MST join).
/// </summary>
public class MtrlClassMapTreeDto
{
    public string TreeId { get; set; } = string.Empty;
    public string? TreeParentId { get; set; }
    public string MtrlClassName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string? ParentClassId { get; set; }
    public string UseYn { get; set; } = string.Empty;
    public int MapLevel { get; set; }
}

/// <summary>
/// Create MtrlClassMap request DTO.
/// </summary>
public class CreateMtrlClassMapDto
{
    public string? ParentTreeId { get; set; }
    public string MtrlClassId { get; set; } = string.Empty;
    public string ClassType { get; set; } = string.Empty;
}

/// <summary>
/// Update MtrlClassMap request DTO.
/// </summary>
public class UpdateMtrlClassMapDto
{
    public string TreeId { get; set; } = string.Empty;
    public string UseYn { get; set; } = string.Empty;
}

/// <summary>
/// MtrlClassMap operation result DTO.
/// </summary>
public class MtrlClassMapResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? TreeId { get; set; }
}

#endregion
