namespace Tsuki.Platform.GLFW.Args;

public class MaximizeEventArgs : EventArgs
{
    public bool IsMaximized { get; }
    public MaximizeEventArgs(bool maximized)
    {
        IsMaximized = maximized;
    }

}