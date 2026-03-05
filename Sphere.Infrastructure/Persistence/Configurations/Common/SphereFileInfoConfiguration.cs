using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for SphereFileInfo entity.
/// </summary>
public class SphereFileInfoConfiguration : IEntityTypeConfiguration<SphereFileInfo>
{
    public void Configure(EntityTypeBuilder<SphereFileInfo> builder)
    {
        builder.ToTable("SPC_FILE_INFO");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.FileId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FileId)
            .HasColumnName("file_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.OriginalFileName)
            .HasColumnName("original_file_name")
            .HasMaxLength(500);

        builder.Property(e => e.StoredFileName)
            .HasColumnName("stored_file_name")
            .HasMaxLength(500);

        builder.Property(e => e.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(1000);

        builder.Property(e => e.FileSize)
            .HasColumnName("file_size");

        builder.Property(e => e.FileExtension)
            .HasColumnName("file_extension")
            .HasMaxLength(20);

        builder.Property(e => e.MimeType)
            .HasColumnName("mime_type")
            .HasMaxLength(100);

        builder.Property(e => e.RefType)
            .HasColumnName("ref_type")
            .HasMaxLength(50);

        builder.Property(e => e.RefId)
            .HasColumnName("ref_id")
            .HasMaxLength(100);

        builder.Property(e => e.UploadDate)
            .HasColumnName("upload_date");

        builder.Property(e => e.UploadUserId)
            .HasColumnName("upload_user_id")
            .HasMaxLength(50);

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
        builder.HasIndex(e => new { e.DivSeq, e.RefType, e.RefId })
            .HasDatabaseName("IX_SphereFileInfo_DivSeq_RefType_RefId");
    }
}
