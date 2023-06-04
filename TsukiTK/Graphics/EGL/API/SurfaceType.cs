namespace Tsuki.Framework.Graphics.EGL;

public enum EGL_SurfaceType : int
{
    PBufferBit = EGL.PBufferBit,
    PixmapBit = EGL.PixmapBit,
    WindowBit = EGL.WindowBit,
    VGColorspaceLinearBit = EGL.VGColorspaceLinearBit,
    VGAlphaFormatPreBit = EGL.VGAlphaFormatPreBit,
    MultisampleResolveBoxBit = EGL.MultisampleResolveBoxBit,
    SwapBehaviorPreservedBit = EGL.SwapBehaviorPreservedBit,
}