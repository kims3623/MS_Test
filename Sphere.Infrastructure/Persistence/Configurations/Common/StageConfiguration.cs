using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Stage entity.
/// </summary>
public class StageConfiguration : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
    {
        builder.ToTable("SPC_STAGE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.StageId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.StageId)
            .HasColumnName("stage_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.StageName)
            .HasColumnName("stage_name")
            .HasMaxLength(200);

        builder.Property(e => e.StageNameK)
            .HasColumnName("stage_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.StageNameE)
            .HasColumnName("stage_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.StageNameC)
            .HasColumnName("stage_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.StageNameV)
            .HasColumnName("stage_name_v")
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
            .HasDatabaseName("IX_Stage_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Stage_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Stage_DspSeq");
    }
}
