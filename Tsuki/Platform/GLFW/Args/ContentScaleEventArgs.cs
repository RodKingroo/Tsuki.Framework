namespace Tsuki.Platform.GLFW.Args;

public class ContentScaleEventArgs : EventArgs
{
    public float XScale { get; }
    public float YScale { get; }

    public ContentScaleEventArgs(float xScale, float yScale)
    {
        XScale = xScale;
        YScale = yScale;
    }
}