using Tsuki.Framework.Graphics.Primitives;

namespace Tsuki.Framework;

public class DisplayResolution
{
    private Rectangle bounds;

    public nint BitsPerPixel { get; set; }
    public double RefreshRate { get; set; }

    public DisplayResolution() 
    {

    }

    public DisplayResolution(nint x, nint y, nint width, nint height, nint bitsPerPixel, double refreshRate)
    {
        bounds = new Rectangle(x, y, width, height);
        BitsPerPixel = bitsPerPixel;
        RefreshRate = refreshRate;
    }

    public Rectangle Bounds => bounds;

    public int Width
    {
        get => (int)bounds.Width;
        set => Width = value;
    }

    public int Height
    {
        get => (int)bounds.Height;
        set => Height = value;
    }

    public override string ToString()
        => string.Format("{0}x{1}@{2}HZ", Bounds, BitsPerPixel, RefreshRate);

    public override bool Equals(object? obj)
        => obj is DisplayResolution && Equals((DisplayResolution)obj);

    public bool Equals(DisplayResolution other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(Bounds, BitsPerPixel, RefreshRate);

    public static bool operator ==(DisplayResolution left, DisplayResolution right)
        => left.Equals(right);
    
    public static bool operator !=(DisplayResolution left, DisplayResolution right)
        => !left.Equals(right);
}