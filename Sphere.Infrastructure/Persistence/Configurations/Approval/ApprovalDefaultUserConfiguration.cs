using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Approval;

namespace Sphere.Infrastructure.Persistence.Configurations.Approval;

/// <summary>
/// EF Core configuration for ApprovalDefaultUser entity.
/// </summary>
public class ApprovalDefaultUserConfiguration : IEntityTypeConfiguration<ApprovalDefaultUser>
{
    public void Configure(EntityTypeBuilder<ApprovalDefaultUser> builder)
    {
        builder.ToTable("SPC_APPROVAL_DEFAULT_USER");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.Seq, e.ChgTypeId, e.AprovActionId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.Seq)
            .HasColumnName("seq")
            .IsRequired();

        builder.Property(e => e.ChgTypeId)
            .HasColumnName("chg_type_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.AprovActionId)
            .HasColumnName("aprov_action_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.Writer)
            .HasColumnName("writer")
            .HasMaxLength(50);

        builder.Property(e => e.UserList)
            .HasColumnName("user_list")
            .HasMaxLength(1000);

        builder.Property(e => e.ActiName)
            .HasColumnName("acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.OriginActiName)
            .HasColumnName("origin_acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.ReasonCode)
            .HasColumnName("reason_code")
            .HasMaxLength(40);

        builder.Property(e => e.ParallGroup)
            .HasColumnName("parall_group")
            .HasMaxLength(40);

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
        builder.HasIndex(e => new { e.DivSeq, e.ChgTypeId })
            .HasDatabaseName("IX_ApprovalDefaultUser_DivSeq_ChgTypeId");

        builder.HasIndex(e => new { e.DivSeq, e.AprovActionId })
            .HasDatabaseName("IX_ApprovalDefaultUser_DivSeq_AprovActionId");

        builder.HasIndex(e => new { e.DivSeq, e.Writer })
            .HasDatabaseName("IX_ApprovalDefaultUser_DivSeq_Writer");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_ApprovalDefaultUser_UseYn");
    }
}
