namespace Tsuki.Platform.GLFW.Args;

public class FileDropEventArgs : EventArgs
{
    public string[] Filenames { get; }
    public FileDropEventArgs(string[] filenames)
    {
        Filenames = filenames;
    }
}