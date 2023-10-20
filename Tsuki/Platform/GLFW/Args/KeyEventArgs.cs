using Tsuki.Platform.GLFW.Enums;

namespace Tsuki.Platform.GLFW.Args;

public class KeyEventArgs : EventArgs
{
    public Keys Key { get; }
    public int ScanCode { get; }
    public InputState State { get; }
    public ModifierKeys Modifiers { get; }

    public KeyEventArgs(Keys key, int scanCode, InputState state, ModifierKeys modifiers)
    {
        Key = key;
        ScanCode = scanCode;
        State = state;
        Modifiers = modifiers;
    }
    
}