using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for SystemConfig entity.
/// Maps to SPC_SYSTEM_CONFIG table.
/// </summary>
public class SystemConfigConfiguration : IEntityTypeConfiguration<SystemConfig>
{
    public void Configure(EntityTypeBuilder<SystemConfig> builder)
    {
        builder.ToTable("SPC_SYSTEM_CONFIG");

        // Primary Key
        builder.HasKey(e => e.ConfigKey);

        // Column mappings
        builder.Property(e => e.ConfigKey)
            .HasColumnName("config_key")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Category)
            .HasColumnName("category")
            .HasMaxLength(50);

        builder.Property(e => e.ConfigValue)
            .HasColumnName("config_value")
            .HasMaxLength(2000);

        builder.Property(e => e.ValueType)
            .HasColumnName("value_type")
            .HasMaxLength(20)
            .HasDefaultValue("STRING");

        builder.Property(e => e.DefaultValue)
            .HasColumnName("default_value")
            .HasMaxLength(2000);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(e => e.EditableYn)
            .HasColumnName("editable_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        // Base entity columns
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
        builder.HasIndex(e => e.Category)
            .HasDatabaseName("IX_SystemConfig_Category");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_SystemConfig_UseYn");
    }
}
