using Tsuki.Framework.Math;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Graphics.Primitives;

public struct Rectangle : IEquatable<Rectangle>
{
    public double X;
    public double Y;

    public double Width;
    public double Height;

    public Rectangle(double value) : this (value, value, value, value)
    {

    }

    public Rectangle(double x, double y, double width, double height)
    {
        X = x;
        Y = y;

        Width = width;
        Height = height;
    }

    public Rectangle(Vector2 location, Vector2 size)
    {
        X = location.X;
        Y = location.Y;

        Width = size.X;
        Height = size.Y;
    }

    public Vector2 Location
    {
        get => new Vector2(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Vector2 Size
    {
        get => new Vector2(Width, Height);
        set
        {
            Width = value.X;
            Height = value.Y;
        }
    }

    private Vector2 _min;

    public Vector2 Min
    {
        get => _min;
        set
        {
            _max = Vector2.ComponentMax(_max, value);
            _min = value;
        }
    }

    private Vector2 _max;

    public Vector2 Max
    {
        get => _max;
        set
        {
            _min = Vector2.ComponentMin(_min, value);
            _max = value;
        }
    }

    public double Left => X;
    public double Top => Y;
    public double Right => X + Width;
    public double Bottom => Y + Height;

    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);

    public Vector2 Centre => new Vector2(X + Width / 2, Y + Height / 2);

    public bool IsEmpty => Width <= 0 || Height <= 0;

    public double Area => Width * Height;

    public static Rectangle FromLTRB(double left, double top, double right, double bottom)
    {
        Est.FromLTRB(left, top, right, bottom, out Rectangle result);
        return result;
    }

    public static Rectangle FromDimensions(double left, double top, double width, double height)
    {
        Est.FromDimensions(left, top, width, height, out Rectangle result);
        return result;
    }

    public static Rectangle FromDimension(Vector2 position, Vector2 size)
    {
        Est.FromDimensions(position, size, out Rectangle result);
        return result;
    }

    public static Rectangle Multiply(Rectangle rect, double scale)
    {
        Est.Multiply(rect, scale, out Rectangle result);
        return result;
    }

    public static Rectangle Multiply(Rectangle rect, Vector2 scale)
    {
        Est.Multiply(rect, scale, out Rectangle result);
        return result;
    }

    public static Rectangle Divide(Rectangle rect, double scale)
    {
        Est.Divide(rect, scale, out Rectangle result);
        return result;
    }

    public static Rectangle Divide(Rectangle rect, Vector2 scale)
    {
        Est.Divide(rect, scale, out Rectangle result);
        return result;
    }

    public static Rectangle Intersect(Rectangle a, Rectangle b)
    {
        Est.Intersect(a, b, out Rectangle result);
        return result;
    }

    public static bool operator ==(Rectangle left, Rectangle right) 
        => left.Equals(right);

    public static bool operator !=(Rectangle left, Rectangle right) 
        => !left.Equals(right);

    public static Rectangle operator *(Rectangle rectangle, double scale) 
        => Multiply(rectangle, scale);
    
    public static Rectangle operator *(double scale, Rectangle rectangle) 
        => Multiply(rectangle, scale);

    public static Rectangle operator *(Rectangle rectangle, Vector2 scale) 
        => Multiply(rectangle, scale);

    public static Rectangle operator *(Vector2 scale, Rectangle rectangle) 
        => Multiply(rectangle, scale);

    public static Rectangle operator /(Rectangle rectangle, double scale) 
        => Divide(rectangle, scale);

    public static Rectangle operator /(double scale, Rectangle rectangle) 
        => Divide(rectangle, scale);

    public static Rectangle operator /(Rectangle rectangle, Vector2 scale) 
        => Divide(rectangle, scale);

    public static Rectangle operator /(Vector2 scale, Rectangle rectangle) 
        => Divide(rectangle, scale);

    public bool Contains(Vector2 pt) 
        => Contains(pt.X, pt.Y);

    public bool Contains(Rectangle rect) 
        => X <= rect.X && rect.X + rect.Width <= X + Width 
        && Y <= rect.Y && rect.Y + rect.Height <= Y + Height;

    public bool Contains(double x, double y) 
        => X <= x && x <= X + Width 
        && Y <= y && y <= Y + Height;

    public override bool Equals(object? obj)
        => obj is Rectangle && Equals((Rectangle)obj);

    public bool Equals(Rectangle other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(Left, Right, Top, Bottom);

    public override string ToString()
        => String.Format("({0}{4} {1}) - ({2}{4} {3})", Left, Top, Right, Bottom, MathHelper.ListSeparator);
}