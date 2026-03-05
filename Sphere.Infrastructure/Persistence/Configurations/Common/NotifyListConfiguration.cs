using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for NotifyList entity.
/// Maps to SPC_NOTIFY_LIST table.
/// </summary>
public class NotifyListConfiguration : IEntityTypeConfiguration<NotifyList>
{
    public void Configure(EntityTypeBuilder<NotifyList> builder)
    {
        builder.ToTable("SPC_NOTIFY_LIST");

        // Primary Key (auto-increment)
        builder.HasKey(e => e.TableSysId);

        // Column mappings - Identity
        builder.Property(e => e.TableSysId)
            .HasColumnName("table_sys_id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        // Module
        builder.Property(e => e.ModuleId)
            .HasColumnName("module_id")
            .HasMaxLength(40);

        builder.Property(e => e.NotiTypeId)
            .HasColumnName("noti_type_id")
            .HasMaxLength(40);

        // Alarm
        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(100);

        builder.Property(e => e.AlmActionId)
            .HasColumnName("alm_action_id")
            .HasMaxLength(100);

        // Approval
        builder.Property(e => e.AprovId)
            .HasColumnName("aprov_id")
            .HasMaxLength(100);

        builder.Property(e => e.AprovActionId)
            .HasColumnName("aprov_action_id")
            .HasMaxLength(100);

        // Content
        builder.Property(e => e.Receiver)
            .HasColumnName("receiver")
            .HasMaxLength(2000);

        builder.Property(e => e.Title)
            .HasColumnName("title")
            .HasMaxLength(500);

        builder.Property(e => e.Contents)
            .HasColumnName("contents")
            .HasMaxLength(4000);

        // Status
        builder.Property(e => e.SendYn)
            .HasColumnName("send_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ErrorYn)
            .HasColumnName("error_yn")
            .HasMaxLength(1)
            .HasDefaultValue("N");

        builder.Property(e => e.ErrorCode)
            .HasColumnName("error_code")
            .HasMaxLength(50);

        builder.Property(e => e.ErrorMsg)
            .HasColumnName("error_msg")
            .HasMaxLength(2000);

        // Common
        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(1)
            .HasDefaultValue("Y");

        // Audit Trail
        builder.Property(e => e.ActiName)
            .HasColumnName("acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.OriginActiName)
            .HasColumnName("origin_acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.ReasonCode)
            .HasColumnName("reason_code")
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

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
        builder.HasIndex(e => new { e.DivSeq, e.SendYn })
            .HasDatabaseName("IX_NotifyList_DivSeq_SendYn");

        builder.HasIndex(e => new { e.DivSeq, e.Receiver })
            .HasDatabaseName("IX_NotifyList_DivSeq_Receiver");

        builder.HasIndex(e => e.CreateDate)
            .HasDatabaseName("IX_NotifyList_CreateDate");

        builder.HasIndex(e => new { e.SendYn, e.ErrorYn })
            .HasDatabaseName("IX_NotifyList_SendYn_ErrorYn");
    }
}
