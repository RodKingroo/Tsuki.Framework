using System.Drawing;

namespace Tsuki.Platform.GLFW.Args;

public class MouseMoveEventArgs : EventArgs
{
    public float X { get; }
    public float Y { get; }

    public MouseMoveEventArgs(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Point Position => new Point((int)X, (int)Y);

}