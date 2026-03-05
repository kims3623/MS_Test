using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Batch job entity for managing scheduled background jobs.
/// Maps to SPC_BATCH_JOB table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + JobId
/// Defines job schedules using cron expressions and tracks execution status.
/// Supports retry logic with configurable retry counts.
/// </remarks>
public class BatchJob : SphereEntity
{
    /// <summary>
    /// Batch job identifier (PK)
    /// </summary>
    public string JobId { get; set; } = string.Empty;

    /// <summary>
    /// Job display name
    /// </summary>
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// Job type classification
    /// </summary>
    public string JobType { get; set; } = string.Empty;

    /// <summary>
    /// Cron expression for scheduling
    /// </summary>
    public string CronExpression { get; set; } = string.Empty;

    /// <summary>
    /// Job status (ACTIVE, INACTIVE, PAUSED)
    /// </summary>
    public string Status { get; set; } = "ACTIVE";

    /// <summary>
    /// Last execution timestamp
    /// </summary>
    public DateTime? LastRunDate { get; set; }

    /// <summary>
    /// Next scheduled execution timestamp
    /// </summary>
    public DateTime? NextRunDate { get; set; }

    /// <summary>
    /// Last execution result (SUCCESS, FAILED, PARTIAL)
    /// </summary>
    public string LastResult { get; set; } = string.Empty;

    /// <summary>
    /// Job parameters as JSON
    /// </summary>
    public string Parameters { get; set; } = string.Empty;

    /// <summary>
    /// Maximum retry attempts on failure
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// Job description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
