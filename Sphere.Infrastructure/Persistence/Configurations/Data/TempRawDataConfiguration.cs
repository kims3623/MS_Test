using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for TempRawData entity.
/// Maps to SPC_TEMP_RAWDATA table.
/// </summary>
public class TempRawDataConfiguration : IEntityTypeConfiguration<TempRawData>
{
    public void Configure(EntityTypeBuilder<TempRawData> builder)
    {
        builder.ToTable("SPC_TEMP_RAWDATA");

        // Composite Primary Key: DivSeq, SpecSysId, WorkDate, Shift, ItemTypeId
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.WorkDate, e.Shift, e.TempId });

        // Column mappings - Primary Key fields
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.TempId)
            .HasColumnName("temp_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(20)
            .IsRequired();

        // Material Info
        builder.Property(e => e.MtrlId)
            .HasColumnName("mtrl_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlName)
            .HasColumnName("mtrl_name")
            .HasMaxLength(200);

        // Vendor Info
        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        // Raw Data Values
        builder.Property(e => e.RawDataValue)
            .HasColumnName("raw_data")
            .HasMaxLength(4000);

        builder.Property(e => e.InputQty)
            .HasColumnName("input_qty")
            .HasMaxLength(20);

        builder.Property(e => e.DefectQty)
            .HasColumnName("defect_qty")
            .HasMaxLength(20);

        // Upload Info
        builder.Property(e => e.UploadStatus)
            .HasColumnName("upload_status")
            .HasMaxLength(20);

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(1000);

        builder.Property(e => e.BatchId)
            .HasColumnName("batch_id")
            .HasMaxLength(40);

        builder.Property(e => e.UploadDate)
            .HasColumnName("upload_date");

        // Common Audit Fields
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
        builder.HasIndex(e => new { e.DivSeq, e.BatchId })
            .HasDatabaseName("IX_TempRawData_DivSeq_BatchId");

        builder.HasIndex(e => new { e.DivSeq, e.UploadStatus })
            .HasDatabaseName("IX_TempRawData_DivSeq_UploadStatus");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_TempRawData_UseYn");
    }
}
