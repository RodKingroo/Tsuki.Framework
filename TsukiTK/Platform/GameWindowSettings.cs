namespace Tsuki.Framework.Platform;

public class GameWindowSettings
{
    public static GameWindowSettings Default = new GameWindowSettings();
    
    public bool IsMultiThreaded { get; set; } = false;
    
    public double RenderFrequency { get; set; } = 0;
    
    public double UpdateFrequency { get; set; } = 0.0;

}