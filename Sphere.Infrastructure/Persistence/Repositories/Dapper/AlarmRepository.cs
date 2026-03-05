using System.Data;
using Dapper;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Alarm repository implementation using Dapper for stored procedures.
/// </summary>
public class AlarmRepository : DapperRepositoryBase, IAlarmRepository
{
    public AlarmRepository(IDbConnection connection) : base(connection) { }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_ALARM_ACTION_USER_SELECT
    /// DB params: @P_div_seq VARCHAR, @P_alm_sys_id VARCHAR (only 2 params)
    /// Note: AlmProcId from query DTO is mapped to alm_sys_id parameter.
    /// </remarks>
    public async Task<IEnumerable<AlarmActionUserDto>> GetActionUsersAsync(AlarmActionUserQueryDto query)
    {
        return await QueryAsync<AlarmActionUserDto>(
            "USP_SPC_ALARM_ACTION_USER_SELECT",
            new
            {
                div_seq = query.DivSeq,
                alm_sys_id = query.AlmProcId ?? string.Empty
            });
    }

    /// <inheritdoc />
    public async Task<AlarmIssueFirstActionDto?> GetIssueFirstActionAsync(string divSeq, string almSysId)
    {
        return await QueryFirstOrDefaultAsync<AlarmIssueFirstActionDto>(
            "USP_SPC_ALARM_ISSUE_FIRST_ACTION",
            new { div_seq = divSeq, alm_sys_id = almSysId });
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AlarmActionDto>> GetAlarmActionsAsync(
        string divSeq,
        string alarmSysId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await QueryAsync<dynamic>(
                "USP_SPC_ALARM_ACTION_USER_SELECT",
                new
                {
                    div_seq = divSeq,
                    alm_proc_id = "",
                    vendor_id = "",
                    mtrl_class_id = ""
                });

            var actions = result
                .Select(r => (string)(r.alm_action_id ?? ""))
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .Select((id, i) => new AlarmActionDto
                {
                    ActionId = id,
                    ActionName = id,
                    ActionNameK = id,
                    Seq = i + 1
                })
                .ToList();

            if (actions.Count > 0) return actions;
        }
        catch
        {
            // SP may not exist; fall through to defaults
        }

        return new List<AlarmActionDto>
        {
            new() { ActionId = "ACCEPT", ActionName = "수락", ActionNameK = "수락", Seq = 1 },
            new() { ActionId = "REJECT", ActionName = "거부", ActionNameK = "거부", Seq = 2 },
            new() { ActionId = "ESCALATE", ActionName = "상위보고", ActionNameK = "상위보고", Seq = 3 },
            new() { ActionId = "CLOSE", ActionName = "종료", ActionNameK = "종료", Seq = 4 }
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH4020_STOP
    /// DB params: @P_div_seq VARCHAR(40), @P_alm_sys_ids VARCHAR(MAX), @P_reason_code VARCHAR(40),
    ///            @P_description NVARCHAR(4000), @P_create_user_id VARCHAR(40)
    /// </remarks>
    public async Task<(bool Success, string? Message, string? NewStatus)> CloseAlarmAsync(
        string divSeq,
        string alarmSysId,
        string actionId,
        string stopReason,
        string userId,
        List<string>? customerIds,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await QueryFirstOrDefaultAsync<dynamic>(
                "USP_SPC_SPH4020_STOP",
                new
                {
                    div_seq = divSeq,
                    alm_sys_ids = alarmSysId,        // singular → plural (comma separated)
                    reason_code = actionId,           // actionId maps to reason_code
                    description = stopReason,         // stopReason maps to description
                    create_user_id = userId
                });

            if (result != null)
            {
                var success = result.success ?? true;
                var message = result.message as string;
                var newStatus = result.new_status as string ?? "Closed";
                return (success, message, newStatus);
            }

            return (true, null, "Closed");
        }
        catch (Exception ex)
        {
            return (false, ex.Message, null);
        }
    }

    #region B6 New Methods - Alarm List

    /// <inheritdoc />
    public async Task<AlarmListResponseDto> GetListAsync(
        AlarmListFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        // DB USP: USP_SPC_SPH4020_SELECT (20 params)
        // Note: page_number/page_size removed - DB USP does not support server-side paging
        var items = await QueryAsync<AlarmListItemDto>(
            "USP_SPC_SPH4020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = filter.DivSeq,
                Search_Type = "Total",
                My_vendor_id = "",
                login_id = filter.UserId ?? "",
                vendor_id = filter.VendorId ?? "",
                start_date = filter.StartDate,
                end_date = filter.EndDate,
                alm_sys_id = "",
                alm_action_id = filter.AlmActionId ?? "",
                alm_proc_id = "",
                mtrl_class_id = filter.MtrlClassId ?? "",
                mtrl_id = "",
                act_prod_id = "",
                project_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                alm_proc_001_YN = filter.AlmProcYn ?? "N",
                stop_YN = filter.StopYn ?? "N"
            });

        var itemList = items.ToList();

        return new AlarmListResponseDto
        {
            Items = itemList,
            TotalCount = itemList.Count,
            PageNumber = filter.PageNumber ?? 1,
            PageSize = filter.PageSize ?? 20
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH4020_SELECT (same USP as list, filtered by alm_sys_id for detail)
    /// </remarks>
    public async Task<AlarmDetailDto?> GetDetailAsync(
        string divSeq,
        string almSysId,
        CancellationToken cancellationToken = default)
    {
        return await QueryFirstOrDefaultAsync<AlarmDetailDto>(
            "USP_SPC_SPH4020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                Search_Type = "Total",
                My_vendor_id = "",
                login_id = "",
                vendor_id = "",
                start_date = "",
                end_date = "",
                alm_sys_id = almSysId,
                alm_action_id = "",
                alm_proc_id = "",
                mtrl_class_id = "",
                mtrl_id = "",
                act_prod_id = "",
                project_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                alm_proc_001_YN = "N",
                stop_YN = "N"
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH4020_INSERT with TVP SPC_ALARM_ATTACH_LIST_TYPE.
    /// Uses DynamicParameters directly to support DataTable TVP parameter.
    /// </remarks>
    public async Task<CreateAlarmResponseDto> CreateAsync(
        CreateAlarmRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@P_div_seq", request.DivSeq);
        parameters.Add("@P_alm_proc_id", request.AlmProcId);
        parameters.Add("@P_title", request.Title);
        parameters.Add("@P_contents", request.Contents);
        parameters.Add("@P_vendor_id", request.VendorId);
        parameters.Add("@P_mtrl_class_id", request.MtrlClassId);
        parameters.Add("@P_spec_sys_id", request.SpecSysId ?? "");
        parameters.Add("@P_severity", request.Severity);
        parameters.Add("@P_create_user_id", request.CreateUserId);

        // Empty TVP for attachments (alarm can be created without attachments)
        var attachTable = new DataTable();
        attachTable.Columns.Add("file_name", typeof(string));
        attachTable.Columns.Add("file_extension", typeof(string));
        attachTable.Columns.Add("file_content", typeof(byte[]));
        parameters.Add("@P_attach_list", attachTable.AsTableValuedParameter("SPC_ALARM_ATTACH_LIST_TYPE"));

        var result = await QueryFirstOrDefaultWithTvpAsync<CreateAlarmResponseDto>(
            "USP_SPC_SPH4020_INSERT", parameters);

        return result ?? new CreateAlarmResponseDto { Result = "FAIL", ResultMessage = "No response from stored procedure" };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH4020_UPDATE
    /// DB params: @P_div_seq VARCHAR(40), @P_alm_sys_id VARCHAR(40), @P_next_alm_action_id VARCHAR(40),
    ///            @P_create_user_id VARCHAR(40), @P_reject_yn CHAR(1),
    ///            @IsSuccess BIT OUTPUT, @ErrorMessage NVARCHAR(MAX) OUTPUT
    /// Note: OUTPUT params (IsSuccess, ErrorMessage) are ignored for now - input params only.
    /// </remarks>
    public async Task<UpdateAlarmResponseDto> UpdateAsync(
        UpdateAlarmRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await QueryFirstOrDefaultAsync<UpdateAlarmResponseDto>(
            "USP_SPC_SPH4020_UPDATE",
            new
            {
                div_seq = request.DivSeq,
                alm_sys_id = request.AlmSysId,
                next_alm_action_id = request.AlmActionId ?? "",
                create_user_id = request.UpdateUserId,
                reject_yn = "N"
            });

        return result ?? new UpdateAlarmResponseDto { Result = "FAIL", ResultMessage = "No response from stored procedure" };
    }

    #endregion

    #region B6 New Methods - Alarm History

    /// <inheritdoc />
    public async Task<AlarmHistoryResponseDto> GetHistoryAsync(
        AlarmHistoryQueryDto query,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            SELECT
                CAST(ROW_NUMBER() OVER (ORDER BY a.update_date DESC) AS VARCHAR(20)) AS hist_seq,
                a.alm_sys_id, a.alm_action_id,
                ac.code_name_k AS alm_action_name,
                '' AS prev_action_id, '' AS prev_action_name,
                '' AS action_type, '' AS action_type_name,
                a.description AS comment,
                CONVERT(VARCHAR(20), a.update_date, 120) AS create_date,
                a.update_user_id AS create_user_id,
                u.user_name AS create_user_name
            FROM SPC_ALARM a
            LEFT JOIN SPC_CODE_MST ac ON a.div_seq = ac.div_seq AND a.alm_action_id = ac.code_id AND ac.code_class_id = 'ALM_ACTION'
            LEFT JOIN SPC_USER_INFO u ON a.div_seq = u.div_seq AND a.update_user_id = u.user_id
            WHERE a.div_seq = @div_seq AND a.alm_sys_id = @alm_sys_id
            ORDER BY a.update_date DESC";

        try
        {
            var items = (await _connection.QueryAsync<AlarmHistoryItemDto>(sql, new
            {
                div_seq = query.DivSeq,
                alm_sys_id = query.AlmSysId
            })).ToList();

            return new AlarmHistoryResponseDto { Items = items, TotalCount = items.Count };
        }
        catch
        {
            return new AlarmHistoryResponseDto { Items = new List<AlarmHistoryItemDto>(), TotalCount = 0 };
        }
    }

    /// <inheritdoc />
    public async Task<AlarmStatisticsDto> GetStatisticsAsync(
        string divSeq,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default)
    {
        // Reuse USP_SPC_SPH4020_SELECT to get alarm list, then aggregate in C#
        var items = await QueryAsync<AlarmListItemDto>(
            "USP_SPC_SPH4020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                Search_Type = "Total",
                My_vendor_id = "",
                login_id = "",
                vendor_id = "",
                start_date = startDate ?? "",
                end_date = endDate ?? "",
                alm_sys_id = "",
                alm_action_id = "",
                alm_proc_id = "",
                mtrl_class_id = "",
                mtrl_id = "",
                act_prod_id = "",
                project_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                alm_proc_001_YN = "N",
                stop_YN = "N"
            });

        var list = items.ToList();
        var closedCount = list.Count(x => x.CurAlmActionId == "CLOSE" || x.CurAlmActionId == "CLOSED");
        var openCount = list.Count - closedCount;

        var byType = list.GroupBy(x => new { x.AlmProcId, x.AlmProcName })
            .Select(g => new AlarmStatsByTypeDto
            {
                AlmProcId = g.Key.AlmProcId,
                AlmProcName = g.Key.AlmProcName,
                Count = g.Count(),
                Percentage = list.Count > 0 ? Math.Round((double)g.Count() / list.Count * 100, 1) : 0
            }).ToList();

        var bySeverity = list.GroupBy(x => x.Severity)
            .Select(g => new AlarmStatsBySeverityDto
            {
                Severity = g.Key,
                SeverityName = g.Key,
                Count = g.Count(),
                Percentage = list.Count > 0 ? Math.Round((double)g.Count() / list.Count * 100, 1) : 0
            }).ToList();

        return new AlarmStatisticsDto
        {
            DivSeq = divSeq,
            Period = $"{startDate} ~ {endDate}",
            TotalCount = list.Count,
            OpenCount = openCount,
            ClosedCount = closedCount,
            InProgressCount = list.Count(x => x.CurAlmActionId == "ACCEPT" || x.CurAlmActionId == "ESCALATE"),
            OverdueCount = 0,
            AvgResolutionTime = 0,
            ByType = byType,
            BySeverity = bySeverity
        };
    }

    /// <inheritdoc />
    public async Task<AlarmTrendResponseDto> GetTrendAsync(
        AlarmTrendQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // Reuse USP_SPC_SPH4020_SELECT for alarm list, then group by date in C#
        var items = await QueryAsync<AlarmListItemDto>(
            "USP_SPC_SPH4020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                Search_Type = "Total",
                My_vendor_id = "",
                login_id = "",
                vendor_id = query.VendorId ?? "",
                start_date = query.StartDate,
                end_date = query.EndDate,
                alm_sys_id = "",
                alm_action_id = "",
                alm_proc_id = "",
                mtrl_class_id = query.MtrlClassId ?? "",
                mtrl_id = "",
                act_prod_id = "",
                project_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                alm_proc_001_YN = "N",
                stop_YN = "N"
            });

        var list = items.ToList();

        // Group by date portion based on GroupBy (DAY/WEEK/MONTH)
        var trendItems = list
            .GroupBy(x => query.GroupBy?.ToUpper() switch
            {
                "MONTH" => x.CreateDate.Length >= 7 ? x.CreateDate[..7] : x.CreateDate,
                "WEEK" => GetWeekKey(x.CreateDate),
                _ => x.CreateDate.Length >= 10 ? x.CreateDate[..10] : x.CreateDate // DAY default
            })
            .OrderBy(g => g.Key)
            .Select(g => new AlarmTrendDto
            {
                Date = g.Key,
                CreatedCount = g.Count(),
                ClosedCount = g.Count(x => x.CurAlmActionId == "CLOSE" || x.CurAlmActionId == "CLOSED"),
                OpenCount = g.Count(x => x.CurAlmActionId != "CLOSE" && x.CurAlmActionId != "CLOSED")
            })
            .ToList();

        var closedTotal = list.Count(x => x.CurAlmActionId == "CLOSE" || x.CurAlmActionId == "CLOSED");
        return new AlarmTrendResponseDto
        {
            Items = trendItems,
            Summary = new AlarmStatisticsDto
            {
                DivSeq = query.DivSeq,
                Period = $"{query.StartDate} ~ {query.EndDate}",
                TotalCount = list.Count,
                ClosedCount = closedTotal,
                OpenCount = list.Count - closedTotal
            }
        };
    }

    private static string GetWeekKey(string dateStr)
    {
        if (DateTime.TryParse(dateStr, out var dt))
        {
            var cal = System.Globalization.CultureInfo.InvariantCulture.Calendar;
            var week = cal.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return $"{dt.Year}-W{week:D2}";
        }
        return dateStr.Length >= 10 ? dateStr[..10] : dateStr;
    }

    #endregion

    #region B6 New Methods - Alarm Attachments

    /// <inheritdoc />
    public async Task<AlarmAttachmentListResponseDto> GetAttachmentsAsync(
        AlarmAttachmentQueryDto query,
        CancellationToken cancellationToken = default)
    {
        var items = await QueryAsync<AlarmAttachmentDto>(
            "USP_SPC_ALARM_ATTACH_LIST_SELECT",
            new
            {
                div_seq = query.DivSeq,
                alm_sys_id = query.AlmSysId
            });

        var itemList = items.ToList();

        return new AlarmAttachmentListResponseDto
        {
            Items = itemList,
            TotalCount = itemList.Count,
            TotalSize = itemList.Sum(x => x.FileSize)
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// No SP exists for individual attachment insert. Uses direct SQL INSERT.
    /// </remarks>
    public async Task<UploadAlarmAttachmentResponseDto> UploadAttachmentAsync(
        UploadAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            INSERT INTO SPC_ALARM_ATTACH_LIST
                (div_seq, alm_sys_id, file_name, original_file_name,
                 file_size, file_type, mime_type, file_content, use_yn, create_user_id, create_date)
            VALUES
                (@div_seq, @alm_sys_id, @file_name, @original_file_name,
                 @file_size, @file_type, @mime_type, @file_content, 'Y', @create_user_id, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() AS VARCHAR(40)) AS AttachSeq;";

        var attachSeq = await QueryFirstOrDefaultRawSqlAsync<string>(sql, new
        {
            div_seq = request.DivSeq,
            alm_sys_id = request.AlmSysId,
            file_name = request.FileName,
            original_file_name = request.OriginalFileName,
            file_size = request.FileSize,
            file_type = request.FileType,
            mime_type = request.MimeType,
            file_content = request.FileContent,
            create_user_id = request.CreateUserId
        });

        return new UploadAlarmAttachmentResponseDto
        {
            Result = "SUCCESS",
            ResultMessage = "Attachment uploaded successfully.",
            AttachSeq = attachSeq ?? ""
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// No SP exists for attachment delete. Uses direct SQL UPDATE (soft delete).
    /// </remarks>
    public async Task<DeleteAlarmAttachmentResponseDto> DeleteAttachmentAsync(
        DeleteAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            UPDATE SPC_ALARM_ATTACH_LIST
            SET use_yn = 'N', update_user_id = @user_id, update_date = GETDATE()
            WHERE div_seq = @div_seq AND alm_sys_id = @alm_sys_id AND attach_seq = @attach_seq";

        var affected = await ExecuteRawSqlAsync(sql, new
        {
            div_seq = request.DivSeq,
            alm_sys_id = request.AlmSysId,
            attach_seq = request.AttachSeq,
            user_id = request.UserId
        });

        return new DeleteAlarmAttachmentResponseDto
        {
            Result = affected > 0 ? "SUCCESS" : "FAIL",
            ResultMessage = affected > 0 ? "Attachment deleted successfully." : "Attachment not found."
        };
    }

    /// <inheritdoc />
    public async Task<DownloadAlarmAttachmentResponseDto?> DownloadAttachmentAsync(
        DownloadAlarmAttachmentRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            SELECT file_name AS FileName, mime_type AS MimeType, file_content AS FileContent
            FROM SPC_ALARM_ATTACH_LIST
            WHERE div_seq = @div_seq
              AND alm_sys_id = @alm_sys_id
              AND attach_seq = @attach_seq
              AND use_yn = 'Y'";

        return await QueryFirstOrDefaultRawSqlAsync<DownloadAlarmAttachmentResponseDto>(sql, new
        {
            div_seq = request.DivSeq,
            alm_sys_id = request.AlmSysId,
            attach_seq = request.AttachSeq
        });
    }

    #endregion

    #region B6 New Methods - Alarm Actions

    /// <inheritdoc />
    public async Task<ExecuteAlarmActionResponseDto> ExecuteActionAsync(
        ExecuteAlarmActionRequestDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await QueryFirstOrDefaultAsync<dynamic>(
                "USP_SPC_SPH4020_UPDATE",
                new
                {
                    div_seq = request.DivSeq,
                    alm_sys_id = request.AlmSysId,
                    next_alm_action_id = request.AlmActionId,
                    create_user_id = request.UserId,
                    reject_yn = "N"
                });

            return new ExecuteAlarmActionResponseDto
            {
                Result = "SUCCESS",
                ResultMessage = "Action executed successfully.",
                AlmSysId = request.AlmSysId,
                NewActionId = request.AlmActionId,
                NewActionName = request.AlmActionId
            };
        }
        catch (Exception ex)
        {
            return new ExecuteAlarmActionResponseDto
            {
                Result = "FAIL",
                ResultMessage = ex.Message,
                AlmSysId = request.AlmSysId
            };
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH4020_MERGE
    /// DB params: @P_div_seq VARCHAR(40), @P_alm_sys_id VARCHAR(40), @P_merge_alm_id VARCHAR(MAX) DEFAULT '',
    ///            @P_create_user_id VARCHAR(40), @P_merge_yn CHAR(1)
    /// Note: merge_reason removed - DB USP does not have this parameter.
    /// </remarks>
    public async Task<MergeAlarmResponseDto> MergeAlarmsAsync(
        MergeAlarmRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var almSysIdsCsv = string.Join(",", request.AlmSysIds);

        var result = await QueryFirstOrDefaultAsync<MergeAlarmResponseDto>(
            "USP_SPC_SPH4020_MERGE",
            new
            {
                div_seq = request.DivSeq,
                alm_sys_id = request.TargetAlmSysId,    // target becomes primary
                merge_alm_id = almSysIdsCsv,             // list of IDs to merge
                create_user_id = request.UserId,
                merge_yn = "Y"
            });

        return result ?? new MergeAlarmResponseDto { Result = "FAIL", ResultMessage = "Merge failed" };
    }

    #endregion
}
