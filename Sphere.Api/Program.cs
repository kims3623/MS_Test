using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Sphere.Api.Extensions;
using Sphere.Api.Middleware;
using Sphere.Application;
using Sphere.Application.Common.Constants;
using Sphere.Infrastructure;
using Sphere.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.MapInboundClaims = false; // Preserve custom claim types (div_seq, dept_code, etc.)
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer ?? "SphereApi",
        ValidAudience = jwtSettings?.Audience ?? "SphereApp",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings?.Secret ??
                throw new InvalidOperationException("JWT Secret not configured")))
    };
});

// Add Localization
builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supported = SupportedLocales.Map.Values
        .Select(c => new CultureInfo(c)).ToList();
    options.DefaultRequestCulture = new RequestCulture("ko-KR");
    options.SupportedCultures = supported;
    options.SupportedUICultures = supported;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new SphereRequestCultureProvider()
    };
});

// Add Controllers
builder.Services.AddControllers();

// Add API Explorer and Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Sphere API", Version = "v1" });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:3000" })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add Health Checks
builder.Services.AddHealthChecks();

// Add Rate Limiting
builder.Services.AddCustomRateLimiting();

// Add Authorization Policies (RBAC)
builder.Services.AddAuthorization(options =>
{
    // Basic authentication policy
    options.AddPolicy("RequireAuthenticated", policy =>
        policy.RequireAuthenticatedUser());

    // Admin-only policy
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("CanAdmin", "Y"));

    // Read permission policy
    options.AddPolicy("RequireRead", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("CanRead", "Y"));

    // Write permission policy
    options.AddPolicy("RequireWrite", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("CanWrite", "Y"));

    // Delete permission policy
    options.AddPolicy("RequireDelete", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("CanDelete", "Y"));

    // Export permission policy
    options.AddPolicy("RequireExport", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("CanExport", "Y"));
});

// CSRF Protection
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "XSRF-TOKEN";
    options.Cookie.HttpOnly = false;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sphere API v1");
    });

    // Note: Test users already exist in database (admin@sphere.com, user@sphere.com, vendor@test.com)
    // Password is stored in UserInfo.PasswordHash column
}

// Exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Security headers middleware
app.UseSecurityHeaders();

// Request logging
app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowFrontend");

// Request localization middleware
app.UseRequestLocalization();

// Rate limiting middleware
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// Make Program class visible to test projects
public partial class Program { }
