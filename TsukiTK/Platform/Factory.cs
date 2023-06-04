using Tsuki.Framework.Graphics;

namespace Tsuki.Framework.Platform;

public sealed class Factory : IPlatformFactory
{
    public IDisplayDeviceDriver CreateDisplayDeviceDriver()
    {
        throw new NotImplementedException();
    }

    public IGraphicsContext CreateGLContext(GraphicsMode mode, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags)
    {
        throw new NotImplementedException();
    }

    public IGraphicsContext CreateGLContext(ContextHandle handle, IWindowInfo window, IGraphicsContext shareContext, bool directRendering, int major, int minor, GraphicsContextFlags flags)
    {
        throw new NotImplementedException();
    }

    public INativeWindow CreateNativeWindow(int x, int y, int width, int height, string title, GraphicsMode mode, GameWindowFlags options, DisplayDevice device)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}