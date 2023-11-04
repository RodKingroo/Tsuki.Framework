namespace Tsuki.Platform.GLFW.Structs;

public struct EglSurface : IEquatable<EglSurface>
{
    public static readonly EglSurface None;
    
    private readonly IntPtr _handle;
    
    public static bool operator ==(EglSurface left, EglSurface right) => left.Equals(right);
    public static bool operator !=(EglSurface left, EglSurface right) => !(left == right);
    
    public static implicit operator IntPtr(EglSurface surface) => surface._handle;
    
    public override string ToString() => _handle.ToString();
    
    public bool Equals(EglSurface other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is EglSurface other && Equals(other);
    
    public override int GetHashCode() => _handle.GetHashCode();
}