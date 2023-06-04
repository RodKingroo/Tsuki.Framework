using System.Globalization;
using System.Runtime.Serialization;

namespace Tsuki.Framework.Math.Data;

public struct Half : ISerializable, IComparable<Half>, IFormattable, IEquatable<Half>
{
    private ushort _bits;

    public static double MinValue = 5.96046448e-08;
    public static double MinNormalizedValue = 6.10351562e-05;
    public static double MaxValue = 65504.0;

    public static double Epsilon = 0.00097656;

    public static int SizeInBytes = 2;

    public bool IsZero
        => _bits == 0 || _bits == 32768;

    public bool IsNan 
        => (_bits & 31744) == 31744 && (_bits & 1023) != 0;

    public bool IsPositiveInfinity
        => _bits == 31744;

    public bool IsNegativeInfinity
        => _bits == 64512;
    
    public Half(double d) : this()
        => _bits = SingleToHalf((int)d);

    public Half(double d, bool throwOnError) : this(d)
    {
        if (throwOnError)
        {
            if (d > MaxValue)
                throw new ArithmeticException("Half: Positive maximum value exceeded.");

            if (d < -MaxValue)
                throw new ArithmeticException("Half: Negative minimum value exceeded.");

            if (double.IsNaN(d))
                throw new ArithmeticException("Half: Input is not a number (NaN).");

            if (double.IsPositiveInfinity(d))
                throw new ArithmeticException("Half: Input is positive infinity.");

            if (double.IsNegativeInfinity(d))
                throw new ArithmeticException("Half: Input is negative infinity.");
        }
    }

    private ushort SingleToHalf(int si32)
    {
        int sign = (si32 >> 16) & 32768;
        int exponent = ((si32 >> 23) & 255) - (127 - 15);
        int mantissa = si32 & 8388607;

        switch (exponent)
        {
            case <= 0:
                if (exponent < -10) return (ushort)sign;

                mantissa = mantissa | 8388608;

                int t = 14 - exponent;
                int a = (1 << t - 1) - 1;
                int b = mantissa >> t & 1;

                mantissa = mantissa + a + b >> t;

                return (ushort)(sign | mantissa);
            
            case 255 - (127 - 15):
                if (mantissa == 0)
                    return (ushort)(sign | 31744);

                mantissa >>= 13;
                return (ushort)(sign | 31744 | mantissa | (mantissa == 0 ? 1 : 0));
        }

        mantissa = mantissa + 4095 + ((mantissa >> 13) & 1);

        if ((mantissa & 8388608) != 0)
        {
            mantissa = 0;
            exponent += 1;
        }

        if (exponent > 30)
            throw new ArithmeticException("Half: Hardware floating-point overflow.");

        return (ushort)(sign | (exponent << 10) | (mantissa >> 13));
    }

    public float ToSingle()
        => (float)HalfToFloat(_bits);
        
    private int HalfToFloat(ushort ui16)
    {
        int sign = (ui16 >> 15) & 1;
        int exponent = (ui16 >> 10) & 31;
        int mantissa = ui16 & 1023;

        switch (exponent)
        {
            case 0:
                if (mantissa == 0)
                    return sign << 31;

                while ((mantissa & 1024) == 0)
                {
                    mantissa <<= 1;
                    exponent -= 1;
                }

                exponent += 1;
                mantissa &= ~1024;
                break;
            case 31:
                if (mantissa == 0)
                    return sign << 31 | 2139095040;

            return sign << 31 | 2139095040 | mantissa << 13;
        }

        exponent = exponent + (127 - 15);
        mantissa = mantissa << 13;

        return (sign << 31) | (exponent << 23) | mantissa;
    }

    public static explicit operator Half(double d)
        => new Half(d);

    public static implicit operator float(Half h)
        => h.ToSingle();

    public static implicit operator double(Half h)
        => h.ToSingle();

    public static bool operator ==(Half left, Half right)
        => left.Equals(right);

    public static bool operator !=(Half left, Half right)
        => !(left == right);

    public Half(SerializationInfo info, StreamingContext context)
        => _bits = (ushort)info.GetValue("bits", typeof(ushort))!;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
        => info.AddValue("bits", _bits);

    public void FromBinaryStream(BinaryReader bin)
        => _bits = bin.ReadUInt16();
        
    public void ToBinaryStream(BinaryWriter bin)
        => bin.Write(_bits);

    public override int GetHashCode()
        => HashCode.Combine(_bits);

    public override bool Equals(object? obj)
        => base.Equals(obj);  

    public bool Equals(Half other)
    {
        const int maxUlps = 1;

        short k = unchecked((short)other._bits);
        short l = unchecked((short)_bits);

        if (k < 0)
            k = (short)(32768 - k);

        if (l < 0)
            l = (short)(32768 - l);

        short intDiff = System.Math.Abs((short)(k - l));

        if (intDiff <= maxUlps)
            return true;

        return false;
    }

    public int CompareTo(Half other)
        => ((double)this).CompareTo(other);

    public override string ToString()
        => ToSingle().ToString();

    public string ToString(string? format, IFormatProvider? formatProvider)
        => ToSingle().ToString(format, formatProvider);

    public static Half Parse(string s)
        => (Half)double.Parse(s);

    public static Half Parse(string s, NumberStyles style, IFormatProvider provider)
        => (Half)double.Parse(s, style, provider);

    public static bool TryParse(string s, out Half result)
    {
        bool b = double.TryParse(s, out double f);
        result = (Half)f;
        return b;
    }

    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Half result)
    {
        bool b = double.TryParse(s, style, provider, out double f);
        result = (Half)f;
        return b;
    }

    public static byte[] GetBytes(Half h)
        => BitConverter.GetBytes(h._bits);

    public static Half FromBytes(byte[] value, int startIndex)
    {
        Half h;
        h._bits = BitConverter.ToUInt16(value, startIndex);
        return h;
    }
}