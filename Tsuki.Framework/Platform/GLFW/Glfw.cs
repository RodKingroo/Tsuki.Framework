using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Tsuki.Graphics.Khronos.Enums;
using Tsuki.Platform.GLFW.Enums;

using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;
using Window = Tsuki.Platform.GLFW.Structs.Window;
using GamePadState = Tsuki.Platform.GLFW.Structs.GamePadState;
using Image = Tsuki.Platform.GLFW.Structs.Image;
using VideoMode = Tsuki.Platform.GLFW.Structs.VideoMode;
using GammaRamp = Tsuki.Platform.GLFW.Structs.GammaRamp;
using Cursor = Tsuki.Platform.GLFW.Structs.Cursor;
using Profile = Tsuki.Graphics.Khronos.Enums.Profile;

namespace Tsuki.Platform.GLFW;

public static class Glfw
{
#if Windows
    public const string Library = "glfw3.dll";
#else
    public const string Library = "libglfw.so.3";
#endif
    
    static Glfw()
    {
        Init();
        SetErrorCallback(ErrorCallback);
    }

    public static ErrorCode GetError(out string description)
    {
        var code = GetErrorPrivate(out var ptr);
        description = (code == ErrorCode.Unknown ? null : Util.PtrToStringUTF8(ptr))!;
        return code;
    }
    
    [DllImport(Library, EntryPoint = "glfwSetWindowMaximizeCallback")]
    public static extern WindowMaximizedCallback SetWindowMaximizeCallback(Window window, WindowMaximizedCallback cb);
    

    [DllImport(Library, EntryPoint = "glfwSetWindowContentScaleCallback")]
    public static extern WindowContentsScaleCallback SetWindowContentScaleCallback(Window window,
        WindowContentsScaleCallback cb);
    
    [DllImport(Library, EntryPoint = "glfwSetErrorCallback")]
    public static extern ErrorCallback SetErrorCallback(ErrorCallback errorHandler);
    
    [DllImport(Library, EntryPoint = "glfwSetWindowPosCallback")]
    public static extern PositionCallback SetWindowPositionCallback(Window window, PositionCallback positionCallback);

    [DllImport(Library, EntryPoint = "glfwSetWindowSizeCallback")]
    public static extern SizeCallback SetWindowSizeCallback(Window window, SizeCallback sizeCallback);
    
    [DllImport(Library, EntryPoint = "glfwSetWindowFocusCallback")]
    public static extern FocusCallback SetWindowFocusCallback(Window window, FocusCallback focusCallback);
    
    [DllImport(Library, EntryPoint = "glfwGetPrimaryMonitor")]
    public static extern WindowCallback SetCloseCallback(Window window, WindowCallback closeCallback);

    [DllImport(Library, EntryPoint = "glfwSetDropCallback")]
    public static extern FileDropCallback SetDropCallback(Window window, FileDropCallback dropCallback);

    [DllImport(Library, EntryPoint = "glfwSetCursorPosCallback")]
    public static extern MouseCallback SetCursorPositionCallback(Window window, MouseCallback mouseCallback);

    [DllImport(Library, EntryPoint = "glfwSetCursorEnterCallback")]
    public static extern MouseEnterCallback
        SetCursorEnterCallback(Window window, MouseEnterCallback mouseEnterCallback);

    [DllImport(Library, EntryPoint = "glfwSetMouseButtonCallback")]
    public static extern MouseButtonCallback SetMouseButtonCallback(Window window,
        MouseButtonCallback mouseButtonCallback);

    [DllImport(Library, EntryPoint = "glfwSetScrollCallback")]
    public static extern MouseCallback SetScrollCallback(Window window, MouseCallback mouseCallback);

    [DllImport(Library, EntryPoint = "glfwSetCharCallback")]
    public static extern CharCallback SetCharCallback(Window window, CharCallback charCallback);

    [DllImport(Library, EntryPoint = "glfwSetCharModsCallback")]
    public static extern CharModsCallback SetCharModsCallback(Window window, CharModsCallback charCallback);

    [DllImport(Library, EntryPoint = "glfwSetFramebufferSizeCallback")]
    public static extern SizeCallback SetFramebufferSizeCallback(Window window, SizeCallback sizeCallback);

    [DllImport(Library, EntryPoint = "glfwSetWindowRefreshCallback")]
    public static extern WindowCallback SetWindowRefreshCallback(Window window, WindowCallback windowCallback);

    [DllImport(Library, EntryPoint = "glfwSetKeyCallback")]
    public static extern KeyCallback SetKeyCallback(Window window, KeyCallback keyCallback);

    [DllImport(Library, EntryPoint = "glfwSetJoystickCallback")]
    public static extern JoystickCallback SetJoystickCallback(JoystickCallback joystickCallback);

    [DllImport(Library, EntryPoint = "glfwSetMonitorCallback")]
    public static extern MonitorCallback SetMonitorCallback(MonitorCallback monitorCallback);

    [DllImport(Library, EntryPoint = "glfwSetWindowIconifyCallback")]
    public static extern IconifyCallback SetWindowIconifyCallback(Window window, IconifyCallback iconifyCallback);
    

    [DllImport(Library, EntryPoint = "glfwGetMonitorContentScale")]
    public static extern void GetMonitorContentScale(Monitor monitor, out float xScale, out float yScale);
    
    [DllImport(Library, EntryPoint = "glfwSetMonitorUserPointer")]
    public static extern void SetMonitorUserPointer(Monitor monitor, Pointer pointer);

    [DllImport(Library, EntryPoint = "glfwSetWindowOpacity")]
    public static extern void SetWindowOpacity(Window window, float opacity);
    
    [DllImport(Library, EntryPoint = "glfwWindowHintString")]
    public static extern void WindowHintString(Hint hint, byte[] value);

    [DllImport(Library, EntryPoint = "glfwGetWindowContentScale")]
    public static extern void GetWindowContentScale(Window window, out float xScale, out float yScale);

    [DllImport(Library, EntryPoint = "glfwRequestWindowAttention")]
    public static extern void RequestWindowAttention(Window window);
    
    [DllImport(Library, EntryPoint = "glfwSetWindowAttrib")]
    public static extern void SetWindowAttribute(Window window, WindowAttribute attribute, bool value);

    [DllImport(Library, EntryPoint = "glfwSetJoystickUserPointer")]
    public static extern void SetJoystickUserPointer(int joystickId, Pointer pointer);

    [DllImport(Library, EntryPoint = "glfwJoystickIsGamepad")]
    public static extern void JoystickIsGamepad(int joystickId);

    [DllImport(Library, EntryPoint = "glfwGetVersion")]
    public static extern void GetVersion(out int major, out int minor, out int rev);

    [DllImport(Library, EntryPoint = "glfwInitHint")]
    public static extern void InitHint(Hint hint, bool value);
    
    [DllImport(Library, EntryPoint = "glfwTerminate")]
    public static extern void Terminate();
    
    [DllImport(Library, EntryPoint = "glfwDestroyWindow")]
    public static extern void DestroyWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwShowWindow")]
    public static extern void ShowWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwHideWindow")]
    public static extern void HideWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwGetWindowPos")]
    public static extern void GetWindowPosition(Window window, out int x, out int y);

    [DllImport(Library, EntryPoint = "glfwSetWindowPos")]
    public static extern void SetWindowPosition(Window window, int x, int y);

    [DllImport(Library, EntryPoint = "glfwGetWindowSize")]
    public static extern void GetWindowSize(Window window, out int width, out int height);

    [DllImport(Library, EntryPoint = "glfwSetWindowSize")]
    public static extern void SetWindowSize(Window window, int width, int height);

    [DllImport(Library, EntryPoint = "glfwGetFramebufferSize")]
    public static extern void GetFramebufferSize(Window window, out int width, out int height);

    [DllImport(Library, EntryPoint = "glfwFocusWindow")]
    public static extern void FocusWindow(Window window);
    
    [DllImport(Library, EntryPoint = "glfwGetWindowFrameSize")]
    public static extern void GetWindowFrameSize(Window window, out int left, out int top, out int right,
        out int bottom);

    [DllImport(Library, EntryPoint = "glfwMaximizeWindow")]
    public static extern void MaximizeWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwIconifyWindow")]
    public static extern void IconifyWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwRestoreWindow")]
    public static extern void RestoreWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwMakeContextCurrent")]
    public static extern void MakeContextCurrent(Window window);

    [DllImport(Library, EntryPoint = "glfwSwapBuffers")]
    public static extern void SwapBuffers(Window window);

    [DllImport(Library, EntryPoint = "glfwSwapInterval")]
    public static extern void SwapInterval(int interval);

    [DllImport(Library, EntryPoint = "glfwDefaultWindowHints")]
    public static extern void DefaultWindowHints();
    
    [DllImport(Library, EntryPoint = "glfwSetWindowShouldClose")]
    public static extern void SetWindowShouldClose(Window window, bool close);

    [DllImport(Library, EntryPoint = "glfwSetWindowIcon")]
    public static extern void SetWindowIcon(Window window, int count, Image[] images);

    [DllImport(Library, EntryPoint = "glfwWaitEvents")]
    public static extern void WaitEvents();

    [DllImport(Library, EntryPoint = "glfwPollEvents")]
    public static extern void PollEvents();

    [DllImport(Library, EntryPoint = "glfwPostEmptyEvent")]
    public static extern void PostEmptyEvent();

    [DllImport(Library, EntryPoint = "glfwWaitEventsTimeout")]
    public static extern void WaitEventsTimeout(float timeout);
    
    [DllImport(Library, EntryPoint = "glfwSetWindowMonitor")]
    public static extern void SetWindowMonitor(Window window, Monitor monitor, int x, int y, int width, int height,
        int refreshRate);

    [DllImport(Library, EntryPoint = "glfwSetGammaRamp")]
    public static extern void SetGammaRamp(Monitor monitor, GammaRamp ramp);

    [DllImport(Library, EntryPoint = "glfwSetGamma")]
    public static extern void SetGamma(Monitor monitor, float gamma);

    [DllImport(Library, EntryPoint = "glfwDestroyCursor")]
    public static extern void DestroyCursor(Cursor cursor);

    [DllImport(Library, EntryPoint = "glfwSetCursor")]
    public static extern void SetCursor(Window window, Cursor cursor);

    [DllImport(Library, EntryPoint = "glfwGetCursorPos")]
    public static extern void GetCursorPosition(Window window, out float x, out float y);

    [DllImport(Library, EntryPoint = "glfwSetCursorPos")]
    public static extern void SetCursorPosition(Window window, float x, float y);

    [DllImport(Library, EntryPoint = "glfwSetWindowUserPointer")]
    public static extern void SetWindowUserPointer(Window window, Pointer pointer);

    [DllImport(Library, EntryPoint = "glfwSetWindowSizeLimits")]
    public static extern void SetWindowSizeLimits(Window window, int minWidth, int minHeight, int maxWidth,
        int maxHeight);

    [DllImport(Library, EntryPoint = "glfwSetWindowAspectRatio")]
    public static extern void SetWindowAspectRatio(Window window, int numerator, int denominator);

    [DllImport(Library, EntryPoint = "glfwGetMonitorPhysicalSize")]
    public static extern void GetMonitorPhysicalSize(Monitor monitor, out int width, out int height);

    [DllImport(Library, EntryPoint = "glfwGetMonitorPos")]
    public static extern void GetMonitorPosition(Monitor monitor, out int x, out int y);

    [DllImport(Library, EntryPoint = "glfwSetInputMode")]
    public static extern void SetInputMode(Window window, InputMode mode, int value);

    [DllImport(Library, EntryPoint = "glfwGetMonitorWorkarea")]
    public static extern void GetMonitorWorkArea(IntPtr monitor, out int x, out int y, out int width, out int height);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(Hint hint, int value);

    [DllImport(Library, EntryPoint = "glfwGetMonitorUserPointer")]
    public static extern Monitor GetMonitorUserPointer(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetWindowOpacity")]
    public static extern float GetWindowOpacity(Window window);

    [DllImport(Library, EntryPoint = "glfwRawMouseMotionSupported")]
    public static extern bool RawMouseMotionSupported();

    [DllImport(Library, EntryPoint = "glfwGetKeyScancode")]
    public static extern int GetKeyScanCode(Keys key);

    [DllImport(Library, EntryPoint = "glfwGetJoystickHats")]
    public static extern Hat GetJoystickHats(int joystickId, out int count);

    [DllImport(Library, EntryPoint = "glfwJoystickUserPointer")]
    public static extern IntPtr GetJoystickUserPointer(int joystickId);

    [DllImport(Library, EntryPoint = "glfwGetGamepadState")]
    public static extern bool GetGamepadState(int id, out GamePadState state);

    [DllImport(Library, EntryPoint = "glfwInit")]
    public static extern bool Init();

    [DllImport(Library, EntryPoint = "glfwWindowShouldClose")]
    public static extern bool WindowShouldClose(Window window);

    [DllImport(Library, EntryPoint = "glfwGetWindowMonitor")]
    public static extern Monitor GetWindowMonitor(Window window);

    [DllImport(Library, EntryPoint = "glfwGetGammaRamp")]
    public static extern IntPtr GetGammaRampInternal(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwCreateCursor")]
    public static extern Cursor CreateCursor(Image image, int xHotspot, int yHotspot);

    [DllImport(Library, EntryPoint = "glfwCreateStandardCursor")]
    public static extern Cursor CreateStandardCursor(CursorType type);

    [DllImport(Library, EntryPoint = "glfwGetMouseButton")]
    public static extern InputState GetMouseButton(Window window, MouseButton button);

    [DllImport(Library, EntryPoint = "glfwGetWindowUserPointer")]
    public static extern Pointer GetWindowUserPointer(Window window);

    [DllImport(Library, EntryPoint = "glfwGetKey")]
    public static extern InputState GetKey(Window window, Keys key);

    [DllImport(Library, EntryPoint = "glfwJoystickPresent")]
    public static extern bool JoystickPresent(Joystick joystick);

    [DllImport(Library, EntryPoint = "glfwGetInputMode")]
    public static extern int GetInputMode(Window window, InputMode mode);
    
    [DllImport(Library, EntryPoint = "glfwGetJoystickGUID")]
    static private extern IntPtr GetJoystickGuidPrivate(int joystickId);

    [DllImport(Library, EntryPoint = "glfwUpdateGamepadMappings")]
    static extern bool UpdateGamepadMappings(byte[] mappings);

    [DllImport(Library, EntryPoint = "glfwGetGamepadName")]
    static extern IntPtr GetGamepadNamePrivate(int gamepadId);

    [DllImport(Library, EntryPoint = "glfwGetCurrentContext")]
    static extern Window GetCurrentContext();

    [DllImport(Library, EntryPoint = "glfwGetPrimaryMonitor")]
    static extern Monitor GetPrimaryMonitor();

    [DllImport(Library, EntryPoint = "glfwGetTime")]
    static extern float GetTime();

    [DllImport(Library, EntryPoint = "glfwSetTime")]
    static extern void SetTime(float time);

    [DllImport(Library, EntryPoint = "glfwGetTimerFrequency")]
    static extern ulong GetTimerFrequency();

    [DllImport(Library, EntryPoint = "glfwGetTimerValue")]
    static extern ulong GetTimerValue();

    [DllImport(Library, EntryPoint = "glfwGetVersionString")]
    static extern IntPtr GetVersionString();
    
    [DllImport(Library, EntryPoint = "glfwCreateWindow")]
    static extern Window CreateWindow(int width, int height, byte[] title, Monitor monitor, Window share);

    [DllImport(Library, EntryPoint = "glfwSetWindowTitle")]
    static extern void SetWindowTitle(Window window, byte[] title);
    
    [DllImport(Library, EntryPoint = "glfwExtensionSupported")]
    static extern bool GetExtensionSupported(byte[] extension);

    [DllImport(Library, EntryPoint = "glfwGetVideoMode")]
    static extern IntPtr GetVideoModeInternal(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetVideoModes")]
    static extern IntPtr GetVideoModes(Monitor monitor, out int count);
    
    [DllImport(Library, EntryPoint = "glfwGetClipboardString")]
    static extern IntPtr GetClipboardStringInternal(Window window);

    [DllImport(Library, EntryPoint = "glfwSetClipboardString")]
    static extern void SetClipboardString(Window window, byte[] bytes);
    
    [DllImport(Library, EntryPoint = "glfwGetMonitorName")]
    static extern IntPtr GetMonitorNameInternal(Monitor monitor);
    
    [DllImport(Library, EntryPoint = "glfwGetMonitors")]
    static extern IntPtr GetMonitors(out int count);

    [DllImport(Library, EntryPoint = "glfwGetKeyName")]
    static extern IntPtr GetKeyNameInternal(Keys key, int scancode);
    
    [DllImport(Library, EntryPoint = "glfwGetJoystickName")]
    static extern IntPtr GetJoystickNameInternal(Joystick joystick);

    [DllImport(Library, EntryPoint = "glfwGetJoystickAxes")]
    static extern IntPtr GetJoystickAxes(Joystick joystick, out int count);

    [DllImport(Library, EntryPoint = "glfwGetJoystickButtons")]
    static extern IntPtr GetJoystickButtons(Joystick joystick, out int count);
    
    [DllImport(Library, EntryPoint = "glfwGetProcAddress")]
    static extern IntPtr GetProcAddress(byte[] procName);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    static extern int GetWindowAttribute(Window window, int attribute);

    [DllImport(Library, EntryPoint = "glfwGetError")]
    static extern ErrorCode GetErrorPrivate(out IntPtr description);
    
    [Obsolete("Obsolete")]
    public static Hat GetJoystickHats(int joystickId)
    {
        Hat hat = Hat.Centered;
        Hat ptr = GetJoystickHats(joystickId, out var count);

        for (var i = 0; i < count; i++)
        {
            byte value = Marshal.ReadByte(ptr, i);
            hat |= (Hat)value;
        }

        return hat;
    }

    public static string GetJoystickGuid(int joystickId)
    {
        var ptr = GetJoystickGuidPrivate(joystickId);
        return (ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr))!;
    }

    public static bool UpdateGamepadMappings(string mappings)
    {
        return UpdateGamepadMappings(Encoding.ASCII.GetBytes(mappings));
    }

    public static string GetGamepadName(int gamepadId)
    {
        var ptr = GetGamepadNamePrivate(gamepadId);
        return (ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr))!;
    }

    public static Window CurrentContext => GetCurrentContext();
    public static Monitor PrimaryMonitor => GetPrimaryMonitor();
    public static ulong TimerFrequency => GetTimerFrequency();
    public static ulong TimerValue => GetTimerValue();

    public static void WindowHintStringUtf8(Hint hint, string value)
    {
        WindowHintString(hint, Encoding.UTF8.GetBytes(value));
    }
    
    public static void WindowHintStringAscii(Hint hint, string value)
    {
        WindowHintString(hint, Encoding.ASCII.GetBytes(value));
    }
    
    

    public static Monitor[] Monitors
    {
        get
        {
            var ptr = GetMonitors(out var count);
            Monitor[] monitors = new Monitor[count];
            var offset = 0;

            for (var i = 0; i < count; i++, offset += IntPtr.Size)
            {
                monitors[i] = Marshal.PtrToStructure<Monitor>(ptr + offset);
            }

            return monitors;
        }
    }

    public static Version Version
    {
        get
        {
            GetVersion(out var major, out var minor, out var revision);
            return new Version(major, minor, revision);
        }
    }

    public static string VersionString => Util.PtrToStringUTF8(GetVersionString());

    public static float Time
    {
        get => GetTime();
        set => SetTime(value);
    }

    public static Window CreateWindow(int width, int height, string title, Monitor monitor, Window share)
    {
        return CreateWindow(width, height, Encoding.UTF8.GetBytes(title), monitor, share);
    }

    public static ClientApi GetClientApi(Window window) 
        => (ClientApi)GetWindowAttribute(window, (int)ContextAttributes.ClientApi);
    
    public static string GetClipboardString(Window window)
        => Util.PtrToStringUTF8(GetClipboardStringInternal(window));
    
    public static ContextApi GetContextCreationApi(Window window)
        => (ContextApi)GetWindowAttribute(window, (int)ContextAttributes.ContextCreationApi);
    
    public static Version GetContextVersion(Window window)
    {
        GetContextVersion(window, out int major, out int minor, out int revision);
        return new Version(major, minor, revision);
    }

    static void GetContextVersion(Window window, out int major, out int minor, out int revision)
    {
        major = GetWindowAttribute(window, (int)ContextAttributes.ContextVersionMajor);
        minor = GetWindowAttribute(window, (int)ContextAttributes.ContextVersionMinor);
        revision = GetWindowAttribute(window, (int)ContextAttributes.ContextVersionRevision);
    }

    public static bool GetExtensionSupported(string extension) => 
        GetExtensionSupported(Encoding.ASCII.GetBytes(extension));

    public static GammaRamp GetGammaRamp(Monitor monitor) => 
        Marshal.PtrToStructure<GammaRamp>(GetGammaRampInternal(monitor));

    public static bool GetIsDebugContext(Window window) =>
        GetWindowAttribute(window, (int)ContextAttributes.OpenGlDebugContext) == (int)Constants.True;

    public static bool GetIsForwardCompatible(Window window) =>
        GetWindowAttribute(window, (int)ContextAttributes.OpenGlForwardCompat) == (int)Constants.True;

    public static float[] GetJoystickAxes(Joystick joystick)
    {
        var ptr = GetJoystickAxes(joystick, out var count);
        var axes = new float[count];
        if(count > 0)
            Marshal.Copy(ptr, axes, 0, count);
        return axes;
    }

    public static InputState[] GetJoystickButtons(Joystick joystick)
    {
        var ptr = GetJoystickButtons(joystick, out var count);
        var states = new InputState[count];
        for (var i = 0; i < count; i++)
            states[i] = (InputState)Marshal.ReadByte(ptr, i);
        return states;
    }

    public static string GetJoystickName(Joystick joystick) => 
        Util.PtrToStringUTF8(GetJoystickNameInternal(joystick));

    public static string GetKeyName(Keys key, int scancode) =>
        Util.PtrToStringUTF8(GetKeyNameInternal(key, scancode));

    public static string GetMonitorName(Monitor monitor) => 
        Util.PtrToStringUTF8(GetMonitorNameInternal(monitor));
    
    
    public static IntPtr GetProcAddress(string procName) =>
        GetProcAddress(Encoding.ASCII.GetBytes(procName));

    public static Profile GetProfile(Window window) =>
        (Profile)GetWindowAttribute(window, (int)ContextAttributes.OpenGlProfile);

    public static Robustness GetRobustness(Window window) => 
        (Robustness)GetWindowAttribute(window, (int)ContextAttributes.ContextRobustness);

    public static VideoMode GetVideoMode(Monitor monitor)
    {
        var ptr = GetVideoModeInternal(monitor);
        return Marshal.PtrToStructure<VideoMode>(ptr);
    }

    public static VideoMode[] GetVideoModes(Monitor monitor)
    {
        var pointer = GetVideoModes(monitor, out var count);
        var modes = new VideoMode[count];
        for (var i = 0; i < count; i++, pointer += Marshal.SizeOf<VideoMode>())
            modes[i] = Marshal.PtrToStructure<VideoMode>(pointer);
        return modes;
    }

    public static bool GetWindowAttribute(Window window, WindowAttribute attribute) =>
        GetWindowAttribute(window, (int)attribute) == (int)Constants.True;
    

    public static void SetClipboardString(Window window, string str)
    {
        SetClipboardString(window, Encoding.UTF8.GetBytes(str));
    }

    public static void SetWindowTitle(Window window, string title)
    {
        SetWindowTitle(window, Encoding.UTF8.GetBytes(title));
    }

    public static void WindowHint(Hint hint, bool value)
    {
        WindowHint(hint, (int)(value ? Constants.True : Constants.False));
    }

    public static void WindowHint(Hint hint, ClientApi value)
    {
        WindowHint(hint, (int)value);
    }

    public static void WindowHint(Hint hint, Constants value)
    {
        WindowHint(hint, (int)value);
    }

    public static void WindowHint(Hint hint, ContextApi value)
    {
        WindowHint(hint, (int)value);
    }

    public static void WindowHint(Hint hint, Robustness value)
    {
        WindowHint(hint, (int)value);
    }

    public static void WindowHint(Hint hint, Profile value)
    {
        WindowHint(hint, (int)value);
    }

    public static void WindowHint(Hint hint, ReleaseBehavior value)
    {
        WindowHint(hint, (int)value);
    }
    

    static readonly ErrorCallback ErrorCallback = GlfwError;
    static void GlfwError(ErrorCode code, IntPtr message)
    {
        throw new Exception(Util.PtrToStringUTF8(message));
    }

}