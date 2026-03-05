using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for DataType entity.
/// Maps to SPC_DATA_TYPE table with single primary key.
/// </summary>
public class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
{
    public void Configure(EntityTypeBuilder<DataType> builder)
    {
        builder.ToTable("SPC_DATA_TYPE");

        // Single Primary Key (DtType)
        builder.HasKey(e => e.DtType);

        // Column mappings - Primary Key
        builder.Property(e => e.DtType)
            .HasColumnName("dt_type")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - DivSeq (not part of PK but still needed)
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.DtTypeName)
            .HasColumnName("dt_type_name")
            .HasMaxLength(200);

        builder.Property(e => e.DtTypeNameK)
            .HasColumnName("dt_type_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.DtTypeNameE)
            .HasColumnName("dt_type_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.DtTypeNameC)
            .HasColumnName("dt_type_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.DtTypeNameV)
            .HasColumnName("dt_type_name_v")
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
            .HasDatabaseName("IX_DataType_DspSeq");
    }
}
