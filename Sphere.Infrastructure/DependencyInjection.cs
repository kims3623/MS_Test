using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Interfaces;
using Sphere.Application.Interfaces.Repositories;
using Sphere.Infrastructure.Identity;
using Sphere.Infrastructure.Persistence;
using Sphere.Infrastructure.Persistence.Repositories.Dapper;
using Sphere.Infrastructure.Persistence.Repositories.EF;
using Sphere.Infrastructure.Services;

namespace Sphere.Infrastructure;

/// <summary>
/// Dependency injection configuration for Infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Enable Dapper snake_case → PascalCase column mapping
        // SP returns columns like div_seq, spec_sys_id → maps to DivSeq, SpecSysId
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        // Database Context
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Dapper IDbConnection for stored procedures
        services.AddScoped<IDbConnection>(sp =>
            new SqlConnection(connectionString));

        // Dapper Repositories (21 USPs)
        services.AddScoped<IApprovalRepository, ApprovalRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAlarmRepository, AlarmRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IPersonalDocRepository, PersonalDocRepository>();
        services.AddScoped<IRawDataRepository, RawDataRepository>();
        services.AddScoped<ISPCRepository, SPCRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();

        // Master Data Repositories (B3 batch)
        services.AddScoped<ICodeMasterRepository, CodeMasterRepository>();
        services.AddScoped<IMaterialMasterRepository, MaterialMasterRepository>();
        services.AddScoped<IVendorMasterRepository, VendorMasterRepository>();
        services.AddScoped<IProjectMasterRepository, ProjectMasterRepository>();
        services.AddScoped<ISpecMasterRepository, SpecMasterRepository>();
        services.AddScoped<IDefaultRuleRepository, DefaultRuleRepository>();
        services.AddScoped<IMtrlClassMapRepository, MtrlClassMapRepository>();

        // System Repository
        services.AddScoped<ISystemRepository, SystemRepository>();

        // EF Core Repositories
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();

        // Dapper Notification Repository
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // OTP Service
        services.AddScoped<IOtpService, OtpService>();

        // JWT Settings
        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        // Memory Cache for token storage
        services.AddMemoryCache();

        // Identity Services
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Locale & Localization Services
        services.AddScoped<ILocaleService, LocaleService>();
        services.AddScoped<IMessageLocalizer, MessageLocalizer>();

        // Services
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IAuthService, AuthService>();

        // HTTP Context Accessor
        services.AddHttpContextAccessor();

        return services;
    }
}
