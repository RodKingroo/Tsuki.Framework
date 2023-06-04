using System.Collections;
using System.Text;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Input.State;

public class MouseState
{
    public const int MaxButtons = 16;

    private BitArray _buttons = new BitArray(MaxButtons);
    private BitArray _buttonsPrev = new BitArray(MaxButtons);

    public Vector2 Position { get; set; }
    public Vector2 PositionPrev { get; set; }
    public Vector2 PositionDelta => Position - PositionPrev;

    public double X => Position.X;
    public double XPrev => PositionPrev.X;

    public double Y => Position.Y;
    public double YPrev => PositionPrev.Y;

    public Vector2 Scroll { get; set; }
    public Vector2 ScrollPrev { get; set; }
    public Vector2 ScrollDelta => Scroll - ScrollPrev;
    
    internal MouseState()
    {

    }

    private MouseState(MouseState source)
    {
        _buttons = (BitArray)source._buttons.Clone();
        _buttonsPrev = (BitArray)source._buttonsPrev.Clone();

        Position = source.Position;
        PositionPrev = source.PositionPrev;

        Scroll = source.Scroll;
        ScrollPrev = source.ScrollPrev;
    }

    public bool this[MouseButton button]
    {
        get => IsButtonDown(button);
        set => SetKeyState(button, value);
    }

    public bool IsAnyButtonDown
    {
        get
        {
            for(int i = 0; i < MaxButtons; i++)
            {
                if(_buttons[i]) return true;
            }
            return false;
        }
    }

    public override string ToString()
    {
        StringBuilder stringbuilder = new StringBuilder();
        for (int i = 0; i < MaxButtons; i++)
        {
            if (_buttons[i])
                stringbuilder.Append("1");
            else
                stringbuilder.Append("0");
        }

        return string.Format("[X={0}, Y={1}, Buttons={2}]", X, Y, stringbuilder);
    }

    public void Update()
    {
        _buttonsPrev.SetAll(false);
        _buttonsPrev.Or(_buttons);

        PositionPrev = Position;
        ScrollPrev = Scroll;
    }

    public bool IsButtonDown(MouseButton button)
        => _buttons[(int)button];

    public bool IsButtonPressed(MouseButton button)
        => _buttons[(int)button] && !_buttonsPrev[(int)button];

    public bool IsButtonReleased(MouseButton button)
        => !_buttons[(int)button] && _buttonsPrev[(int)button];

    public bool WasButtonDown(MouseButton button)
        => _buttonsPrev[(int)button];

    public bool SetKeyState(MouseButton button, bool down)
        => _buttons[(int)button] = down;
    
    public MouseState GetSnapshot() => new MouseState(this);
}