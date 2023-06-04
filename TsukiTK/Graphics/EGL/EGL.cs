using System.Runtime.InteropServices;
using Tsuki.Framework.Graphics.EGL.API;

namespace Tsuki.Framework.Graphics.EGL;

using EGLNativeDisplayType = IntPtr;
using EGLNativeWindowType = IntPtr;
using EGLNativePixmapType = IntPtr;
using EGLConfig = IntPtr;
using EGLContext = IntPtr;
using EGLDisplay = IntPtr;
using EGLSurface = IntPtr;
using EGLClientBuffer = IntPtr;
public static partial class EGL
{
    [DllImport(Library, EntryPoint = "eglQuerySurfacePointerANGLE")]
    public static extern bool QuerySurfacePointerANGLE(EGLDisplay display, EGLSurface surface, int attribute, out IntPtr value);
    
    [DllImport(Library, EntryPoint = "eglGetPlatformDisplayEXT")]
    public static extern EGLDisplay GetPlatformDisplay(int platform, EGLNativeDisplayType displayId, int[] attribList);

    [DllImport(Library, EntryPoint = "eglGetError")]
    public static extern ErrorCode GetError();

    [DllImport(Library, EntryPoint = "eglGetDisplay")]
    public static extern EGLDisplay GetDisplay(EGLNativeDisplayType display_id);

    [DllImport(Library, EntryPoint = "eglTerminate")]
    public static extern bool Terminate(EGLDisplay dpy);

    [DllImport(Library, EntryPoint = "eglQueryString")]
    public static extern nint QueryString(EGLDisplay dpy, int name);

    [DllImport(Library, EntryPoint = "eglGetConfigs")]
    public static extern bool GetConfigs(EGLDisplay dpy, EGLConfig[] configs, int config_size, out int num_config);

    [DllImport(Library, EntryPoint = "eglChooseConfig")]
    public static extern bool ChooseConfig(EGLDisplay dpy, int[] attrib_list, EGLConfig[] configs, int config_size, out int num_config);

    [DllImport(Library, EntryPoint = "eglGetConfigAttrib")]
    public static extern bool GetConfigAttrib(EGLDisplay dpy, EGLConfig config, int attribute, out int value);

    [DllImport(Library, EntryPoint = "eglCreateWindowSurface")]
    public static extern EGLSurface CreateWindowSurface(EGLDisplay dpy, EGLConfig config, nint win, nint attrib_list);

    [DllImport(Library, EntryPoint = "eglCreatePbufferSurface")]
    public static extern EGLSurface CreatePbufferSurface(EGLDisplay dpy, EGLConfig config, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglCreatePixmapSurface")]
    public static extern EGLSurface CreatePixmapSurface(EGLDisplay dpy, EGLConfig config, EGLNativePixmapType pixmap, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglDestroySurface")]
    public static extern bool DestroySurface(EGLDisplay dpy, EGLSurface surface);

    [DllImport(Library, EntryPoint = "eglQuerySurface")]
    public static extern bool QuerySurface(EGLDisplay dpy, EGLSurface surface, int attribute, out int value);

    [DllImport(Library, EntryPoint = "eglBindAPI")]
    public static extern bool BindAPI(EGL_Renderable api);

    [DllImport(Library, EntryPoint = "eglQueryAPI")]
    public static extern int QueryAPI();

    [DllImport(Library, EntryPoint = "eglWaitClient")]
    public static extern bool WaitClient();

    [DllImport(Library, EntryPoint = "eglReleaseThread")]
    public static extern bool ReleaseThread();

    [DllImport(Library, EntryPoint = "eglCreatePbufferFromClientBuffer")]
    public static extern EGLSurface CreatePbufferFromClientBuffer(EGLDisplay dpy, int buftype, EGLClientBuffer buffer, EGLConfig config, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglSurfaceAttrib")]
    public static extern bool SurfaceAttrib(EGLDisplay dpy, EGLSurface surface, int attribute, int value);

    [DllImport(Library, EntryPoint = "eglBindTexImage")]
    public static extern bool BindTexImage(EGLDisplay dpy, EGLSurface surface, int buffer);

    [DllImport(Library, EntryPoint = "eglReleaseTexImage")]
    public static extern bool ReleaseTexImage(EGLDisplay dpy, EGLSurface surface, int buffer);

    [DllImport(Library, EntryPoint = "eglSwapInterval")]
    public static extern bool SwapInterval(EGLDisplay dpy, int interval);

    [DllImport(Library, EntryPoint = "eglCreateContext")]
    public static extern nint CreateContext(EGLDisplay dpy, EGLConfig config, EGLContext share_context, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglDestroyContext")]
    public static extern bool DestroyContext(EGLDisplay dpy, EGLContext ctx);

    [DllImport(Library, EntryPoint = "eglMakeCurrent")]
    public static extern bool MakeCurrent(EGLDisplay dpy, EGLSurface draw, EGLSurface read, EGLContext ctx);

    [DllImport(Library, EntryPoint = "eglGetCurrentContext")]
    public static extern EGLContext GetCurrentContext();

    [DllImport(Library, EntryPoint = "eglGetCurrentSurface")]
    public static extern EGLSurface GetCurrentSurface(int readdraw);

    [DllImport(Library, EntryPoint = "eglGetCurrentDisplay")]
    public static extern EGLDisplay GetCurrentDisplay();

    [DllImport(Library, EntryPoint = "eglQueryContext")]
    public static extern bool QueryContext(EGLDisplay dpy, EGLContext ctx, int attribute, out int value);

    [DllImport(Library, EntryPoint = "eglWaitGL")]
    public static extern bool WaitGL();

    [DllImport(Library, EntryPoint = "eglWaitNative")]
    public static extern bool WaitNative(int engine);

    [DllImport(Library, EntryPoint = "eglSwapBuffers")]
    public static extern bool SwapBuffers(EGLDisplay dpy, EGLSurface surface);

    [DllImport(Library, EntryPoint = "eglCopyBuffers")]
    public static extern bool CopyBuffers(EGLDisplay dpy, EGLSurface surface, EGLNativePixmapType target);

    [DllImport(Library, EntryPoint = "eglGetProcAddress")]
    public static extern nint GetProcAddress(string funcname);

    [DllImport(Library, EntryPoint = "eglGetProcAddress")]
    public static extern nint GetProcAddress(nint funcname);

    [DllImport(Library, EntryPoint = "eglGetPlatformDisplayEXT")]
    public static extern EGLDisplay GetPlatformDisplayEXT(int platform, EGLNativeDisplayType native_display, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglCreatePlatformWindowSurfaceEXT")]
    public static extern EGLSurface CreatePlatformWindowSurfaceEXT(EGLDisplay dpy, EGLConfig config, EGLNativeWindowType native_window, int[] attrib_list);

    [DllImport(Library, EntryPoint = "eglCreatePlatformPixmapSurfaceEXT")]
    public static extern EGLSurface CreatePlatformPixmapSurfaceEXT(EGLDisplay dpy, EGLConfig config, EGLNativePixmapType native_pixmap, int[] attrib_list);

    public static EGLNativeDisplayType SOFTWARE_DISPLAY_ANGLE = new EGLNativeDisplayType(-1);
    public static EGLNativeDisplayType D3D11_ELSE_D3D9_DISPLAY_ANGLE = new EGLNativeDisplayType(-2);
    public static EGLNativeDisplayType D3D11_ONLY_DISPLAY_ANGLE = new EGLNativeDisplayType(-3);

}