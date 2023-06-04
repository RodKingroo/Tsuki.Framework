using static Tsuki.Framework.Platform.GLFW.GLFW;
using static Tsuki.Framework.Platform.GLFW.GLFWCallbacks;
using static Tsuki.Framework.Platform.GLFW.GLFWProvider;

namespace Tsuki.Framework.Platform;

public static class Joysticks
{
    public static event JoystickCallback? JoystickCallback;

    private static JoystickCallback _joystickCallback;


    static Joysticks()
    {
        EnsureInitialized();
        _joystickCallback = (id, state) => JoystickCallback!(id, state);
        SetJoystickCallback(_joystickCallback);

    }
}