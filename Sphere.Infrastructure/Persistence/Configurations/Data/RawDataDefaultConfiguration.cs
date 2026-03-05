using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for RawDataDefault entity.
/// Maps to SPC_RAWDATA_DEFAULT table.
/// </summary>
public class RawDataDefaultConfiguration : IEntityTypeConfiguration<RawDataDefault>
{
    public void Configure(EntityTypeBuilder<RawDataDefault> builder)
    {
        builder.ToTable("SPC_RAWDATA_DEFAULT");

        // Composite Primary Key: DivSeq, SpecSysId
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.DefaultId });

        // Column mappings - Primary Key fields
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.DefaultId)
            .HasColumnName("default_id")
            .HasMaxLength(40)
            .IsRequired();

        // Default Value Info
        builder.Property(e => e.FieldName)
            .HasColumnName("field_name")
            .HasMaxLength(100);

        builder.Property(e => e.DefaultValue)
            .HasColumnName("default_value")
            .HasMaxLength(500);

        builder.Property(e => e.ValueType)
            .HasColumnName("value_type")
            .HasMaxLength(20)
            .HasDefaultValue("STRING");

        builder.Property(e => e.ApplyCondition)
            .HasColumnName("apply_condition")
            .HasMaxLength(500);

        builder.Property(e => e.Priority)
            .HasColumnName("priority")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId })
            .HasDatabaseName("IX_RawDataDefault_DivSeq_SpecSysId");

        builder.HasIndex(e => new { e.DivSeq, e.FieldName })
            .HasDatabaseName("IX_RawDataDefault_DivSeq_FieldName");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_RawDataDefault_UseYn");
    }
}
