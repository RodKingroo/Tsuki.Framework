using System.Runtime.InteropServices;

namespace Tsuki.Framework.Platform.Windows.Native;

public class Icon : IDisposable
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool DestroyIcon(nint hIcon);

    public nint Handle { get; set; }
    public int Width;
    public int Height;

    public Icon(nint handle, int width, int height)
    {
        Handle = handle;
        Width = width;
        Height = height;
    }

    ~Icon() => Dispose(false);

    private bool _disposed;
    protected virtual void Dispose(bool disposing)
    {
        if(_disposed) return;

        if(Handle != nint.Zero)
        {
            DestroyIcon(Handle);
            Handle = nint.Zero;
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}