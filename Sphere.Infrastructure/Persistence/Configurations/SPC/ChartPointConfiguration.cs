using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for ChartPoint entity.
/// Maps to SPC_CHART_POINT table for individual data points in charts.
/// </summary>
public class ChartPointConfiguration : IEntityTypeConfiguration<ChartPoint>
{
    public void Configure(EntityTypeBuilder<ChartPoint> builder)
    {
        builder.ToTable("SPC_CHART_POINT");

        // Primary Key
        builder.HasKey(e => e.PointId);

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.PointId)
            .HasColumnName("point_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SeriesId)
            .HasColumnName("series_id")
            .HasMaxLength(40);

        builder.Property(e => e.XValue)
            .HasColumnName("x_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.YValue)
            .HasColumnName("y_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.Label)
            .HasColumnName("label")
            .HasMaxLength(100);

        builder.Property(e => e.Marker)
            .HasColumnName("marker")
            .HasMaxLength(20);

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
        builder.HasIndex(e => new { e.DivSeq, e.SeriesId })
            .HasDatabaseName("IX_ChartPoint_DivSeq_SeriesId");

        builder.HasIndex(e => e.XValue)
            .HasDatabaseName("IX_ChartPoint_XValue");
    }
}
