using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmAttachFile entity.
/// Maps to SPC_ALM_ATTACH_FILE table.
/// </summary>
public class AlarmAttachFileConfiguration : IEntityTypeConfiguration<AlarmAttachFile>
{
    public void Configure(EntityTypeBuilder<AlarmAttachFile> builder)
    {
        // TODO: SPC_ALM_ATTACH_FILE table not found in DB script - verify actual table name
        builder.ToTable("SPC_ALM_ATTACH_FILE");

        // Single Primary Key (FileId mapped to FileName as unique identifier)
        builder.HasKey(e => e.FileName);

        // Column mappings
        builder.Property(e => e.FileName)
            .HasColumnName("file_id")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.FileExtension)
            .HasColumnName("file_extension")
            .HasMaxLength(20);

        builder.Property(e => e.FileSize)
            .HasColumnName("file_size");

        // Base entity properties
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

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
        builder.HasIndex(e => e.FileExtension)
            .HasDatabaseName("IX_AlarmAttachFile_FileExtension");
    }
}
