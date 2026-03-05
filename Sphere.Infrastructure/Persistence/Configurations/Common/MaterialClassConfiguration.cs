using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for MaterialClass entity.
/// </summary>
public class MaterialClassConfiguration : IEntityTypeConfiguration<MaterialClass>
{
    public void Configure(EntityTypeBuilder<MaterialClass> builder)
    {
        builder.ToTable("SPC_MTRL_CLASS_MAP");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.MtrlClassId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MtrlClassId)
            .HasColumnName("mtrl_class_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MtrlClassName)
            .HasColumnName("mtrl_class_name")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassNameK)
            .HasColumnName("mtrl_class_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassNameE)
            .HasColumnName("mtrl_class_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassNameC)
            .HasColumnName("mtrl_class_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassNameV)
            .HasColumnName("mtrl_class_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.MtrlClassGroupId)
            .HasColumnName("mtrl_class_group_id")
            .HasMaxLength(40);

        builder.Property(e => e.MtrlClassGroupName)
            .HasColumnName("mtrl_class_group_name")
            .HasMaxLength(200);

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
        builder.HasIndex(e => new { e.DivSeq, e.MtrlClassGroupId })
            .HasDatabaseName("IX_MtrlClass_DivSeq_GroupId");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_MtrlClass_UseYn");
    }
}
