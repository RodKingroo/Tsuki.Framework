using System.Drawing;
using System.Runtime.CompilerServices;

using Tsuki.Framework.Math;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Graphics.Colour;

public partial struct ColorRGBA : IEquatable<ColorRGBA>
{
    public double ChannelR;
    public double ChannelG;
    public double ChannelB;
    public double Alpha;

    public ColorRGBA(double value) : this(value, value, value, value)
    {

    }

    public ColorRGBA(double R, double G, double B, double A)
    {
        ChannelR = R;
        ChannelG = G;
        ChannelB = B;
        Alpha = A;
    }

    public ColorRGBA(byte R, byte G, byte B, byte A)
    {
        ChannelR = R;
        ChannelG = G;
        ChannelB = B;
        Alpha = A;
    }

    public uint ToARGB()
    {
        uint value = ((uint)(Alpha * byte.MaxValue) << 24) |
                     ((uint)(ChannelR * byte.MaxValue) << 16) |
                     ((uint)(ChannelG * byte.MaxValue) << 8) |
                      (uint)(ChannelB * byte.MaxValue);

        return value;
    }

    public static bool operator ==(ColorRGBA left, ColorRGBA right)
        => left.Equals(right);

    public static bool operator !=(ColorRGBA left, ColorRGBA right)
        => !left.Equals(right);

    public static implicit operator ColorRGBA(Color color)
        => new ColorRGBA(color.R, color.G, color.B, color.A);

    public static explicit operator Color(ColorRGBA color)
        => Color.FromArgb((int)(color.Alpha * byte.MaxValue),
                          (int)(color.ChannelR * byte.MaxValue),
                          (int)(color.ChannelG * byte.MaxValue),
                          (int)(color.ChannelB * byte.MaxValue));

    public static explicit operator Vector4(ColorRGBA color)
        => Unsafe.As<ColorRGBA, Vector4>(ref color);

    public bool Equals(ColorRGBA other)
        => this == other;

    public override bool Equals(object? obj)
        => obj is ColorRGBA && Equals((ColorRGBA)obj);

    public override int GetHashCode()
        => HashCode.Combine(ChannelR, ChannelG, ChannelB, Alpha);

    public override string ToString()
        => string.Format("{{(R{4} G{4} B{4} A) = ({0}{4} {1}{4} {2}{4} {3})}}", ChannelR, ChannelG, ChannelB, Alpha, MathHelper.ListSeparator);

    public static ColorRGBA FromSRGB(Vector4 srgb)
    {
        Est.FromSRGB(srgb, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToSRGB(ColorRGBA rgb)
    {
        Est.ToSRGB(rgb, out Vector4 result);
        return result;
    }

    public static ColorRGBA FromHSL(Vector4 hsl)
    {
        Est.FromHSL(hsl, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToHSL(ColorRGBA rgb)
    {
        Est.ToHSL(rgb, out Vector4 result);
        return result;       
    }

    public static ColorRGBA FromHSV(Vector4 hsv)
    {
        Est.FromHSV(hsv, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToHSV(ColorRGBA rgb)
    {
        Est.ToHSV(rgb, out Vector4 result);
        return result;
    }

    public static ColorRGBA FromXYZ(Vector4 xyz)
    {
        Est.FromXYZ(xyz, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToXYZ(ColorRGBA rgb)
    {
        Est.ToXYZ(rgb, out Vector4 result);
        return result;
    }

    public static ColorRGBA FromYCBCR(Vector4 ycbcr)
    {
        Est.FromYCBCR(ycbcr, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToYCBCR(ColorRGBA rgb)
    {
        Est.ToYCBCR(rgb, out Vector4 result);
        return result;
    }

    public static ColorRGBA FromHCY(Vector4 hcy)
    {
        Est.FromHCY(hcy, out ColorRGBA result);
        return result;
    }

    public static Vector4 ToHCY(ColorRGBA rgb)
    {
        Est.ToHCY(rgb, out Vector4 result);
        return result;
    }
    
}