using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.System;

namespace Sphere.Infrastructure.Persistence.Configurations.System;

/// <summary>
/// EF Core configuration for EmailTemplate entity.
/// Maps to SPC_EMAIL_TEMPLATE table.
/// </summary>
public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.ToTable("SPC_EMAIL_TEMPLATE");

        // Primary Key
        builder.HasKey(e => e.TemplateId);

        // Column mappings
        builder.Property(e => e.TemplateId)
            .HasColumnName("template_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.TemplateName)
            .HasColumnName("template_name")
            .HasMaxLength(200);

        builder.Property(e => e.TemplateType)
            .HasColumnName("template_type")
            .HasMaxLength(50);

        builder.Property(e => e.Subject)
            .HasColumnName("subject")
            .HasMaxLength(500);

        builder.Property(e => e.SubjectK)
            .HasColumnName("subject_k")
            .HasMaxLength(500);

        builder.Property(e => e.SubjectE)
            .HasColumnName("subject_e")
            .HasMaxLength(500);

        builder.Property(e => e.Body)
            .HasColumnName("body")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.BodyK)
            .HasColumnName("body_k")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.BodyE)
            .HasColumnName("body_e")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Variables)
            .HasColumnName("variables")
            .HasMaxLength(2000);

        // Base entity columns
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40);

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
        builder.HasIndex(e => e.TemplateType)
            .HasDatabaseName("IX_EmailTemplate_Type");

        builder.HasIndex(e => e.UseYn)
            .HasDatabaseName("IX_EmailTemplate_UseYn");
    }
}
