using Tsuki.Framework.Graphics.Primitives;
using Tsuki.Framework.Platform.GLFW;
using static Tsuki.Framework.Platform.GLFW.GLFW;

namespace Tsuki.Framework.Platform;

public unsafe class MonitorInfo
{
    private readonly MonitorHandle _handle;
    private Monitor* HandleAsPtr => _handle.ToUnsafePtr<Monitor>();
    public MonitorHandle Handle => _handle;
    public string? Name { get; set; }

    public Rectangle ClientArea { get; set; }

    public Rectangle WorkArea { get; set; }

    public int HorizontalResolution => (int)ClientArea.Size.X;
    public int VerticalResolution => (int)ClientArea.Size.Y;

    public double HorizontalScale { get; set; }
    public double VerticalScale { get; set; }

    public double HorizontalDpi { get; set; }
    public double VerticalDpi { get; set; }
    
    public int PhysicalWidth { get; set; }
    public int PhysicalHeight { get; set; }
        
    public double HorizontalRawDpi { get; set; }
    public double VerticalRawDpi { get; set; }

    private VideoMode[]? _supportedVideoModes;

    public IReadOnlyList<VideoMode>? SupportedVideoModes
    {
        get => _supportedVideoModes;
    }

    public VideoMode CurrentVideoMode { get; set; }

    public MonitorInfo(MonitorHandle handle)
    {
        GLFWProvider.EnsureInitialized();

        if (handle.Pointer == IntPtr.Zero)
            throw new ArgumentNullException(nameof(handle));

        _handle = handle;

        Name = GetMonitorName(HandleAsPtr);

        GetMonitorPos(HandleAsPtr, out int x, out int y);
        VideoMode* videoMode = GetVideoMode(HandleAsPtr);
        ClientArea = new Rectangle(x, y, x + videoMode->Width, y + videoMode->Height);

        GetMonitorWorkarea(HandleAsPtr, out int workAreaX, out int workAreaY, out int workAreaWidth, out int workAreaHeight);
        WorkArea = new Rectangle(workAreaX, workAreaY, workAreaWidth, workAreaHeight);

        GetMonitorPhysicalSize(HandleAsPtr, out int width, out int height);
        PhysicalWidth = width;
        PhysicalHeight = height;

        GetMonitorContentScale(HandleAsPtr, out double horizontalScale, out double verticalScale);
        HorizontalScale = horizontalScale;
        VerticalScale = verticalScale;

        double defaultDpi = Monitors.GetPlatformDefaultDpi();

        HorizontalDpi = defaultDpi * HorizontalScale;
        VerticalDpi = defaultDpi * VerticalScale;

        HorizontalRawDpi = CalculateDpi(HorizontalResolution, PhysicalWidth);
        VerticalRawDpi = CalculateDpi(VerticalResolution, PhysicalHeight);

        _supportedVideoModes = GetVideoModes(HandleAsPtr);
        CurrentVideoMode = *GetVideoMode(HandleAsPtr);
    }

    private double CalculateDpi(int pixels, int length) 
        => (pixels / length) * 25.4;

}