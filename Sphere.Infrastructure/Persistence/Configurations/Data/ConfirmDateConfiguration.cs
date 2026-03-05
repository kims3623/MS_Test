using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for ConfirmDate entity.
/// Maps to SPC_CONFIRM_DATE table.
/// </summary>
public class ConfirmDateConfiguration : IEntityTypeConfiguration<ConfirmDate>
{
    public void Configure(EntityTypeBuilder<ConfirmDate> builder)
    {
        builder.ToTable("SPC_CONFIRM_DATE");

        // Composite Primary Key: DivSeq, MtrlClassId, VendorId, StatTypeId
        // Note: Entity has SpecSysId, WorkDate, Shift as PKs based on entity definition
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.WorkDate, e.Shift });

        // Column mappings - Primary Key fields
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
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

        // Confirm Info
        builder.Property(e => e.ConfirmYn)
            .HasColumnName("confirm_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ConfirmUserId)
            .HasColumnName("confirm_user_id")
            .HasMaxLength(50);

        builder.Property(e => e.ConfirmUserName)
            .HasColumnName("confirm_user_name")
            .HasMaxLength(100);

        builder.Property(e => e.ConfirmDateTime)
            .HasColumnName("confirm_date_time");

        builder.Property(e => e.Remarks)
            .HasColumnName("remarks")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.ConfirmYn })
            .HasDatabaseName("IX_ConfirmDate_DivSeq_ConfirmYn");

        builder.HasIndex(e => new { e.DivSeq, e.WorkDate })
            .HasDatabaseName("IX_ConfirmDate_DivSeq_WorkDate");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_ConfirmDate_UseYn");
    }
}
