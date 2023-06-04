namespace Tsuki.Framework.Platform.GLFW;

public unsafe class GLFWGraphicsContext : IGLFWGraphicsContext
{
    private Window* _windowPtr;
    public nint WindowPtr => (nint)_windowPtr;

    public GLFWGraphicsContext(Window* windowPtr)
    {
        _windowPtr = windowPtr;
    }

    public bool IsCurrent
    {
        get => GLFW.GetCurrentContext() == _windowPtr;
    }

    private int _swapInterval;
    public int SwapInterval 
    { 
        get => _swapInterval; 
        set 
        {
            GLFW.SwapInterval(value);
            _swapInterval = value;
        }
    }

    public void MakeCurrent()
    {
        GLFW.MakeContextCurrent(_windowPtr);
    }

    public void MakeNoneCurrent()
    {
        GLFW.MakeContextCurrent(null);
    }

    public void SwapBuffers()
    {
        GLFW.SwapBuffers(_windowPtr);
    }
}