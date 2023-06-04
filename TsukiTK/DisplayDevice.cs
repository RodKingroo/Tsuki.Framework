using Tsuki.Framework.Graphics.Primitives;
using Tsuki.Framework.Platform;

namespace Tsuki.Framework;

public class DisplayDevice
{
    private bool primary;
    private Rectangle bounds;
    private DisplayResolution current_resolution = new DisplayResolution();
    private List<DisplayResolution> available_res = new List<DisplayResolution>();
    private IList<DisplayDevice>? available_res_readonly;

    internal object? ID;

    private static object display_lock = new object();
    private static DisplayDevice? primary_display;

    private static IDisplayDeviceDriver? implementation;

    static DisplayDevice()
    {
    }

}