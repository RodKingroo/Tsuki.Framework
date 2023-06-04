using Tsuki.Framework.Math;
using Tsuki.Framework.Math.Vector;


namespace Tsuki.Framework.Graphics.Primitives;

public struct Box : IEquatable<Box>
{
    public double X;
    public double Y;
    public double Z;

    public double Width;
    public double Height;
    public double Length;

    public Box(double value) : this(value, value, value, 
                                    value, value, value)
    {

    }

    public Box(double x, double y, double z,
               double width, double height, double length)
    {
        X = x;
        Y = y;
        Z = z;

        Width = width;
        Height = height;
        Length = length;
    }

    public Box(Vector3 location, Vector3 size)
    {
        X = location.X;
        Y = location.Y;
        Z = location.Z;

        Width = size.X;
        Height = size.Y;
        Length = size.Z;
    }

    public Vector3 Location
    {
        get => new Vector3(X, Y, Z);
        set
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }

    public Vector3 Size
    {
        get => new Vector3(Width, Height, Length);
        set
        {
            Width = value.X;
            Height = value.Y;
            Length = value.Z;
        }
    }

    public double Left => X;
    public double Top => Y;
    public double Front => Z;
    public double Right => X + Width;
    public double Bottom => Y + Height;
    public double Back => Z + Length;

    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 TopFront => new Vector2(Front, Top);
    public Vector2 TopBack => new Vector2(Back, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);
    public Vector2 BottomFront => new Vector2(Front, Bottom);
    public Vector2 BottomBack => new Vector2(Front, Bottom);
    public Vector2 FrontLeft => new Vector2(Left, Front);
    public Vector2 FrontRight => new Vector2(Right, Front);
    public Vector2 BackLeft => new Vector2(Left, Back);
    public Vector2 BackRight => new Vector2(Right, Back);

    public Vector3 FrontTopLeft => new Vector3(Left, Top, Front);
    public Vector3 BackTopLeft => new Vector3(Left, Top, Back);
    public Vector3 BackTopRight => new Vector3(Right, Top, Back);
    public Vector3 FrontTopRight => new Vector3(Right, Top, Front);
    public Vector3 FrontBottomLeft => new Vector3(Left, Bottom, Front);
    public Vector3 BackBottomLeft => new Vector3(Left, Bottom, Back);
    public Vector3 BackBottomRight => new Vector3(Right, Bottom, Back);
    public Vector3 FromBottomRight => new Vector3(Right, Bottom, Front);

    public Vector3 Centre => new Vector3();

    public bool IsEmpty => Width <= 0 || Height <=0 || Length <=0;
    public double Area => Width * Height * Length;

    public static Box FromLTFRBB(double left, double top, double front, 
                                 double right, double bottom, double back)
    {
        Est.FromLTFRBB(left, top, front, right, bottom, back, out Box result);
        return result;
    }

    public static Box FromDimensions(double left, double top, double front, 
                                     double height, double width, double length)
    {
        Est.FromDimensions(left, top, front, width, height, length, out Box result);
        return result;
    }

    public static Box FromDimension(Vector3 position, Vector3 size)
    {
        Est.FromDimensions(position, size, out Box result);
        return result;
    }

    public static Box Multiply(Box box, double scale)
    {
        Est.Multiply(box, scale, out Box result);
        return result;
    }

    public static Box Multiply(Box box, Vector3 scale)
    {
        Est.Multiply(box, scale, out Box result);
        return result;
    }

    public static Box Divide(Box box, double scale)
    {
        Est.Divide(box, scale, out Box result);
        return result;
    }

    public static Box Divide(Box box, Vector3 scale)
    {
        Est.Divide(box, scale, out Box result);
        return result;
    }

    public static bool operator ==(Box left, Box right)
        => left.Equals(right);
    public static bool operator !=(Box left, Box right)
        => !left.Equals(right);

    public static Box operator *(Box box, double scale)
        => Multiply(box, scale);

    public static Box operator *(double scale, Box box)
        => Multiply(box, scale);

    public static Box operator *(Box box, Vector3 scale)
        => Multiply(box, scale);

    public static Box operator *(Vector3 scale, Box box)
        => Multiply(box, scale);

    public static Box operator /(Box box, double scale)
        => Divide(box, scale);

    public static Box operator /(double scale, Box box)
        => Divide(box, scale);

    public static Box operator /(Box box, Vector3 scale)
        => Divide(box, scale);

    public static Box operator /(Vector3 scale, Box box)
        => Divide(box, scale);

    public bool Contains(Vector3 pt)
        => Contains(pt.X, pt.Y, pt.Z);

    public bool Contains(Box box)
        => X <= box.X && box.X + box.Width <= X + Width 
        && Y <= box.Y && box.Y + box.Height <= Y + Height
        && Z <= box.Z && box.Z + box.Length <= Z + Length;

    public bool Contains(double x, double y, double z)
        => X <= x && x <= X + Width
        && Y <= y && x <= Y + Height
        && Z <= z && z <= Z + Length;

    public override bool Equals(object? obj)
        => obj is Box && Equals((Box)obj);

    public bool Equals(Box other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(Left, Right, Top, Bottom, Front, Back);

    public override string ToString()
        => String.Format("({0}{6} {1}) - ({2}{6} {3}) - ({4}{6} {5})", Left, Top, Right, Bottom, Front, Back, MathHelper.ListSeparator);
}