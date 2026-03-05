using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for YieldSpec entity.
/// Maps to SPC_YIELD_SPEC_MST table with composite primary key.
/// </summary>
public class YieldSpecConfiguration : IEntityTypeConfiguration<YieldSpec>
{
    public void Configure(EntityTypeBuilder<YieldSpec> builder)
    {
        builder.ToTable("A");

        // Composite Primary Key (DivSeq, SpecSysId)
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Foreign Keys
        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        builder.Property(e => e.YieldStepId)
            .HasColumnName("yield_step_id")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepName)
            .HasColumnName("yield_step_name")
            .HasMaxLength(200);

        builder.Property(e => e.TargetYield)
            .HasColumnName("target_yield")
            .HasPrecision(18, 6);

        builder.Property(e => e.LowerLimit)
            .HasColumnName("lower_limit")
            .HasPrecision(18, 6);

        builder.Property(e => e.UpperLimit)
            .HasColumnName("upper_limit")
            .HasPrecision(18, 6);

        builder.Property(e => e.Unit)
            .HasColumnName("unit")
            .HasMaxLength(20);

        builder.Property(e => e.EffectiveFrom)
            .HasColumnName("effective_from");

        builder.Property(e => e.EffectiveTo)
            .HasColumnName("effective_to");

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        // Common audit fields from SphereEntity
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
            .HasDatabaseName("IX_YieldSpec_DivSeq_VendorId");

        builder.HasIndex(e => new { e.DivSeq, e.MtrlClassId })
            .HasDatabaseName("IX_YieldSpec_DivSeq_MtrlClassId");

        builder.HasIndex(e => new { e.DivSeq, e.YieldStepId })
            .HasDatabaseName("IX_YieldSpec_DivSeq_YieldStepId");
    }
}
