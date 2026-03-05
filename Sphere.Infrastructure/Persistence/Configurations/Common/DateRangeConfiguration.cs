using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for DateRange entity.
/// </summary>
public class DateRangeConfiguration : IEntityTypeConfiguration<DateRange>
{
    public void Configure(EntityTypeBuilder<DateRange> builder)
    {
        builder.ToTable("SPC_DATE_RANGE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.RangeId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RangeId)
            .HasColumnName("range_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RangeName)
            .HasColumnName("range_name")
            .HasMaxLength(100);

        builder.Property(e => e.RangeNameK)
            .HasColumnName("range_name_k")
            .HasMaxLength(100);

        builder.Property(e => e.RangeNameE)
            .HasColumnName("range_name_e")
            .HasMaxLength(100);

        builder.Property(e => e.RangeType)
            .HasColumnName("range_type")
            .HasMaxLength(20);

        builder.Property(e => e.DaysBack)
            .HasColumnName("days_back")
            .HasDefaultValue(0);

        builder.Property(e => e.DaysForward)
            .HasColumnName("days_forward")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.RangeType })
            .HasDatabaseName("IX_DateRange_DivSeq_RangeType");
    }
}
