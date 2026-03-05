using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for Authority entity.
/// </summary>
public class AuthorityConfiguration : IEntityTypeConfiguration<Authority>
{
    public void Configure(EntityTypeBuilder<Authority> builder)
    {
        builder.ToTable("SPC_AUTHORITY");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.UserId, e.MenuId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.AuthType)
            .HasColumnName("auth_type")
            .HasMaxLength(40);

        builder.Property(e => e.AuthTypeName)
            .HasColumnName("auth_type_name")
            .HasMaxLength(100);

        builder.Property(e => e.MenuId)
            .HasColumnName("menu_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MenuName)
            .HasColumnName("menu_name")
            .HasMaxLength(100);

        builder.Property(e => e.CanRead)
            .HasColumnName("can_read")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.CanWrite)
            .HasColumnName("can_write")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.CanDelete)
            .HasColumnName("can_delete")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.CanExport)
            .HasColumnName("can_export")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.CanAdmin)
            .HasColumnName("can_admin")
            .HasMaxLength(1)
            .HasDefaultValue("N");

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_Authority_DivSeq_UserId");

        builder.HasIndex(e => new { e.DivSeq, e.MenuId })
            .HasDatabaseName("IX_Authority_DivSeq_MenuId");

        builder.HasIndex(e => new { e.DivSeq, e.AuthType })
            .HasDatabaseName("IX_Authority_DivSeq_AuthType");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Authority_UseYn");
    }
}
