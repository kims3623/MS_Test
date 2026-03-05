using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Notification entity.
/// </summary>
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("SPC_NOTIFICATION");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.NotiId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.NotiId)
            .HasColumnName("noti_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.NotiType)
            .HasColumnName("noti_type")
            .HasMaxLength(20);

        builder.Property(e => e.Title)
            .HasColumnName("title")
            .HasMaxLength(200);

        builder.Property(e => e.Message)
            .HasColumnName("message")
            .HasMaxLength(2000);

        builder.Property(e => e.RefType)
            .HasColumnName("ref_type")
            .HasMaxLength(50);

        builder.Property(e => e.RefId)
            .HasColumnName("ref_id")
            .HasMaxLength(100);

        builder.Property(e => e.ReadYn)
            .HasColumnName("read_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ReadDate)
            .HasColumnName("read_date");

        builder.Property(e => e.SentDate)
            .HasColumnName("sent_date");

        builder.Property(e => e.Priority)
            .HasColumnName("priority")
            .HasDefaultValue(2);

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId, e.ReadYn })
            .HasDatabaseName("IX_Notification_DivSeq_UserId_ReadYn");

        builder.HasIndex(e => e.SentDate)
            .HasDatabaseName("IX_Notification_SentDate");
    }
}
