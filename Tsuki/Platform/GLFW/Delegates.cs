using Tsuki.Platform.GLFW.Enums;
using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;
using Window = Tsuki.Platform.GLFW.Structs.Window;

namespace Tsuki.Platform.GLFW;

public delegate void ErrorCallback(ErrorCode code, IntPtr message);
public delegate void SizeCallback(Window window, int width, int height);
public delegate void PositionCallback(Window window, float x, float y);
public delegate void FocusCallback(Window window, bool focused);
public delegate void WindowCallback(Window window);
public delegate void FileDropCallback(Window window, int count, IntPtr arrayPtr);
public delegate void MouseCallback(Window window, float x, float y);
public delegate void MouseEnterCallback(Window window, bool entered);
public delegate void MouseButtonCallback(Window window, MouseButton button, InputState state, ModifierKeys modifiers);
public delegate void CharCallback(Window window, uint codePoint);
public delegate void CharModsCallback(Window window, uint codePoint, ModifierKeys modifiers);
public delegate void KeyCallback(Window window, Keys key, int scanCode, InputState state, ModifierKeys modifiers);
public delegate void JoystickCallback(Joystick joystick, ConnectionStatus status);
public delegate void MonitorCallback(Monitor monitor, ConnectionStatus status);
public delegate void IconifyCallback(Window window, bool focusing);
public delegate void WindowContentsScaleCallback(Window window, float xScale, float yScale);
public delegate void WindowMaximizedCallback(Window window, bool maximized);