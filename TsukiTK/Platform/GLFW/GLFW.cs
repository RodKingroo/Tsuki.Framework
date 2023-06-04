// XML-documentation is from https://www.glfw.org/docs/latest/
// Still missing in documentation
using System.Runtime.InteropServices;
using Tsuki.Framework.Graphics.EGL.API;
using Tsuki.Framework.Input;
using Tsuki.Framework.Input.State;

namespace Tsuki.Framework.Platform.GLFW;

#pragma warning disable CS8500
public static class GLFW
{
    public const double DontCare = -1;
    
    public static bool Init()
        => GLFWNative.Init() == GLFWNative.GLFW_TRUE;

    public static void Terminate()
        => GLFWNative.Terminate();

    public static void InitHint(InitHintBool hintBool, bool value)
    {
        if (value)
            GLFWNative.InitHint((int)hintBool, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.InitHint((int)hintBool, GLFWNative.GLFW_FALSE);
    }

    public static void InitHint(InitHintInt hintInt, int value)
        => GLFWNative.InitHint((int) hintInt, value);

    public static unsafe void GetVersion(out int major, out int minor, out int revision)
    {
        int localMajor;
        int localMinor;
        int localRevision;
        
        GLFWNative.GetVersion(&localMajor, &localMinor, &localRevision);
        
        major = localMajor;
        minor = localMinor;
        revision = localRevision;
    }

    public static unsafe string GetVersionString()
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetVersionString())!;

    public static unsafe byte* GetVersionStringRaw()
        => GLFWNative.GetVersionString();

    public static unsafe ErrorCode GetError(out string description)
    {
        byte* desc;
        ErrorCode code = GLFWNative.GetError(&desc);
        description = Marshal.PtrToStringUTF8((nint)desc)!;
        return code;
    }

    public static unsafe ErrorCode GetErrorRaw(out byte* description)
    {
        byte* desc;
        ErrorCode code = GLFWNative.GetError(&desc);
        description = desc;
        return code;
    }

    public static unsafe Monitor** GetMonitorsRaw(out int count)
    {
        fixed(int* ptr = &count)
            return GLFWNative.GetMonitors(ptr);
    }

    public static unsafe Monitor** GetMonitorRaw(int* count)
        => GLFWNative.GetMonitors(count);

    public static unsafe Monitor*[] GetMonitors()
    {
        Monitor** ptr = GetMonitorsRaw(out var count);

        if (ptr == null) return null!;

        Monitor*[] array = new Monitor*[count];

        for (var i = 0; i < count; i++)
            array[i] = ptr[i];

        return array;
    }

    public static unsafe void GetMonitorPos(Monitor* monitor, out int x, out int y)
    {
        int localX;
        int localY;
        GLFWNative.GetMonitorPos(monitor, &localX, &localY);

        x = localX;
        y = localY;

    }

    public static unsafe void GetMonitorPos(Monitor* monitor, int* x, int* y)
        => GLFWNative.GetMonitorPos(monitor, x, y);

    public static unsafe void GetMonitorWorkarea(Monitor* monitor, out int x, out int y, out int width, out int height)
    {
        int localX;
        int localY;
        int localWidth;
        int localHeight;

        GLFWNative.GetMonitorWorkarea(monitor, &localX, &localY, &localWidth, &localHeight);

        x = localX;
        y = localY;
        width = localWidth;
        height = localHeight;
    }

    public static unsafe void GetMonitorWorkarea(Monitor* monitor, int* x, int* y, int* width, int* height)
        => GLFWNative.GetMonitorWorkarea(monitor, x, y, width, height);

    public static unsafe void GetMonitorPhysicalSize(Monitor* monitor, out int width, out int height)
    {
        int localWidth;
        int localHeight;

        GLFWNative.GetMonitorPhysicalSize(monitor, &localWidth, &localHeight);

        width = localWidth;
        height = localHeight;
        
    }

    public static unsafe void GetMonitorPhysicalSize(Monitor* monitor, int* width, int* height)
        => GLFWNative.GetMonitorPhysicalSize(monitor, width, height);

    public static unsafe void GetMonitorContentScale(Monitor* monitor, out double xScale, out double yScale)
    {
        double localX;
        double localY;

        GLFWNative.GetMonitorContentScale(monitor, &localX, &localY);

        xScale = localX;
        yScale = localY;
    }

    public static unsafe void GetMonitorContentScaleRaw(Monitor* monitor, double* xScale, double* yScale)
        => GLFWNative.GetMonitorContentScale(monitor, xScale, yScale);
    
    public static unsafe string GetMonitorName(Monitor* monitor)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetMonitorName(monitor))!;

    public static unsafe byte* GetMonitorNameRaw(Monitor* monitor)
        => GLFWNative.GetMonitorName(monitor);

    public static unsafe void SetMonitorUserPointer(Monitor* monitor, void* pointer)
        => GLFWNative.SetMonitorUserPointer(monitor, pointer);

    public static unsafe void* GetMonitorUserPointer(Monitor* monitor)
        => GLFW.GetMonitorUserPointer(monitor);

    public static unsafe VideoMode* GetVideoModesRaw(Monitor* monitor, out int count)
    {
        fixed(int* ptr = &count)
            return GLFWNative.GetVideoModes(monitor, ptr);
    }

    public static unsafe VideoMode* GetVideoModesRaw(Monitor* monitor, int* count)
        => GLFWNative.GetVideoModes(monitor, count);

    public static unsafe VideoMode[]? GetVideoModes(Monitor* monitor)
    {
        VideoMode* ptr = GetVideoModesRaw(monitor, out var count);

        if (ptr == null) return null;

        VideoMode[] array = new VideoMode[count];
        for (int i = 0; i < count; i++)
            array[i] = ptr[i];

        return array;
    }

    public static unsafe void SetWindowSizeLimits(Window* window, double minWidth, double minHeight, double maxWidth, double maxHeight)
        => GLFWNative.SetWindowSizeLimits(window, (int)minWidth, (int)minHeight, (int)maxWidth, (int)maxHeight);

    public static unsafe void SetGamma(Monitor* monitor, double gamma)
        => GLFWNative.SetGamma(monitor, gamma);

    public static unsafe GammaRamp* GammaRamp(Monitor* monitor)
        => GLFWNative.GetGammaRamp(monitor);

    public static unsafe void SetGammaRamp(Monitor* monitor, GammaRamp* ramp)
        => GLFWNative.SetGammaRamp(monitor, ramp);

    public static void DefaultWindowHints()
        => GLFWNative.DefaultWindowHints();

    public static unsafe void WindowHint(WindowHintString hint, string value)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(value);

        try
        {
            GLFWNative.WindowHintString((int)hint, (byte*)ptr);
        }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe void WindowHintRaw(WindowHintString hint, byte* value)
        => GLFWNative.WindowHintString((int)hint, value);

    public static unsafe void SetValueSizeLimits(Window* window, int minWidth, int minHeight, int maxWidth, int maxHeight)
        => GLFWNative.SetWindowSizeLimits(window, minWidth, minHeight, maxWidth, maxHeight);

    public static unsafe void SetWindowAspectRatio(Window* window, double numerator, double denomirator)
        => GLFWNative.SetWindowAspectRatio(window, (int)numerator, (int)denomirator);

    public static unsafe void GetWindowFrameSize(Window* window, out int left, out int top, out int right, out int bottom)
    {
        int localLeft;
        int localTop;
        int localRight;
        int localBottom;

        GLFWNative.GetWindowFrameSize(window, &localLeft, &localTop, &localRight, &localBottom);

        left = localLeft;
        top = localTop;
        right = localRight;
        bottom = localBottom;
    }

    public static unsafe void GetWindowContentScale(Window* window, out double xScale, out double yScale)
    {
        double localX;
        double localY;

        GLFWNative.GetWindowContentScale(window, &localX, &localY);

        xScale = localX;
        yScale = localY;
    }

    public static unsafe double GetWindowOpacity(Window* window)
        => GLFWNative.GetWindowOpacity(window);

    public static unsafe void SetWindowOpacity(Window* window, double opacity)
        => GLFWNative.SetWindowOpacity(window, opacity);

    public static unsafe void RequestWindowAttention(Window* window)
        => GLFWNative.RequestWindowAttention(window);

    public static unsafe void SetWindowAttrib(Window* window, WindowAttribute attribute, bool value)
    {
        if (value)
            GLFWNative.SetWindowAttrib(window, attribute, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.SetWindowAttrib(window, attribute, GLFWNative.GLFW_FALSE);

    }

    public static bool RawMouseMotionSupported()
        => GLFWNative.RawMouseMotionSupported() == GLFWNative.GLFW_TRUE;

    public static unsafe string GetKeyName(Keys key, int scanCode)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetKeyName(key, scanCode))!;

    public static unsafe byte* GetKeyNameRaw(Keys key, int scancode)
        => GLFWNative.GetKeyName(key, scancode);

    public static int GetKeyScancode(Keys key)
        => GetKeyScancode(key);

    public static unsafe InputAction GetKey(Window* window, Keys key)
        => GLFWNative.GetKey(window, key);

    public static unsafe InputAction GetMouseButton(Window* window, MouseButton button)
        => GLFWNative.GetMouseButton(window, button);

    public static unsafe void GetCursorPos(Window* window, out double xPos, out double yPos)
    {
        double localX;
        double localY;

        GLFWNative.GetCursorPos(window, &localX, &localY);

        xPos = localX;
        yPos = localY;
    }

    public static unsafe void GetCursorPosRaw(Window* window, double* xPos, double* yPos)
        => GLFWNative.GetCursorPos(window, xPos, yPos);

    public static unsafe void SetCursorPos(Window* window, double xPos, double yPos)
        => GLFWNative.SetCursorPos(window, xPos, yPos);

    public static unsafe Cursor* CreateCursorRaw(Image* image, int xHot, int yHot)
        => GLFWNative.CreateCursor(image, xHot, yHot);

    public static unsafe Cursor* CreateCursor(in Image image, int xHot, int yHot)
    {
        fixed (Image* ptr = &image)
        {
            return CreateCursorRaw(ptr, xHot, yHot);
        }
    }

    public static unsafe Cursor* CreateStandardCursor(CursorShape shape)
        => GLFWNative.CreateStandardCursor(shape);

    public static unsafe void DestroyCursor(Cursor* cursor)
        => GLFWNative.DestroyCursor(cursor);

    public static unsafe void SetCursor(Window* window, Cursor* cursor)
        => GLFWNative.SetCursor(window, cursor);

    public static bool JoystickPresent(int jid)
        => GLFWNative.JoystickPresent(jid) == GLFWNative.GLFW_TRUE;

    public static unsafe double* GetJoystickAxesRaw(int jid, out int count)
    {
        fixed(int* ptr = &count)
        {
            return GetJoystickAxesRaw(jid, ptr);
        }
    }

    public static unsafe double* GetJoystickAxesRaw(int jid, int* count)
        => GLFWNative.GetJoystickAxes(jid, count);

    public static unsafe Span<double> GetJoystickAxes(int jid)
        => new Span<double>(GetJoystickAxesRaw(jid, out int count), count);

    public static unsafe JoystickInputAction* GetJoystickButtonsRaw(int jid, out int count)
    {
        fixed(int* ptr = &count)
        {
            return GetJoystickButtonsRaw(jid, ptr);
        }
    }

    public static unsafe JoystickInputAction* GetJoystickButtonsRaw(int jid, int* count)
        => GLFWNative.GetJoystickButtons(jid, count);

    public static unsafe JoystickInputAction[]? GetJoystickButtons(int jid)
    {
        JoystickInputAction* ptr = GetJoystickButtonsRaw(jid, out int count);

        if (ptr == null) return null;

       JoystickInputAction[] array = new JoystickInputAction[count];
        for (var i = 0; i < count; i++)
            array[i] = ptr[i];

        return array;
    }

    public static unsafe JoystickHats* GetJoystickHatsRaw(int jid, out int count)
    {
        fixed(int* ptr = &count)
        {
            return GLFWNative.GetJoystickHats(jid, ptr);
        }
    }

    public static unsafe JoystickHats* GetJoystickHatsRaw(int jid, int* count)
        => GLFWNative.GetJoystickHats(jid, count);

    public static unsafe JoystickHats[]? GetJoystickHats(int jid)
    {
        JoystickHats* ptr = GetJoystickHatsRaw(jid, out int count);

        if (ptr == null) return null;

        JoystickHats[] array = new JoystickHats[count];
        for (var i = 0; i < count; i++)
            array[i] = ptr[i];

        return array;
    }

    public static unsafe string GetJoystickName(int jid)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetJoystickName(jid))!;

    public static unsafe byte* GetJoystickNameRaw(int jid)
        => GLFWNative.GetJoystickName(jid);

    public static unsafe string GetJoystickGUID(int jid)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetJoystickGUID(jid))!;

    public static unsafe byte* GetJoystickGUIDRaw(int jid)
        => GLFWNative.GetJoystickGUID(jid);

    public static unsafe void SetJoystickUserPointer(int jid, void* ptr)
        => GLFWNative.SetJoystickUserPointer(jid, ptr);

    public static unsafe void* GetJoystickUserPointer(int jid)
        => GLFWNative.GetJoystickUserPointer(jid);

    public static bool JoystickIsGamepad(int jid)
        => GLFWNative.JoystickIsGamepad(jid) == GLFWNative.GLFW_TRUE;

    public static unsafe bool UpdateGamepadMappings(string newMapping)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(newMapping);
        try
        {
            return GLFWNative.UpdateGamepadMappings((byte*)ptr) == GLFWNative.GLFW_TRUE;
        }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe bool UpdateGamepadMappingsRaw(byte* newMapping)
        => GLFWNative.UpdateGamepadMappings(newMapping) == GLFWNative.GLFW_TRUE;

    public static unsafe string GetGamepadName(int jid)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetGamepadName(jid))!;

    public static unsafe byte* GetGamepadNameRaw(int jid)
        => GLFWNative.GetGamepadName(jid);

    public static unsafe bool GetGamepadState(int jid, out GamepadState state)
    {
        fixed (GamepadState* ptr = &state)
        {
            return GLFWNative.GetGamepadState(jid, ptr) == GLFWNative.GLFW_TRUE;
        }
    }

    public static unsafe bool GetFamepadStateRaw(int jid, GamepadState* state)
        => GLFWNative.GetGamepadState(jid, state) == GLFWNative.GLFW_TRUE;

    public static double GetTime()
        => GLFWNative.GetTime();

    public static void SetTime(double time)
        => GLFWNative.SetTime(time);

    public static long GetTimerValue()
        => GLFWNative.GetTimerValue();

    public static long GetTimerFrequency() 
        => GLFWNative.GetTimerFrequency();

    public static unsafe Window* GetCurrentContext() 
        => GLFWNative.GetCurrentContext();
    
    public static unsafe void SwapBuffers(Window* window) 
        => GLFWNative.SwapBuffers(window);

    public static unsafe bool ExtensionSupported(string extensionName)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(extensionName);

        try
        {
            return GLFWNative.ExtensionSupported((byte*)ptr) ==  GLFWNative.GLFW_TRUE;
        }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe nint GetProcAddress(string procName)
        => GLFWNative.GetProcAddress(procName);

    public static unsafe nint GetProcAddressRaw(byte* procName)
        => GLFWNative.GetProcAddress(procName);

    public static unsafe bool ExtensionSupportedRaw(byte* extensionName)
        => GLFWNative.ExtensionSupported(extensionName) == GLFWNative.GLFW_TRUE;

    public static unsafe Window* CreateWindow(double width, double height, string title, Monitor* monitor, Window* share)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(title);

        try
        {
            return GLFWNative.CreateWindow((int)width, (int)height, (byte*)ptr, monitor, share);
        }
        finally
        {
            if (ptr != nint.Zero) Marshal.FreeCoTaskMem(ptr);
        }
    }

    public static unsafe Window* CreateWindowRaw(int width, int height, byte* title, Monitor* monitor, Window* share)
        => GLFWNative.CreateWindow(width, height, title, monitor, share);

    public static unsafe void DestroyWindow(Window* window) 
        => GLFWNative.DestroyWindow(window);
    
    public static unsafe void FocusWindow(Window* window) 
        => GLFWNative.FocusWindow(window);

    public static unsafe string GetClipboardString(Window* window)
        => Marshal.PtrToStringUTF8((nint)GLFWNative.GetClipboardString(window))!;

    public static unsafe byte* GetClipboardStringRaw(Window* window)
        => GLFWNative.GetClipboardString(window);

    public static unsafe void GetFramebufferSize(Window* window, out int width, out int height)
    {
        int localWidth;
        int localHeight;

        GLFWNative.GetFramebufferSize(window, &localWidth, &localHeight);

        width = localWidth;
        height = localHeight;
    }

    public static unsafe void GetFramebufferSizeRaw(Window* window, int* width, int* height)
        => GLFWNative.GetFramebufferSize(window, width, height);

    public static unsafe bool GetInputMode(Window* window, StickyAttributes mode)
        => GLFWNative.GetInputMode(window, mode) == GLFWNative.GLFW_TRUE;

    public static unsafe CursorModeValue GetInputMode(Window* window, CursorStateAttribute mode)
        => GLFWNative.GetInputMode(window, mode);

    public static unsafe bool GetInputMode(Window* window, LockKeyModAttribute mode)
        => GLFWNative.GetInputMode(window, mode) == GLFWNative.GLFW_TRUE;

    public static unsafe bool GetInputMode(Window* window, RawMouseMotionAttribute mode)
        => GLFWNative.GetInputMode(window, mode) == GLFWNative.GLFW_TRUE;

    public static unsafe Monitor* GetPrimaryMonitor() 
        => GLFWNative.GetPrimaryMonitor();

    public static unsafe VideoMode* GetVideoMode(Monitor* monitor) 
        => GLFWNative.GetVideoMode(monitor);
    
    public static unsafe bool GetWindowAttrib(Window* window, WindowAttributeGetBool attribute)
        => GLFWNative.GetWindowAttrib(window, attribute) == GLFWNative.GLFW_TRUE;
        
    public static unsafe int GetWindowAttrib(Window* window, WindowAttributeGetInt attribute)
        => GLFWNative.GetWindowAttrib(window, attribute);
    
    public static unsafe ClientApi GetWindowAttrib(Window* window, WindowAttributeGetClientApi attribute)
        => GLFWNative.GetWindowAttrib(window, attribute);
    
    public static unsafe ContextApi GetWindowAttrib(Window* window, WindowAttributeGetContextApi attribute)
        => GLFWNative.GetWindowAttrib(window, attribute);

    public static unsafe OpenGlProfile GetWindowAttrib(Window* window, WindowAttributeGetOpenGlProfile attribute)
        => GLFW.GetWindowAttrib(window, attribute);

    public static unsafe ReleaseBehavior GetWindowAttrib(Window* window, WindowAttributeGetReleaseBehavior attribute)
        => GLFWNative.GetWindowAttrib(window, attribute);

    public static unsafe Robustness GetWindowAttrib(Window* window, WindowAttributeGetRobustness attribute)
        => GLFWNative.GetWindowAttrib(window, attribute);
    
    public static unsafe void SetWindowUserPointer(Window* window, void* pointer)
        => GLFWNative.SetWindowUserPointer(window, pointer);
    
    public static unsafe void* GetWindowUserPointer(Window* window)
        => GLFWNative.GetWindowUserPointer(window);
    
    public static unsafe void GetWindowSize(Window* window, out int width, out int height)
    {
        int localWidth;
        int localHeight;
        
        GLFWNative.GetWindowSize(window, &localWidth, &localHeight);
            
        width = localWidth;
        height = localHeight;
    }

    public static unsafe void GetWindowSizeRaw(Window* window, int* width, int* height)
        => GLFWNative.GetWindowSize(window, width, height);

    public static unsafe void GetWindowPos(Window* window, out int x, out int y)
    {
        int localX;
        int localY;

        GLFWNative.GetWindowPos(window, &localX, &localY);
            
        x = localX;
        y = localY;
    }

    public static unsafe void GetWindowPosRaw(Window* window, int* x, int* y)
        => GLFWNative.GetWindowPos(window, x, y);
    
    public static unsafe Monitor* GetWindowMonitor(Window* window) 
        => GLFWNative.GetWindowMonitor(window);

    public static unsafe void HideWindow(Window* window) 
        => GLFWNative.HideWindow(window);
    
    public static unsafe void IconifyWindow(Window* window) 
        => GLFWNative.IconifyWindow(window);

    public static unsafe void MakeContextCurrent(Window* window) 
        => GLFWNative.MakeContextCurrent(window);

    public static unsafe void MaximizeWindow(Window* window) 
        => GLFWNative.MaximizeWindow(window);

    public static unsafe nint SetWindowMaximizeCallback(Window* window, GLFWCallbacks.WindowMaximizeCallback callback)
        => GLFWNative.SetWindowMaximizeCallback(window, callback);

    public static unsafe nint SetFramebufferSizeCallback(Window* window, GLFWCallbacks.FramebufferSizeCallback callback)
        => GLFWNative.SetFramebufferSizeCallback(window, callback);
    
    public static unsafe nint SetWindowContentScaleCallback(Window* window, GLFWCallbacks.WindowContentScaleCallback callback)
        => GLFWNative.SetWindowContentScaleCallback(window, callback);

    public static void PollEvents() 
        => GLFWNative.PollEvents();

    public static void PostEmptyEvent()
        => GLFWNative.PostEmptyEvent();

    public static unsafe void RestoreWindow(Window* window) 
        => GLFWNative.RestoreWindow(window);

    public static unsafe nint SetCharCallback(Window* window, GLFWCallbacks.CharCallback callback)
        => GLFWNative.SetCharCallback(window, callback);

    public static unsafe nint SetCharModsCallback(Window* window, GLFWCallbacks.CharModsCallback callback)
        => GLFWNative.SetCharModsCallback(window, callback);
    
    public static unsafe void SetClipboardString(Window* window, string data)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(data);

        try { GLFWNative.SetClipboardString(window, (byte*)ptr); }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe void SetClipboardStringRaw(Window* window, byte* data)
        => GLFWNative.SetClipboardString(window, data);

    public static unsafe nint SetCursorEnterCallback(Window* window, GLFWCallbacks.CursorEnterCallback callback)
        => GLFWNative.SetCursorEnterCallback(window, callback);

    public static unsafe nint SetCursorPosCallback(Window* window, GLFWCallbacks.CursorPosCallback callback)
        => GLFWNative.SetCursorPosCallback(window, callback);

    public static unsafe nint SetDropCallback(Window* window, GLFWCallbacks.DropCallback callback)
        => GLFWNative.SetDropCallback(window, callback);

    public static nint SetErrorCallback(GLFWCallbacks.ErrorCallback callback)
        => GLFWNative.SetErrorCallback(callback);

    public static unsafe void SetInputMode(Window* window, CursorStateAttribute mode, CursorModeValue value)
        => GLFWNative.SetInputMode(window, mode, value);

    public static unsafe void SetInputMode(Window* window, StickyAttributes mode, bool value)
    {
        if (value)
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_FALSE);
    }

    public static unsafe void SetInputMode(Window* window, LockKeyModAttribute mode, bool value)
    {
        if (value)
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_FALSE);
    }

    public static unsafe void SetInputMode(Window* window, RawMouseMotionAttribute mode, bool value)
    {
        if (value)
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.SetInputMode(window, mode, GLFWNative.GLFW_FALSE);
    }

    public static nint SetJoystickCallback(GLFWCallbacks.JoystickCallback callback)
        => GLFWNative.SetJoystickCallback(callback);

    public static unsafe nint SetKeyCallback(Window* window, GLFWCallbacks.KeyCallback callback)
        => GLFWNative.SetKeyCallback(window, callback);

    public static unsafe nint SetScrollCallback(Window* window, GLFWCallbacks.ScrollCallback callback)
        => GLFWNative.SetScrollCallback(window, callback);

    public static nint SetMonitorCallback(GLFWCallbacks.MonitorCallback callback)
        => GLFWNative.SetMonitorCallback(callback);

    public static unsafe nint SetMouseButtonCallback(Window* window, GLFWCallbacks.MouseButtonCallback callback)
        => GLFWNative.SetMouseButtonCallback(window, callback);

    public static unsafe nint SetWindowCloseCallback(Window* window, GLFWCallbacks.WindowCloseCallback callback)
        => GLFWNative.SetWindowCloseCallback(window, callback);

    public static unsafe nint SetWindowFocusCallback(Window* window, GLFWCallbacks.WindowFocusCallback callback)
        => GLFWNative.SetWindowFocusCallback(window, callback);

    public static unsafe void SetWindowIcon(Window* window, ReadOnlySpan<Image> images)
    {
        fixed (Image* ptr = images)
        {
            GLFWNative.SetWindowIcon(window, images.Length, ptr);
        }
    }

    public static unsafe void SetWindowIconRaw(Window* window, int count, Image* images)
        => GLFWNative.SetWindowIcon(window, count, images);

    public static unsafe nint SetWindowIconifyCallback(Window* window, GLFWCallbacks.WindowIconifyCallback callback)
        => GLFWNative.SetWindowIconifyCallback(window, callback);

    public static unsafe void SetWindowMonitor(Window* window, Monitor* monitor, double x, double y, double width, double height, int refreshRate)
        => GLFWNative.SetWindowMonitor(window, monitor, (int)x, (int)y, (int)width, (int)height, refreshRate);

    public static unsafe void SetWindowPos(Window* window, double x, double y)
        => GLFWNative.SetWindowPos(window, (int)x, (int)y);
    
    public static unsafe nint SetWindowPosCallback(Window* window, GLFWCallbacks.WindowPosCallback callback)
        => GLFWNative.SetWindowPosCallback(window, callback);

    public static unsafe nint SetWindowRefreshCallback(Window* window, GLFWCallbacks.WindowRefreshCallback callback)
        => GLFWNative.SetWindowRefreshCallback(window, callback);

    public static unsafe void SetWindowSize(Window* window, double width, double height)
        => GLFWNative.SetWindowSize(window, (int)width, (int)height);
    
    public static unsafe nint SetWindowSizeCallback(Window* window, GLFWCallbacks.WindowSizeCallback callback)
        => GLFWNative.SetWindowSizeCallback(window, callback);

    public static unsafe void SetWindowShouldClose(Window* window, bool value)
    {
        if (value)
            GLFWNative.SetWindowShouldClose(window, GLFWNative.GLFW_TRUE);
        else
            GLFWNative.SetWindowShouldClose(window, GLFWNative.GLFW_FALSE);
    }

    public static unsafe void SetWindowTitle(Window* window, string title)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(title);

        try { GLFWNative.SetWindowTitle(window, (byte*)ptr); }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe void SetWindowTitleRaw(Window* window, byte* title)
        => GLFWNative.SetWindowTitle(window, title);

    public static unsafe void ShowWindow(Window* window) 
        => GLFWNative.ShowWindow(window);

    public static void SwapInterval(int interval) 
        => GLFWNative.SwapInterval(interval);

    public static void WaitEvents() 
        => GLFWNative.WaitEvents();

    public static void WaitEventsTimeout(double timeout) 
        => GLFWNative.WaitEventsTimeout(timeout);

    public static void WindowHint(WindowHintInt hint, int value) 
        => GLFWNative.WindowHint(hint, value);
    
    public static void WindowHint(WindowHintBool hint, bool value)
    {
        if (value)
        {
            GLFWNative.WindowHint(hint, GLFWNative.GLFW_TRUE);
        }
        else
        {
            GLFWNative.WindowHint(hint, GLFWNative.GLFW_FALSE);
        }
    }

    public static void WindowHint(WindowHintClientApi hint, ClientApi value) 
        => GLFWNative.WindowHint(hint, value);

    public static void WindowHint(WindowHintReleaseBehavior hint, ReleaseBehavior value)
        => GLFWNative.WindowHint(hint, value);

    public static void WindowHint(WindowHintContextApi hint, ContextApi value)
        => GLFWNative.WindowHint(hint, value);

    public static void WindowHint(WindowHintRobustness hint, Robustness value)
        => GLFWNative.WindowHint(hint, value);

    public static void WindowHint(WindowHintOpenGlProfile hint, OpenGlProfile value)
        => GLFWNative.WindowHint(hint, value);

    public static unsafe bool WindowShouldClose(Window* window) 
        => GLFWNative.WindowShouldClose(window) == GLFWNative.GLFW_TRUE;

    public static bool VulkanSupported() 
        => GLFWNative.VulkanSupported() == GLFWNative.GLFW_TRUE;
    
    public static unsafe byte** GetRequiredInstanceExtensionsRaw(out uint count)
    {
        fixed (uint* ptr = &count)
        {
            return GLFWNative.GetRequiredInstanceExtensions(ptr);
        }
    }

    public static unsafe byte** GetRequiredInstanceExtensionsRaw(uint* count)
        => GLFWNative.GetRequiredInstanceExtensions(count);

    public static unsafe string[] GetRequiredInstanceExtensions()
    {
        byte** ptr = GetRequiredInstanceExtensionsRaw(out uint count);
        string[] array = new string[count];
        for (int i = 0; i < count; i++)
            array[i] = Marshal.PtrToStringUTF8((nint)ptr[i])!;

        return array;
    }

    public static unsafe nint GetInstanceProcAddress(VkHandle instance, string procName)
    {
        nint ptr = Marshal.StringToCoTaskMemUTF8(procName);

        try { return GLFWNative.GetInstanceProcAddress(instance, (byte*)ptr); }
        finally { Marshal.FreeCoTaskMem(ptr); }
    }

    public static unsafe nint GetInstanceProcAddressRaw(VkHandle instance, byte* procName)
        => GLFWNative.GetInstanceProcAddress(instance, procName);

    public static bool GetPhysicalDevicePresentationSupport(VkHandle instance, VkHandle device, int queueFamily)
        => GLFWNative.GetPhysicalDevicePresentationSupport(instance, device, queueFamily) == GLFWNative.GLFW_TRUE;
        
    public static unsafe int CreateWindowSurface(VkHandle instance, Window* window, void* allocator, out VkHandle surface)
        => GLFWNative.CreateWindowSurface(instance, window, allocator, out surface);

    public static unsafe string GetWin32Adapter(Monitor* monitor)
    {
        byte* strPtr = GLFWNative.GetWin32Adapter(monitor);
        string str = Marshal.PtrToStringUTF8((nint)strPtr)!;
        return str;
    }

    public static unsafe string GetWin32Monitor(Monitor* monitor)
    {
        byte* strPtr = GLFWNative.GetWin32Monitor(monitor);
        string str = Marshal.PtrToStringUTF8((nint)strPtr)!;
        return str;
    }

    public static unsafe nint GetWin32Window(Window* window) 
        => GLFWNative.GetWin32Window(window);

    public static unsafe nint GetWGLContext(Window* window) 
        => GLFWNative.GetWGLContext(window);
    
    public static unsafe uint GetCocoaMonitor(Monitor* monitor) 
        => GLFWNative.GetCocoaMonitor(monitor);

    public static unsafe nint GetCocoaWindow(Window* window) 
        => GLFWNative.GetCocoaWindow(window);
    
    public static unsafe nint GetNSGLContext(Window* window) 
        => GLFWNative.GetNSGLContext(window);

    public static unsafe nint GetX11Display() 
        => GLFWNative.GetX11Display();

    public static unsafe nuint GetX11Adapter(Monitor* monitor) 
        => GLFWNative.GetX11Adapter(monitor);
    
    public static unsafe nuint GetX11Monitor(Monitor* monitor) 
        => GLFWNative.GetX11Monitor(monitor);

    public static unsafe nuint GetX11Window(Window* window) 
        => GLFWNative.GetX11Window(window);
    
    public static unsafe void SetX11SelectionString(string @string)
    {
        nint strPtr = Marshal.StringToCoTaskMemUTF8(@string);
        GLFWNative.SetX11SelectionString((byte*)strPtr);
        Marshal.FreeCoTaskMem(strPtr);
    }

    public static unsafe string GetX11SelectionString()
    {
        byte* strPtr = GLFWNative.GetX11SelectionString();
        return Marshal.PtrToStringUTF8((nint)strPtr)!;
    }

    public static unsafe uint GetGLXContext(Window* window)
        => GLFWNative.GetGLXContext(window);

    public static unsafe uint GetGLXWindow(Window* window) 
        => GLFWNative.GetGLXWindow(window);
    
    public static unsafe nint GetWaylandDisplay() 
        => GLFWNative.GetWaylandDisplay();

    public static unsafe nint GetWaylandMonitor(Monitor* monitor) 
        => GLFWNative.GetWaylandMonitor(monitor);

    public static unsafe nint GetWaylandWindow(Window* window) 
        => GLFWNative.GetWaylandWindow(window);
    
    public static unsafe nint GetEGLDisplay() 
        => GLFWNative.GetEGLDisplay();

    public static unsafe nint GetEGLContext(Window* window) 
        => GLFWNative.GetEGLContext(window);
    
    public static unsafe nint GetEGLSurface(Window* window) 
        => GLFWNative.GetEGLSurface(window);

    public static unsafe bool GetOSMesaColorBuffer(Window* window, out int width, out int height, out int format, out nint buffer)
    {
        fixed (int* widthPtr = &width)
        fixed (int* heightPtr = &height)
        fixed (int* formatPtr = &format)
        fixed (nint* bufferPtr = &buffer)
        {
            return GLFWNative.GetOSMesaColorBuffer(window, widthPtr, heightPtr, formatPtr, (void**)bufferPtr) != 0;
        }
    }

    public static unsafe bool GetOSMesaDepthBuffer(Window* window, out int width, out int height, out int bytesPerValue, out nint buffer)
    {
        fixed (int* widthPtr = &width)
        fixed (int* heightPtr = &height)
        fixed (int* bytesPerValuePtr = &bytesPerValue)
        fixed (nint* bufferPtr = &buffer)
        {
            return GLFWNative.GetOSMesaDepthBuffer(window, widthPtr, heightPtr, bytesPerValuePtr, (void**)bufferPtr) != 0;
        }
    }

    public static unsafe nint GetOSMesaContext(Window* window)
        => GLFWNative.GetOSMesaContext(window);

}