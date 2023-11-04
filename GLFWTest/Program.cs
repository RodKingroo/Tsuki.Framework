using Tsuki.Platform.GLFW;
using Tsuki.Platform.GLFW.Structs;
using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;

public static class Program
{
    public static void Main()
    {
        var window = CreateWindow(800, 600, "Tsuki.Framework(GLFWTest)");
    }

    private static Window CreateWindow(int width, int height, string title)
    {
        var window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
        Glfw.MakeContextCurrent(window);

        var screen = Glfw.PrimaryMonitor.WorkArea;
        var x = (screen.Width - width) / 2;
        var y = (screen.Height - height) / 2;
        Glfw.SetWindowPosition(window, x, y);
        return window;
    }

}