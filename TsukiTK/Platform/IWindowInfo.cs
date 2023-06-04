namespace Tsuki.Framework.Platform;

public interface IWindowInfo : IDisposable
{
    nint Handle { get; }
}