using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for TempRawDataEtc entity.
/// Maps to SPC_TEMP_RAWDATA_ETC table.
/// </summary>
public class TempRawDataEtcConfiguration : IEntityTypeConfiguration<TempRawDataEtc>
{
    public void Configure(EntityTypeBuilder<TempRawDataEtc> builder)
    {
        builder.ToTable("SPC_TEMP_RAWDATA_ETC");

        // Composite Primary Key: DivSeq, SpecSysId, WorkDate (per task spec) + TempId, FieldSeq for uniqueness
        builder.HasKey(e => new { e.DivSeq, e.TempId, e.FieldSeq });

        // Column mappings - Primary Key fields
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.TempId)
            .HasColumnName("temp_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FieldSeq)
            .HasColumnName("field_seq")
            .IsRequired();

        // Field Info
        builder.Property(e => e.FieldName)
            .HasColumnName("field_name")
            .HasMaxLength(100);

        builder.Property(e => e.FieldValue)
            .HasColumnName("field_value")
            .HasMaxLength(1000);

        builder.Property(e => e.FieldType)
            .HasColumnName("field_type")
            .HasMaxLength(20)
            .HasDefaultValue("STRING");

        builder.Property(e => e.Remarks)
            .HasColumnName("remarks")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.TempId })
            .HasDatabaseName("IX_TempRawDataEtc_DivSeq_TempId");

        builder.HasIndex(e => new { e.DivSeq, e.FieldName })
            .HasDatabaseName("IX_TempRawDataEtc_DivSeq_FieldName");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_TempRawDataEtc_UseYn");
    }
}
