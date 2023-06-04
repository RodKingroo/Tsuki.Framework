using Tsuki.Framework.Context;

namespace Tsuki.Framework.Platform.GLFW;

public class GLFWBindingsContext : IBindingsContext
{
    public nint GetProcAddress(string procName)
    {
        return GLFW.GetProcAddress(procName);
    }
}