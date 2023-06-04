namespace Tsuki.Framework.Input.Event;

public struct TextInputEventArgs
{
    public int Unicode { get; }

    public TextInputEventArgs(int unicode)
    {
        Unicode = unicode;
    }

    public string AsString => char.ConvertFromUtf32(Unicode);
}