using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Input.Event;

public struct MouseWheelEventArgs
{
    public Vector2 Offset { get; }

    public MouseWheelEventArgs(Vector2 offset)
    {
        Offset = offset;
    }

    public MouseWheelEventArgs(double offsetX, double offsetY)
        : this(new Vector2(offsetX, offsetY))
    {
    }

    public double OffsetX => OffsetX;
    public double OffsetY => OffsetY;
}