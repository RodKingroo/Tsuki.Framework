using System.Diagnostics;
using System.Reflection;
using static Tsuki.Framework.Platform.GLFW.GLFWCallbacks;
using static Tsuki.Framework.Platform.GLFW.GLFW;

namespace Tsuki.Framework.Platform.GLFW;

public static class GLFWProvider
{
    private static readonly ErrorCallback ErrorCallback = (errorCode, description)
        => throw new GLFWException(description, errorCode);

    private static Thread? _mainThread;

    public static bool IsOnMainThread => CheckForMainThread == false || _mainThread == Thread.CurrentThread;
    public static bool CheckForMainThread { get; set; } = true;

    private static bool Initialized = false;

    public static void EnsureInitialized()
    {
        if(CheckForMainThread)
        {
            if(_mainThread == null)
            {
                if (Thread.CurrentThread.IsBackground == false &&
                    Thread.CurrentThread.IsThreadPoolThread == false &&
                    Thread.CurrentThread.IsAlive)
                {
                    MethodInfo correctEntryMethod = Assembly.GetEntryAssembly()!.EntryPoint!;
                    StackTrace trace = new StackTrace();
                    StackFrame[] frames = trace.GetFrames();
                    for(int i = frames.Length - 1; i >= 0; i--)
                    {
                        if(correctEntryMethod == frames[i].GetMethod()!)
                        {
                            _mainThread = Thread.CurrentThread;
                            break;
                        }
                    }
                }
            }
            if (_mainThread?.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
                throw new GLFWException("GLFW can only be called from the main thread!");
        }
        if (Initialized == false)
        {
            Init();
            SetErrorCallback(ErrorCallback);
            Initialized = true;
        }
    }

}