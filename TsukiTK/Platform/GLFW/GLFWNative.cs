using System.Runtime.InteropServices;
using System.Reflection;
using Tsuki.Framework.Graphics.EGL.API;
using Tsuki.Framework.Input;
using Tsuki.Framework.Input.State;

namespace Tsuki.Framework.Platform.GLFW;

public static unsafe class GLFWNative
{
    private const string Library = "glfw3.dll";

    public const int GLFW_TRUE = 1;
    public const int GLFW_FALSE = 0;

    static GLFWNative()
    {
        NativeLibrary.SetDllImportResolver(typeof(GLFWNative).Assembly, (name, assembly, path) =>
        {
            if(name != Library)
                return nint.Zero;

            return LoadLibrary("glfw", new Version(3, 3), assembly, path);
        });
    }
    
    private static nint LoadLibrary(string libraryName, Version version, Assembly assembly, DllImportSearchPath? searchPath)
    {
        IEnumerable<string> GetNextVersion()
        {
            for (var i = 2; i >= 0; i--)
                yield return version.ToString(i);
        }

        Func<string, string, string> libNameFormatter;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            libNameFormatter = (libName, ver) =>
                libName + ".so" + (string.IsNullOrEmpty(ver) ? string.Empty : "." + ver);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            libNameFormatter = (libName, ver) =>
                libName + (string.IsNullOrEmpty(ver) ? string.Empty : "." + ver) + ".dylib";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            libNameFormatter = (libName, ver) =>
                libName + (string.IsNullOrEmpty(ver) ? string.Empty : ver) + ".dll";
        }
        else
        {
            return nint.Zero;
        }

        foreach (var ver in GetNextVersion())
        {
            if (NativeLibrary.TryLoad(libNameFormatter(libraryName, ver), assembly, searchPath, out var handle))
            {
                return handle;
            }
        }
        return NativeLibrary.Load(libraryName, assembly, searchPath);
    }

    #pragma warning disable CS8500

    [DllImport(Library, EntryPoint = "glfwInit")]
    public static extern int Init();

    [DllImport(Library, EntryPoint = "glfwTerninate")]
    public static extern void Terminate();

    [DllImport(Library, EntryPoint = "glfwInitHint")]
    public static extern void InitHint(int hint, int value); 

    [DllImport(Library, EntryPoint = "glfwGetVersion")]
    public static extern void GetVersion(int* major, int* minor, int* revision);


    [DllImport(Library, EntryPoint = "glfwGetVersionString")]
    public static extern byte* GetVersionString();

    [DllImport(Library, EntryPoint = "glfwGetError")]
    public static extern ErrorCode GetError(byte** description);

    [DllImport(Library, EntryPoint = "glfwGetMonitors")]
    public static extern Monitor** GetMonitors(int* count);

    [DllImport(Library, EntryPoint = "glfwGetMonitorPos")]
    public static extern void GetMonitorPos(Monitor* monitor, int* x, int* y);

    [DllImport(Library, EntryPoint = "glfwGetMonitorWorkarea")]
    public static extern void GetMonitorWorkarea(Monitor* monitor, int* xpos, int* ypos, int* width, int* height);

    [DllImport(Library, EntryPoint = "glfwGetMonitorPhysicalSize")]
    public static extern void GetMonitorPhysicalSize(Monitor* monitor, int* width, int* height);
    
    [DllImport(Library, EntryPoint = "glfwGetMonitorContentScale")]
    public static extern void GetMonitorContentScale(Monitor* monitor, double* xscale, double* yscale);

    [DllImport(Library, EntryPoint = "glfwGetMonitorName")]
    public static extern byte* GetMonitorName(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwSetMonitorUserPointer")]
    public static extern void SetMonitorUserPointer(Monitor* monitor, void* pointer);

    [DllImport(Library, EntryPoint = "glfwGetMonitorUserPointer")]
    public static extern void* GetMonitorUserPointer(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetVideoModes")]
    public static extern VideoMode* GetVideoModes(Monitor* monitor, int* count);

    [DllImport(Library, EntryPoint = "glfwSetGamma")]
    public static extern void SetGamma(Monitor* monitor, double gamma);

    [DllImport(Library, EntryPoint = "glfwGetGammaRamp")]
    public static extern GammaRamp* GetGammaRamp(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwSetGammaRamp")]
    public static extern void SetGammaRamp(Monitor* monitor, GammaRamp* ramp);

    [DllImport(Library, EntryPoint = "glfwDefaultWindowHints")]
    public static extern void DefaultWindowHints();

    [DllImport(Library, EntryPoint = "glfwWindowHintString")]
    public static extern void WindowHintString(int hint, byte* value);

    [DllImport(Library, EntryPoint = "glfwSetWindowSizeLimits")]
    public static extern void SetWindowSizeLimits(Window* window, int minwidth, int minheight, int maxwidth, int maxheight);

    [DllImport(Library, EntryPoint = "glfwSetWindowAspectRatio")]
    public static extern void SetWindowAspectRatio(Window* window, int numer, int denom);

    [DllImport(Library, EntryPoint = "glfwGetWindowFrameSize")]
    public static extern void GetWindowFrameSize(Window* window, int* left, int* top, int* right, int* bottom);

    [DllImport(Library, EntryPoint = "glfwGetWindowContentScale")]
    public static extern void GetWindowContentScale(Window* window, double* xscale, double* yscale);

    [DllImport(Library, EntryPoint = "glfwGetWindowOpacity")]
    public static extern double GetWindowOpacity(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetWindowOpacity")]
    public static extern void SetWindowOpacity(Window* window, double opacity);

    [DllImport(Library, EntryPoint = "glfwRequestWindowAttention")]
    public static extern void RequestWindowAttention(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetWindowAttrib")]
    public static extern void SetWindowAttrib(Window* window, WindowAttribute attribute, int value);

    [DllImport(Library, EntryPoint = "glfwRawMouseMotionSupported")]
    public static extern int RawMouseMotionSupported();

    [DllImport(Library, EntryPoint = "glfwGetKeyName")]
    public static extern byte* GetKeyName(Keys key, int scancode);

    [DllImport(Library, EntryPoint = "glfwGetKeyScancode")]
    public static extern int GetKeyScancode(Keys key);

    [DllImport(Library, EntryPoint = "glfwGetKey")]
    public static extern InputAction GetKey(Window* window, Keys key);

    [DllImport(Library, EntryPoint = "glfwGetMouseButton")]
    public static extern InputAction GetMouseButton(Window* window, MouseButton button);

    [DllImport(Library, EntryPoint = "glfwGetCursorPos")]
    public static extern void GetCursorPos(Window* window, double* xpos, double* ypos);

    [DllImport(Library, EntryPoint = "glfwSetCursorPos")]
    public static extern void SetCursorPos(Window* window, double xpos, double ypos);

    [DllImport(Library, EntryPoint = "glfwCreateCursor")]
    public static extern Cursor* CreateCursor(Image* image, int xhot, int yhot);

    [DllImport(Library, EntryPoint = "glfwCreateStandardCursor")]
    public static extern Cursor* CreateStandardCursor(CursorShape shape);

    [DllImport(Library, EntryPoint = "glfwDestroyCursor")]
    public static extern void DestroyCursor(Cursor* cursor);

    [DllImport(Library, EntryPoint = "glfwSetCursor")]
    public static extern void SetCursor(Window* window, Cursor* cursor);

    [DllImport(Library, EntryPoint = "glfwJoystickPresent")]
    public static extern int JoystickPresent(int jid);

    [DllImport(Library, EntryPoint = "glfwGetJoystickAxes")]
    public static extern double* GetJoystickAxes(int jid, int* count);

    [DllImport(Library, EntryPoint = "glfwGetJoystickButtons")]
    public static extern JoystickInputAction* GetJoystickButtons(int jid, int* count);

    [DllImport(Library, EntryPoint = "glfwGetJoystickHats")]
    public static extern JoystickHats* GetJoystickHats(int jid, int* count);

    [DllImport(Library, EntryPoint = "glfwGetJoystickName")]
    public static extern byte* GetJoystickName(int jid);

    [DllImport(Library, EntryPoint = "glfwGetJoystickGUID")]
    public static extern byte* GetJoystickGUID(int jid);

    [DllImport(Library, EntryPoint = "glfwSetJoystickUserPointer")]
    public static extern void SetJoystickUserPointer(int jid, void* ptr);

    [DllImport(Library, EntryPoint = "glfwGetJoystickUserPointer")]
    public static extern void* GetJoystickUserPointer(int jid);

    [DllImport(Library, EntryPoint = "glfwJoystickisGamepad")]
    public static extern int JoystickIsGamepad(int jid);

    [DllImport(Library, EntryPoint = "glfwUpdateGamepadMappings")]
    public static extern int UpdateGamepadMappings(byte* newMapping);

    [DllImport(Library, EntryPoint = "glfwGetGamepadName")]
    public static extern byte* GetGamepadName(int jid);

    [DllImport(Library, EntryPoint = "glfwGetGamepadState")]
    public static extern int GetGamepadState(int jid, GamepadState* state);

    [DllImport(Library, EntryPoint = "glfwGetTime")]
    public static extern double GetTime();

    [DllImport(Library, EntryPoint = "glfwSetTime")]
    public static extern void SetTime(double time);

    [DllImport(Library, EntryPoint = "glfwGetTimerValue")]
    public static extern long GetTimerValue();

    [DllImport(Library, EntryPoint = "glfwGetTimerFrequency")]
    public static extern long GetTimerFrequency();

    [DllImport(Library, EntryPoint = "glfwGetCurrentContext")]
    public static extern Window* GetCurrentContext();

    [DllImport(Library, EntryPoint = "glfwSwapBuffers")]
    public static extern void SwapBuffers(Window* window);

    [DllImport(Library, EntryPoint = "glfwExtensionSupported")]
    public static extern int ExtensionSupported(byte* extensionName);

    [DllImport(Library, EntryPoint = "glfwGetProcAddress")]
    public static extern nint GetProcAddress(byte* procName);

    [DllImport(Library, EntryPoint = "glfwGetProcAddress")]
    public static extern nint GetProcAddress(string procName);

    [DllImport(Library, EntryPoint = "glfwCreateWindow")]
    public static extern Window* CreateWindow(int width, int height, byte* title, Monitor* monitor, Window* share);

    [DllImport(Library, EntryPoint = "glfwGetPrimaryMonitor")]
    public static extern Monitor* GetPrimaryMonitor();

    [DllImport(Library, EntryPoint = "glfwDestroyWindow")]
    public static extern void DestroyWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwFocusWindow")]
    public static extern void FocusWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetFramebufferSize")]
    public static extern void GetFramebufferSize(Window* window, int* width, int* height);

    [DllImport(Library, EntryPoint = "glfwGetInputMode")]
    public static extern CursorModeValue GetInputMode(Window* window, CursorStateAttribute mode);

    [DllImport(Library, EntryPoint = "glfwGetInputMode")]
    public static extern int GetInputMode(Window* window, StickyAttributes mode);

    [DllImport(Library, EntryPoint = "glfwGetInputMode")]
    public static extern int GetInputMode(Window* window, LockKeyModAttribute mode);

    [DllImport(Library, EntryPoint = "glfwGetInputMode")]
    public static extern int GetInputMode(Window* window, RawMouseMotionAttribute mode);

    [DllImport(Library, EntryPoint = "glfwRestoreWindow")]
    public static extern void RestoreWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetVideoMode")]
    public static extern VideoMode* GetVideoMode(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern int GetWindowAttrib(Window* window, WindowAttributeGetBool attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern int GetWindowAttrib(Window* window, WindowAttributeGetInt attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern ClientApi GetWindowAttrib(Window* window, WindowAttributeGetClientApi attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern ContextApi GetWindowAttrib(Window* window, WindowAttributeGetContextApi attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern OpenGlProfile GetWindowAttrib(Window* window, WindowAttributeGetOpenGlProfile attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern ReleaseBehavior GetWindowAttrib(Window* window, WindowAttributeGetReleaseBehavior attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
    public static extern Robustness GetWindowAttrib(Window* window, WindowAttributeGetRobustness attribute);

    [DllImport(Library, EntryPoint = "glfwGetWindowSize")]
    public static extern void GetWindowSize(Window* window, int* width, int* height);

    [DllImport(Library, EntryPoint = "glfwGetWindowPos")]
    public static extern void GetWindowPos(Window* window, int* x, int* y);

    [DllImport(Library, EntryPoint = "glfwGetWindowMonitor")]
    public static extern Monitor* GetWindowMonitor(Window* window);
    
    [DllImport(Library, EntryPoint = "glfwHideWindow")]
    public static extern void HideWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwIconifyWindow")]
    public static extern void IconifyWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwMakeContextCurrent")]
    public static extern void MakeContextCurrent(Window* window);

    [DllImport(Library, EntryPoint = "glfwMaximizeWindow")]
    public static extern void MaximizeWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwPollEvents")]
    public static extern void PollEvents();

    [DllImport(Library, EntryPoint = "glfwPostEmptyEvent")]
    public static extern void PostEmptyEvent();

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintInt hint, int value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintBool hint, int value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintClientApi hint, ClientApi value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintReleaseBehavior hint, ReleaseBehavior value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintContextApi hint, ContextApi value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintRobustness hint, Robustness value);

    [DllImport(Library, EntryPoint = "glfwWindowHint")]
    public static extern void WindowHint(WindowHintOpenGlProfile hint, OpenGlProfile value);

    [DllImport(Library, EntryPoint = "glfwWindowShouldClose")]
    public static extern int WindowShouldClose(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetWindowUserPointer")]
    public static extern void SetWindowUserPointer(Window* window, void* pointer);

    [DllImport(Library, EntryPoint = "glfwGetWindowUserPointer")]
    public static extern void* GetWindowUserPointer(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetCharCallback")]
    public static extern nint SetCharCallback(Window* window, GLFWCallbacks.CharCallback callback);
        
    [DllImport(Library, EntryPoint = "glfwSetCharModsCallback")]
    public static extern nint SetCharModsCallback(Window* window, GLFWCallbacks.CharModsCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetCursorEnterCallback")]
    public static extern nint SetCursorEnterCallback(Window* window, GLFWCallbacks.CursorEnterCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetCursorPosCallback")]
    public static extern nint SetCursorPosCallback(Window* window, GLFWCallbacks.CursorPosCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetDropCallback")]
    public static extern nint SetDropCallback(Window* window, GLFWCallbacks.DropCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetErrorCallback")]
    public static extern nint SetErrorCallback(GLFWCallbacks.ErrorCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetInputMode")]
    public static extern void SetInputMode(Window* window, CursorStateAttribute mode, CursorModeValue value);

    [DllImport(Library, EntryPoint = "glfwSetInputMode")]
    public static extern void SetInputMode(Window* window, StickyAttributes mode, int value);

    [DllImport(Library, EntryPoint = "glfwSetInputMode")]
    public static extern void SetInputMode(Window* window, LockKeyModAttribute mode, int value);

    [DllImport(Library, EntryPoint = "glfwSetInputMode")]
    public static extern void SetInputMode(Window* window, RawMouseMotionAttribute mode, int value);

    [DllImport(Library, EntryPoint = "glfwSetJoystickCallback")]
    public static extern nint SetJoystickCallback(GLFWCallbacks.JoystickCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetKeyCallback")]
    public static extern nint SetKeyCallback(Window* window, GLFWCallbacks.KeyCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetScrollCallback")]
    public static extern nint SetScrollCallback(Window* window, GLFWCallbacks.ScrollCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetMonitorCallback")]
    public static extern nint SetMonitorCallback(GLFWCallbacks.MonitorCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetMouseButtonCallback")]
    public static extern nint SetMouseButtonCallback(Window* window, GLFWCallbacks.MouseButtonCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowCloseCallback")]
    public static extern nint SetWindowCloseCallback(Window* window, GLFWCallbacks.WindowCloseCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowFocusCallback")]
    public static extern nint SetWindowFocusCallback(Window* window, GLFWCallbacks.WindowFocusCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowIcon")]
    public static extern void SetWindowIcon(Window* window, int count, Image* images);

    [DllImport(Library, EntryPoint = "glfwSetWindowIconifyCallback")]
    public static extern nint SetWindowIconifyCallback(Window* window, GLFWCallbacks.WindowIconifyCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowMaximizeCallback")]
    public static extern nint SetWindowMaximizeCallback(Window* window, GLFWCallbacks.WindowMaximizeCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetFramebufferSizeCallback")]
    public static extern nint SetFramebufferSizeCallback(Window* window, GLFWCallbacks.FramebufferSizeCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowContentScaleCallback")]
    public static extern nint SetWindowContentScaleCallback(Window* window, GLFWCallbacks.WindowContentScaleCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowTitle")]
    public static extern void SetWindowTitle(Window* window, byte* title);

    [DllImport(Library, EntryPoint = "glfwShowWindow")]
    public static extern void ShowWindow(Window* window);
        
    [DllImport(Library, EntryPoint = "glfwSetWindowSize")]
    public static extern void SetWindowSize(Window* window, int width, int height);

    [DllImport(Library, EntryPoint = "glfwSetWindowSizeCallback")]
    public static extern nint SetWindowSizeCallback(Window* window, GLFWCallbacks.WindowSizeCallback callback);

    [DllImport(Library, EntryPoint = "glfwSetWindowShouldClose")]
    public static extern void SetWindowShouldClose(Window* window, int value);

    [DllImport(Library, EntryPoint = "glfwSetWindowMonitor")]
    public static extern void SetWindowMonitor(Window* window, Monitor* monitor, int x, int y, int width, int height, int refreshRate);

    [DllImport(Library, EntryPoint = "glfwSetWindowPos")]
    public static extern void SetWindowPos(Window* window, int x, int y);

    [DllImport(Library, EntryPoint = "glfwSetWindowPosCallback")]
    public static extern nint SetWindowPosCallback(Window* window, GLFWCallbacks.WindowPosCallback callback);
       
    [DllImport(Library, EntryPoint = "glfwSetWindowRefreshCallback")]
    public static extern nint SetWindowRefreshCallback(Window* window, GLFWCallbacks.WindowRefreshCallback callback);

    [DllImport(Library, EntryPoint = "glfwSwapInterval")]
    public static extern void SwapInterval(int interval);

    [DllImport(Library, EntryPoint = "glfwWaitEvents")]
    public static extern void WaitEvents();

    [DllImport(Library, EntryPoint = "glfwWaitEventsTimeout")]
    public static extern void WaitEventsTimeout(double timeout);

    [DllImport(Library, EntryPoint = "glfwGetClipboardString")]
    public static extern byte* GetClipboardString(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetClipboardString")]
    public static extern void SetClipboardString(Window* window, byte* data);

    [DllImport(Library, EntryPoint = "glfwVulkanSupported")]
    public static extern int VulkanSupported();

    [DllImport(Library, EntryPoint = "glfwGetRequiredInstanceExtensions")]
    public static extern byte** GetRequiredInstanceExtensions(uint* count);

    [DllImport(Library, EntryPoint = "glfwGetInstanceProcAddress")]
    public static extern nint GetInstanceProcAddress(VkHandle instance, byte* procName);

    [DllImport(Library, EntryPoint = "glfwGetPhysicalDevicePresentationSupport")]
    public static extern int GetPhysicalDevicePresentationSupport(VkHandle instance, VkHandle device, int queueFamily);

    [DllImport(Library, EntryPoint = "glfwCreateWindowSurface")]
    public static extern int CreateWindowSurface(VkHandle instance, Window* window, void* allocator, out VkHandle surface);

    [DllImport(Library, EntryPoint = "glfwGetWin32Adapter")]
    public static extern byte* GetWin32Adapter(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetWin32Monitor")]
    public static extern byte* GetWin32Monitor(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetWin32Window")]
    public static extern nint GetWin32Window(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetWGLContext")]
    public static extern nint GetWGLContext(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetCocoaMonitor")]
    public static extern uint GetCocoaMonitor(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetCocoaWindow")]
    public static extern nint GetCocoaWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetNSGLContext")]
    public static extern nint GetNSGLContext(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetX11Display")]
    public static extern nint GetX11Display();

    [DllImport(Library, EntryPoint = "glfwGetX11Adapter")]
    public static extern nuint GetX11Adapter(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetX11Monitor")]
    public static extern nuint GetX11Monitor(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetX11Window")]
    public static extern nuint GetX11Window(Window* window);

    [DllImport(Library, EntryPoint = "glfwSetX11SelectionString")]
    public static extern void SetX11SelectionString(byte* @string);

    [DllImport(Library, EntryPoint = "glfwGetX11SelectionString")]
    public static extern byte* GetX11SelectionString();

    [DllImport(Library, EntryPoint = "glfwGetGLXContext")]
    public static extern uint GetGLXContext(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetGLXWindow")]
    public static extern uint GetGLXWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetWaylandDisplay")]
    public static extern nint GetWaylandDisplay();

    [DllImport(Library, EntryPoint = "glfwGetWaylandMonitor")]
    public static extern nint GetWaylandMonitor(Monitor* monitor);

    [DllImport(Library, EntryPoint = "glfwGetWaylandWindow")]
    public static extern nint GetWaylandWindow(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetEGLDisplay")]
    public static extern nint GetEGLDisplay();

    [DllImport(Library, EntryPoint = "glfwGetEGLContext")]
    public static extern nint GetEGLContext(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetEGLSurface")]
    public static extern nint GetEGLSurface(Window* window);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaColorBuffer")]
    public static extern int GetOSMesaColorBuffer(Window* window, int* width, int* height, int* format, void** buffer);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaDepthBuffer")]
    public static extern int GetOSMesaDepthBuffer(Window* window, int* width, int* height, int* bytesPerValue, void** buffer);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaContext")]
    public static extern nint GetOSMesaContext(Window* window);


}