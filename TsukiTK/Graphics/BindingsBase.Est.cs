using System.Runtime.InteropServices;
using System.Text;

namespace Tsuki.Framework.Graphics;

internal partial class Est
{
    private static Encoding CurrentEncoding = Encoding.ASCII;
    
    internal static void MarshalPtrToString(nint ptr, out string result)
    {
        if(CurrentEncoding == Encoding.UTF8)
            result = Marshal.PtrToStringUTF8(ptr)!;
        else if(CurrentEncoding == Encoding.Unicode)
            result = Marshal.PtrToStringUni(ptr)!;
        else 
            result = Marshal.PtrToStringAnsi(ptr)!;
    }

    public static void MarshalStringToPtr(string str, out PinnedByteArray? result)
    {
        if (str == null) result = null;

		byte[] bytes = CurrentEncoding.GetBytes(str + '\0');
		result = new PinnedByteArray(bytes);
    }

    public static void MarshalStringArrayToPtr(string[] str_array, out nint result)
    {
        nint ptr = nint.Zero;
        if(str_array != null && str_array.Length != 0)
        {
            ptr = Marshal.AllocHGlobal(str_array.Length * nint.Size);
            int i = 0;
            try
            {
                for(i = 0; i < str_array.Length; i++)
                {
                    nint str = BindingsBase.MarshalStringToPtr(str_array[i])!;
                    Marshal.WriteIntPtr(ptr, i * nint.Size, str);
                }
            }
            catch(OutOfMemoryException)
            {
                for(i = i - 1; i>=0; --i)
                    Marshal.FreeHGlobal(Marshal.ReadIntPtr(ptr, i * IntPtr.Size));
                Marshal.FreeHGlobal(ptr);
            }
        }
        result = ptr;
    }
}