using System.Runtime.InteropServices;

namespace Tsuki.Framework.Graphics;

public abstract class BindingsBase
{
    public BindingsBase()
    {
    }

    public static string MarshalPtrToString(nint ptr)
    {
        Est.MarshalPtrToString(ptr, out string result);
        return result;
    }

    public static PinnedByteArray? MarshalStringToPtr(string str)
    {
        Est.MarshalStringToPtr(str, out PinnedByteArray? result);
        return result;
    }

    public static void FreeStringPtr(nint ptr)
        => Marshal.FreeHGlobal(ptr);

    public static nint MarshalStringArrayToPtr(string[] str_array)
    {
        Est.MarshalStringArrayToPtr(str_array, out nint result);
        return result;
    }

    public static void FreeStringArrayPtr(nint ptr, int length)
    {
        for(int i = 0; i < length; i++)
            Marshal.FreeHGlobal(Marshal.ReadIntPtr(ptr, i * nint.Size));
        Marshal.FreeHGlobal(ptr);
    }

    private static void InitializeDummyEntryPoints(nint[] entryPoints)
    {
        nint ptr = Marshal.GetFunctionPointerForDelegate(UninitializedDelegate);
        for (nint i = 0; i < entryPoints.Length; i++)
            entryPoints[i] = ptr;
    }

    private static Action UninitializedDelegate = Uninitialized;

    private static void Uninitialized()
        => throw new InvalidOperationException("First you need to initialize the OpenGL binding by calling LoadBindings() or by creating a compatible OpenGL window.");

}