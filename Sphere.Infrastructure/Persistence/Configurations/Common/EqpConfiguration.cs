using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Eqp entity.
/// </summary>
public class EqpConfiguration : IEntityTypeConfiguration<Eqp>
{
    public void Configure(EntityTypeBuilder<Eqp> builder)
    {
        builder.ToTable("SPC_EQP");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.EqpId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.EqpId)
            .HasColumnName("eqp_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.EqpName)
            .HasColumnName("eqp_name")
            .HasMaxLength(200);

        builder.Property(e => e.EqpNameK)
            .HasColumnName("eqp_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.EqpNameE)
            .HasColumnName("eqp_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.EqpNameC)
            .HasColumnName("eqp_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.EqpNameV)
            .HasColumnName("eqp_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.EqpType)
            .HasColumnName("eqp_type")
            .HasMaxLength(40);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.EqpType })
            .HasDatabaseName("IX_Eqp_DivSeq_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Eqp_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Eqp_DspSeq");
    }
}
