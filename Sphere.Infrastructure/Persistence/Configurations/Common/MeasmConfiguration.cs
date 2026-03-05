using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Measm entity.
/// </summary>
public class MeasmConfiguration : IEntityTypeConfiguration<Measm>
{
    public void Configure(EntityTypeBuilder<Measm> builder)
    {
        builder.ToTable("SPC_MEASM");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.MeasmId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MeasmId)
            .HasColumnName("measm_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MeasmName)
            .HasColumnName("measm_name")
            .HasMaxLength(200);

        builder.Property(e => e.MeasmNameK)
            .HasColumnName("measm_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.MeasmNameE)
            .HasColumnName("measm_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.MeasmNameC)
            .HasColumnName("measm_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.MeasmNameV)
            .HasColumnName("measm_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => e.DivSeq)
            .HasDatabaseName("IX_Measm_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Measm_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Measm_DspSeq");
    }
}
