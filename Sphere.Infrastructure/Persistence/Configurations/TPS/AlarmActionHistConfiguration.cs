using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmActionHist entity.
/// Maps to SPC_ALM_ACTION_HIST table.
/// </summary>
public class AlarmActionHistConfiguration : IEntityTypeConfiguration<AlarmActionHist>
{
    public void Configure(EntityTypeBuilder<AlarmActionHist> builder)
    {
        builder.ToTable("SPC_ALM_ACTION_HIST");

        // Composite Primary Key (AlmSysId, AlmActionId mapped to ActionId, HistSeq)
        builder.HasKey(e => new { e.AlmSysId, e.ActionId, e.HistSeq });

        // Column mappings
        builder.Property(e => e.HistSeq)
            .HasColumnName("hist_seq")
            .IsRequired();

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActionId)
            .HasColumnName("alm_action_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActionName)
            .HasColumnName("action_name")
            .HasMaxLength(100);

        builder.Property(e => e.ActionType)
            .HasColumnName("action_type")
            .HasMaxLength(50);

        builder.Property(e => e.PrevStatus)
            .HasColumnName("prev_status")
            .HasMaxLength(20);

        builder.Property(e => e.NewStatus)
            .HasColumnName("new_status")
            .HasMaxLength(20);

        builder.Property(e => e.ActionUserId)
            .HasColumnName("action_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.ActionUserName)
            .HasColumnName("action_user_name")
            .HasMaxLength(100);

        builder.Property(e => e.ActionDate)
            .HasColumnName("action_date");

        builder.Property(e => e.Comment)
            .HasColumnName("comment")
            .HasMaxLength(2000);

        builder.Property(e => e.RootCause)
            .HasColumnName("root_cause")
            .HasMaxLength(2000);

        builder.Property(e => e.CorrectiveAction)
            .HasColumnName("corrective_action")
            .HasMaxLength(2000);

        // Base entity properties
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

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
        builder.HasIndex(e => new { e.AlmSysId, e.ActionDate })
            .HasDatabaseName("IX_AlarmActionHist_AlmSysId_ActionDate");

        builder.HasIndex(e => e.ActionType)
            .HasDatabaseName("IX_AlarmActionHist_ActionType");

        builder.HasIndex(e => e.ActionUserId)
            .HasDatabaseName("IX_AlarmActionHist_ActionUserId");
    }
}
