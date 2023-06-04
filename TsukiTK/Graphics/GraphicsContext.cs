using Tsuki.Framework.Platform;

namespace Tsuki.Framework.Graphics;

public sealed class GraphicsContext : IGraphicsContext
{
    public delegate nint GetAddressDelegate(string func);

    public delegate ContextHandle GetCurrentContextDelegate();

    private IGraphicsContext? implementation;

    

    public bool IsCurrent => throw new NotImplementedException();

    public int SwapInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void MakeCurrent()
    {
        throw new NotImplementedException();
    }

    public void MakeNoneCurrent()
    {
        throw new NotImplementedException();
    }

    public void SwapBuffers()
    {
        throw new NotImplementedException();
    }
}