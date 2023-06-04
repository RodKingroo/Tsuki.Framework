using static Tsuki.Framework.Platform.GLFW.GLFW;
using static Tsuki.Framework.Math.MathHelper;

namespace Tsuki.Framework.Platform;

public struct Window : IEquatable<Window>
{
    public static Window None;

    public nint Handle;

    public Window(nint handle)
    {
        Handle = handle;
    }

    public unsafe double Opacity
    {
        get => GetWindowOpacity((Window*)Handle);
        set => SetWindowOpacity((Window*)Handle, Min(1, value));
    }

    public unsafe static implicit operator nint(Window window)
        => (nint)window.Handle;

    public static explicit operator Window(nint handle)
        => new Window(handle);

    public static bool operator ==(Window left, Window right)
        => left.Equals(right);

    public static bool operator !=(Window left, Window right)
        => !left.Equals(right);

    public override bool Equals(object? obj)
        => obj is Window && Equals((Window)obj);

    public bool Equals(Window other)
        => this == other;

    public unsafe override string ToString()
        => string.Format("{0}", Handle);

    public override int GetHashCode()
        => Handle.GetHashCode();
}