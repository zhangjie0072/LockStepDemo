using UnityEngine;

public class PlayerPane : MonoBehaviour
{
    public enum Status
    {
        Unready,
        Ready,
        Host,
    }

    private static string[] _status_text = { "", "准备", "房主" };//

    private UIWidget _widget;
    private GameObject _selection;
    private UIWidget _team_bar;
    private UIWidget[] _role_item_widget = new UIWidget[3];
    private RoleItemReady[] _role_item = new RoleItemReady[3];
    private GameObject _info;
    private GameObject _waiting_join;
    private UILabel _team_name;
    private UILabel _status;
    private UIWidget _signal_container;
    private UISprite[] _signal = new UISprite[3];

    public UIWidget widget { get { return _widget; } }

    public string team_name
    {
        set
        {
            _team_name.text = value;
            ShowWaitingJoin(false);
        }
    }

    public Status status
    {
        set
        {
            _status.text = _status_text[(int)value];
            ShowWaitingJoin(false);
        }
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

    private uint _role_count = 1;

    public bool selected
    {
        set { _selection.SetActive(value); }
        get { return _selection.activeSelf; }
    }

    public UIEventListener.VoidDelegate onClick
    {
        set { UIEventListener.Get(_widget.gameObject).onClick = value; }
        get { return UIEventListener.Get(_widget.gameObject).onClick; }
    }

    public RoleItemReady[] role_item
    {
        get { return _role_item; }
    }

    public void ShowWaitingJoin(bool show = true)
    {
        _info.SetActive(!show);
        _waiting_join.SetActive(show);
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
        _widget = transform.FindChild("Widget").GetComponent<UIWidget>();
        _selection = transform.FindChild("Widget/Selection").gameObject;
        _team_bar = transform.FindChild("Widget/TeamBar").GetComponent<UIWidget>();
        //UIEventListener.Get(_team_bar.gameObject).onClick = OnTeamBarClick;
        _info = _team_bar.transform.FindChild("Info").gameObject;
        _waiting_join = _team_bar.transform.FindChild("WaitingJoin").gameObject;
        _team_name = _info.transform.FindChild("TeamName").GetComponent<UILabel>();
        _status = _info.transform.FindChild("Status").GetComponent<UILabel>();
        _signal_container = _info.transform.FindChild("Signals").GetComponent<UIWidget>();
        for (int i = 0; i < 3; ++i)
        {
            _signal[i] = _signal_container.transform.GetChild(i).GetComponent<UISprite>();
        }
        for (uint i = 0; i < _role_count; ++i)
        {
            CreateRoleItem(i);
        }
        _widget.height = _team_bar.height + (int)(_role_item_widget[0].height * _role_count);
    }

    //void Update()
    //{ 
        //UIPanel panel = _widget.parent.GetComponent<UIPanel>();
        //panel.clipping = UIDrawCall.Clipping.SoftClip;
        //panel.baseClipRegion = new Vector4(_widget.width / 2, -_widget.height / 2 - _widget.height % 2, _widget.width, _widget.height);
        //panel.clipSoftness = new Vector2(0, 0);
    //}

    //private void OnTeamBarClick(GameObject go)
    //{
    //    ToggleRoleItem();
    //}

    //private void ToggleRoleItem()
    //{
    //    if (_role_item == null)
    //    {
    //        CreateRoleItem();
    //    }

    //    if (_role_item.gameObject.activeSelf)
    //    {
    //        TweenHeight tween_height = _widget.GetComponent<TweenHeight>();
    //        tween_height.from = _team_bar.height + _role_item.height;
    //        tween_height.to = _team_bar.height;
    //        tween_height.ResetToBeginning();
    //        tween_height.PlayForward();
    //        tween_height.AddOnFinished(OnFinished);
    //    }
    //    else
    //    {
    //        _role_item.gameObject.SetActive(true);
    //        TweenHeight tween_height = _widget.GetComponent<TweenHeight>();
    //        tween_height.from = _team_bar.height;
    //        tween_height.to = _team_bar.height + _role_item.height;
    //        tween_height.ResetToBeginning();
    //        tween_height.PlayForward();
    //        tween_height.onFinished.Clear();
    //    }
    //}

    //private void OnFinished()
    //{
    //    _role_item.gameObject.SetActive(false);
    //}

    private void CreateRoleItem(uint index)
    {
        GameObject role_item_obj = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/RoleItemPVPSelf", _widget.transform);
        NGUITools.BringForward(role_item_obj);
        UIWidget role_item_widget = role_item_obj.GetComponent<UIWidget>();
        _role_item_widget[index] = role_item_widget;
        role_item_widget.leftAnchor.target = _widget.transform;
        role_item_widget.leftAnchor.Set(0f, 0);
        role_item_widget.rightAnchor.target = _widget.transform;
        role_item_widget.rightAnchor.Set(1f, 0);
        role_item_widget.topAnchor.target = _widget.transform;
        role_item_widget.topAnchor.Set(0f, role_item_widget.height * (_role_count - index));
        role_item_widget.bottomAnchor.target = _widget.transform;
        role_item_widget.bottomAnchor.Set(0f, role_item_widget.height * (_role_count - index - 1));
        role_item_obj.SetActive(true);
        role_item_widget.ResetAnchors();
        role_item_widget.UpdateAnchors();
        _role_item[index] = role_item_obj.GetComponent<RoleItemReady>();
    }
}
