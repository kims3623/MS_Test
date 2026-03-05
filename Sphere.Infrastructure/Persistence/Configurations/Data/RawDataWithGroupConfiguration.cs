using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for RawDataWithGroup entity.
/// Maps to SPC_RAWDATA_GROUP table.
/// </summary>
public class RawDataWithGroupConfiguration : IEntityTypeConfiguration<RawDataWithGroup>
{
    public void Configure(EntityTypeBuilder<RawDataWithGroup> builder)
    {
        builder.ToTable("SPC_RAWDATA_GROUP");

        // Composite Primary Key: GroupSpecSysId, DivSeq, SpecSysId
        builder.HasKey(e => new { e.GroupSpecSysId, e.DivSeq, e.SpecSysId });

        // Column mappings - Primary Key fields
        builder.Property(e => e.GroupSpecSysId)
            .HasColumnName("group_spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        // Material Info
        builder.Property(e => e.MtrlId)
            .HasColumnName("mtrl_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlName)
            .HasColumnName("mtrl_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        // Project Info
        builder.Property(e => e.ProjectId)
            .HasColumnName("project_id")
            .HasMaxLength(40);

        builder.Property(e => e.ProjectName)
            .HasColumnName("project_name")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectNameEn)
            .HasColumnName("project_name_en")
            .HasMaxLength(200);

        // Vendor Info
        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.VendorNameEn)
            .HasColumnName("vendor_name_en")
            .HasMaxLength(200);

        // Product Info
        builder.Property(e => e.ActProdId)
            .HasColumnName("act_prod_id")
            .HasMaxLength(40);

        builder.Property(e => e.ActProdName)
            .HasColumnName("act_prod_name")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdNameEn)
            .HasColumnName("act_prod_name_en")
            .HasMaxLength(200);

        // Step Info
        builder.Property(e => e.StepId)
            .HasColumnName("step_id")
            .HasMaxLength(40);

        builder.Property(e => e.StepName)
            .HasColumnName("step_name")
            .HasMaxLength(200);

        builder.Property(e => e.StepNameEn)
            .HasColumnName("step_name_en")
            .HasMaxLength(200);

        // Item Info
        builder.Property(e => e.ItemId)
            .HasColumnName("item_id")
            .HasMaxLength(40);

        builder.Property(e => e.ItemName)
            .HasColumnName("item_name")
            .HasMaxLength(200);

        builder.Property(e => e.ItemNameEn)
            .HasColumnName("item_name_en")
            .HasMaxLength(200);

        // Measurement Info
        builder.Property(e => e.MeasmId)
            .HasColumnName("measm_id")
            .HasMaxLength(40);

        builder.Property(e => e.MeasmName)
            .HasColumnName("measm_name")
            .HasMaxLength(200);

        builder.Property(e => e.MeasmNameEn)
            .HasColumnName("measm_name_en")
            .HasMaxLength(200);

        // Sample Info
        builder.Property(e => e.SplCnt)
            .HasColumnName("spl_cnt")
            .HasMaxLength(20);

        // Work Data
        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10);

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(20);

        builder.Property(e => e.ShiftName)
            .HasColumnName("shift_name")
            .HasMaxLength(100);

        builder.Property(e => e.ShiftNameEn)
            .HasColumnName("shift_name_en")
            .HasMaxLength(100);

        // Raw Data Values
        builder.Property(e => e.RawDataValue)
            .HasColumnName("raw_data")
            .HasMaxLength(4000);

        builder.Property(e => e.InputQty)
            .HasColumnName("input_qty")
            .HasMaxLength(20);

        builder.Property(e => e.DefectQty)
            .HasColumnName("defect_qty")
            .HasMaxLength(20);

        // Approval Info
        builder.Property(e => e.AprovId)
            .HasColumnName("aprov_id")
            .HasMaxLength(40);

        // Classification
        builder.Property(e => e.Gubun)
            .HasColumnName("gubun")
            .HasMaxLength(50);

        // Multilingual Names
        builder.Property(e => e.MtrlClassNameEn)
            .HasColumnName("mtrl_class_name_en")
            .HasMaxLength(200);

        // Common Audit Fields
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
        builder.HasIndex(e => new { e.DivSeq, e.GroupSpecSysId })
            .HasDatabaseName("IX_RawDataWithGroup_DivSeq_GroupSpecSysId");

        builder.HasIndex(e => new { e.DivSeq, e.VendorId })
            .HasDatabaseName("IX_RawDataWithGroup_DivSeq_VendorId");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_RawDataWithGroup_UseYn");
    }
}
