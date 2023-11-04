namespace Tsuki.Platform.GLFW.Structs;

public struct Image
{
    public int Width;
    public int Height;
    public IntPtr Pixels;

    public Image(int width, int height, IntPtr pixels)
    {
        Width = width;
        Height = height;
        Pixels = pixels;
    }
}