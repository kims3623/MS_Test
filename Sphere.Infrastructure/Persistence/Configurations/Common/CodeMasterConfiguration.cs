using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for CodeMaster entity.
/// </summary>
public class CodeMasterConfiguration : IEntityTypeConfiguration<CodeMaster>
{
    public void Configure(EntityTypeBuilder<CodeMaster> builder)
    {
        builder.ToTable("SPC_CODE_MST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.CodeId, e.CodeClassId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CodeId)
            .HasColumnName("code_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CodeClassId)
            .HasColumnName("code_class_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CodeAlias)
            .HasColumnName("code_alias")
            .HasMaxLength(100);

        builder.Property(e => e.CodeNameK)
            .HasColumnName("code_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameE)
            .HasColumnName("code_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameC)
            .HasColumnName("code_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameV)
            .HasColumnName("code_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.CodeNameLocale)
            .HasColumnName("code_name_locale")
            .HasMaxLength(200);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.CodeOpt)
            .HasColumnName("code_opt")
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

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
        builder.HasIndex(e => new { e.DivSeq, e.CodeClassId })
            .HasDatabaseName("IX_CodeMaster_DivSeq_ClassId");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_CodeMaster_UseYn");
    }
}
