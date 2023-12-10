using System.Runtime.InteropServices;
using System.Text;

namespace Tsuki.Graphics;

public abstract class BindingsBase
{
    protected static string MarshalPtrToString(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero) throw new ArgumentException(null, nameof(ptr));

        unsafe
        {
            var str = (sbyte*)ptr;
            var len = 0;

            while (*str != 0)
            {
                ++len;
                ++str;
            }

            return new string((sbyte*)ptr, 0, len, Encoding.UTF8);
        }
    }

    protected static IntPtr MarshalStringToPtr(string str)
    {
        if (string.IsNullOrEmpty(str)) return IntPtr.Zero;

        var maxCount = Encoding.UTF8.GetMaxByteCount(str.Length) + 1;
        var ptr = Marshal.AllocHGlobal(maxCount);

        if (ptr == IntPtr.Zero) throw new OutOfMemoryException();

        unsafe
        {
            fixed (char* pstr = str)
            {
                var actualCount = Encoding.UTF8.GetBytes(pstr, str.Length, (byte*)ptr, maxCount);
                Marshal.WriteByte(ptr, actualCount, 0);
                return ptr;
            }
        }
    }

    protected static void FreeStringPtr(IntPtr ptr)
    {
        Marshal.FreeHGlobal(ptr);
    }

    protected static IntPtr MarshalStringArrayToPtr(string[]? strArray)
    {
        IntPtr ptr = IntPtr.Zero;

        if (strArray != null && strArray.Length != 0)
        {
            ptr = Marshal.AllocHGlobal(strArray.Length * IntPtr.Size);

            if (ptr == IntPtr.Zero) throw new OutOfMemoryException();

            int i = 0;

            try
            {
                for (i = 0; i < strArray.Length; i++)
                {
                    IntPtr str = MarshalStringToPtr(strArray[i]);
                    Marshal.WriteIntPtr(ptr, i * IntPtr.Size, str);
                }
            }
            catch (OutOfMemoryException)
            {
                for (i = i - 1; i >= 0; --i)
                {
                    Marshal.FreeHGlobal(Marshal.ReadIntPtr(ptr, i * IntPtr.Size));
                }

                Marshal.FreeHGlobal(ptr);
                throw;
            }
        }

        return ptr;
    }

    protected static void FreeStringArrayPtr(IntPtr ptr, int length)
    {
        for (int i = 0; i < length; i++)
        {
            Marshal.FreeHGlobal(Marshal.ReadIntPtr(ptr, i * IntPtr.Size));
        }

        Marshal.FreeHGlobal(ptr);
    }

    static private void Uninitialized()
    {
        throw new InvalidOperationException("You need to initialize the LoadBindings() or creating a compatible OpenGL window.");
    }

    static private readonly Action UninitializedDelegate = Uninitialized;
    static private void InitializeDummyEntryPoints(IList<IntPtr> entryPoints)
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(UninitializedDelegate);

        for (var i = 0; i < entryPoints.Count; i++)
        {
            entryPoints[i] = ptr;
        }
    }
    
    
}