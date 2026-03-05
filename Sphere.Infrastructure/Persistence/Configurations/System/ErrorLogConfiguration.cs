using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for ErrorLog entity.
/// Maps to SPC_ERROR_LOG table.
/// </summary>
public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.ToTable("SPC_ERROR_LOG");

        // Primary Key
        builder.HasKey(e => e.ErrorId);

        // Column mappings
        builder.Property(e => e.ErrorId)
            .HasColumnName("error_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ErrorType)
            .HasColumnName("error_type")
            .HasMaxLength(100);

        builder.Property(e => e.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(4000);

        builder.Property(e => e.StackTrace)
            .HasColumnName("stack_trace")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Source)
            .HasColumnName("source")
            .HasMaxLength(500);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.Url)
            .HasColumnName("url")
            .HasMaxLength(1000);

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50);

        builder.Property(e => e.ErrorDate)
            .HasColumnName("error_date");

        builder.Property(e => e.Severity)
            .HasColumnName("severity")
            .HasMaxLength(20)
            .HasDefaultValue("ERROR");

        builder.Property(e => e.ResolvedYn)
            .HasColumnName("resolved_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

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
        builder.HasIndex(e => e.ErrorDate)
            .HasDatabaseName("IX_ErrorLog_ErrorDate");

        builder.HasIndex(e => e.ErrorType)
            .HasDatabaseName("IX_ErrorLog_ErrorType");

        builder.HasIndex(e => e.Severity)
            .HasDatabaseName("IX_ErrorLog_Severity");

        builder.HasIndex(e => e.ResolvedYn)
            .HasDatabaseName("IX_ErrorLog_ResolvedYn");

        builder.HasIndex(e => e.UserId)
            .HasDatabaseName("IX_ErrorLog_UserId");
    }
}
