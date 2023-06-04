using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Input.Event;

public struct WindowPositionEventArgs
{
    public Vector2 Position { get; }

    public WindowPositionEventArgs(Vector2 position)
    {
        Position = position;
    }

    public WindowPositionEventArgs(double x, double y)
        : this(new Vector2(x, y))
    {

    }

    public double X => Position.X;
    public double Y => Position.Y;
}