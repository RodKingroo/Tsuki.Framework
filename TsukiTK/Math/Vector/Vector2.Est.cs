using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Matrix;

namespace Tsuki.Framework.Math.Vector;

internal partial class Est
{
    internal static void Add(Vector2 a, Vector2 b, out Vector2 result)
    {
        result = new Vector2(a.X + b.X, a.Y + b.Y);
    }

    internal static void Subtract(Vector2 a, Vector2 b, out Vector2 result)
    {
        result = new Vector2(a.X - b.X, a.Y - b.Y);
    }

    internal static void Negative(Vector2 a, out Vector2 result)
    {
         result = new Vector2(-a.X, -a.Y);
    }

    internal static void Multiply(Vector2 vector, double scale, out Vector2 result)
    {
        result = new Vector2(vector.X * scale, vector.Y * scale);
    }

    internal static void Multiply(Vector2 vector, Vector2 scale, out Vector2 result)
    {
        result = new Vector2(vector.X * scale.X, vector.Y * scale.Y);
    }

    internal static void Divide(Vector2 vector, double scale, out Vector2 result)
    {
        result = new Vector2(vector.X / scale, vector.Y / scale);
    }

    internal static void Divide(Vector2 vector, Vector2 scale, out Vector2 result)
    {
        result = new Vector2(vector.X / scale.X, vector.Y / scale.Y);
    }
    
    internal static void ComponentMin(Vector2 a, Vector2 b, out Vector2 result)
    {
        if (a.X < b.X) result.X = a.X; 
        else result.X = b.X; 

        if (a.Y < b.Y) result.Y = a.Y; 
        else result.Y = b.Y;
    }

    internal static void ComponentMax(Vector2 a, Vector2 b, out Vector2 result)
    {
        if (a.X > b.X) result.X = a.X; 
        else result.X = b.X; 

        if (a.Y > b.Y) result.Y = a.Y; 
        else result.Y = b.Y;
    }

    internal static void MagnitudeMin(Vector2 left, Vector2 right, out Vector2 result)
    {
        if (left.LengthSquared < right.LengthSquared) result = left;
        else result = right;
    }
    
    internal static void MagnitudeMax(Vector2 left, Vector2 right, out Vector2 result)
    {
        if (left.LengthSquared >= right.LengthSquared) result = left; 
        else result = right; 
    }

    internal static void Clamp(Vector2 vec, Vector2 min, Vector2 max, out Vector2 result)
    {
        if (vec.X < min.X) result.X = min.X; 
        else if (vec.X > max.X) result.X = max.X; 
        else result.X = vec.X; 

        if (vec.Y < min.Y) result.Y = min.Y; 
        else if (vec.Y > max.Y) result.Y = max.Y; 
        else result.Y = vec.Y;
    }

    internal static void Distance(Vector2 vec1, Vector2 vec2, out double result)
    {
        result = System.Math.Sqrt(((vec2.X - vec1.X) * (vec2.X - vec1.X)) 
                                + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y)));
    }

    internal static void DistanceSquared(Vector2 vec1, Vector2 vec2, out double result)
    {
        result = ((vec2.X - vec1.X) * (vec2.X - vec1.X)) + ((vec2.Y - vec1.Y) * (vec2.Y - vec1.Y));
    }

    internal static void Normalize(Vector2 vec, out Vector2 result)
    {
        double scale = 1 / vec.Length;
        result = new Vector2(vec.X * scale, vec.Y * scale);
    }

    
    internal static void NormalizeFast(Vector2 vec, out Vector2 result)
    {
        double scale = 1 / vec.LengthFast;
        result = new Vector2(vec.X * scale, vec.Y * scale);
    }

    
    internal static void Dot(Vector2 left, Vector2 right, out double result)
    {
        result = (left.X * right.X) + (left.Y * right.Y);
    }

    
    internal static void Cross(Vector2 left, Vector2 right, out double result)
    {
        result = (left.X * right.Y) - (left.Y * right.X);
    }

    internal static void Lerp(Vector2 a, Vector2 b, double blend, out Vector2 result)
    {
        result = new Vector2
        (
            (blend * (b.X - a.X)) + a.X,
            (blend * (b.X - a.X)) + a.X
        );
    }

    internal static void Lerp(Vector2 a, Vector2 b, Vector2 blend, out Vector2 result)
    {
        result = new Vector2
        (
            (blend.X * (b.X - a.X)) + a.X,
            (blend.Y * (b.Y - a.Y)) + a.Y
        );
    }

    internal static void BaryCentric(Vector2 a, Vector2 b, Vector2 c, double u, double v, out Vector2 result)
    { 
        Subtract(b, a, out Vector2 ab);
        Multiply(ab, u, out Vector2 abU);
        Add(a, abU, out Vector2 uPos);

        Subtract(c, a, out Vector2 ac);
        Multiply(ac, v, out Vector2 acV);
        Add(uPos, acV, out result);
    }

    internal static void TransformVector(Vector2 vec, Matrix2 mat, out Vector2 result)
    {
        result = new Vector2
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21),
            (vec.X * mat.M12) + (vec.Y * mat.M22)
        );
    }

    internal static void TransformNormal(Vector2 norm, Matrix2 mat, out Vector2 result)
    {
        Matrix2 invMat = Matrix2.Invert(mat);
        result = new Vector2
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22)
        );
    }

    internal static void TransformNormalInverse(Vector2 norm, Matrix2 invMat, out Vector2 result)
    {
        result = new Vector2
        (
            (norm.X * invMat.M11) + (norm.Y * invMat.M12),
            (norm.X * invMat.M21) + (norm.Y * invMat.M22)
        );
    }

    internal static void TransformPosition(Vector2 pos, Matrix2 mat, out Vector2 result)
    {
        result = new Vector2
        (
            (pos.X * mat.M11) + (pos.Y * mat.M21) + mat.M21,
            (pos.X * mat.M12) + (pos.Y * mat.M22) + mat.M22
        );
    }

    internal static void TransformRow(Vector2 vec, Matrix2 mat, out Vector2 result)
    {
        result = new Vector2
        (
            (vec.X * mat.M11) + (vec.Y * mat.M21),
            (vec.X * mat.M12) + (vec.Y * mat.M22)
        );
    }

    internal static void Transform(Vector2 vec, Quaternion quat, out Vector2 result)
    {
        var v = new Quaternion(vec.X, vec.Y, 0, 0);

        Data.Est.Invert(quat, out Quaternion i);
        Data.Est.Multiply(quat, v, out Quaternion t);
        Data.Est.Multiply(t, i, out v);

        result = new Vector2(v.X, v.Y);
    }

    internal static void TransformColumn(Matrix2 mat, Vector2 vec, out Vector2 result)
    {
        result = new Vector2
        (
            (mat.M11 * vec.X) + (mat.M12 * vec.Y),
            (mat.M21 * vec.X) + (mat.M22 * vec.Y)
        );
    }

    internal static void TransformPerspective(Vector2 vec, Matrix3 mat, out Vector2 result)
    {
        var v = new Vector3(vec.X, vec.Y, 1);
        TransformRow(v, mat, out v);
        result = new Vector2
        (
            v.X / v.Z,
            v.Y / v.Z
        );
    }

    internal static void CalculateAngle(Vector2 first, Vector2 second, out double result)
    {
        double temp = (first.X * second.X) + (first.Y * second.Y);

        result = System.Math.Acos(MathHelper.Clamp(temp / (first.Length * second.Length), -1, 1));
    }

} 