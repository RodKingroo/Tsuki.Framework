namespace Tsuki.Framework;

public class WindowIcon
{
    private Image[]? _images;
    public Image[] Images 
    { 
        get => _images!; 
        set => _images = value; 
    }

    public WindowIcon(Image[] images)
    {
        Images = images;
    }

    private WindowIcon()
    {

    }
}