using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for RunRuleMaster entity.
/// Maps to SPC_MNG_RULE_MST table with composite primary key.
/// </summary>
public class RunRuleMasterConfiguration : IEntityTypeConfiguration<RunRuleMaster>
{
    public void Configure(EntityTypeBuilder<RunRuleMaster> builder)
    {
        builder.ToTable("SPC_MNG_RULE_MST");

        // Composite Primary Key (DivSeq, RunruleId)
        builder.HasKey(e => new { e.DivSeq, e.RunRuleId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
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

        builder.Property(e => e.RunRuleNameK)
            .HasColumnName("runrule_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.RunRuleNameE)
            .HasColumnName("runrule_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.RunRuleType)
            .HasColumnName("runrule_type")
            .HasMaxLength(40);

        builder.Property(e => e.RuleNo)
            .HasColumnName("rule_no");

        builder.Property(e => e.PointsRequired)
            .HasColumnName("points_required");

        builder.Property(e => e.Zone)
            .HasColumnName("zone")
            .HasMaxLength(20);

        builder.Property(e => e.Condition)
            .HasColumnName("condition")
            .HasMaxLength(500);

        builder.Property(e => e.Severity)
            .HasColumnName("severity");

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
        builder.HasIndex(e => new { e.DivSeq, e.RunRuleType })
            .HasDatabaseName("IX_RunRuleMaster_DivSeq_RunRuleType");

        builder.HasIndex(e => new { e.DivSeq, e.RuleNo })
            .HasDatabaseName("IX_RunRuleMaster_DivSeq_RuleNo");
    }
}
