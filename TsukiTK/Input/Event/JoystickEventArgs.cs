namespace Tsuki.Framework.Input.Event;

public struct JoystickEventArgs
{
    public int JoystickId { get; }
    public bool IsConnected { get; }
    
    public JoystickEventArgs(int joystickId, bool isConnected)
    {
        JoystickId = joystickId;
        IsConnected = isConnected;
    }
}