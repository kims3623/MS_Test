using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for UserSession entity.
/// Mapped to SPC_USER_SESSION table.
/// </summary>
public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("SPC_USER_SESSION");

        // Primary Key
        builder.HasKey(e => e.SessionId);

        // Column mappings (matches actual DB structure)
        builder.Property(e => e.SessionId)
            .HasColumnName("session_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.RefreshToken)
            .HasColumnName("refresh_token")
            .HasMaxLength(500);

        builder.Property(e => e.ExpiresAt)
            .HasColumnName("expires_at");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50);

        builder.Property(e => e.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);

        builder.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        // Indexes
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_UserSession_DivSeq_UserId");

        builder.HasIndex(e => e.RefreshToken)
            .HasDatabaseName("IX_UserSession_RefreshToken");

        builder.HasIndex(e => e.ExpiresAt)
            .HasDatabaseName("IX_UserSession_ExpiresAt");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_UserSession_IsActive");
    }
}
