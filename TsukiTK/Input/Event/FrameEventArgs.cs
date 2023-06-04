namespace Tsuki.Framework.Input.Event;

public struct FrameEventArgs
{
    public double Time { get; }
    public FrameEventArgs(double elapsed)
    {
        Time = elapsed;
    }
}