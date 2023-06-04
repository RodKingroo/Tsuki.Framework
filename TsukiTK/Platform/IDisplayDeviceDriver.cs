namespace Tsuki.Framework.Platform;

public interface IDisplayDeviceDriver
{
    bool TryChangeResolution(DisplayDevice device, DisplayResolution resulution);
    bool TryRestoreResolution(DisplayDevice device);
    DisplayDevice GetDisplay(DisplayIndex displayIndex);
    List<DisplayDevice> AvailableDevices { get; }
}