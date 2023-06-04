using static Tsuki.Framework.Platform.GLFW.GLFWProvider;
using static Tsuki.Framework.Platform.GLFW.GLFW;
using Tsuki.Framework.Platform.GLFW;
using Tsuki.Framework.Math.Vector;

namespace Tsuki.Framework.Platform;

public class NativeWindowSettings
{
    public static NativeWindowSettings Default = new NativeWindowSettings();

    public MonitorHandle CurrentMonitor{ get; set; }
    public IGLFWGraphicsContext? SharedContext{ get; set; }
    public WindowIcon? Icon { get; set; }
    public bool IsEventDriven { get; set; } = false;
    public ContextApi API { get; set; } = ContextApi.OpenGL;
    public ContextProfile Profile { get; set; } = ContextProfile.Core;
    public ContextFlags Flags { get; set; } = ContextFlags.Default;
    public bool AutoLoadBindings { get; set; } = true;

    public Version APIVersion { get; set; } = new Version(3, 3);

    public string Title { get; set; } = "TsukiTK Window";

    public bool StartFocused { get; set; } = true;

    public bool StartVisible  { get; set; } = true;

    public WindowState WindowState { get; set; } = WindowState.Normal;
    public WindowBorder WindowBorder { get; set; } = WindowBorder.Resizable;
    public Vector2? Location { get; set; }
    public Vector2 Size { get; set; } = new Vector2(640, 360);

    public Vector2? MinimumSize { get; set; } = null;
    public Vector2? MaximumSize { get; set; } = null;

    public (int numerator, int denominator)? AspectRatio { get; set; } = null;

    public bool IsFullscreen { get; set; } = false;
    public int NumberOfSamples{ get; set; }

    public int? StencilBits { get; set; }
    public int? DepthBits { get; set; }
    public int? RedBits { get; set; }
    public int? GreenBits { get; set; }
    public int? BlueBits { get; set; }
    public int? AlphaBits { get; set; }
    public bool SrgbCapable { get; set; }

    public unsafe NativeWindowSettings()
    {
        EnsureInitialized();
        CurrentMonitor = new MonitorHandle((nint)GetPrimaryMonitor());
    }
}