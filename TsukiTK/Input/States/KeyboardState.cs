using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Tsuki.Framework.Input.Enums;

namespace Tsuki.Framework.Input.States;

public struct KeyboardState : IEquatable<KeyboardState>
{
    private const int IntSize = sizeof(int) * 8;
    private const int NumInts = (int)(Keys.Menu + IntSize - 1) / IntSize;
    
    private unsafe fixed int Key[NumInts];

    public bool this[Keys index]
    {
        get => IsKeyDown(index);
        set => SetKeyState(index, value);
    }

    public bool this[short code] => IsKeyDown((Keys)code);

    internal bool ReadBit(int offset)
    {
        ValidateOffset(offset);

        int int_offset = offset / IntSize;
        int bit_offset = offset % IntSize;
        unsafe
        {
            fixed (int* k = Key)
            { 
                return (*(k + int_offset) & (1 << bit_offset)) != 0; 
            }
        }
    }

    private static void ValidateOffset(int offset)
    {
        switch (offset)
        {
            case < 0:
            case >= NumInts * IntSize:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool IsAnyKeyDown
    {
        get
        {
            unsafe
            {
                fixed(int* k = Key)
                {
                    for(int i = 0; i < NumInts; ++i)
                    {
                        if(k[i] != 0)
                            return true;
                    }
                }
            }

            return false;
        }
    }

    public bool IsKeyDown(Keys index) 
        => ReadBit((int)index);

    public bool IsKeyUp(Keys index)
        => !IsKeyDown(index);

    public bool IsKeyDown(short code)
        => code >= 0 && code < (short)Keys.Menu && ReadBit(code);

    public bool IsKeyUp(short code)
        => !IsKeyDown(code);

    public bool IsConnected { get; set; }

    public void SetKeyState(Keys key, bool down)
    {
        if(down) EnableBit((int)key);
        else DisableBit((int)key);
    }

    public void EnableBit(int offset)
    {
        ValidateOffset(offset);

        int int_offset = offset / IntSize;
        int bit_offset = offset % IntSize;
        unsafe
        {
            fixed (int* k = Key) 
            { 
                *(k + int_offset) |= 1 << bit_offset; 
            }
        }
    }

    public void DisableBit(int offset)
    {
        ValidateOffset(offset);

        int int_offset = offset / IntSize;
        int bit_offset = offset % IntSize;
        unsafe
        {
            fixed (int* k = Key) { *(k + int_offset) &= ~(1 << bit_offset); }
        }
    }

    internal void MergeBits(KeyboardState other)
    {
        unsafe
        {
            int* k2 = other.Key;
            fixed (int* k1 = Key)
            {
                for (int i = 0; i < NumInts; i++)
                    *(k1 + i) |= *(k2 + i);
            }
        }
        IsConnected |= other.IsConnected;
    }
    
    public void SetIsConnected(bool value)
    {
        IsConnected = value;
    }

    public static bool operator ==(KeyboardState left, KeyboardState right)
        => left.Equals(right);

    public static bool operator !=(KeyboardState left, KeyboardState right)
        => !left.Equals(right);

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is KeyboardState && Equals((KeyboardState)obj);

    public bool Equals(KeyboardState other)
    {
        bool equal = true;
        unsafe
        {
            int* k2 = other.Key;
            fixed(int* k1 = Key)
            {
                for (int i = 0; equal && i < NumInts; i++)
                    equal &= *(k1 + i) == *(k2 + i);
            }
        }

        return equal;
    }

    public override string ToString()
    {
        string result = "";
        bool first = true;
        for (Keys index = 0; index <= Keys.Menu; index++)
        {
            if(IsKeyDown(index))
            {
                if (!first)
                    result = string.Format("[{0}{1}]", index, ", ");
                else
                    result = string.Format("[{0}{1}]", index, string.Empty);
            }
        }

        return result;
    }

    public override int GetHashCode()
    {
        unsafe
        {
            fixed (int* k = Key)
            {
                int hashcode = 0;
                for (int i = 0; i < NumInts; i++)
                    hashcode ^= (k + i)->GetHashCode();
                return hashcode;
            }
        }
    }
}