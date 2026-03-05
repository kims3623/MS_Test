using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Pagination entity.
/// </summary>
public class PaginationConfiguration : IEntityTypeConfiguration<Pagination>
{
    public void Configure(EntityTypeBuilder<Pagination> builder)
    {
        builder.ToTable("SPC_PAGINATION");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ConfigId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ConfigId)
            .HasColumnName("config_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.ScreenId)
            .HasColumnName("screen_id")
            .HasMaxLength(40);

        builder.Property(e => e.PageSize)
            .HasColumnName("page_size")
            .HasDefaultValue(20);

        builder.Property(e => e.PageSizeOptions)
            .HasColumnName("page_size_options")
            .HasMaxLength(100)
            .HasDefaultValue("10,20,50,100");

        builder.Property(e => e.ShowTotal)
            .HasColumnName("show_total")
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
        builder.HasIndex(e => new { e.DivSeq, e.UserId, e.ScreenId })
            .HasDatabaseName("IX_Pagination_DivSeq_UserId_ScreenId");
    }
}
