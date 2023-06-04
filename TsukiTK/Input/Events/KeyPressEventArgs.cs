namespace Tsuki.Framework.Input.Events;

public class KeyPressEventArgs : EventArgs
{
    public char KeyChar { get; set; }
    
    public KeyPressEventArgs(char keyChar)
    {
        KeyChar = keyChar;
    }
}