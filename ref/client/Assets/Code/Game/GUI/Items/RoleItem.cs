using fogs.proto.msg;
using UnityEngine;
using System.Collections.Generic;

public class RoleItem : MonoBehaviour
{
	public enum Status
	{
		None,
		Captain,
		Selected,
		Candidate,
	}
	private GameObject _blankBG;
	private UIWidget _widget;
	private UISprite _background;
    private UILabel _name;
    private UISprite _icon;
    private UILabel _level;
    private UISprite _quality;
    private UISprite _position;
	private GameObject _captain;
    private GameObject _button_join;
    private GameObject _button_replace;

	private RoleBaseData2 data;

	public List<string> backgrounds = new List<string>();
	public int index
	{
		set
		{
			_background.spriteName = backgrounds[value % backgrounds.Count];
		}
	}

	public string name
	{
		set { if (_name) _name.text = value; }
	}

	private Status _status;
	public Status status
	{
		get { return _status; }
		set
		{
			_status = value;
			NGUITools.SetActive(_widget.gameObject, _status != Status.None);
			NGUITools.SetActive(_captain, _status == Status.Captain);
			NGUITools.SetActive(_button_join, _status == Status.Candidate);
			NGUITools.SetActive(_button_replace, _status == Status.Selected);
			NGUITools.SetActive(_level.gameObject, _status == Status.Captain);
			NGUITools.SetActive(_quality.gameObject, _status != Status.Captain);
			NGUITools.SetActive(_blankBG, _status != Status.Candidate);
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
				data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(_role_id);
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
				name = data.name;
				string position = ((PositionType)data.position).ToString();
				if (_position)
					_position.spriteName = position;
				if (status == Status.Captain)
				{
					level = MainPlayer.Instance.Level;
					name = MainPlayer.Instance.Name;
				}
				else
				{
					Player role = MainPlayer.Instance.GetRole(_role_id);
					if (role != null)
					{
						quality = role.GetQuality();
					}
				}
			}
        }
        get { return _role_id; }
    }

    public uint level
    {
        set { if (_level) _level.text = "Lv" + value; }
    }

	public PositionType position
	{
		get { return (PositionType)data.position; }
	}

    public uint quality
    {
        set
        {
            if (_quality)
            {
                string quality_string = ((QualityType)value).ToString();
                _quality.spriteName = CommonFunction.GetQualityString(quality_string);
            }
        }
    }

	public bool frameVisible
	{
		get { return NGUITools.GetActive(_blankBG); }
		set { NGUITools.SetActive(_blankBG, value); }
	}

	public delegate void RoleItemDelegate(RoleItem item);
	public RoleItemDelegate onReplace;
	public RoleItemDelegate onJoin;

    void Awake()
    {
		_blankBG = transform.FindChild("BlankBG").gameObject;
		_widget = transform.FindChild("Widget").GetComponent<UIWidget>();

		_background = _widget.transform.FindChild("Background").GetComponent<UISprite>();
		_name = _widget.transform.FindChild("Name").GetComponent<UILabel>();
        _icon = _widget.transform.FindChild("Icon").GetComponent<UISprite>();
		_level = _widget.transform.FindChild("Level").GetComponent<UILabel>();
		_quality = _widget.transform.FindChild("Quality").GetComponent<UISprite>();
		_position = _widget.transform.FindChild("Position").GetComponent<UISprite>();
		_captain = _widget.transform.FindChild("Captain").gameObject;
        _button_join = _widget.transform.FindChild("ButtonJoin").gameObject;
        _button_replace = _widget.transform.FindChild("ButtonReplace").gameObject;

		UIEventListener.Get(_button_join).onClick += (GameObject) => { if (onJoin != null) onJoin(this); };
		UIEventListener.Get(_button_replace).onClick += (GameObject) => { if (onReplace != null) onReplace(this); };

        Clear();
    }

    public void Clear()
    {
        _icon.spriteName = "icon_none";
        if (_level)
            _level.text = "";
        if (_name)
            _name.text = "";
        if (_position)
            _position.spriteName = "";
        if (_quality)
            _quality.spriteName = "";
    }
}
