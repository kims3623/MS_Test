using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for ApiLog entity.
/// Maps to SPC_API_LOG table.
/// </summary>
public class ApiLogConfiguration : IEntityTypeConfiguration<ApiLog>
{
    public void Configure(EntityTypeBuilder<ApiLog> builder)
    {
        builder.ToTable("SPC_API_LOG");

        // Primary Key
        builder.HasKey(e => e.LogId);

        // Column mappings
        builder.Property(e => e.LogId)
            .HasColumnName("log_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Endpoint)
            .HasColumnName("endpoint")
            .HasMaxLength(500);

        builder.Property(e => e.Method)
            .HasColumnName("method")
            .HasMaxLength(10);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.RequestBody)
            .HasColumnName("request_body")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.ResponseBody)
            .HasColumnName("response_body")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.StatusCode)
            .HasColumnName("status_code");

        builder.Property(e => e.DurationMs)
            .HasColumnName("duration_ms");

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50);

        builder.Property(e => e.RequestDate)
            .HasColumnName("request_date");

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(2000);

        // Base entity columns
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
        builder.HasIndex(e => e.UserId)
            .HasDatabaseName("IX_ApiLog_UserId");

        builder.HasIndex(e => e.RequestDate)
            .HasDatabaseName("IX_ApiLog_RequestDate");

        builder.HasIndex(e => e.Endpoint)
            .HasDatabaseName("IX_ApiLog_Endpoint");

        builder.HasIndex(e => e.StatusCode)
            .HasDatabaseName("IX_ApiLog_StatusCode");
    }
}
