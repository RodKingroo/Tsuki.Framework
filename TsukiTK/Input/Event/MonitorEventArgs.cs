namespace Tsuki.Framework.Input.Event;

public struct MonitorEventArgs
{
    public MonitorHandle Monitor { get; }
    public bool IsConnected { get; }

    public MonitorEventArgs(MonitorHandle monitor, bool isConnected)
    {
        Monitor = monitor;
        IsConnected = isConnected;
    }
}