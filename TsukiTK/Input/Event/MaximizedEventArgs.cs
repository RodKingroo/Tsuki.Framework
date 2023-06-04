namespace Tsuki.Framework.Input.Event;

public struct MaximizedEventArgs
{
    public bool IsMaximized { get; }

    public MaximizedEventArgs(bool isMaximized)
    {
        IsMaximized = isMaximized;
    }
}