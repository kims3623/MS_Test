using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// EF Core configuration for AuthorityFilter entity.
/// </summary>
public class AuthorityFilterConfiguration : IEntityTypeConfiguration<AuthorityFilter>
{
    public void Configure(EntityTypeBuilder<AuthorityFilter> builder)
    {
        builder.ToTable("SPC_AUTHORITY_FILTER");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.UserId, e.FilterType });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FilterType)
            .HasColumnName("filter_type")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.FilterValue)
            .HasColumnName("filter_value")
            .HasMaxLength(100);

        builder.Property(e => e.FilterValueName)
            .HasColumnName("filter_value_name")
            .HasMaxLength(200);

        builder.Property(e => e.AccessLevel)
            .HasColumnName("access_level")
            .HasMaxLength(20)
            .HasDefaultValue("ALL");

        builder.Property(e => e.VendorIds)
            .HasColumnName("vendor_ids")
            .HasMaxLength(2000);

        builder.Property(e => e.MtrlClassIds)
            .HasColumnName("mtrl_class_ids")
            .HasMaxLength(2000);

        builder.Property(e => e.ProjectIds)
            .HasColumnName("project_ids")
            .HasMaxLength(2000);

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_AuthorityFilter_DivSeq_UserId");

        builder.HasIndex(e => new { e.DivSeq, e.FilterType })
            .HasDatabaseName("IX_AuthorityFilter_DivSeq_FilterType");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_AuthorityFilter_UseYn");
    }
}
