using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Shift entity.
/// </summary>
public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("SPC_SHIFT");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ShiftId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ShiftId)
            .HasColumnName("shift")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ShiftName)
            .HasColumnName("shift_name")
            .HasMaxLength(200);

        builder.Property(e => e.ShiftNameK)
            .HasColumnName("shift_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.ShiftNameE)
            .HasColumnName("shift_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ShiftNameC)
            .HasColumnName("shift_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.ShiftNameV)
            .HasColumnName("shift_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.StartTime)
            .HasColumnName("start_time")
            .HasMaxLength(20);

        builder.Property(e => e.EndTime)
            .HasColumnName("end_time")
            .HasMaxLength(20);

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
        builder.HasIndex(e => e.DivSeq)
            .HasDatabaseName("IX_Shift_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Shift_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Shift_DspSeq");
    }
}
