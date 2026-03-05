using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sphere.Domain.Entities.Common;

namespace Sphere.Infrastructure.Persistence.Configurations.Common;

/// <summary>
/// EF Core configuration for SearchHistory entity.
/// </summary>
public class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{
    public void Configure(EntityTypeBuilder<SearchHistory> builder)
    {
        builder.ToTable("SPC_SEARCH_HISTORY");

        // Composite Primary Key
        builder.HasKey(e => new { e.DivSeq, e.HistoryId });

        // Column mappings
        builder.Property(e => e.DivSeq)
            .HasColumnName("div_seq")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.HistoryId)
            .HasColumnName("history_id")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(50);

        builder.Property(e => e.ScreenId)
            .HasColumnName("screen_id")
            .HasMaxLength(40);

        builder.Property(e => e.SearchText)
            .HasColumnName("search_text")
            .HasMaxLength(500);

        builder.Property(e => e.SearchJson)
            .HasColumnName("search_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.SearchDate)
            .HasColumnName("search_date");

        builder.Property(e => e.UseCount)
            .HasColumnName("use_count")
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
        builder.HasIndex(e => new { e.DivSeq, e.UserId })
            .HasDatabaseName("IX_SearchHistory_DivSeq_UserId");

        builder.HasIndex(e => new { e.DivSeq, e.ScreenId })
            .HasDatabaseName("IX_SearchHistory_DivSeq_ScreenId");

        builder.HasIndex(e => e.SearchDate)
            .HasDatabaseName("IX_SearchHistory_SearchDate");
    }
}
