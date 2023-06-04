using System.Runtime.Serialization;
using Tsuki.Framework.Graphics.EGL.API;

namespace Tsuki.Framework.Platform.GLFW;

public class GLFWException : Exception
{
    public ErrorCode ErrorCode { get; }

    public GLFWException()
    {

    }

    public GLFWException(string message) : base(message)
    {

    }

    public GLFWException(string message, ErrorCode errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public GLFWException(string message, Exception innerException) : base(message, innerException)
    {

    }

    protected GLFWException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        
    }
}