using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for ExportConfig entity.
/// </summary>
public class ExportConfigConfiguration : IEntityTypeConfiguration<ExportConfig>
{
    public void Configure(EntityTypeBuilder<ExportConfig> builder)
    {
        builder.ToTable("SPC_EXPORT_CONFIG");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ExportId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ExportId)
            .HasColumnName("export_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ExportName)
            .HasColumnName("export_name")
            .HasMaxLength(200);

        builder.Property(e => e.ExportType)
            .HasColumnName("export_type")
            .HasMaxLength(20)
            .HasDefaultValue("EXCEL");

        builder.Property(e => e.ScreenId)
            .HasColumnName("screen_id")
            .HasMaxLength(50);

        builder.Property(e => e.ColumnList)
            .HasColumnName("column_list")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.FilterJson)
            .HasColumnName("filter_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.TemplateFile)
            .HasColumnName("template_file")
            .HasMaxLength(500);

        builder.Property(e => e.MaxRows)
            .HasColumnName("max_rows")
            .HasDefaultValue(10000);

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
            .HasDatabaseName("IX_ExportConfig_DivSeq_ScreenId");
    }
}
