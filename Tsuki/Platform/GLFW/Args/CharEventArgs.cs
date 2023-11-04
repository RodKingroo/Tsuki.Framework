using Tsuki.Platform.GLFW.Enums;

namespace Tsuki.Platform.GLFW.Args;

public class CharEventArgs : EventArgs
{
    public uint CodePoint { get; }
    public ModifierKeys ModifierKeys { get; }
    
    public CharEventArgs(uint codePoint, ModifierKeys modifierKeys)
    {
        CodePoint = codePoint;
        ModifierKeys = modifierKeys;
    }
    
    public string Char  => char.ConvertFromUtf32(unchecked((int) CodePoint));
}