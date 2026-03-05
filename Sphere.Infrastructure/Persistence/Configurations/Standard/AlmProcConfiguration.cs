using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for AlmProc entity.
/// Maps to SPC_ALM_PROC table with composite primary key.
/// </summary>
public class AlmProcConfiguration : IEntityTypeConfiguration<AlmProc>
{
    public void Configure(EntityTypeBuilder<AlmProc> builder)
    {
        // TODO: SPC_ALM_PROC table not found in DB script - verify actual table name
        builder.ToTable("SPC_ALM_PROC");

        // Composite Primary Key (DivSeq, AlmProcId)
        builder.HasKey(e => new { e.DivSeq, e.AlmProcId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AlmProcId)
            .HasColumnName("alm_proc_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.AlmProcName)
            .HasColumnName("alm_proc_name")
            .HasMaxLength(200);

        builder.Property(e => e.AlmProcNameK)
            .HasColumnName("alm_proc_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.AlmProcNameE)
            .HasColumnName("alm_proc_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ProcType)
            .HasColumnName("proc_type")
            .HasMaxLength(40);

        builder.Property(e => e.Priority)
            .HasColumnName("priority");

        builder.Property(e => e.NotifyMethod)
            .HasColumnName("notify_method")
            .HasMaxLength(100);

        builder.Property(e => e.EscalationRule)
            .HasColumnName("escalation_rule")
            .HasMaxLength(500);

        builder.Property(e => e.ResponseTime)
            .HasColumnName("response_time");

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
        builder.HasIndex(e => new { e.DivSeq, e.ProcType })
            .HasDatabaseName("IX_AlmProc_DivSeq_ProcType");

        builder.HasIndex(e => new { e.DivSeq, e.Priority })
            .HasDatabaseName("IX_AlmProc_DivSeq_Priority");
    }
}
