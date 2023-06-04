using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Graphics.Colour;

internal partial class Est
{
    internal static void FromSRGB(Vector4 srgb, out ColorRGBA result)
    {
        double r, g, b;
        if(srgb.X <= 0.04045)
            r = srgb.X / 12.92;
        else r = System.Math.Pow((srgb.X + 0.055) / (1 + 0.055), 2.4);

        if(srgb.Y <= 0.04045)
            g = srgb.Y / 12.92;
        else g = System.Math.Pow((srgb.Y + 0.055) / (1 + 0.055), 2.4);

        if(srgb.Z <= 0.04045)
            b = srgb.Z / 12.92;
        else b = System.Math.Pow((srgb.Z + 0.055) / (1 + 0.055), 2.4);

        result = new ColorRGBA(r, g, b, srgb.W);
    }

    internal static void ToSRGB(ColorRGBA rgb, out Vector4 result)
    {
        double r, g, b;

        if(rgb.ChannelR <= 0.0031308)
            r = 12.92 * rgb.ChannelR;
        else r = ((1 + 0.055) * System.Math.Pow(rgb.ChannelR, 1 / 2.4)) - 0.055;

        if(rgb.ChannelG <= 0.0031308)
            g = 12.92 * rgb.ChannelG;
        else g = ((1 + 0.055) * System.Math.Pow(rgb.ChannelG, 1 / 2.4)) - 0.055;

        if(rgb.ChannelB <= 0.0031308)
            b = 12.92 * rgb.ChannelB;
        else b = ((1 + 0.055) * System.Math.Pow(rgb.ChannelB, 1 / 2.4)) - 0.055;

        result = new Vector4(r, g, b, rgb.Alpha);
    }

    internal static void FromHSL(Vector4 hsl, out ColorRGBA result)
    {
        double hue = hsl.X * 360;
        double saturation = hsl.Y;
        double lightness = hsl.Z;

        double c = (1 - System.Math.Abs((2 * lightness) - 1)) * saturation;

        double h = hue / 60;
        double x = c * (1 - System.Math.Abs((h % 2) - 1));

        double r, g, b;
        switch (h)
        {
            case >= 0 and < 1:
                r = c;
                g = x;
                b = 0;
                break;
            case >= 1 and < 2:
                r = x;
                g = c;
                b = 0;
                break;
            case >= 2 and < 3:
                r = 0;
                g = c;
                b = x;
                break;
            case >= 3 and < 4:
                r = 0;
                g = x;
                b = c;
                break;
            case >= 4 and < 5:
                r = x;
                g = 0;
                b = c;
                break;
            case >= 5 and < 6:
                r = c;
                g = 0;
                b = x;
                break;
            default:
                r = 0;
                g = 0;
                b = 0;
                break;
        }

        double m = lightness - (c / 2);
        if (m < 0) m = 0;
        result = new ColorRGBA(r + m, g + m, b + m, hsl.W);
    }

    internal static void ToHSL(ColorRGBA rgb, out Vector4 result)
    {
        double max = System.Math.Max(rgb.ChannelR, System.Math.Max(rgb.ChannelG, rgb.ChannelB));
        double min = System.Math.Min(rgb.ChannelR, System.Math.Min(rgb.ChannelG, rgb.ChannelB));
        double diff = max - min;
        double h = 0;

        if(diff == 0) h = 0;
        
        if(max == rgb.ChannelR)
        {
            h = ((rgb.ChannelG - rgb.ChannelB) / diff) % 6;
            if (h < 0) h += 6; 
        }
        else if (max == rgb.ChannelG)
            h = ((rgb.ChannelB - rgb.ChannelR) / diff) + 2;
        else if (max == rgb.ChannelB)
            h = ((rgb.ChannelR - rgb.ChannelG) / diff) + 4;
        
        double hue = h / 6;

        if (hue < 0) hue += 1;

        double lightness = (max + min) / 2;

        double saturation = 0;
        
        if ((1 - System.Math.Abs((2 * lightness) - 1)) != 0)
            saturation = diff / (1 - System.Math.Abs((2 * lightness) - 1));

        result = new Vector4(hue, saturation, lightness, rgb.Alpha);
    }

    internal static void FromHSV(Vector4 hsv, out ColorRGBA result)
    {
        double hue = hsv.X * 360;
        double saturation = hsv.Y;
        double value = hsv.Z;

        double c = value * saturation;

        double h = hue / 60;
        double x = c * (1 - System.Math.Abs((h % 2) - 1));

        double r, g, b;
        switch (h)
        {
            case >= 0 and < 1:
                r = c;
                g = x;
                b = 0;
                break;
            case >= 1 and < 2:
                r = x;
                g = c;
                b = 0;
                break;
            case >= 2 and < 3:
                r = 0;
                g = c;
                b = x;
                break;
            case >= 3 and < 4:
                r = 0;
                g = x;
                b = c;
                break;
            case >= 4 and < 5:
                r = x;
                g = 0;
                b = c;
                break;
            case >= 5 and < 6:
                r = c;
                g = 0;
                b = x;
                break;
            default:
                r = 0;
                g = 0;
                b = 0;
                break;
        }

        double m = value - c;
        result = new ColorRGBA(r + m, g + m, b + m, hsv.W);
    }

    internal static void ToHSV(ColorRGBA rgb, out Vector4 result)
    {
        double max = System.Math.Max(rgb.ChannelR, System.Math.Max(rgb.ChannelG, rgb.ChannelB));
        double min = System.Math.Min(rgb.ChannelR, System.Math.Min(rgb.ChannelG, rgb.ChannelB));
        double diff = max - min;

        double h = 0;

        if (diff == 0) h = 0;
        
        if (max == rgb.ChannelR)
        {
            h = ((rgb.ChannelG - rgb.ChannelB) / diff) % 6;
            if (h < 0) h += 6;
        }
        else if (max == rgb.ChannelG)
            h = ((rgb.ChannelB - rgb.ChannelR) / diff) + 2;
        else if (max == rgb.ChannelB)
            h = ((rgb.ChannelR - rgb.ChannelG) / diff) + 4;
        
        double hue = h * 60 / 360;

        double saturation = 0;
        if (max != 0)
            saturation = diff / max;

        result = new Vector4(hue, saturation, max, rgb.Alpha);
    }

    internal static void FromXYZ(Vector4 xyz, out ColorRGBA result)
    {
        double r = (0.41847 * xyz.X) + (-0.15866 * xyz.Y) + (-0.082835 * xyz.Z);
        double g = (-0.091169 * xyz.X) + (0.25243 * xyz.Y) + (0.015708 * xyz.Z);
        double b = (0.00092090 * xyz.X) + (-0.0025498 * xyz.Y) + (0.17860 * xyz.Z);
        result = new ColorRGBA(r, g, b, xyz.W);
    }

    internal static void ToXYZ(ColorRGBA rgb, out Vector4 result)
    {
        double x = ((0.49 * rgb.ChannelR) + (0.31 * rgb.ChannelG) + (0.2 * rgb.ChannelB)) / 0.17697;
        double y = ((0.17697 * rgb.ChannelR) + (0.8124 * rgb.ChannelG) + (0.01063 * rgb.ChannelB)) / 0.17697;
        double z = ((0 * rgb.ChannelR) + (0.01 * rgb.ChannelG) + (0.99 * rgb.ChannelB)) / 0.17697;
        result = new Vector4(x, y, z, rgb.Alpha);
    }

    internal static void FromYCBCR(Vector4 ycbcr, out ColorRGBA result)
    {
        double r = (1 * ycbcr.X) + (0 * ycbcr.Y) + (1.402 * ycbcr.Z);
        double g = (1 * ycbcr.X) + (-0.344136 * ycbcr.Y) + (-0.714136 * ycbcr.Z);
        double b = (1 * ycbcr.X) + (1.772 * ycbcr.Y) + (0 * ycbcr.Z);
        result =  new ColorRGBA(r, g, b, ycbcr.W);
    }

    internal static void ToYCBCR(ColorRGBA rgb, out Vector4 result)
    {
        double y = (0.299 * rgb.ChannelR) + (0.587 * rgb.ChannelG) + (0.114 * rgb.ChannelB);
        double u = (-0.168736 * rgb.ChannelR) + (-0.331264 * rgb.ChannelG) + (0.5 * rgb.ChannelB);
        double v = (0.5 * rgb.ChannelR) + (-0.418688 * rgb.ChannelG) + (-0.081312 * rgb.ChannelB);
        result = new Vector4(y, u, v, rgb.Alpha);
    }

    internal static void FromHCY(Vector4 hcy, out ColorRGBA result)
    {
        double hue = hcy.X * 360;
        double y = hcy.Y;
        double luminance = hcy.Z;

        double h = hue / 60;
        double x = y * (1 - System.Math.Abs((h % 2) - 1));

        double r, g, b;
        switch (h)
        {
            case >= 0 and < 1:
                r = y;
                g = x;
                b = 0;
                break;
            case >= 1 and < 2:
                r = x;
                g = y;
                b = 0;
                break;
            case >= 2 and < 3:
                r = 0;
                g = y;
                b = x;
                break;
            case >= 3 and < 4:
                r = 0;
                g = x;
                b = y;
                break;
            case >= 4 and < 5:
                r = x;
                g = 0;
                b = y;
                break;
            case >= 5 and < 6:
                r = y;
                g = 0;
                b = x;
                break;
            default:
                r = 0;
                g = 0;
                b = 0;
                break;
        }

        double m = luminance - (0.3 * r) + (0.59 * g) + (0.11 * b);
        result = new ColorRGBA(r + m, g + m, b + m, hcy.W);
    }

    internal static void ToHCY(ColorRGBA rgb, out Vector4 result)
    {
        double max = System.Math.Max(rgb.ChannelR, System.Math.Max(rgb.ChannelG, rgb.ChannelB));
        double min = System.Math.Min(rgb.ChannelR, System.Math.Min(rgb.ChannelG, rgb.ChannelB));
        double diff = max - min;

        double h = 0;
        if (max == rgb.ChannelR)
            h = ((rgb.ChannelG - rgb.ChannelB) / diff) % 6;
        else if (max == rgb.ChannelG)
            h = ((rgb.ChannelB - rgb.ChannelR) / diff) + 2;
        else if (max == rgb.ChannelB)
            h = ((rgb.ChannelR - rgb.ChannelG) / diff) + 4;

        double hue = h * 1 / 6;

        double luminance = (0.3 * rgb.ChannelR) + (0.59 * rgb.ChannelG) + (0.11 * rgb.ChannelB);

        result = new Vector4(hue, diff, luminance, rgb.Alpha);
    }
}