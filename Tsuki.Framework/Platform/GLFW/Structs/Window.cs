namespace Tsuki.Platform.GLFW.Structs;

public readonly struct Window : IEquatable<Window>
{
    public static readonly Window None;

    public readonly IntPtr _handle;

    public static bool operator ==(Window left, Window right) => left.Equals(right);
    public static bool operator !=(Window left, Window right) => !left.Equals(right);

    public static implicit operator IntPtr(Window context) => context._handle;

    public override string ToString() => _handle.ToString();

    public bool Equals(Window other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is Window other && Equals(other);

    public override int GetHashCode() => _handle.GetHashCode();
}