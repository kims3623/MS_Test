using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for BatchJob entity.
/// </summary>
public class BatchJobConfiguration : IEntityTypeConfiguration<BatchJob>
{
    public void Configure(EntityTypeBuilder<BatchJob> builder)
    {
        builder.ToTable("SPC_BATCH_JOB");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.JobId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.JobId)
            .HasColumnName("job_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.JobName)
            .HasColumnName("job_name")
            .HasMaxLength(200);

        builder.Property(e => e.JobType)
            .HasColumnName("job_type")
            .HasMaxLength(50);

        builder.Property(e => e.CronExpression)
            .HasColumnName("cron_expression")
            .HasMaxLength(100);

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("ACTIVE");

        builder.Property(e => e.LastRunDate)
            .HasColumnName("last_run_date");

        builder.Property(e => e.NextRunDate)
            .HasColumnName("next_run_date");

        builder.Property(e => e.LastResult)
            .HasColumnName("last_result")
            .HasMaxLength(50);

        builder.Property(e => e.Parameters)
            .HasColumnName("parameters")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.RetryCount)
            .HasColumnName("retry_count")
            .HasDefaultValue(0);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.Status })
            .HasDatabaseName("IX_BatchJob_DivSeq_Status");

        builder.HasIndex(e => e.NextRunDate)
            .HasDatabaseName("IX_BatchJob_NextRunDate");
    }
}
