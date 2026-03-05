using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for PicType entity.
/// Maps to SPC_PIC_TYPE table with single primary key.
/// </summary>
public class PicTypeConfiguration : IEntityTypeConfiguration<PicType>
{
    public void Configure(EntityTypeBuilder<PicType> builder)
    {
        builder.ToTable("SPC_PIC_TYPE");

        // Single Primary Key (PicTypeId)
        builder.HasKey(e => e.PicTypeId);

        // Column mappings - Primary Key
        builder.Property(e => e.PicTypeId)
            .HasColumnName("pic_type_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - DivSeq (not part of PK but still needed)
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.PicTypeName)
            .HasColumnName("pic_type_name")
            .HasMaxLength(200);

        builder.Property(e => e.PicTypeNameK)
            .HasColumnName("pic_type_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.PicTypeNameE)
            .HasColumnName("pic_type_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ResponsibilityArea)
            .HasColumnName("responsibility_area")
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
        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_PicType_DspSeq");
    }
}
