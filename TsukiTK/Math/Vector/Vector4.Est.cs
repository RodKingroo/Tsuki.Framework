using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Matrix;

namespace Tsuki.Framework.Math.Vector;

internal partial class Est
{
    internal static void Add(Vector4 a, Vector4 b, out Vector4 result)
    {
        result = new Vector4
        (
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z,
            a.W + b.W
        );
    }

    internal static void Subtract(Vector4 a, Vector4 b, out Vector4 result)
    {
        result = new Vector4
        (
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z,
            a.W - b.W
        );
    }

    internal static void Negative(Vector4 a, out Vector4 result)
    {
         result = new Vector4(-a.X, -a.Y, -a.Z, -a.W);
    }

    internal static void Multiply(Vector4 vector, double scale, out Vector4 result)
    {
        result = new Vector4
        (
            vector.X * scale,
            vector.Y * scale,
            vector.Z * scale,
            vector.W * scale
        );
    }

    internal static void Multiply(Vector4 vector, Vector4 scale, out Vector4 result)
    {
        result = new Vector4
        (
            vector.X * scale.X,
            vector.Y * scale.Y,
            vector.Z * scale.Z,
            vector.W * scale.W
        );
    }


    internal static void Divide(Vector4 vector, double scale, out Vector4 result)
    {
        result = new Vector4
        (
            vector.X / scale,
            vector.Y / scale,
            vector.Z / scale,
            vector.W / scale
        );
    }

    internal static void Divide(Vector4 vector, Vector4 scale, out Vector4 result)
    {
        result = new Vector4
        (
            vector.X / scale.X,
            vector.Y / scale.Y,
            vector.Z / scale.Z,
            vector.W / scale.W
        );
    }
    
    internal static void ComponentMin(Vector4 a, Vector4 b, out Vector4 result)
    {
        if (a.X < b.X) result.X = a.X;
        else result.X = b.X;

        if (a.Y < b.Y) result.Y = a.Y;
        else result.Y = b.Y;

        if (a.Z < b.Z) result.Z = a.Z;
        else result.Z = b.Z;

        if (a.W < b.W) result.W = a.W;
        else result.W = b.W;
    }

    internal static void ComponentMax(Vector4 a, Vector4 b, out Vector4 result)
    {
        if (a.X > b.X) result.X = a.X;
        else result.X = b.X;

        if (a.Y > b.Y) result.Y = a.Y;
        else result.Y = b.Y;

        if (a.Z > b.Z) result.Z = a.Z;
        else result.Z = b.Z;

        if (a.W > b.W) result.W = a.W;
        else result.W = b.W;
    }

    internal static void MagnitudeMin(Vector4 left, Vector4 right, out Vector4 result)
    {
        if (left.LengthSquared < right.LengthSquared) result = left;
        else result = right;
    }
    
    internal static void MagnitudeMax(Vector4 left, Vector4 right, out Vector4 result)
    {
        if (left.LengthSquared >= right.LengthSquared) result = left; 
        else result = right;
    }

    internal static void Clamp(Vector4 vec, Vector4 min, Vector4 max, out Vector4 result)
    {
        if (vec.X < min.X) result.X = min.X;
        else if (vec.X > max.X) result.X = max.X;
        else result.X = vec.X;

        if (vec.Y < min.Y) result.Y = min.Y;
        else if (vec.Y > max.Y) result.Y = max.Y;
        else result.Y = vec.Y;

        if (vec.X < min.Z) result.Z = min.Z;
        else if (vec.Z > max.Z) result.Z = max.Z;
        else result.Z = vec.Z;

        if (vec.Y < min.W) result.W = min.W;
        else if (vec.W > max.W) result.W = max.W;
        else result.W = vec.W;
    }

    internal static void Distance(Vector4 vec1, Vector4 vec2, out double result)
    {
        result = System.Math.Sqrt(((vec2.X - vec1.X) * (vec2.X - vec1.X)) 
                                + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y)) 
                                + ((vec2.Z - vec1.Z) * (vec2.Z - vec1.Z))
                                + ((vec2.W - vec1.W) * (vec2.W - vec1.W)));
    }

    internal static void DistanceSquared(Vector4 vec1, Vector4 vec2, out double result)
    {
        result = ((vec2.X - vec1.X) * (vec2.X - vec1.X))
               + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y)) 
               + ((vec2.Z - vec1.Z) * (vec2.Z - vec1.Z))
               + ((vec2.W - vec1.W) * (vec2.W - vec1.W));
    }

    internal static void Normalize(Vector4 vec, out Vector4 result)
    {
        var scale = 1 / vec.Length;
        result = new Vector4
        (
            vec.X * scale,
            vec.Y * scale,
            vec.Z * scale,
            vec.W * scale
        );
    }

    
    internal static void NormalizeFast(Vector4 vec, out Vector4 result)
    {
        var scale = 1 / vec.LengthFast;
        result = new Vector4
        (
            vec.X * scale,
            vec.Y * scale,
            vec.Z * scale,
            vec.W * scale
        );
    }

    
    internal static void Dot(Vector4 left, Vector4 right, out double result)
    {
        result = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W);
    }

    internal static void Cross(Vector4 left, Vector4 right, out Vector4 result)
    {
        result = new Vector4
        (
            (left.Y * right.Z) - (left.Z * right.Y),
            (left.Z * right.X) - (left.X * right.Z),
            (left.X * right.Y) - (left.Y * right.X),
            0
        );
    }

    internal static void Lerp(Vector4 a, Vector4 b, double blend, out Vector4 result)
    {
        result = new Vector4
        (
            (blend * (b.X - a.X)) + a.X,
            (blend * (b.Y - a.Y)) + a.Y,
            (blend * (b.Z - a.Z)) + a.Z,
            (blend * (b.W - a.W)) + a.W
        );
    }

    internal static void Lerp(Vector4 a, Vector4 b, Vector4 blend, out Vector4 result)
    {
        result = new Vector4
        (
            (blend.X * (b.X - a.X)) + a.X,
            (blend.Y * (b.Y - a.Y)) + a.Y,
            (blend.Z * (b.Z - a.Z)) + a.Z,
            (blend.W * (b.W - a.W)) + a.W
        );
    }

    internal static void BaryCentric(Vector4 a, Vector4 b, Vector4 c, double u, double v, out Vector4 result)
    {
        Subtract(b, a, out Vector4 ab);
        Multiply(ab, u, out Vector4 abU);
        Add(a, abU, out Vector4 uPos);

        Subtract(c, a, out Vector4 ac);
        Multiply(ac, v, out Vector4 acV);
        Add(uPos, acV, out result);
    }

    internal static void TransformVector(Vector4 vec, Matrix4 mat, out Vector4 result)
    {
        result = new Vector4
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21) + (vec.Z * mat.M31) + (vec.W * mat.M41),
            (vec.X * mat.M12) + (vec.Y * mat.M22) + (vec.Z * mat.M32) + (vec.W * mat.M42),
            (vec.X * mat.M13) + (vec.Y * mat.M23) + (vec.Z * mat.M33) + (vec.Z * mat.M43),
            (vec.X * mat.M14) + (vec.Y * mat.M24) + (vec.Z * mat.M34) + (vec.Z * mat.M44)
        );
    }

    internal static void TransformNormal(Vector4 norm, Matrix4 mat, out Vector4 result)
    {
        Matrix4 invMat = Matrix4.Invert(mat);
        result = new Vector4
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12) + (norm.Z * invMat.M13) + (norm.W * invMat.M14),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22) + (norm.Z * invMat.M23) + (norm.W * invMat.M24),
            (norm.X * invMat.M31) + (norm.Y * invMat.M32) + (norm.Z * invMat.M33) + (norm.W * invMat.M34),
            (norm.X * invMat.M41) + (norm.Y * invMat.M42) + (norm.Z * invMat.M43) + (norm.W * invMat.M44)
        );
    }

    internal static void TransformNormalInverse(Vector4 norm, Matrix4 invMat, out Vector4 result)
    {
        result = new Vector4
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12) + (norm.Z * invMat.M13) + (norm.W * invMat.M14),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22) + (norm.Z * invMat.M23) + (norm.W * invMat.M24),
            (norm.X * invMat.M31) + (norm.Y * invMat.M32) + (norm.Z * invMat.M33) + (norm.W * invMat.M34),
            (norm.X * invMat.M41) + (norm.Y * invMat.M42) + (norm.Z * invMat.M43) + (norm.W * invMat.M44)
        );
    }

    internal static void TransformPosition(Vector4 pos, Matrix4 mat, out Vector4 result)
    {
        result = new Vector4
        (
            (pos.X * mat.M11) + (pos.Y * mat.M21) + (pos.Z * mat.M31) + (pos.W * mat.M41) + mat.M41,
            (pos.X * mat.M12) + (pos.Y * mat.M22) + (pos.Z * mat.M32) + (pos.W * mat.M42) + mat.M42,
            (pos.X * mat.M13) + (pos.Y * mat.M23) + (pos.Z * mat.M33) + (pos.W * mat.M43) + mat.M43,
            (pos.X * mat.M14) + (pos.Y * mat.M24) + (pos.Z * mat.M34) + (pos.W * mat.M44) + mat.M44
        );
    }

    internal static void TransformRow(Vector4 vec, Matrix4 mat, out Vector4 result)
    {
        result = new Vector4
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21) + (vec.Z * mat.M31) + (vec.W * mat.M41),
            (vec.X * mat.M12) + (vec.Y * mat.M22) + (vec.Z * mat.M32) + (vec.W * mat.M42),
            (vec.X * mat.M13) + (vec.Y * mat.M23) + (vec.Z * mat.M33) + (vec.W * mat.M43),
            (vec.X * mat.M14) + (vec.Y * mat.M24) + (vec.Z * mat.M34) + (vec.W * mat.M44)
        );
    }

    internal static void Transform(Vector4 vec, Quaternion quat, out Vector4 result)
    {
        var v = new Quaternion(vec.X, vec.Y, vec.Z, vec.W);

        Data.Est.Invert(quat, out Quaternion i);
        Data.Est.Multiply(quat, v, out Quaternion t);
        Data.Est.Multiply(t, i, out v);

        result = new Vector4(v.X, v.Y, v.Z, v.W);
    }

    internal static void TransformColumn(Matrix4 mat, Vector4 vec, out Vector4 result)
    {
        result = new Vector4
        (
            (mat.M11 * vec.X) + (mat.M21 * vec.Y) + (mat.M31 * vec.Z) + (mat.M41 * vec.W),
            (mat.M21 * vec.X) + (mat.M22 * vec.Y) + (mat.M32 * vec.Z) + (mat.M42 * vec.W),
            (mat.M32 * vec.X) + (mat.M23 * vec.Y) + (mat.M33 * vec.Z) + (mat.M43 * vec.W),
            (mat.M33 * vec.X) + (mat.M24 * vec.Y) + (mat.M34 * vec.Z) + (mat.M44 * vec.W)
        );
    }

    internal static void TransformPerspective(Vector4 vec, Matrix4 mat, out Vector4 result)
    {
        var v = new Vector4(vec.X, vec.Y, vec.Z, 1);
        TransformRow(v, mat, out v);
        result = new Vector4
        (
            v.X / v.W,
            v.Y / v.W,
            v.Z / v.W,
            1
        );
    }

    internal static void CalculateAngle(Vector4 first, Vector4 second, out double result)
    {
        double temp = (first.X * second.X) + (first.Y * second.Y) + (first.Z * second.Z) * (first.W * second.W);

        result = System.Math.Acos(MathHelper.Clamp(temp / (first.Length * second.Length), -1, 1));
    }

} 