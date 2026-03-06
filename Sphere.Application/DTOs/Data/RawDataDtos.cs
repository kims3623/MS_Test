namespace Sphere.Application.DTOs.Data;

#region Query DTOs

/// <summary>
/// Raw data query DTO for filtering.
/// </summary>
public class RawDataQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? ProjectId { get; set; }
    public string? ActProdId { get; set; }
    public string? StepId { get; set; }
    public string? ItemId { get; set; }
    public string? MeasmId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public string? SpecSysId { get; set; }
    public string? ApprovalYn { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

/// <summary>
/// Raw data filter DTO for API query parameters.
/// </summary>
public class RawDataFilterDto
{
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? ProjectId { get; set; }
    public string? ActProdId { get; set; }
    public string? StepId { get; set; }
    public string? ItemId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public string? SpecSysId { get; set; }
    public string? ApprovalYn { get; set; }
    public string? SearchText { get; set; }
    public string? StatTypeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

#endregion

#region Result DTOs

/// <summary>
/// Raw data result DTO with all grid fields (22+ columns).
/// </summary>
public class RawDataResultDto
{
    public int TotalCount { get; set; }
    public long RawDataId { get; set; }
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string ActProdName { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string StepName { get; set; } = string.Empty;
    public string EqpId { get; set; } = string.Empty;
    public string EqpName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string MeasmId { get; set; } = string.Empty;
    public string MeasmName { get; set; } = string.Empty;
    public int SplNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string AprovId { get; set; } = string.Empty;
    public string ApprovalYn { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";
    public string CreateUserId { get; set; } = string.Empty;
    public DateTime? CreateDate { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
    public DateTime? UpdateDate { get; set; }
}

/// <summary>
/// Raw data list response DTO.
/// </summary>
public class RawDataListDto
{
    public List<RawDataResultDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

#endregion

#region Create/Update/Delete DTOs

/// <summary>
/// Raw data insert DTO.
/// </summary>
public class RawDataInsertDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Raw data update DTO.
/// </summary>
public class RawDataUpdateDto
{
    public long RawDataId { get; set; }
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Raw data delete DTO.
/// </summary>
public class RawDataDeleteDto
{
    public long RawDataId { get; set; }
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Raw data operation result DTO.
/// </summary>
public class RawDataInsertResultDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public int AffectedRowCount { get; set; }
}

/// <summary>
/// Generic raw data operation result DTO.
/// </summary>
public class RawDataOperationResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public long? RawDataId { get; set; }
    public int AffectedRowCount { get; set; }
}

#endregion

#region Temp Raw Data DTOs

/// <summary>
/// Temporary raw data DTO.
/// </summary>
public class TempRawDataDto
{
    public long TempRawDataId { get; set; }
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string Status { get; set; } = "DRAFT";
    public string ValidationStatus { get; set; } = string.Empty;
    public string ValidationMessage { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public DateTime? CreateDate { get; set; }
}

/// <summary>
/// Temporary raw data list DTO.
/// </summary>
public class TempRawDataListDto
{
    public List<TempRawDataDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Save temporary raw data DTO.
/// </summary>
public class SaveTempRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

#endregion

#region Confirm DTOs

/// <summary>
/// Confirm raw data request DTO.
/// </summary>
public class ConfirmRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string ConfirmUserId { get; set; } = string.Empty;
}

/// <summary>
/// Confirm date DTO.
/// </summary>
public class ConfirmDateDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string ConfirmDate { get; set; } = string.Empty;
    public string ConfirmUserId { get; set; } = string.Empty;
    public string ConfirmUserName { get; set; } = string.Empty;
    public DateTime? ConfirmDateTime { get; set; }
    public int DataCount { get; set; }
}

/// <summary>
/// Confirm date list DTO.
/// </summary>
public class ConfirmDateListDto
{
    public List<ConfirmDateDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Update confirm date DTO.
/// </summary>
public class UpdateConfirmDateDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string StatTypeId { get; set; } = string.Empty;
    public string ConfirmDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

#endregion

#region Group DTOs

/// <summary>
/// Raw data by group DTO.
/// </summary>
public class RawDataGroupDto
{
    public string GroupSpecSysId { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public List<RawDataResultDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

#endregion

#region Import/Export DTOs

/// <summary>
/// Import raw data request DTO.
/// </summary>
public class ImportRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string ImportUserId { get; set; } = string.Empty;
    public List<ImportRawDataRowDto> Rows { get; set; } = new();
}

/// <summary>
/// Import raw data row DTO.
/// </summary>
public class ImportRawDataRowDto
{
    public int RowNumber { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
}

/// <summary>
/// Import result DTO.
/// </summary>
public class ImportRawDataResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int TotalRows { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public List<ImportErrorDto> Errors { get; set; } = new();
}

/// <summary>
/// Import error DTO.
/// </summary>
public class ImportErrorDto
{
    public int RowNumber { get; set; }
    public string Field { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}

/// <summary>
/// Export raw data request DTO.
/// </summary>
public class ExportRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? SpecSysId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string ExportFormat { get; set; } = "XLSX";
}

/// <summary>
/// Export result DTO.
/// </summary>
public class ExportRawDataResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public byte[]? FileContent { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public int ExportedRowCount { get; set; }
}

#endregion

#region Validation DTOs

/// <summary>
/// Validate raw data request DTO.
/// </summary>
public class ValidateRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
}

/// <summary>
/// Validation result DTO.
/// </summary>
public class ValidationResultDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<ValidationErrorDto> Errors { get; set; } = new();
}

/// <summary>
/// Validation error DTO.
/// </summary>
public class ValidationErrorDto
{
    public string Field { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string? ExpectedValue { get; set; }
    public string? ActualValue { get; set; }
}

#endregion

#region Batch DTOs

/// <summary>
/// Batch save raw data request DTO.
/// </summary>
public class BatchSaveRawDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string SaveUserId { get; set; } = string.Empty;
    public List<BatchRawDataRowDto> Rows { get; set; } = new();
}

/// <summary>
/// Batch raw data row DTO.
/// </summary>
public class BatchRawDataRowDto
{
    public long? RawDataId { get; set; }
    public string OperationType { get; set; } = "INSERT";
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public decimal? RawDataValue { get; set; }
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
}

/// <summary>
/// Batch save result DTO.
/// </summary>
public class BatchSaveResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int TotalRows { get; set; }
    public int InsertedCount { get; set; }
    public int UpdatedCount { get; set; }
    public int DeletedCount { get; set; }
    public int FailedCount { get; set; }
    public List<BatchErrorDto> Errors { get; set; } = new();
}

/// <summary>
/// Batch error DTO.
/// </summary>
public class BatchErrorDto
{
    public int RowIndex { get; set; }
    public long? RawDataId { get; set; }
    public string OperationType { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}

#endregion

#region Statistics DTOs

/// <summary>
/// Raw data statistics DTO.
/// </summary>
public class RawDataStatisticsDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public decimal? Average { get; set; }
    public decimal? StdDev { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? Cp { get; set; }
    public decimal? Cpk { get; set; }
    public decimal? UpperLimit { get; set; }
    public decimal? LowerLimit { get; set; }
    public decimal? TargetValue { get; set; }
}

#endregion
