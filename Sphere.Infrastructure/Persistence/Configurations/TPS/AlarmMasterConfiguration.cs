using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmMaster entity.
/// Maps to SPC_ALARM table.
/// </summary>
public class AlarmMasterConfiguration : IEntityTypeConfiguration<AlarmMaster>
{
    public void Configure(EntityTypeBuilder<AlarmMaster> builder)
    {
        builder.ToTable("SPC_ALARM");

        // Composite Primary Key (DivSeq, AlmSysId)
        builder.HasKey(e => new { e.DivSeq, e.AlmSysId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AlmType)
            .HasColumnName("alm_type")
            .HasMaxLength(20);

        builder.Property(e => e.AlmTypeName)
            .HasColumnName("alm_type_name")
            .HasMaxLength(100);

        builder.Property(e => e.Severity)
            .HasColumnName("severity")
            .HasDefaultValue(1);

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.TriggerCondition)
            .HasColumnName("trigger_condition")
            .HasMaxLength(500);

        builder.Property(e => e.ThresholdValue)
            .HasColumnName("threshold_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("OPEN");

        builder.Property(e => e.TriggeredDate)
            .HasColumnName("triggered_date");

        builder.Property(e => e.AcknowledgedDate)
            .HasColumnName("acknowledged_date");

        builder.Property(e => e.ResolvedDate)
            .HasColumnName("resolved_date");

        builder.Property(e => e.AssignedUserId)
            .HasColumnName("assigned_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.AssignedUserName)
            .HasColumnName("assigned_user_name")
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

        // Base entity properties
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
        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_AlarmMaster_Status");

        builder.HasIndex(e => e.Severity)
            .HasDatabaseName("IX_AlarmMaster_Severity");

        builder.HasIndex(e => e.SpecSysId)
            .HasDatabaseName("IX_AlarmMaster_SpecSysId");

        builder.HasIndex(e => e.TriggeredDate)
            .HasDatabaseName("IX_AlarmMaster_TriggeredDate");

        builder.HasIndex(e => e.AssignedUserId)
            .HasDatabaseName("IX_AlarmMaster_AssignedUserId");
    }
}
