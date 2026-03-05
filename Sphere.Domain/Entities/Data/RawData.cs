using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Raw data entity mapping to SPC_RAWDATA table.
/// DB PK: (table_sys_id IDENTITY, update_date) — partitioned table.
/// </summary>
public class RawData : SphereEntity
{
    /// <summary>
    /// Auto-increment primary key (IDENTITY)
    /// </summary>
    public long TableSysId { get; set; }

    /// <summary>
    /// Spec system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Work date (varchar(20))
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift identifier (varchar(40))
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// Measurement count (varchar(40), NOT NULL)
    /// </summary>
    public string MesmCnt { get; set; } = string.Empty;

    /// <summary>
    /// Sample identifier
    /// </summary>
    public string? SplId { get; set; }

    /// <summary>
    /// Lot name
    /// </summary>
    public string? LotName { get; set; }

    /// <summary>
    /// Stage
    /// </summary>
    public string? Stage { get; set; }

    /// <summary>
    /// Frequency
    /// </summary>
    public string? Frequency { get; set; }

    /// <summary>
    /// NG code
    /// </summary>
    public string? NgCode { get; set; }

    /// <summary>
    /// Equipment identifier
    /// </summary>
    public string? EqpId { get; set; }

    /// <summary>
    /// Raw data value (varchar(40))
    /// </summary>
    public string? RawDataValue { get; set; }

    /// <summary>
    /// Action name
    /// </summary>
    public string? ActiName { get; set; }

    /// <summary>
    /// Original action name
    /// </summary>
    public string? OriginActiName { get; set; }

    /// <summary>
    /// Reason code
    /// </summary>
    public string? ReasonCode { get; set; }

    /// <summary>
    /// Description (varchar(4000))
    /// </summary>
    public string? Description { get; set; }
}
