namespace Sphere.Application.DTOs.Master;

#region Project Master DTOs

public class ProjectMasterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string Description { get; set; } = string.Empty;
}

public class ProjectMasterListDto
{
    public List<ProjectMasterDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class ProjectMasterFilterDto
{
    public string? CustomerId { get; set; }
    public string? Status { get; set; }
    public string? UseYn { get; set; }
    public string? SearchText { get; set; }
}

#endregion
