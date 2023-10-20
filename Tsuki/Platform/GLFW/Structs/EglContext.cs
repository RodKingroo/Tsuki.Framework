namespace Tsuki.Platform.GLFW.Structs;

public readonly struct EglContext : IEquatable<EglContext>
{
    public static readonly EglContext None;
    
    private readonly IntPtr _handle;
    
    public static bool operator ==(EglContext left, EglContext right) => left.Equals(right);
    public static bool operator !=(EglContext left, EglContext right) => !left.Equals(right);
    
    public static implicit operator IntPtr(EglContext context) => context._handle;
    
    public override string ToString() => _handle.ToString();
    
    public bool Equals(EglContext other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is EglContext other && Equals(other);
    
    public override int GetHashCode() => _handle.GetHashCode();
    
}