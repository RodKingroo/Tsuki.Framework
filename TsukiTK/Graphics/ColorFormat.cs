namespace Tsuki.Framework.Graphics;

public struct ColorFormat : IComparable<ColorFormat>, IEquatable<ColorFormat>
{
    private byte red;
    private byte green;
    private byte blue;
    private byte alpha;

    public bool IsIndexed { get; set; }
    public int BitsPerPixel { get; set; }

    public int Red
    {
        get => red;
        set => red = (byte)value;
    }

    public int Green
    {
        get => green;
        set => green = (byte)value;
    }

    public int Blue
    {
        get => blue;
        set => blue = (byte)value;
    }

    public int Alpha
    {
        get => alpha;
        set => alpha = (byte)value;
    }

    

    public ColorFormat(int bpp)
    {
        BitsPerPixel = bpp;
        IsIndexed = false;

        switch(bpp)
        {
            case 32:
                Red = 8;
                Green = 8;
                Blue = 8;
                Alpha = 8;
                break;
            case 24:
                Red = 8;
                Green = 8;
                Blue = 8;
                break;
            case 16:
                Red = 5;
                Green = 6;
                Blue = 5;
                break;
            case 15:
                Red = 5;
                Green = 5;
                Blue = 5;
                break;
            case 8:
                Red = 3;
                Green = 3;
                Blue = 2;
                IsIndexed = true;
                break;
            case 4:
                Red = 2;
                Green = 2;
                Blue = 1;
                IsIndexed = true;
                break;
            default:
                Red = (byte)bpp / 4;
                Green = (byte)(bpp/4) + (bpp % 4);
                Blue = (byte)bpp / 4;
                Alpha = (byte)bpp / 4;
                break;
        }
    }

    public ColorFormat(int red, int green, int blue, int alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;

        BitsPerPixel = red + green + blue + alpha;
        IsIndexed = false;
        if(BitsPerPixel < 15 && BitsPerPixel != 0)
            IsIndexed = true;
    }

    public static ColorFormat Empty = new ColorFormat(0);

    public static implicit operator ColorFormat(int bpp)
        => new ColorFormat(bpp);

    public int CompareTo(ColorFormat other)
    {
        int result = BitsPerPixel.CompareTo(other.BitsPerPixel);
        if (result != 0) return result;
        result = IsIndexed.CompareTo(other.IsIndexed);
        if (result != 0) return result;
        result = Alpha.CompareTo(other.Alpha);
        return result;
    }

    public override bool Equals(object? obj)
        => obj is ColorFormat && Equals((ColorFormat)obj);

    public bool Equals(ColorFormat other)
        => this == other;

    public static bool operator ==(ColorFormat left, ColorFormat right)
        => left.Equals(right);
        
    public static bool operator !=(ColorFormat left, ColorFormat right)
        => !left.Equals(right);

    public static bool operator >(ColorFormat left, ColorFormat right)
        => left.CompareTo(right) > 0;

    public static bool operator >=(ColorFormat left, ColorFormat right)
        => left.CompareTo(right) >= 0;

    public static bool operator <(ColorFormat left, ColorFormat right)
        => left.CompareTo(right) < 0;
    
    public static bool operator <=(ColorFormat left, ColorFormat right)
        => left.CompareTo(right) <= 0;

    public override int GetHashCode()
        => HashCode.Combine(Red, Green, Blue, Alpha);

    public override string ToString()
    {
        if (IsIndexed)
            return string.Format("{0} ({1})", BitsPerPixel, (" indexed"));
        else
            return string.Format("{0} ({1})", BitsPerPixel, (Red.ToString() + Green.ToString() + Blue.ToString() + Alpha.ToString()));
    }
}