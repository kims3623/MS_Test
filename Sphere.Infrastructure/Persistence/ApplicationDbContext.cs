using Microsoft.EntityFrameworkCore;
using Sphere.Application.Common.Interfaces;
using Sphere.Domain.Common;
using Sphere.Domain.Entities.Approval;
using Sphere.Domain.Entities.Auth;
using Sphere.Domain.Entities.Common;
using Sphere.Domain.Entities.Dashboard;
using Sphere.Domain.Entities.Data;
using Sphere.Domain.Entities.SPC;
using Sphere.Domain.Entities.Standard;
using Sphere.Domain.Entities.System;
using Sphere.Domain.Entities.TPS;
using SphereFileInfo = Sphere.Domain.Entities.Common.SphereFileInfo;

namespace Sphere.Infrastructure.Persistence;

/// <summary>
/// Application database context for SPHERE application.
/// Total: 103 Entity DbSets
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    #region Common Entities (37)

    public DbSet<CodeMaster> CodeMasters => Set<CodeMaster>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<MaterialClass> MaterialClasses => Set<MaterialClass>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ActProd> ActProds => Set<ActProd>();
    public DbSet<Step> Steps => Set<Step>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Measm> Measms => Set<Measm>();
    public DbSet<Eqp> Eqps => Set<Eqp>();
    public DbSet<Line> Lines => Set<Line>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<Frequency> Frequencies => Set<Frequency>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Cust> Custs => Set<Cust>();
    public DbSet<EndUser> EndUsers => Set<EndUser>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<UserFavorite> UserFavorites => Set<UserFavorite>();
    public DbSet<NotifyList> NotifyLists => Set<NotifyList>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<SphereFileInfo> FileInfos => Set<SphereFileInfo>();
    public DbSet<GridColumn> GridColumns => Set<GridColumn>();
    public DbSet<ExportConfig> ExportConfigs => Set<ExportConfig>();
    public DbSet<ImportConfig> ImportConfigs => Set<ImportConfig>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<BatchJob> BatchJobs => Set<BatchJob>();
    public DbSet<BatchJobHistory> BatchJobHistories => Set<BatchJobHistory>();
    public DbSet<TreeNode> TreeNodes => Set<TreeNode>();
    public DbSet<Lookup> Lookups => Set<Lookup>();
    public DbSet<DropdownItem> DropdownItems => Set<DropdownItem>();
    public DbSet<DateRange> DateRanges => Set<DateRange>();
    public DbSet<SearchHistory> SearchHistories => Set<SearchHistory>();
    public DbSet<SortConfig> SortConfigs => Set<SortConfig>();
    public DbSet<ValidationRule> ValidationRules => Set<ValidationRule>();
    public DbSet<Pagination> Paginations => Set<Pagination>();
    public DbSet<ValidationResultEntity> ValidationResultEntities => Set<ValidationResultEntity>();

    #endregion

    #region Auth Entities (7)

    public DbSet<UserInfo> UserInfos => Set<UserInfo>();
    public DbSet<LoginUserInfo> LoginUserInfos => Set<LoginUserInfo>();
    public DbSet<Authority> Authorities => Set<Authority>();
    public DbSet<AuthorityFilter> AuthorityFilters => Set<AuthorityFilter>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<VendorAccountRequest> VendorAccountRequests => Set<VendorAccountRequest>();

    #endregion

    #region Approval Entities (4)

    public DbSet<Approval> Approvals => Set<Approval>();
    public DbSet<ApprovalDefaultUser> ApprovalDefaultUsers => Set<ApprovalDefaultUser>();
    public DbSet<ApprovalHistory> ApprovalHistories => Set<ApprovalHistory>();
    public DbSet<ApprovalAttachment> ApprovalAttachments => Set<ApprovalAttachment>();

    #endregion

    #region Standard Entities (17)

    public DbSet<MaterialMaster> MaterialMasters => Set<MaterialMaster>();
    public DbSet<Specification> Specifications => Set<Specification>();
    public DbSet<SpecificationDetail> SpecificationDetails => Set<SpecificationDetail>();
    public DbSet<YieldSpec> YieldSpecs => Set<YieldSpec>();
    public DbSet<YieldStep> YieldSteps => Set<YieldStep>();
    public DbSet<RunRuleMaster> RunRuleMasters => Set<RunRuleMaster>();
    public DbSet<SpecRunRuleRelation> SpecRunRuleRelations => Set<SpecRunRuleRelation>();
    public DbSet<UserGroup> UserGroups => Set<UserGroup>();
    public DbSet<UserAlarmRelation> UserAlarmRelations => Set<UserAlarmRelation>();
    public DbSet<DefaultManagementRule> DefaultManagementRules => Set<DefaultManagementRule>();
    public DbSet<AlmProc> AlmProcs => Set<AlmProc>();
    public DbSet<AlmAction> AlmActions => Set<AlmAction>();
    public DbSet<PicType> PicTypes => Set<PicType>();
    public DbSet<StatType> StatTypes => Set<StatType>();
    public DbSet<CntlnType> CntlnTypes => Set<CntlnType>();
    public DbSet<SpecType> SpecTypes => Set<SpecType>();
    public DbSet<DataType> DataTypes => Set<DataType>();

    #endregion

    #region Data Entities (6)

    public DbSet<RawData> RawDatas => Set<RawData>();
    public DbSet<TempRawData> TempRawDatas => Set<TempRawData>();
    public DbSet<RawDataWithGroup> RawDataWithGroups => Set<RawDataWithGroup>();
    public DbSet<ConfirmDate> ConfirmDates => Set<ConfirmDate>();
    public DbSet<RawDataDefault> RawDataDefaults => Set<RawDataDefault>();
    public DbSet<TempRawDataEtc> TempRawDataEtcs => Set<TempRawDataEtc>();

    #endregion

    #region Dashboard Entities (1)

    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();

    #endregion

    #region SPC Entities (18)

    public DbSet<ControlLimit> ControlLimits => Set<ControlLimit>();
    public DbSet<StatisticsCalc> StatisticsCalcs => Set<StatisticsCalc>();
    public DbSet<ChartData> ChartDatas => Set<ChartData>();
    public DbSet<SPH3010Chart> SPH3010Charts => Set<SPH3010Chart>();
    public DbSet<SPH3020Day> SPH3020Days => Set<SPH3020Day>();
    public DbSet<SPH3020Month> SPH3020Months => Set<SPH3020Month>();
    public DbSet<SPH3030Pareto> SPH3030Paretos => Set<SPH3030Pareto>();
    public DbSet<SPH3050Histogram> SPH3050Histograms => Set<SPH3050Histogram>();
    public DbSet<SPH3070Cpk> SPH3070Cpks => Set<SPH3070Cpk>();
    public DbSet<SPH4010Trend> SPH4010Trends => Set<SPH4010Trend>();
    public DbSet<ProcessCapability> ProcessCapabilities => Set<ProcessCapability>();
    public DbSet<YieldSpecMaster> YieldSpecMasters => Set<YieldSpecMaster>();
    public DbSet<ChartSeries> ChartSeriess => Set<ChartSeries>();
    public DbSet<ChartPoint> ChartPoints => Set<ChartPoint>();
    public DbSet<XBarRChart> XBarRCharts => Set<XBarRChart>();
    public DbSet<PChart> PCharts => Set<PChart>();
    public DbSet<CpkCalc> CpkCalcs => Set<CpkCalc>();
    public DbSet<SPH3030Chart> SPH3030Charts => Set<SPH3030Chart>();

    #endregion

    #region TPS Entities (7)

    public DbSet<AlarmAttachment> AlarmAttachments => Set<AlarmAttachment>();
    public DbSet<AlarmActionHist> AlarmActionHists => Set<AlarmActionHist>();
    public DbSet<AlarmMaster> AlarmMasters => Set<AlarmMaster>();
    public DbSet<AlarmRule> AlarmRules => Set<AlarmRule>();
    public DbSet<AlarmNotification> AlarmNotifications => Set<AlarmNotification>();
    public DbSet<SPH4020Table> SPH4020Tables => Set<SPH4020Table>();
    public DbSet<AlarmAttachFile> AlarmAttachFiles => Set<AlarmAttachFile>();

    #endregion

    #region System Entities (6)

    public DbSet<SystemConfig> SystemConfigs => Set<SystemConfig>();
    public DbSet<SystemCodeMaster> SystemCodeMasters => Set<SystemCodeMaster>();
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    public DbSet<ApiLog> ApiLogs => Set<ApiLog>();
    public DbSet<ErrorLog> ErrorLogs => Set<ErrorLog>();
    public DbSet<LocaleResource> LocaleResources => Set<LocaleResource>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global convention: Convert DB NULL to string.Empty for all non-nullable string properties.
        // This prevents SqlNullValueException when DB columns contain NULL values
        // that map to non-nullable C# string properties.
        var nullToEmptyConverter = new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<string, string?>(
            v => v, v => v ?? string.Empty);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string) && !property.IsNullable
                    && !property.IsPrimaryKey())
                {
                    // Mark as nullable in DB (allow NULL reads) while keeping C# non-nullable
                    property.IsNullable = true;
                    property.SetValueConverter(nullToEmptyConverter);
                }
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update audit fields for SphereEntity
        foreach (var entry in ChangeTracker.Entries<SphereEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateDate ??= DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
