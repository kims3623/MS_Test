using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Dashboard;

namespace Sphere.Infrastructure.Persistence.Configurations.Dashboard;

/// <summary>
/// EF Core configuration for DashboardWidget entity.
/// Maps to SPC_DASHBOARD_WIDGET table for configurable dashboard widgets.
/// </summary>
public class DashboardWidgetConfiguration : IEntityTypeConfiguration<DashboardWidget>
{
    public void Configure(EntityTypeBuilder<DashboardWidget> builder)
    {
        builder.ToTable("SPC_DASHBOARD_WIDGET");

        // Primary Key
        builder.HasKey(e => e.WidgetId);

        // Column mappings
        builder.Property(e => e.WidgetId)
            .HasColumnName("widget_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.WidgetName)
            .HasColumnName("widget_name")
            .HasMaxLength(100);

        builder.Property(e => e.WidgetType)
            .HasColumnName("widget_type")
            .HasMaxLength(20);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.PositionX)
            .HasColumnName("position_x")
            .HasDefaultValue(0);

        builder.Property(e => e.PositionY)
            .HasColumnName("position_y")
            .HasDefaultValue(0);

        builder.Property(e => e.Width)
            .HasColumnName("width")
            .HasDefaultValue(1);

        builder.Property(e => e.Height)
            .HasColumnName("height")
            .HasDefaultValue(1);

        builder.Property(e => e.ConfigJson)
            .HasColumnName("config_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.RefreshInterval)
            .HasColumnName("refresh_interval")
            .HasMaxLength(10)
            .HasDefaultValue("300");

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
        builder.HasIndex(e => e.UserId)
            .HasDatabaseName("IX_DashboardWidget_UserId");

        builder.HasIndex(e => e.WidgetType)
            .HasDatabaseName("IX_DashboardWidget_WidgetType");
    }
}
