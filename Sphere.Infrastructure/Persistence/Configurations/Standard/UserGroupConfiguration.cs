using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for UserGroup entity.
/// Maps to SPC_USER_GROUP table with composite primary key.
/// </summary>
public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable("SPC_USER_GROUP");

        // Composite Primary Key (DivSeq, UserGroupId, MtrlClassId, CustId, EndUserId)
        // Note: Entity uses GroupId, mapping to user_group_id
        builder.HasKey(e => new { e.DivSeq, e.GroupId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.GroupId)
            .HasColumnName("user_group_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.GroupName)
            .HasColumnName("group_name")
            .HasMaxLength(200);

        builder.Property(e => e.GroupNameK)
            .HasColumnName("group_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.GroupNameE)
            .HasColumnName("group_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.GroupType)
            .HasColumnName("group_type")
            .HasMaxLength(40);

        builder.Property(e => e.ParentGroupId)
            .HasColumnName("parent_group_id")
            .HasMaxLength(40);

        builder.Property(e => e.MemberUserIds)
            .HasColumnName("member_user_ids")
            .HasMaxLength(2000);

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
        builder.HasIndex(e => new { e.DivSeq, e.GroupType })
            .HasDatabaseName("IX_UserGroup_DivSeq_GroupType");

        builder.HasIndex(e => new { e.DivSeq, e.ParentGroupId })
            .HasDatabaseName("IX_UserGroup_DivSeq_ParentGroupId");
    }
}
