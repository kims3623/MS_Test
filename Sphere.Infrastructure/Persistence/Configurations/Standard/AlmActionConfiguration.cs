using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for AlmAction entity.
/// Maps to SPC_ALARM_ACTION table with single primary key.
/// </summary>
public class AlmActionConfiguration : IEntityTypeConfiguration<AlmAction>
{
    public void Configure(EntityTypeBuilder<AlmAction> builder)
    {
        builder.ToTable("SPC_ALARM_ACTION");

        // Single Primary Key (AlmActionId)
        // Note: This entity does not use DivSeq as part of PK per mapping doc
        builder.HasKey(e => e.AlmActionId);

        // Column mappings - Primary Key
        builder.Property(e => e.AlmActionId)
            .HasColumnName("alm_action_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - DivSeq (not part of PK but still needed)
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.AlmActionName)
            .HasColumnName("alm_action_name")
            .HasMaxLength(200);

        builder.Property(e => e.AlmActionNameK)
            .HasColumnName("alm_action_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.AlmActionNameE)
            .HasColumnName("alm_action_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ActionType)
            .HasColumnName("action_type")
            .HasMaxLength(40);

        builder.Property(e => e.ActionScript)
            .HasColumnName("action_script")
            .HasMaxLength(2000);

        builder.Property(e => e.Parameters)
            .HasColumnName("parameters")
            .HasMaxLength(1000);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq");

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
        builder.HasIndex(e => e.ActionType)
            .HasDatabaseName("IX_AlmAction_ActionType");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_AlmAction_DspSeq");
    }
}
