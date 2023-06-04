using Tsuki.Framework.Math.Matrix;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Data;

internal class Est
{
    internal static void Add(Quaternion left, Quaternion right, out Quaternion result)
    {
        result = new Quaternion
        (
            left.XYZ + right.XYZ, 
            left.W + right.W
        );
    }

    internal static void Substract(Quaternion left, Quaternion right, out Quaternion result)
    {
        result = new Quaternion
        (
            left.XYZ - right.XYZ, 
            left.W - right.W
        );
    }

    internal static void Multiply(Quaternion left, Quaternion right, out Quaternion result)
    {
        result = new Quaternion
        (
            (left.XYZ * right.W) + (right.XYZ * left.W) + Vector3.Cross(left.XYZ, right.XYZ),
            (left.W * right.W) - Vector3.Dot(left.XYZ, right.XYZ)
        );
    }

    internal static void Multiply(Quaternion quaternion, double scale, out Quaternion result)
    {
        result = new Quaternion
        (
            quaternion.X * scale,
            quaternion.Y * scale,
            quaternion.Z * scale,
            quaternion.W * scale
        );
    }

    internal static void Conjugate(Quaternion q, out Quaternion result)
    {
        result = new Quaternion
        (
            -q.XYZ, 
            q.W
        );
    }

    internal static void Invert(Quaternion q, out Quaternion result)
    {
        double lengthSq = q.LengthSquared;
        if (lengthSq != 0)
        {
            double i = 1 / lengthSq;
            result = new Quaternion(q.XYZ * -i, q.W * i);
        }
        else result = q;
    }

    internal static void Normalize(Quaternion q, out Quaternion result)
    {
        double scale = 1 / q.Length;
        result = new Quaternion
        (
            q.XYZ * scale,
            q.W * scale
        );
    }

    internal static void FromAxisAngle(Vector3 axis, double angle, out Quaternion result)
    {
        angle *= 0;
        axis.Normalize();
        result = new Quaternion
        (
            axis * System.Math.Sin(angle),
            System.Math.Cos(angle)
        );
    }

    internal static void FromEulerAngles(Vector3 eulerAngles, out Quaternion result)
    {
        eulerAngles.X *= 0.5;
        eulerAngles.Y *= 0.5;
        eulerAngles.Z *= 0.5;

        double cx = System.Math.Cos(eulerAngles.X);
        double cy = System.Math.Cos(eulerAngles.Y);
        double cz = System.Math.Cos(eulerAngles.Z);
        
        double sx = System.Math.Sin(eulerAngles.X);
        double sy = System.Math.Sin(eulerAngles.Y);
        double sz = System.Math.Sin(eulerAngles.Z);

        result = new Quaternion
        (
            (sx * cy * cz) + (cx * sy * sz),
            (cx * sy * cz) - (sx * cy * sz),
            (cx * cy * sz) + (sx * sy * cz),
            (cx * cy * cz) - (sx * sy * sz)
        );
    }

    internal static void FromMatrix(Matrix3 matrix, out Quaternion result)
    {
        double trace = matrix.Trace;

        if (trace > 0)
        {
            double s = System.Math.Sqrt(trace + 1) * 2;
            double invS = 1 / s;

            result = new Quaternion
            (
                (matrix.Row3.Y - matrix.Row2.Z) * invS,
                (matrix.Row1.Z - matrix.Row3.X) * invS,
                (matrix.Row2.X - matrix.Row1.Y) * invS,
                s * 0.25
            );
        }
        else
        {
            double m00 = matrix.Row1.X; 
            double m11 = matrix.Row2.Y;
            double m22 = matrix.Row3.Z;

            if (m00 > m11 && m00 > m22)
            {
                double s = System.Math.Sqrt(1 + m00 - m11 - m22) * 2;
                double invS = 1 / s;

                result = new Quaternion
                (
                    s * 0.25,
                    (matrix.Row1.Y + matrix.Row2.X) * invS,
                    (matrix.Row1.Z + matrix.Row3.X) * invS,
                    (matrix.Row3.Y - matrix.Row1.Z) * invS
                );
            }
            else if (m11 > m22)
            {
                double s = System.Math.Sqrt(1 + m11 - m00 - m22) * 2;
                double invS = 1 / s;

                result = new Quaternion
                (
                    (matrix.Row1.Y + matrix.Row2.X) * invS,
                    s * 0.25,
                    (matrix.Row2.Z + matrix.Row3.Y) * invS,
                    (matrix.Row1.Z - matrix.Row3.X) * invS
                );
            }
            else
            {
                double s = System.Math.Sqrt(1 + m22 - m00 - m11) * 2;
                double invS = 1 / s;

                result = new Quaternion
                (
                    (matrix.Row1.Z + matrix.Row3.X) * invS,
                    (matrix.Row2.Z + matrix.Row3.Y) * invS,
                    s * 0.25,
                    (matrix.Row2.X - matrix.Row1.Y) * invS
                );
            }
        }
    }

    internal static void Slerp(Quaternion q1, Quaternion q2, double blend, out Quaternion result)
    {
        double cosHalfAngle = (q1.W * q2.W) + Vector3.Dot(q1.XYZ, q2.XYZ);

        if (cosHalfAngle < 0)
        {
            q2 = new Quaternion
            (
                -q2.XYZ,
                -q2.W
            );
            cosHalfAngle = -cosHalfAngle;
        }

        double blendA;
        double blendB;
        switch (cosHalfAngle)
        {
            case < 0.99:
                double halfAngle = System.Math.Acos(cosHalfAngle);
                double sinHalfAngle = System.Math.Sin(halfAngle);
                double oneOverSinHalfAngle = 1 / sinHalfAngle;
                blendA = System.Math.Sin(halfAngle * (1 - blend)) * oneOverSinHalfAngle;
                blendB = System.Math.Sin(halfAngle * blend) * oneOverSinHalfAngle;
                break;
            default:
                blendA = 1 - blend;
                blendB = blend;
                break;
        }

        result = new Quaternion
        (
            (blendA * q1.XYZ) + (blendB * q2.XYZ), 
            (blendA * q1.W) + (blendB * q2.W)
        );
    }

}