namespace Tsuki.Framework.Graphics.WGL.API;

public struct PixelFormatDescriptor
{
    public short Size;
    public short Version;
    public PixelFormatDescriptorFlags Flags;
    public PixelType PixelType;
    public byte ColorBits;
    public byte RedBits;
    public byte RedShift;
    public byte GreenBits;
    public byte GreenShift;
    public byte BlueBits;
    public byte BlueShift;
    public byte AlphaBits;
    public byte AlphaShift;
    public byte AccumBits;
    public byte AccumRedBits;
    public byte AccumGreenBits;
    public byte AccumBlueBits;
    public byte AccumAlphaBits;
    public byte DepthBits;
    public byte StencilBits;
    public byte AuxBuffers;
    public byte LayerType;
    public byte Reserved;
    public int LayerMask;
    public int VisibleMask;
    public int DamageMask;

}