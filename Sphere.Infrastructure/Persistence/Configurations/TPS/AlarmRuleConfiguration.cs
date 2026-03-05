using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.TPS;

namespace Sphere.Infrastructure.Persistence.Configurations.TPS;

/// <summary>
/// EF Core configuration for AlarmRule entity.
/// Maps to SPC_ALM_RULE table.
/// </summary>
public class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRule>
{
    public void Configure(EntityTypeBuilder<AlarmRule> builder)
    {
        builder.ToTable("SPC_ALM_RULE");

        // Single Primary Key (RuleId)
        builder.HasKey(e => e.RuleId);

        // Column mappings
        builder.Property(e => e.RuleId)
            .HasColumnName("rule_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RuleName)
            .HasColumnName("rule_name")
            .HasMaxLength(100);

        builder.Property(e => e.RuleType)
            .HasColumnName("rule_type")
            .HasMaxLength(50);

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.Condition)
            .HasColumnName("condition")
            .HasMaxLength(1000);

        builder.Property(e => e.ThresholdValue)
            .HasColumnName("threshold_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.Operator)
            .HasColumnName("operator")
            .HasMaxLength(20);

        builder.Property(e => e.Severity)
            .HasColumnName("severity")
            .HasDefaultValue(1);

        builder.Property(e => e.NotifyUsers)
            .HasColumnName("notify_users")
            .HasMaxLength(2000);

        builder.Property(e => e.NotifyType)
            .HasColumnName("notify_type")
            .HasMaxLength(20)
            .HasDefaultValue("EMAIL");

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

        // Base entity properties
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

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
        builder.HasIndex(e => e.RuleType)
            .HasDatabaseName("IX_AlarmRule_RuleType");

        builder.HasIndex(e => e.SpecSysId)
            .HasDatabaseName("IX_AlarmRule_SpecSysId");

        builder.HasIndex(e => e.Severity)
            .HasDatabaseName("IX_AlarmRule_Severity");
    }
}
