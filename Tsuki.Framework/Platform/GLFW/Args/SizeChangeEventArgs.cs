using System.Drawing;

namespace Tsuki.Platform.GLFW.Args;

public class SizeChangeEventArgs : EventArgs
{
    public Size Size { get; }

    public SizeChangeEventArgs(int width, int height) : this(new Size(width, height))
    {
        
    }

    public SizeChangeEventArgs(Size size)
    {
        Size = size;
    }
}