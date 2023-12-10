using Tsuki.Platform.GLFW.Enums;

namespace Tsuki.Platform.GLFW.Structs;

public struct GamePadState
{
    private readonly InputState[] states;
    private readonly float[] axes;
    
    public InputState GetButtonState(GamePadButton button) => states[(int)button];
    public float GetAxis(GamePadAxis axis) => axes[(int)axis];
}