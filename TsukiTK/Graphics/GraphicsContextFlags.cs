namespace Tsuki.Framework.Graphics;

[Flags]
public enum GraphicsContextFlags
{
    Default = 0,
    Debug = 1,
    ForwardCompatible = 2,
    Embedded = 4,
    Offscreen = 8,
    Angle = 16,
    AngleD3D9 = 32,
    AngleD3D11 = 64,
    AngleOpenGL = 128,

}