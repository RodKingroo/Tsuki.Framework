using System.Runtime.CompilerServices;

using Tsuki.Framework.Math.Matrix;
using Tsuki.Framework.Math.Data;

namespace Tsuki.Framework.Math.Vector;

public struct Vector3 : IEquatable<Vector3>
{
    public double X;
    public double Y;
    public double Z;

    public Vector3(double value) : this(value, value, value)
    {
        
    }

    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3(Vector2 v)
    {
        X = v.X;
        Y = v.Y;
        Z = 0.0;
    }

    public Vector3(Vector3 v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
    }

    public Vector3(Vector4 v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
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

    public Vector2 XZ
    {
        get => new Vector2(X, Z);
        set
        {
            X = value.X;
            Z = value.Y;
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

    public Vector2 YZ
    {
        get => new Vector2(Y, Z);
        set
        {
            Y = value.X;
            Z = value.Y;
        }
    }

    public Vector2 ZX
    {
        get => new Vector2(Z, X);
        set
        {
            Z = value.X;
            X = value.Y;
        }
    }

    public Vector2 ZY
    {
        get => new Vector2(Z, Y);
        set
        {
            Z = value.X;
            Y = value.Y;
        }
    }

    public Vector3 XZY
    {
        get => new Vector3(X, Z, Y);
        set
        {
            X = value.X;
            Z = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 YXZ
    {
        get => new Vector3(Y, X, Z);
        set
        {
            Y = value.X;
            X = value.Y;
            Z = value.Z;
        }
    }

    public Vector3 YZX
    {
        get => new Vector3(Y, Z, X);
        set
        {
            Y = value.X;
            Z = value.Y;
            X = value.Z;
        }
    }

    public Vector3 ZXY
    {
        get => new Vector3(Z, X, Y);
        set
        {
            Z = value.X;
            X = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 ZYX
    {
        get => new Vector3(Z, Y, X);
        set
        {
            Z = value.X;
            Y = value.Y;
            X = value.X;
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
                2 => Z,
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
                case 2:
                    Z = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"You tried to set this vector at index: {index}");
            }
        }
    }

    public double LengthSquared 
        => System.Math.Pow(X, 2) + System.Math.Pow(Y, 2) + System.Math.Pow(Z, 2);

    public double Length => System.Math.Sqrt(LengthSquared);

    public double LengthFast => MathHelper.InverseSqrtFast(LengthSquared);

    public Vector3 Normalized()
    {
        Vector3 v = this;
        v.Normalized();
        return v;
    }

    public void Normalize()
    {
        double scale = 1 / Length;
        X *= scale;
        Y *= scale;
        Z *= scale;
    }

    public void NormalizeFast()
    {
        double scale = 1 / LengthFast;
        X *= scale;
        Y *= scale;
        Z *= scale;
    }

    public static Vector3 UnitX = new Vector3(1, 0, 0);
    public static Vector3 UnitY = new Vector3(0, 1, 0);
    public static Vector3 UnitZ = new Vector3(0, 0, 1);
    public static Vector3 Zero = new Vector3(0, 0, 0);
    public static Vector3 One = new Vector3(1, 1, 1);

    public static Vector3 PositiveInfinity 
        = new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

    public static Vector3 NegativeInfinity 
        = new Vector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

    public static int SizeInBytes = Unsafe.SizeOf<Vector3>();

    public static Vector3 Add(Vector3 a, Vector3 b)
    {
        Est.Add(a, b, out Vector3 result);
        return result;
    }

    public static Vector3 Subtract(Vector3 a, Vector3 b)
    {
        Est.Subtract(a, b, out Vector3 result);
        return result;
    }

    public static Vector3 Negative(Vector3 a)
    {
        Est.Negative(a, out Vector3 result);
        return result;
    }

    public static Vector3 Multiply(Vector3 vector, double scale)
    {
        Est.Multiply(vector, scale, out Vector3 result);
        return result;
    }

    public static Vector3 Multiply(Vector3 vector, Vector3 scale)
    {
        Est.Multiply(vector, scale, out Vector3 result);
        return result;
    }

    public static Vector3 Divide(Vector3 vector, double scale)
    {
        Est.Divide(vector, scale, out Vector3 result);
        return result;
    }

    public static Vector3 Divide(Vector3 vector, Vector3 scale)
    {
        Est.Divide(vector, scale, out Vector3 result);
        return result;
    }

    public static Vector3 ComponentMin(Vector3 a, Vector3 b)
    {
        Est.ComponentMin(a, b, out Vector3 result);
        return result;
    }

    public static Vector3 ComponentMax(Vector3 a, Vector3 b)
    {
        Est.ComponentMax(a, b, out Vector3 result);
        return result;
    }

    public static Vector3 MagnitudeMin(Vector3 left, Vector3 right)
    {
        Est.MagnitudeMin(left, right, out Vector3 result);
        return result;
    }

    public static Vector3 MagnitudeMax(Vector3 left, Vector3 right)
    {
        Est.MagnitudeMax(left, right, out Vector3 result);
        return result;
    }

    public static Vector3 Clamp(Vector3 vec, Vector3 min, Vector3 max)
    {
        Est.Clamp(vec, min, max, out Vector3 result);
        return result;
    }

    public static double Distance(Vector3 vec1, Vector3 vec2)
    {
        Est.Distance(vec1, vec2, out double result);
        return result;
    }

    public static double DistanceSquared(Vector3 vec1, Vector3 vec2)
    {
        Est.DistanceSquared(vec1, vec2, out double result);
        return result;
    }

    public static Vector3 Normalize(Vector3 vec)
    {
        Est.Normalize(vec, out Vector3 result);
        return result;
    }

    public static Vector3 NormalizeFast(Vector3 vec)
    {
        Est.NormalizeFast(vec, out Vector3 result);
        return result;
    }

    public static double Dot(Vector3 left, Vector3 right)
    {
        Est.Dot(left, right, out double result);
        return result;
    }

    public static Vector3 Cross(Vector3 left, Vector3 right)
    {
        Est.Cross(left, right, out Vector3 result);
        return result;
    }

    public static Vector3 Lerp(Vector3 a, Vector3 b, double blend)
    {
        Est.Lerp(a, b, blend, out Vector3 result);
        return result;
    }

    public static Vector3 Lerp(Vector3 a, Vector3 b, Vector3 blend)
    {
        Est.Lerp(a, b, blend, out Vector3 result);
        return result;
    }

    public static Vector3 BaryCentric(Vector3 a, Vector3 b, Vector3 c, double u, double v)
    {
        Est.BaryCentric(a, b, c, u, v, out Vector3 result);
        return result;
    }
    public static Vector3 TransformVector(Vector3 vec, Matrix3 mat)
    {
        Est.TransformVector(vec, mat, out Vector3 result);
        return result;
    }

    public static Vector3 TransformNormal(Vector3 norm, Matrix3 mat)
    {
        Est.TransformNormal(norm, mat, out Vector3 result);
        return result;
    }

    public static Vector3 TransformNormalInverse(Vector3 norm, Matrix3 invMat)
    {
        Est.TransformNormalInverse(norm, invMat, out Vector3 result);
        return result;
    }

    public static Vector3 TransformPosition(Vector3 pos, Matrix3 mat)
    {
        Est.TransformPosition(pos, mat, out Vector3 result);
        return result;
    }

    public static Vector3 TransformRow(Vector3 vec, Matrix3 mat)
    {
        Est.TransformRow(vec, mat, out Vector3 result);
        return result;
    }

    public static Vector3 Transform(Vector3 vec, Quaternion quat)
    {
        Est.Transform(vec, quat, out Vector3 result);
        return result;
    }

    public static Vector3 TransformColumn(Matrix3 mat, Vector3 vec)
    {
        Est.TransformColumn(mat, vec, out Vector3 result);
        return result;
    }

    public static Vector3 TransformPerspective(Vector3 vec, Matrix4 mat)
    {
        Est.TransformPerspective(vec, mat, out Vector3 result);
        return result;
    }

    public static double CalculateAngle(Vector3 first, Vector3 second)
    {
        Est.CalculateAngle(first, second, out double result);
        return result;
    }

    public static Vector3 operator +(Vector3 left, Vector3 right)
        => Add(left, right);

    public static Vector3 operator -(Vector3 left, Vector3 right)
        => Subtract(left, right);

    public static Vector3 operator -(Vector3 vec)
        => Negative(vec);

    public static Vector3 operator *(Vector3 vec, double scale)
        => Multiply(vec, scale);

    public static Vector3 operator *(double scale, Vector3 vec)
        => Multiply(vec, scale);

    public static Vector3 operator *(Vector3 vec, Vector3 scale)
        => Multiply(vec, scale);

    public static Vector3 operator *(Vector3 vec, Matrix3 mat)
        => TransformRow(vec, mat);

    public static Vector3 operator *(Matrix3 mat, Vector3 vec)
        => TransformRow(vec, mat);

    public static Vector3 operator *(Vector3 vec, Quaternion quat)
        => Transform(vec, quat);

    public static Vector3 operator /(Vector3 vec, double scale)
        => Divide(vec, scale); 

    public static bool operator ==(Vector3 left, Vector3 right)
        => left.Equals(right);

    public static bool operator !=(Vector3 left, Vector3 right)
        => !left.Equals(right);

    public static implicit operator Vector3((double X, double Y, double Z) values)
        => new Vector3(values.X, values.Y, values.Z);

    public override string ToString()
        => string.Format("({0}{3} {1}{3} {2})", X, Y, Z, MathHelper.ListSeparator);

    public override bool Equals(object? obj)
        => obj is Vector3 && Equals((Vector3)obj);

    public bool Equals(Vector3 other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(X, Y, Z);

    public void Deconstruct(double x, double y, double z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}