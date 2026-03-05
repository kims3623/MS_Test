using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Standard;

namespace Sphere.Infrastructure.Persistence.Configurations.Standard;

/// <summary>
/// EF Core configuration for StatType entity.
/// Maps to SPC_STAT_TYPE table with single primary key.
/// </summary>
public class StatTypeConfiguration : IEntityTypeConfiguration<StatType>
{
    public void Configure(EntityTypeBuilder<StatType> builder)
    {
        builder.ToTable("SPC_STAT_TYPE");

        // Single Primary Key (StatTypeId)
        builder.HasKey(e => e.StatTypeId);

        // Column mappings - Primary Key
        builder.Property(e => e.StatTypeId)
            .HasColumnName("stat_type_id")
            .HasMaxLength(40)
            .IsRequired();

        // Column mappings - DivSeq (not part of PK but still needed)
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

        // Column mappings - Attributes
        builder.Property(e => e.StatTypeName)
            .HasColumnName("stat_type_name")
            .HasMaxLength(200);

        builder.Property(e => e.StatTypeNameK)
            .HasColumnName("stat_type_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.StatTypeNameE)
            .HasColumnName("stat_type_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.CalcFormula)
            .HasColumnName("calc_formula")
            .HasMaxLength(500);

        builder.Property(e => e.ChartType)
            .HasColumnName("chart_type")
            .HasMaxLength(40);

        builder.Property(e => e.DspSeq)
            .HasColumnName("dsp_seq");

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
        builder.HasIndex(e => e.ChartType)
            .HasDatabaseName("IX_StatType_ChartType");

        builder.HasIndex(e => e.DspSeq)
            .HasDatabaseName("IX_StatType_DspSeq");
    }
}
