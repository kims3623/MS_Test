using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for MaterialMaster entity.
/// Maps to SPC_MTRL_MST table with composite primary key.
/// </summary>
public class MaterialMasterConfiguration : IEntityTypeConfiguration<MaterialMaster>
{
    public void Configure(EntityTypeBuilder<MaterialMaster> builder)
    {
        builder.ToTable("SPC_MTRL_MST");

        // Composite Primary Key (DivSeq, MtrlId)
        builder.HasKey(e => new { e.DivSeq, e.MtrlId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MtrlId)
            .HasColumnName("mtrl_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.MtrlName)
            .HasColumnName("mtrl_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlNameK)
            .HasColumnName("mtrl_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlNameE)
            .HasColumnName("mtrl_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlNameC)
            .HasColumnName("mtrl_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlNameV)
            .HasColumnName("mtrl_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlType)
            .HasColumnName("mtrl_type")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlSpec)
            .HasColumnName("mtrl_spec")
            .HasMaxLength(200);

        builder.Property(e => e.Unit)
            .HasColumnName("unit")
            .HasMaxLength(20);

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq");

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
        builder.HasIndex(e => new { e.DivSeq, e.MtrlClassId })
            .HasDatabaseName("IX_MaterialMaster_DivSeq_MtrlClassId");

        builder.HasIndex(e => new { e.DivSeq, e.VendorId })
            .HasDatabaseName("IX_MaterialMaster_DivSeq_VendorId");
    }
}
