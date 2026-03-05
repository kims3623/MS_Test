using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for SpecType entity.
/// Maps to SPC_SPEC_TYPE table with single primary key.
/// </summary>
public class SpecTypeConfiguration : IEntityTypeConfiguration<SpecType>
{
    public void Configure(EntityTypeBuilder<SpecType> builder)
    {
        builder.ToTable("SPC_SPEC_TYPE");

        // Single Primary Key (SpecTypeId)
        builder.HasKey(e => e.SpecTypeId);

        // Column mappings - Primary Key
        builder.Property(e => e.SpecTypeId)
            .HasColumnName("spec_type_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - DivSeq (not part of PK but still needed)
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.SpecTypeName)
            .HasColumnName("spec_type_name")
            .HasMaxLength(200);

        builder.Property(e => e.SpecTypeNameK)
            .HasColumnName("spec_type_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.SpecTypeNameE)
            .HasColumnName("spec_type_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.Category)
            .HasColumnName("category")
            .HasMaxLength(40);

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
        builder.HasIndex(e => e.Category)
            .HasDatabaseName("IX_SpecType_Category");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_SpecType_DspSeq");
    }
}
