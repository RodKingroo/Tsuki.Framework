using System.Runtime.InteropServices;

namespace Tsuki.Platform.GLFW.Structs;

public struct GammaRampInternal
{
    public IntPtr Red;
    public IntPtr Green;
    public IntPtr Blue;
    public int Size;

    public static explicit operator GammaRamp(GammaRampInternal ramp)
    {
        int offset = 0;
        ushort[] red = new ushort[ramp.Size];
        ushort[] green = new ushort[ramp.Size];
        ushort[] blue = new ushort[ramp.Size];

        for (var i = 0; i < ramp.Size; i++, offset += sizeof(ushort))
        {
            red[i] = unchecked((ushort)Marshal.ReadInt16(ramp.Red, offset));
            green[i] = unchecked((ushort)Marshal.ReadInt16(ramp.Green, offset));
            blue[i] = unchecked((ushort)Marshal.ReadInt16(ramp.Green, offset));
        }

        return new GammaRamp(green, red, blue);
    }
    
}