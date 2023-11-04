namespace Tsuki.Platform.GLFW.Structs;

public struct Hglrc
{
    public static readonly Hglrc None;

    private readonly IntPtr _handle;

    public static bool operator ==(Hglrc left, Hglrc right) => left.Equals(right);
    public static bool operator !=(Hglrc left, Hglrc right) => !left.Equals(right);

    public static implicit operator IntPtr(Hglrc context) => context._handle;

    public override string ToString() => _handle.ToString();

    public bool Equals(Hglrc other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is Hglrc other && Equals(other);

    public override int GetHashCode() => _handle.GetHashCode();
}