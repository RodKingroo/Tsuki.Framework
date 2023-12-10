namespace Tsuki.Platform.GLFW.Structs;

public struct GammaRamp
{
    public ushort[] Red;
    public ushort[] Green;
    public ushort[] Blue;

    public readonly uint Size;

    public GammaRamp(ushort[] red, ushort[] green, ushort[] blue)
    {
        if (red.Length != green.Length || green.Length != blue.Length)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Size = (uint)red.Length;
        }
        else
        {
            throw new ArgumentException($"{nameof(red)} and {nameof(green)} and {nameof(blue)} must have the same");
        }
    }

}