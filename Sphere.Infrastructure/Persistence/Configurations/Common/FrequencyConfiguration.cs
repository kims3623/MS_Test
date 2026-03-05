using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Frequency entity.
/// </summary>
public class FrequencyConfiguration : IEntityTypeConfiguration<Frequency>
{
    public void Configure(EntityTypeBuilder<Frequency> builder)
    {
        builder.ToTable("SPC_FREQUENCY");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.FrequencyId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FrequencyId)
            .HasColumnName("frequency")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FrequencyName)
            .HasColumnName("frequency_name")
            .HasMaxLength(200);

        builder.Property(e => e.FrequencyNameK)
            .HasColumnName("frequency_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.FrequencyNameE)
            .HasColumnName("frequency_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.FrequencyNameC)
            .HasColumnName("frequency_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.FrequencyNameV)
            .HasColumnName("frequency_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.FrequencyValue)
            .HasColumnName("frequency_value")
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
            .HasDatabaseName("IX_Frequency_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Frequency_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Frequency_DspSeq");
    }
}
