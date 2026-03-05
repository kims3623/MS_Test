using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for SpecRunRuleRelation entity.
/// Maps to SPC_SPEC_RUNRULE_REL table with composite primary key.
/// </summary>
public class SpecRunRuleRelationConfiguration : IEntityTypeConfiguration<SpecRunRuleRelation>
{
    public void Configure(EntityTypeBuilder<SpecRunRuleRelation> builder)
    {
        builder.ToTable("SPC_SPEC_RUNRULE_REL");

        // Composite Primary Key (DivSeq, SpecSysId, DtType, RunruleId)
        // Note: Entity uses RunRuleId, mapping to documented runrule_id
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.RunRuleId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RunRuleId)
            .HasColumnName("runrule_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.RunRuleName)
            .HasColumnName("runrule_name")
            .HasMaxLength(200);

        builder.Property(e => e.ActiveYn)
            .HasColumnName("active_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.Priority)
            .HasColumnName("priority");

        builder.Property(e => e.TriggerAction)
            .HasColumnName("trigger_action")
            .HasMaxLength(200);

        builder.Property(e => e.NotifyYn)
            .HasColumnName("notify_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId })
            .HasDatabaseName("IX_SpecRunRuleRelation_DivSeq_SpecSysId");

        builder.HasIndex(e => new { e.DivSeq, e.RunRuleId })
            .HasDatabaseName("IX_SpecRunRuleRelation_DivSeq_RunRuleId");
    }
}
