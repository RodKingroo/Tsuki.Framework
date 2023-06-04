namespace Tsuki.Framework.Input.Event;

public struct FileDropEventArgs
{
    public string[] FileNames { get; }
    public FileDropEventArgs(string[] paths)
    {
        FileNames = paths;
    }
}