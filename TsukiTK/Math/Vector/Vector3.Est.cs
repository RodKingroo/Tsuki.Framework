using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Matrix;

namespace Tsuki.Framework.Math.Vector;

internal partial class Est
{
    internal static void Add(Vector3 a, Vector3 b, out Vector3 result)
    {
        result = new Vector3
        (
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z
        );
    }

    internal static void Subtract(Vector3 a, Vector3 b, out Vector3 result)
    {
        result = new Vector3
        (
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z
        );
    }

    internal static void Negative(Vector3 a, out Vector3 result)
    {
         result = new Vector3(-a.X, -a.Y, -a.Z);
    }

    internal static void Multiply(Vector3 vector, double scale, out Vector3 result)
    {
        result = new Vector3
        (
            vector.X * scale,
            vector.Y * scale,
            vector.Z * scale
        );
    }

    internal static void Multiply(Vector3 vector, Vector3 scale, out Vector3 result)
    {
        result = new Vector3
        (
            vector.X * scale.X,
            vector.Y * scale.Y,
            vector.Z * scale.Z
        );
    }


    internal static void Divide(Vector3 vector, double scale, out Vector3 result)
    {
        result = new Vector3
        (
            vector.X / scale,
            vector.Y / scale,
            vector.Z / scale
        );
    }

    internal static void Divide(Vector3 vector, Vector3 scale, out Vector3 result)
    {
        result = new Vector3
        (
            vector.X / scale.X,
            vector.Y / scale.Y,
            vector.Z / scale.Z
        );
    }
    
    internal static void ComponentMin(Vector3 a, Vector3 b, out Vector3 result)
    {
        if (a.X < b.X) result.X = a.X;
        else result.X = b.X;

        if (a.Y < b.Y) result.Y = a.Y;
        else result.Y = b.Y;

        if (a.Z < b.Z) result.Z = a.Z;
        else result.Z = b.Z;
    }

    internal static void ComponentMax(Vector3 a, Vector3 b, out Vector3 result)
    {
        if (a.X > b.X) result.X = a.X;
        else result.X = b.X;

        if (a.Y > b.Y) result.Y = a.Y;
        else result.Y = b.Y;

        if (a.Z > b.Z) result.Z = a.Z;
        else result.Z = b.Z;
    }

    internal static void MagnitudeMin(Vector3 left, Vector3 right, out Vector3 result)
    {
        if (left.LengthSquared < right.LengthSquared) result = left; 
        else result = right;
    }
    
    internal static void MagnitudeMax(Vector3 left, Vector3 right, out Vector3 result)
    {
        if (left.LengthSquared >= right.LengthSquared) result = left;
        else result = right;
    }

    internal static void Clamp(Vector3 vec, Vector3 min, Vector3 max, out Vector3 result)
    {
        if (vec.X < min.X) result.X = min.X;
        else if (vec.X > max.X) result.X = max.X;
        else result.X = vec.X;

        if (vec.Y < min.Y) result.Y = min.Y;
        else if (vec.Y > max.Y) result.Y = max.Y;
        else result.Y = vec.Y;

        if (vec.Z < min.Z) result.Z = min.Z;
        else if (vec.Z > max.Z) result.Z = max.Z;
        else result.Z = vec.Z;
    }

    internal static void Distance(Vector3 vec1, Vector3 vec2, out double result)
    {
        result = System.Math.Sqrt(((vec2.X - vec1.X) * (vec2.X - vec1.X)) 
                                + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y)) 
                                + ((vec2.Z - vec1.Z) * (vec2.Z - vec1.Z)));
    }

    internal static void DistanceSquared(Vector3 vec1, Vector3 vec2, out double result)
    {
        result = ((vec2.X - vec1.X) * (vec2.X - vec1.X))
               + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y)) 
               + ((vec2.Z - vec1.Z) * (vec2.Z - vec1.Z));
    }

    internal static void Normalize(Vector3 vec, out Vector3 result)
    {
        double scale = 1 / vec.Length;
        result = new Vector3
        (
            vec.X * scale,
            vec.Y * scale,
            vec.Z * scale
        );
    }

    
    internal static void NormalizeFast(Vector3 vec, out Vector3 result)
    {
        double scale = 1 / vec.LengthFast;
        result = new Vector3
        (
            vec.X * scale,
            vec.Y * scale,
            vec.Z * scale
        );
    }

    
    internal static void Dot(Vector3 left, Vector3 right, out double result)
    {
        result = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
    }

    internal static void Cross(Vector3 left, Vector3 right, out Vector3 result)
    {
        result = new Vector3
        (
            (left.Y * right.Z) - (left.Z * right.Y),
            (left.Z * right.X) - (left.X * right.Z),
            (left.X * right.Y) - (left.Y * right.X)
        );
    }

    internal static void Lerp(Vector3 a, Vector3 b, double blend, out Vector3 result)
    {
        result = new Vector3
        (
            (blend * (b.X - a.X)) + a.X,
            (blend * (b.Y - a.Y)) + a.Y,
            (blend * (b.Z - a.Z)) + a.Z
        );
    }

    internal static void Lerp(Vector3 a, Vector3 b, Vector3 blend, out Vector3 result)
    {
        result = new Vector3
        (
            (blend.X * (b.X - a.X)) + a.X,
            (blend.Y * (b.Y - a.Y)) + a.Y,
            (blend.Z * (b.Z - a.Z)) + a.Z
        );
    }

    internal static void BaryCentric(Vector3 a, Vector3 b, Vector3 c, double u, double v, out Vector3 result)
    {
        Subtract(b, a, out Vector3 ab);
        Multiply(ab, u, out Vector3 abU);
        Add(a, abU, out Vector3 uPos);

        Subtract(c, a, out Vector3 ac);
        Multiply(ac, v, out Vector3 acV);
        Add(uPos, acV, out result);
    }

    internal static void TransformVector(Vector3 vec, Matrix3 mat, out Vector3 result)
    {
        result = new Vector3
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21) + (vec.Z * mat.M31),
            (vec.X * mat.M12) + (vec.Y * mat.M22) + (vec.Z * mat.M32),
            (vec.X * mat.M13) + (vec.Y * mat.M23) + (vec.Z * mat.M33)
        );
    }

    internal static void TransformNormal(Vector3 norm, Matrix3 mat, out Vector3 result)
    {
        Matrix3 invMat = Matrix3.Invert(mat);
        result = new Vector3
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12) + (norm.Z * invMat.M13),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22) + (norm.Z * invMat.M23),
            (norm.X * invMat.M31) + (norm.Y * invMat.M32) + (norm.Z * invMat.M33)
        );
    }

    internal static void TransformNormalInverse(Vector3 norm, Matrix3 invMat, out Vector3 result)
    {
        result = new Vector3
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12) + (norm.Z * invMat.M13),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22) + (norm.Z * invMat.M23),
            (norm.X * invMat.M31) + (norm.Y * invMat.M32) + (norm.Z * invMat.M33)
        );
    }

    internal static void TransformPosition(Vector3 pos, Matrix3 mat, out Vector3 result)
    {
        result = new Vector3
        (
            (pos.X * mat.M11) + (pos.Y * mat.M21) + (pos.Z * mat.M31) + mat.M31,
            (pos.X * mat.M12) + (pos.Y * mat.M22) + (pos.Z * mat.M32) + mat.M32,
            (pos.X * mat.M13) + (pos.Y * mat.M23) + (pos.Z * mat.M33) + mat.M33
        );
    }

    internal static void TransformRow(Vector3 vec, Matrix3 mat, out Vector3 result)
    {
        result = new Vector3
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21) + (vec.Z * mat.M31),
            (vec.X * mat.M12) + (vec.Y * mat.M22) + (vec.Z * mat.M32),
            (vec.X * mat.M13) + (vec.Y * mat.M23) + (vec.Z * mat.M33)
        );
    }

    internal static void Transform(Vector3 vec, Quaternion quat, out Vector3 result)
    {
        var v = new Quaternion(vec.X, vec.Y, vec.Z, 0);

        Data.Est.Invert(quat, out Quaternion i);
        Data.Est.Multiply(quat, v, out Quaternion t);
        Data.Est.Multiply(t, i, out v);

        result = new Vector3(v.X, v.Y, v.Z);
    }

    internal static void TransformColumn(Matrix3 mat, Vector3 vec, out Vector3 result)
    {
        result = new Vector3
        (
            (mat.M11 * vec.X) + (mat.M12 * vec.Y) + (mat.M13 * vec.Z),
            (mat.M21 * vec.X) + (mat.M22 * vec.Y) + (mat.M23 * vec.Z),
            (mat.M31 * vec.X) + (mat.M32 * vec.Y) + (mat.M33 * vec.Z)
        );
    }

    internal static void TransformPerspective(Vector3 vec, Matrix4 mat, out Vector3 result)
    {
        var v = new Vector4(vec.X, vec.Y, vec.Z, 1);
        TransformRow(v, mat, out v);
        result = new Vector3
        (
            v.X / v.W,
            v.Y / v.W,
            v.Z / v.W
        );
    }

    internal static void CalculateAngle(Vector3 first, Vector3 second, out double result)
    {
        double temp = (first.X * second.X) + (first.Y * second.Y) + (first.Z * second.Z);

        result = System.Math.Acos(MathHelper.Clamp(temp / (first.Length * second.Length), -1, 1));
    }

} 