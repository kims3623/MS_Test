using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for DefaultManagementRule entity.
/// Maps to SPC_DEFAULT_MNG_RULE table with composite primary key.
/// </summary>
public class DefaultManagementRuleConfiguration : IEntityTypeConfiguration<DefaultManagementRule>
{
    public void Configure(EntityTypeBuilder<DefaultManagementRule> builder)
    {
        builder.ToTable("SPC_DEFAULT_MNG_RULE");

        // Composite Primary Key (DivSeq, RuleId)
        builder.HasKey(e => new { e.DivSeq, e.RuleId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RuleId)
            .HasColumnName("rule_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.RuleType)
            .HasColumnName("rule_type")
            .HasMaxLength(40);

        builder.Property(e => e.RuleName)
            .HasColumnName("rule_name")
            .HasMaxLength(200);

        builder.Property(e => e.RuleNameK)
            .HasColumnName("rule_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.RuleNameE)
            .HasColumnName("rule_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.TargetEntity)
            .HasColumnName("target_entity")
            .HasMaxLength(100);

        builder.Property(e => e.Condition)
            .HasColumnName("condition")
            .HasMaxLength(1000);

        builder.Property(e => e.Action)
            .HasColumnName("action")
            .HasMaxLength(500);

        builder.Property(e => e.Priority)
            .HasColumnName("priority");

        builder.Property(e => e.EffectiveFrom)
            .HasColumnName("effective_from");

        builder.Property(e => e.EffectiveTo)
            .HasColumnName("effective_to");

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
        builder.HasIndex(e => new { e.DivSeq, e.RuleType })
            .HasDatabaseName("IX_DefaultManagementRule_DivSeq_RuleType");

        builder.HasIndex(e => new { e.DivSeq, e.TargetEntity })
            .HasDatabaseName("IX_DefaultManagementRule_DivSeq_TargetEntity");
    }
}
