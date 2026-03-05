using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Filter entity.
/// </summary>
public class FilterConfiguration : IEntityTypeConfiguration<Filter>
{
    public void Configure(EntityTypeBuilder<Filter> builder)
    {
        builder.ToTable("SPC_FILTER");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.FilterId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FilterId)
            .HasColumnName("filter_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FilterName)
            .HasColumnName("filter_name")
            .HasMaxLength(200);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.ScreenId)
            .HasColumnName("screen_id")
            .HasMaxLength(50);

        builder.Property(e => e.FilterJson)
            .HasColumnName("filter_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.PublicYn)
            .HasColumnName("public_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.DefaultYn)
            .HasColumnName("default_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId, e.ScreenId })
            .HasDatabaseName("IX_Filter_DivSeq_UserId_ScreenId");

        builder.HasIndex(e => e.PublicYn)
            .HasDatabaseName("IX_Filter_PublicYn");
    }
}
