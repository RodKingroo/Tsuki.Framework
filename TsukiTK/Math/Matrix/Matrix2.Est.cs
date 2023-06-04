using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

internal partial class Est
{
    internal static void Transpose(Matrix2 mat, out Matrix2 result)
    {
        result = new Matrix2
        (
            mat.Column0,
            mat.Column1
        );
    }

    internal static void Invert(Matrix2 mat, out Matrix2 result)
    {
        double det = (mat.M11 * mat.M22) - (mat.M12 * mat.M21);

        if (det == 0)
            throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

        double invDet = 1 / det;

        result = new Matrix2
        (
            new Vector2(mat.M22, mat.M12) * invDet,
            new Vector2(mat.M21, mat.M11) * invDet
        );
    }

    internal static void CreateRotation(double angle, out Matrix2 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix2
        (
            new Vector2(cos, sin),
            new Vector2(-sin, cos)
        );
    }

    internal static void CreateScale(double scale, out Matrix2 result)
    {
        result = new Matrix2
        (
            new Vector2(scale, 0),
            new Vector2(0, scale)
        );
    }

    internal static void CreateScale(Vector2 scale, out Matrix2 result)
    {
        result = new Matrix2
        (
            new Vector2(scale.X, 0),
            new Vector2(0, scale.Y)
        );
    }

    internal static void CreateScale(double x, double y, out Matrix2 result)
    {
        result = new Matrix2
        (
            new Vector2(x, 0),
            new Vector2(0, y)
        );
    }

    internal static void Add(Matrix2 left, Matrix2 right, out Matrix2 result)
    {
        double Row1X = left.M11 + right.M11;
        double Row1Y = left.M12 + right.M12;
        
        double Row2X = left.M21 + right.M21;
        double Row2Y = left.M22 + right.M22;

        result = new Matrix2
        (
            Row1X, Row1Y,
            Row2X, Row2Y
        );
    }

    internal static void Subtract(Matrix2 left, Matrix2 right, out Matrix2 result)
    {
        double Row1X = left.M11 - right.M11;
        double Row1Y = left.M12 - right.M12;
        
        double Row2X = left.M21 - right.M21;
        double Row2Y = left.M22 - right.M22;

        result = new Matrix2
        (
            Row1X, Row1Y,
            Row2X, Row2Y
        );
    }

    internal static void Multuply(Matrix2 left, double right, out Matrix2 result)
    {
        double Row1X = left.M11 * right;
        double Row1Y = left.M12 * right;
        
        double Row2X = left.M21 * right;
        double Row2Y = left.M22 * right;

        result = new Matrix2
        (
            Row1X, Row1Y,
            Row2X, Row2Y
        );
    }

    internal static void Multuply(Matrix2 left, Matrix2 right, out Matrix2 result)
    {
        double Row1X = (left.M11 * right.M11) + (left.M12 * right.M21);
        double Row1Y = (left.M11 * right.M12) + (left.M12 * right.M22);
        
        double Row2X = (left.M21 * right.M11) + (left.M22 * right.M21);
        double Row2Y = (left.M21 * right.M12) + (left.M22 * right.M22);

        result = new Matrix2
        (
            Row1X, Row1Y,
            Row2X, Row2Y
        );
    }
}