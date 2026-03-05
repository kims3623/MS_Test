using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for RolePermission entity.
/// </summary>
public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("SPC_ROLE_PERMISSION");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.RoleCode, e.PermissionCode });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RoleCode)
            .HasColumnName("role_code")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.PermissionCode)
            .HasColumnName("permission_code")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.PermissionName)
            .HasColumnName("permission_name")
            .HasMaxLength(100);

        builder.Property(e => e.ResourceType)
            .HasColumnName("resource_type")
            .HasMaxLength(40);

        builder.Property(e => e.ResourceId)
            .HasColumnName("resource_id")
            .HasMaxLength(100);

        builder.Property(e => e.ActionType)
            .HasColumnName("action_type")
            .HasMaxLength(20)
            .HasDefaultValue("READ");

        builder.Property(e => e.GrantedYn)
            .HasColumnName("granted_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

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
        builder.HasIndex(e => new { e.DivSeq, e.RoleCode })
            .HasDatabaseName("IX_RolePermission_DivSeq_RoleCode");

        builder.HasIndex(e => new { e.DivSeq, e.ResourceType, e.ResourceId })
            .HasDatabaseName("IX_RolePermission_DivSeq_Resource");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_RolePermission_UseYn");
    }
}
