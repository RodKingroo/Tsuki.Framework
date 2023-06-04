using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

internal partial class Est
{
    internal static void Transpose(Matrix4 mat, out Matrix4 result)
    {
        result = new Matrix4
        (
            mat.Column0,
            mat.Column1,
            mat.Column2,
            mat.Column3
        );
    }

    internal static void Invert(Matrix4 mat, out Matrix4 result)
    {
        double f01 = (mat.M33 * mat.M44) - (mat.M34 * mat.M43);
        double f02 = (mat.M34 * mat.M42) - (mat.M32 * mat.M44);
        double f03 = (mat.M32 * mat.M43) - (mat.M33 * mat.M42);
        double f04 = (mat.M34 * mat.M43) - (mat.M33 * mat.M44);
        double f05 = (mat.M31 * mat.M44) - (mat.M34 * mat.M41);
        double f06 = (mat.M33 * mat.M41) - (mat.M31 * mat.M43);
        double f07 = (mat.M32 * mat.M44) - (mat.M34 * mat.M42);
        double f08 = (mat.M34 * mat.M41) - (mat.M31 * mat.M44);
        double f09 = (mat.M31 * mat.M42) - (mat.M32 * mat.M41);
        double f10 = (mat.M33 * mat.M42) - (mat.M32 * mat.M43);
        double f11 = (mat.M31 * mat.M43) - (mat.M33 * mat.M41);
        double f12 = (mat.M32 * mat.M41) - (mat.M31 * mat.M42);
        double f13 = (mat.M13 * mat.M24) - (mat.M14 * mat.M23);
        double f14 = (mat.M14 * mat.M22) - (mat.M12 * mat.M24);
        double f15 = (mat.M12 * mat.M23) - (mat.M13 * mat.M22);
        double f16 = (mat.M14 * mat.M23) - (mat.M13 * mat.M24);
        double f17 = (mat.M11 * mat.M24) - (mat.M14 * mat.M21);
        double f18 = (mat.M13 * mat.M21) - (mat.M11 * mat.M23);
        double f19 = (mat.M12 * mat.M24) - (mat.M14 * mat.M22);
        double f20 = (mat.M14 * mat.M21) - (mat.M11 * mat.M24);
        double f21 = (mat.M11 * mat.M22) - (mat.M12 * mat.M21);
        double f22 = (mat.M13 * mat.M22) - (mat.M12 * mat.M23);
        double f23 = (mat.M11 * mat.M23) - (mat.M13 * mat.M21);
        double f24 = (mat.M12 * mat.M21) - (mat.M11 * mat.M22);

        double invM11 = (mat.M22 * f01) - (mat.M23 * f02) + (mat.M24 * f03);
        double invM12 = (mat.M23 * f05) - (mat.M21 * f04) - (mat.M24 * f06);
        double invM13 = (mat.M21 * f07) - (mat.M22 * f08) + (mat.M24 * f09);
        double invM14 = (mat.M22 * f11) - (mat.M21 * f10) - (mat.M23 * f12);

        double invM21 = (mat.M13 * f07) - (mat.M12 * f04) - (mat.M14 * f10);
        double invM22 = (mat.M11 * f01) - (mat.M13 * f08) + (mat.M14 * f11);
        double invM23 = (mat.M12 * f05) - (mat.M11 * f02) - (mat.M14 * f12);
        double invM24 = (mat.M11 * f03) - (mat.M12 * f06) + (mat.M13 * f09);

        double invM31 = (mat.M42 * f13) - (mat.M43 * f14) + (mat.M44 * f15);
        double invM32 = (mat.M43 * f17) - (mat.M41 * f16) - (mat.M44 * f18);
        double invM33 = (mat.M41 * f19) - (mat.M42 * f20) + (mat.M44 * f21);
        double invM34 = (mat.M42 * f23) - (mat.M41 * f22) - (mat.M43 * f24);

        double invM41 = (mat.M33 * f19) - (mat.M32 * f16) - (mat.M34 * f22);
        double invM42 = (mat.M31 * f13) - (mat.M33 * f20) + (mat.M34 * f23);
        double invM43 = (mat.M32 * f17) - (mat.M31 * f14) - (mat.M34  * f24);
        double invM44 = (mat.M31 * f15) - (mat.M32 * f18) + (mat.M33 * f21);

        double det = (mat.M11 * invM11) + (mat.M12 * invM12) + (mat.M13 * invM13) + (mat.M14 * invM14);
        
        if (det == 0)
            throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

        double invDet = 1 / det;

        result = new Matrix4
        (
            new Vector4(invM11, invM12, invM13, invM14) * invDet,
            new Vector4(invM21, invM22, invM23, invM24) * invDet,
            new Vector4(invM31, invM32, invM33, invM34) * invDet,
            new Vector4(invM41, invM42, invM43, invM44) * invDet
        );
    }

    internal static void CreateFromAxisAngle(Vector3 axis, double angle, out Matrix4 result)
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

        result = new Matrix4
        (
            new Vector4(tXX + cos, tXY - sinZ, tXZ + sinY, 0),
            new Vector4(tXY + sinZ, tYY + cos, tYZ - sinX, 0),
            new Vector4(tXZ - sinY, tYZ + sinX, tZZ + cos, 0),
            Vector4.UnitW
        );
    }

    internal static void CreateFromQuaternion(Quaternion q, out Matrix4 result)
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

        result = new Matrix4
        (
            new Vector4(Row1X, Row1Y, Row1Z, 0),
            new Vector4(Row2X, Row2Y, Row2Z, 0),
            new Vector4(Row3X, Row3Y, Row3Z, 0),
            Vector4.UnitW
        );
    }

    internal static void CreateRotationX(double angle, out Matrix4 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix4
        (
            Vector4.UnitX,
            new Vector4(0, cos, sin, 0),
            new Vector4(0, -sin, cos, 0),
            Vector4.UnitW
        );
    }

    
    internal static void CreateRotationY(double angle, out Matrix4 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix4
        (
            new Vector4(cos, 0, -sin, 0),
            Vector4.UnitY,
            new Vector4(sin, 0, cos, 0),
            Vector4.UnitW
        );
    }

    internal static void CreateRotationZ(double angle, out Matrix4 result)
    {
        double cos = System.Math.Cos(angle);
        double sin = System.Math.Sin(angle);

        result = new Matrix4
        (
            new Vector4(cos, sin, 0, 0),
            new Vector4(-sin, cos, 0, 0),
            Vector4.UnitZ,
            Vector4.UnitW
        );
    }

    internal static void CreateScale(double scale, out Matrix4 result)
    {
        result = new Matrix4
        (
            new Vector4(scale, 0, 0, 0),
            new Vector4(0, scale, 0, 0),
            new Vector4(0, 0, scale, 0),
            Vector4.UnitW
        );
    }

    internal static void CreateScale(Vector4 scale, out Matrix4 result)
    {
        result = new Matrix4
        (
            new Vector4(scale.X, 0, 0, 0),
            new Vector4(0, scale.Y, 0, 0),
            new Vector4(0, 0, scale.Z, 0),
            Vector4.UnitW
        );
    }

    internal static void CreateScale(double x, double y, double z,
                                     out Matrix4 result)
    {
        result = new Matrix4
        (
            new Vector4(x, 0, 0, 0),
            new Vector4(0, y, 0, 0),
            new Vector4(0, 0, z, 0),
            Vector4.UnitW
        );
    }

    internal static void Add(Matrix4 left, Matrix4 right, out Matrix4 result)
    {
        double Row1X = left.M11 + right.M11;
        double Row1Y = left.M12 + right.M12;
        double Row1Z = left.M13 + right.M13;
        double Row1W = left.M14 + right.M14;
        
        double Row2X = left.M21 + right.M21;
        double Row2Y = left.M22 + right.M22;
        double Row2Z = left.M23 + right.M23;
        double Row2W = left.M24 + right.M24;

        double Row3X = left.M31 + right.M31;
        double Row3Y = left.M32 + right.M32;
        double Row3Z = left.M33 + right.M33;
        double Row3W = left.M34 + right.M34;

        double Row4X = left.M41 + right.M41;
        double Row4Y = left.M42 + right.M42;
        double Row4Z = left.M43 + right.M43;
        double Row4W = left.M44 + right.M44;

        result = new Matrix4
        (
            Row1X, Row1Y, Row1Z, Row1W,
            Row2X, Row2Y, Row2Z, Row2W,
            Row3X, Row3Y, Row3Z, Row3W,
            Row4X, Row4Y, Row4Z, Row4W
        );
    }

    internal static void Subtract(Matrix4 left, Matrix4 right, out Matrix4 result)
    {
        double Row1X = left.M11 - right.M11;
        double Row1Y = left.M12 - right.M12;
        double Row1Z = left.M13 - right.M13;
        double Row1W = left.M14 - right.M14;
        
        double Row2X = left.M21 - right.M21;
        double Row2Y = left.M22 - right.M22;
        double Row2Z = left.M23 - right.M23;
        double Row2W = left.M24 - right.M24;

        double Row3X = left.M31 - right.M31;
        double Row3Y = left.M32 - right.M32;
        double Row3Z = left.M33 - right.M33;
        double Row3W = left.M34 - right.M34;

        double Row4X = left.M41 - right.M41;
        double Row4Y = left.M42 - right.M42;
        double Row4Z = left.M43 - right.M43;
        double Row4W = left.M44 - right.M44;

        result = new Matrix4
        (
            Row1X, Row1Y, Row1Z, Row1W,
            Row2X, Row2Y, Row2Z, Row2W,
            Row3X, Row3Y, Row3Z, Row3W,
            Row4X, Row4Y, Row4Z, Row4W
        );
    }

    internal static void Multuply(Matrix4 left, Matrix4 right, out Matrix4 result)
    {
        double Row1X = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
        double Row1Y = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
        double Row1Z = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
        double Row1W = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
        
        double Row2X = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
        double Row2Y = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
        double Row2Z = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
        double Row2W = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
        
        double Row3X = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
        double Row3Y = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
        double Row3Z = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
        double Row3W = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
        
        double Row4X = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
        double Row4Y = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
        double Row4Z = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
        double Row4W = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
    
        result = new Matrix4
        (
            Row1X, Row1Y, Row1Z, Row1W,
            Row2X, Row2Y, Row2Z, Row2W,
            Row3X, Row3Y, Row3Z, Row3W,
            Row4X, Row4Y, Row4Z, Row4W
        );
    }

    internal static void Multuply(Matrix4 left, double right, out Matrix4 result)
    {
        double Row1X = left.M11 * right;
        double Row1Y = left.M12 * right;
        double Row1Z = left.M13 * right;
        double Row1W = left.M14 * right;

        double Row2X = left.M21 * right;
        double Row2Y = left.M22 * right;
        double Row2Z = left.M23 * right;
        double Row2W = left.M24 * right;

        double Row3X = left.M31 * right;
        double Row3Y = left.M32 * right;
        double Row3Z = left.M33 * right;
        double Row3W = left.M34 * right;

        double Row4X = left.M41 * right;
        double Row4Y = left.M42 * right;
        double Row4Z = left.M43 * right;
        double Row4W = left.M44 * right;

        result = new Matrix4
        (
            Row1X, Row1Y, Row1Z, Row1W,
            Row2X, Row2Y, Row2Z, Row2W,
            Row3X, Row3Y, Row3Z, Row3W,
            Row4X, Row4Y, Row4Z, Row4W
        );
    }
}