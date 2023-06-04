using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

public struct Matrix3 : IEquatable<Matrix3>
{
    public Vector3 Row1;
    public Vector3 Row2;
    public Vector3 Row3;

    public Matrix3(Vector3 value) : this(value, value, value)
    {

    }

    public Matrix3(Vector3 Row1, Vector3 Row2, Vector3 Row3)
    {
        this.Row1 = Row1;
        this.Row2 = Row2;
        this.Row3 = Row3;
    }

    public Matrix3(double M11, double M12, double M13,
                   double M21, double M22, double M23,
                   double M31, double M32, double M33)
    {
        Row1 = new Vector3(M11, M12, M13);
        Row2 = new Vector3(M21, M22, M23);
        Row3 = new Vector3(M31, M32, M33);
    }

    public Vector3 Column0
    {
        get => new Vector3(Row1.X, Row2.X, Row3.X);
        set
        {
            Row1.X = value.X;
            Row2.X = value.Y;
            Row3.X = value.Z;
        }
    }

    public Vector3 Column1
    {
        get => new Vector3(Row1.Y, Row2.Y, Row3.Y);
        set
        {
            Row1.Y = value.X;
            Row2.Y = value.Y;
            Row3.Y = value.Z;
        }
    }

    public Vector3 Column2
    {
        get => new Vector3(Row1.Z, Row2.Z, Row3.Z);
        set
        {
            Row1.Z = value.X;
            Row2.Z = value.Y;
            Row3.Z = value.Z;

        }
    }

    public double M11
    {
        get => Row1.X;
        set => Row1.X = value;
    }

    public double M12
    {
        get => Row1.Y;
        set => Row1.Y = value;
    }

    public double M13
    {
        get => Row1.Z;
        set => Row1.Z = value;
    }

    public double M21
    {
        get => Row2.X;
        set => Row2.X = value;
    }

    public double M22
    {
        get => Row2.Y;
        set => Row2.Y = value;
    }

    public double M23
    {
        get => Row2.Z;
        set => Row2.Z = value;
    }

    public double M31
    {
        get => Row3.X;
        set => Row3.X = value;
    }

    public double M32
    {
        get => Row3.Y;
        set => Row3.Y = value;
    }

    public double M33
    {
        get => Row3.Z;
        set => Row3.Z = value;
    }

    public Vector3 Diagonal
    {
        get => new Vector3(Row1.X, Row2.Y, Row3.Y);
        set
        {
            Row1.X = value.X;
            Row2.Y = value.Y;
            Row3.Z = value.Z;
        }
    }

    public double Determinant
    {
        get
        {
            return (M11 * M22 * M33) + (M12 * M23 * M31) 
                 + (M13 * M21 * M32) - (M13 * M22 * M31) 
                 - (M11 * M23 * M32) - (M12 * M21 * M33);
        }
    }

    public static Matrix3 Identity = new Matrix3(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
    public static Matrix3 Zero = new Matrix3(Vector3.Zero, Vector3.Zero, Vector3.Zero);

    public double Trace => M11 + M22 + M33;

    public double this[int rowIndex, int columnIndex]
    {
        get
        {
            return rowIndex switch
            {
                0 => Row1[columnIndex],
                1 => Row2[columnIndex],
                2 => Row3[columnIndex],
                _ => throw new IndexOutOfRangeException($"You tried to access this matrix at: ({rowIndex}, {columnIndex}")
            };
        }
        set
        {
            switch (rowIndex)
            {
                case 0:
                    Row1[columnIndex] = value;
                    break;
                case 1:
                    Row2[columnIndex] = value;
                    break;
                case 2:
                    Row3[columnIndex] = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"You tried to set this matrix at: {rowIndex}, {columnIndex}");
            }
        }
    }

    public void Transpose() => this = Transpose(this);

    public static Matrix3 Transpose(Matrix3 mat)
    {
        Est.Transpose(mat, out Matrix3 result);
        return result;
    }

    public void Invert() => this = Invert(this);

    public static Matrix3 Invert(Matrix3 mat)
    {
        Est.Invert(mat, out Matrix3 result);
        return result;
    }

    public Matrix3 Normalized()
    {
        var m = this;
        m.Normalize();
        return m;
    }

    public void Normalize()
    {
        Row1 /= Determinant;
        Row2 /= Determinant;
        Row3 /= Determinant;
    }

    public Matrix3 Inverted()
    {
        var m = this;
        if(m.Determinant != 0)
            m.Invert();

        return m;
    }

    public Matrix3 ClearScale()
    {
        Matrix3 mat = this;
        mat = new Matrix3
        (
            mat.Row1.Normalized(),
            mat.Row2.Normalized(),
            mat.Row3.Normalized()
        );
        return mat;
    }

    public Matrix3 ClearRotation()
    {
        Matrix3 mat = this;
        mat = new Matrix3
        (
            new Vector3(mat.Row1.Length, 0, 0),
            new Vector3(0, mat.Row2.Length, 0),
            new Vector3(0, 0, mat.Row3.Length)
        );
        return mat;
    }

    public Vector3 ExtractScale()
        => new Vector3(Row1.Length, Row2.Length, Row3.Length);

    public Quaternion ExtractRotation(bool rowNormalize = true)
    {
        Vector3 row1 = Row1;
        Vector3 row2 = Row2;
        Vector3 row3 = Row3;

        if (rowNormalize)
        {
            row1 = row1.Normalized();
            row2 = row2.Normalized();
            row3 = row3.Normalized();
        }

        Quaternion q = default(Quaternion);
        double trace = 0.25 * (row1[0] + row2[1] + row3[2] + 1);

        if (trace > 0)
        {
            double sq = System.Math.Sqrt(trace);
            q = new Quaternion
            (
                (row2[2] - row3[1]) * (1 / (4 * sq)),
                (row3[0] - row1[2]) * (1 / (4 * sq)),
                (row1[1] - row2[0]) * (1 / (4 * sq)),
                sq
            );
        }
        else if (row1[0] > row2[1] && row1[0] > row3[2])
        {
            double sq = 2 * System.Math.Sqrt(1 + row1[0] - row2[1] - row3[2]);
            q = new Quaternion
            (
                0.25 * sq,
                (row2[0] + row1[1]) * (1 / sq),
                (row3[0] + row1[2]) * (1 / sq),
                (row3[1] - row2[2]) * (1 / sq)
            );
        }
        else if (row2[1] > row3[2])
        {
            double sq = 2 * System.Math.Sqrt(1 + row2[1] - row1[0] - row3[2]);
            q = new Quaternion
            (
                (row2[0] + row1[1]) * (1 / sq),
                0.25 * sq,
                (row3[1] + row2[2]) * (1 / sq),
                (row3[0] - row1[2]) * (1 / sq)

            );
        }
        else
        {
            double sq = 2 * System.Math.Sqrt(1 + row3[2] - row1[0] - row2[1]);
            q = new Quaternion
            (
                (row3[0] + row1[2]) * (1 / sq),
                (row3[1] + row2[2]) * (1 / sq),
                0.25 * sq,
                (row3[1] + row2[2]) * (1 / sq)
            );
        }

        q.Normalize();
        return q;
    }

    public static Matrix3 CreateFromAxisAngle(Vector3 axis, double angle)
    {
        Est.CreateFromAxisAngle(axis, angle, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateFromQuaternion(Quaternion q)
    {
        Est.CreateFromQuaternion(q, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateRotationX(double angle)
    {
        Est.CreateRotationX(angle, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateRotationY(double angle)
    {
        Est.CreateRotationY(angle, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateRotationZ(double angle)
    {
        Est.CreateRotationZ(angle, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateScale(double scale)
    {
        Est.CreateScale(scale, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateScale(Vector3 scale)
    {
        Est.CreateScale(scale, out Matrix3 result);
        return result;
    }

    public static Matrix3 CreateScale(double x, double y, double z)
    {
        Est.CreateScale(x, y, z, out Matrix3 result);
        return result;
    }

    public static Matrix3 Add(Matrix3 left, Matrix3 right)
    {
        Est.Add(left, right, out Matrix3 result);
        return result;
    }

    public static Matrix3 Subtract(Matrix3 left, Matrix3 right)
    {
        Est.Subtract(left, right, out Matrix3 result);
        return result;
    }

    public static Matrix3 Multuply(Matrix3 left, double right)
    {
        Est.Multuply(left, right, out Matrix3 result);
        return result;
    }

    public static Matrix3 Multuply(Matrix3 left, Matrix3 right)
    {
        Est.Multuply(left, right, out Matrix3 result);
        return result;
    }

    public static Matrix3 operator *(double left, Matrix3 right)
        => Multuply(right, left);

    public static Matrix3 operator *(Matrix3 left, double right)
        => Multuply(left, right);

    public static Matrix3 operator *(Matrix3 left, Matrix3 right)
        => Multuply(left, right);

    public static Matrix3 operator +(Matrix3 left, Matrix3 right)
        => Add(left, right);

    public static Matrix3 operator -(Matrix3 left, Matrix3 right)
        => Subtract(left, right);

    public static bool operator ==(Matrix3 left, Matrix3 right)
        => left.Equals(right);

    public static bool operator !=(Matrix3 left, Matrix3 right)
        => !left.Equals(right);

    public override string ToString()
        => string.Format("{0}\n{1}\n{2}", Row1, Row2, Row3);

    public override int GetHashCode()
        => HashCode.Combine(Row1, Row2, Row3);

    public override bool Equals(object? obj)
        => obj is Matrix3 && Equals((Matrix3)obj);

    public bool Equals(Matrix3 other)
        => this == other;
}