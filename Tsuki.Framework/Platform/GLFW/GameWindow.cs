using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;
using Window = Tsuki.Platform.GLFW.Structs.Window;

namespace Tsuki.Platform.GLFW;

public class GameWindow : NativeWindow
{
    public GameWindow()
    {
        
    }

    public GameWindow(int width, int height, string title) : base(width, height, title)
    {
        
    }

    public GameWindow(int width, int height, string title, Monitor monitor, Window share) : base(width, height, title,
        monitor, share)
    {
        
    }
}