using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Lookup entity.
/// </summary>
public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
{
    public void Configure(EntityTypeBuilder<Lookup> builder)
    {
        builder.ToTable("SPC_LOOKUP");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.LookupId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.LookupId)
            .HasColumnName("lookup_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.LookupType)
            .HasColumnName("lookup_type")
            .HasMaxLength(50);

        builder.Property(e => e.LookupCode)
            .HasColumnName("lookup_code")
            .HasMaxLength(50);

        builder.Property(e => e.LookupName)
            .HasColumnName("lookup_name")
            .HasMaxLength(200);

        builder.Property(e => e.LookupNameK)
            .HasColumnName("lookup_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.LookupNameE)
            .HasColumnName("lookup_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.LookupValue)
            .HasColumnName("lookup_value")
            .HasMaxLength(500);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq")
            .HasDefaultValue(0);

        builder.Property(e => e.ParentCode)
            .HasColumnName("parent_code")
            .HasMaxLength(50);

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
        builder.HasIndex(e => new { e.DivSeq, e.LookupType })
            .HasDatabaseName("IX_Lookup_DivSeq_LookupType");

        builder.HasIndex(e => e.LookupCode)
            .HasDatabaseName("IX_Lookup_LookupCode");
    }
}
