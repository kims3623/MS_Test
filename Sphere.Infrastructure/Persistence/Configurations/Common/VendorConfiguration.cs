using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Vendor entity.
/// </summary>
public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("SPC_VENDOR_INFO");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.VendorId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        builder.Property(e => e.VendorNameK)
            .HasColumnName("vendor_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.VendorNameE)
            .HasColumnName("vendor_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.VendorNameC)
            .HasColumnName("vendor_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.VendorNameV)
            .HasColumnName("vendor_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.VendorType)
            .HasColumnName("vendor_type")
            .HasMaxLength(40);

        builder.Property(e => e.VendorTypeName)
            .HasColumnName("vendor_type_name")
            .HasMaxLength(100);

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
        builder.HasIndex(e => new { e.DivSeq, e.VendorType })
            .HasDatabaseName("IX_Vendor_DivSeq_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Vendor_UseYn");
    }
}
