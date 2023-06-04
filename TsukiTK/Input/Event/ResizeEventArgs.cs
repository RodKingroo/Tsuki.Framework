using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Input.Event;

public struct ResizeEventArgs
{
    public Vector2 Size { get; }

    public ResizeEventArgs(Vector2 size)
    {
        Size = size;
    }

    public ResizeEventArgs(double width, double height)
        : this(new Vector2(width, height))
    {
        
    }

    public double Width => Size.X;
    public double Height => Size.Y;
    
}