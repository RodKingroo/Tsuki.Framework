using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Graphics.Primitives;

internal partial class Est
{
    internal static void FromLTFRBB(double left, double top, double front, 
                                    double right, double bottom, double back, 
                                    out Box result)
    {
        result = new Box
        (
            left, 
            top, 
            front, 
            right, 
            bottom, 
            back
        );
    }

    internal static void FromDimensions(double left, double top, double front, 
                                        double width, double height, double length, 
                                        out Box result)
    {
        result = new Box
        (
            left, 
            top, 
            front, 
            left + width,
            top + height,
            front + length
        );
    }

    internal static void FromDimensions(Vector3 position, Vector3 size, out Box result)
    {
        result = new Box
        (
            position.X,
            position.Y,
            position.Z,
            position.X + size.X,
            position.Y + size.Y,
            position.Z + size.Z
        );
    }

    internal static void Multiply(Box box, double scale, out Box result)
    {
        result =  new Box
        (
            box.X * scale,
            box.Y * scale,
            box.Z * scale,
            box.Width * scale,
            box.Height * scale,
            box.Length * scale
        );
    }

    internal static void Multiply(Box box, Vector3 scale, out Box result)
    {
        result = new Box
        (
            box.X * scale.X,
            box.Y * scale.Y,
            box.Z * scale.Z,
            box.Width * scale.X,
            box.Height * scale.Y,
            box.Length * scale.Z
        );
    }

    internal static void Divide(Box box, double scale, out Box result)
    {
        result =  new Box
        (
            box.X / scale,
            box.Y / scale,
            box.Z / scale,
            box.Width / scale,
            box.Height / scale,
            box.Length / scale
        );
    }

    internal static void Divide(Box box, Vector3 scale, out Box result)
    {
        result = new Box
        (
            box.X / scale.X,
            box.Y / scale.Y,
            box.Z / scale.Z,
            box.Width / scale.X,
            box.Height / scale.Y,
            box.Length / scale.Z
        );
    }
}