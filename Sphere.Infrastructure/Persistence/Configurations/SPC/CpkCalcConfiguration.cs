using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for CpkCalc entity.
/// Maps to SPC_CPK_CALC table for process capability calculations.
/// </summary>
public class CpkCalcConfiguration : IEntityTypeConfiguration<CpkCalc>
{
    public void Configure(EntityTypeBuilder<CpkCalc> builder)
    {
        builder.ToTable("SPC_CPK_CALC");

        // Primary Key
        builder.HasKey(e => e.CalcId);

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CalcId)
            .HasColumnName("calc_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.Cp)
            .HasColumnName("cp")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cpk)
            .HasColumnName("cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.CpU)
            .HasColumnName("cp_u")
            .HasPrecision(18, 6);

        builder.Property(e => e.CpL)
            .HasColumnName("cp_l")
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
            .HasDatabaseName("IX_CpkCalc_DivSeq_SpecSysId");
    }
}
