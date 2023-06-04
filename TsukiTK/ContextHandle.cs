using System.Diagnostics.CodeAnalysis;

namespace Tsuki.Framework;

public struct ContextHandle : IComparable<ContextHandle>, IEquatable<ContextHandle>
{
    private nint handle;

    public nint Handle => handle;
    public static ContextHandle Zero = new ContextHandle(nint.Zero);

    public ContextHandle(nint handle)
    {
        this.handle = handle;
    }

    public override string ToString()
        => Handle.ToString();

    public override bool Equals(object? obj)
        => obj is ContextHandle && Equals((ContextHandle)obj);

    public bool Equals(ContextHandle other)
        => this == other;

    public static explicit operator nint(ContextHandle c)
    {
        if (c != ContextHandle.Zero) return c.handle;
        else return nint.Zero;

    }

    public static explicit operator ContextHandle(nint p)
        => new ContextHandle(p);

    public static bool operator ==(ContextHandle left, ContextHandle right)
        => left.Equals(right);

    public static bool operator !=(ContextHandle left, ContextHandle right)
        => !left.Equals(right);

    public override int GetHashCode()
        => Handle.GetHashCode();

    public unsafe int CompareTo(ContextHandle other)
        => (int)((int*)other.handle.ToPointer() - (int*)this.handle.ToPointer());

}