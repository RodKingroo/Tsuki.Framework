using System.Runtime.InteropServices;
using Tsuki.Framework.Graphics.WGL.API;

namespace Tsuki.Framework.Graphics.WGL;

public partial class WGL
{
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglCreateContext")]
    public extern static nint CreateContext(nint hDc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDeleteContext")]
    public extern static bool DeleteContext(nint oldContext);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetCurrentContext")]
    public extern static nint GetCurrentContext();

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglMakeCurrent")]
    public extern static bool MakeCurrent(nint hDc, nint newContext);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglChoosePixelFormat")]
    public extern static int ChoosePixelFormat(nint hDc, PixelFormatDescriptor pPfd);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDescribePixelFormat")]
    public extern static int DescribePixelFormat(nint hdc, int ipfd, int cjpfd, PixelFormatDescriptor ppfd);
    
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetCurrentDC")]
    public extern static nint GetCurrentDC();

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetProcAddress")]
    public extern static nint GetProcAddress(string lpszProc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetProcAddress")]
    public extern static nint GetProcAddress(nint lpszProc);
        
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetPixelFormat")]
    public extern static int GetPixelFormat(nint hdc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglSetPixelFormat")]
    public extern static bool SetPixelFormat(nint hdc, int ipfd, PixelFormatDescriptor ppfd); 
    
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglSwapBuffers")]
    public extern static bool SwapBuffers(nint hdc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglShareLists")]
    public extern static bool ShareLists(nint hrcSrvShare, nint hrcSrvSource);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglCreateContextAttribsARB")]
    public static extern nint CreateContextAttribsARB(nint hDC, nint hShareContext, int attribList);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetExtensionsStringARB")]
    public static extern nint GetExtensionsStringARB(nint hdc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetPixelFormatAttribivARB")]
    public static extern bool GetPixelFormatAttribivARB(nint hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int piAttributes, out int piValues);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetPixelFormatAttribfvARB")]
    public static extern bool GetPixelFormatAttribfvARB(nint hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int piAttributes, out double pfValues);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglChoosePixelFormatARB")]
    public static extern bool ChoosePixelFormatARB(nint hdc, int piAttribIList, double pfAttribFList, uint nMaxFormats, out int piFormats, out uint nNumFormats);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglMakeContextCurrentARB")]
    public static extern bool MakeContextCurrentARB(nint hDrawDC, nint hReadDC, nint hglrc);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetCurrentReadDCARB")]
    public static extern nint GetCurrentReadDCARB();

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglCreatePbufferARB")]
    public static extern nint CreatePbufferARB(nint hDC, int iPixelFormat, int iWidth, int iHeight, int piAttribList);
    
    [DllImport(OpenGL.GL.Library,  EntryPoint = "wglGetPbufferDCARB")]
    public static extern nint GetPbufferDCARB(nint hPbuffer);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglReleasePbufferDCARB")]
    public static extern int ReleasePbufferDCARB(nint hPbuffer, nint hDC);
         
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDestroyPbufferARB")]
    public static extern bool DestroyPbufferARB(nint hPbuffer);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglQueryPbufferARB")]
    public static extern bool QueryPbufferARB(nint hPbuffer, int iAttribute, out int piValue);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglBindTexImageARB")]
    public static extern bool BindTexImageARB(nint hPbuffer, int iBuffer);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglReleaseTexImageARB")]
    public static extern bool ReleaseTexImageARB(nint hPbuffer, int iBuffer);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglSetPbufferAttribARB")]
    public static extern bool SetPbufferAttribARB(nint hPbuffer, int piAttribList);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetExtensionsStringEXT")]
    public static extern nint GetExtensionsStringEXT();
        
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglSwapIntervalEXT")]
    public static extern bool SwapIntervalEXT(int interval);
    
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglGetSwapIntervalEXT")]
    public static extern int GetSwapIntervalEXT();
        
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXSetResourceShareHandleNV")]
    public static extern bool DXSetResourceShareHandleNV(nint dxResource, nint shareHandle);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXOpenDeviceNV")]
    public static extern nint DXOpenDeviceNV(nint dxDevice);
        
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXCloseDeviceNV")]
    public static extern bool DXCloseDeviceNV(nint handle);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXRegisterObjectNV")]
    public static extern nint DXRegisterObjectNV(nint hDevice, nint dxObject, uint name, uint type, NV.DX.Interop access);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXUnregisterObjectNV")]
    public static extern bool DXUnregisterObjectNV(nint hDevice, nint hObject);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXObjectAccessNV")]
    public static extern bool DXObjectAccessNV(nint hObject, NV.DX.Interop access);

    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXLockObjectsN")]
    public static extern bool DXLockObjectsNV(nint hDevice, int count, nint hObjects);
    
    [DllImport(OpenGL.GL.Library, EntryPoint = "wglDXUnlockObjectsNV")]
    public static extern bool DXUnlockObjectsNV(nint hDevice, int count, nint hObjects);
}