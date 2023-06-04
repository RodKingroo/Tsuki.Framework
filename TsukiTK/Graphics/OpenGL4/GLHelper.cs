using System.Diagnostics;
using Tsuki.Framework.Context;

namespace Tsuki.Framework.Graphics.OpenGL4;

public sealed partial class GL : BindingsBase
{    
    private static nint[]? EntryPoints;
    private static string[]? EntryPointNames;

    public static void LoadBindings(IBindingsContext context)
    {
        Debug.Print($"Load entry points for {typeof(GL).FullName}");

        if(context == null)
            throw new ArgumentNullException(nameof(context));

        for(int i = 0; i < EntryPoints!.Length; ++i)
            EntryPoints[i] = context.GetProcAddress(EntryPointNames![i]);

    }
}