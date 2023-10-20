namespace Tsuki.Platform.GLFW.Structs;

public readonly struct EglDisplay : IEquatable<EglDisplay>
{
    public static readonly EglDisplay None;
    
    private readonly IntPtr _handle;
    
    public static bool operator ==(EglDisplay left, EglDisplay right) => left.Equals(right);
    public static bool operator !=(EglDisplay left, EglDisplay right) => !left.Equals(right);
    
    public static implicit operator IntPtr(EglDisplay display) => display._handle;
    
    public override string ToString() => _handle.ToString();
    
    public bool Equals(EglDisplay other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is EglDisplay other && Equals(other);
    
    public override int GetHashCode() => _handle.GetHashCode();
}