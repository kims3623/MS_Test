using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3050Histogram entity.
/// Maps to SPC_SPH3050_HISTOGRAM table for histogram chart data.
/// </summary>
public class SPH3050HistogramConfiguration : IEntityTypeConfiguration<SPH3050Histogram>
{
    public void Configure(EntityTypeBuilder<SPH3050Histogram> builder)
    {
        builder.ToTable("SPC_SPH3050_HISTOGRAM");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.PeriodFrom)
            .HasColumnName("period_from")
            .HasMaxLength(10);

        builder.Property(e => e.PeriodTo)
            .HasColumnName("period_to")
            .HasMaxLength(10);

        builder.Property(e => e.BinNo)
            .HasColumnName("bin_no")
            .HasDefaultValue(0);

        builder.Property(e => e.BinLower)
            .HasColumnName("bin_lower")
            .HasPrecision(18, 6);

        builder.Property(e => e.BinUpper)
            .HasColumnName("bin_upper")
            .HasPrecision(18, 6);

        builder.Property(e => e.Frequency)
            .HasColumnName("frequency")
            .HasDefaultValue(0);

        builder.Property(e => e.RelativeFrequency)
            .HasColumnName("relative_frequency")
            .HasPrecision(18, 4);

        builder.Property(e => e.CumulativeFrequency)
            .HasColumnName("cumulative_frequency")
            .HasDefaultValue(0);

        builder.Property(e => e.TotalCount)
            .HasColumnName("total_count")
            .HasDefaultValue(0);

        builder.Property(e => e.Mean)
            .HasColumnName("mean")
            .HasPrecision(18, 6);

        builder.Property(e => e.StdDev)
            .HasColumnName("std_dev")
            .HasPrecision(18, 6);

        // Base entity properties
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
        builder.HasIndex(e => new { e.DivSeq, e.PeriodFrom, e.PeriodTo })
            .HasDatabaseName("IX_SPH3050Histogram_DivSeq_Period");

        builder.HasIndex(e => e.BinNo)
            .HasDatabaseName("IX_SPH3050Histogram_BinNo");
    }
}
