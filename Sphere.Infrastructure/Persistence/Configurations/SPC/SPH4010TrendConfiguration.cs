using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH4010Trend entity.
/// Maps to SPC_SPH4010_TREND table for trend analysis data.
/// </summary>
public class SPH4010TrendConfiguration : IEntityTypeConfiguration<SPH4010Trend>
{
    public void Configure(EntityTypeBuilder<SPH4010Trend> builder)
    {
        builder.ToTable("SPC_SPH4010_TREND");

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

        builder.Property(e => e.Period)
            .HasColumnName("period")
            .HasMaxLength(10);

        builder.Property(e => e.PeriodType)
            .HasColumnName("period_type")
            .HasMaxLength(10)
            .HasDefaultValue("DAY");

        builder.Property(e => e.AvgValue)
            .HasColumnName("avg_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.MinValue)
            .HasColumnName("min_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.MaxValue)
            .HasColumnName("max_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.StdDev)
            .HasColumnName("std_dev")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cpk)
            .HasColumnName("cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.SampleCount)
            .HasColumnName("sample_count")
            .HasDefaultValue(0);

        builder.Property(e => e.OosCount)
            .HasColumnName("oos_count")
            .HasDefaultValue(0);

        builder.Property(e => e.TrendSlope)
            .HasColumnName("trend_slope")
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
        builder.HasIndex(e => new { e.DivSeq, e.Period })
            .HasDatabaseName("IX_SPH4010Trend_DivSeq_Period");

        builder.HasIndex(e => e.PeriodType)
            .HasDatabaseName("IX_SPH4010Trend_PeriodType");
    }
}
