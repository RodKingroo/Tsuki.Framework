using System.Diagnostics;

namespace Tsuki.Framework.Graphics;

public class GraphicsMode : IEquatable<GraphicsMode>
{
    private int samples;

    private static GraphicsMode? defaultMode;
    private static object SyncRoot = new object();

    public nint? Index { get; set; } = null;

    public ColorFormat ColorFormat { get; set; }
    public int Depth { get; set; }

    public int Stencil { get; set; }
    public int Samples { get; set; }
    public ColorFormat AccumulatorFormat { get; set; }

    public int Buffers { get; set; }
    
    public bool Stereo { get; set; } 

    static GraphicsMode()
    {

    }

    public GraphicsMode(GraphicsMode mode) 
        : this(mode.ColorFormat, mode.Depth, mode.Stencil, mode.Samples, mode.AccumulatorFormat, mode.Buffers, mode.Stereo) 
    { 

    }

    public GraphicsMode(ColorFormat color, int depth) 
        : this(color, depth, Default.Stencil, Default.Samples, Default.AccumulatorFormat, Default.Buffers, Default.Stereo)
    {

    }

    public GraphicsMode(ColorFormat color, int depth, int stencil)
            : this(color, depth, stencil, Default.Samples, Default.AccumulatorFormat, Default.Buffers, Default.Stereo)
    {

    }

    public GraphicsMode(ColorFormat color, int depth, int stencil, int samples)
        : this(color, depth, stencil, samples, Default.AccumulatorFormat, Default.Buffers, Default.Stereo)
    {

    }

    public GraphicsMode(ColorFormat color, int depth, int stencil, int samples, ColorFormat accum)
        : this(color, depth, stencil, samples, accum, Default.Buffers, Default.Stereo)
    {

    }

    public GraphicsMode(ColorFormat color, int depth, int stencil, int samples, ColorFormat accum, int buffers)
        : this(color, depth, stencil, samples, accum, buffers, Default.Stereo)
    {

    }

     public GraphicsMode(ColorFormat color, int depth, int stencil, int samples, ColorFormat accum, int buffers, bool stereo)
        : this(null, color, depth, stencil, samples, accum, buffers, stereo) 
    { 

    }

    public GraphicsMode(IntPtr? index, ColorFormat color, int depth, int stencil, int samples, ColorFormat accum, int buffers, bool stereo)
    {
        Index = index;
        ColorFormat = color;
        Depth = depth;
        Stencil = stencil;
        Samples = samples;
        AccumulatorFormat = accum;
        Buffers = buffers;
        Stereo = stereo;
    }

    public static GraphicsMode Default
    {
        get
        {
            lock(SyncRoot)
            {
                if(defaultMode == null)
                {
                    defaultMode = new GraphicsMode(null, 32, 16, 0, 0, 0, 2, false);
                    Debug.Print("GraphicsMode.Default = {0}", defaultMode.ToString());
                }
                return defaultMode;
            }
        }
    }

    public override string ToString()
        => string.Format("Index: {0}, Color: {1}, Depth: {2}, Stencil: {3}, Samples: {4}, Accum: {5}, Buffers: {6}, Stereo: {7}",
                         Index, ColorFormat, Depth, Stencil, Samples, AccumulatorFormat, Buffers, Stereo);

    public override int GetHashCode()
        => Index.GetHashCode();

    public override bool Equals(object? obj)
        => obj is GraphicsMode && Equals((GraphicsMode)obj);

    public bool Equals(GraphicsMode? other)
        => this == other;

}