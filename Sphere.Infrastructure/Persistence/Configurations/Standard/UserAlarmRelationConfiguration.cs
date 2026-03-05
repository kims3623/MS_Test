using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for UserAlarmRelation entity.
/// Maps to SPC_USER_ALM_REL table with composite primary key.
/// </summary>
public class UserAlarmRelationConfiguration : IEntityTypeConfiguration<UserAlarmRelation>
{
    public void Configure(EntityTypeBuilder<UserAlarmRelation> builder)
    {
        builder.ToTable("SPC_USER_ALM_REL");

        // Composite Primary Key (DivSeq, AlmProcId, AlmActionId, PicTypeId, VendorId, MtrlClassId, UserGroupId)
        // Note: Entity uses UserId and AlarmTypeId with different structure
        builder.HasKey(e => new { e.DivSeq, e.UserId, e.AlarmTypeId, e.SpecSysId });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.AlarmTypeId)
            .HasColumnName("alarm_type_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(100);

        builder.Property(e => e.AlarmTypeName)
            .HasColumnName("alarm_type_name")
            .HasMaxLength(200);

        builder.Property(e => e.EmailYn)
            .HasColumnName("email_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.SmsYn)
            .HasColumnName("sms_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.PushYn)
            .HasColumnName("push_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        builder.Property(e => e.Priority)
            .HasColumnName("priority");

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
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_UserAlarmRelation_DivSeq_UserId");

        builder.HasIndex(e => new { e.DivSeq, e.AlarmTypeId })
            .HasDatabaseName("IX_UserAlarmRelation_DivSeq_AlarmTypeId");

        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId })
            .HasDatabaseName("IX_UserAlarmRelation_DivSeq_SpecSysId");
    }
}
