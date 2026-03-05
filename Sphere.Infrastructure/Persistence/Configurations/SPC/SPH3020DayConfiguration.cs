using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3020Day entity.
/// Maps to SPC_SPH3020_DAY table for daily aggregated data.
/// </summary>
public class SPH3020DayConfiguration : IEntityTypeConfiguration<SPH3020Day>
{
    public void Configure(EntityTypeBuilder<SPH3020Day> builder)
    {
        builder.ToTable("SPC_SPH3020_DAY");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.WorkDate });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.SampleCount)
            .HasColumnName("sample_count")
            .HasDefaultValue(0);

        builder.Property(e => e.DailyAvg)
            .HasColumnName("daily_avg")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyMin)
            .HasColumnName("daily_min")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyMax)
            .HasColumnName("daily_max")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyRange)
            .HasColumnName("daily_range")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyStdDev)
            .HasColumnName("daily_std_dev")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyCp)
            .HasColumnName("daily_cp")
            .HasPrecision(18, 6);

        builder.Property(e => e.DailyCpk)
            .HasColumnName("daily_cpk")
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
        builder.HasIndex(e => new { e.DivSeq, e.WorkDate })
            .HasDatabaseName("IX_SPH3020Day_DivSeq_WorkDate");

        builder.HasIndex(e => e.AlarmCount)
            .HasDatabaseName("IX_SPH3020Day_AlarmCount");
    }
}
