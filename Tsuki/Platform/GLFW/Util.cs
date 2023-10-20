using System.Runtime.InteropServices;

namespace Tsuki.Platform.GLFW;

public static class Util
{
    public static string PtrToStringUTF8(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr)!;
    }
}