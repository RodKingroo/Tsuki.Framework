namespace Tsuki.Framework.Platform;

public interface IGraphicsContext
{
    bool IsCurrent { get; }
    int SwapInterval { get; set; }
    void SwapBuffers();
    void MakeCurrent();
    void MakeNoneCurrent();
}