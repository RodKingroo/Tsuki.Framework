using System.Globalization;

namespace Tsuki.Framework.Math;

public static class MathHelper
{
    public const float pi = 3.1415927f;

    public const float pi_over_two = pi / 2;
    public const float pi_over_three = pi / 3;
    public const float pi_over_four = pi / 4;
    public const float pi_over_six = pi / 6;

    public const float two_pi = 2 * pi;

    public const float three_pi_over_two = 3 * pi / 2;
    public const float e = 2.7182817f;

    public const float log_10_e = 0.4342945f;
    public const float log_2_e = 1.442695f;

    public static decimal Abs(decimal value)
        => System.Math.Abs(value);

    public static double Abs(double value) 
        => System.Math.Abs(value);

    public static short Abs(short value)
        => System.Math.Abs(value);
    
    public static int Abs(int value)
        => System.Math.Abs(value);

    public static long Abs(long value)
        => System.Math.Abs(value);

    public static sbyte Abs(sbyte value) 
        => System.Math.Abs(value);

    public static float Abs(float value) 
        => System.Math.Abs(value);

    public static double Sin(double a) 
        => System.Math.Sin(a);

    public static double Sinh(double value)
        => System.Math.Sinh(value);

    public static double Asin(double d) 
        => System.Math.Asin(d);

    public static double Cos(double d) 
        => System.Math.Cos(d);

    public static double Cosh(double value) 
        => System.Math.Cosh(value);

    public static double Acos(double d) 
        => System.Math.Acos(d);

    public static double Tan(double a) 
        => System.Math.Tan(a);

    public static double Tanh(double value) 
        => System.Math.Tanh(value);

    public static double Atan(double d) 
        => System.Math.Atan(d);

    public static double Atan2(double y, double x) 
        => System.Math.Atan2(y, x);

    public static long BigMul(int a, int b) 
        => System.Math.BigMul(a, b);

    public static double Sqrt(double d) 
        => System.Math.Sqrt(d);

    public static double Pow(double x, double y) 
        => System.Math.Pow(x, y);

    public static decimal Ceiling(decimal d) 
        => System.Math.Ceiling(d);

    public static double Ceiling(double a) 
        => System.Math.Ceiling(a);

    public static decimal Floor(decimal d) 
        => System.Math.Floor(d);

    public static double Floor(double d) 
        => System.Math.Floor(d);

    public static int IntLenght(ulong i)
    {
        return i switch
        {
            < 10 => 1,
            < 100 => 2,
            < 1000 => 3,
            < 10000 => 4,
            < 100000 => 5,
            < 1000000 => 6,
            < 10000000 => 7,
            < 100000000 => 8,
            < 1000000000 => 9,
            < 10000000000 => 10,
            < 100000000000 => 11,
            < 1000000000000 => 12,
            < 10000000000000 => 13,
            < 100000000000000 => 14,
            < 1000000000000000 => 15,
            < 10000000000000000 => 16,
            < 100000000000000000 => 17,
            < 1000000000000000000 => 18,
            < 10000000000000000000 => 19,
            _ => 20
        };
    }

    public static char IntToHex(int n)
    {
        return n switch
        {
            <= 9 => (char)(n + 48),
            _ => (char)(n - 10 + 97)
        };
    }

    public static int DivRem(int a, int b, out int result) 
        => System.Math.DivRem(a, b, out result);

    public static long DivRem(long a, long b, out long result) 
        => System.Math.DivRem(a, b, out result);

    public static double Log(double d) 
        => System.Math.Log(d);

    public static double Log(double n, double newBase) 
        => System.Math.Log(n, newBase);

    public static double Log10(double d) 
        => System.Math.Log10(d);

    public static double Log2(double a) 
        => System.Math.Log(a, 2);

    public static double Exp(double d) 
        => System.Math.Exp(d);

    public static double IEEERemainder(double x, double y) 
        => System.Math.IEEERemainder(x, y);

    public static byte Max(byte val1, byte val2) 
        => System.Math.Max(val2, val2);

    public static sbyte Max(sbyte val1, sbyte val2) 
        => System.Math.Max(val1, val2);

    public static short Max(short val1, short val2) 
        => System.Math.Max(val1, val2);

    public static ushort Max(ushort val1, ushort val2) 
        => System.Math.Max(val1, val2);
    
    public static decimal Max(decimal val1, decimal val2) 
        => System.Math.Max(val1, val2);

    public static int Max(int val1, int val2) 
        => System.Math.Max(val1, val2);
    public static uint Max(uint val1, uint val2) 
        => System.Math.Max(val1, val2);

    public static float Max(float val1, float val2) 
        => System.Math.Max(val1, val2);

    public static long Max(long val1, long val2) 
        => System.Math.Max(val1, val2);

    public static ulong Max(ulong val1, ulong val2) 
        => System.Math.Max(val1, val2);

    public static byte Min(byte val1, byte val2) 
        => System.Math.Min(val1, val2);

    public static sbyte Min(sbyte val1, sbyte val2) 
        => System.Math.Min(val1, val2);

    public static short Min(short val1, short val2) 
        => System.Math.Min(val1, val2);

    public static ushort Min(ushort val1, ushort val2) 
        => System.Math.Min(val1, val2);

    public static decimal Min(decimal val1, decimal val2) 
        => System.Math.Min(val1, val2);

    public static int Min(int val1, int val2) 
        => System.Math.Min(val1, val2);

    public static uint Min(uint val1, uint val2) 
        => System.Math.Min(val1, val2);

    public static float Min(float val1, float val2) 
        => System.Math.Min(val1, val2);

    public static double Min(double val1, double val2) 
        => System.Math.Min(val1, val2);

    public static long Min(long val1, long val2) 
        => System.Math.Min(val1, val2);

    public static ulong Min(ulong val1, ulong val2) 
        => System.Math.Min(val1, val2);

    public static decimal Round(decimal d, int decimals, MidpointRounding mode) 
        => System.Math.Round(d, decimals, mode);

    public static double Round(double d, int digits, MidpointRounding mode) 
        => System.Math.Round(d, digits, mode);

    public static decimal Round(decimal d, MidpointRounding mode) 
        => System.Math.Round(d, mode);

    public static double Round(double value, MidpointRounding mode) 
        => System.Math.Round(value, mode);

    public static decimal Round(decimal d, int decimals) 
        => System.Math.Round(d, decimals);

    public static double Round(double value, int digits) 
        => System.Math.Round(value, digits);

    public static decimal Round(decimal d) 
        => System.Math.Round(d);

    public static double Round(double a) 
        => System.Math.Round(a);

    public static decimal Truncate(decimal d) 
        => System.Math.Truncate(d);

    public static double Truncate(double d) 
        => System.Math.Truncate(d);

    public static int Sign(sbyte value) 
        => System.Math.Sign(value);

    public static int Sign(short value) 
        => System.Math.Sign(value);

    public static int Sign(int value) 
        => System.Math.Sign(value);

    public static int Sign(float value) 
        => System.Math.Sign(value);

    public static int Sign(decimal value) 
        => System.Math.Sign(value);

    public static int Sign(double value) 
        => System.Math.Sign(value);

    public static int Sign(long value) 
        => System.Math.Sign(value: value);

    public static long NextPowerOfTwo(long a) 
        => (long)System.Math.Pow(2, System.Math.Ceiling(System.Math.Log(a, 2)));

    public static int NextPowerOfTwo(int a)
        => (int)System.Math.Pow(2, System.Math.Ceiling(System.Math.Log(a, 2)));

    public static float NextPowerOfTwo(float x)
        => System.MathF.Pow(2, System.MathF.Ceiling(System.MathF.Log(x, 2)));
        
    public static double NextPowerOfTwo(double a)
        => System.Math.Pow(2, System.Math.Ceiling(System.Math.Log(a, 2)));
        
    public static long Factorial(int n)
    {
        long result = 1;

        for (; n > 1; n--)
            result *= n;

        return result;
    }

    public static long BinomialCoefficient(int n1, int n2)
        => Factorial(n1) / (Factorial(n2) * Factorial(n1 - n2));
        
    public static float InverseSqrtFast(float x)
    {
        float xhalf = 0.5f * x;
        uint i = (uint) x;
        i = 1597463007 - (i >> 1);
        x = (float)i;
        x = x * (1.5f - (xhalf * x * x));
        return x;
    } 

    public static double InverseSqrtFast(double x)
    {
        double xhalf = 0.5 * x;
        uint i = (uint) x;
        i = 1597463007 - (i >> 1);
        x = (double)i;
        x = x *(1.5f - (xhalf * x * x));
        return x;
    }

    public static float DegreesToRadians(float degrees)
        => degrees * (float)(MathF.PI / 180.0f);
    
    public static float RadiansToDegrees(float radians)
        => radians * (float)(180.0f / MathF.PI);
    
    public static double DegreesToRadians(double degrees)
        => degrees * (double)(System.Math.PI / 180.0);
    
    public static double RadiansToDegrees(double radians)
        => radians * (double)(180.0 / System.Math.PI);

    public static void Swap<T>(ref T a, ref T b) 
        => (a, b) = (b, a);

    public static int Clamp(int n, int min, int max)
        => System.Math.Max(System.Math.Min(n, max), min);
        
    public static float Clamp(float n, float min, float max)
        => System.Math.Max(System.Math.Min(n, max), min);

    public static double Clamp(double n, double min, double max)
        => System.Math.Max(System.Math.Min(n, max), min);
    
    public static int MapRange(int value, int valueMin, int valueMax, int resultMin, int resultMax)
        => resultMin + ((resultMax - resultMin) * ((value - valueMin) / (valueMax - valueMin)));
    
    public static float MapRange(float value, float valueMin, float valueMax, float resultMin, float resultMax)
        => resultMin + ((float)(resultMax - resultMin) * ((value - valueMin) / (float)(valueMax - valueMin)));
    
    public static double MapRange(double value, double valueMin, double valueMax, double resultMin, double resultMax)
        => resultMin + ((double)(resultMax - resultMin) * ((value - valueMin) / (double)(valueMax - valueMin)));
    
    public static bool ApproximatelyEqual(float a, float b, int maxDeltaBits)
    {
        long k = BitConverter.SingleToInt32Bits(value: a);
        if (k < 0) k = int.MinValue - k;
        long l = BitConverter.SingleToInt32Bits(value: b);
        if (l < 0) l = int.MinValue - l;

        return System.Math.Abs(value: k - l) <= 1 << maxDeltaBits;
    }

    public static bool ApproximatelyEqualEpsilon(double a, double b, double epsilon)
    {
        if (a == b) return true;

        const double doubleNormal = (1L << 52) * double.Epsilon;
        if (a == 0.0f || b == 0.0f || (double)System.Math.Abs(value: a - b) < doubleNormal)
            return (double)System.Math.Abs(value: a - b) < epsilon * doubleNormal;

        var relativeError = (double)System.Math.Abs(value: a - b) 
            / System.Math.Min(val1: (double)System.Math.Abs(value: a) + (double)System.Math.Abs(value: b),
            val2: double.MaxValue);

        return relativeError < epsilon;
    }

    public static bool ApproximatelyEqualEpsilon(float a, float b, float epsilon)
    {

        if (a == b) return true;

        const float floatNormal = (1 << 23) * float.Epsilon;
        if (a == 0.0f || b == 0.0f || (float)System.Math.Abs(value: a - b) < floatNormal)
            return (float)System.Math.Abs(value: a - b) < epsilon * floatNormal;

        var relativeError = (float)System.Math.Abs(value: a - b) 
            / System.Math.Min(val1: (float)System.Math.Abs(value: a) + (float)System.Math.Abs(value: b),
            val2: float.MaxValue);

        return relativeError < epsilon;
    }

    public static bool ApproximatelyEquivalent(float a, float b, float tolerance)
    {
        if (a == b) return true;
        return (float)System.Math.Abs(value: a - b) <= tolerance;

    }

    public static bool ApproximatelyEquivalent(double a, double b, double tolerance)
    {
        if (a == b) return true;
        return (double)System.Math.Abs(value: a - b) <= tolerance;
    }

    public static float Lerp(float start, float end, float t)
        => start + (System.Math.Clamp(value: t, min: 0, max: 1) * (end - start));
    
    public static double Lerp(double start, double end, double t)
        => start + (System.Math.Clamp(value: t, min: 0, max: 1) * (end - start));
        
    public static float NormalizeAngle(float angle)
    {
        if (ClampAngle(angle: angle) > 180f) angle -= 360f;
        return angle;

    }

    public static double NormalizeAngle(double angle)
    {
        if (ClampAngle(angle: angle) > 180f) angle -= 360f;
        return angle;

    }

    public static float NormalizeRadians(float angle)
    {
        if (ClampRadians(angle: angle) > pi) angle -= 2 * pi;
        return angle;
    }

    public static double NormalizeRadians(double angle)
    {
        if (ClampRadians(angle: angle) > pi) angle -= 2 * pi;
        return angle;
    }

    public static float ClampAngle(float angle)
    {
        angle %= 360f;

        if (angle < 0.0f) angle += 360f;
        return angle;

    }

    public static double ClampAngle(double angle)
    {
        angle %= 360d;

        if (angle < 0.0d) angle += 360d;
        return angle;
    }

    public static float ClampRadians(float angle)
    {
        angle %= two_pi;

        if (angle < 0.0f) angle += two_pi;
        return angle;

    }

    public static double ClampRadians(double angle)
    {
        angle %= 2d * System.Math.PI;
        if (angle < 0.0d) angle += 2d * System.Math.PI;

        return angle;
    }

    internal static readonly string ListSeparator 
        = CultureInfo.CurrentCulture.TextInfo.ListSeparator;


}