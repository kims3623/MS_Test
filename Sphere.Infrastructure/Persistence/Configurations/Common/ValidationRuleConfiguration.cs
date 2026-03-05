using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for ValidationRule entity.
/// </summary>
public class ValidationRuleConfiguration : IEntityTypeConfiguration<ValidationRule>
{
    public void Configure(EntityTypeBuilder<ValidationRule> builder)
    {
        builder.ToTable("SPC_VALIDATION_RULE");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.RuleId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RuleId)
            .HasColumnName("rule_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.RuleName)
            .HasColumnName("rule_name")
            .HasMaxLength(100);

        builder.Property(e => e.EntityType)
            .HasColumnName("entity_type")
            .HasMaxLength(100);

        builder.Property(e => e.FieldName)
            .HasColumnName("field_name")
            .HasMaxLength(100);

        builder.Property(e => e.RuleType)
            .HasColumnName("rule_type")
            .HasMaxLength(40);

        builder.Property(e => e.RuleExpression)
            .HasColumnName("rule_expression")
            .HasMaxLength(1000);

        builder.Property(e => e.ErrorMessageK)
            .HasColumnName("error_message_k")
            .HasMaxLength(500);

        builder.Property(e => e.ErrorMessageE)
            .HasColumnName("error_message_e")
            .HasMaxLength(500);

        builder.Property(e => e.Priority)
            .HasColumnName("priority")
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
        builder.HasIndex(e => new { e.DivSeq, e.EntityType })
            .HasDatabaseName("IX_ValidationRule_DivSeq_EntityType");

        builder.HasIndex(e => new { e.DivSeq, e.RuleType })
            .HasDatabaseName("IX_ValidationRule_DivSeq_RuleType");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_ValidationRule_UseYn");
    }
}
