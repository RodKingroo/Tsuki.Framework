namespace Tsuki.Framework.Platform;

public unsafe struct Image
{
    public int Width;
    public int Height;
    public byte* Pixels;

    public Image(int width, int height, byte* pixels)
    {
        Width = width;
        Height = height;
        Pixels = pixels;
    }
}