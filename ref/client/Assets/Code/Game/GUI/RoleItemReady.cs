using fogs.proto.msg;
using UnityEngine;

public class RoleItemReady : MonoBehaviour
{
	public enum Status
	{
		Host,
		Ready,
	}
    private UISprite _bg;
    private UISprite _icon;
    private UILabel _level;
    private UILabel _name;
    private UISprite _position_bg;
    private UISprite _position;
    private UISprite _quality;
    private UISprite _symbol;
    private UILabel _control_mode;
	private UISprite _status_sprite;
    private UIWidget _signal_container;
    private UISprite[] _signal = new UISprite[3];
    private GameObject _replace;

	[HideInInspector]
	public UIWidget widget;

	public string name
	{
		set { if (_name) _name.text = value; }
	}

    public bool bg_visible
    {
        set { if(_bg) _bg.enabled = value; }
    }

	private Status _status;
	public Status status
	{
		get { return _status; }
		set
		{
			_status = value;
			if (_status_sprite)
			{
				switch (_status)
				{
					case Status.Host:
						_status_sprite.spriteName = "host";
						break;
					case Status.Ready:
						_status_sprite.spriteName = "ready";
						break;
				}
			}
		}
	}

    private uint _role_id;
    public uint role_id
    {
        set
        {
            _role_id = value;
            Clear();
            if (_role_id != 0)
            {
                RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(_role_id);
                string portAtlas = "Atlas/icon/iconPortrait";
                if (_role_id >= 1000 && _role_id < 1500)
                    portAtlas = portAtlas;
                else if (_role_id < 1800)
                    portAtlas += "_1";
                else if (role_id < 2000)
                    portAtlas += "_2";
                else
                    Debug.Log("cannot getPortrait by id=" + _role_id);

                _icon.atlas = ResourceLoadManager.Instance.GetAtlas(portAtlas);
                _icon.spriteName = data.icon;

                if (_name)
                    _name.text = data.name;
                string position = ((PositionType)data.position).ToString();
                if (_position_bg)
                    _position_bg.spriteName = position + "_select_bg";
                if (_position)
                    _position.spriteName = position;
                Player role = MainPlayer.Instance.GetRole(_role_id);
                if (role != null)
                {
                    quality = role.GetQuality();
                }
            }
        }
        get { return _role_id; }
    }

    public uint level
    {
        set { if (_level) _level.text = "Lv" + value; }
    }

    private uint _signal_count = 1;
    public uint signal_count
    {
        set
        {
            _signal_count = value;
            for (uint i = 0; i < 3; ++i)
            {
                _signal[i].gameObject.SetActive(i < value);
            }
        }
        get { return _signal_count; }
    }

    public uint quality
    {
        set
        {
            if (_quality)
            {
                string quality_string = ((QualityType)value).ToString();
                _quality.spriteName = CommonFunction.GetQualityString(quality_string);
                if (_symbol)
                    _symbol.spriteName = CommonFunction.GetQualitySymbolString(quality_string);
            }
        }
    }

    public bool player_control
    {
        set
        {
            if (_control_mode)
            {
                _control_mode.text = value ? CommonFunction.GetConstString("PLAYER_USER_CONTROL") : CommonFunction.GetConstString("PLAYER_AI_CONTROL");
                _control_mode.gameObject.SetActive(true);
            }
        }
    }

    public UIEventListener.VoidDelegate onReplace
    {
        get { return UIEventListener.Get(_replace).onClick; }
        set { 
			UIEventListener.Get(_replace).onClick = value;
			UIEventListener.Get(gameObject).onClick = value;
		}
    }

    public void SetSignal(uint index, bool on)
    {
        if (index < signal_count)
        {
            _signal[index].spriteName = on ? "signal" : "signal_off";
        }
    }

    void Awake()
    {
		widget = GetComponent<UIWidget>();

        _bg = transform.GetComponent<UISprite>();
        _icon = transform.FindChild("Icon").GetComponent<UISprite>();
        Transform level = transform.FindChild("Level");
        if (level)
            _level = level.GetComponent<UILabel>();
        Transform name = transform.FindChild("Name");
        if (name)
            _name = name.GetComponent<UILabel>();
        Transform position = transform.FindChild("Position");
        if (position)
        {
            _position_bg = position.GetComponent<UISprite>();
            _position = _position_bg.transform.FindChild("Text").GetComponent<UISprite>();
        }
        Transform quality = transform.FindChild("Quality");
        if (quality)
        {
            _quality = quality.GetComponent<UISprite>();
            Transform symbol = _quality.transform.FindChild("Symbol");
            if (symbol)
                _symbol = symbol.GetComponent<UISprite>();
        }
        Transform control_mode = transform.FindChild("ControlMode");
        if (control_mode)
            _control_mode = control_mode.GetComponent<UILabel>();
		Transform status = transform.FindChild("Status");
		if (status)
			_status_sprite = status.GetComponent<UISprite>();
        _signal_container = transform.FindChild("Signals").GetComponent<UIWidget>();
        for (int i = 0; i < 3; ++i)
        {
            _signal[i] = _signal_container.transform.GetChild(i).GetComponent<UISprite>();
        }
        Transform replace = transform.FindChild("Replace");
        if (replace)
            _replace = replace.gameObject;
        Clear();
    }

    public void Clear()
    {
        bg_visible = false;
        _icon.spriteName = "icon_none";
        if (_level)
            _level.text = "";
        if (_name)
            _name.text = "";
        if (_position_bg)
            _position_bg.spriteName = "";
        if (_position)
            _position.spriteName = "";
        if (_quality)
            _quality.spriteName = "";
        if (_symbol)
            _symbol.spriteName = "";
        if (_control_mode)
            _control_mode.gameObject.SetActive(false);
    }
}
