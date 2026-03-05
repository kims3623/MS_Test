using Sphere.Application.Common.Interfaces;

namespace Sphere.Infrastructure.Services;

/// <summary>
/// Date/time service implementation
/// </summary>
public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
