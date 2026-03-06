using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Raw Data 리포지토리 - Dapper(SP + raw SQL) 구현체
/// 주요 테이블: SPC_RAWDATA, SPC_RAWDATA_GROUP, SPC_TEMP_RAWDATA, SPC_CONFIRM_DATE
/// </summary>
public class RawDataRepository : DapperRepositoryBase, IRawDataRepository
{
    private readonly ILogger<RawDataRepository> _logger;

    public RawDataRepository(IDbConnection connection, ILogger<RawDataRepository> logger)
        : base(connection)
    {
        _logger = logger;
    }

    #region Basic CRUD

    /// <inheritdoc />
    /// <remarks>SP: USP_SPC_RAWDATA_SELECT</remarks>
    public async Task<RawDataListDto> GetRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        filter ??= new RawDataFilterDto();

        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize   = filter.PageSize   <= 0 ? 50 : filter.PageSize;

        // SP: USP_SPC_RAWDATA_SELECT
        // 파라미터: @P_div_seq, @P_spec_sys_id, @P_vendor_id, @P_mtrl_class_id,
        //           @P_project_id, @P_act_prod_id, @P_step_id, @P_item_id,
        //           @P_start_date, @P_end_date, @P_page_number, @P_page_size
        var items = (await QueryAsync<RawDataResultDto>(
            "USP_SPC_RAWDATA_SELECT",
            new
            {
                div_seq       = divSeq,
                spec_sys_id   = filter.SpecSysId ?? string.Empty,
                vendor_id     = filter.VendorId ?? string.Empty,
                mtrl_class_id = filter.MtrlClassId ?? string.Empty,
                project_id    = filter.ProjectId ?? string.Empty,
                act_prod_id   = filter.ActProdId ?? string.Empty,
                step_id       = filter.StepId ?? string.Empty,
                item_id       = filter.ItemId ?? string.Empty,
                start_date    = filter.StartDate ?? string.Empty,
                end_date      = filter.EndDate ?? string.Empty,
                page_number   = pageNumber,
                page_size     = pageSize
            })).ToList();

        // SP가 total_count 컬럼을 반환하면 첫 번째 행에서 읽어옴
        var totalCount = items.Count > 0 && items[0].TotalCount > 0
            ? items[0].TotalCount
            : items.Count;

        return new RawDataListDto
        {
            Items      = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize   = pageSize
        };
    }

    /// <inheritdoc />
    /// <remarks>SP: USP_SPC_RAWDATA_SELECT (rawDataId 필터 적용)</remarks>
    public async Task<RawDataResultDto?> GetRawDataByIdAsync(
        string divSeq,
        long rawDataId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                r.table_sys_id    AS RawDataId,
                r.spec_sys_id     AS SpecSysId,
                g.vendor_id       AS VendorId,
                g.vendor_name     AS VendorName,
                g.mtrl_class_id   AS MtrlClassId,
                g.mtrl_class_id   AS MtrlClassName,
                g.mtrl_id         AS MtrlId,
                g.mtrl_name       AS MtrlName,
                g.project_id      AS ProjectId,
                g.project_name    AS ProjectName,
                g.act_prod_id     AS ActProdId,
                g.act_prod_name   AS ActProdName,
                g.step_id         AS StepId,
                g.step_name       AS StepName,
                r.eqp_id          AS EqpId,
                g.item_id         AS ItemId,
                g.item_name       AS ItemName,
                g.measm_id        AS MeasmId,
                g.measm_name      AS MeasmName,
                r.work_date       AS WorkDate,
                r.shift           AS Shift,
                g.shift_name      AS ShiftName,
                r.lot_name        AS LotName,
                r.raw_data        AS RawDataValue,
                g.input_qty       AS InputQty,
                g.defect_qty      AS DefectQty,
                g.aprov_id        AS AprovId,
                r.use_yn          AS ApprovalYn,
                r.use_yn          AS UseYn,
                r.create_user_id  AS CreateUserId,
                r.create_date     AS CreateDate,
                r.update_user_id  AS UpdateUserId,
                r.update_date     AS UpdateDate
            FROM SPC_RAWDATA r
            LEFT JOIN SPC_RAWDATA_GROUP g
                ON r.spec_sys_id = g.spec_sys_id
               AND r.work_date   = g.work_date
               AND r.shift       = g.shift
               AND r.div_seq     = g.div_seq
            WHERE r.div_seq       = @DivSeq
              AND r.table_sys_id  = @RawDataId
              AND r.use_yn        = 'Y'";

        return await _connection.QueryFirstOrDefaultAsync<RawDataResultDto>(
            new CommandDefinition(sql, new { DivSeq = divSeq, RawDataId = rawDataId }, cancellationToken: cancellationToken));
    }

    /// <inheritdoc />
    /// <remarks>SP 없음 - raw SQL INSERT 사용</remarks>
    public async Task<RawDataOperationResultDto> InsertRawDataAsync(
        string divSeq,
        RawDataInsertDto dto,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO SPC_RAWDATA
                (div_seq, spec_sys_id, work_date, shift, mesm_cnt,
                 lot_name, frequency, raw_data, use_yn,
                 acti_name, origin_acti_name,
                 create_user_id, create_date, update_user_id, update_date)
            VALUES
                (@DivSeq, @SpecSysId, @WorkDate, @Shift, '1',
                 @LotName, @Frequency, @RawDataValue, 'Y',
                 'RawDataInsert', 'RawDataInsert',
                 @CreateUserId, GETDATE(), @CreateUserId, GETDATE())";

        try
        {
            var affected = await _connection.ExecuteAsync(
                new CommandDefinition(sql,
                    new
                    {
                        DivSeq       = divSeq,
                        SpecSysId    = dto.SpecSysId,
                        WorkDate     = dto.WorkDate,
                        Shift        = dto.Shift,
                        LotName      = dto.LotName,
                        Frequency    = dto.Frequency,
                        RawDataValue = dto.RawDataValue?.ToString(),
                        CreateUserId = dto.CreateUserId
                    },
                    cancellationToken: cancellationToken));

            _logger.LogInformation("Inserted raw data: DivSeq={DivSeq}, Spec={Spec}, Date={Date}", divSeq, dto.SpecSysId, dto.WorkDate);
            return new RawDataOperationResultDto { Success = true, Message = "Raw data created successfully.", AffectedRowCount = affected };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to insert raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    /// <inheritdoc />
    /// <remarks>SP 없음 - raw SQL UPDATE 사용</remarks>
    public async Task<RawDataOperationResultDto> UpdateRawDataAsync(
        string divSeq,
        RawDataUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_RAWDATA
            SET lot_name       = @LotName,
                raw_data       = @RawDataValue,
                update_user_id = @UpdateUserId,
                update_date    = GETDATE()
            WHERE div_seq    = @DivSeq
              AND spec_sys_id = @SpecSysId
              AND work_date   = @WorkDate
              AND shift       = @Shift";

        try
        {
            var affected = await _connection.ExecuteAsync(
                new CommandDefinition(sql,
                    new
                    {
                        DivSeq       = divSeq,
                        SpecSysId    = dto.SpecSysId,
                        WorkDate     = dto.WorkDate,
                        Shift        = dto.Shift,
                        LotName      = dto.LotName,
                        RawDataValue = dto.RawDataValue?.ToString(),
                        UpdateUserId = dto.UpdateUserId
                    },
                    cancellationToken: cancellationToken));

            if (affected == 0)
                return new RawDataOperationResultDto { Success = false, Message = $"Raw data not found: Spec={dto.SpecSysId}, Date={dto.WorkDate}" };

            _logger.LogInformation("Updated raw data: DivSeq={DivSeq}, Spec={Spec}", divSeq, dto.SpecSysId);
            return new RawDataOperationResultDto { Success = true, Message = "Raw data updated successfully.", AffectedRowCount = affected };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    /// <inheritdoc />
    /// <remarks>SP 없음 - raw SQL UPDATE(논리 삭제) 사용</remarks>
    public async Task<RawDataOperationResultDto> DeleteRawDataAsync(
        string divSeq,
        RawDataDeleteDto dto,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE SPC_RAWDATA
            SET use_yn         = 'N',
                update_user_id = @DeleteUserId,
                update_date    = GETDATE()
            WHERE div_seq    = @DivSeq
              AND spec_sys_id = @SpecSysId
              AND work_date   = @WorkDate
              AND shift       = @Shift";

        try
        {
            var affected = await _connection.ExecuteAsync(
                new CommandDefinition(sql,
                    new
                    {
                        DivSeq      = divSeq,
                        SpecSysId   = dto.SpecSysId,
                        WorkDate    = dto.WorkDate,
                        Shift       = dto.Shift ?? string.Empty,
                        DeleteUserId = dto.DeleteUserId
                    },
                    cancellationToken: cancellationToken));

            if (affected == 0)
                return new RawDataOperationResultDto { Success = false, Message = $"Raw data not found: Spec={dto.SpecSysId}, Date={dto.WorkDate}" };

            _logger.LogInformation("Deleted raw data: DivSeq={DivSeq}, Spec={Spec}", divSeq, dto.SpecSysId);
            return new RawDataOperationResultDto { Success = true, Message = "Raw data deleted successfully.", AffectedRowCount = affected };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    #endregion

    #region Temp Raw Data

    /// <inheritdoc />
    /// <remarks>SP 없음 - raw SQL SELECT 사용</remarks>
    public async Task<TempRawDataListDto> GetTempRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT
                0            AS TempRawDataId,
                spec_sys_id  AS SpecSysId,
                vendor_id    AS VendorId,
                vendor_name  AS VendorName,
                mtrl_id      AS MtrlId,
                mtrl_name    AS MtrlName,
                work_date    AS WorkDate,
                shift        AS Shift,
                raw_data     AS RawDataValue,
                input_qty    AS InputQty,
                defect_qty   AS DefectQty,
                upload_status AS Status,
                create_user_id AS CreateUserId,
                create_date  AS CreateDate
            FROM SPC_TEMP_RAWDATA
            WHERE div_seq = @DivSeq
              AND use_yn  = 'Y'
              AND (@SpecSysId = '' OR spec_sys_id = @SpecSysId)
              AND (@StartDate = '' OR work_date >= @StartDate)
              AND (@EndDate   = '' OR work_date <= @EndDate)
            ORDER BY create_date DESC";

        var items = (await _connection.QueryAsync<TempRawDataDto>(
            new CommandDefinition(sql,
                new
                {
                    DivSeq    = divSeq,
                    SpecSysId = filter?.SpecSysId ?? string.Empty,
                    StartDate = filter?.StartDate ?? string.Empty,
                    EndDate   = filter?.EndDate   ?? string.Empty
                },
                cancellationToken: cancellationToken))).ToList();

        return new TempRawDataListDto { Items = items, TotalCount = items.Count };
    }

    /// <inheritdoc />
    /// <remarks>SP 없음 - raw SQL INSERT 사용</remarks>
    public async Task<RawDataOperationResultDto> SaveTempRawDataAsync(
        string divSeq,
        SaveTempRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO SPC_TEMP_RAWDATA
                (div_seq, temp_id, spec_sys_id, work_date, shift,
                 raw_data, input_qty, defect_qty,
                 upload_status, use_yn, create_user_id, create_date)
            VALUES
                (@DivSeq, @TempId, @SpecSysId, @WorkDate, @Shift,
                 @RawDataValue, @InputQty, @DefectQty,
                 'DRAFT', 'Y', @CreateUserId, GETDATE())";

        try
        {
            await _connection.ExecuteAsync(
                new CommandDefinition(sql,
                    new
                    {
                        DivSeq       = divSeq,
                        TempId       = Guid.NewGuid().ToString("N")[..20],
                        SpecSysId    = dto.SpecSysId,
                        WorkDate     = dto.WorkDate,
                        Shift        = dto.Shift,
                        RawDataValue = dto.RawDataValue?.ToString() ?? "",
                        InputQty     = dto.InputQty.ToString(),
                        DefectQty    = dto.DefectQty.ToString(),
                        CreateUserId = dto.CreateUserId
                    },
                    cancellationToken: cancellationToken));

            return new RawDataOperationResultDto { Success = true, Message = "Temp raw data saved successfully.", AffectedRowCount = 1 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save temp raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    #endregion

    #region Confirm

    /// <inheritdoc />
    /// <remarks>SP 없음 - TEMP → RAWDATA 이관 로직</remarks>
    public async Task<RawDataOperationResultDto> ConfirmRawDataAsync(
        string divSeq,
        ConfirmRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        const string selectSql = @"
            SELECT temp_id, spec_sys_id, work_date, shift, raw_data, input_qty, defect_qty
            FROM SPC_TEMP_RAWDATA
            WHERE div_seq     = @DivSeq
              AND spec_sys_id = @SpecSysId
              AND work_date   = @WorkDate
              AND upload_status = 'DRAFT'
              AND use_yn      = 'Y'";

        try
        {
            var temps = (await _connection.QueryAsync<dynamic>(
                new CommandDefinition(selectSql,
                    new { DivSeq = divSeq, SpecSysId = dto.SpecSysId, WorkDate = dto.WorkDate },
                    cancellationToken: cancellationToken))).ToList();

            if (temps.Count == 0)
                return new RawDataOperationResultDto { Success = false, Message = "No draft data found to confirm." };

            var confirmed = 0;
            foreach (var temp in temps)
            {
                var insertDto = new RawDataInsertDto
                {
                    DivSeq       = divSeq,
                    SpecSysId    = temp.spec_sys_id,
                    WorkDate     = temp.work_date,
                    Shift        = temp.shift,
                    RawDataValue = decimal.TryParse((string?)temp.raw_data, out var v) ? v : null,
                    CreateUserId = dto.ConfirmUserId
                };

                var result = await InsertRawDataAsync(divSeq, insertDto, cancellationToken);
                if (result.Success)
                {
                    await _connection.ExecuteAsync(
                        new CommandDefinition(
                            "UPDATE SPC_TEMP_RAWDATA SET upload_status = 'CONFIRMED' WHERE div_seq = @DivSeq AND temp_id = @TempId",
                            new { DivSeq = divSeq, TempId = (string)temp.temp_id },
                            cancellationToken: cancellationToken));
                    confirmed++;
                }
            }

            return new RawDataOperationResultDto
            {
                Success          = confirmed > 0,
                Message          = $"Confirmed {confirmed} of {temps.Count} records.",
                AffectedRowCount = confirmed
            };
        }
        catch (Exception ex)
        {
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    /// <inheritdoc />
    /// <remarks>SP: USP_SPC_CONFIRM_DATE_SELECT</remarks>
    public async Task<ConfirmDateListDto> GetConfirmDateAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        filter ??= new RawDataFilterDto();

        var items = (await QueryAsync<ConfirmDateDto>(
            "USP_SPC_CONFIRM_DATE_SELECT",
            new
            {
                div_seq       = divSeq,
                mtrl_class_id = filter.MtrlClassId ?? string.Empty,
                vendor_id     = filter.VendorId    ?? string.Empty,
                stat_type_id  = string.Empty
            })).ToList();

        return new ConfirmDateListDto { Items = items, TotalCount = items.Count };
    }

    /// <inheritdoc />
    /// <remarks>SP: USP_SPC_CONFIRM_DATE_INSERT_UPDATE</remarks>
    public async Task<RawDataOperationResultDto> UpdateConfirmDateAsync(
        string divSeq,
        UpdateConfirmDateDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await QueryFirstOrDefaultAsync<RawDataOperationResultDto>(
                "USP_SPC_CONFIRM_DATE_INSERT_UPDATE",
                new
                {
                    div_seq       = divSeq,
                    mtrl_class_id = dto.MtrlClassId  ?? string.Empty,
                    vendor_id     = dto.VendorId      ?? string.Empty,
                    stat_type_id  = dto.StatTypeId    ?? string.Empty,
                    confirm_date  = dto.ConfirmDate   ?? string.Empty,
                    update_user_id = dto.UpdateUserId ?? string.Empty
                });

            _logger.LogInformation("UpdateConfirmDate: DivSeq={DivSeq}, MtrlClass={MtrlClass}", divSeq, dto.MtrlClassId);

            return result ?? new RawDataOperationResultDto { Success = true, Message = "Confirm date updated." };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update confirm date for DivSeq={DivSeq}", divSeq);
            return new RawDataOperationResultDto { Success = false, Message = ex.Message };
        }
    }

    #endregion

    #region Group

    public async Task<RawDataGroupDto> GetRawDataByGroupAsync(
        string divSeq,
        string groupSpecSysId,
        CancellationToken cancellationToken = default)
    {
        var result = await GetRawDataAsync(divSeq, new RawDataFilterDto { SpecSysId = groupSpecSysId }, cancellationToken);
        return new RawDataGroupDto { GroupSpecSysId = groupSpecSysId, Items = result.Items.ToList(), TotalCount = result.TotalCount };
    }

    #endregion

    #region Import/Export

    /// <inheritdoc />
    public async Task<ImportRawDataResultDto> ImportRawDataAsync(
        string divSeq,
        ImportRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        var successCount = 0;
        var failedCount  = 0;
        var errors       = new List<ImportErrorDto>();

        foreach (var row in dto.Rows)
        {
            var result = await InsertRawDataAsync(divSeq, new RawDataInsertDto
            {
                DivSeq       = divSeq,
                SpecSysId    = dto.SpecSysId,
                WorkDate     = row.WorkDate,
                Shift        = row.Shift,
                LotName      = row.LotName,
                RawDataValue = row.RawDataValue,
                InputQty     = row.InputQty,
                DefectQty    = row.DefectQty,
                CreateUserId = dto.ImportUserId
            }, cancellationToken);

            if (result.Success)
                successCount++;
            else
            {
                failedCount++;
                errors.Add(new ImportErrorDto { RowNumber = row.RowNumber, Field = "General", ErrorMessage = result.Message });
            }
        }

        return new ImportRawDataResultDto
        {
            Success      = failedCount == 0,
            Message      = failedCount == 0 ? $"Successfully imported {successCount} rows." : $"Imported {successCount} rows with {failedCount} errors.",
            TotalRows    = dto.Rows.Count,
            SuccessCount = successCount,
            FailedCount  = failedCount,
            Errors       = errors
        };
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RawDataResultDto>> ExportRawDataAsync(
        string divSeq,
        ExportRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await GetRawDataAsync(divSeq, new RawDataFilterDto
        {
            VendorId    = dto.VendorId,
            MtrlClassId = dto.MtrlClassId,
            SpecSysId   = dto.SpecSysId,
            StartDate   = dto.StartDate,
            EndDate     = dto.EndDate,
            PageSize    = 10000
        }, cancellationToken);

        return result.Items;
    }

    #endregion

    #region Validation

    /// <inheritdoc />
    public async Task<ValidationResultDto> ValidateRawDataAsync(
        string divSeq,
        ValidateRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        var errors = new List<ValidationErrorDto>();

        if (string.IsNullOrEmpty(dto.SpecSysId))
            errors.Add(new ValidationErrorDto { Field = "SpecSysId", ErrorMessage = "Spec ID is required." });
        if (string.IsNullOrEmpty(dto.WorkDate))
            errors.Add(new ValidationErrorDto { Field = "WorkDate", ErrorMessage = "Work date is required." });
        if (dto.InputQty < 0)
            errors.Add(new ValidationErrorDto { Field = "InputQty", ErrorMessage = "Input quantity must be >= 0." });
        if (dto.DefectQty < 0)
            errors.Add(new ValidationErrorDto { Field = "DefectQty", ErrorMessage = "Defect quantity must be >= 0." });
        if (dto.DefectQty > dto.InputQty && dto.InputQty > 0)
            errors.Add(new ValidationErrorDto { Field = "DefectQty", ErrorMessage = "Defect quantity cannot exceed input quantity." });

        // 스펙 한계값 검증 (SP 없음 - raw SQL)
        if (!string.IsNullOrEmpty(dto.SpecSysId) && dto.RawDataValue.HasValue && errors.Count == 0)
        {
            try
            {
                const string specSql = @"
                    SELECT usl_value, lsl_value
                    FROM SPC_SPEC_MST
                    WHERE spec_sys_id = @SpecSysId";

                var spec = await _connection.QueryFirstOrDefaultAsync<dynamic>(
                    new CommandDefinition(specSql, new { SpecSysId = dto.SpecSysId }, cancellationToken: cancellationToken));

                if (spec != null)
                {
                    if (decimal.TryParse((string?)spec.usl_value, out var usl) && dto.RawDataValue > usl)
                        errors.Add(new ValidationErrorDto { Field = "RawDataValue", ErrorMessage = $"Value exceeds USL ({usl})." });
                    if (decimal.TryParse((string?)spec.lsl_value, out var lsl) && dto.RawDataValue < lsl)
                        errors.Add(new ValidationErrorDto { Field = "RawDataValue", ErrorMessage = $"Value below LSL ({lsl})." });
                }
            }
            catch { /* Spec 조회 실패 시 한계값 검증 건너뜀 */ }
        }

        return new ValidationResultDto
        {
            IsValid = errors.Count == 0,
            Message = errors.Count == 0 ? "Validation passed." : $"{errors.Count} validation error(s) found.",
            Errors  = errors
        };
    }

    #endregion

    #region Batch

    /// <inheritdoc />
    public async Task<BatchSaveResultDto> BatchSaveRawDataAsync(
        string divSeq,
        BatchSaveRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        var insertedCount = 0;
        var updatedCount  = 0;
        var deletedCount  = 0;
        var failedCount   = 0;
        var errors        = new List<BatchErrorDto>();

        for (var i = 0; i < dto.Rows.Count; i++)
        {
            var row = dto.Rows[i];
            try
            {
                RawDataOperationResultDto result;
                switch (row.OperationType.ToUpperInvariant())
                {
                    case "INSERT":
                        result = await InsertRawDataAsync(divSeq, new RawDataInsertDto
                        {
                            DivSeq = divSeq, SpecSysId = dto.SpecSysId, WorkDate = row.WorkDate,
                            Shift = row.Shift, LotName = row.LotName, RawDataValue = row.RawDataValue,
                            InputQty = row.InputQty, DefectQty = row.DefectQty, CreateUserId = dto.SaveUserId
                        }, cancellationToken);
                        if (result.Success) insertedCount++;
                        else { failedCount++; errors.Add(new BatchErrorDto { RowIndex = i, OperationType = row.OperationType, ErrorMessage = result.Message }); }
                        break;

                    case "UPDATE":
                        if (!row.RawDataId.HasValue)
                        { failedCount++; errors.Add(new BatchErrorDto { RowIndex = i, OperationType = row.OperationType, ErrorMessage = "RawDataId is required for UPDATE." }); continue; }
                        result = await UpdateRawDataAsync(divSeq, new RawDataUpdateDto
                        {
                            RawDataId = row.RawDataId.Value, SpecSysId = dto.SpecSysId, WorkDate = row.WorkDate,
                            Shift = row.Shift, LotName = row.LotName, RawDataValue = row.RawDataValue,
                            InputQty = row.InputQty, DefectQty = row.DefectQty, UpdateUserId = dto.SaveUserId
                        }, cancellationToken);
                        if (result.Success) updatedCount++;
                        else { failedCount++; errors.Add(new BatchErrorDto { RowIndex = i, RawDataId = row.RawDataId, OperationType = row.OperationType, ErrorMessage = result.Message }); }
                        break;

                    case "DELETE":
                        if (!row.RawDataId.HasValue)
                        { failedCount++; errors.Add(new BatchErrorDto { RowIndex = i, OperationType = row.OperationType, ErrorMessage = "RawDataId is required for DELETE." }); continue; }
                        result = await DeleteRawDataAsync(divSeq, new RawDataDeleteDto
                        {
                            RawDataId = row.RawDataId.Value, SpecSysId = dto.SpecSysId,
                            WorkDate = row.WorkDate, DeleteUserId = dto.SaveUserId
                        }, cancellationToken);
                        if (result.Success) deletedCount++;
                        else { failedCount++; errors.Add(new BatchErrorDto { RowIndex = i, RawDataId = row.RawDataId, OperationType = row.OperationType, ErrorMessage = result.Message }); }
                        break;

                    default:
                        failedCount++;
                        errors.Add(new BatchErrorDto { RowIndex = i, OperationType = row.OperationType, ErrorMessage = $"Unknown operation: {row.OperationType}" });
                        break;
                }
            }
            catch (Exception ex)
            {
                failedCount++;
                errors.Add(new BatchErrorDto { RowIndex = i, RawDataId = row.RawDataId, OperationType = row.OperationType, ErrorMessage = ex.Message });
            }
        }

        return new BatchSaveResultDto
        {
            Success       = failedCount == 0,
            Message       = failedCount == 0 ? $"Batch save: {insertedCount} inserted, {updatedCount} updated, {deletedCount} deleted." : $"Batch save with {failedCount} errors.",
            TotalRows     = dto.Rows.Count,
            InsertedCount = insertedCount,
            UpdatedCount  = updatedCount,
            DeletedCount  = deletedCount,
            FailedCount   = failedCount,
            Errors        = errors
        };
    }

    #endregion

    #region Statistics

    /// <inheritdoc />
    public async Task<RawDataStatisticsDto?> GetRawDataStatisticsAsync(
        string divSeq,
        string specSysId,
        string? startDate = null,
        string? endDate   = null,
        CancellationToken cancellationToken = default)
    {
        var rawData = await GetRawDataAsync(divSeq, new RawDataFilterDto
        {
            SpecSysId = specSysId,
            StartDate = startDate,
            EndDate   = endDate,
            PageSize  = 10000
        }, cancellationToken);

        var values = rawData.Items
            .Where(r => r.RawDataValue.HasValue)
            .Select(r => r.RawDataValue!.Value)
            .ToList();

        if (values.Count == 0) return null;

        var mean   = values.Average();
        var stdDev = values.Count > 1
            ? (decimal)Math.Sqrt((double)values.Sum(v => (v - mean) * (v - mean)) / (values.Count - 1))
            : 0m;

        return new RawDataStatisticsDto
        {
            SpecSysId  = specSysId,
            SpecName   = rawData.Items.FirstOrDefault()?.ItemName ?? "",
            TotalCount = values.Count,
            Average    = mean,
            StdDev     = stdDev,
            MinValue   = values.Min(),
            MaxValue   = values.Max()
        };
    }

    #endregion
}
