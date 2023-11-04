namespace Tsuki.Platform.GLFW.Structs;

public readonly struct GLXContext : IEquatable<GLXContext>
{
    public static GLXContext None;
    
    private readonly IntPtr _handle;
    
    public static bool operator ==(GLXContext left, GLXContext right) => left.Equals(right);
    public static bool operator !=(GLXContext left, GLXContext right) => !(left == right);

    public static implicit operator IntPtr(GLXContext context) => context._handle;
    
    public override string ToString() => _handle.ToString();

    public bool Equals(GLXContext other) => _handle.Equals(other._handle);
    public override bool Equals(object? obj) => obj is GLXContext other && Equals(other);
    
    public override int GetHashCode() => _handle.GetHashCode();
}