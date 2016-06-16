using fogs.proto.msg;

public class ShootStrength
{
    public enum Mode
    {
        Normal,
        Absolute,
    }

    private bool _running;
    public bool running
    {
        get { return _running; }
        private set
        {
            if (!value)
            {
                if (_running)
                {
                    rate_adjustment = IM.Number.zero;
                    for (int i = 0; i < config.sections.Count; ++i)
                    {
                        if (_ratio <= config.sections[i].end_time && (i == 0 || _ratio >= config.sections[i - 1].end_time))
                        {
                            if (mode == Mode.Absolute)
                                rate_adjustment = (config.sections[i].section == 1) ? -IM.Number.one : IM.Number.one;
                            else
                                rate_adjustment = config.sections[i].value;
                            break;
                        }
                    }
                }
            }

            _running = value;
        }
    }

    public bool unfinished { get; private set; }

    public Mode mode = Mode.Normal;

    private bool _auto_stop;

    public IM.Number rate_adjustment;

    public ArticleStrength config { get; private set; }

    public IM.Number total_seconds;
    private IM.Number _elapsed_time;
    public IM.Number ratio { get { return _ratio; } }
    private IM.Number _ratio;

    public ShootStrength(PositionType position)
    {
        config = GameSystem.Instance.ArticleStrengthConfig.GetConfig(position);
    }

    public void Update(IM.Number deltaTime)
    {
        if (running)
        {
            _elapsed_time += deltaTime;
            _ratio = _elapsed_time / total_seconds;
            if (_ratio >= IM.Number.one)
            {
                _ratio = IM.Number.one;
                running = false;
            }
        }
    }

    public void Begin()
    {
        _elapsed_time = IM.Number.zero;
        _ratio = IM.Number.zero;
        rate_adjustment = IM.Number.zero;
        running = true;
        unfinished = true;
        if (_auto_stop)
            Stop();
    }

    public void Stop()
    {
        if (!running)
            _auto_stop = true;
        running = false;
    }

    public void End()
    {
        _auto_stop = false;
        unfinished = false;
    }
}
