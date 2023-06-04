using System.Diagnostics;
using Tsuki.Framework.Context;

namespace Tsuki.Framework.Graphics.WGL;

public partial class WGL
{
    private static nint[]? EntryPoints;
    private static string[]? EntryPointNames;

    public static void LoadBindings(IBindingsContext context)
    {
        Debug.Print($"Load entry points for {typeof(WGL).FullName}");

        if(context == null)
            throw new ArgumentNullException(nameof(context));

        for(int i = 0; i < EntryPoints!.Length; ++i)
            EntryPoints[i] = context.GetProcAddress(EntryPointNames![i]);

    }

    public const int DoubleBuffer = 1;
    public const int Stereo = 2;
    public const int DrawToWindow = 4;
    public const int DrawToBitmap = 8;
    public const int SupportGDI = 16;
    public const int SupportOpenGL = 32;
    public const int GenericFormat = 64;
    public const int NeedPalette = 128;
    public const int NeedSystemPalette = 256;
    public const int SwapExchange = 512;
    public const int SwapCopy = 1024;
    public const int SwapLayerBuffers = 2048;
    public const int GenericAccelerated = 4096;
    public const int SupportDirectDraw = 8192;
    public const int SupportComposition = 16384;
    public const byte RGBA = 0;
    public const byte Indexed = 1;

    public const int AccessReadOnly = 0;
    public const int AccessReadWrite = 1;
    public const int AccessWriteDiscard = 2;
    

}