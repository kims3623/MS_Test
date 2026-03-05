namespace Sphere.Application.DTOs.Master;

#region Default Rule DTOs

public class DefaultRuleDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RuleId { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public string RuleType { get; set; } = string.Empty;
    public string RuleTypeName { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public string TargetId { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
    public string CreateUser { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}

public class DefaultRuleListDto
{
    public List<DefaultRuleDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class DefaultRuleFilterDto
{
    public string? RuleType { get; set; }
    public string? TargetType { get; set; }
    public string? UseYn { get; set; }
    public string? SearchText { get; set; }
}

#endregion
