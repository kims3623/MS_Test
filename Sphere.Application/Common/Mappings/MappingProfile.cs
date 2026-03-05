using AutoMapper;

namespace Sphere.Application.Common.Mappings;

/// <summary>
/// Base AutoMapper profile for the application
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Global mappings can be configured here
        // Feature-specific mappings should be in their own profiles
    }
}
