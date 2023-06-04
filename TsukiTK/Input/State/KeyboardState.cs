using System.Collections;

namespace Tsuki.Framework.Input.State;

public class KeyboardState
{
    /*
        Keys.Menu - Last Key to enum
    */
    private BitArray _keys = new BitArray((int)Keys.Menu  + 1);

    private BitArray _keysPrev = new BitArray((int)Keys.Menu + 1);


    private KeyboardState(KeyboardState source)
    {
        _keys = (BitArray)source._keys!.Clone();
        _keysPrev = (BitArray)source._keysPrev!.Clone();
    }

    public KeyboardState()
    {

    }

    public bool this[Keys index]
    {
        get => IsKeyDown(index);
        set => SetKeyState(index, value);
    }

    public bool IsAnyKeyDown
    {
        get
        {
            foreach (var _ in from bool v in _keys!
                     where v select new { })
            {
                return true;
            }

            return false;
        }
    }

    public bool IsKeyDown(Keys index) 
        => _keys![(int)index];

    public bool IsKeyPressed(Keys index)
        => _keys![(int)index] && !_keysPrev![(int)index];

    public bool IsKeyReleased(Keys index)
        => !_keys![(int)index] && _keysPrev![(int)index];

    public void Update()
    {
        _keysPrev!.SetAll(false);
        _keysPrev.Or(_keys!);
    }

    public void SetKeyState(Keys index, bool down)
        => _keys![(int)index] = down;

    public bool WasKeyDown(Keys index)
        => _keysPrev![(int)index];

    public KeyboardState GetShapshot() => new KeyboardState(this);

    public override string ToString()
    {
        string result = "";
        bool first = true;
        for (Keys index = 0; index <= Keys.Menu; index++)
        {
            if(IsKeyDown(index))
            {
                if (!first)
                    result = string.Format("[{0}{1}]", index, ", ");
                else
                    result = string.Format("[{0}{1}]", index, string.Empty);
            }
        }

        return result;
    }
}