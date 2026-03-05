using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Tree node entity for hierarchical data structures.
/// Maps to SPC_TREE_NODE table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + NodeId
/// Supports multi-level tree structures with parent-child relationships.
/// Stores additional node data as JSON for flexibility.
/// </remarks>
public class TreeNode : SphereEntity
{
    /// <summary>
    /// Node identifier (PK)
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Parent node identifier for tree structure
    /// </summary>
    public string ParentNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Node display name (default locale)
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node name in Korean
    /// </summary>
    public string NodeNameK { get; set; } = string.Empty;

    /// <summary>
    /// Node name in English
    /// </summary>
    public string NodeNameE { get; set; } = string.Empty;

    /// <summary>
    /// Node type classification
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Node depth level in tree (0=root)
    /// </summary>
    public int NodeLevel { get; set; }

    /// <summary>
    /// Display sequence among siblings
    /// </summary>
    public int NodeSeq { get; set; }

    /// <summary>
    /// Materialized path for efficient queries (e.g., /1/3/5/)
    /// </summary>
    public string NodePath { get; set; } = string.Empty;

    /// <summary>
    /// Additional node data as JSON
    /// </summary>
    public string DataJson { get; set; } = string.Empty;

    /// <summary>
    /// Default expanded state flag (Y/N)
    /// </summary>
    public string ExpandedYn { get; set; } = "N";
}
