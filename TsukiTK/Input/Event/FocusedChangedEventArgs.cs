namespace Tsuki.Framework.Input.Event;

public struct FocusedChangedEventArgs
{
    public bool IsFocused { get; }
    
    public FocusedChangedEventArgs(bool isFocused)
    {
        IsFocused = isFocused;
    }
}