using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Tsuki.Platform.GLFW.Args;
using Tsuki.Platform.GLFW.Enums;
using Tsuki.Platform.GLFW.Structs;
using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;
using Window = Tsuki.Platform.GLFW.Structs.Window;

namespace Tsuki.Platform.GLFW;

public class NativeWindow : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<NativeWindow>
{
    private readonly Window _window;
    public IntPtr Handle => handle;

    private SizeCallback? _windowSizeCallback;
    private SizeCallback? _framebufferSizeCallback;
    private PositionCallback? _windowPositionCallback;
    private FocusCallback? _windowFocusCallback;
    private WindowCallback? _closeCallback;
    private WindowCallback? _windowRefreshCallback;
    private FileDropCallback? _dropCallback;
    private MouseCallback? _cursorPositionCallback;
    private MouseCallback? _scrollCallback;
    private MouseEnterCallback? _cursorEnterCallback;
    private MouseButtonCallback? _mouseButtonCallback;
    private CharModsCallback? _charModsCallback;
    private KeyCallback? _keyCallback;
    private WindowMaximizedCallback? _windowMaximizeCallback;
    private WindowContentsScaleCallback? _windowContentScaleCallback;

    public event EventHandler<ContentScaleEventArgs>? ContentScaleChanged;
    public event EventHandler<MaximizeEventArgs>? MaximizeChanged;

    protected NativeWindow() : this(640, 480, "Tsuki Empty", Monitor.None, Window.None)
    {
        
    }

    protected NativeWindow(int width, int height, string title) : this(width, height, title, Monitor.None, Window.None)
    {
        
    }

    protected NativeWindow(int width, int height, string title, Monitor monitor, Window window) : base(true)
    {
        _window = Glfw.CreateWindow(width, height, title, monitor, window);
        SetHandle(_window);
        if (Glfw.GetClientApi(this) != ClientApi.None)
            MakeCurrent();
        BindCallbacks();
    }

    protected virtual void OnContentScaleChanged(float xScale, float yScale)
    {
        ContentScaleChanged?.Invoke(this, new ContentScaleEventArgs(xScale, yScale));
    }

    protected virtual void OnMaximizeChanged(bool maximized)
    {
        MaximizeChanged?.Invoke(this, new MaximizeEventArgs(maximized));
    }

    private Point Position
    {
        get
        {
            Glfw.GetWindowPosition(_window, out var x, out var y);
            Glfw.GetWindowFrameSize(_window, out var left, out var top, out var dummy1, out var dummy2);
            return new Point(x - left, y - top);
        }
        set
        {
            Glfw.GetWindowFrameSize(_window, out var left, out var top, out var dummy1, out var dummy2);
            Glfw.SetWindowPosition(_window, value.X + left, value.Y + top);
        }
    }

    private Size Size
    {
        get
        {
            Glfw.GetWindowSize(_window, out var width, out var height);
            Glfw.GetWindowFrameSize(_window, out var left, out var top, out var right, out var bottom);
            return new Size(width + left + right, height + top + bottom);
        }
        set
        {
            Glfw.GetWindowFrameSize(_window, out var left, out var top, out var right, out var bottom);
            Glfw.SetWindowSize(_window, value.Width - left - right, value.Height - top - bottom);
        }
    }

    public Size ClientSize
    {
        get
        {
            Glfw.GetWindowSize(_window, out var width, out var height);
            return new Size(width, height);
        }
        set => Glfw.SetWindowSize(_window, value.Width, value.Height);
    }

    public Rectangle Bounds
    {
        get => new Rectangle(Position, Size);
        set
        {
            Size = value.Size;
            Position = value.Location;
        }
    }

    public PointF ContentScale
    {
        get
        {
            Glfw.GetWindowContentScale(_window, out var x, out var y);
            return new PointF(x, y);
        }
    }

    public Rectangle ClientBounds
    {
        get => new Rectangle(Position, ClientSize);
        set
        {
            Glfw.SetWindowPosition(_window, value.X, value.Y);
            Glfw.SetWindowSize(_window, value.Width, value.Height);
        }
    }

    public int ClientWidth
    {
        get
        {
            Glfw.GetWindowSize(_window, out var width, out var dummy);
            return width;
        }
        set
        {
            if (value < 1)
                throw new Exception("Window width must be greater that 0");
            Glfw.GetWindowSize(_window, out var dummy, out var height);
            Glfw.SetWindowSize(_window, value, height);
        }
        
    }

    public int ClientHeight
    {
        get
        {
            Glfw.GetWindowSize(_window, out var dummy, out var height);
            return height;
        }
        set
        {
            if (value < 1)
                throw new Exception("Window height must be greater that 0");
            Glfw.GetWindowSize(_window, out var height, out var dummy);
            Glfw.SetWindowSize(_window, height, value);
        }
    }

    public void RequestAttention()
    {
        Glfw.RequestWindowAttention(_window);
    }

    public string Clipboard
    {
        get => Glfw.GetClipboardString(_window);
        set => Glfw.SetClipboardString(_window, value);
    }

    public CursorMode CursorMode
    {
        get => (CursorMode)Glfw.GetInputMode(_window, InputMode.Cursor);
        set => Glfw.SetInputMode(_window, InputMode.Cursor, (int)value);
    }

/*
    public IntPtr Hwnd
    {
        get
        {
            try
            {
                return Native.GetWin32Window(_window);
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }
    }
*/

    public bool Maximized
    {
        get => Glfw.GetWindowAttribute(_window, WindowAttribute.Maximized);
        set
        {
            if (value) Glfw.MaximizeWindow(_window);
            else Glfw.RestoreWindow(_window);
        }
    }

    public bool Minimized
    {
        get => Glfw.GetWindowAttribute(_window, WindowAttribute.AutoIconify);
        set
        {
            if (value) Glfw.IconifyWindow(_window);
            else Glfw.RestoreWindow(_window);
        }
    }

    public bool StickyKeys
    {
        get => Glfw.GetInputMode(_window, InputMode.StickyKeys) == (int)Constants.True;
        set => Glfw.SetInputMode(_window, InputMode.StickyKeys, value ? (int)Constants.True : (int)Constants.False);
    }

    public bool StickyMouseButtons
    {
        get => Glfw.GetInputMode(_window, InputMode.StickyMouseButtons) == (int)Constants.True;
        set => Glfw.SetInputMode(_window, InputMode.StickyMouseButtons,
            value ? (int)Constants.True : (int)Constants.False);
    }

    public bool Visible
    {
        get => Glfw.GetWindowAttribute(_window, WindowAttribute.Visible);
        set
        {
            if (value) Glfw.ShowWindow(_window);
            else Glfw.HideWindow(_window);
        }
    }

    override protected bool ReleaseHandle()
    {
        try
        {
            Glfw.DestroyWindow(_window);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsClosing => Glfw.WindowShouldClose(_window);
    public bool IsDecorated => Glfw.GetWindowAttribute(_window, WindowAttribute.Decorated);
    public bool IsFloating => Glfw.GetWindowAttribute(_window, WindowAttribute.Floating);
    public bool IsFocused => Glfw.GetWindowAttribute(_window, WindowAttribute.Focused);
    public bool IsResizable => Glfw.GetWindowAttribute(_window, WindowAttribute.Resizable);

    private Monitor Monitor => Glfw.GetWindowMonitor(_window);

    public Point MousePosition
    {
        get
        {
            Glfw.GetCursorPosition(_window, out var x, out var y);
            return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
        }
        set => Glfw.SetCursorPosition(_window, value.X, value.Y);
    }

    public Pointer UserPointer
    {
        get => Glfw.GetWindowUserPointer(_window);
        set => Glfw.SetWindowUserPointer(_window, value);
    }

    public VideoMode VideoMode
    {
        get
        {
            var monitor = Monitor;
            return Glfw.GetVideoMode(monitor == Monitor.None ? Glfw.PrimaryMonitor : monitor);
        }
    }

    public void CenterOnScreen()
    {
        if (Maximized) return;
        var monitor = Monitor == Monitor.None ? Glfw.PrimaryMonitor : Monitor;
        var videoMode = Glfw.GetVideoMode(monitor);
        var size = Size;
        Position = new Point((videoMode.Width - size.Width) / 2, (videoMode.Height - size.Height) / 2);
    }
    
    public new void Close()
    {
        Glfw.SetWindowShouldClose(_window, true);
        OnClosing();
        base.Close();
    }

    public void Focus() { Glfw.FocusWindow(_window); }
    public void Fullscreen() { Fullscreen(Glfw.PrimaryMonitor); }

    private void MakeCurrent() { Glfw.MakeContextCurrent(_window); }
    
    public void Maximize() { Glfw.MaximizeWindow(_window); }
    public void Minimize() { Glfw.IconifyWindow(_window); }

    public void Restore() { Glfw.RestoreWindow(_window); }

    public void SwapBuffers() { Glfw.SwapBuffers(_window); }

    private void Fullscreen(Monitor monitor)
    {
        Glfw.SetWindowMonitor(_window, monitor, 0, 0, 0, 0, -1);
    }

    public void SetAspectRatio(int numerator, int denominator)
    {
        Glfw.SetWindowAspectRatio(_window, numerator, denominator);
    }

    public void SetIcons(params Image[] images)
    {
        Glfw.SetWindowIcon(_window, images.Length, images);
    }

    public void SetMonitor(Monitor monitor, int x, int y, int width, int height,
        int refreshRate = (int)Constants.Default)
    {
        Glfw.SetWindowMonitor(_window, monitor, x, y, width, height, refreshRate);
    }

    public void SetSizeLimits(Size minSize, Size maxSize)
    {
        SetSizeLimits(minSize.Width, minSize.Height, maxSize.Width, maxSize.Height);
    }

    private void SetSizeLimits(int minWidth, int minHeight, int maxWidth, int maxHeight)
    {
        Glfw.SetWindowSizeLimits(_window, minWidth, minHeight, maxWidth, maxHeight);
    }

    private void BindCallbacks()
    {
        _windowPositionCallback = (_, x, y) => OnPositionChanged(x, y);
        _windowSizeCallback = (_, width, height) => OnSizeChanged(width, height);
        _windowFocusCallback = (_, focusing) => OnFocusChanged(focusing);
        _closeCallback = _ => OnClosing();
        _dropCallback = (_, count, arrayPtr) => OnFileDrop(count, arrayPtr);
        _cursorPositionCallback = (_, x, y) => OnMouseMove(x, y);
        _cursorEnterCallback = (_, entering) => OnMouseEnter(entering);
        _mouseButtonCallback = (_, button, state, mod) => OnMouseButton(button, state, mod);
        _scrollCallback = (_, x, y) => OnMouseScroll(x, y);
        _charModsCallback = (_, cp, mods) => OnCharacterInput(cp, mods);
        _framebufferSizeCallback = (_, width, height) => OnFramebufferSizeChanged(width, height);
        _windowRefreshCallback = _ => OnWindowRefresh();
        _keyCallback = (_, key, code, state, mods) => OnKey(key, code, state, mods);
        _windowMaximizeCallback = (_, maximized) => OnMaximizeChanged(maximized);
        _windowContentScaleCallback = (_, x, y) => OnContentScaleChanged(x, y);

        Glfw.SetWindowPositionCallback(_window, _windowPositionCallback);
        Glfw.SetWindowSizeCallback(_window, _windowSizeCallback);
        Glfw.SetWindowFocusCallback(_window, _windowFocusCallback);
        Glfw.SetCloseCallback(_window, _closeCallback);
        Glfw.SetDropCallback(_window, _dropCallback);
        Glfw.SetCursorPositionCallback(_window, _cursorPositionCallback);
        Glfw.SetCursorEnterCallback(_window, _cursorEnterCallback);
        Glfw.SetMouseButtonCallback(_window, _mouseButtonCallback);
        Glfw.SetScrollCallback(_window, _scrollCallback);
        Glfw.SetCharModsCallback(_window, _charModsCallback);
        Glfw.SetFramebufferSizeCallback(_window, _framebufferSizeCallback);
        Glfw.SetWindowRefreshCallback(_window, _windowRefreshCallback);
        Glfw.SetKeyCallback(_window, _keyCallback);
        Glfw.SetWindowMaximizeCallback(_window, _windowMaximizeCallback);
        Glfw.SetWindowContentScaleCallback(_window, _windowContentScaleCallback);
    }

    public event EventHandler PositionChanged = null!;
    protected virtual void OnPositionChanged(float x, float y)
    {
        PositionChanged.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler SizeChanged = null!;

    protected virtual void OnSizeChanged(int width, int height)
    {
        SizeChanged.Invoke(this, new SizeChangeEventArgs(new Size(width, height)));
    }

    public event EventHandler FocusChanged = null!;

    protected virtual void OnFocusChanged(bool focusing)
    {
        FocusChanged.Invoke(this, EventArgs.Empty);
    }

    public event CancelEventHandler Closing = null!;
    protected virtual void OnClosing()
    {
        var args = new CancelEventArgs();
        Closing.Invoke(this, args);
        if (args.Cancel) Glfw.SetWindowShouldClose(_window, false);
        else
        {
            base.Close();
            OnClosed();
        }
    }

    public event EventHandler Closed = null!;
    protected virtual void OnClosed()
    {
        Closed.Invoke(this, EventArgs.Empty);
    }

    private void OnFileDrop(int count, IntPtr pointer)
    {
        var paths = new string[count];
        var offset = 0;

        for (var i = 0; i < count; i++, offset += IntPtr.Size)
        {
            var ptr = Marshal.ReadIntPtr(pointer + offset);
            paths[i] = Util.PtrToStringUTF8(ptr);
        }

        OnFileDrop(paths);
    }

    public event EventHandler<FileDropEventArgs> FileDrop = null!;
    protected virtual void OnFileDrop(string[] paths)
    {
        FileDrop.Invoke(this, new FileDropEventArgs(paths));
    }

    public event EventHandler<MouseMoveEventArgs> MouseMoved = null!;
    protected virtual void OnMouseMove(float x, float y)
    {
        MouseMoved.Invoke(this, new MouseMoveEventArgs(x, y));
    }

    public event EventHandler MouseEnter = null!;
    public event EventHandler MouseLeave = null!;
    protected virtual void OnMouseEnter(bool entering)
    {
        if (entering) MouseEnter.Invoke(this, EventArgs.Empty);
        else MouseLeave.Invoke(this, EventArgs.Empty);
    }

    
    public event EventHandler<MouseButtonEventArgs> MouseButton = null!;
    protected virtual void OnMouseButton(MouseButton button, InputState state, ModifierKeys modifier)
    {
        MouseButton.Invoke(this, new MouseButtonEventArgs(button, state, modifier));
    }

    public event EventHandler<MouseMoveEventArgs> MouseScroll = null!;
    protected virtual void OnMouseScroll(float x, float y)
    {
        MouseScroll.Invoke(this, new MouseMoveEventArgs(x, y));
    }

    public event EventHandler<CharEventArgs> CharacterInput = null!;
    protected virtual void OnCharacterInput(uint codePoint, ModifierKeys modifiers)
    {
        CharacterInput.Invoke(this, new CharEventArgs(codePoint, modifiers));
    }

    public event EventHandler<SizeChangeEventArgs> FramebufferSizeChanged = null!;
    protected virtual void OnFramebufferSizeChanged(int width, int height)
    {
        FramebufferSizeChanged.Invoke(this, new SizeChangeEventArgs(new Size(width, height)));
    }

    public event EventHandler Refreshed = null!;
    protected virtual void OnWindowRefresh()
    {
        Refreshed.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler<KeyEventArgs>? KeyPress;
    public event EventHandler<KeyEventArgs>? KeyRelease;
    public event EventHandler<KeyEventArgs>? KeyRepeat;
    public event EventHandler<KeyEventArgs>? KeyAction;
    protected virtual void OnKey(Keys key, int scanCode, InputState state, ModifierKeys modifiers)
    {
        var args = new KeyEventArgs(key, scanCode, state, modifiers);
        if (state.HasFlag(InputState.Press)) KeyPress?.Invoke(this, args);
        else if (state.HasFlag(InputState.Release)) KeyRelease?.Invoke(this, args);
        else KeyRepeat?.Invoke(this, args);
        KeyAction?.Invoke(this, args);
    }
    
    public static implicit operator Window(NativeWindow window) => window._window;
    public static implicit operator IntPtr(NativeWindow window) => window._window;

    public static bool operator ==(NativeWindow left, NativeWindow right) => left.Equals(right);
    public static bool operator !=(NativeWindow left, NativeWindow right) => !left.Equals(right);

    public event EventHandler Disposed = null!;
    override protected void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Disposed.Invoke(this, EventArgs.Empty);
    }
    
    public bool Equals(NativeWindow? other) => _window.Equals(other!._window);
    public override bool Equals(object? obj) => obj is NativeWindow other && Equals(other);
    
    public override int GetHashCode() => _window.GetHashCode();
}