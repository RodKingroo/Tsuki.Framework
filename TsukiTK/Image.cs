namespace Tsuki.Framework;

public class Image
{
    public int Width { get; }
    public int Height { get; }
    public byte[]? Data { get; }

    public Image(int width, int height, byte[] data)
    {
        Width = width;
        Height = height;
        Data = data;
    }

    protected Image()
    {
                
    }
}