using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH5100 Grid entity for OTP management grid.
/// </summary>
public class SPH5100Grid : SphereEntity
{
    public string OtpId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string OtpType { get; set; } = string.Empty;
    public string OtpTypeName { get; set; } = string.Empty;
    public string OtpState { get; set; } = string.Empty;
    public string OtpStateName { get; set; } = string.Empty;
    public DateTime? RequestDate { get; set; }
    public string RequestDateStr { get; set; } = string.Empty;
    public DateTime? ExpireDate { get; set; }
    public string ExpireDateStr { get; set; } = string.Empty;
    public DateTime? VerifyDate { get; set; }
    public string VerifyDateStr { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public int VerifyCount { get; set; }
    public int MaxVerifyCount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
