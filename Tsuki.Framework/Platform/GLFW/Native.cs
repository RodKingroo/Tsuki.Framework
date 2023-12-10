using System.Runtime.InteropServices;
using System.Text;
using static Tsuki.Platform.GLFW.Glfw;

using Monitor = Tsuki.Platform.GLFW.Structs.Monitor;
using Window = Tsuki.Platform.GLFW.Structs.Window;
using EGLSurface = Tsuki.Platform.GLFW.Structs.EglSurface;
using EGLDisplay = Tsuki.Platform.GLFW.Structs.EglDisplay;
using EGLContext = Tsuki.Platform.GLFW.Structs.EglContext;
using HGlRC = Tsuki.Platform.GLFW.Structs.Hglrc;
using NSOpenGLContext = Tsuki.Platform.GLFW.Structs.NsOpenGlContext;
using OsMesaContext = Tsuki.Platform.GLFW.Structs.OsMesaContext;
using GLXContext = Tsuki.Platform.GLFW.Structs.GLXContext;

namespace Tsuki.Platform.GLFW;

public static class Native
{
    [DllImport(Library, EntryPoint = "glfwGetCocoaMonitor")]
    public static extern uint GetCocoaMonitor(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetX11Display")]
    public static extern IntPtr GetX11Display();

    [DllImport(Library, EntryPoint = "glfwGetWaylandDisplay")]
    public static extern IntPtr GetWaylandDisplay();

    [DllImport(Library, EntryPoint = "glfwGetWaylandMonitor")]
    public static extern Monitor GetWaylandMonitor(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetWaylandWindow")]
    public static extern Window GetWaylandWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwGetGLXWindow")]
    public static extern Window GetGLXWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwGetX11Window")]
    public static extern Window GetX11Window(Window window);

    [DllImport(Library, EntryPoint = "glfwGetX11Monitor")]
    public static extern Monitor GetX11Monitor(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetX11Adapter")]
    public static extern IntPtr GetX11Adapter(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetCocoaWindow")]
    public static extern Window getCocoaWindow(Window window);

    [DllImport(Library, EntryPoint = "glfwGetNSGLContext")]
    public static extern NSOpenGLContext GetNsGlContext(Window window);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaContext")]
    public static extern OsMesaContext GetOSMesaContext(Window window);

    [DllImport(Library, EntryPoint = "glfwGetGLXContext")]
    public static extern GLXContext GetGLXContext(Window window);

    [DllImport(Library, EntryPoint = "glfwGetEGLContext")]
    public static extern EGLContext GetEglContext(Window window);

    [DllImport(Library, EntryPoint = "glfwGetEglDisplay")]
    public static extern EGLDisplay GetEglDisplay();

    [DllImport(Library, EntryPoint = "glfwGetEqlSurface")]
    public static extern EGLSurface GetEglSurface(Window window);

    [DllImport(Library, EntryPoint = "glfwGetWGLContext")]
    public static extern HGlRC GetWglContext(Window window);

    [DllImport(Library, EntryPoint = "glfwGetWin32Window")]
    public static extern Window GetWin32Window(Window window);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaColorBuffer")]
    public static extern bool GetOSMesaColorBuffer(Window window, out int width, out int height, int format,
        out IntPtr buffer);

    [DllImport(Library, EntryPoint = "glfwGetOSMesaDepthBuffer")]
    public static extern bool GetOSMesaDepthBuffer(Window window, out int width, out int height, out int bytesPerValue,
        out IntPtr buffer);

    [DllImport(Library, EntryPoint = "glfwSetX11SelectionString")]
    static extern void SetX11SelectionString(byte[] str);

    [DllImport(Library, EntryPoint = "glfwGetX11SelectionString")]
    static extern IntPtr GetX11SelectionStringInternal();

    [DllImport(Library, EntryPoint = "glfwGetWin32Adapter")]
    static extern IntPtr GetWin32AdapterInternal(Monitor monitor);

    [DllImport(Library, EntryPoint = "glfwGetWin32Monitor")]
    static extern Monitor GetWin32MonitorInternal(Monitor monitor);
    
    public static string GetX11SelectionString()
    {
        var ptr = GetX11SelectionStringInternal();
        return (ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr))!;
    }

    public static void SetX11SelectionString(string str)
    {
        SetX11SelectionString(Encoding.UTF8.GetBytes(str));
    }

    public static string GetWin32Adapter(Monitor monitor)
        => Util.PtrToStringUTF8(GetWin32AdapterInternal(monitor));

    public static string GetWin32Monitor(Monitor monitor)
        => Util.PtrToStringUTF8(GetWin32MonitorInternal(monitor));
    
}