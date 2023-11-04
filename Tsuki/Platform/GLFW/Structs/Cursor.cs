namespace Tsuki.Platform.GLFW.Structs;

public readonly struct Cursor : IEquatable<Cursor>
{
    public static readonly Cursor None;
    
    private readonly IntPtr _handle;

    public static bool operator ==(Cursor left, Cursor right) => left.Equals(right);
    public static bool operator !=(Cursor left, Cursor right) => !left.Equals(right);

    public static implicit operator IntPtr(Cursor context) => context._handle;
    
    public bool Equals(Cursor other) => _handle == other._handle;
    public override bool Equals(object? obj) => obj is Cursor other && Equals(other);
    
    public override int GetHashCode() => _handle.GetHashCode();
    
}