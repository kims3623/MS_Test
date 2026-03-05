namespace Sphere.Application.DTOs.Master;

#region Code Master DTOs

/// <summary>
/// Code master DTO (matches CodeMstRec entity).
/// </summary>
public class CodeMasterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string CodeId { get; set; } = string.Empty;
    public string CodeClassId { get; set; } = string.Empty;
    public string CodeAlias { get; set; } = string.Empty;
    public string CodeNameK { get; set; } = string.Empty;
    public string CodeNameE { get; set; } = string.Empty;
    public string CodeNameC { get; set; } = string.Empty;
    public string CodeNameV { get; set; } = string.Empty;
    public string CodeNameLocale { get; set; } = string.Empty;
    public int DisplaySeq { get; set; }
    public string CodeOpt { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Code master list response DTO.
/// </summary>
public class CodeMasterListDto
{
    public List<CodeMasterDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Code master filter DTO.
/// </summary>
public class CodeMasterFilterDto
{
    public string? CodeClassId { get; set; }
    public string? UseYn { get; set; }
    public string? SearchText { get; set; }
}

/// <summary>
/// Create code master request DTO.
/// </summary>
public class CreateCodeMasterDto
{
    public string CodeId { get; set; } = string.Empty;
    public string CodeClassId { get; set; } = string.Empty;
    public string CodeAlias { get; set; } = string.Empty;
    public string CodeNameK { get; set; } = string.Empty;
    public string CodeNameE { get; set; } = string.Empty;
    public string CodeNameC { get; set; } = string.Empty;
    public string CodeNameV { get; set; } = string.Empty;
    public int DisplaySeq { get; set; }
    public string CodeOpt { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Update code master request DTO.
/// </summary>
public class UpdateCodeMasterDto
{
    public string CodeAlias { get; set; } = string.Empty;
    public string CodeNameK { get; set; } = string.Empty;
    public string CodeNameE { get; set; } = string.Empty;
    public string CodeNameC { get; set; } = string.Empty;
    public string CodeNameV { get; set; } = string.Empty;
    public int DisplaySeq { get; set; }
    public string CodeOpt { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Code master operation result DTO.
/// </summary>
public class CodeMasterResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? CodeId { get; set; }
}

#endregion
