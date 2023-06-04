using System.Globalization;
using System.Runtime.CompilerServices;

using Tsuki.Framework.Math.Matrix;
using Tsuki.Framework.Math.Data;

namespace Tsuki.Framework.Math.Vector;

public struct Vector2 : IEquatable<Vector2>
{
    public double X;
    public double Y;

    public Vector2(double value) : this(value, value)
    {
    }

    public Vector2(double x, double y)
    {
        X = y;
        Y = y;
    }

    public Vector2(Vector3 v)
    {
        X = v.X;
        Y = v.Y;
    }

    public Vector2(Vector4 v)
    {
        X = v.X;
        Y = v.Y;
    }

    public Vector2 XY
    {
        get => new Vector2(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }
    public Vector2 YX
    {
        get => new Vector2(Y, X);
        set
        {
            Y = value.X;
            X = value.Y;
        }
    }

    public double this[int index]
    {
        get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                _ => throw new IndexOutOfRangeException($"You tried to access this vector at index: {index}")
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    X = value;
                    break;
                case 1:
                    Y = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"You tried to set this vector at index: {index}");
            }
        }
    }

    public double LengthSquared 
        => System.Math.Pow(X, 2) + System.Math.Pow(Y, 2);

    public double Length => System.Math.Sqrt(LengthSquared);

    public double LengthFast => MathHelper.InverseSqrtFast(LengthSquared);

    public Vector2 PerpendicularRight => new Vector2(Y, -X);

    public Vector2 PerpendicularLeft => new Vector2(-Y, X);

    public Vector2 Normalized()
    {
        Vector2 v = this;
        v.Normalize();
        return v;
    }

    public void Normalize()
    {
        double scale = 1 / Length;
        X *= scale;
        Y *= scale;
    }

    public void NormalizeFast()
    {
        double scale = 1 / LengthFast;
        X *= scale;
        Y *= scale;
    }

    public static Vector2 UnitX = new Vector2(1, 0);
    public static Vector2 UnitY = new Vector2(0, 1);
    public static Vector2 Zero = new Vector2(0, 0);
    public static Vector2 One = new Vector2(1, 1);

    public static Vector2 PositiveInfinity = new Vector2(double.PositiveInfinity, double.PositiveInfinity);
    public static Vector2 NegativeInfinity = new Vector2(double.NegativeInfinity, double.NegativeInfinity);

    public static int SizeInBytes = Unsafe.SizeOf<Vector2>();
    
    public static Vector2 Add(Vector2 a, Vector2 b)
    {
        Est.Add(a, b, out Vector2 result);
        return result;
    }

    public static Vector2 Subtract(Vector2 a, Vector2 b)
    {
        Est.Subtract(a, b, out Vector2 result);
        return result;
    }

    public static Vector2 Negative(Vector2 a)
    {
        Est.Negative(a, out Vector2 result);
        return result;
    }

    public static Vector2 Multiply(Vector2 vector, double scale)
    {
        Est.Multiply(vector, scale, out Vector2 result);
        return result;
    }

    public static Vector2 Multiply(Vector2 vector, Vector2 scale)
    {
        Est.Multiply(vector, scale, out Vector2 result);
        return result;
    }

    public static Vector2 Divide(Vector2 vector, double scale)
    {
        Est.Divide(vector, scale, out Vector2 result);
        return result;
    }

    public static Vector2 Divide(Vector2 vector, Vector2 scale)
    {
        Est.Divide(vector, scale, out Vector2 result);
        return result;
    }

    public static Vector2 ComponentMin(Vector2 a, Vector2 b)
    {
        Est.ComponentMin(a, b, out Vector2 result);
        return result;
    }

    public static Vector2 ComponentMax(Vector2 a, Vector2 b)
    {
        Est.ComponentMax(a, b, out Vector2 result);
        return result;
    }

    public static Vector2 MagnitudeMin(Vector2 left, Vector2 right)
    {
        Est.MagnitudeMin(left, right, out Vector2 result);
        return result;
    }

    public static Vector2 MagnitudeMax(Vector2 left, Vector2 right)
    {
        Est.MagnitudeMax(left, right, out Vector2 result);
        return result;
    }

    public static Vector2 Clamp(Vector2 vec, Vector2 min, Vector2 max)
    {
        Est.Clamp(vec, min, max, out Vector2 result);
        return result;
    }

    public static double Distance(Vector2 vec1, Vector2 vec2)
    {
        Est.Distance(vec1, vec2, out double result);
        return result;
    }

    public static double DistanceSquared(Vector2 vec1, Vector2 vec2)
    {
        Est.DistanceSquared(vec1, vec2, out double result);
        return result;
    }

    public static Vector2 Normalize(Vector2 vec)
    {
        Est.Normalize(vec, out Vector2 result);
        return result;
    }

    public static Vector2 NormalizeFast(Vector2 vec)
    {
        Est.NormalizeFast(vec, out Vector2 result);
        return result;
    }

    public static double Dot(Vector2 left, Vector2 right)
    {
        Est.Dot(left, right, out double result);
        return result;
    }

    public static double Cross(Vector2 left, Vector2 right)
    {
        Est.Cross(left, right, out double result);
        return result;
    }

    public static Vector2 Lerp(Vector2 a, Vector2 b, double blend)
    {
        Est.Lerp(a, b, blend, out Vector2 result);
        return result;
    }

    public static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 blend)
    {
        Est.Lerp(a, b, blend, out Vector2 result);
        return result;
    }

    public static Vector2 BaryCentric(Vector2 a, Vector2 b, Vector2 c, double u, double v)
    {
        Est.BaryCentric(a, b, c, u, v, out Vector2 result);
        return result;
    }

    public static Vector2 TransformVector(Vector2 vec, Matrix2 mat)
    {
        Est.TransformVector(vec, mat, out Vector2 result);
        return result;
    }

    public static Vector2 TransformNormal(Vector2 norm, Matrix2 mat)
    {
        Est.TransformNormal(norm, mat, out Vector2 result);
        return result;
    }

    public static Vector2 TransformNormalInverse(Vector2 norm, Matrix2 invMat)
    {
        Est.TransformNormalInverse(norm, invMat, out Vector2 result);
        return result;
    }

    public static Vector2 TransformPosition(Vector2 pos, Matrix2 mat)
    {
        Est.TransformPosition(pos, mat, out Vector2 result);
        return result;
    }

    public static Vector2 TransformRow(Vector2 vec, Matrix2 mat)
    {
        Est.TransformRow(vec, mat, out Vector2 result);
        return result;
    }

    public static Vector2 Transform(Vector2 vec, Quaternion quat)
    {
        Est.Transform(vec, quat, out Vector2 result);
        return result;
    }

    public static Vector2 TransformColumn(Matrix2 mat, Vector2 vec)
    {
        Est.TransformColumn(mat, vec, out Vector2 result);
        return result;
    }

    public static Vector2 TransformPerspective(Vector2 vec, Matrix3 mat)
    {
        Est.TransformPerspective(vec, mat, out Vector2 result);
        return result;
    }

    public static double CalculateAngle(Vector2 first, Vector2 second)
    {
        Est.CalculateAngle(first, second, out double result);
        return result;
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
        => Add(left, right);

    public static Vector2 operator -(Vector2 left, Vector2 right)
        => Subtract(left, right);

    public static Vector2 operator -(Vector2 vec)
        => Negative(vec);

    public static Vector2 operator *(Vector2 vec, double scale)
        => Multiply(vec, scale);

    public static Vector2 operator *(double scale, Vector2 vec)
        => Multiply(vec, scale);

    public static Vector2 operator *(Vector2 vec, Vector2 scale)
        => Multiply(vec, scale);

    public static Vector2 operator *(Vector2 vec, Matrix2 mat)
        => TransformRow(vec, mat);

    public static Vector2 operator *(Matrix2 mat, Vector2 vec)
        => TransformColumn(mat, vec);

    public static Vector2 operator *(Quaternion quat, Vector2 vec)
        => Transform(vec, quat);

    public static Vector2 operator /(Vector2 vec, double scale)
        => Divide(vec, scale);

    public static Vector2 operator /(double scale, Vector2 vec)
        => Divide(vec, scale);

    public static Vector2 operator /(Vector2 vec, Vector2 scale)
        => Divide(vec, scale);

    public static bool operator ==(Vector2 left, Vector2 right)
        => left.Equals(right);

    public static bool operator !=(Vector2 left, Vector2 right)
        => !left.Equals(right);

    public static implicit operator Vector2((double X, double Y) values)
        => new Vector2(values.X, values.Y);

    public override string ToString()
        => string.Format("({0}{2} {1})", X, Y, MathHelper.ListSeparator);


    public override bool Equals(object? obj)
        => obj is Vector2 && Equals((Vector2)obj);

    public bool Equals(Vector2 other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(X, Y);
    
    public void Deconstruct(double x, double y)
    {
        x = X;
        x = Y;
    }
}