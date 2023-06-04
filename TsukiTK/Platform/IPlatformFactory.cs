using Tsuki.Framework.Graphics;

namespace Tsuki.Framework.Platform;

public interface IPlatformFactory : IDisposable
{
    INativeWindow CreateNativeWindow(int x, int y, int width, int height, string title, GraphicsMode mode, GameWindowFlags options, DisplayDevice device);
    IDisplayDeviceDriver CreateDisplayDeviceDriver();
    IGraphicsContext CreateGLContext(GraphicsMode mode, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags);
    IGraphicsContext CreateGLContext(ContextHandle handle, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags);
}