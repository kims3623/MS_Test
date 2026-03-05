using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// Modal state entity for storing modal state.
/// </summary>
public class ModalState : SphereEntity
{
    public string ModalId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string ModalType { get; set; } = string.Empty;
    public int Width { get; set; } = 600;
    public int Height { get; set; } = 400;
    public string Position { get; set; } = "center";
}
