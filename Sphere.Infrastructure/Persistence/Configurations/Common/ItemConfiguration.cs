using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Item entity.
/// </summary>
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("SPC_ITEM");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ItemId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ItemId)
            .HasColumnName("item_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ItemName)
            .HasColumnName("item_name")
            .HasMaxLength(200);

        builder.Property(e => e.ItemNameK)
            .HasColumnName("item_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.ItemNameE)
            .HasColumnName("item_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ItemNameC)
            .HasColumnName("item_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.ItemNameV)
            .HasColumnName("item_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.ItemType)
            .HasColumnName("item_type")
            .HasMaxLength(40);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.Unit)
            .HasColumnName("unit")
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.RowStatus)
            .HasColumnName("ROW_STATUS")
            .HasMaxLength(10);

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
        builder.HasIndex(e => new { e.DivSeq, e.ItemType })
            .HasDatabaseName("IX_Item_DivSeq_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Item_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Item_DspSeq");
    }
}
