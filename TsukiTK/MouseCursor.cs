namespace Tsuki.Framework;

public sealed class MouseCursor : Image
{
    internal StandardShape Shape { get; }
    public int X { get; }
    public int Y { get; }

    private MouseCursor(StandardShape shape)
    {
        Shape = shape;
    }

    public MouseCursor(int hotX, int hotY, int width, int height, byte[] data)
        : base(width, height, data)
    {
        X = hotX;
        Y = hotY;
    }

    public static MouseCursor Default { get; } = new MouseCursor(StandardShape.Arrow);
    public static MouseCursor IBeam { get; } = new MouseCursor(StandardShape.IBeam);
    public static MouseCursor Crosshair { get; } = new MouseCursor(StandardShape.Crosshair);
    public static MouseCursor Hand { get; } = new MouseCursor(StandardShape.Hand);
    public static MouseCursor VResize { get; } = new MouseCursor(StandardShape.VResize);
    public static MouseCursor HResize { get; } = new MouseCursor(StandardShape.HResize);

    public static MouseCursor Empty { get; } = new MouseCursor
    (
        0, 0, 16, 16, new byte[16 * 16 * 4]
    );
}