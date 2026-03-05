using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for SPH4020Table entity.
/// Maps to SPC_SPH4020_TABLE table.
/// </summary>
public class SPH4020TableConfiguration : IEntityTypeConfiguration<SPH4020Table>
{
    public void Configure(EntityTypeBuilder<SPH4020Table> builder)
    {
        builder.ToTable("SPC_SPH4020_TABLE");

        // Composite Primary Key (DivSeq, AlmSysId mapped to RecordId)
        builder.HasKey(e => new { e.DivSeq, e.RecordId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RecordId)
            .HasColumnName("record_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorId)
            .HasColumnName("vendor_id")
            .HasMaxLength(40);

        builder.Property(e => e.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(100);

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(100);

        builder.Property(e => e.PeriodFrom)
            .HasColumnName("period_from")
            .HasMaxLength(20);

        builder.Property(e => e.PeriodTo)
            .HasColumnName("period_to")
            .HasMaxLength(20);

        builder.Property(e => e.AlarmCount)
            .HasColumnName("alarm_count")
            .HasDefaultValue(0);

        builder.Property(e => e.OosCount)
            .HasColumnName("oos_count")
            .HasDefaultValue(0);

        builder.Property(e => e.AvgCpk)
            .HasColumnName("avg_cpk")
            .HasPrecision(18, 6);

        builder.Property(e => e.Status)
            .HasColumnName("status")
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
        builder.HasIndex(e => e.SpecSysId)
            .HasDatabaseName("IX_SPH4020Table_SpecSysId");

        builder.HasIndex(e => e.VendorId)
            .HasDatabaseName("IX_SPH4020Table_VendorId");

        builder.HasIndex(e => e.MtrlClassId)
            .HasDatabaseName("IX_SPH4020Table_MtrlClassId");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_SPH4020Table_Status");
    }
}
