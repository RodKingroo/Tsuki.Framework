using Tsuki.Platform.GLFW.Enums;

namespace Tsuki.Platform.GLFW.Args;

public class MouseButtonEventArgs : EventArgs
{
    public MouseButton Button { get; }
    public InputState Action { get; }
    public ModifierKeys Modifiers { get; }

    public MouseButtonEventArgs(MouseButton button, InputState state, ModifierKeys modifiers)
    {
        Button = button;
        Action = state;
        Modifiers = modifiers;
    }
}