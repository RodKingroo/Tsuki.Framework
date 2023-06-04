using Tsuki.Framework;
using Tsuki.Framework.Input.Event;
using Tsuki.Framework.Platform;

namespace EmptyWindow;

public class Window : GameWindow
{
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {

    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        if(KeyboardState.IsKeyDown(Keys.Escape))
            Close();
        base.OnUpdateFrame(e);
    }
    
}