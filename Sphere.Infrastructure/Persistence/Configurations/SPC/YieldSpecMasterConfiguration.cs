using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for YieldSpecMaster entity.
/// Maps to SPC_YIELD_SPEC_MST table for yield specification master data.
/// </summary>
public class YieldSpecMasterConfiguration : IEntityTypeConfiguration<YieldSpecMaster>
{
    public void Configure(EntityTypeBuilder<YieldSpecMaster> builder)
    {
        builder.ToTable("SPC_YIELD_SPEC_MST");

        // Composite Primary Key (7 fields as per mapping table)
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.VendorId, e.MtrlClassId, e.ActProdId, e.YieldStepId, e.WorkDate });

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
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdId)
            .HasColumnName("act_prod_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActProdName)
            .HasColumnName("act_prod_name")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepId)
            .HasColumnName("yield_step_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.YieldStepName)
            .HasColumnName("yield_step_name")
            .HasMaxLength(200);

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(10);

        builder.Property(e => e.ShiftName)
            .HasColumnName("shift_name")
            .HasMaxLength(100);

        builder.Property(e => e.InputQty)
            .HasColumnName("input_qty")
            .HasMaxLength(20);

        builder.Property(e => e.DefectQty)
            .HasColumnName("defect_qty")
            .HasMaxLength(20);

        builder.Property(e => e.Yield)
            .HasColumnName("yield")
            .HasMaxLength(20);

        builder.Property(e => e.Wlcl)
            .HasColumnName("wlcl")
            .HasMaxLength(20);

        builder.Property(e => e.Mlcl)
            .HasColumnName("mlcl")
            .HasMaxLength(20);

        builder.Property(e => e.Alarm)
            .HasColumnName("alarm")
            .HasMaxLength(20);

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.YieldCalcType)
            .HasColumnName("yield_calc_type")
            .HasMaxLength(20);

        builder.Property(e => e.YieldCalcTypeName)
            .HasColumnName("yield_calc_type_name")
            .HasMaxLength(100);

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
            .HasDatabaseName("IX_YieldSpecMaster_DivSeq_VendorId");

        builder.HasIndex(e => new { e.DivSeq, e.WorkDate })
            .HasDatabaseName("IX_YieldSpecMaster_DivSeq_WorkDate");

        builder.HasIndex(e => e.ActProdId)
            .HasDatabaseName("IX_YieldSpecMaster_ActProdId");
    }
}
