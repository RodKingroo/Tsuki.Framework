using System.Collections;
using System.Text;
using Tsuki.Framework.Math;
using Tsuki.Framework.Platform;
using Tsuki.Framework.Platform.GLFW;

namespace Tsuki.Framework.Input.State;

public class JoystickState
{
    private Hat[]? _hats;
    private Hat[]? _hatPrev;

    private double[]? _axes;
    private double[]? _axesPrev;

    private BitArray? _buttons;
    private BitArray? _buttonPrev;

    public int Id { get; }
    public string? Name { get; }

    public int ButtonCount { get => _buttons!.Length; }
    public int AxisCount { get => _axes!.Length; }
    public int HatCount { get => _hats!.Length; }

    internal JoystickState(int hatCount, int axesCount, int buttonCount, int id, string name)
        {
            _hats = new Hat[hatCount];
            _axes = new double[axesCount];
            _buttons = new BitArray(buttonCount);

            _hatPrev = new Hat[hatCount];
            _axesPrev = new double[axesCount];
            _buttonPrev = new BitArray(buttonCount);

            Id = id;
            Name = name;
        }

    private JoystickState(JoystickState source) : this(source._hats!.Length,source._axes!.Length, source._buttons!.Length, source.Id, source.Name!)
    {
        Array.Copy(source._hats, _hats!, source._hats.Length);
        Array.Copy(source._hatPrev!, _hatPrev!, source._hatPrev!.Length);

        Array.Copy(source._axes, _axes!, source._axes.Length);
        Array.Copy(source._axesPrev!, _axesPrev!, source._axesPrev!.Length);

        _buttons = (BitArray)source._buttons.Clone();
        _buttonPrev = (BitArray)source._buttonPrev!.Clone();
    }

    public JoystickState()
    {
    }

    public bool this[int index]
    {
        get => IsButtonDown(index);
        set => SetButtonState(index, value);
    }

    public bool IsButtonDown(int index)
        => _buttons![index];

    public bool IsButtonPressed(int index)
        => IsButtonDown(index) && !WasButtonDown(index);
    
    public bool IsButtonReleased(int index)
        => !IsButtonDown(index) && WasButtonDown(index);

    public Hat GetHat(int index)
        => _hats![index];

    public Hat GetHatPrev(int index)
        => _hatPrev![index];

    public double GetAxis(int index)
        => _axes![index];

    public double GetAxisPrev(int index)
        => _axesPrev![index];

    private void SetHat(int index, Hat value)
        => _hats![index] = value;

    private void SetButtonState(int index, bool value)
        => _buttons![index] = value;

    public void SetAxis(int index, double value)
    {
        if (value < -1)
            _axes![index] = -1;
        else if (value > 1)
            _axes![index] = 1;
        else 
            _axes![index] = value;
    }

    private void SetAxes(Span<double> axes)
    {
        if(axes.Length > _axes!.Length)
            _axes = new double[axes.Length];
        axes.CopyTo(_axes);
    }

    private void SetButtonDown(int index, bool value)
    {
        _buttons![index] = value;
    }

    public bool WasButtonDown(int index)
        => _buttonPrev![index];

    public override string ToString()
    {
        string joinedHats = string.Join(", ", _hats!);
        string joinedAxes = string.Join(", ", _axes!);

        var buttonBuilder = new StringBuilder();

        for (int i = 0; i < _buttons!.Length; i++)
        {
            if(IsButtonDown(i))
                buttonBuilder.Append("down");
            else
                buttonBuilder.Append("up");
        }

        return string.Format("{{id: {0}, name: {1}, hats: [{2}], axes: [{3}], buttons: [{4}]}}",
                              Id, Name, joinedHats, joinedAxes, buttonBuilder);
    }

    public void Update()
    {
        UpdateHats();
        UpdateAxes();
        UpdateButtons();
    }

    private unsafe void UpdateHats()
    {
        MathHelper.Swap(ref _hats, ref _hatPrev);

        JoystickHats* h = GLFW.GetJoystickHatsRaw(Id, out int count);
        for (int j = 0; j < count; j++)
            SetHat(j, (Hat)h[j]);
    }

    private void UpdateAxes()
    {
        MathHelper.Swap(ref _axes, ref _axesPrev);
        
        Span<double> axes = GLFW.GetJoystickAxes(Id);
        SetAxes(axes);
    }

    private unsafe void UpdateButtons()
    {
        MathHelper.Swap(ref _buttons, ref _buttonPrev);

        JoystickInputAction* b = GLFW.GetJoystickButtonsRaw(Id, out int count);
        for (var j = 0; j < count; j++)
        {
            SetButtonDown(j, b[j] == JoystickInputAction.Press);
        }
    }

    public JoystickState GetSnapshot()
        => new JoystickState(this);

}