using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for AuditLog entity.
/// </summary>
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("SPC_AUDIT_LOG");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.LogId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.LogId)
            .HasColumnName("log_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(100);

        builder.Property(e => e.Action)
            .HasColumnName("action")
            .HasMaxLength(50);

        builder.Property(e => e.EntityType)
            .HasColumnName("entity_type")
            .HasMaxLength(100);

        builder.Property(e => e.EntityId)
            .HasColumnName("entity_id")
            .HasMaxLength(100);

        builder.Property(e => e.OldValue)
            .HasColumnName("old_value")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.NewValue)
            .HasColumnName("new_value")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50);

        builder.Property(e => e.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);

        builder.Property(e => e.ActionDate)
            .HasColumnName("action_date");

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId, e.ActionDate })
            .HasDatabaseName("IX_AuditLog_DivSeq_UserId_ActionDate");

        builder.HasIndex(e => new { e.EntityType, e.EntityId })
            .HasDatabaseName("IX_AuditLog_EntityType_EntityId");

        builder.HasIndex(e => e.ActionDate)
            .HasDatabaseName("IX_AuditLog_ActionDate");
    }
}
