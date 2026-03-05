using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3030Pareto entity.
/// Maps to SPC_SPH3030_PARETO table for Pareto chart data.
/// </summary>
public class SPH3030ParetoConfiguration : IEntityTypeConfiguration<SPH3030Pareto>
{
    public void Configure(EntityTypeBuilder<SPH3030Pareto> builder)
    {
        builder.ToTable("SPC_SPH3030_PARETO");

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

        builder.Property(e => e.PeriodFrom)
            .HasColumnName("period_from")
            .HasMaxLength(10);

        builder.Property(e => e.PeriodTo)
            .HasColumnName("period_to")
            .HasMaxLength(10);

        builder.Property(e => e.DefectCategory)
            .HasColumnName("defect_category")
            .HasMaxLength(50);

        builder.Property(e => e.DefectCategoryName)
            .HasColumnName("defect_category_name")
            .HasMaxLength(200);

        builder.Property(e => e.DefectCount)
            .HasColumnName("defect_count")
            .HasDefaultValue(0);

        builder.Property(e => e.Percentage)
            .HasColumnName("percentage")
            .HasPrecision(18, 4);

        builder.Property(e => e.CumulativePercentage)
            .HasColumnName("cumulative_percentage")
            .HasPrecision(18, 4);

        builder.Property(e => e.Rank)
            .HasColumnName("rank")
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
        builder.HasIndex(e => new { e.DivSeq, e.PeriodFrom, e.PeriodTo })
            .HasDatabaseName("IX_SPH3030Pareto_DivSeq_Period");

        builder.HasIndex(e => e.Rank)
            .HasDatabaseName("IX_SPH3030Pareto_Rank");
    }
}
