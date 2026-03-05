using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// Email template entity for notification templates.
/// </summary>
public class EmailTemplate : SphereEntity
{
    public string TemplateId { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public string TemplateType { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string SubjectK { get; set; } = string.Empty;
    public string SubjectE { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string BodyK { get; set; } = string.Empty;
    public string BodyE { get; set; } = string.Empty;
    public string Variables { get; set; } = string.Empty;
}
