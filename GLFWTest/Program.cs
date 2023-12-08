using System.Runtime.InteropServices;
using Tsuki.Graphics.Khronos.Enums;
using Tsuki.Platform.GLFW;
using Tsuki.Platform.GLFW.Enums;
using Tsuki.Platform.GLFW.Structs;
using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;

public static class Program
{
    private const string Title = "WindowTest";
    private const int Width = 800;
    private const int Height = 600;
    
    private const int GlColorBufferBit = 0x00004000;

    private delegate void GlClearColorHandler(float r, float g, float b, float a);

    private delegate void GlClearHandler(int mask);

    private static GlClearColorHandler glClearColor;
    private static GlClearHandler glClear;

    private static Random random;
    
    public static void Main()
    {
        Glfw.WindowHint(Hint.ClientApi, ClientApi.Gl);
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenGlProfile, Profile.Core);
        Glfw.WindowHint(Hint.DoubleBuffer, true);
        Glfw.WindowHint(Hint.Decorated, true);
        Glfw.WindowHint(Hint.OpenGlForwardCompatible, true);
        
        random = new Random();
        
        var window = Glfw.CreateWindow(Width, Height, Title, Monitor.None, Window.None);
        Glfw.MakeContextCurrent(window);
        
        Glfw.SwapInterval(1);
        
        var screen = Glfw.PrimaryMonitor.WorkArea;
        var x = (screen.Width - Width) / 2;
        var y = (screen.Height - Height) / 2;
        Glfw.SetWindowPosition(window, x, y);
        
        Glfw.SetKeyCallback(window, KeyCallback);
        
        glClearColor = Marshal.GetDelegateForFunctionPointer<GlClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
        glClear = Marshal.GetDelegateForFunctionPointer<GlClearHandler>(Glfw.GetProcAddress("glClear"));
        
        var tick = 0L;
        ChangeRandomColor();
        
        while (!Glfw.WindowShouldClose(window))
        {
            Glfw.PollEvents();
            Glfw.SwapBuffers(window);
            
            if(tick++ % 100 == 0)
                ChangeRandomColor();
        
            glClear(GlColorBufferBit);
        }
    }

    private static void ChangeRandomColor()
    {
        var r = random.NextSingle();
        var g = random.NextSingle();
        var b = random.NextSingle();
        glClearColor(r, g, b, 1);
    }

    private static void KeyCallback(Window window, Keys key, int scancode, InputState state, ModifierKeys mods)
    {
        switch (key)
        {
            case Keys.Escape:
                Glfw.SetWindowShouldClose(window, true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
    }
}