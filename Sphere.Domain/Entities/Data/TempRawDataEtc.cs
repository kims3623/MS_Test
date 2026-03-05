using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Temporary raw data etc entity for additional temp data fields.
/// </summary>
public class TempRawDataEtc : SphereEntity
{
    public string TempId { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string FieldValue { get; set; } = string.Empty;
    public string FieldType { get; set; } = "STRING";
    public int FieldSeq { get; set; }
    public string Remarks { get; set; } = string.Empty;
}
