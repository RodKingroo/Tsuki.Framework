namespace Tsuki.Framework.Input.Event;

public struct MinimizedEventArgs
{
    public bool IsMinimized { get; }

    public MinimizedEventArgs(bool isMinimized)
    {
        IsMinimized = isMinimized;
    }
}