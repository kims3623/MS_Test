using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Line entity.
/// </summary>
public class LineConfiguration : IEntityTypeConfiguration<Line>
{
    public void Configure(EntityTypeBuilder<Line> builder)
    {
        builder.ToTable("SPC_LINE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.LineId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.LineId)
            .HasColumnName("line")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.LineName)
            .HasColumnName("line_name")
            .HasMaxLength(200);

        builder.Property(e => e.LineNameK)
            .HasColumnName("line_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.LineNameE)
            .HasColumnName("line_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.LineNameC)
            .HasColumnName("line_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.LineNameV)
            .HasColumnName("line_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

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
            .HasDatabaseName("IX_Line_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Line_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Line_DspSeq");
    }
}
