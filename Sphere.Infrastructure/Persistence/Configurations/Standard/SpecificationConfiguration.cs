using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for Specification entity.
/// Handles complex composite primary key (7 fields).
/// </summary>
public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.ToTable("SPC_SPEC_MST");

        // Composite Primary Key (7 fields)
        builder.HasKey(e => new
        {
            e.DivSeq,
            e.SpecSysId,
            e.CntlnTypeId,
            e.SpecTypeId,
            e.SpecVer,
            e.UseYn,
            e.UpdateDate
        });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CntlnTypeId)
            .HasColumnName("cntln_type_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecTypeId)
            .HasColumnName("spec_type_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecVer)
            .HasColumnName("spec_ver")
            .IsRequired();

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y")
            .IsRequired();

        builder.Property(e => e.UpdateDate)
            .HasColumnName("update_date")
            .IsRequired();

        // Column mappings - Foreign Keys and Attributes
        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlId)
            .HasColumnName("mtrl_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlName)
            .HasColumnName("mtrl_name")
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

        builder.Property(e => e.MeasmId)
            .HasColumnName("measm_id")
            .HasMaxLength(40);

        builder.Property(e => e.MeasmName)
            .HasColumnName("measm_name")
            .HasMaxLength(200);

        builder.Property(e => e.SplCnt)
            .HasColumnName("spl_cnt")
            .HasMaxLength(10);

        builder.Property(e => e.EqpId)
            .HasColumnName("eqp_id")
            .HasMaxLength(40);

        builder.Property(e => e.Line)
            .HasColumnName("line")
            .HasMaxLength(40);

        builder.Property(e => e.MoldCnt)
            .HasColumnName("mold_cnt")
            .HasMaxLength(10);

        builder.Property(e => e.Cavity)
            .HasColumnName("cavity")
            .HasMaxLength(10);

        builder.Property(e => e.Needle)
            .HasColumnName("needle")
            .HasMaxLength(10);

        builder.Property(e => e.Angle)
            .HasColumnName("angle")
            .HasMaxLength(10);

        builder.Property(e => e.Statistics)
            .HasColumnName("statistics")
            .HasMaxLength(40);

        // Spec limit values
        builder.Property(e => e.LlValue)
            .HasColumnName("ll_value")
            .HasMaxLength(50);

        builder.Property(e => e.ClValue)
            .HasColumnName("cl_value")
            .HasMaxLength(50);

        builder.Property(e => e.UlValue)
            .HasColumnName("ul_value")
            .HasMaxLength(50);

        builder.Property(e => e.LslValue)
            .HasColumnName("lsl_value")
            .HasMaxLength(50);

        builder.Property(e => e.TargetValue)
            .HasColumnName("target_value")
            .HasMaxLength(50);

        builder.Property(e => e.UslValue)
            .HasColumnName("usl_value")
            .HasMaxLength(50);

        builder.Property(e => e.AprovId)
            .HasColumnName("aprov_id")
            .HasMaxLength(40);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(e => e.Remarks)
            .HasColumnName("remarks")
            .HasMaxLength(1000);

        builder.Property(e => e.RowStatus)
            .HasColumnName("ROW_STATUS")
            .HasMaxLength(10);

        builder.Property(e => e.CreateUserId)
            .HasColumnName("create_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.CreateDate)
            .HasColumnName("create_date");

        builder.Property(e => e.UpdateUserId)
            .HasColumnName("update_user_id")
            .HasMaxLength(50);

        // Indexes
        builder.HasIndex(e => new { e.DivSeq, e.VendorId })
            .HasDatabaseName("IX_Specification_DivSeq_VendorId");

        builder.HasIndex(e => new { e.DivSeq, e.MtrlClassId })
            .HasDatabaseName("IX_Specification_DivSeq_MtrlClassId");

        builder.HasIndex(e => new { e.DivSeq, e.MtrlId })
            .HasDatabaseName("IX_Specification_DivSeq_MtrlId");

        builder.HasIndex(e => new { e.DivSeq, e.ProjectId })
            .HasDatabaseName("IX_Specification_DivSeq_ProjectId");
    }
}
