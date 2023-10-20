namespace Tsuki.Platform.GLFW.Structs;

public struct NsOpenGlContext : IEquatable<NsOpenGlContext>
{
    public static readonly NsOpenGlContext None;

    private readonly IntPtr _handle;

    public static bool operator ==(NsOpenGlContext left, NsOpenGlContext right) => left.Equals(right);
    public static bool operator !=(NsOpenGlContext left, NsOpenGlContext right) => !left.Equals(right);

    public static implicit operator IntPtr(NsOpenGlContext context) => context._handle;

    public override string ToString() => _handle.ToString();

    public bool Equals(NsOpenGlContext other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is NsOpenGlContext other && Equals(other);

    public override int GetHashCode() => _handle.GetHashCode();
}