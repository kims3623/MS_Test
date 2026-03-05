using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for YieldStep entity.
/// Maps to SPC_YIELD_STEP table with composite primary key.
/// </summary>
public class YieldStepConfiguration : IEntityTypeConfiguration<YieldStep>
{
    public void Configure(EntityTypeBuilder<YieldStep> builder)
    {
        builder.ToTable("SPC_YIELD_STEP");

        // Composite Primary Key (DivSeq, YieldStepId)
        builder.HasKey(e => new { e.DivSeq, e.YieldStepId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.YieldStepId)
            .HasColumnName("yield_step_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.YieldStepName)
            .HasColumnName("yield_step_name")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepNameK)
            .HasColumnName("yield_step_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepNameE)
            .HasColumnName("yield_step_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepNameC)
            .HasColumnName("yield_step_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.YieldStepNameV)
            .HasColumnName("yield_step_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.StepSeq)
            .HasColumnName("step_seq");

        builder.Property(e => e.Formula)
            .HasColumnName("formula")
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        // Common audit fields from SphereEntity
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
        builder.HasIndex(e => new { e.DivSeq, e.StepSeq })
            .HasDatabaseName("IX_YieldStep_DivSeq_StepSeq");
    }
}
