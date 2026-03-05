using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for SPH3030Chart entity.
/// Maps to SPC_SPH3030_CHART table for Pareto analysis chart.
/// </summary>
public class SPH3030ChartConfiguration : IEntityTypeConfiguration<SPH3030Chart>
{
    public void Configure(EntityTypeBuilder<SPH3030Chart> builder)
    {
        builder.ToTable("SPC_SPH3030_CHART");

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

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectId)
            .HasColumnName("project_id")
            .HasMaxLength(40);

        builder.Property(e => e.ProjectName)
            .HasColumnName("project_name")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdId)
            .HasColumnName("act_prod_id")
            .HasMaxLength(40);

        builder.Property(e => e.ActProdName)
            .HasColumnName("act_prod_name")
            .HasMaxLength(200);

        builder.Property(e => e.StepId)
            .HasColumnName("step_id")
            .HasMaxLength(40);

        builder.Property(e => e.StepName)
            .HasColumnName("step_name")
            .HasMaxLength(200);

        builder.Property(e => e.ItemId)
            .HasColumnName("item_id")
            .HasMaxLength(40);

        builder.Property(e => e.ItemName)
            .HasColumnName("item_name")
            .HasMaxLength(200);

        builder.Property(e => e.CategoryName)
            .HasColumnName("category_name")
            .HasMaxLength(200);

        builder.Property(e => e.DefectCount)
            .HasColumnName("defect_count")
            .HasDefaultValue(0);

        builder.Property(e => e.DefectRate)
            .HasColumnName("defect_rate")
            .HasPrecision(18, 4);

        builder.Property(e => e.CumulativeRate)
            .HasColumnName("cumulative_rate")
            .HasPrecision(18, 4);

        builder.Property(e => e.Rank)
            .HasColumnName("rank")
            .HasDefaultValue(0);

        builder.Property(e => e.FromDate)
            .HasColumnName("from_date")
            .HasMaxLength(10);

        builder.Property(e => e.ToDate)
            .HasColumnName("to_date")
            .HasMaxLength(10);

        builder.Property(e => e.ChartType)
            .HasColumnName("chart_type")
            .HasMaxLength(20);

        builder.Property(e => e.ChartTitle)
            .HasColumnName("chart_title")
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
        builder.HasIndex(e => new { e.DivSeq, e.VendorId })
            .HasDatabaseName("IX_SPH3030Chart_DivSeq_VendorId");

        builder.HasIndex(e => new { e.DivSeq, e.FromDate, e.ToDate })
            .HasDatabaseName("IX_SPH3030Chart_DivSeq_DateRange");

        builder.HasIndex(e => e.Rank)
            .HasDatabaseName("IX_SPH3030Chart_Rank");
    }
}
