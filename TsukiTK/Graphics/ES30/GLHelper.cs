using System.Diagnostics;
using Tsuki.Framework.Context;

namespace Tsuki.Framework.Graphics.ES30;

public sealed partial class GL : BindingsBase
{
#if IPHONE
    public const string Library = "/System/Library/Frameworks/OpenGLES.framework/OpenGLES";
#else
    public const string Library = "libGLESv2.dll";
#endif

    private static nint[]? EntryPoints;
    private static string[]? EntryPointNames;

    public static void LoadBindings(IBindingsContext context)
    {
        Debug.Print("Loading entry points for {0}", typeof(GL).FullName);

        if (context == null)
            throw new ArgumentNullException(nameof(context));

        for (int i = 0; i < EntryPoints!.Length; i++)
            EntryPoints[i] = context.GetProcAddress(EntryPointNames![i]);
    }
    
}