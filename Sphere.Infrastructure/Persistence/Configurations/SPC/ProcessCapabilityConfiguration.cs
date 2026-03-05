using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for ProcessCapability entity.
/// Maps to SPC_PROCESS_CAPABILITY table for capability study results.
/// </summary>
public class ProcessCapabilityConfiguration : IEntityTypeConfiguration<ProcessCapability>
{
    public void Configure(EntityTypeBuilder<ProcessCapability> builder)
    {
        builder.ToTable("SPC_PROCESS_CAPABILITY");

        // Primary Key (using StudyId as the unique identifier since table shows CapabilityId)
        builder.HasKey(e => new { e.DivSeq, e.StudyId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.StudyId)
            .HasColumnName("study_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.PeriodFrom)
            .HasColumnName("period_from")
            .HasMaxLength(10);

        builder.Property(e => e.PeriodTo)
            .HasColumnName("period_to")
            .HasMaxLength(10);

        builder.Property(e => e.SampleSize)
            .HasColumnName("sample_size")
            .HasDefaultValue(0);

        builder.Property(e => e.Mean)
            .HasColumnName("mean")
            .HasPrecision(18, 6);

        builder.Property(e => e.StdDev)
            .HasColumnName("std_dev")
            .HasPrecision(18, 6);

        builder.Property(e => e.Usl)
            .HasColumnName("usl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Lsl)
            .HasColumnName("lsl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Target)
            .HasColumnName("target")
            .HasPrecision(18, 6);

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

        builder.Property(e => e.Cpm)
            .HasColumnName("cpm")
            .HasPrecision(18, 6);

        builder.Property(e => e.StudyStatus)
            .HasColumnName("study_status")
            .HasMaxLength(20);

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId })
            .HasDatabaseName("IX_ProcessCapability_DivSeq_SpecSysId");

        builder.HasIndex(e => e.StudyStatus)
            .HasDatabaseName("IX_ProcessCapability_StudyStatus");
    }
}
