using Sphere.Application.DTOs.Data;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Raw data repository interface for stored procedure operations.
/// </summary>
public interface IRawDataRepository
{
    #region Basic CRUD

    /// <summary>
    /// Gets raw data list by query. (USP_SPC_RAWDATA_SELECT)
    /// </summary>
    Task<RawDataListDto> GetRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets raw data by ID. (USP_SPC_RAWDATA_SELECT)
    /// </summary>
    Task<RawDataResultDto?> GetRawDataByIdAsync(
        string divSeq,
        long rawDataId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts raw data. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<RawDataOperationResultDto> InsertRawDataAsync(
        string divSeq,
        RawDataInsertDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates raw data. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<RawDataOperationResultDto> UpdateRawDataAsync(
        string divSeq,
        RawDataUpdateDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes raw data. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<RawDataOperationResultDto> DeleteRawDataAsync(
        string divSeq,
        RawDataDeleteDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Temp Raw Data

    /// <summary>
    /// Gets temporary raw data. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<TempRawDataListDto> GetTempRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves temporary raw data. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<RawDataOperationResultDto> SaveTempRawDataAsync(
        string divSeq,
        SaveTempRawDataDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Confirm

    /// <summary>
    /// Confirms raw data. (TODO [P3]: No DB USP - confirm logic redesign needed)
    /// </summary>
    Task<RawDataOperationResultDto> ConfirmRawDataAsync(
        string divSeq,
        ConfirmRawDataDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets confirm dates. (USP_SPC_CONFIRM_DATE_SELECT)
    /// </summary>
    Task<ConfirmDateListDto> GetConfirmDateAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates confirm date. (USP_SPC_CONFIRM_DATE_INSERT_UPDATE (TODO [P1]: @V_ prefix issue))
    /// </summary>
    Task<RawDataOperationResultDto> UpdateConfirmDateAsync(
        string divSeq,
        UpdateConfirmDateDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Group

    /// <summary>
    /// Gets raw data by group. (TODO [P3]: No DB USP - use GetRawDataAsync filter)
    /// </summary>
    Task<RawDataGroupDto> GetRawDataByGroupAsync(
        string divSeq,
        string groupSpecSysId,
        CancellationToken cancellationToken = default);

    #endregion

    #region Import/Export

    /// <summary>
    /// Imports raw data. (USP_SPC_RAWDATA_IMPORT)
    /// </summary>
    Task<ImportRawDataResultDto> ImportRawDataAsync(
        string divSeq,
        ImportRawDataDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports raw data. (TODO [P3]: No DB USP - use GetRawDataAsync)
    /// </summary>
    Task<IEnumerable<RawDataResultDto>> ExportRawDataAsync(
        string divSeq,
        ExportRawDataDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Validation

    /// <summary>
    /// Validates raw data. (TODO [P3]: No DB USP - client-side validation)
    /// </summary>
    Task<ValidationResultDto> ValidateRawDataAsync(
        string divSeq,
        ValidateRawDataDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Batch

    /// <summary>
    /// Batch saves raw data. (TODO: Verify if USP_SPC_RAWDATA_BATCH_INSERT exists in DB)
    /// </summary>
    Task<BatchSaveResultDto> BatchSaveRawDataAsync(
        string divSeq,
        BatchSaveRawDataDto dto,
        CancellationToken cancellationToken = default);

    #endregion

    #region Statistics

    /// <summary>
    /// Gets raw data statistics. (USP_SPC_INIT_CL_CALC (TVP - pending migration))
    /// </summary>
    Task<RawDataStatisticsDto?> GetRawDataStatisticsAsync(
        string divSeq,
        string specSysId,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default);

    #endregion
}
