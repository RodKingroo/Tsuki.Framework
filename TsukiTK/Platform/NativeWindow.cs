using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using Tsuki.Framework.Math.Vector;
using Tsuki.Framework.Input.State;
using Tsuki.Framework.Platform.GLFW;
using Tsuki.Framework.Graphics.Primitives;
using Tsuki.Framework.Input.Event;
using Tsuki.Framework.Input;
using static Tsuki.Framework.Platform.GLFW.GLFW;
using static Tsuki.Framework.Platform.GLFW.GLFWCallbacks;

namespace Tsuki.Framework.Platform;

public class NativeWindow : IDisposable
{
    public unsafe Window* WindowPtr { get; set; }
    public bool IsEventDriven { get; set; }

    private Vector2 _cachedWindowClientSize;
    private Vector2 _cachedWindowLocation;
    private WindowState _unminimizedWindowState;
    private static ConcurrentQueue<ExceptionDispatchInfo> _callbackExceptions = new ConcurrentQueue<ExceptionDispatchInfo>();
    private unsafe Cursor* _glfwCursor;
    private MouseCursor _managedCursor = MouseCursor.Default;

    public ContextApi API { get; }
    public ContextProfile Profile { get; }
    public ContextFlags Flags { get; }
    public Version? APIVersion { get; }
    public IGLFWGraphicsContext? Context { get; }

    public KeyboardState KeyboardState { get; } = new KeyboardState();

    public KeyboardState? LastKeyboardState => null;

    private readonly JoystickState[] _joystickStates = new JoystickState[16];

    public IReadOnlyList<JoystickState> JoystickStates => _joystickStates;
    
    public IReadOnlyList<JoystickState>? LastJoystickStates => null;

    public unsafe NativeWindow(NativeWindowSettings settings)
    {
        GLFWProvider.EnsureInitialized();
        _title = settings.Title;
        _currentMonitor = settings.CurrentMonitor;

        switch (settings.WindowBorder)
        {
            case WindowBorder.Hidden:
                WindowHint(WindowHintBool.Decorated, false);
                break;
            case WindowBorder.Resizable:
                WindowHint(WindowHintBool.Resizable, true);
                break;
            case WindowBorder.Fixed:
                WindowHint(WindowHintBool.Resizable, false);
                break;
        }

        bool isOpenGL = false;
        API = settings.API;
        switch (API)
        {
            case ContextApi.NoAPI:
                WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);
                break;
            case ContextApi.OpenGLES:
                WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlEsApi);
                isOpenGL = true;
                break;
            case ContextApi.OpenGL:
                WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlApi);
                isOpenGL = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        WindowHint(WindowHintInt.ContextVersionMajor, settings.APIVersion.Major);
        WindowHint(WindowHintInt.ContextVersionMinor, settings.APIVersion.Minor);

        APIVersion = settings.APIVersion;
        Flags = settings.Flags;

        if (settings.Flags.HasFlag(ContextFlags.ForwardCompatible))
            WindowHint(WindowHintBool.OpenGLForwardCompat, true);
        if (settings.Flags.HasFlag(ContextFlags.Debug))
            WindowHint(WindowHintBool.OpenGLDebugContext, true);

        Profile = settings.Profile;
        switch (settings.Profile)
        {
            case ContextProfile.Any:
                WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Any);
                break;
            case ContextProfile.Compatability:
                WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Compat);
                break;
            case ContextProfile.Core:
                WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
                break;
        }

        WindowHint(WindowHintBool.Focused, settings.StartFocused);
        _windowBorder = settings.WindowBorder;

        _isVisible = settings.StartVisible;
        WindowHint(WindowHintBool.Visible, _isVisible);
        WindowHint(WindowHintInt.Samples, settings.NumberOfSamples);
        WindowHint(WindowHintBool.SrgbCapable, settings.SrgbCapable);

        Monitor* monitor = settings.CurrentMonitor.ToUnsafePtr<Monitor>();
        VideoMode* modePtr = GetVideoMode(monitor);
        
        WindowHint(WindowHintInt.RedBits, settings.RedBits ?? modePtr->RedBits);
        WindowHint(WindowHintInt.GreenBits, settings.GreenBits ?? modePtr->GreenBits);
        WindowHint(WindowHintInt.BlueBits, settings.BlueBits ?? modePtr->BlueBits);

        if (settings.AlphaBits.HasValue)
            WindowHint(WindowHintInt.AlphaBits, settings.AlphaBits.Value);
        if (settings.DepthBits.HasValue)
                WindowHint(WindowHintInt.DepthBits, settings.DepthBits.Value);
        if (settings.StencilBits.HasValue)
            WindowHint(WindowHintInt.StencilBits, settings.StencilBits.Value);
        WindowHint(WindowHintInt.RefreshRate, modePtr->RefreshRate);
        
        if (settings.WindowState == WindowState.Fullscreen && _isVisible)
        {
            _windowState = WindowState.Fullscreen;
            _cachedWindowLocation = settings.Location ?? new Vector2(32, 32);
            _cachedWindowClientSize = settings.Size;
            WindowPtr = CreateWindow(modePtr->Width, modePtr->Height, _title, monitor, (Window*)(settings.SharedContext?.WindowPtr ?? nint.Zero));
        }
        else
            WindowPtr = CreateWindow(settings.Size.X, settings.Size.Y, _title, null, (Window*)(settings.SharedContext?.WindowPtr ?? nint.Zero));

        if(settings.API != ContextApi.NoAPI)
            Context = new GLFWGraphicsContext(WindowPtr);

        Exists = true;
        if (isOpenGL)
        {
            Context?.MakeCurrent();

            if (settings.AutoLoadBindings)
                InitializeGlBindings();
        }

        SetInputMode(WindowPtr, LockKeyModAttribute.LockKeyMods, true);
        RegisterWindowCallbacks();

        InitialiseJoystickStates();

        _isFocused = settings.StartFocused;

        if (settings.StartFocused) Focus();

        WindowState = settings.WindowState;

        IsEventDriven = settings.IsEventDriven;

        if (settings.Icon != null) Icon = settings.Icon;

        if (settings.Location.HasValue) Location = settings.Location.Value;

        GetWindowSize(WindowPtr, out int width, out int height);

        HandleResize(width, height);

        AspectRatio = settings.AspectRatio;
        _minimumSize = settings.MinimumSize;
        _maximumSize = settings.MaximumSize;

        SetWindowSizeLimits(WindowPtr, _minimumSize?.X ?? DontCare, _minimumSize?.Y ?? DontCare, _maximumSize?.X ?? DontCare, _maximumSize?.Y ?? DontCare);

        GetWindowPos(WindowPtr, out int x, out int y);
        _location = new Vector2(x, y);

        GetCursorPos(WindowPtr, out double mousex, out double mousey);
        _lastReportedMousePos = new Vector2(mousex, mousey);
        MouseState.Position = _lastReportedMousePos;
        _isFocused = GetWindowAttrib(WindowPtr, WindowAttributeGetBool.Focused);

    }

    private unsafe void InitialiseJoystickStates()
    {
        for (int i = 0; i < _joystickStates.Length; i++)
        {
            if (JoystickPresent(i))
            {
                GetJoystickHatsRaw(i, out int hatCount);
                GetJoystickAxesRaw(i, out int axisCount);
                GetJoystickButtonsRaw(i, out int buttonCount);
                string name = GetJoystickName(i);

                _joystickStates[i] = new JoystickState(hatCount, axisCount, buttonCount, i, name);
            }
        }
    }

    private static void InitializeGlBindings()
    {
        Assembly assembly;
        try { assembly = Assembly.Load("Tsuki.Framework.Graphics"); }
        catch { return; }

        var provider = new GLFWBindingsContext();

        void LoadBindings(string typeNamespace)
        {
            var type = assembly.GetType($"Tsuki.Framework.Graphics.{typeNamespace}.GL");
            if (type == null) return;

            MethodInfo load = type.GetMethod("LoadBindings")!;
            load.Invoke(null, new object[] { provider! });
        }
        LoadBindings("ES11");
        LoadBindings("ES20");
        LoadBindings("ES30");
        LoadBindings("OpenGL");
        LoadBindings("OpenGL4");
        
    }

    private WindowPosCallback? _windowPosCallback;
    private unsafe void WindowPosCallback(Window* window, int x, int y)
    {
        try { OnMove(new WindowPositionEventArgs(x, y)); }
        catch (Exception e)
        { 
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMove(WindowPositionEventArgs e)
    {
        Move?.Invoke(e);

        _location.X = e.X;
        _location.Y = e.Y;
    }

    public event Action<WindowPositionEventArgs>? Move;

    private WindowSizeCallback? _windowSizeCallback;
    private unsafe void WindowSizeCallback(Window* window, int width, int height)
    {
        try { OnResize(new ResizeEventArgs(width, height)); }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnResize(ResizeEventArgs e)
    {
        HandleResize((int)e.Width, (int)e.Height);

        Resize?.Invoke(e);
    }

    private unsafe void HandleResize(int width, int height)
    {
        _size.X = width;
        _size.Y = height;

        GetFramebufferSize(WindowPtr, out width, out height);

        ClientSize = new Vector2(width, height);
    }

    public event Action<ResizeEventArgs>? Resize;

    private WindowIconifyCallback? _windowIconifyCallback;

    private unsafe void WindowIconifyCallback(Window* window, bool iconified)
    {
        try { OnMinimized(new MinimizedEventArgs(iconified)); }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMinimized(MinimizedEventArgs e)
    {
        if (e.IsMinimized)
            _windowState = WindowState.Minimized;
        else
            _windowState = GetWindowStateFromGLFW();

        Minimized?.Invoke(e);
    }

    public event Action<MinimizedEventArgs>? Minimized;

    private unsafe WindowState GetWindowStateFromGLFW()
    {
        if (GetWindowAttrib(WindowPtr, WindowAttributeGetBool.Iconified))
            return WindowState.Minimized;

        if (GetWindowAttrib(WindowPtr, WindowAttributeGetBool.Maximized))
            return WindowState.Maximized;

        if (GetWindowMonitor(WindowPtr) != null)
            return WindowState.Fullscreen;

        return WindowState.Normal;
    }

    private WindowMaximizeCallback? _windowMaximizeCallback;

    private unsafe void WindowMaximizeCallback(Window* window, bool maximized)
    {
        try
        {
            OnMaximized(new MaximizedEventArgs(maximized));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMaximized(MaximizedEventArgs e)
    {
        if (e.IsMaximized)
            _windowState = WindowState.Maximized;
        else
            _windowState = GetWindowStateFromGLFW();

        if (_windowState != WindowState.Minimized)
            _unminimizedWindowState = _windowState;

        Maximized?.Invoke(e);
    }

    public event Action<MaximizedEventArgs>? Maximized;

    private WindowFocusCallback? _windowFocusCallback;

    private unsafe void WindowFocusCallback(Window* window, bool focused)
    {
        try
        {
            OnFocusedChanged(new FocusedChangedEventArgs(focused));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnFocusedChanged(FocusedChangedEventArgs e)
    {
        FocusedChanged?.Invoke(e);

        _isFocused = e.IsFocused;
    }

    public event Action<FocusedChangedEventArgs>? FocusedChanged;

    private CharCallback? _charCallback;

    private unsafe void CharCallback(Window* window, uint codepoint)
    {
        try
        {
            OnTextInput(new TextInputEventArgs((int)codepoint));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnTextInput(TextInputEventArgs e)
        => TextInput?.Invoke(e);

    public event Action<TextInputEventArgs>? TextInput;

    private ScrollCallback? _scrollCallback;

    private unsafe void ScrollCallback(Window* window, double offsetX, double offsetY)
    {
        try
        {
            var offset = new Vector2(offsetX, offsetY);

            MouseState.Scroll += offset;

            OnMouseWheel(new MouseWheelEventArgs(offset));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMouseWheel(MouseWheelEventArgs e)
        => MouseWheel?.Invoke(e);

    public event Action<MouseWheelEventArgs>? MouseWheel;

    private WindowRefreshCallback? _windowRefreshCallback;

    private unsafe void WindowRefreshCallback(Window* window)
    {
        try { OnRefresh(); }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnRefresh()
        => Refresh?.Invoke();

    public event Action? Refresh;

    private WindowCloseCallback? _windowCloseCallback;

    private unsafe void WindowCloseCallback(Window* window)
    {
        try
        {
            var c = new CancelEventArgs();
            OnClosing(c);
            if (c.Cancel)
                SetWindowShouldClose(WindowPtr, false);
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnClosing(CancelEventArgs e)
        => Closing?.Invoke(e);
    public event Action<CancelEventArgs>? Closing;
    

    private KeyCallback? _keyCallback;

    private unsafe void KeyCallback(Window* window, Keys key, int scancode, InputAction action, KeyModifiers mods)
    {
        try
        {
            var args = new KeyboardKeyEventArgs(key, scancode, mods, action == InputAction.Repeat);

            if (action == InputAction.Release)
            {
                if (key != Keys.Unknown)
                    KeyboardState.SetKeyState(key, false);

                OnKeyUp(args);
            }
            else
            {
                if (key != Keys.Unknown)
                    KeyboardState.SetKeyState(key, true);

                OnKeyDown(args);
            }
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnKeyUp(KeyboardKeyEventArgs e)
        => KeyUp?.Invoke(e);

    public event Action<KeyboardKeyEventArgs>? KeyUp;

    protected virtual void OnKeyDown(KeyboardKeyEventArgs e)
        => KeyDown?.Invoke(e);

    public event Action<KeyboardKeyEventArgs>? KeyDown;

    private CursorEnterCallback? _cursorEnterCallback;

    private unsafe void CursorEnterCallback(Window* window, bool entered)
    {
        try
        {
            if (entered) OnMouseEnter();
            else OnMouseLeave();
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMouseEnter()
        => MouseEnter?.Invoke();
    
    public event Action? MouseEnter;

    protected virtual void OnMouseLeave()
        => MouseLeave?.Invoke();

    public event Action? MouseLeave;

    private MouseButtonCallback? _mouseButtonCallback;

    private unsafe void MouseButtonCallback(Window* window, MouseButton button, InputAction action, KeyModifiers mods)
    {
        try
        {
            var args = new MouseButtonEventArgs(button, action, mods);

            if (action == InputAction.Release)
            {
                MouseState[button] = false;
                OnMouseUp(args);
            }
            else
            {
                MouseState[button] = true;
                OnMouseDown(args);
            }
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMouseUp(MouseButtonEventArgs e)
        => MouseUp?.Invoke(e);

    public event Action<MouseButtonEventArgs>? MouseUp;

    protected virtual void OnMouseDown(MouseButtonEventArgs e)
        => MouseDown?.Invoke(e);

    public event Action<MouseButtonEventArgs>? MouseDown;

    

    private CursorPosCallback? _cursorPosCallback;

    private unsafe void CursorPosCallback(Window* window, double posX, double posY)
    {
        try
        {
            var newPos = new Vector2(posX, posY);
            var delta = newPos - _lastReportedMousePos;

            _lastReportedMousePos = newPos;

            OnMouseMove(new MouseMoveEventArgs(newPos, delta));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnMouseMove(MouseMoveEventArgs e)
        => MouseMove?.Invoke(e);
    
    public event Action<MouseMoveEventArgs>? MouseMove;

    private DropCallback? _dropCallback;

    private unsafe void DropCallback(Window* window, int count, byte** paths)
    {
        try
        {
            var arrayOfPaths = new string[count];

            for (var i = 0; i < count; i++)
                arrayOfPaths[i] = Marshal.PtrToStringUTF8((nint)paths[i])!;

            OnFileDrop(new FileDropEventArgs(arrayOfPaths));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnFileDrop(FileDropEventArgs e)
        => FileDrop?.Invoke(e);

    public event Action<FileDropEventArgs>? FileDrop;

    private JoystickCallback? _joystickCallback;

    private unsafe void JoystickCallback(int joy, ConnectedState eventCode)
    {
        try
        {
            if (eventCode == ConnectedState.Connected)
            {
                GetJoystickHatsRaw(joy, out int hatCount);
                GetJoystickAxesRaw(joy, out int axisCount);
                GetJoystickButtonsRaw(joy, out int buttonCount);
                string name = GetJoystickName(joy);

                _joystickStates![joy] = new JoystickState(hatCount, axisCount, buttonCount, joy, name);
            }
            else _joystickStates[joy] = null!;

            OnJoystickConnected(new JoystickEventArgs(joy, eventCode == ConnectedState.Connected));
        }
        catch (Exception e)
        {
            _callbackExceptions.Enqueue(ExceptionDispatchInfo.Capture(e));
        }
    }

    protected virtual void OnJoystickConnected(JoystickEventArgs e)
        => JoystickConnected?.Invoke(e);

    public event Action<JoystickEventArgs>? JoystickConnected;

    private unsafe void RegisterWindowCallbacks()
    {
        _windowPosCallback = WindowPosCallback;
        _windowSizeCallback = WindowSizeCallback;
        _windowCloseCallback = WindowCloseCallback;
        _windowRefreshCallback = WindowRefreshCallback;
        _windowFocusCallback = WindowFocusCallback;
        _windowIconifyCallback = WindowIconifyCallback;
        _windowMaximizeCallback = WindowMaximizeCallback;

        _mouseButtonCallback = MouseButtonCallback;
        _cursorPosCallback = CursorPosCallback;
        _cursorEnterCallback = CursorEnterCallback;
        _scrollCallback = ScrollCallback;

        _keyCallback = KeyCallback;
        _charCallback = CharCallback;

        _dropCallback = DropCallback;

        _joystickCallback = JoystickCallback;

        SetWindowPosCallback(WindowPtr, _windowPosCallback);
        SetWindowSizeCallback(WindowPtr, _windowSizeCallback);
        SetWindowCloseCallback(WindowPtr, _windowCloseCallback);
        SetWindowRefreshCallback(WindowPtr, _windowRefreshCallback);
        SetWindowFocusCallback(WindowPtr, _windowFocusCallback);
        SetWindowIconifyCallback(WindowPtr, _windowIconifyCallback);
        SetWindowMaximizeCallback(WindowPtr, _windowMaximizeCallback);

        SetMouseButtonCallback(WindowPtr, _mouseButtonCallback);
        SetCursorPosCallback(WindowPtr, _cursorPosCallback);
        SetCursorEnterCallback(WindowPtr, _cursorEnterCallback);
        SetScrollCallback(WindowPtr, _scrollCallback);

        SetKeyCallback(WindowPtr, _keyCallback);
        SetCharCallback(WindowPtr, _charCallback);

        SetDropCallback(WindowPtr, _dropCallback);

        Joysticks.JoystickCallback += _joystickCallback;
    }

    private void UnregisterWindowCallbacks()
        => Joysticks.JoystickCallback -= _joystickCallback;

    private Vector2 _lastReportedMousePos;
    public unsafe Vector2 MousePosition
    {
        get => _lastReportedMousePos;
        set
        {
            SetCursorPos(WindowPtr, value.X, value.Y);
        }
    }

    public Vector2 MouseDelta => Vector2.Zero;

    public MouseState MouseState { get; } = new MouseState();

    public MouseState? LastMouseState => null;

    public bool IsAnyKeyDown => KeyboardState.IsAnyKeyDown;

    public bool IsAnyMouseButtonDown => MouseState.IsAnyButtonDown;

    private VSyncMode _vSync;
    public VSyncMode VSync
    {
        get => _vSync;
        set
        {
            switch (value)
            {
                case VSyncMode.On:
                    Context!.SwapInterval = 1;
                    break;
                case VSyncMode.Off:
                    Context!.SwapInterval = 0;
                    break;
            }
            _vSync = value;
        }
    }

    private WindowIcon? _icon;
    public unsafe WindowIcon? Icon
    {
        get => _icon;
        set
        {
            Framework.Image[] images = value!.Images;
            Span<GCHandle> handles = stackalloc GCHandle[images.Length];
            Span<Platform.Image> glfwImages = stackalloc Platform.Image[images.Length];
            
            for(int i = 0; i < images.Length; i++)
            {
                Framework.Image image = images[i];
                handles[i] = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
                byte* addrOfPinnedObject = (byte*)handles[i].AddrOfPinnedObject();
                glfwImages[i] = new Image(image.Width, image.Height, addrOfPinnedObject);
            }
            SetWindowIcon(WindowPtr, glfwImages);

            foreach(var handle in handles)
                handle.Free();

            _icon = value;
        }
    }

    public unsafe string ClipboardString
    {
        get => GetClipboardString(WindowPtr);
        set => SetClipboardString(WindowPtr, value);
    }

    private string? _title;
    public unsafe string Title
    {
        get => _title!;
        set
        {
            SetWindowTitle(WindowPtr, value);
            _title = value;
        }
    }

    private MonitorHandle _currentMonitor;
    public unsafe MonitorHandle CurrentMonitor
    {
        get => _currentMonitor;
        set
        {
            Monitor* monitor = value.ToUnsafePtr<Monitor>();
            VideoMode* mode = GetVideoMode(monitor);
            SetWindowMonitor(WindowPtr, monitor, _location.X, _location.Y, _size.X, _size.Y, mode->RefreshRate);

            _currentMonitor = value;
        }
    }

    private bool _isFocused;
    public bool IsFocused => _isFocused;

    public unsafe void Focus() => FocusWindow(WindowPtr);

    public bool _isVisible;
    public unsafe bool IsVisible
    {
        get => _isVisible;
        set
        {
            if(value != _isVisible)
            {
                _isVisible = value;
                UpdateWindowForStateAndVisibility();
            }
        }
    }

    public bool Exists { get; set; }
    
    public unsafe bool IsExiting => WindowShouldClose(WindowPtr);

    private WindowState _windowState = WindowState.Normal;
    public unsafe WindowState WindowState
    {
        get => _windowState;
        set
        {
            if(_windowState != value)
            {
                if(value != WindowState.Minimized) _unminimizedWindowState = value;
                bool shouldCacheSizeAndLocation = _windowState != WindowState.Fullscreen &&
                                                  _windowState != WindowState.Minimized &&
                                                  (value == WindowState.Fullscreen || value == WindowState.Minimized);

                if(_windowState == WindowState.Fullscreen && value != WindowState.Fullscreen && _isVisible)
                    SetWindowMonitor(WindowPtr, null, _cachedWindowLocation.X, _cachedWindowLocation.Y, _cachedWindowClientSize.X, _cachedWindowClientSize.Y, 0);
                
                if(shouldCacheSizeAndLocation)
                {
                    _cachedWindowClientSize = ClientSize;
                    _cachedWindowLocation = Location;
                }
                
                _windowState = value;
                UpdateWindowForStateAndVisibility();
            }
        }
    }

    private WindowBorder _windowBorder;
    public unsafe WindowBorder WindowBorder
    {
        get => _windowBorder;
        set
        {
            GetVersion(out int major, out int minor, out _);
            if (major == 3 && minor < 3)
                throw new NotSupportedException("Cannot be implemented in GLFW 3.2.");

            switch (value)
            {
                case WindowBorder.Hidden:
                    SetWindowAttrib(WindowPtr, WindowAttribute.Decorated, false);
                    SetWindowAttrib(WindowPtr, WindowAttribute.Resizable, false);
                    break;
                case WindowBorder.Resizable:
                    SetWindowAttrib(WindowPtr, WindowAttribute.Decorated, true);
                    SetWindowAttrib(WindowPtr, WindowAttribute.Resizable, true);
                    break;
                case WindowBorder.Fixed:
                    SetWindowAttrib(WindowPtr, WindowAttribute.Decorated, true);
                    SetWindowAttrib(WindowPtr, WindowAttribute.Resizable, false);
                    break;
            }
            _windowBorder = value;
        }
    }

    public unsafe Rectangle Bounds
    {
        get => new Rectangle(Location, Location + Size);
        set
        {
            SetWindowSize(WindowPtr, value.Size.X, value.Size.Y);
            SetWindowPos(WindowPtr, value.Location.X, value.Location.Y);
        }
    }

    private unsafe void UpdateWindowForStateAndVisibility()
    {
        bool isMsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        if(!_isVisible)
        {
            HideWindow(WindowPtr);
            return;
        }

        if(!isMsWindows) ShowWindow(WindowPtr);

        switch (_windowState)
        {
            case WindowState.Normal:
                RestoreWindow(WindowPtr);
                break;
            case WindowState.Minimized:
                IconifyWindow(WindowPtr);
                break;
            case WindowState.Maximized:
                if (isMsWindows) RestoreWindow(WindowPtr);
                MaximizeWindow(WindowPtr);
                break;
            case WindowState.Fullscreen:
                if (isMsWindows)
                {
                    ShowWindow(WindowPtr);
                    RestoreWindow(WindowPtr);
                }

                Monitor* monitor = CurrentMonitor.ToUnsafePtr<Monitor>();
                VideoMode* modePtr = GetVideoMode(monitor);
                SetWindowMonitor(WindowPtr, monitor, 0, 0, modePtr->Width, modePtr->Height, modePtr->RefreshRate);
                break;
        }

    }

    private Vector2 _location;
    public unsafe Vector2 Location
    {
        get => _location;
        set
        {
            SetWindowPos(WindowPtr, value.X, value.Y);
            _location = value;
        }
    }
    
    public Vector2 ClientSize { get; set; }

    private Vector2 _size;
    public unsafe Vector2 Size
    {
        get => _size;
        set
        {
            SetWindowSize(WindowPtr, value.X, value.Y);
        }
    }

    private Vector2? _minimumSize;
    private Vector2? _maximumSize;

    public unsafe Vector2? MinimumSize
    {
        get => _minimumSize;
        set
        {
            _minimumSize = value;
            SetWindowSizeLimits(WindowPtr, value?.X ?? DontCare, value?.Y ?? DontCare, _maximumSize?.X ?? DontCare, _maximumSize?.Y ?? DontCare);
        }
    }

    public unsafe Vector2? MaximumSize
    {
        get => _maximumSize;
        set
        {
            _maximumSize = value;
            SetWindowSizeLimits(WindowPtr, _minimumSize?.X ?? DontCare, _minimumSize?.Y ?? DontCare, value?.X ?? DontCare, value?.Y ?? DontCare);
        }
    }

    private (int numerator, int denominator)? _aspectRatio;

    public unsafe (int numerator, int denominator)? AspectRatio
    {
        get => _aspectRatio;
        set
        {
            _aspectRatio = value;
            SetWindowAspectRatio(WindowPtr, value?.numerator ?? DontCare, value?.denominator ?? DontCare);

        }
    }
    
    public Rectangle ClientRectagle
    {
        get => new Rectangle(Location, Location + Size);
        set
        {
            Size = value.Size;
            Location = value.Location;
        }
    }

    public Vector2 CLientSize { get; set; }

    public bool IsFullscreen => WindowState == WindowState.Fullscreen;

    public unsafe MouseCursor Cursor
    {
        get => _managedCursor;
        set
        {
            _managedCursor = value ??
                             throw new ArgumentNullException(
                             nameof(value),
                             "Cursor cannot be null. To reset to default cursor, set it to MouseCursor.Default instead.");
 
            Cursor* oldCursor = _glfwCursor;
            _glfwCursor = null;

            if(value.Shape == StandardShape.CustomShape)
            {
                fixed(byte* ptr = value.Data)
                {
                    Image cursorImg = new Image(value.Width, value.Height, ptr);
                    _glfwCursor = CreateCursor(cursorImg, value.X, value.Y);

                }
            }
            else if(value != MouseCursor.Default)
                _glfwCursor = CreateStandardCursor(MapStandardCursorShape(value.Shape));
        
            SetCursor(WindowPtr, _glfwCursor);

            if(oldCursor != null) DestroyCursor(oldCursor);
        }
    }

    private static CursorShape MapStandardCursorShape(StandardShape shape)
    {
        return shape switch
        {
            StandardShape.Arrow => (CursorShape)StandardShape.Arrow,
            StandardShape.IBeam => (CursorShape)StandardShape.IBeam,
            StandardShape.Crosshair => (CursorShape)StandardShape.Crosshair,
            StandardShape.Hand => (CursorShape)StandardShape.Hand,
            StandardShape.HResize => (CursorShape)StandardShape.HResize,
            StandardShape.VResize => (CursorShape)StandardShape.VResize,
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
        };
    }

    public unsafe CursorState CursorState
    {
        get
        {
            CursorModeValue inputMode = GetInputMode(WindowPtr, CursorStateAttribute.Cursor);
            switch (inputMode)
            {
                case CursorModeValue.CursorNormal:
                    return CursorState.Normal;
                case CursorModeValue.CursorHidden:
                    return CursorState.Hidden;
                case CursorModeValue.CursorDisabled:
                    return CursorState.Grabbed;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        set
        {
            CursorModeValue inputMode;
            switch(value)
            {
                case CursorState.Normal:
                    inputMode = CursorModeValue.CursorNormal;
                    break;
                case CursorState.Hidden:
                    inputMode = CursorModeValue.CursorHidden;
                    break;
                case CursorState.Grabbed:
                    inputMode = CursorModeValue.CursorDisabled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SetInputMode(WindowPtr, CursorStateAttribute.Cursor, inputMode);

        }
    }

    public unsafe bool CursorVisible
    {
        get
        {
            var inputMode = GetInputMode(WindowPtr, CursorStateAttribute.Cursor);
            return inputMode != CursorModeValue.CursorHidden &&
                   inputMode != CursorModeValue.CursorDisabled;
        }
        set
        {
            if (value)
                SetInputMode(WindowPtr, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
            else 
                SetInputMode(WindowPtr, CursorStateAttribute.Cursor, CursorModeValue.CursorHidden);
        }
    }

    public unsafe bool CursorGrabbed
    {
        get => GetInputMode(WindowPtr, CursorStateAttribute.Cursor) == CursorModeValue.CursorDisabled;
        set
        {
            if (value)
                SetInputMode(WindowPtr, CursorStateAttribute.Cursor, CursorModeValue.CursorDisabled);
            else if (CursorVisible)
                SetInputMode(WindowPtr, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
            else
                SetInputMode(WindowPtr, CursorStateAttribute.Cursor, CursorModeValue.CursorHidden);
        }
    }

    public virtual unsafe void Close()
    {
        CancelEventArgs c = new CancelEventArgs();
        OnClosing(c);
        if (c.Cancel == false)
        {
            SetWindowShouldClose(WindowPtr, true);
        }
    }

    public void MakeCurrent()
    {
        if (Context == null)
            throw new InvalidOperationException("Cannot make a context current when running with ContextAPI.NoAPI.");

        Context?.MakeCurrent();
    }

    public bool ProcessEvents(double timeout)
    {
        WaitEventsTimeout(timeout);

        ProcessInputEvents();

        RethrowCallbackExceptionsIfNeeded();

        return true;
    }

    public virtual void ProcessEvents()
    {
        ProcessInputEvents();

        ProcessWindowEvents(IsEventDriven);
    }

    public unsafe void ProcessInputEvents()
    {
        MouseState.Update();
        KeyboardState.Update();

        GetCursorPos(WindowPtr, out double x, out double y);
        MouseState.Position = new Vector2(x, y);

        foreach (JoystickState v in _joystickStates)
        {
            if (v == null)
                continue;

            v.Update();
        }
    }

    private static void RethrowCallbackExceptionsIfNeeded()
    {
        while (_callbackExceptions.TryDequeue(out ExceptionDispatchInfo? exception))
            _localThreadExceptions.Add(exception);

        if (_localThreadExceptions.Count == 1)
        {
            ExceptionDispatchInfo exception = _localThreadExceptions[0];
            _localThreadExceptions.Clear();
            exception.Throw();
        }
        else if (_localThreadExceptions.Count > 1)
        {
            Exception[] exceptions = new Exception[_localThreadExceptions.Count];
            for (int i = 0; i < _localThreadExceptions.Count; i++)
                exceptions[i] = _localThreadExceptions[i].SourceException;
            Exception exception = new AggregateException("Multiple exceptions in callback handlers while processing events.", exceptions);
            _localThreadExceptions.Clear();
            throw exception;
        }
    }

    public static void ProcessWindowEvents(bool waitForEvents)
    {
        if (waitForEvents) WaitEvents();
        else PollEvents();

        RethrowCallbackExceptionsIfNeeded();
    }

    private static List<ExceptionDispatchInfo> _localThreadExceptions = new List<ExceptionDispatchInfo>();

    public Vector2 PointToClient(Vector2 point)
        => point - Location;

    public Vector2 PointToScreen(Vector2 point)
        => point + Location;

    private bool _disposedValue;

    protected virtual unsafe void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
        }

        if (GLFWProvider.IsOnMainThread)
        {
            UnregisterWindowCallbacks();
            DestroyWindow(WindowPtr);
            Exists = false;
        }
        else
            throw new GLFWException("You can only dispose windows on the main thread. The window needs to be disposed as it cannot safely be disposed in the finalizer.");

        _disposedValue = true;
    }

    ~NativeWindow() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void CenterWindow() => CenterWindow(Size);

    public Rectangle ClientRectangle
    {
        get => new Rectangle(Location, Location + Size);
        set
        {
            Location = value.Min;
            Size = value.Size;
        }
    }

    public void CenterWindow(Vector2 newSize)
    {
        MonitorInfo monitorInfo = Monitors.GetMonitorFromWindow(this);

        Rectangle monitorRectangle = monitorInfo.ClientArea;
        int x = (int)(monitorRectangle.Min.X + monitorRectangle.Max.X - newSize.X) / 2;
        int y = (int)(monitorRectangle.Min.Y + monitorRectangle.Max.Y - newSize.Y) / 2;

        if (x < monitorRectangle.Min.X)  x = (int)monitorRectangle.Min.X;

        if (y < monitorRectangle.Min.Y) y = (int)monitorRectangle.Min.Y;
        
        ClientRectangle = new Rectangle(x, y, x + newSize.X, y + newSize.Y);
    
    }

}