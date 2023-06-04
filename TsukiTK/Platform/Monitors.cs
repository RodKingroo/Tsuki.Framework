using static Tsuki.Framework.Platform.GLFW.GLFWProvider;
using static Tsuki.Framework.Platform.GLFW.GLFWCallbacks;
using static Tsuki.Framework.Platform.GLFW.GLFW;
using Tsuki.Framework.Input.Event;
using System.Runtime.InteropServices;
using Tsuki.Framework.Graphics.Primitives;

namespace Tsuki.Framework.Platform;

public static class Monitors
{
    private static MonitorCallback _monitorCallback;


    static unsafe Monitors()
    {
        EnsureInitialized();

        _monitorCallback = MonitorCallback;
        SetMonitorCallback(_monitorCallback);
    }

    public static event Action<MonitorEventArgs>? OnMonitorConnected;

    public static int Count => throw new Exception("This property is Obsolete, use GetMonitors() instead.");

    public static double GetPlatformDefaultDpi()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return 72;
        else return 96;
    
    }

    private static int GetRectangleIntersectionArea(Rectangle a, Rectangle b)
    {
        Rectangle area = Rectangle.Intersect(a, b);
        return (int)area.Size.X * (int)area.Size.Y;
    }

    public static unsafe MonitorInfo GetPrimaryMonitor()
    {
        var monitor = new MonitorHandle((nint)GLFW.GLFW.GetPrimaryMonitor());
        return new MonitorInfo(monitor);
    }

    public static unsafe List<MonitorInfo> GetMonitors()
    {
        Monitor** monitorsRaw = GetMonitorsRaw(out int count);

        List<MonitorInfo> monitors = new List<MonitorInfo>(count);

        for (int i = 0; i < count; i++)
            monitors.Add(new MonitorInfo(new MonitorHandle((nint)monitorsRaw[i])));

        return monitors;
    }

    public static unsafe MonitorInfo GetMonitorFromWindow(Window* window)
    {
        MonitorHandle value = new MonitorHandle((nint)GetWindowMonitor(window));
        if (value.Pointer != nint.Zero) return new MonitorInfo(value);

        Rectangle windowArea;
        GetWindowPos(window, out int windowX, out int windowY);
        GetWindowSize(window, out int windowWidth, out int windowHeight);
        windowArea = new Rectangle(windowX, windowY, windowX + windowWidth, windowY + windowHeight);
            
        List<MonitorInfo> monitors = GetMonitors();

        int selectedIndex = 0;
        for (int i = 0; i < monitors.Count; i++)
        {
            if 
            (
                GetRectangleIntersectionArea(monitors[i].ClientArea, windowArea) >
                GetRectangleIntersectionArea(monitors[selectedIndex].ClientArea, windowArea)
            )
            selectedIndex = i;
        }

        return monitors[selectedIndex];
    }

    public static unsafe MonitorInfo GetMonitorFromWindow(NativeWindow window) 
        => GetMonitorFromWindow(window.WindowPtr);

    private static unsafe void MonitorCallback(Monitor* monitor, ConnectedState state)
        => OnMonitorConnected?.Invoke(new MonitorEventArgs(new MonitorHandle((IntPtr)monitor), state == ConnectedState.Connected));

    public static bool TryGetMonitorInfo(int index, out MonitorInfo info) 
        => throw new Exception("This function is Obsolete, use GetMonitors() instead.");
    
    public static bool TryGetMonitorInfo(MonitorHandle monitor, out MonitorInfo info) 
        => throw new Exception("This function is Obsolete, use GetMonitors() instead.");

    public static unsafe bool CheckCache() 
        => throw new Exception("There is no cache anymore, don't call this function.");
    
    public static unsafe void BuildMonitorCache() 
        => throw new Exception("There is no cache anymore, don't call this function.");

    
}