using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Graphics.Primitives;

internal partial class Est
{
    internal static void FromLTRB(double left, double top, double right, double bottom, out Rectangle result)
    {
        result = new Rectangle(left, top, right, bottom);
    }

    internal static void FromDimensions(double left, double top, double width, double height, out Rectangle result)
    {
        result = new Rectangle
        (
            left, 
            top, 
            left + width, 
            top + height
        );
    }

    internal static void FromDimensions(Vector2 position, Vector2 size, out Rectangle result)
    {
        result = new Rectangle
        (
            position.X, 
            position.Y, 
            position.X + size.X, 
            position.Y + size.Y
        );
    }

    internal static void Multiply(Rectangle rect, double scale, out Rectangle result)
    {
        result = new Rectangle
        (
            rect.X * scale, 
            rect.Y * scale, 
            rect.Width * scale, 
            rect.Height * scale
        );
    }

    internal static void Multiply(Rectangle rect, Vector2 scale, out Rectangle result)
    {
        result = new Rectangle
        (
            rect.X * scale.X,
            rect.Y * scale.Y,
            rect.Width * scale.X,
            rect.Height * scale.Y
        );
    }

    internal static void Divide(Rectangle rect, double scale, out Rectangle result)
    {
        result = new Rectangle
        (
            rect.X * scale,
            rect.Y * scale,
            rect.Width * scale,
            rect.Height * scale
        );
    }

    internal static void Divide(Rectangle rect, Vector2 scale, out Rectangle result)
    {
        result = new Rectangle
        (
            rect.X * scale.X,
            rect.Y * scale.Y,
            rect.Width * scale.X,
            rect.Height * scale.Y
        );
    }

    internal static void Intersect(Rectangle a, Rectangle b, out Rectangle result)
    {
        Vector2 min = Vector2.ComponentMin(a.Min, b.Min);
        Vector2 max = Vector2.ComponentMax(a.Max, a.Min);

        if(max.X >= min.X && max.Y >= min.Y)
            result = new Rectangle(min, max);
        else result = new Rectangle(0);
    }
    
}