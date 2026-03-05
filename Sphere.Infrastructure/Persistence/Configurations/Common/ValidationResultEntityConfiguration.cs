using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for ValidationResultEntity.
/// Note: This is typically used as a DTO for validation results, but configured for potential table storage.
/// </summary>
public class ValidationResultEntityConfiguration : IEntityTypeConfiguration<ValidationResultEntity>
{
    public void Configure(EntityTypeBuilder<ValidationResultEntity> builder)
    {
        builder.ToTable("SPC_VALIDATION_RESULT");

        // Primary Key - using surrogate key for result storage
        builder.HasKey(e => new { e.DivSeq, e.FieldName, e.ErrorCode });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.IsValid)
            .HasColumnName("is_valid")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.ErrorCode)
            .HasColumnName("error_code")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(500);

        builder.Property(e => e.FieldName)
            .HasColumnName("field_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

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

        builder.Property(e => e.UpdateDate)
            .HasColumnName("update_date");
    }
}
