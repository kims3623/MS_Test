using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Approval;

namespace Sphere.Infrastructure.Persistence.Configurations.Approval;

/// <summary>
/// EF Core configuration for ApprovalHistory entity.
/// </summary>
public class ApprovalHistoryConfiguration : IEntityTypeConfiguration<ApprovalHistory>
{
    public void Configure(EntityTypeBuilder<ApprovalHistory> builder)
    {
        builder.ToTable("SPC_APPROVAL_HISTORY");

        // Composite Primary Key - HistSeq is the unique identifier per Approval
        builder.HasKey(e => new { e.DivSeq, e.AprovId, e.HistSeq });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.HistSeq)
            .HasColumnName("hist_seq")
            .IsRequired();

        builder.Property(e => e.AprovId)
            .HasColumnName("aprov_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ApproverId)
            .HasColumnName("approver_id")
            .HasMaxLength(50);

        builder.Property(e => e.ApproverName)
            .HasColumnName("approver_name")
            .HasMaxLength(100);

        builder.Property(e => e.Action)
            .HasColumnName("action")
            .HasMaxLength(40);

        builder.Property(e => e.ActionName)
            .HasColumnName("action_name")
            .HasMaxLength(100);

        builder.Property(e => e.PrevState)
            .HasColumnName("prev_state")
            .HasMaxLength(20);

        builder.Property(e => e.NewState)
            .HasColumnName("new_state")
            .HasMaxLength(20);

        builder.Property(e => e.Comment)
            .HasColumnName("comment")
            .HasMaxLength(2000);

        builder.Property(e => e.ActionDate)
            .HasColumnName("action_date");

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.RowStatus)
            .HasColumnName("ROW_STATUS")
            .HasMaxLength(10);

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
        builder.HasIndex(e => new { e.DivSeq, e.AprovId })
            .HasDatabaseName("IX_ApprovalHistory_DivSeq_AprovId");

        builder.HasIndex(e => new { e.DivSeq, e.ApproverId })
            .HasDatabaseName("IX_ApprovalHistory_DivSeq_ApproverId");

        builder.HasIndex(e => e.ActionDate)
            .HasDatabaseName("IX_ApprovalHistory_ActionDate");

        builder.HasIndex(e => new { e.DivSeq, e.Action })
            .HasDatabaseName("IX_ApprovalHistory_DivSeq_Action");
    }
}
