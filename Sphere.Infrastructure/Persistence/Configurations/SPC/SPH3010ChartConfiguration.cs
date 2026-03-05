using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3010Chart entity.
/// Maps to SPC_SPH3010_CHART table for X-bar R chart data.
/// </summary>
public class SPH3010ChartConfiguration : IEntityTypeConfiguration<SPH3010Chart>
{
    public void Configure(EntityTypeBuilder<SPH3010Chart> builder)
    {
        builder.ToTable("SPC_SPH3010_CHART");

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

        builder.Property(e => e.ChartId)
            .HasColumnName("chart_id")
            .HasMaxLength(40);

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10);

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(10);

        builder.Property(e => e.GroupNo)
            .HasColumnName("group_no")
            .HasDefaultValue(0);

        builder.Property(e => e.XBar)
            .HasColumnName("x_bar")
            .HasPrecision(18, 6);

        builder.Property(e => e.RValue)
            .HasColumnName("r_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.SValue)
            .HasColumnName("s_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.XBarUcl)
            .HasColumnName("x_bar_ucl")
            .HasPrecision(18, 6);

        builder.Property(e => e.XBarCl)
            .HasColumnName("x_bar_cl")
            .HasPrecision(18, 6);

        builder.Property(e => e.XBarLcl)
            .HasColumnName("x_bar_lcl")
            .HasPrecision(18, 6);

        builder.Property(e => e.RUcl)
            .HasColumnName("r_ucl")
            .HasPrecision(18, 6);

        builder.Property(e => e.RCl)
            .HasColumnName("r_cl")
            .HasPrecision(18, 6);

        builder.Property(e => e.RLcl)
            .HasColumnName("r_lcl")
            .HasPrecision(18, 6);

        builder.Property(e => e.XBarOocYn)
            .HasColumnName("x_bar_ooc_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ROocYn)
            .HasColumnName("r_ooc_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.RunRuleYn)
            .HasColumnName("run_rule_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

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
            .HasDatabaseName("IX_SPH3010Chart_DivSeq_SpecSysId_WorkDate");

        builder.HasIndex(e => e.XBarOocYn)
            .HasDatabaseName("IX_SPH3010Chart_XBarOocYn");
    }
}
