using Tsuki.Framework.Input.Enums;

namespace Tsuki.Framework.Input.Events;

public struct KeyboardKeyEventArgs
{
    public Keys Key { get; }
    public int ScanCode { get; }
    public KeyModifiers Modifiers { get; }
    public bool  IsRepeat { get; }

    public KeyboardKeyEventArgs(Keys key, int scanCode, KeyModifiers modifiers, bool isRepeat)
    {
        Key = key;
        ScanCode = scanCode;
        Modifiers = modifiers;
        IsRepeat = isRepeat;
    }

    public bool Alt => Modifiers.HasFlag(KeyModifiers.Alt);
    public bool Control => Modifiers.HasFlag(KeyModifiers.Control);
    public bool Shift => Modifiers.HasFlag(KeyModifiers.Shift);
    public bool Command => Modifiers.HasFlag(KeyModifiers.Super);
}