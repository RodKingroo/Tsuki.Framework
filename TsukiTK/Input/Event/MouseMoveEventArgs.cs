using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Input.Event;

public struct MouseMoveEventArgs
{
    public Vector2 Position { get; }
    public Vector2 Delta { get; }

    public MouseMoveEventArgs(Vector2 position, Vector2 delta)
    {
        Position = position;
        Delta = delta;
    }

    public MouseMoveEventArgs(double x, double y, double deltaX, double deltaY)
            : this(new Vector2(x, y), new Vector2(deltaX, deltaY))
    {

    }

    public double X => Position.X;
    public double Y => Position.Y;

    public double DeltaX => Delta.X;
    public double DeltaY => Delta.Y;
}