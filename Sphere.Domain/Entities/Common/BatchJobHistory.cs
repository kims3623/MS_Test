using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Batch job history entity for tracking job execution history.
/// Maps to SPC_BATCH_JOB_HIST table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + HistoryId
/// Records detailed execution metrics including counts and timing.
/// Maintains relationship with BatchJob for job identification.
/// </remarks>
public class BatchJobHistory : SphereEntity
{
    /// <summary>
    /// History record identifier (PK)
    /// </summary>
    public string HistoryId { get; set; } = string.Empty;

    /// <summary>
    /// Related batch job identifier (FK)
    /// </summary>
    public string JobId { get; set; } = string.Empty;

    /// <summary>
    /// Job name at execution time
    /// </summary>
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// Execution start timestamp
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Execution end timestamp
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Execution status (RUNNING, COMPLETED, FAILED, CANCELLED)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Execution result summary
    /// </summary>
    public string Result { get; set; } = string.Empty;

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Total records processed
    /// </summary>
    public int ProcessedCount { get; set; }

    /// <summary>
    /// Successfully processed records
    /// </summary>
    public int SuccessCount { get; set; }

    /// <summary>
    /// Failed records count
    /// </summary>
    public int FailCount { get; set; }
}
