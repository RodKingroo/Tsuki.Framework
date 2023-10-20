namespace Tsuki.Platform.GLFW.Structs;

public readonly struct Monitor : IEquatable<Monitor>
{
    public static readonly Monitor None;
    
    private readonly IntPtr _handle;

    public static bool operator ==(Monitor left, Monitor right) => left.Equals(right);
    public static bool operator !=(Monitor left, Monitor right) => !left.Equals(right);
    
    public static implicit operator IntPtr(Monitor context) => context._handle;
    
    public bool Equals(Monitor other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is Monitor other && Equals(other);

    public override int GetHashCode() => _handle.GetHashCode();
}