using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3020Month entity.
/// Maps to SPC_SPH3020_MONTH table for monthly aggregated data.
/// </summary>
public class SPH3020MonthConfiguration : IEntityTypeConfiguration<SPH3020Month>
{
    public void Configure(EntityTypeBuilder<SPH3020Month> builder)
    {
        builder.ToTable("SPC_SPH3020_MONTH");

        // Composite Primary Key (DivSeq + SpecSysId + Year + Month as YearMonth)
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.Year, e.Month });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.Year)
            .HasColumnName("year")
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(e => e.Month)
            .HasColumnName("month")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(e => e.SampleCount)
            .HasColumnName("sample_count")
            .HasDefaultValue(0);

        builder.Property(e => e.MonthlyAvg)
            .HasColumnName("monthly_avg")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyMin)
            .HasColumnName("monthly_min")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyMax)
            .HasColumnName("monthly_max")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyRange)
            .HasColumnName("monthly_range")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyStdDev)
            .HasColumnName("monthly_std_dev")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyCp)
            .HasColumnName("monthly_cp")
            .HasPrecision(18, 6);

        builder.Property(e => e.MonthlyCpk)
            .HasColumnName("monthly_cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.OosCount)
            .HasColumnName("oos_count")
            .HasDefaultValue(0);

        builder.Property(e => e.AlarmCount)
            .HasColumnName("alarm_count")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.Year, e.Month })
            .HasDatabaseName("IX_SPH3020Month_DivSeq_Year_Month");

        builder.HasIndex(e => e.AlarmCount)
            .HasDatabaseName("IX_SPH3020Month_AlarmCount");
    }
}
