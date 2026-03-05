using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for Project entity.
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("SPC_PROJECT");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.ProjectId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ProjectId)
            .HasColumnName("project_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.ProjectName)
            .HasColumnName("project_name")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectNameK)
            .HasColumnName("project_name_k")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectNameE)
            .HasColumnName("project_name_e")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectNameC)
            .HasColumnName("project_name_c")
            .HasMaxLength(200);

        builder.Property(e => e.ProjectNameV)
            .HasColumnName("project_name_v")
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
        builder.HasIndex(e => e.DivSeq)
            .HasDatabaseName("IX_Project_DivSeq");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_Project_UseYn");
    }
}
