using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmNotification entity.
/// Maps to SPC_ALM_NOTIFICATION table.
/// </summary>
public class AlarmNotificationConfiguration : IEntityTypeConfiguration<AlarmNotification>
{
    public void Configure(EntityTypeBuilder<AlarmNotification> builder)
    {
        builder.ToTable("SPC_ALM_NOTIFICATION");

        // Single Primary Key (NotiId)
        builder.HasKey(e => e.NotiId);

        // Column mappings
        builder.Property(e => e.NotiId)
            .HasColumnName("noti_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(100);

        builder.Property(e => e.NotiType)
            .HasColumnName("noti_type")
            .HasMaxLength(20)
            .HasDefaultValue("EMAIL");

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("PENDING");

        builder.Property(e => e.SentDate)
            .HasColumnName("sent_date");

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(2000);

        builder.Property(e => e.RetryCount)
            .HasColumnName("retry_count")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => e.AlmSysId)
            .HasDatabaseName("IX_AlarmNotification_AlmSysId");

        builder.HasIndex(e => e.UserId)
            .HasDatabaseName("IX_AlarmNotification_UserId");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_AlarmNotification_Status");

        builder.HasIndex(e => e.SentDate)
            .HasDatabaseName("IX_AlarmNotification_SentDate");
    }
}
