namespace Tsuki.Framework.Graphics.WGL.API;

[Flags]
public enum PixelFormatDescriptorFlags : int
{
    DoubleBuffer = WGL.DoubleBuffer,
    Stereo = WGL.Stereo,
    DrawToWindow = WGL.DrawToWindow,
    DrawToBitmap = WGL.DrawToBitmap,
    SupportGDI = WGL.SupportGDI,
    SupportOpenGL = WGL.SupportOpenGL,
    GenericFormat = WGL.GenericFormat,
    NeedPalette = WGL.NeedPalette,
    NeedSystemPalette = WGL.NeedSystemPalette,
    SwapExchange = WGL.SwapExchange,
    SwapCopy = WGL.SwapCopy,
    SwapLayerBuffers = WGL.SwapLayerBuffers,
    GenericAccelerated = WGL.GenericAccelerated,
    SupportDirectDraw = WGL.SupportDirectDraw,
    SupportComposition = WGL.SupportComposition
}