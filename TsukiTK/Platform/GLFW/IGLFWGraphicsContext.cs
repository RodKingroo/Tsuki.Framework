namespace Tsuki.Framework.Platform.GLFW;

public interface IGLFWGraphicsContext : IGraphicsContext
{
    unsafe nint WindowPtr { get; }
}