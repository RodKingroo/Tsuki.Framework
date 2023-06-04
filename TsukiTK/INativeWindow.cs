using System.ComponentModel;
using System.Drawing;
using Tsuki.Framework.Input.Events;
using Tsuki.Framework.Platform;
using Tsuki.Framework.Platform.Windows.Native;

namespace Tsuki.Framework;

public interface INativeWindow : IDisposable
{
    Icon Icon { get; set; }
    string Title { get; set; }
    bool Focused { get; }
    bool Visible { get; set; }
    bool Exist { get; }
    IWindowInfo WindowInfo { get; }
    WindowState WindowState { get; set; }
    WindowBorder WindowBorder { get; set; }
    Graphics.Primitives.Rectangle Bounds { get; set; }
    Point Location { get; set; }
    Size size { get; set; }
    int X { get; set; }
    int Y { get; set; }

    int Width { get; set; }
    int Height { get; set; }

    Rectangle ClientRectable { get; set; }
    Size ClientSize { get; set; }
    MouseCursor Cursor { get; set; }
    bool CursorVisible { get; set; }
    bool CursorGrabbed { get; set; }
    void Close();
    void ProcessEvent();
    Point PointToClient(Point point);
    Point PointToScreen(Point point);
    event EventHandler<EventArgs> Move;
    event EventHandler<EventArgs> Resize;
    event EventHandler<CancelEventArgs> Closing;
    event EventHandler<EventArgs> Closed;
    event EventHandler<EventArgs> Disposed;
    event EventHandler<EventArgs> IconChanged;
    event EventHandler<EventArgs> TitleChanged;
    event EventHandler<EventArgs> VisibleChanged;
    event EventHandler<EventArgs> FocusedChanged;
    event EventHandler<EventArgs> WindowBorderChanged;
    event EventHandler<EventArgs> WindowStateChanged;
    event EventHandler<KeyboardKeyEventArgs> KeyDown;
    event EventHandler<KeyPressEventArgs> KeyPress;
    event EventHandler<KeyboardKeyEventArgs> KeyUp;
    event EventHandler<EventArgs> MouseLeve;
    event EventHandler<EventArgs> MouseEnter;
    event EventHandler<MouseButtonEventArgs> MouseDown;
    event EventHandler<MouseButtonEventArgs> MouseUp;
    event EventHandler<MouseMoveEventArgs> MouseMove;
    event EventHandler<MouseButtonEventArgs> MouseWheel;
    event EventHandler<FileDropEventArgs> FileDrop;

}