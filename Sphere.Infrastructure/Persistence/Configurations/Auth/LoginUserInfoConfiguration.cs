using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for LoginUserInfo entity.
/// </summary>
public class LoginUserInfoConfiguration : IEntityTypeConfiguration<LoginUserInfo>
{
    public void Configure(EntityTypeBuilder<LoginUserInfo> builder)
    {
        builder.ToTable("SPC_LOGIN_USER_INFO");

        // Primary Key - using UserId as single key since this represents active session
        builder.HasKey(e => e.UserId);

        // Column mappings
        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        builder.Property(e => e.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(100);

        builder.Property(e => e.UserNameK)
            .HasColumnName("user_name_k")
            .HasMaxLength(100);

        builder.Property(e => e.UserNameE)
            .HasColumnName("user_name_e")
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(e => e.DeptCode)
            .HasColumnName("dept_code")
            .HasMaxLength(40);

        builder.Property(e => e.DeptName)
            .HasColumnName("dept_name")
            .HasMaxLength(100);

        builder.Property(e => e.RoleCode)
            .HasColumnName("role_code")
            .HasMaxLength(40);

        builder.Property(e => e.RoleName)
            .HasColumnName("role_name")
            .HasMaxLength(100);

        builder.Property(e => e.Language)
            .HasColumnName("language")
            .HasMaxLength(10)
            .HasDefaultValue("ko-KR");

        builder.Property(e => e.IsAdmin)
            .HasColumnName("is_admin")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.SessionToken)
            .HasColumnName("session_token")
            .HasMaxLength(500);

        builder.Property(e => e.SessionExpiry)
            .HasColumnName("session_expiry");

        builder.Property(e => e.CurrentMenuId)
            .HasColumnName("current_menu_id")
            .HasMaxLength(40);

        builder.Property(e => e.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(45);

        builder.Property(e => e.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);

        builder.Property(e => e.LoginDate)
            .HasColumnName("login_date");

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
        builder.HasIndex(e => e.SessionToken)
            .HasDatabaseName("IX_LoginUserInfo_SessionToken");

        builder.HasIndex(e => e.SessionExpiry)
            .HasDatabaseName("IX_LoginUserInfo_SessionExpiry");

        builder.HasIndex(e => e.LoginDate)
            .HasDatabaseName("IX_LoginUserInfo_LoginDate");
    }
}
