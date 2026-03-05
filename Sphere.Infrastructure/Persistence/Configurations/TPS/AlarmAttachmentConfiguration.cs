using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmAttachment entity.
/// Maps to SPC_ALARM_ATTACH_LIST table.
/// </summary>
public class AlarmAttachmentConfiguration : IEntityTypeConfiguration<AlarmAttachment>
{
    public void Configure(EntityTypeBuilder<AlarmAttachment> builder)
    {
        builder.ToTable("SPC_ALARM_ATTACH_LIST");

        // Composite Primary Key (TableSysId mapped to AttachmentId, DivSeq)
        builder.HasKey(e => new { e.AttachmentId, e.DivSeq });

        // Column mappings
        builder.Property(e => e.AttachmentId)
            .HasColumnName("attachment_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.FileName)
            .HasColumnName("file_name")
            .HasMaxLength(255);

        builder.Property(e => e.OriginalFileName)
            .HasColumnName("original_file_name")
            .HasMaxLength(255);

        builder.Property(e => e.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(500);

        builder.Property(e => e.FileSize)
            .HasColumnName("file_size");

        builder.Property(e => e.FileType)
            .HasColumnName("file_type")
            .HasMaxLength(50);

        builder.Property(e => e.MimeType)
            .HasColumnName("mime_type")
            .HasMaxLength(100);

        builder.Property(e => e.UploadDate)
            .HasColumnName("upload_date");

        builder.Property(e => e.UploadUserId)
            .HasColumnName("upload_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        // Base entity properties
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
        builder.HasIndex(e => e.AlmSysId)
            .HasDatabaseName("IX_AlarmAttachment_AlmSysId");

        builder.HasIndex(e => e.UploadDate)
            .HasDatabaseName("IX_AlarmAttachment_UploadDate");
    }
}
