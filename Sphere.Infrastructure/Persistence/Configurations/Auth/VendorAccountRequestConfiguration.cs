using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for VendorAccountRequest entity.
/// Maps to SPC_VENDOR_ACCOUNT_REQUEST table.
/// </summary>
public class VendorAccountRequestConfiguration : IEntityTypeConfiguration<VendorAccountRequest>
{
    public void Configure(EntityTypeBuilder<VendorAccountRequest> builder)
    {
        builder.ToTable("SPC_VENDOR_ACCOUNT_REQUEST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.RequestId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RequestId)
            .HasColumnName("request_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.BusinessNumber)
            .HasColumnName("business_number")
            .HasMaxLength(50);

        builder.Property(e => e.ContactName)
            .HasColumnName("contact_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.ContactEmail)
            .HasColumnName("contact_email")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.ContactPhone)
            .HasColumnName("contact_phone")
            .HasMaxLength(20);

        builder.Property(e => e.Address)
            .HasColumnName("address")
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("PENDING");

        builder.Property(e => e.RequestedAt)
            .HasColumnName("requested_at");

        builder.Property(e => e.ProcessedAt)
            .HasColumnName("processed_at");

        builder.Property(e => e.ProcessedBy)
            .HasColumnName("processed_by")
            .HasMaxLength(50);

        builder.Property(e => e.RejectionReason)
            .HasColumnName("rejection_reason")
            .HasMaxLength(1000);

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

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
        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_VendorAccountRequest_Status");

        builder.HasIndex(e => e.ContactEmail)
            .HasDatabaseName("IX_VendorAccountRequest_ContactEmail");

        builder.HasIndex(e => new { e.DivSeq, e.VendorName })
            .HasDatabaseName("IX_VendorAccountRequest_DivSeq_VendorName");
    }
}
