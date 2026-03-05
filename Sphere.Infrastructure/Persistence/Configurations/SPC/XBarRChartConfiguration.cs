using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for XBarRChart entity.
/// Maps to SPC_XBAR_R_CHART table for X-bar R control charts.
/// </summary>
public class XBarRChartConfiguration : IEntityTypeConfiguration<XBarRChart>
{
    public void Configure(EntityTypeBuilder<XBarRChart> builder)
    {
        builder.ToTable("SPC_XBAR_R_CHART");

        // Primary Key
        builder.HasKey(e => e.ChartId);

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ChartId)
            .HasColumnName("chart_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10);

        builder.Property(e => e.XBar)
            .HasColumnName("x_bar")
            .HasPrecision(18, 6);

        builder.Property(e => e.Range)
            .HasColumnName("range")
            .HasPrecision(18, 6);

        builder.Property(e => e.Ucl)
            .HasColumnName("ucl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Lcl)
            .HasColumnName("lcl")
            .HasPrecision(18, 6);

        builder.Property(e => e.Cl)
            .HasColumnName("cl")
            .HasPrecision(18, 6);

        builder.Property(e => e.UclR)
            .HasColumnName("ucl_r")
            .HasPrecision(18, 6);

        builder.Property(e => e.LclR)
            .HasColumnName("lcl_r")
            .HasPrecision(18, 6);

        builder.Property(e => e.ClR)
            .HasColumnName("cl_r")
            .HasPrecision(18, 6);

        builder.Property(e => e.SubgroupSize)
            .HasColumnName("subgroup_size")
            .HasDefaultValue(0);

        builder.Property(e => e.AlarmYn)
            .HasColumnName("alarm_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.AlarmType)
            .HasColumnName("alarm_type")
            .HasMaxLength(50);

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
            .HasDatabaseName("IX_XBarRChart_DivSeq_SpecSysId");

        builder.HasIndex(e => new { e.DivSeq, e.WorkDate })
            .HasDatabaseName("IX_XBarRChart_DivSeq_WorkDate");

        builder.HasIndex(e => e.AlarmYn)
            .HasDatabaseName("IX_XBarRChart_AlarmYn");
    }
}
