namespace Tsuki.Framework.Graphics.EGL.API;

public enum ErrorCode : int
{
    Success = EGL.Success,
    NotInitialized = EGL.NotInitialized,
    BadAccess = EGL.BadAccess,
    BadAlloc = EGL.BadAlloc,
    BadAttribute = EGL.BadAttribute,
    BadConfig = EGL.BadConfig,
    BadContext = EGL.BadContext,
    BadCurrentSurface = EGL.BadCurrentSurface,
    BadDisplay = EGL.BadDisplay,
    BadMatch = EGL.BadMatch,
    BadNativePixmap = EGL.BadNativePixmap,
    BadNativeWindow = EGL.BadNativeWindow,
    BadParameter = EGL.BadParameter,
    BadSurface = EGL.BadSurface,
    ContextLost = EGL.ContextLost,
}