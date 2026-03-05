using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for GridColumn entity.
/// </summary>
public class GridColumnConfiguration : IEntityTypeConfiguration<GridColumn>
{
    public void Configure(EntityTypeBuilder<GridColumn> builder)
    {
        builder.ToTable("SPC_GRID_COLUMN");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.GridId, e.ColumnId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.GridId)
            .HasColumnName("grid_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ColumnId)
            .HasColumnName("column_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ColumnName)
            .HasColumnName("column_name")
            .HasMaxLength(200);

        builder.Property(e => e.ColumnNameK)
            .HasColumnName("column_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.ColumnNameE)
            .HasColumnName("column_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.DataType)
            .HasColumnName("data_type")
            .HasMaxLength(20)
            .HasDefaultValue("string");

        builder.Property(e => e.Width)
            .HasColumnName("width")
            .HasDefaultValue(100);

        builder.Property(e => e.Align)
            .HasColumnName("align")
            .HasMaxLength(10)
            .HasDefaultValue("left");

        builder.Property(e => e.VisibleYn)
            .HasColumnName("visible_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.SortableYn)
            .HasColumnName("sortable_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.FilterableYn)
            .HasColumnName("filterable_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.Format)
            .HasColumnName("format")
            .HasMaxLength(100);

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
        builder.HasIndex(e => new { e.DivSeq, e.GridId })
            .HasDatabaseName("IX_GridColumn_DivSeq_GridId");
    }
}
