using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for ChartData entity.
/// Maps to SPC_CHART_DATA table.
/// </summary>
public class ChartDataConfiguration : IEntityTypeConfiguration<ChartData>
{
    public void Configure(EntityTypeBuilder<ChartData> builder)
    {
        builder.ToTable("SPC_CHART_DATA");

        // Composite Primary Key (DivSeq + SpecSysId + PointSeq as unique identifier)
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.PointSeq });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.PointSeq)
            .HasColumnName("point_seq")
            .IsRequired();

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10);

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(10);

        builder.Property(e => e.XValue)
            .HasColumnName("x_value")
            .HasMaxLength(50);

        builder.Property(e => e.YValue)
            .HasColumnName("y_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.ChartType)
            .HasColumnName("chart_type")
            .HasMaxLength(20);

        builder.Property(e => e.Series)
            .HasColumnName("series")
            .HasMaxLength(50);

        builder.Property(e => e.Ucl)
            .HasColumnName("ucl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cl)
            .HasColumnName("cl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Lcl)
            .HasColumnName("lcl")
            .HasPrecision(18, 6);

        builder.Property(e => e.OocYn)
            .HasColumnName("ooc_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.RunRuleYn)
            .HasColumnName("run_rule_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ViolatedRules)
            .HasColumnName("violated_rules")
            .HasMaxLength(200);

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId, e.WorkDate })
            .HasDatabaseName("IX_ChartData_DivSeq_SpecSysId_WorkDate");

        builder.HasIndex(e => e.ChartType)
            .HasDatabaseName("IX_ChartData_ChartType");

        builder.HasIndex(e => e.OocYn)
            .HasDatabaseName("IX_ChartData_OocYn");
    }
}
