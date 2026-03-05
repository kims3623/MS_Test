using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for UserFavorite entity.
/// Maps to SPC_USER_FAVORITE table.
/// </summary>
public class UserFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
{
    public void Configure(EntityTypeBuilder<UserFavorite> builder)
    {
        builder.ToTable("SPC_USER_FAVORITE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.UserId, e.MenuId });

        // Column mappings
        builder.Property(e => e.TableSysId)
            .HasColumnName("table_sys_id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.MenuId)
            .HasColumnName("menu_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.AddedDate)
            .HasColumnName("added_date");

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_UserFavorite_DivSeq_UserId");

        builder.HasIndex(e => new { e.DivSeq, e.UserId, e.UseYn })
            .HasDatabaseName("IX_UserFavorite_DivSeq_UserId_UseYn");

        builder.HasIndex(e => e.TableSysId)
            .HasDatabaseName("IX_UserFavorite_TableSysId")
            .IsUnique();

        // Relationship with Menu (optional)
        builder.HasOne(e => e.Menu)
            .WithMany()
            .HasForeignKey(e => new { e.DivSeq, e.MenuId })
            .HasPrincipalKey(m => new { m.DivSeq, m.MenuId })
            .OnDelete(DeleteBehavior.Restrict);
    }
}
