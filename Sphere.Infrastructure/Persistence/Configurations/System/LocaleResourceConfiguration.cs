using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for LocaleResource entity.
/// Maps to SPC_LOCALE_RESOURCE table.
/// </summary>
public class LocaleResourceConfiguration : IEntityTypeConfiguration<LocaleResource>
{
    public void Configure(EntityTypeBuilder<LocaleResource> builder)
    {
        builder.ToTable("SPC_LOCALE_RESOURCE");

        // Primary Key (ResourceKey only - entity doesn't have Locale field)
        builder.HasKey(e => e.ResourceKey);

        // Column mappings
        builder.Property(e => e.ResourceKey)
            .HasColumnName("resource_key")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.ResourceCategory)
            .HasColumnName("resource_category")
            .HasMaxLength(100);

        builder.Property(e => e.ValueK)
            .HasColumnName("value_k")
            .HasMaxLength(2000);

        builder.Property(e => e.ValueE)
            .HasColumnName("value_e")
            .HasMaxLength(2000);

        builder.Property(e => e.ValueC)
            .HasColumnName("value_c")
            .HasMaxLength(2000);

        builder.Property(e => e.ValueV)
            .HasColumnName("value_v")
            .HasMaxLength(2000);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

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
        builder.HasIndex(e => e.ResourceCategory)
            .HasDatabaseName("IX_LocaleResource_Category");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_LocaleResource_UseYn");
    }
}
