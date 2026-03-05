using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Step entity.
/// </summary>
public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.ToTable("SPC_STEP");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.StepId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.StepId)
            .HasColumnName("step_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.StepName)
            .HasColumnName("step_name")
            .HasMaxLength(200);

        builder.Property(e => e.StepNameK)
            .HasColumnName("step_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.StepNameE)
            .HasColumnName("step_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.StepNameC)
            .HasColumnName("step_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.StepNameV)
            .HasColumnName("step_name_v")
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
            .HasDatabaseName("IX_Step_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Step_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Step_DspSeq");
    }
}
