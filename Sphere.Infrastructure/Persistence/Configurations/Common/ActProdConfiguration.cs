using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for ActProd entity.
/// </summary>
public class ActProdConfiguration : IEntityTypeConfiguration<ActProd>
{
    public void Configure(EntityTypeBuilder<ActProd> builder)
    {
        builder.ToTable("SPC_ACT_PROD");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ActProdId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActProdId)
            .HasColumnName("act_prod_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActProdName)
            .HasColumnName("act_prod_name")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdNameK)
            .HasColumnName("act_prod_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdNameE)
            .HasColumnName("act_prod_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdNameC)
            .HasColumnName("act_prod_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.ActProdNameV)
            .HasColumnName("act_prod_name_v")
            .HasMaxLength(200);

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
        builder.HasIndex(e => e.DivSeq)
            .HasDatabaseName("IX_ActProd_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_ActProd_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_ActProd_DspSeq");
    }
}
