using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for ImportConfig entity.
/// </summary>
public class ImportConfigConfiguration : IEntityTypeConfiguration<ImportConfig>
{
    public void Configure(EntityTypeBuilder<ImportConfig> builder)
    {
        builder.ToTable("SPC_IMPORT_CONFIG");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ImportId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ImportId)
            .HasColumnName("import_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ImportName)
            .HasColumnName("import_name")
            .HasMaxLength(200);

        builder.Property(e => e.ImportType)
            .HasColumnName("import_type")
            .HasMaxLength(20)
            .HasDefaultValue("EXCEL");

        builder.Property(e => e.ScreenId)
            .HasColumnName("screen_id")
            .HasMaxLength(50);

        builder.Property(e => e.ColumnMapping)
            .HasColumnName("column_mapping")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.ValidationRules)
            .HasColumnName("validation_rules")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.TemplateFile)
            .HasColumnName("template_file")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.ScreenId })
            .HasDatabaseName("IX_ImportConfig_DivSeq_ScreenId");
    }
}
