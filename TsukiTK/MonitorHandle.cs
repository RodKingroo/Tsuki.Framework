namespace Tsuki.Framework;

public struct MonitorHandle
{
    public nint Pointer { get; }

    public MonitorHandle(nint ptr)
    {
        Pointer = ptr;
    }

    public unsafe T* ToUnsafePtr<T>() where T : unmanaged => (T*)Pointer;
}