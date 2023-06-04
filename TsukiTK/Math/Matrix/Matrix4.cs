using Tsuki.Framework.Math.Data;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

public struct Matrix4 : IEquatable<Matrix4>
{
    public Vector4 Row1;
    public Vector4 Row2;
    public Vector4 Row3;
    public Vector4 Row4;

    public Matrix4(Vector4 value) : this(value, value, value, value)
    {

    }

    public Matrix4(Vector4 Row1, Vector4 Row2, Vector4 Row3, Vector4 Row4)
    {
        this.Row1 = Row1;
        this.Row2 = Row2;
        this.Row3 = Row3;
        this.Row4 = Row4;
    }

    public Matrix4(double M11, double M12, double M13, double M14,
                   double M21, double M22, double M23, double M24,
                   double M31, double M32, double M33, double M34,
                   double M41, double M42, double M43, double M44)
    {
        Row1 = new Vector4(M11, M12, M13, M14);
        Row2 = new Vector4(M21, M22, M23, M24);
        Row3 = new Vector4(M31, M32, M33, M34);
        Row4 = new Vector4(M41, M42, M43, M44);
    }

    public Vector4 Column0
    {
        get => new Vector4(Row1.X, Row2.X, Row3.X, Row4.X);
        set
        {
            Row1.X = value.X;
            Row2.X = value.Y;
            Row3.X = value.Z;
            Row4.X = value.W;
        }
    }

    public Vector4 Column1
    {
        get => new Vector4(Row1.Y, Row2.Y, Row3.Y, Row4.Y);
        set
        {
            Row1.Y = value.X;
            Row2.Y = value.Y;
            Row3.Y = value.Z;
            Row4.Y = value.W;
        }
    }

    public Vector4 Column2
    {
        get => new Vector4(Row1.Z, Row2.Z, Row3.Z, Row4.Z);
        set
        {
            Row1.Z = value.X;
            Row2.Z = value.Y;
            Row3.Z = value.Z;
            Row4.Z = value.W;
        }
    }

    public Vector4 Column3
    {
        get => new Vector4(Row1.W, Row2.W, Row3.W, Row4.W);
        set
        {
            Row1.W = value.X;
            Row2.W = value.Y;
            Row3.W = value.Z;
            Row4.W = value.W;
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

    public double M14
    {
        get => Row1.W;
        set => Row1.W = value;
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

    public double M24
    {
        get => Row2.W;
        set => Row2.W = value;
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

    public double M34
    {
        get => Row3.W;
        set => Row3.W = value;
    }

    public double M41
    {
        get => Row4.X;
        set => Row4.X = value;
    }

    public double M42
    {
        get => Row4.Y;
        set => Row4.Y = value;
    }

    public double M43
    {
        get => Row4.Z;
        set => Row4.Z = value;
    }

    public double M44
    {
        get => Row4.W;
        set => Row4.W = value;
    }

    public Vector4 Diagonal
    {
        get => new Vector4(Row1.X, Row2.Y, Row3.Z, Row4.W);
        set
        {
            Row1.X = value.X;
            Row2.Y = value.Y;
            Row3.Z = value.Z;
            Row4.W = value.W;
        }
    }

    public double Determinant
    {
        get
        {
            return (M11 * M22 * M33 * M44) - (M11 * M22 * M34 * M43) +
                   (M11 * M23 * M34 * M42) - (M11 * M23 * M32 * M44) + 
                   (M11 * M24 * M32 * M43) - (M11 * M24 * M33 * M42) -
                   (M12 * M23 * M34 * M41) + (M12 * M23 * M31 * M44) - 
                   (M12 * M24 * M31 * M43) + (M12 * M24 * M33 * M41) -
                   (M12 * M21 * M33 * M44) + (M12 * M21 * M34 * M43) + 
                   (M13 * M24 * M31 * M42) - (M13 * M24 * M32 * M41) + 
                   (M13 * M21 * M32 * M44) - (M13 * M21 * M34 * M42) + 
                   (M13 * M22 * M34 * M41) - (M13 * M22 * M31 * M44) -
                   (M14 * M21 * M32 * M43) + (M14 * M21 * M33 * M42) - 
                   (M14 * M22 * M33 * M41) + (M14 * M22 * M31 * M43) -
                   (M14 * M23 * M31 * M42) + (M14 * M23 * M32 * M41);
        }
    }

    public static Matrix4 Identity
        = new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);

    public static Matrix4 Zero
        = new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);
    
    public static Matrix4 One
        = new Matrix4(Vector4.One, Vector4.One, Vector4.One, Vector4.One);

    public double Trace => M11 + M22 + M33 + M44;

    public double this[int rowIndex, int columnIndex]
    {
        get
        {
            return rowIndex switch
            {
                0 => Row1[columnIndex],
                1 => Row2[columnIndex],
                2 => Row3[columnIndex],
                3 => Row4[columnIndex],
                _ => throw new IndexOutOfRangeException($"You tried to access this matrix at: ({rowIndex}, {columnIndex})")
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
                case 3:
                    Row4[columnIndex] = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"You tried to set this matrix at: ({rowIndex}, {columnIndex})");
            }
        }
    }

    public void Transpose() => this = Transpose(this);

    public static Matrix4 Transpose(Matrix4 mat)
    {
        Est.Transpose(mat, out Matrix4 result);
        return result;
    }

    public void Invert() => this = Invert(this);

    public static Matrix4 Invert(Matrix4 mat)
    {
        Est.Invert(mat, out Matrix4 result);
        return result;
    }

    public Matrix4 Normalized()
    {
        Matrix4 m = this;
        m.Normalize();
        return m;
    }

    public void Normalize()
    {
        double determinant = Determinant;
        Row1 /= determinant;
        Row2 /= determinant;
        Row3 /= determinant;
        Row4 /= determinant;
    }

    public Matrix4 Inverted()
    {
        Matrix4 m = this;
        if(m.Determinant != 0)
            m.Invert();
        
        return m;
    }

    public Matrix4 ClearTranslation()
    {
        Matrix4 mat = this;
        mat.Row4.XYZ = Vector3.Zero;
        return mat;
    }

    public Matrix4 ClearScale()
    {
        Matrix4 mat = this;
        mat = new Matrix4
        (
            mat.Row1.Normalized(),
            mat.Row2.Normalized(),
            mat.Row3.Normalized(),
            mat.Row4.Normalized()
        );
        return mat;
    }

    public Matrix4 ClearRotation()
    {
        Matrix4 mat = this;
        mat = new Matrix4
        (
            new Vector4(mat.Row1.Length, 0, 0, 0),
            new Vector4(0, mat.Row2.Length, 0, 0),
            new Vector4(0, 0, mat.Row3.Length, 0),
            new Vector4(0, 0, 0, mat.Row4.Length)
        );
        return mat;
    }

    public Vector4 ExtractScale()
        => new Vector4(Row1.Length, Row2.Length, Row3.Length, Row4.Length);

    public Quaternion ExtractRotation(bool rowNormalize = true)
    {
        Vector4 row1 = Row1;
        Vector4 row2 = Row2;
        Vector4 row3 = Row3;
        Vector4 row4 = Row4;

        if (rowNormalize)
        {
            row1 = row1.Normalized();
            row2 = row2.Normalized();
            row3 = row3.Normalized();
            row4 = row4.Normalized();
        }

        Quaternion q = default(Quaternion);
        double trace = 0.25 * (row1[0] + row2[1] + row3[2] + row4[3] + 1);

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
                (row2[0] - row1[1]) * (1 / sq)
            );
        }

        q.Normalize();
        return q;
    }

    public Vector4 ExtractProjection() => Column3;

    public static Matrix4 CreateFromAxisAngle(Vector3 axis, double angle)
    {
        Est.CreateFromAxisAngle(axis, angle, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateFromQuaternion(Quaternion q)
    {
        Est.CreateFromQuaternion(q, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateRotationX(double angle)
    {
        Est.CreateRotationX(angle, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateRotationY(double angle)
    {
        Est.CreateRotationY(angle, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateRotationZ(double angle)
    {
        Est.CreateRotationZ(angle, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateScale(double scale)
    {
        Est.CreateScale(scale, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateScale(Vector4 scale)
    {
        Est.CreateScale(scale, out Matrix4 result);
        return result;
    }

    public static Matrix4 CreateScale(double x, double y, double z)
    {
        Est.CreateScale(x, y, z, out Matrix4 result);
        return result;
    }

    public static Matrix4 Add(Matrix4 left, Matrix4 right)
    {
        Est.Add(left, right, out Matrix4 result);
        return result;
    }

    public static Matrix4 Subtract(Matrix4 left, Matrix4 right)
    {
        Est.Subtract(left, right, out Matrix4 result);
        return result;
    }

    public static Matrix4 Multuply(Matrix4 left, Matrix4 right)
    {
        Est.Multuply(left, right, out Matrix4 result);
        return result;
    }

    public static Matrix4 Multuply(Matrix4 left, double right)
    {
        Est.Multuply(left, right, out Matrix4 result);
        return result;
    }

    public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        => Multuply(left, right);

    public static Matrix4 operator *(Matrix4 left, double right)
        => Multuply(left, right);

    public static Matrix4 operator *(double left, Matrix4 right)
        => Multuply(right, left);

    public static Matrix4 operator +(Matrix4 left, Matrix4 right)
        => Add(left, right);

    public static Matrix4 operator -(Matrix4 left, Matrix4 right)
        => Subtract(left, right);

    public static bool operator ==(Matrix4 left, Matrix4 right)
        => left.Equals(right);

    public static bool operator !=(Matrix4 left, Matrix4 right)
        => !left.Equals(right);

    public override string ToString()
        => string.Format("{0}\n{1}\n{2}\n{3}", Row1, Row2, Row3, Row4);

    public override int GetHashCode()
        => HashCode.Combine(Row1, Row2, Row3, Row4);

    public override bool Equals(object? obj)
        => obj is Matrix4 && Equals((Matrix4)obj);

    public bool Equals(Matrix4 other)
        => this == other;
}