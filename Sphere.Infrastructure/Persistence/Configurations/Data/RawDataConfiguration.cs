using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Data;

namespace Sphere.Infrastructure.Persistence.Configurations.Data;

/// <summary>
/// EF Core configuration for RawData entity.
/// Maps to SPC_RAWDATA table with PK (table_sys_id, update_date).
/// </summary>
public class RawDataConfiguration : IEntityTypeConfiguration<RawData>
{
    public void Configure(EntityTypeBuilder<RawData> builder)
    {
        builder.ToTable("SPC_RAWDATA");

        // Actual DB PK: (table_sys_id IDENTITY, update_date) — partitioned table
        builder.HasKey(e => new { e.TableSysId, e.UpdateDate });

        // IDENTITY column
        builder.Property(e => e.TableSysId)
            .HasColumnName("table_sys_id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SpecSysId)
            .HasColumnName("spec_sys_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.WorkDate)
            .HasColumnName("work_date")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Shift)
            .HasColumnName("shift")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.MesmCnt)
            .HasColumnName("mesm_cnt")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.SplId)
            .HasColumnName("spl_id")
            .HasMaxLength(40);

        builder.Property(e => e.LotName)
            .HasColumnName("lot_name")
            .HasMaxLength(40);

        builder.Property(e => e.Stage)
            .HasColumnName("stage")
            .HasMaxLength(40);

        builder.Property(e => e.Frequency)
            .HasColumnName("frequency")
            .HasMaxLength(40);

        builder.Property(e => e.NgCode)
            .HasColumnName("ng_code")
            .HasMaxLength(40);

        builder.Property(e => e.EqpId)
            .HasColumnName("eqp_id")
            .HasMaxLength(40);

        builder.Property(e => e.RawDataValue)
            .HasColumnName("raw_data")
            .HasMaxLength(40);

        builder.Property(e => e.UseYn)
            .HasColumnName("use_yn")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ActiName)
            .HasColumnName("acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.OriginActiName)
            .HasColumnName("origin_acti_name")
            .HasMaxLength(100);

        builder.Property(e => e.ReasonCode)
            .HasColumnName("reason_code")
            .HasMaxLength(40);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(4000);

        builder.Property(e => e.CreateUserId)
            .HasColumnName("create_user_id")
            .HasMaxLength(40);

        builder.Property(e => e.CreateDate)
            .HasColumnName("create_date");

        builder.Property(e => e.UpdateUserId)
            .HasColumnName("update_user_id")
            .HasMaxLength(40);

        builder.Property(e => e.UpdateDate)
            .HasColumnName("update_date")
            .IsRequired();

        // RowStatus is from SphereEntity base but does NOT exist in SPC_RAWDATA
        builder.Ignore(e => e.RowStatus);
    }
}
