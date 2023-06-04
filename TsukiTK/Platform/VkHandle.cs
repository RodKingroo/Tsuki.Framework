namespace Tsuki.Framework.Platform;

public struct VkHandle
{
    public nint Handle;

    public VkHandle(nint handle)
    {
        Handle = handle;
    }
}