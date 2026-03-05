using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for ControlLimit entity.
/// Maps to SPC_CONTROL_LIMIT table.
/// </summary>
public class ControlLimitConfiguration : IEntityTypeConfiguration<ControlLimit>
{
    public void Configure(EntityTypeBuilder<ControlLimit> builder)
    {
        builder.ToTable("SPC_CONTROL_LIMIT");

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

        builder.Property(e => e.LimitType)
            .HasColumnName("limit_type")
            .HasMaxLength(20);

        builder.Property(e => e.ChartType)
            .HasColumnName("chart_type")
            .HasMaxLength(20);

        builder.Property(e => e.LimitValue)
            .HasColumnName("limit_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.EffectiveFrom)
            .HasColumnName("effective_from");

        builder.Property(e => e.EffectiveTo)
            .HasColumnName("effective_to");

        builder.Property(e => e.CalcMethod)
            .HasColumnName("calc_method")
            .HasMaxLength(50);

        builder.Property(e => e.Version)
            .HasColumnName("version")
            .HasDefaultValue(1);

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId, e.LimitType })
            .HasDatabaseName("IX_ControlLimit_DivSeq_SpecSysId_LimitType");

        builder.HasIndex(e => e.ChartType)
            .HasDatabaseName("IX_ControlLimit_ChartType");
    }
}
