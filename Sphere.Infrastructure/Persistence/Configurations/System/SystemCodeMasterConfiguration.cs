using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for SystemCodeMaster entity.
/// Maps to SPC_SYSTEM_CODE_MST table.
/// </summary>
public class SystemCodeMasterConfiguration : IEntityTypeConfiguration<SystemCodeMaster>
{
    public void Configure(EntityTypeBuilder<SystemCodeMaster> builder)
    {
        builder.ToTable("SPC_SYSTEM_CODE_MST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.SysCodeId, e.SysCodeClassId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SysCodeId)
            .HasColumnName("sys_code_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SysCodeClassId)
            .HasColumnName("sys_code_class_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CodeName)
            .HasColumnName("code_name")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameK)
            .HasColumnName("code_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameE)
            .HasColumnName("code_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.CodeValue)
            .HasColumnName("code_value")
            .HasMaxLength(500);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        // Base entity columns
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
        builder.HasIndex(e => new { e.DivSeq, e.SysCodeClassId })
            .HasDatabaseName("IX_SystemCodeMaster_DivSeq_ClassId");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_SystemCodeMaster_UseYn");
    }
}
