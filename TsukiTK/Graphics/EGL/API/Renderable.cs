namespace Tsuki.Framework.Graphics.EGL.API;

public enum EGL_Renderable : int
{
    ES = EGL.OPENGL_ES_BIT,
    ES2 = EGL.OPENGL_ES2_BIT,
    ES3 = EGL.OPENGL_ES3_BIT,
    GL = EGL.OPENGL_BIT,
    VG = EGL.OPENVG_BIT
}