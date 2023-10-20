namespace Tsuki.Platform.GLFW.Structs;

public struct OsMesaContext
{
    public static readonly OsMesaContext None;

    private readonly IntPtr _handle;

    public static bool operator ==(OsMesaContext left, OsMesaContext right) => left.Equals(right);
    public static bool operator !=(OsMesaContext left, OsMesaContext right) => !left.Equals(right);

    public static implicit operator IntPtr(OsMesaContext context) => context._handle;

    public override string ToString() => _handle.ToString();

    public bool Equals(OsMesaContext other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is OsMesaContext other && Equals(other);

    public override int GetHashCode() => _handle.GetHashCode();
}