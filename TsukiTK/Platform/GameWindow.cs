using System.Diagnostics;
using Tsuki.Framework.Input.Event;
using static Tsuki.Framework.Platform.GLFW.GLFW;

namespace Tsuki.Framework.Platform;

public class GameWindow : NativeWindow
{
    public event Action? Load;
    public event Action? Unload;
    public event Action<FrameEventArgs>? UpdateFrame;
    public event Action? RenderThreadStarted;
    public event Action<FrameEventArgs>? RenderFrame;
    private const double MaxFrequency = 500;
    private Stopwatch _watchRender = new Stopwatch();
    private Stopwatch _watchUpdate = new Stopwatch();
    protected bool IsRunningSlowly { get; set; }
    private double _updateEpsilon;
    private double _renderFrequency;

    public double RenderFrequency
    {
        get => _renderFrequency;
        set
        {
            switch (value)
            {
                case <= 1:
                    _renderFrequency = 0;
                    break;
                case <= MaxFrequency:
                    _renderFrequency = value;
                    break;
                default:
                    Debug.Print("Target render frequency clamped to {0}Hz.", MaxFrequency);
                    _renderFrequency = MaxFrequency;
                    break;
            }
        }
    }

    private double _updateFrequency;

    public double UpdateFrequency
    {
        get => _updateFrequency;
        set
        {
            switch (value)
            {
                case < 1:
                    _updateFrequency = 0;
                    break;
                case <= MaxFrequency:
                    _updateFrequency = value;
                    break;
                default:
                    Debug.Print("Target render frequency clamped to {0}Hz.", MaxFrequency);
                    _updateFrequency = MaxFrequency;
                    break;
            }
        }
    }

    public GameWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(nativeWindowSettings)
    {
        IsMultiThreaded = gameWindowSettings.IsMultiThreaded;

        RenderFrequency = gameWindowSettings.RenderFrequency;
        UpdateFrequency = gameWindowSettings.UpdateFrequency;
    }

    private Thread? _renderThread;

    public bool IsMultiThreaded { get; }

    public double RenderTime { get; set; }
    public double UpdateTime { get; set; }

    public virtual unsafe void Run()
    {
        Context?.MakeCurrent();

        OnLoad();

        OnResize(new ResizeEventArgs(Size));

        Debug.Print("Entering main loop.");
        if (IsMultiThreaded)
        {
            Context?.MakeNoneCurrent();

            _renderThread = new Thread(StartRenderThread);
                _renderThread.Start();
        }

        _watchRender.Start();
        _watchUpdate.Start();
        while (WindowShouldClose(WindowPtr) == false)
        {
            double timeToNextUpdateFrame = DispatchUpdateFrame();

            double sleepTime = timeToNextUpdateFrame;
            if (!IsMultiThreaded)
            {
                double timeToNextRenderFrame = DispatchRenderFrame();

                sleepTime = System.Math.Min(sleepTime, timeToNextRenderFrame);
            }

            if (sleepTime > 0)
                Thread.Sleep((int)System.Math.Floor(sleepTime * 1000));
        }

        OnUnload();
    }

    public virtual void SwapBuffers()
    {
        if (Context == null)
            throw new InvalidOperationException("Cannot use SwapBuffers when running with ContextAPI.NoAPI.");

        Context.SwapBuffers();
    }

    public override void Close()
        => base.Close();

    protected virtual void OnUnload()
        => Unload?.Invoke();

    protected virtual void OnLoad()
        => Load?.Invoke();

    private unsafe void StartRenderThread()
    {
        Context?.MakeCurrent();

        OnRenderThreadStarted();
        _watchRender.Start();
        while (WindowShouldClose(WindowPtr) == false)
            DispatchRenderFrame();
    }

    protected virtual void OnRenderThreadStarted()
        => RenderThreadStarted?.Invoke();

    private double DispatchRenderFrame()
    {
        double elapsed = _watchRender.Elapsed.TotalSeconds;
        double renderPeriod = RenderFrequency == 0 ? 0 : 1 / RenderFrequency;
        if (elapsed > 0 && elapsed >= renderPeriod)
        {
            _watchRender.Restart();
            RenderTime = elapsed;
            OnRenderFrame(new FrameEventArgs(elapsed));

            if (VSync == VSyncMode.Adaptive && IsRunningSlowly)
                SwapInterval(0);
            else if(VSync == VSyncMode.Adaptive && !IsRunningSlowly)
                SwapInterval(1);
        }

        if (RenderFrequency == 0) return 0;
        else return (renderPeriod - elapsed);
    }

    private double DispatchUpdateFrame()
    {
        int isRunningSlowlyRetries = 4;
        double elapsed = _watchUpdate.Elapsed.TotalSeconds;

        double updatePeriod;
        if (UpdateFrequency == 0)
            updatePeriod = 0;
        else
            updatePeriod = 1 / UpdateFrequency;

        while (elapsed > 0 && elapsed + _updateEpsilon >= updatePeriod)
        {
            ProcessInputEvents();
            ProcessWindowEvents(IsEventDriven);

            _watchUpdate.Restart();
            UpdateTime = elapsed;
            OnUpdateFrame(new FrameEventArgs(elapsed));

           _updateEpsilon += elapsed - updatePeriod;

            if (UpdateFrequency <= double.Epsilon)
            {
                break;
            }

            IsRunningSlowly = _updateEpsilon >= updatePeriod;

            if (IsRunningSlowly && --isRunningSlowlyRetries == 0)
            {
                _updateEpsilon = 0;
                break;
            }

            elapsed = _watchUpdate.Elapsed.TotalSeconds;
        }

        if (UpdateFrequency == 0) return 0;
        else return updatePeriod - elapsed;
    }

    protected virtual void OnUpdateFrame(FrameEventArgs args)
        => UpdateFrame?.Invoke(args);

    protected virtual void OnRenderFrame(FrameEventArgs args)
        => RenderFrame?.Invoke(args);

}