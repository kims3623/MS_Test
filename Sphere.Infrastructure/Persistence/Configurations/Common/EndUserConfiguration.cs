using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for EndUser entity.
/// </summary>
public class EndUserConfiguration : IEntityTypeConfiguration<EndUser>
{
    public void Configure(EntityTypeBuilder<EndUser> builder)
    {
        builder.ToTable("SPC_END_USER");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.EndUserId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.EndUserId)
            .HasColumnName("end_user_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.EndUserName)
            .HasColumnName("end_user_name")
            .HasMaxLength(200);

        builder.Property(e => e.EndUserNameK)
            .HasColumnName("end_user_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.EndUserNameE)
            .HasColumnName("end_user_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.EndUserNameC)
            .HasColumnName("end_user_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.EndUserNameV)
            .HasColumnName("end_user_name_v")
            .HasMaxLength(200);

        builder.Property(e => e.EndUserType)
            .HasColumnName("end_user_type")
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
        builder.HasIndex(e => new { e.DivSeq, e.EndUserType })
            .HasDatabaseName("IX_EndUser_DivSeq_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_EndUser_UseYn");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_EndUser_DspSeq");
    }
}
