using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Interfaces;
using Sphere.Application.Interfaces.Repositories;
using Sphere.Infrastructure.Identity;
using Sphere.Infrastructure.Persistence;
using Sphere.Infrastructure.Persistence.Repositories.Dapper;
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
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Dapper global: DB 컬럼(snake_case) → C# 프로퍼티(PascalCase) 자동 매핑
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        // IDbConnection - Scoped (요청마다 새 커넥션)
        services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

        // IDbConnectionFactory - 여러 커넥션이 필요한 레포지토리용
        services.AddScoped<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        // Dapper Repositories
        services.AddScoped<IApprovalRepository, ApprovalRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAlarmRepository, AlarmRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IPersonalDocRepository, PersonalDocRepository>();
        services.AddScoped<IRawDataRepository, RawDataRepository>();
        services.AddScoped<ISPCRepository, SPCRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();

        // Master Data Repositories
        services.AddScoped<ICodeMasterRepository, CodeMasterRepository>();
        services.AddScoped<IMaterialMasterRepository, MaterialMasterRepository>();
        services.AddScoped<IVendorMasterRepository, VendorMasterRepository>();
        services.AddScoped<IProjectMasterRepository, ProjectMasterRepository>();
        services.AddScoped<ISpecMasterRepository, SpecMasterRepository>();
        services.AddScoped<IDefaultRuleRepository, DefaultRuleRepository>();
        services.AddScoped<IMtrlClassMapRepository, MtrlClassMapRepository>();

        // System Repository
        services.AddScoped<ISystemRepository, SystemRepository>();

        // Favorite Repository (Dapper)
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();

        // Notification Repository
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // Account Repository
        services.AddScoped<IAccountRepository, AccountRepository>();

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
