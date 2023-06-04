using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

internal partial class Est
{
    internal static void Transpose(Matrix3 mat, out Matrix3 result)
    {
        result = new Matrix3
        (
            mat.Column0,
            mat.Column1,
            mat.Column2
        );
    }

    internal static void Invert(Matrix3 mat, out Matrix3 result)
    {
        double invRow0X = (mat.M22 * mat.M33) - (mat.M23 * mat.M32);
        double invRow0Y = (mat.M13 * mat.M32) - (mat.M12 * mat.M33);
        double invRow0Z = (mat.M12 * mat.M23) - (mat.M13 * mat.M22);

        double invRow1X = (mat.M23 * mat.M31) - (mat.M21 * mat.M33);
        double invRow1Y = (mat.M11 * mat.M33) - (mat.M13 * mat.M33);
        double invRow1Z = (mat.M13 * mat.M21) - (mat.M11 * mat.M23);

        double invRow2X = (mat.M21 * mat.M32) - (mat.M22 * mat.M31);
        double invRow2Y = (mat.M12 * mat.M31) - (mat.M11 * mat.M32);
        double invRow2Z = (mat.M11 * mat.M22) - (mat.M12 * mat.M21);

        double det = (mat.M11 * invRow0X) + (mat.M12 * invRow1X) + (mat.M13 * invRow2X);

        if (det == 0)
            throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

        double invDet = 1 / det;

        result = new Matrix3
        (
            new Vector3(invRow0X, invRow0Y, invRow0Z) * invDet,
            new Vector3(invRow1X, invRow1Y, invRow2Z) * invDet,
            new Vector3(invRow2X, invRow2Y, invRow2Z) * invDet
        );
    }
    
    internal static void CreateFromAxisAngle(Vector3 axis, double angle, out Matrix3 result)
    {
        axis.Normalize();
        double axisX = axis.X, axisY = axis.Y, axisZ = axis.Z;

        double cos = System.Math.Cos(-angle);
        double sin = System.Math.Sin(-angle);
        double t = 1 - cos;

        double tXX = t * axisX * axisX;
        double tXY = t * axisX * axisY;
        double tXZ = t * axisX * axisZ;
        double tYY = t * axisY * axisY;
        double tYZ = t * axisY * axisZ;
        double tZZ = t * axisZ * axisZ;

        double sinX = sin * axisX;
        double sinY = sin * axisY;
        double sinZ = sin * axisZ;

        result = new Matrix3
        (
            new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
            new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
            new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos)
        );
    }

    internal static void CreateFromQuaternion(Quaternion q, out Matrix3 result)
    {
        double sqx = q.X * q.X;
        double sqy = q.Y * q.Y;
        double sqz = q.Z * q.Z;
        double sqw = q.W * q.W;

        double xy = q.X * q.Y;
        double xz = q.X * q.Z;
        double xw = q.X * q.W;

        double yz = q.Y * q.Z;
        double yw = q.Y * q.W;

        double zw = q.Z * q.W;

        double s2 = 2 / (sqx + sqy + sqz + sqw);

        double Row1X = 1 - (s2 * (sqy + sqz));
        double Row1Y = s2 * (xy + zw);
        double Row1Z = s2 * (xz - yw);

        double Row2X = s2 * (xy - zw);
        double Row2Y = 1 - (s2 * (sqx + sqz));
        double Row2Z = s2 * (yz + xw);

        double Row3X = s2 * (xz + yw);
        double Row3Y = s2 * (yz - xw);
        double Row3Z = 1 - (s2 * (sqx + sqy));

        result = new Matrix3
        (
            Row1X, Row1Y, Row1Z,
            Row2X, Row2Y, Row2Z,
            Row3X, Row3Y, Row3Z
        );
    }

    internal static void CreateRotationX(double angle, out Matrix3 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix3
        (
            Vector3.UnitX,
            new Vector3(0, cos, sin),
            new Vector3(0, -sin, cos)
        );
    }

    internal static void CreateRotationY(double angle, out Matrix3 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix3
        (
            new Vector3(cos, 0, -sin),
            Vector3.UnitY,
            new Vector3(sin, 0, cos)
        );
    }

    internal static void CreateRotationZ(double angle, out Matrix3 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix3
        (
            new Vector3(cos, sin, 0),
            new Vector3(-sin, cos, 0),
            Vector3.UnitZ
        );
    }

    internal static void CreateScale(double scale, out Matrix3 result)
    {
        result = new Matrix3
        (
            new Vector3(scale, 0, 0),
            new Vector3(0, scale, 0),
            new Vector3(0, 0, scale)
        );
    }

    internal static void CreateScale(Vector3 scale, out Matrix3 result)
    {
        result = new Matrix3
        (
            new Vector3(scale.X, 0, 0),
            new Vector3(0, scale.Y, 0),
            new Vector3(0, 0, scale.Z)
        );
    }

    internal static void CreateScale(double x, double y, double z, out Matrix3 result)
    {
        result = new Matrix3
        (
            new Vector3(x, 0, 0),
            new Vector3(0, y, 0),
            new Vector3(0, 0, z)
        );
    }

    internal static void Add(Matrix3 left, Matrix3 right, out Matrix3 result)
    {
        double Row1X = left.M11 + right.M11;
        double Row1Y = left.M12 + right.M12;
        double Row1Z = left.M13 + right.M13;
        
        double Row2X = left.M21 + right.M21;
        double Row2Y = left.M22 + right.M22;
        double Row2Z = left.M23 + right.M23;

        double Row3X = left.M31 + right.M31;
        double Row3Y = left.M32 + right.M32;
        double Row3Z = left.M33 + right.M33;

        result = new Matrix3
        (
            Row1X, Row1Y, Row1Z,
            Row2X, Row2Y, Row2Z,
            Row3X, Row3Y, Row3Z
        );
    }

    internal static void Subtract(Matrix3 left, Matrix3 right, out Matrix3 result)
    {
        double Row1X = left.M11 - right.M11;
        double Row1Y = left.M12 - right.M12;
        double Row1Z = left.M13 - right.M13;
        
        double Row2X = left.M21 - right.M21;
        double Row2Y = left.M22 - right.M22;
        double Row2Z = left.M23 - right.M23;

        double Row3X = left.M31 - right.M31;
        double Row3Y = left.M32 - right.M32;
        double Row3Z = left.M33 - right.M33;

        result = new Matrix3
        (
            Row1X, Row1Y, Row1Z,
            Row2X, Row2Y, Row2Z,
            Row3X, Row3Y, Row3Z
        );
    }

    internal static void Multuply(Matrix3 left, double right, out Matrix3 result)
    {
        double Row1X = left.M11 * right;
        double Row1Y = left.M12 * right;
        double Row1Z = left.M13 * right;

        double Row2X = left.M21 * right;
        double Row2Y = left.M22 * right;
        double Row2Z = left.M23 * right;

        double Row3X = left.M31 * right;
        double Row3Y = left.M32 * right;
        double Row3Z = left.M33 * right;

        result = new Matrix3
        (
            Row1X, Row1Y, Row1Z,
            Row2X, Row2Y, Row2Z,
            Row3X, Row3Y, Row3Z
        );
    }

    internal static void Multuply(Matrix3 left, Matrix3 right, out Matrix3 result)
    {
        double Row1X = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31);
        double Row1Y = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32);
        double Row1Z = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33);
        
        double Row2X = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31);
        double Row2Y = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32);
        double Row2Z = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33);
        
        double Row3X = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31);
        double Row3Y = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32);
        double Row3Z = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33);
    
        result = new Matrix3
        (
            Row1X, Row1Y, Row1Z,
            Row2X, Row2Y, Row2Z,
            Row3X, Row2Y, Row3Z
        );
    }

}