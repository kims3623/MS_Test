using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3070Cpk entity.
/// Maps to SPC_SPH3070_CPK table for Cpk trend chart data.
/// </summary>
public class SPH3070CpkConfiguration : IEntityTypeConfiguration<SPH3070Cpk>
{
    public void Configure(EntityTypeBuilder<SPH3070Cpk> builder)
    {
        builder.ToTable("SPC_SPH3070_CPK");

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

        builder.Property(e => e.SampleCount)
            .HasColumnName("sample_count")
            .HasDefaultValue(0);

        builder.Property(e => e.Cp)
            .HasColumnName("cp")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cpk)
            .HasColumnName("cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cpu)
            .HasColumnName("cpu")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cpl)
            .HasColumnName("cpl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Pp)
            .HasColumnName("pp")
            .HasPrecision(18, 6);

        builder.Property(e => e.Ppk)
            .HasColumnName("ppk")
            .HasPrecision(18, 6);

        builder.Property(e => e.Mean)
            .HasColumnName("mean")
            .HasPrecision(18, 6);

        builder.Property(e => e.StdDev)
            .HasColumnName("std_dev")
            .HasPrecision(18, 6);

        builder.Property(e => e.TargetCpk)
            .HasColumnName("target_cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.PassYn)
            .HasColumnName("pass_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

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
            .HasDatabaseName("IX_SPH3070Cpk_DivSeq_Period");

        builder.HasIndex(e => e.PeriodType)
            .HasDatabaseName("IX_SPH3070Cpk_PeriodType");

        builder.HasIndex(e => e.PassYn)
            .HasDatabaseName("IX_SPH3070Cpk_PassYn");
    }
}
