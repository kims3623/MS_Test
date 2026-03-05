namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// Service for date/time operations (allows for testing)
/// </summary>
public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
