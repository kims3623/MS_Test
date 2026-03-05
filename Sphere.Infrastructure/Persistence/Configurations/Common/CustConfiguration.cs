using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Cust entity.
/// </summary>
public class CustConfiguration : IEntityTypeConfiguration<Cust>
{
    public void Configure(EntityTypeBuilder<Cust> builder)
    {
        builder.ToTable("SPC_CUST");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.CustId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CustId)
            .HasColumnName("cust_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.CustName)
            .HasColumnName("cust_name")
            .HasMaxLength(200);

        builder.Property(e => e.CustNameK)
            .HasColumnName("cust_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.CustNameE)
            .HasColumnName("cust_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.CustNameC)
            .HasColumnName("cust_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.CustNameV)
            .HasColumnName("cust_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.CustType)
            .HasColumnName("cust_type")
            .HasMaxLength(40);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

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
        builder.HasIndex(e => new { e.DivSeq, e.CustType })
            .HasDatabaseName("IX_Cust_DivSeq_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Cust_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_Cust_DspSeq");
    }
}
