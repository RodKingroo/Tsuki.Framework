namespace Tsuki.Framework.Input.Events;

public struct FileDropEventArgs
{
    public string[] FileNames { get; }
    public FileDropEventArgs(string[] paths)
    {
        FileNames = paths;
    }
}