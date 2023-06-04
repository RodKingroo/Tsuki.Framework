using System.Runtime.CompilerServices;

using Tsuki.Framework.Math.Matrix;
using Tsuki.Framework.Math.Data;

namespace Tsuki.Framework.Math.Vector;

public struct Vector4 : IEquatable<Vector4>
{
    public double X;
    public double Y;
    public double Z;
    public double W;

    public Vector4(double value) : this(value, value, value, value)
    {

    }

    public Vector4(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public Vector4(Vector2 v)
    {
        X = v.X;
        Y = v.Y;
        Z = 0.0;
        W = 0.0;
    }

    public Vector4(Vector3 v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
        W = 0.0;
    }

    public Vector4(Vector3 v, double w)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
        W = w;
    }

    public Vector4(Vector4 v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
        W = v.W;
    }

    public static Vector4 UnitX = new Vector4(1, 0, 0, 0);
    public static Vector4 UnitY = new Vector4(0, 1, 0, 0);
    public static Vector4 UnitZ = new Vector4(0, 0, 1, 0);
    public static Vector4 UnitW = new Vector4(0, 0, 0, 1);
    public static Vector4 Zero = new Vector4(0, 0, 0, 0);
    public static Vector4 One = new Vector4(1, 1, 1, 1);

    public static Vector4 PositiveInfinity
        = new Vector4(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
    public static Vector4 NegativeInfinity
        = new Vector4(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

    public static int SizeInBytes = Unsafe.SizeOf<Vector4>();

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

    public Vector2 XW
    {
        get => new Vector2(X, W);
        set
        {
            X = value.X;
            W = value.Y;
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

    public Vector2 YW
    {
        get => new Vector2(Y, W);
        set
        {
            Y = value.X;
            W = value.Y;
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

    public Vector2 ZW
    {
        get => new Vector2(Z, W);
        set
        {
            Z = value.X;
            W = value.Y;
        }
    }

    public Vector2 WX
    {
        get => new Vector2(W, X);
        set
        {
            W = value.X;
            X = value.Y;
        }
    }

    public Vector2 WY
    {
        get => new Vector2(W, Y);
        set
        {
            W = value.X;
            Y = value.Y;
        }
    }

    public Vector2 WZ
    {
        get => new Vector2(W, Z);
        set
        {
            W = value.X;
            Z = value.Y;
        }
    }

    public Vector3 XYZ
    {
        get => new Vector3(X, Y, Z);
        set
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }

    public Vector3 XYW
    {
        get => new Vector3(X, Y, W);
        set
        {
            X = value.X;
            Y = value.Y;
            W = value.Z;
        }
    }

    public Vector3 XZY
    {
        get => new  Vector3(X, Z, Y);
        set
        {
            X = value.X;
            Z = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 XZW
    {
        get => new Vector3(X, Z, W);
        set
        {
            X = value.X;
            Z = value.Y;
            W = value.Z;
        }
    }

    public Vector3 XWY
    {
        get => new Vector3(X, W, Y);
        set
        {
            X = value.X;
            W = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 XWZ
    {
        get => new Vector3(X, W, Z);
        set
        {
            X = value.X;
            W = value.Y;
            Z = value.Z;
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

    public Vector3 YXW
    {
        get => new Vector3(Y, X, W);
        set
        {
            Y = value.X;
            X = value.Y;
            W = value.Z;
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

    public Vector3 YZW
    {
        get => new Vector3(Y, Z, W);
        set
        {
            Y = value.X;
            Z = value.Y;
            W = value.Z;
        }
    }

    public Vector3 YWX
    {
        get => new Vector3(Y, W, X);
        set
        {
            Y = value.X;
            W = value.Y;
            X = value.Z;
        }
    }

    public Vector3 YWZ
    {
        get => new Vector3(Y, W, Z);
        set
        {
            Y = value.X;
            W = value.Y;
            Z = value.Z;
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

    public Vector3 ZXW
    {
        get => new Vector3(Z, X, W);
        set
        {
            Z = value.X;
            X = value.Y;
            W = value.Z;
        }
    }

    public Vector3 ZYX
    {
        get => new Vector3(Z, Y, X);
        set
        {
            Z = value.Z;
            Y = value.Y;
            X = value.Z;
        }
    }

    public Vector3 ZYW
    {
        get => new Vector3(Z, Y, W);
        set
        {
            Z = value.X;
            Y = value.Y;
            W = value.Z;
        }
    }

    public Vector3 ZWX
    {
        get => new Vector3(Z, W, X);
        set
        {
            Z = value.X;
            W = value.Y;
            X = value.Z;
        }
    }

    public Vector3 ZWY
    {
        get => new Vector3(Z, W, Y);
        set
        {
            Z = value.X;
            W = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 WXY
    {
        get => new Vector3(W, X, Y);
        set
        {
            W = value.X;
            X = value.Y;
            Y = value.Z;
        }
    }

    public Vector3 WXZ
    {
        get => new Vector3(W, X, Z);
        set
        {
            W = value.X;
            X = value.Y;
            Z = value.Z;
        }
    }

    public Vector3 WYX
    {
        get => new Vector3(W, Y, X);
        set
        {
            W = value.X;
            Y = value.Y;
            X = value.Z;
        }
    }

    public Vector3 WYZ
    {
        get => new Vector3(W, Y, Z);
        set
        {
            W = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }

    public Vector3 WZX
    {
        get => new Vector3(W, Z, X);
        set
        {
            W = value.X;
            Z = value.Y;
            X = value.Z;
        }
    }

    public Vector3 WZY
    {
        get => new Vector3(W, Z, Y);
        set
        {
            W = value.X;
            Z = value.Y;
            Y = value.Z;
        }
    }

    public Vector4 XYWZ
    {
        get => new Vector4(X, Y, W, Z);
        set
        {
            X = value.X;
            Y = value.Y;
            W = value.Z;
            Z = value.W;
        }
    }

    public Vector4 XZYW
    {
        get => new Vector4(X, Z, Y, W);
        set
        {
            X = value.X;
            Z = value.Y;
            Y = value.Z;
            W = value.W;
        }
    }

    public Vector4 XZWY
    {
        get => new Vector4(X, Z, W, Y);
        set
        {
            X = value.X;
            Z = value.Y;
            W = value.Z;
            Y = value.W;
        }
    }

    public Vector4 XWYZ
    {
        get => new Vector4(X, W, Y, Z);
        set
        {
            X = value.X;
            W = value.Y;
            Y = value.Z;
            Z = value.W;
        }
    }

    public Vector4 XWZY
    {
        get => new Vector4(X, W, Z, Y);
        set
        {
            X = value.X;
            W = value.Y;
            Z = value.Z;
            Y = value.W;
        }
    }

    public Vector4 YXZW
    {
        get => new Vector4(Y, X, Z, W);
        set
        {
            Y = value.X;
            X = value.Y;
            Z = value.Z;
            W = value.W;
        }
    }

    public Vector4 YXWZ
    {
        get => new Vector4(Y, X, W, Z);
        set
        {
            Y = value.X;
            X = value.Y;
            W = value.Z;
            Z = value.W;
        }
    }

    public Vector4 YZXW
    {
        get => new Vector4(Y, Z, X, W);
        set
        {
            Y = value.X;
            Z = value.Y;
            X = value.Z;
            W = value.W;
        }
    }

    public Vector4 YZWX
    {
        get => new Vector4(Y, Z, W, X);
        set
        {
            Y = value.X;
            Z = value.Y;
            W = value.Z;
            X = value.W;
        }
    }

    public Vector4 YWXZ
    {
        get => new Vector4(Y, W, X, Z);
        set
        {
            Y = value.X;
            W = value.Y;
            X = value.Z;
            Z = value.W;
        }
    }

    public Vector4 YWZX
    {
        get => new Vector4(Y, W, Z, X);
        set
        {
            Y = value.X;
            W = value.Y;
            Z = value.Z;
            X = value.W;
        }
    }

    public Vector4 ZXYW
    {
        get => new Vector4(Z, X, Y, W);
        set
        {
            Z = value.X;
            X = value.Y;
            Y = value.Z;
            W = value.W;
        }
    }

    public Vector4 ZXWY
    {
        get => new Vector4(Z, X, W, Y);
        set
        {
            Z = value.X;
            X = value.Y;
            W = value.Z;
            Y = value.W;
        }
    }

    public Vector4 ZYXW
    {
        get => new Vector4(Z, Y, X, W);
        set
        {
            Z = value.X;
            Y = value.Y;
            X = value.Z;
            W = value.W;
        }
    }

    public Vector4 ZYWX
    {
        get => new Vector4(Z, Y, W, X);
        set
        {
            Z = value.X;
            Y = value.Y;
            W = value.Z;
            X = value.W;
        }
    }

    public Vector4 ZWXY
    {
        get => new Vector4(Z, W, X, Y);
        set
        {
            Z = value.X;
            W = value.Y;
            X = value.Z;
            Y = value.W;
        }
    }

    public Vector4 ZWYX
    {
        get => new Vector4(Z, W, Y, X);
        set
        {
            Z = value.X;
            W = value.Y;
            Y = value.Z;
            X = value.W;
        }
    }

    public Vector4 WXYZ
    {
        get => new Vector4(W, X, Y, Z);
        set
        {
            W = value.X;
            X = value.Y;
            Y = value.Z;
            Z = value.W;
        }
    }

    public Vector4 WXZY
    {
        get => new Vector4(W, X, Z, Y);
        set
        {
            W = value.X;
            X = value.Y;
            Z = value.Z;
            Y = value.W;
        }
    }

    public Vector4 WYXZ
    {
        get => new Vector4(W, Y, X, Z);
        set
        {
            W = value.X;
            Y = value.Y;
            X = value.Z;
            Z = value.W;
        }
    }

    public Vector4 WYZX
    {
        get => new Vector4(W, Y, Z, X);
        set
        {
            W = value.X;
            Y = value.Y;
            Z = value.Z;
            X = value.W;
        }
    }

    public Vector4 WZXY
    {
        get => new Vector4(W, Z, X, Y);
        set
        {
            W = value.X;
            Z = value.Y;
            X = value.Z;
            Y = value.W;
        }
    }

    public Vector4 WZYX
    {
        get => new Vector4(W, Z, Y, X);
        set
        {
            W = value.X;
            Z = value.Y;
            Y = value.Z;
            X = value.W;
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
                3 => W,
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
                case 3:
                    W = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"You tried to set this vector at index: {index}");
            }
        }
    }

    public double LengthSquared
        => System.Math.Pow(X, 2) + System.Math.Pow(Y, 2) + System.Math.Pow(Z, 2) + System.Math.Pow(W, 2);

    public double Length
        => System.Math.Sqrt(LengthSquared);

    public double LengthFast
        => MathHelper.InverseSqrtFast(LengthSquared);

    public Vector4 Normalized()
    {
        var v = this;
        v.Normalize();
        return v;
    }

    public void Normalize()
    {
        var scale = 1 / Length;
        X *= scale;
        Y *= scale;
        Z *= scale;
        W *= scale;
    }

    public void NormalizeFast()
    {
        var scale = 1 / LengthFast;
        X *= scale;
        Y *= scale;
        Z *= scale;
        W *= scale;
    }

    public static Vector4 Add(Vector4 a, Vector4 b)
    {
        Est.Add(a, b, out Vector4 result);
        return result;
    }

    public static Vector4 Subtract(Vector4 a, Vector4 b)
    {
        Est.Subtract(a, b, out Vector4 result);
        return result;
    }

    public static Vector4 Negative(Vector4 vector)
    {
        Est.Negative(vector, out Vector4 result);
        return result;
    }

    public static Vector4 Multiply(Vector4 vector, double scale)
    {
        Est.Multiply(vector, scale, out Vector4 result);
        return result;
    }

    public static Vector4 Multiply(Vector4 vector, Vector4 scale)
    {
        Est.Multiply(vector, scale, out Vector4 result);
        return result;
    }

    public static Vector4 Divide(Vector4 vector, double scale)
    {
        Est.Divide(vector, scale, out Vector4 result);
        return result;
    }

    public static Vector4 Divide(Vector4 vector, Vector4 scale)
    {
        Est.Divide(vector, scale, out Vector4 result);
        return result;
    }

    public static Vector4 ComponentMin(Vector4 a, Vector4 b)
    {
        Est.ComponentMin(a, b, out Vector4 result);
        return result;
    }

    public static Vector4 ComponentMax(Vector4 a, Vector4 b)
    {
        Est.ComponentMax(a, b, out Vector4 result);
        return result;
    }

    public static Vector4 MagnitudeMin(Vector4 left, Vector4 right)
    {
        Est.MagnitudeMin(left, right, out Vector4 result);
        return result;
    }

    public static Vector4 MagnitudeMax(Vector4 left, Vector4 right)
    {
        Est.MagnitudeMax(left, right, out Vector4 result);
        return result;
    }

    public static Vector4 Clamp(Vector4 vec, Vector4 min, Vector4 max)
    {
        Est.Clamp(vec, min, max, out Vector4 result);
        return result;
    }

    public static double Distance(Vector4 vec1, Vector4 vec2)
    {
        Est.Distance(vec1, vec2, out double result);
        return result;
    }

    public static double DistanceSquared(Vector4 vec1, Vector4 vec2)
    {
        Est.DistanceSquared(vec1, vec2, out double result);
        return result;
    }

    public static Vector4 Normalize(Vector4 vec)
    {
        Est.Normalize(vec, out Vector4 result);
        return result;
    }

    public static Vector4 NormalizeFast(Vector4 vec)
    {
        Est.NormalizeFast(vec, out Vector4 result);
        return result;
    }

    public static double Dot(Vector4 left, Vector4 right)
    {
        Est.Dot(left, right, out double result);
        return result;
    }

    public static Vector4 Lerp(Vector4 a, Vector4 b, double blend)
    {
        Est.Lerp(a, b, blend, out Vector4 result);
        return result;
    }

    public static Vector4 Lerp(Vector4 a, Vector4 b, Vector4 blend)
    {
        Est.Lerp(a, b, blend, out Vector4 result);
        return result;
    }

    public static Vector4 BaryCentric(Vector4 a, Vector4 b, Vector4 c, double u, double v)
    {
        Est.BaryCentric(a, b, c, u, v, out Vector4 result);
        return result;
    }

    public static Vector4 TransformVector(Vector4 vec, Matrix4 mat)
    {
        Est.TransformVector(vec, mat, out Vector4 result);
        return result;
    }

    public static Vector4 TransformNormal(Vector4 vec, Matrix4 mat)
    {
        Est.TransformNormal(vec, mat, out Vector4 result);
        return result;
    }

    public static Vector4 TransformPosition(Vector4 pos, Matrix4 mat)
    {
        Est.TransformPosition(pos, mat, out Vector4 result);
        return result;
    }

    public static Vector4 TransformRow(Vector4 vec, Matrix4 mat)
    {
        Est.TransformRow(vec, mat, out Vector4 result);
        return result;
    }

    public static Vector4 Transform(Vector4 vec, Quaternion quat)
    {
        Est.Transform(vec, quat, out Vector4 result);
        return result;
    }

    public static Vector4 TransformColumn(Matrix4 mat, Vector4 vec)
    {
        Est.TransformColumn(mat, vec, out Vector4 result);
        return result;
    }

    public static Vector4 TransformPerspective(Vector4 vec, Matrix4 mat)
    {
        Est.TransformPerspective(vec, mat, out Vector4 result);
        return result;
    }

    public static double CalculateAngle(Vector3 first, Vector3 second)
    {
        Est.CalculateAngle(first, second, out double result);
        return result;
    }

    public static Vector4 operator +(Vector4 left, Vector4 right)
        => Add(left, right);

    public static Vector4 operator -(Vector4 left, Vector4 right)
        => Subtract(left, right);

    public static Vector4 operator -(Vector4 vec)
        => Negative(vec);

    public static Vector4 operator *(Vector4 vec, double scale)
        => Multiply(vec, scale);

    public static Vector4 operator *(double scale, Vector4 vec)
        => Multiply(vec, scale);

    public static Vector4 operator *(Vector4 vec, Vector4 scale)
        => Multiply(vec, scale);

    public static Vector4 operator *(Vector4 vec, Matrix4 mat)
        => TransformRow(vec, mat);

    public static Vector4 operator *(Matrix4 mat, Vector4 vec)
        => TransformColumn(mat, vec);

    public static Vector4 operator *(Quaternion quat, Vector4 vec)
        => Transform(vec, quat);

    public static Vector4 operator /(Vector4 vec, double scale)
        => Divide(vec, scale);

    public static Vector4 operator /(Vector4 vec, Vector4 scale)
        => Divide(vec, scale);

    public static bool operator ==(Vector4 left, Vector4 right)
        => left.Equals(right);

    public static bool operator !=(Vector4 left, Vector4 right)
        => !left.Equals(right);

    public override string ToString()
        => string.Format("({0}{4} {1}{4} {2}{4} {3})", X, Y, Z, W, MathHelper.ListSeparator);

    public override bool Equals(object? obj)
        => obj is Vector4 && Equals((Vector4)obj);

    public bool Equals(Vector4 other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(X, Y, Z, W);

    public void Deconstruct(double x, double y, double z, double w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }
}