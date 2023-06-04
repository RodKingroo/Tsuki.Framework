using Tsuki.Framework.Graphics.EGL.API;
using Tsuki.Framework.Input;

namespace Tsuki.Framework.Platform.GLFW;

public static unsafe class GLFWCallbacks
{
    public delegate void CharCallback(Window* window, uint codepoint);
    public delegate void CharModsCallback(Window* window, uint codepoint, KeyModifiers modifiers);
    public delegate void CursorEnterCallback(Window* window, bool entered);
    public delegate void CursorPosCallback(Window* window, double x, double y);
    public delegate void DropCallback(Window* window, int count, byte** paths);
    public delegate void JoystickCallback(int joystick, ConnectedState state);
    public delegate void KeyCallback(Window* window, Keys key, int scanCode, InputAction action, KeyModifiers mods);
    public delegate void MouseButtonCallback(Window* window, MouseButton button, InputAction action, KeyModifiers mods);
    public delegate void ScrollCallback(Window* window, double offsetX, double offsetY);
    public delegate void MonitorCallback(Monitor* monitor, ConnectedState state);
    public delegate void WindowCloseCallback(Window* window);
    public delegate void WindowFocusCallback(Window* window, bool focused);
    public delegate void WindowIconifyCallback(Window* window, bool iconified);
    public delegate void WindowMaximizeCallback(Window* window, bool maximized);
    public delegate void FramebufferSizeCallback(Window* window, int width, int height);
    public delegate void WindowContentScaleCallback(Window* window, double xscale, double yscale);
    public delegate void WindowPosCallback(Window* window, int x, int y);
    public delegate void WindowSizeCallback(Window* window, int width, int height);
    public delegate void ErrorCallback(ErrorCode error, string description);
    public delegate void WindowRefreshCallback(Window* window);


}