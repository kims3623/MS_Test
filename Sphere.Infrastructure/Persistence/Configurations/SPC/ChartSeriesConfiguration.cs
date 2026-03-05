using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.SPC;

namespace Sphere.Infrastructure.Persistence.Configurations.SPC;

/// <summary>
/// EF Core configuration for ChartSeries entity.
/// Maps to SPC_CHART_SERIES table for chart data series.
/// </summary>
public class ChartSeriesConfiguration : IEntityTypeConfiguration<ChartSeries>
{
    public void Configure(EntityTypeBuilder<ChartSeries> builder)
    {
        builder.ToTable("SPC_CHART_SERIES");

        // Primary Key
        builder.HasKey(e => e.SeriesId);

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SeriesId)
            .HasColumnName("series_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ChartId)
            .HasColumnName("chart_id")
            .HasMaxLength(40);

        builder.Property(e => e.SeriesName)
            .HasColumnName("series_name")
            .HasMaxLength(100);

        builder.Property(e => e.SeriesType)
            .HasColumnName("series_type")
            .HasMaxLength(50);

        builder.Property(e => e.Color)
            .HasColumnName("color")
            .HasMaxLength(20);

        builder.Property(e => e.DataKey)
            .HasColumnName("data_key")
            .HasMaxLength(50);

        builder.Property(e => e.DisplayOrder)
            .HasColumnName("display_order")
            .HasDefaultValue(0);

        builder.Property(e => e.Visible)
            .HasColumnName("visible")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

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
        builder.HasIndex(e => new { e.DivSeq, e.ChartId })
            .HasDatabaseName("IX_ChartSeries_DivSeq_ChartId");

        builder.HasIndex(e => e.DisplayOrder)
            .HasDatabaseName("IX_ChartSeries_DisplayOrder");
    }
}
