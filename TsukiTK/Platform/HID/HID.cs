using System.Diagnostics;

namespace Tsuki.Framework.Platform.HID;

public static partial class HID
{
    public static int TranslateJoystickAxis(Page page, int usage)
    {
        switch(page)
        {
            case Page.GenericDesktop:
                return (GenericDesktopUsage)usage switch
                {
                    GenericDesktopUsage.X => 0,
                    GenericDesktopUsage.Y => 1,
                    GenericDesktopUsage.Z => 2,
                    GenericDesktopUsage.RotationX => 3,
                    GenericDesktopUsage.RotationY => 4,
                    GenericDesktopUsage.RotationZ => 5,
                    GenericDesktopUsage.Slider => 6,
                    GenericDesktopUsage.Dial => 7,
                    GenericDesktopUsage.Wheel => 8,
                    _ => -1
                };
            case Page.Simulation:
                return (SimulationUsage)usage switch
                {
                    SimulationUsage.Rudder => 9,
                    SimulationUsage.Throttle => 10,
                    _ => -1
                };
        }

        Debug.Print("[Input] Unknown axis with HID page/usage {0}/{1}", page, usage);
        return 0;
    }
}