using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Raw data with group information for aggregated views.
/// Maps to view/query result for grouped raw data.
/// </summary>
public class RawDataWithGroup : SphereEntity
{
    /// <summary>
    /// Group specification system identifier
    /// </summary>
    public string GroupSpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Material identifier (FK)
    /// </summary>
    public string MtrlId { get; set; } = string.Empty;

    /// <summary>
    /// Material name
    /// </summary>
    public string MtrlName { get; set; } = string.Empty;

    /// <summary>
    /// Material class identifier (FK)
    /// </summary>
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// Project identifier (FK)
    /// </summary>
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// Project name
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (FK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Actual product identifier (FK)
    /// </summary>
    public string ActProdId { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name
    /// </summary>
    public string ActProdName { get; set; } = string.Empty;

    /// <summary>
    /// Step identifier (FK)
    /// </summary>
    public string StepId { get; set; } = string.Empty;

    /// <summary>
    /// Step name
    /// </summary>
    public string StepName { get; set; } = string.Empty;

    /// <summary>
    /// Item identifier (FK)
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// Item name
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method identifier (FK)
    /// </summary>
    public string MeasmId { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name
    /// </summary>
    public string MeasmName { get; set; } = string.Empty;

    /// <summary>
    /// Sample count
    /// </summary>
    public string SplCnt { get; set; } = string.Empty;

    /// <summary>
    /// Work date
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift identifier
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// Shift name
    /// </summary>
    public string ShiftName { get; set; } = string.Empty;

    /// <summary>
    /// Raw data values
    /// </summary>
    public string RawDataValue { get; set; } = string.Empty;

    /// <summary>
    /// Input quantity
    /// </summary>
    public string InputQty { get; set; } = string.Empty;

    /// <summary>
    /// Defect quantity
    /// </summary>
    public string DefectQty { get; set; } = string.Empty;

    /// <summary>
    /// Approval identifier
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// Category/classification
    /// </summary>
    public string Gubun { get; set; } = string.Empty;

    // Multilingual names (English variants)
    public string VendorNameEn { get; set; } = string.Empty;
    public string ShiftNameEn { get; set; } = string.Empty;
    public string MtrlClassNameEn { get; set; } = string.Empty;
    public string ProjectNameEn { get; set; } = string.Empty;
    public string ActProdNameEn { get; set; } = string.Empty;
    public string StepNameEn { get; set; } = string.Empty;
    public string ItemNameEn { get; set; } = string.Empty;
    public string MeasmNameEn { get; set; } = string.Empty;
}
