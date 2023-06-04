namespace Tsuki.Framework.Platform.HID;

public enum Page : ushort
{
    Undefined = HID.Undefined,
    GenericDesktop = HID.GenericDesktop,
    Simulation = HID.Simulation,
    VR = HID.VR,
    Sport = HID.Sport,
    Game = HID.Game,
    GenericDevice = HID.GenericDevice, 
    KeyboardOrKeypad = HID.KeyboardOrKeypad,
    LEDs = HID.LEDs,
    Button = HID.Button,
    Ordinal = HID.Ordinal,
    Telephony = HID.Telephony,
    Consumer = HID.Consumer,
    Digitizer = HID.Digitizer,
    PID = HID.PID,
    Unicode = HID.Unicode,
    AlphanumericDisplay = HID.AlphanumericDisplay,
    MedicalInstruments = HID.MedicalInstruments,
    PowerDevice = HID.PowerDevice,
    BatterySystem = HID.BatterySystem,
    BarCodeScanner = HID.BarCodeScanner,
    Scale = HID.Scale,
    MagneticStripeReader = HID.MagneticStripeReader,
    CameraControl = HID.CameraControl,
    Arcade = HID.Arcade,
    VendorDefinedStart = HID.VendorDefinedStart
}