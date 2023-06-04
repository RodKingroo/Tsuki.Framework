namespace Tsuki.Framework.Input.State;

public struct GamepadState
{
    public byte[] Buttons = new byte[15];
    public double[] Axes = new double[6];

    public GamepadState()
    {
    }
}