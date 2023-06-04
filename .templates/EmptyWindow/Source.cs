using Tsuki.Framework.Math.Vector;
using Tsuki.Framework.Platform;

namespace EmptyWindow;

public static class Program
{
    private static void Main()
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2(512, 512),
            Title = "Empty Window",
            Flags = ContextFlags.ForwardCompatible
        };

        using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
        {
            window.Run();
        }
    }
}
