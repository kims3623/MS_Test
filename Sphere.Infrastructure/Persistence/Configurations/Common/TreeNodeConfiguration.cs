using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for TreeNode entity.
/// </summary>
public class TreeNodeConfiguration : IEntityTypeConfiguration<TreeNode>
{
    public void Configure(EntityTypeBuilder<TreeNode> builder)
    {
        builder.ToTable("SPC_TREE_NODE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.NodeId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.NodeId)
            .HasColumnName("node_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ParentNodeId)
            .HasColumnName("parent_node_id")
            .HasMaxLength(40);

        builder.Property(e => e.NodeName)
            .HasColumnName("node_name")
            .HasMaxLength(200);

        builder.Property(e => e.NodeNameK)
            .HasColumnName("node_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.NodeNameE)
            .HasColumnName("node_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.NodeType)
            .HasColumnName("node_type")
            .HasMaxLength(50);

        builder.Property(e => e.NodeLevel)
            .HasColumnName("node_level")
            .HasDefaultValue(0);

        builder.Property(e => e.NodeSeq)
            .HasColumnName("node_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.NodePath)
            .HasColumnName("node_path")
            .HasMaxLength(500);

        builder.Property(e => e.DataJson)
            .HasColumnName("data_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.ExpandedYn)
            .HasColumnName("expanded_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.RowStatus)
            .HasColumnName("ROW_STATUS")
            .HasMaxLength(10);

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.CreateUserId)
            .HasColumnName("create_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.CreateDate)
            .HasColumnName("create_date");

        builder.Property(e => e.UpdateUserId)
            .HasColumnName("update_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.UpdateDate)
            .HasColumnName("update_date");

        // Indexes
        builder.HasIndex(e => new { e.DivSeq, e.ParentNodeId })
            .HasDatabaseName("IX_TreeNode_DivSeq_ParentNodeId");

        builder.HasIndex(e => e.NodePath)
            .HasDatabaseName("IX_TreeNode_NodePath");
    }
}
