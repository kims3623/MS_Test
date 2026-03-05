using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for BatchJobHistory entity.
/// </summary>
public class BatchJobHistoryConfiguration : IEntityTypeConfiguration<BatchJobHistory>
{
    public void Configure(EntityTypeBuilder<BatchJobHistory> builder)
    {
        builder.ToTable("SPC_BATCH_JOB_HIST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.HistoryId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.HistoryId)
            .HasColumnName("history_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.JobId)
            .HasColumnName("job_id")
            .HasMaxLength(40);

        builder.Property(e => e.JobName)
            .HasColumnName("job_name")
            .HasMaxLength(200);

        builder.Property(e => e.StartDate)
            .HasColumnName("start_date");

        builder.Property(e => e.EndDate)
            .HasColumnName("end_date");

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20);

        builder.Property(e => e.Result)
            .HasColumnName("result")
            .HasMaxLength(500);

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.ProcessedCount)
            .HasColumnName("processed_count")
            .HasDefaultValue(0);

        builder.Property(e => e.SuccessCount)
            .HasColumnName("success_count")
            .HasDefaultValue(0);

        builder.Property(e => e.FailCount)
            .HasColumnName("fail_count")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.JobId })
            .HasDatabaseName("IX_BatchJobHistory_DivSeq_JobId");

        builder.HasIndex(e => e.StartDate)
            .HasDatabaseName("IX_BatchJobHistory_StartDate");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_BatchJobHistory_Status");
    }
}
