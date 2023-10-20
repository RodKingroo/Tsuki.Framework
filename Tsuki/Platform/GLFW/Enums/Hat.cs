namespace Tsuki.Platform.GLFW.Enums;

[Flags]
public enum Hat
{
    Centered = 0x00,
    Up = 0x01,
    Right = 0x02,
    Down = 0x04,
    Left = 0x08,
    RightUp = Right | Up,
    RightDown = Right | Down,
    LeftUp = Left | Up,
    LeftDown = Left | Down
}