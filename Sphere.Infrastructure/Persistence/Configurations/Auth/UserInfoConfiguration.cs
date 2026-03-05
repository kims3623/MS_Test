using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sphere.Domain.Entities.Auth;
using static Dapper.SqlMapper;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for UserInfo entity.
/// </summary>
public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    /// <summary>
    /// Converts DB NULL to string.Empty for non-nullable string properties mapped to nullable DB columns.
    /// </summary>
    private static readonly ValueConverter<string, string?> NullToEmptyConverter =
        new(v => v, v => v ?? string.Empty);

    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("SPC_USER_INFO");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.UserId });

        builder.ToTable("SPC_USER_INFO");

        builder.HasIndex(e => new { e.DivSeq, e.role_code }, "IX_UserInfo_DivSeq_RoleCode");

        builder.HasIndex(e => e.Email, "IX_UserInfo_Email");

        builder.HasIndex(e => e.UseYn, "IX_UserInfo_UseYn");

        builder.Property(e => e.DivSeq)
            .HasMaxLength(40)
            .IsUnicode(false)
            .HasColumnName("div_seq");
        builder.Property(e => e.UserId)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("user_id");
        builder.Property(e => e.CreateDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("create_date");
        builder.Property(e => e.CreateUserId)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("create_user_id");
        builder.Property(e => e.DeptId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("dept_code");
        builder.Property(e => e.DeptName)
            .HasMaxLength(100)
            .HasColumnName("dept_name");
        builder.Property(e => e.Email)
            .HasMaxLength(256)
            .IsUnicode(false)
            .HasColumnName("email");
        builder.Property(e => e.FailCount)
            .HasDefaultValue(0)
            .HasColumnName("fail_count");
        builder.Property(e => e.IsLocked)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasDefaultValue("N")
            .HasColumnName("is_locked");
        builder.Property(e => e.LastLoginDate)
            .HasColumnType("datetime")
            .HasColumnName("last_login_date");
        builder.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(256)
            .IsUnicode(false)
            .HasColumnName("password_hash");
        builder.Property(e => e.PositionName)
            .HasMaxLength(100)
            .HasColumnName("position_name");
        builder.Property(e => e.RoleName)
            .HasMaxLength(100)
            .HasColumnName("role_name");
        builder.Property(e => e.RowStatus)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("ROW_STATUS");
        builder.Property(e => e.Timezone)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasDefaultValue("Asia/Seoul")
            .HasColumnName("timezone");
        builder.Property(e => e.UpdateDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("update_date");
        builder.Property(e => e.UpdateUserId)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("update_user_id");
        builder.Property(e => e.UseYn)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasDefaultValue("Y")
            .HasColumnName("use_yn");
        builder.Property(e => e.UserName)
            .HasMaxLength(100)
            .HasColumnName("user_name");
        builder.Property(e => e.UserNameE)
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("user_name_e");
        builder.Property(e => e.UserNameK)
            .HasMaxLength(100)
            .HasColumnName("user_name_k");
    }
}
