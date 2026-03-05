using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Menu entity.
/// </summary>
public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("SPC_MENU");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.MenuId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MenuId)
            .HasColumnName("menu_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MenuName)
            .HasColumnName("menu_name")
            .HasMaxLength(200);

        builder.Property(e => e.MenuNameK)
            .HasColumnName("menu_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.MenuNameE)
            .HasColumnName("menu_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.MenuNameC)
            .HasColumnName("menu_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.MenuNameV)
            .HasColumnName("menu_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.ParentMenuId)
            .HasColumnName("parent_menu_id")
            .HasMaxLength(40);

        builder.Property(e => e.MenuLevel)
            .HasColumnName("menu_level")
            .HasDefaultValue(1);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.MenuUrl)
            .HasColumnName("menu_url")
            .HasMaxLength(500);

        builder.Property(e => e.Icon)
            .HasColumnName("icon")
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.ParentMenuId })
            .HasDatabaseName("IX_Menu_DivSeq_ParentMenuId");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Menu_UseYn");
    }
}
