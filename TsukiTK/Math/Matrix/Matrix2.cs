using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Math.Matrix;

public struct Matrix2 : IEquatable<Matrix2>
{
    public Vector2 Row1;
    public Vector2 Row2;

    public Matrix2(Vector2 value) : this(value, value)
    {

    }

    public Matrix2(Vector2 Row1, Vector2 Row2)
    {
        this.Row1 = Row1;
        this.Row2 = Row2;
    }

    public Matrix2 (double M00, double M01,
                    double M10, double M11)
    {
        Row1 = new Vector2(M00, M01);
        Row2 = new Vector2(M10, M11);
    }

    public Vector2 Column0
    {
        get => new Vector2(Row1.X, Row2.X);
        set
        {
            Row1.X = value.X;
            Row2.X = value.Y;
        }
    }

    public Vector2 Column1
    {
        get => new Vector2(Row1.Y, Row2.Y);
        set
        {
            Row1.Y = value.X;
            Row2.Y = value.Y;
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

    public Vector2 Diagonal
    {
        get => new Vector2(Row1.X, Row2.Y);
        set
        {
            Row1.X = value.X;
            Row2.Y = value.Y;
        }
    }

    public double Determinant
    {
        get
        {
            return (M11 * M22) - (M12 * M21);
        }
    }

    public static Matrix2 Identity = new Matrix2(Vector2.UnitX, Vector2.UnitY);
    public static Matrix2 Zero = new Matrix2(Vector2.Zero, Vector2.Zero);
    
    public double Trace => M11 + M22;

    public double this[int rowIndex, int columnIndex]
    {
        get
        {
            return rowIndex switch
            {
                0 => Row1[columnIndex],
                1 => Row2[columnIndex],
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
                default:
                    throw new IndexOutOfRangeException($"You tried to set this matrix at: ({rowIndex}, {columnIndex})");
            }
        }
    }

    public void Transpose() => this = Transpose(this);

    public static Matrix2 Transpose(Matrix2 mat)
    {
        Est.Transpose(mat, out Matrix2 result);
        return result;
    }

    public void Invert() => this = Invert(this);

    public static Matrix2 Invert(Matrix2 mat)
    {
        Est.Invert(mat, out Matrix2 result);
        return result;
    }

    public Matrix2 Normalized()
    {
        Matrix2 m = this;
        m.Normalize();
        return m;
    }

    private void Normalize()
    {
        double determinant = Determinant;
        Row1 /= determinant;
        Row2 /= determinant;
    }

    public Matrix2 Inverted()
    {
        Matrix2 m = this;
        if(m.Determinant != 0)
            m.Invert();

        return m;
    }

    public Matrix2 ClearScale()
    {
        Matrix2 mat = this;
        mat = new Matrix2
        (
            mat.Row1.Normalized(),
            mat.Row2.Normalized()
        );
        return mat;
    }

    public Matrix2 ClearRotation()
    {
        Matrix2 mat = this;
        mat = new Matrix2
        (
            new Vector2(mat.Row1.Length, 0),
            new Vector2(0, mat.Row2.Length)
        );
        return mat;
    }

    public static Matrix2 CreateRotation(double angle)
    {
        Est.CreateRotation(angle, out Matrix2 result);
        return result;
    }

    public static Matrix2 CreateScale(double scale)
    {
        Est.CreateScale(scale, out Matrix2 result);
        return result;
    }

    public static Matrix2 CreateScale(Vector2 scale)
    {
        Est.CreateScale(scale, out Matrix2 result);
        return result;
    }

    public static Matrix2 CreateScale(double x, double y)
    {
        Est.CreateScale(x, y, out Matrix2 result);
        return result;
    }

    public static Matrix2 Add(Matrix2 left, Matrix2 right)
    {
        Est.Add(left, right, out Matrix2 result);
        return result;
    }

    public static Matrix2 Subtract(Matrix2 left, Matrix2 right)
    {
        Est.Subtract(left, right, out Matrix2 result);
        return result;
    }

    public static Matrix2 Multuply(Matrix2 left, double right)
    {
        Est.Multuply(left, right, out Matrix2 result);
        return result;
    }

    public static Matrix2 Multuply(Matrix2 left, Matrix2 right)
    {
        Est.Multuply(left, right, out Matrix2 result);
        return result;
    }

    public static Matrix2 operator *(double left, Matrix2 right)
        => Multuply(right, left);

    public static Matrix2 operator *(Matrix2 left, double right)
        => Multuply(left, right);

    public static Matrix2 operator *(Matrix2 left, Matrix2 right)
        => Multuply(left, right);

    public static Matrix2 operator +(Matrix2 left, Matrix2 right)
        => Add(left, right);

    public static Matrix2 operator -(Matrix2 left, Matrix2 right)
        => Subtract(left, right);

    public static bool operator ==(Matrix2 left, Matrix2 right)
        => left.Equals(right);

    public static bool operator !=(Matrix2 left, Matrix2 right)
        => !left.Equals(right);

    public override string ToString()
        => string.Format("{0}\n{1}", Row1, Row2);

    public override int GetHashCode()
        => HashCode.Combine(Row1, Row2);

    public override bool Equals(object? obj)
        => obj is Matrix2 && Equals((Matrix2)obj);

    public bool Equals(Matrix2 other)
        => this == other;
}