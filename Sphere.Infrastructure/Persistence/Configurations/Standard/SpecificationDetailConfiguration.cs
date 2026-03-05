using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for SpecificationDetail entity.
/// Maps to SPC_SPEC_DTL table with composite primary key.
/// </summary>
public class SpecificationDetailConfiguration : IEntityTypeConfiguration<SpecificationDetail>
{
    public void Configure(EntityTypeBuilder<SpecificationDetail> builder)
    {
        builder.ToTable("SPC_SPEC_DTL");

        // Composite Primary Key (DivSeq, SpecSysId, DtType, SpecVer)
        // Note: Entity has different property names, mapping to documented PK fields
        builder.HasKey(e => new { e.DivSeq, e.SpecSysId, e.DetailSeq });

        // Column mappings - Primary Keys
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.DetailSeq)
            .HasColumnName("detail_seq")
            .IsRequired();

        // Column mappings - Attributes
        builder.Property(e => e.ParamName)
            .HasColumnName("param_name")
            .HasMaxLength(100);

        builder.Property(e => e.ParamValue)
            .HasColumnName("param_value")
            .HasMaxLength(200);

        builder.Property(e => e.ParamType)
            .HasColumnName("param_type")
            .HasMaxLength(40);

        builder.Property(e => e.MinValue)
            .HasColumnName("min_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.MaxValue)
            .HasColumnName("max_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.TargetValue)
            .HasColumnName("target_value")
            .HasPrecision(18, 6);

        builder.Property(e => e.Unit)
            .HasColumnName("unit")
            .HasMaxLength(20);

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
        builder.HasIndex(e => new { e.DivSeq, e.SpecSysId })
            .HasDatabaseName("IX_SpecificationDetail_DivSeq_SpecSysId");
    }
}
