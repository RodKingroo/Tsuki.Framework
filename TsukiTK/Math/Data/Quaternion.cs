using Tsuki.Framework.Math.Vector;
using Tsuki.Framework.Math.Matrix;

namespace Tsuki.Framework.Math.Data;

public struct Quaternion : IEquatable<Quaternion>
{
    public Vector3 XYZ;

    public double W;

    public Quaternion(Vector3 v, double w)
    {
        XYZ = v;
        W = w;
    }

    public Quaternion(double x, double y, double z, double w) : this(new Vector3(x, y, z), w)
    {

    }

    public Quaternion(double rotationX, double rotationY, double rotationZ)
    {
        rotationX *= 0.5;
        rotationY *= 0.5;
        rotationZ *= 0.5;

        double cx = System.Math.Cos(rotationX);
        double cy = System.Math.Cos(rotationY);
        double cz = System.Math.Cos(rotationZ);

        double sx = System.Math.Sin(rotationX);
        double sy = System.Math.Sin(rotationY);
        double sz = System.Math.Sin(rotationZ);
        
        W = (cx * cy * cz) - (sx * sy * sz);
        XYZ.X = (sx * cy * cz) + (cx * sy * sz);
        XYZ.Y = (cx * sy * cz) - (sx * cy * sz);
        XYZ.Z = (cx * cy * sz) + (sx * sy * cz);
    }

    public Quaternion(Vector3 eulerAngles) : this(eulerAngles.X, eulerAngles.Y, eulerAngles.Z)
    {

    }

    public double X
    {
        get => XYZ.X;
        set => XYZ.X = value;
    }

    public double Y
    {
        get => XYZ.Y;
        set => XYZ.Y = value;
    }

    public double Z
    {
        get => XYZ.Z;
        set => XYZ.Z = value;
    }

    public void ToAxisAngle(Vector3 axis, double angle)
    {
        Vector4 result = ToAxisAngle();
        axis = result.XYZ;
        angle = result.W;
    }

    public Vector4 ToAxisAngle()
    {
        Quaternion q = this;
        if (System.Math.Abs(q.W) > 1)
            q.Normalize();

        double w = 2 * System.Math.Acos(q.W);

        Vector4 result = new Vector4 ( 0, 0, 0, w );

        double den = System.Math.Sqrt(1 - (q.W * q.W));
        switch (den)
        {
            case > 0.0001:
                result.XYZ = q.XYZ / den;
                break;
            default:
                result.XYZ = Vector3.UnitX;
                break;
        }

        return result;
    }


    public void ToEulerAngles(Vector3 angles)
    {
        angles = ToEulerAngles();
    }

    public Vector3 ToEulerAngles()
    {
        Quaternion q = this;

        Vector3 eulerAngles;

        const double SINGULARITY_THRESHOLD = 0.4999995;

        double sqw = q.W * q.W;
        double sqx = q.X * q.X;
        double sqy = q.Y * q.Y;
        double sqz = q.Z * q.Z;
        double unit = sqx + sqy + sqz + sqw;
        double singularityTest = (q.X * q.Z) + (q.W * q.Y);

        if (singularityTest > SINGULARITY_THRESHOLD * unit)
        {
            double eulerAnglesX = 0;
            double eulerAnglesY = MathHelper.pi_over_two;
            double eulerAnglesZ = 2 * System.Math.Atan2(q.X, q.W);
            

            eulerAngles = new Vector3(eulerAnglesX, eulerAnglesY, eulerAnglesZ);
        }
        else if (singularityTest < -SINGULARITY_THRESHOLD * unit)
        {
            double eulerAnglesX = 0;
            double eulerAnglesY = -MathHelper.pi_over_two;
            double eulerAnglesZ = -2 * System.Math.Atan2(q.X, q.W);

            eulerAngles = new Vector3(eulerAnglesX, eulerAnglesY, eulerAnglesZ);
        }
        else
        {
            double eulerAnglesX = System.Math.Atan2(2 * ((q.W * q.X) - (q.Y * q.Z)), sqw - sqx - sqy + sqz);
            double eulerAnglesY = System.Math.Asin(2 * singularityTest / unit);
            double eulerAnglesZ = System.Math.Atan2(2 * ((q.W * q.Z) - (q.X * q.Y)), sqw + sqx - sqy - sqz);
        
            eulerAngles = new Vector3(eulerAnglesX, eulerAnglesY, eulerAnglesZ);
        }

        return eulerAngles;
    }

    public double LengthSquared => (W * W) + XYZ.LengthSquared;

    public double Length
        => System.Math.Sqrt(LengthSquared);

    public double LengthFast
        => MathHelper.InverseSqrtFast(LengthSquared);

    public Quaternion Normalized()
    {
        Quaternion q = this;
        q.Normalized();
        return q;
    }

    public void Invert()
        => Est.Invert(this, out this);

    public Quaternion Inverted()
    {
        Quaternion q = this;
        q.Invert();
        return q;
    }

    public void Normalize()
    {
        double scale = 1 / Length;
        XYZ *= scale;
        W *= scale;
    }

    public void NormalizeFast()
    {
        double scale = 1 / LengthFast;
        XYZ *= scale;
        W *= scale;
    }

    public void Conjugate()
        => XYZ = -XYZ;

    public static Quaternion Identity = new Quaternion(Vector3.Zero, 1);
    public static Quaternion Zero = new Quaternion(Vector3.Zero, 0);
    
    public static Quaternion Add(Quaternion left, Quaternion right)
    {
        Est.Add(left, right, out Quaternion result);
        return result;
    }

    public static Quaternion Substract(Quaternion left, Quaternion right)
    {
        Est.Substract(left, right, out Quaternion result);
        return result;
    }

    public static Quaternion Multiply(Quaternion left, Quaternion right)
    {
        Est.Multiply(left, right, out Quaternion result);
        return result;
    }

    public static Quaternion Multiply(Quaternion quaternion, double scale)
    {
        Est.Multiply(quaternion, scale, out Quaternion result);
        return result;
    }

    public static Quaternion Conjugate(Quaternion q)
    {
        Est.Conjugate(q, out Quaternion result);
        return result;
    }

    public static Quaternion Invert(Quaternion q)
    {
        Est.Invert(q, out Quaternion result);
        return result;
    }

    public static Quaternion Normalize(Quaternion q)
    {
        Est.Normalize(q, out Quaternion result);
        return result;
    }

    public static Quaternion FromAxisAngle(Vector3 axis, double angle)
    {
        if (axis.LengthSquared == 0)
            return Identity;

        Est.FromAxisAngle(axis, angle, out Quaternion result);
        return Normalize(result);
    }

    public static Quaternion FromEulerAngles(double pitch, double yaw, double roll)
    {
        Est.FromEulerAngles(new Vector3(pitch, yaw, roll), out Quaternion result);
        return result;
    }

    public static Quaternion FromEulerAngles(Vector3 eulerAngles)
    {
        Est.FromEulerAngles(eulerAngles, out Quaternion result);
        return result;
    }

    public static void ToEulerAngles(Quaternion q, Vector3 result)
        => q.ToEulerAngles(result);

    public static Quaternion FromMatrix(Matrix3 matrix)
    {
        Est.FromMatrix(matrix, out Quaternion result);
        return result;
    }

    public static Quaternion Slerp(Quaternion q1, Quaternion q2, double blend)
    {
        if (q1.LengthSquared == 0)
        {
            if (q2.LengthSquared == 0) return Identity;

            return q2;
        }

        if (q2.LengthSquared == 0)
            return q1;

        double cosHalfAngle = (q1.W * q2.W) + Vector3.Dot(q1.XYZ, q2.XYZ);

        if (cosHalfAngle >= 1 || cosHalfAngle <= -1)
            return q1;

        Est.Slerp(q1, q2, blend, out Quaternion result);

        if (result.LengthSquared == 0) return Identity;
        return Normalize(result);
    }

    public static Quaternion operator +(Quaternion left, Quaternion right)
        => Add(left, right);

    public static Quaternion operator -(Quaternion left, Quaternion right)
        => Substract(left, right);

    public static Quaternion operator *(Quaternion left, Quaternion right)
        => Multiply(left, right);

    public static Quaternion operator *(Quaternion quaternion, double scale)
        => Multiply(quaternion, scale);

    public static Quaternion operator *(double scale, Quaternion quaternion)
        => Multiply(quaternion, scale);

    public static bool operator ==(Quaternion left, Quaternion right)
        => left.Equals(right);
    
    public static bool operator !=(Quaternion left, Quaternion right)
        => !left.Equals(right);

    public override bool Equals(object? obj)
        => obj is Quaternion && Equals((Quaternion)obj);

    public bool Equals(Quaternion other)
        => this == other;

    public override int GetHashCode()
        => HashCode.Combine(XYZ, W);

    public override string ToString()
        => string.Format("({0}{2} {1})", XYZ, W, MathHelper.ListSeparator);
}