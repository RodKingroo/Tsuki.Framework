namespace Tsuki.Framework.Input.Events;

public struct MouseButtonEventArgs
{
    public MouseButton Button { get; }
    public InputAction Action { get; }
    public KeyModifiers Modifiers { get; }

    public MouseButtonEventArgs(MouseButton button, InputAction action, KeyModifiers modifiers)
    {
        Button = button;
        Action = action;
        Modifiers = modifiers;
    }

    public bool IsPressed => Action != InputAction.Release;
}