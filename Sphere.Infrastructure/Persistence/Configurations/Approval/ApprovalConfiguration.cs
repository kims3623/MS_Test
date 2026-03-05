using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApprovalEntity = Sphere.Domain.Entities.Approval.Approval;

namespace Sphere.Infrastructure.Persistence.Configurations.Approval;

/// <summary>
/// EF Core configuration for Approval entity.
/// Maps to SPC_APROV_LIST table.
/// </summary>
public class ApprovalConfiguration : IEntityTypeConfiguration<ApprovalEntity>
{
    public void Configure(EntityTypeBuilder<ApprovalEntity> builder)
    {
        builder.ToTable("SPC_APROV_LIST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.AprovId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AprovId)
            .HasColumnName("aprov_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ChgTypeId)
            .HasColumnName("chg_type_id")
            .HasMaxLength(40);

        builder.Property(e => e.ChgTypeName)
            .HasColumnName("chg_type_name")
            .HasMaxLength(100);

        builder.Property(e => e.AprovActionId)
            .HasColumnName("aprov_action_id")
            .HasMaxLength(40);

        builder.Property(e => e.AprovActionName)
            .HasColumnName("aprov_action_name")
            .HasMaxLength(100);

        builder.Property(e => e.AprovState)
            .HasColumnName("aprov_state")
            .HasMaxLength(20);

        builder.Property(e => e.AprovStateName)
            .HasColumnName("aprov_state_name")
            .HasMaxLength(100);

        builder.Property(e => e.Title)
            .HasColumnName("title")
            .HasMaxLength(500);

        builder.Property(e => e.Contents)
            .HasColumnName("contents")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Writer)
            .HasColumnName("writer")
            .HasMaxLength(50);

        builder.Property(e => e.UserList)
            .HasColumnName("user_list")
            .HasMaxLength(1000);

        builder.Property(e => e.BatchId)
            .HasColumnName("batch_id")
            .HasMaxLength(40);

        builder.Property(e => e.AlmSysId)
            .HasColumnName("alm_sys_id")
            .HasMaxLength(40);

        builder.Property(e => e.AlmActionId)
            .HasColumnName("alm_action_id")
            .HasMaxLength(40);

        builder.Property(e => e.NotiFlag)
            .HasColumnName("noti_flag")
            .HasMaxLength(1);

        builder.Property(e => e.Remarks)
            .HasColumnName("remarks")
            .HasMaxLength(1000);

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
        builder.HasIndex(e => new { e.DivSeq, e.AprovState })
            .HasDatabaseName("IX_Approval_DivSeq_State");

        builder.HasIndex(e => new { e.DivSeq, e.Writer })
            .HasDatabaseName("IX_Approval_DivSeq_Writer");

        builder.HasIndex(e => new { e.DivSeq, e.ChgTypeId })
            .HasDatabaseName("IX_Approval_DivSeq_ChgTypeId");

        builder.HasIndex(e => e.CreateDate)
            .HasDatabaseName("IX_Approval_CreateDate");
    }
}
