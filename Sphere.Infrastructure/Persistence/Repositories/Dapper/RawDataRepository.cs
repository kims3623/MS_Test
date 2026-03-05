using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;
using Sphere.Domain.Entities.Data;
using Sphere.Infrastructure.Persistence;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Raw data repository: Dapper for reads (SP), EF Core for writes (no SP exists).
/// </summary>
public class RawDataRepository : DapperRepositoryBase, IRawDataRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RawDataRepository> _logger;

    public RawDataRepository(
        IDbConnection connection,
        ApplicationDbContext context,
        ILogger<RawDataRepository> logger) : base(connection)
    {
        _context = context;
        _logger = logger;
    }

    #region Basic CRUD

    /// <inheritdoc />
    public async Task<RawDataListDto> GetRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var items = await QueryAsync<RawDataResultDto>(
            "USP_SPC_RAWDATA_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                vendor_id = filter?.VendorId ?? string.Empty,
                mtrl_id = string.Empty,
                mtrl_class_id = filter?.MtrlClassId ?? string.Empty,
                project_id = filter?.ProjectId ?? string.Empty,
                act_prod_id = filter?.ActProdId ?? string.Empty,
                step_id = filter?.StepId ?? string.Empty,
                item_id = filter?.ItemId ?? string.Empty,
                measm_id = string.Empty,
                start_date = filter?.StartDate ?? string.Empty,
                end_date = filter?.EndDate ?? string.Empty
            });

        var itemList = items.ToList();
        return new RawDataListDto
        {
            Items = itemList,
            TotalCount = itemList.Count,
            PageNumber = filter?.PageNumber ?? 1,
            PageSize = filter?.PageSize ?? 50
        };
    }

    /// <inheritdoc />
    public async Task<RawDataResultDto?> GetRawDataByIdAsync(
        string divSeq,
        long rawDataId,
        CancellationToken cancellationToken = default)
    {
        return await QueryFirstOrDefaultAsync<RawDataResultDto>(
            "USP_SPC_RAWDATA_SELECT",
            new
            {
                div_seq = divSeq,
                raw_data_id = rawDataId
            });
    }

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> InsertRawDataAsync(
        string divSeq,
        RawDataInsertDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var now = DateTime.Now;
            var entity = new Domain.Entities.Data.RawData
            {
                // TableSysId is IDENTITY — auto-generated
                DivSeq = divSeq,
                SpecSysId = dto.SpecSysId,
                WorkDate = dto.WorkDate,
                Shift = dto.Shift,
                MesmCnt = "1",
                LotName = dto.LotName,
                Frequency = dto.Frequency,
                RawDataValue = dto.RawDataValue?.ToString(),
                UseYn = "Y",
                ActiName = "RawDataInsert",
                OriginActiName = "RawDataInsert",
                CreateUserId = dto.CreateUserId,
                CreateDate = now,
                UpdateUserId = dto.CreateUserId,
                UpdateDate = now, // NOT NULL — part of composite PK
            };

            _context.RawDatas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Inserted raw data: DivSeq={DivSeq}, Spec={Spec}, Date={Date}, Shift={Shift}",
                divSeq, dto.SpecSysId, dto.WorkDate, dto.Shift);

            return new RawDataOperationResultDto
            {
                Success = true,
                Message = "Raw data created successfully.",
                AffectedRowCount = 1
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to insert raw data for Spec={Spec}, Date={Date}", dto.SpecSysId, dto.WorkDate);
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.InnerException?.Message ?? "Failed to insert raw data."
            };
        }
    }

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> UpdateRawDataAsync(
        string divSeq,
        RawDataUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // PK is (TableSysId, UpdateDate) — find by business key instead
            var entity = await _context.RawDatas.FirstOrDefaultAsync(
                e => e.DivSeq == divSeq
                    && e.SpecSysId == dto.SpecSysId
                    && e.WorkDate == dto.WorkDate
                    && e.Shift == dto.Shift,
                cancellationToken);

            if (entity == null)
            {
                return new RawDataOperationResultDto
                {
                    Success = false,
                    Message = $"Raw data not found: Spec={dto.SpecSysId}, Date={dto.WorkDate}, Shift={dto.Shift}"
                };
            }

            entity.LotName = dto.LotName;
            entity.RawDataValue = dto.RawDataValue?.ToString();
            entity.UpdateUserId = dto.UpdateUserId;
            entity.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated raw data: DivSeq={DivSeq}, Spec={Spec}, Date={Date}, Shift={Shift}",
                divSeq, dto.SpecSysId, dto.WorkDate, dto.Shift);

            return new RawDataOperationResultDto
            {
                Success = true,
                Message = "Raw data updated successfully.",
                AffectedRowCount = 1
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to update raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.InnerException?.Message ?? "Failed to update raw data."
            };
        }
    }

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> DeleteRawDataAsync(
        string divSeq,
        RawDataDeleteDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // PK is (TableSysId, UpdateDate) — find by business key instead
            var entity = await _context.RawDatas.FirstOrDefaultAsync(
                e => e.DivSeq == divSeq
                    && e.SpecSysId == dto.SpecSysId
                    && e.WorkDate == dto.WorkDate
                    && e.Shift == (dto.Shift ?? ""),
                cancellationToken);

            if (entity == null)
            {
                return new RawDataOperationResultDto
                {
                    Success = false,
                    Message = $"Raw data not found: Spec={dto.SpecSysId}, Date={dto.WorkDate}"
                };
            }

            _context.RawDatas.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted raw data: DivSeq={DivSeq}, Spec={Spec}, Date={Date}",
                divSeq, dto.SpecSysId, dto.WorkDate);

            return new RawDataOperationResultDto
            {
                Success = true,
                Message = "Raw data deleted successfully.",
                AffectedRowCount = 1
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to delete raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.InnerException?.Message ?? "Failed to delete raw data."
            };
        }
    }

    #endregion

    #region Temp Raw Data

    /// <inheritdoc />
    public async Task<TempRawDataListDto> GetTempRawDataAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.TempRawDatas
            .Where(t => t.DivSeq == divSeq && t.UseYn == "Y");

        if (!string.IsNullOrEmpty(filter?.SpecSysId))
            query = query.Where(t => t.SpecSysId == filter.SpecSysId);
        if (!string.IsNullOrEmpty(filter?.StartDate))
            query = query.Where(t => t.WorkDate.CompareTo(filter.StartDate) >= 0);
        if (!string.IsNullOrEmpty(filter?.EndDate))
            query = query.Where(t => t.WorkDate.CompareTo(filter.EndDate) <= 0);

        var entities = await query
            .OrderByDescending(t => t.CreateDate)
            .ToListAsync(cancellationToken);

        var items = entities.Select(t => new TempRawDataDto
            {
                TempRawDataId = 0,
                SpecSysId = t.SpecSysId,
                VendorId = t.VendorId,
                VendorName = t.VendorName,
                MtrlId = t.MtrlId,
                MtrlName = t.MtrlName,
                WorkDate = t.WorkDate,
                Shift = t.Shift,
                RawDataValue = decimal.TryParse(t.RawDataValue, out var v) ? v : null,
                InputQty = int.TryParse(t.InputQty, out var iq) ? iq : 0,
                DefectQty = int.TryParse(t.DefectQty, out var dq) ? dq : 0,
                Status = t.UploadStatus,
                CreateUserId = t.CreateUserId,
                CreateDate = t.CreateDate
            })
            .ToList();

        return new TempRawDataListDto
        {
            Items = items,
            TotalCount = items.Count
        };
    }

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> SaveTempRawDataAsync(
        string divSeq,
        SaveTempRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new TempRawData
            {
                TempId = Guid.NewGuid().ToString("N")[..20],
                DivSeq = divSeq,
                SpecSysId = dto.SpecSysId,
                WorkDate = dto.WorkDate,
                Shift = dto.Shift,
                RawDataValue = dto.RawDataValue?.ToString() ?? "",
                InputQty = dto.InputQty.ToString(),
                DefectQty = dto.DefectQty.ToString(),
                UploadStatus = "DRAFT",
                UseYn = "Y",
                CreateUserId = dto.CreateUserId,
                CreateDate = DateTime.Now
            };

            _context.TempRawDatas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new RawDataOperationResultDto
            {
                Success = true,
                Message = "Temp raw data saved successfully.",
                AffectedRowCount = 1
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to save temp raw data for Spec={Spec}", dto.SpecSysId);
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.InnerException?.Message ?? "Failed to save temp raw data."
            };
        }
    }

    #endregion

    #region Confirm

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> ConfirmRawDataAsync(
        string divSeq,
        ConfirmRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var temps = await _context.TempRawDatas
                .Where(t => t.DivSeq == divSeq
                    && t.SpecSysId == dto.SpecSysId
                    && t.WorkDate == dto.WorkDate
                    && t.UploadStatus == "DRAFT"
                    && t.UseYn == "Y")
                .ToListAsync(cancellationToken);

            if (temps.Count == 0)
                return new RawDataOperationResultDto { Success = false, Message = "No draft data found to confirm." };

            var confirmed = 0;
            foreach (var temp in temps)
            {
                var insertResult = await InsertRawDataAsync(divSeq, new RawDataInsertDto
                {
                    DivSeq = divSeq,
                    SpecSysId = temp.SpecSysId,
                    WorkDate = temp.WorkDate,
                    Shift = temp.Shift,
                    RawDataValue = decimal.TryParse(temp.RawDataValue, out var v) ? v : null,
                    InputQty = int.TryParse(temp.InputQty, out var iq) ? iq : 0,
                    DefectQty = int.TryParse(temp.DefectQty, out var dq) ? dq : 0,
                    CreateUserId = dto.ConfirmUserId
                }, cancellationToken);

                if (insertResult.Success)
                {
                    temp.UploadStatus = "CONFIRMED";
                    confirmed++;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new RawDataOperationResultDto
            {
                Success = confirmed > 0,
                Message = $"Confirmed {confirmed} of {temps.Count} records.",
                AffectedRowCount = confirmed
            };
        }
        catch (Exception ex)
        {
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.InnerException?.Message ?? ex.Message
            };
        }
    }

    /// <inheritdoc />
    public async Task<ConfirmDateListDto> GetConfirmDateAsync(
        string divSeq,
        RawDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var items = await QueryAsync<ConfirmDateDto>(
            "USP_SPC_CONFIRM_DATE_SELECT",
            new
            {
                div_seq = divSeq,
                mtrl_class_id = filter?.MtrlClassId,
                vendor_id = filter?.VendorId,
                stat_type_id = filter?.StatTypeId
            });

        var itemList = items.ToList();
        return new ConfirmDateListDto
        {
            Items = itemList,
            TotalCount = itemList.Count
        };
    }

    /// <inheritdoc />
    public async Task<RawDataOperationResultDto> UpdateConfirmDateAsync(
        string divSeq,
        UpdateConfirmDateDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // USP_SPC_CONFIRM_DATE_INSERT_UPDATE uses @V_ prefix (not @P_),
            // so we bypass ConvertToSpParameters and build DynamicParameters manually.
            var parameters = new DynamicParameters();
            parameters.Add("V_div_seq", divSeq);
            parameters.Add("V_mtrl_class_id", dto.MtrlClassId ?? "");
            parameters.Add("V_vendor_id", dto.VendorId ?? "");
            parameters.Add("V_stat_type_id", dto.StatTypeId ?? "");
            parameters.Add("V_final_save_date", dto.ConfirmDate ?? "");
            parameters.Add("V_create_user_id", dto.UpdateUserId ?? "");

            await _connection.ExecuteAsync(
                "USP_SPC_CONFIRM_DATE_INSERT_UPDATE",
                parameters,
                transaction: null,
                commandTimeout: null,
                commandType: System.Data.CommandType.StoredProcedure);

            _logger.LogInformation(
                "Updated confirm date: DivSeq={DivSeq}, MtrlClass={MtrlClass}, Vendor={Vendor}, StatType={StatType}, Date={Date}",
                divSeq, dto.MtrlClassId, dto.VendorId, dto.StatTypeId, dto.ConfirmDate);

            return new RawDataOperationResultDto
            {
                Success = true,
                Message = "Confirm date updated successfully.",
                AffectedRowCount = 1
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update confirm date for DivSeq={DivSeq}", divSeq);
            return new RawDataOperationResultDto
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    #endregion

    #region Group

    /// <inheritdoc />
    public async Task<RawDataGroupDto> GetRawDataByGroupAsync(
        string divSeq,
        string groupSpecSysId,
        CancellationToken cancellationToken = default)
    {
        var result = await GetRawDataAsync(divSeq, new RawDataFilterDto
        {
            SpecSysId = groupSpecSysId
        }, cancellationToken);

        return new RawDataGroupDto
        {
            GroupSpecSysId = groupSpecSysId,
            Items = result.Items.ToList(),
            TotalCount = result.TotalCount
        };
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
        var failedCount = 0;
        var errors = new List<ImportErrorDto>();

        foreach (var row in dto.Rows)
        {
            try
            {
                var insertDto = new RawDataInsertDto
                {
                    DivSeq = divSeq,
                    SpecSysId = dto.SpecSysId,
                    WorkDate = row.WorkDate,
                    Shift = row.Shift,
                    LotName = row.LotName,
                    RawDataValue = row.RawDataValue,
                    InputQty = row.InputQty,
                    DefectQty = row.DefectQty,
                    CreateUserId = dto.ImportUserId
                };

                var result = await InsertRawDataAsync(divSeq, insertDto, cancellationToken);
                if (result.Success)
                {
                    successCount++;
                }
                else
                {
                    failedCount++;
                    errors.Add(new ImportErrorDto
                    {
                        RowNumber = row.RowNumber,
                        Field = "General",
                        ErrorMessage = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                failedCount++;
                errors.Add(new ImportErrorDto
                {
                    RowNumber = row.RowNumber,
                    Field = "General",
                    ErrorMessage = ex.Message
                });
            }
        }

        return new ImportRawDataResultDto
        {
            Success = failedCount == 0,
            Message = failedCount == 0
                ? $"Successfully imported {successCount} rows."
                : $"Imported {successCount} rows with {failedCount} errors.",
            TotalRows = dto.Rows.Count,
            SuccessCount = successCount,
            FailedCount = failedCount,
            Errors = errors
        };
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RawDataResultDto>> ExportRawDataAsync(
        string divSeq,
        ExportRawDataDto dto,
        CancellationToken cancellationToken = default)
    {
        // TODO [P3]: USP_SPC_RAWDATA_EXPORT은 DB에 존재하지 않음. GetRawDataAsync 재활용.
        var filter = new RawDataFilterDto
        {
            VendorId = dto.VendorId,
            MtrlClassId = dto.MtrlClassId,
            SpecSysId = dto.SpecSysId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            PageSize = 10000
        };
        var result = await GetRawDataAsync(divSeq, filter, cancellationToken);
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

        // Validate against spec limits if available
        if (!string.IsNullOrEmpty(dto.SpecSysId) && dto.RawDataValue.HasValue)
        {
            var sql = "SELECT usl, lsl FROM SPC_SPEC_MST WHERE spec_sys_id = @spec_sys_id";
            try
            {
                var spec = await QueryFirstOrDefaultRawSqlAsync<dynamic>(sql, new { spec_sys_id = dto.SpecSysId });
                if (spec != null)
                {
                    decimal? usl = spec.usl ?? spec.Usl;
                    decimal? lsl = spec.lsl ?? spec.Lsl;
                    if (usl.HasValue && dto.RawDataValue > usl)
                        errors.Add(new ValidationErrorDto { Field = "RawDataValue", ErrorMessage = $"Value exceeds USL ({usl})." });
                    if (lsl.HasValue && dto.RawDataValue < lsl)
                        errors.Add(new ValidationErrorDto { Field = "RawDataValue", ErrorMessage = $"Value below LSL ({lsl})." });
                }
            }
            catch { /* Spec lookup failed — skip limit check */ }
        }

        return new ValidationResultDto
        {
            IsValid = errors.Count == 0,
            Message = errors.Count == 0 ? "Validation passed." : $"{errors.Count} validation error(s) found.",
            Errors = errors
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
        var updatedCount = 0;
        var deletedCount = 0;
        var failedCount = 0;
        var errors = new List<BatchErrorDto>();

        for (var i = 0; i < dto.Rows.Count; i++)
        {
            var row = dto.Rows[i];
            try
            {
                RawDataOperationResultDto result;

                switch (row.OperationType.ToUpperInvariant())
                {
                    case "INSERT":
                        var insertDto = new RawDataInsertDto
                        {
                            DivSeq = divSeq,
                            SpecSysId = dto.SpecSysId,
                            WorkDate = row.WorkDate,
                            Shift = row.Shift,
                            LotName = row.LotName,
                            RawDataValue = row.RawDataValue,
                            InputQty = row.InputQty,
                            DefectQty = row.DefectQty,
                            CreateUserId = dto.SaveUserId
                        };
                        result = await InsertRawDataAsync(divSeq, insertDto, cancellationToken);
                        if (result.Success) insertedCount++;
                        else
                        {
                            failedCount++;
                            errors.Add(new BatchErrorDto
                            {
                                RowIndex = i,
                                OperationType = row.OperationType,
                                ErrorMessage = result.Message
                            });
                        }
                        break;

                    case "UPDATE":
                        if (!row.RawDataId.HasValue)
                        {
                            failedCount++;
                            errors.Add(new BatchErrorDto
                            {
                                RowIndex = i,
                                OperationType = row.OperationType,
                                ErrorMessage = "RawDataId is required for UPDATE operation."
                            });
                            continue;
                        }
                        var updateDto = new RawDataUpdateDto
                        {
                            RawDataId = row.RawDataId.Value,
                            SpecSysId = dto.SpecSysId,
                            WorkDate = row.WorkDate,
                            Shift = row.Shift,
                            LotName = row.LotName,
                            RawDataValue = row.RawDataValue,
                            InputQty = row.InputQty,
                            DefectQty = row.DefectQty,
                            UpdateUserId = dto.SaveUserId
                        };
                        result = await UpdateRawDataAsync(divSeq, updateDto, cancellationToken);
                        if (result.Success) updatedCount++;
                        else
                        {
                            failedCount++;
                            errors.Add(new BatchErrorDto
                            {
                                RowIndex = i,
                                RawDataId = row.RawDataId,
                                OperationType = row.OperationType,
                                ErrorMessage = result.Message
                            });
                        }
                        break;

                    case "DELETE":
                        if (!row.RawDataId.HasValue)
                        {
                            failedCount++;
                            errors.Add(new BatchErrorDto
                            {
                                RowIndex = i,
                                OperationType = row.OperationType,
                                ErrorMessage = "RawDataId is required for DELETE operation."
                            });
                            continue;
                        }
                        var deleteDto = new RawDataDeleteDto
                        {
                            RawDataId = row.RawDataId.Value,
                            SpecSysId = dto.SpecSysId,
                            WorkDate = row.WorkDate,
                            DeleteUserId = dto.SaveUserId
                        };
                        result = await DeleteRawDataAsync(divSeq, deleteDto, cancellationToken);
                        if (result.Success) deletedCount++;
                        else
                        {
                            failedCount++;
                            errors.Add(new BatchErrorDto
                            {
                                RowIndex = i,
                                RawDataId = row.RawDataId,
                                OperationType = row.OperationType,
                                ErrorMessage = result.Message
                            });
                        }
                        break;

                    default:
                        failedCount++;
                        errors.Add(new BatchErrorDto
                        {
                            RowIndex = i,
                            OperationType = row.OperationType,
                            ErrorMessage = $"Unknown operation type: {row.OperationType}"
                        });
                        break;
                }
            }
            catch (Exception ex)
            {
                failedCount++;
                errors.Add(new BatchErrorDto
                {
                    RowIndex = i,
                    RawDataId = row.RawDataId,
                    OperationType = row.OperationType,
                    ErrorMessage = ex.Message
                });
            }
        }

        return new BatchSaveResultDto
        {
            Success = failedCount == 0,
            Message = failedCount == 0
                ? $"Batch save completed: {insertedCount} inserted, {updatedCount} updated, {deletedCount} deleted."
                : $"Batch save completed with {failedCount} errors.",
            TotalRows = dto.Rows.Count,
            InsertedCount = insertedCount,
            UpdatedCount = updatedCount,
            DeletedCount = deletedCount,
            FailedCount = failedCount,
            Errors = errors
        };
    }

    #endregion

    #region Statistics

    /// <inheritdoc />
    /// <remarks>
    /// Computes statistics in C# from raw data fetched via GetRawDataAsync.
    /// Avoids TVP dependency on USP_SPC_INIT_CL_CALC.
    /// </remarks>
    public async Task<RawDataStatisticsDto?> GetRawDataStatisticsAsync(
        string divSeq,
        string specSysId,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default)
    {
        var rawData = await GetRawDataAsync(divSeq, new RawDataFilterDto
        {
            StartDate = startDate,
            EndDate = endDate
        }, cancellationToken);

        var values = rawData.Items
            .Where(r => r.SpecSysId == specSysId && r.RawDataValue.HasValue)
            .Select(r => r.RawDataValue!.Value)
            .ToList();

        if (values.Count == 0) return null;

        var mean = values.Average();
        var stdDev = values.Count > 1
            ? (decimal)Math.Sqrt((double)values.Sum(v => (v - mean) * (v - mean)) / (values.Count - 1))
            : 0m;

        return new RawDataStatisticsDto
        {
            SpecSysId = specSysId,
            SpecName = rawData.Items.FirstOrDefault(r => r.SpecSysId == specSysId)?.ItemName ?? "",
            TotalCount = values.Count,
            Average = mean,
            StdDev = stdDev,
            MinValue = values.Min(),
            MaxValue = values.Max()
        };
    }

    #endregion
}
