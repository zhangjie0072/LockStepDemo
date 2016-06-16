using UnityEngine;
using System.Collections.Generic;

public class PractiseResult : MonoBehaviour
{
    private UIWidget _window;
    private UILabel _title;
    private UISprite _result_bg;
    private UILabel _result;
    private GameObject _signal_pane;
    private UILabel _completion_num;
    private UISprite[] _signals;
    private UIGrid _bonus_grid;
    private GameObject _no_bonus;
    private GameObject _restart;
    private UILabel _restart_label;
    private GameObject _ok;
    private GameObject _popupFrameBtn;
    private UISprite _resultSprite;
    private UILabel _resultLabel;

    public uint practise_id
    {
        get { return _practise.ID; }
        set
        {
            practise = GameSystem.Instance.PractiseConfig.GetConfig(value);
        }
    }

    private PractiseData _practise;
    public PractiseData practise
    {
        get { return _practise; }
        set
        {
            _practise = value;
            _title.text = _practise.title;
            if (_bonus_grid.transform.childCount == 0)
                SetAwards(_practise.awards);
        }
    }

    public bool completed
    {
        set
        {
            NGUITools.SetActive(_no_bonus, !value);
            NGUITools.SetActive(_bonus_grid.gameObject, value);
            if (value)
            {
                if (_bonus_grid.transform.childCount == 0 && practise != null)
                    SetAwards(_practise.awards);
                _result.text = CommonFunction.GetConstString("MATCH_PRACTISE_SUCCESS");
                _result_bg.spriteName = "com_other_top";
                _restart_label.text = CommonFunction.GetConstString("MATCH_PRACTISE_FREE");
            }
            else
            {
                _result.text = CommonFunction.GetConstString("MATCH_PRACTISE_UNSUCCESS");
                _result_bg.spriteName = "com_gradient_transparent01";
                _restart_label.text = CommonFunction.GetConstString("MATCH_PRACTISE_RECHALLENGE");
            }
        }
        get { return !NGUITools.GetActive(_no_bonus); }
    }

    private PractiseBehaviour.ObjectiveState[] _states;
    public PractiseBehaviour.ObjectiveState[] states
    {
        set
        {
            if (value != null)
            {
                _states = value;
                if (_states.Length > 0)
                {
                    //_window.height = 896;
                    NGUITools.SetActive(_signal_pane, true);
                    SetSignals();
                }
            }
        }
        private get { return _states; }
    }

    private uint _num_complete;
    public uint num_complete
    {
        set
        {
            _num_complete = value;
            _completion_num.text = string.Format(CommonFunction.GetConstString("MATCH_PRACTISE_COMPLETION_NUM"), _num_complete, states.Length);
        }
    }

    public UIEventListener.VoidDelegate onOK
    {
        set { UIEventListener.Get(_ok).onClick = value; }
        get { return UIEventListener.Get(_ok).onClick; }
    }

    public UIEventListener.VoidDelegate onRestart
    {
        set { UIEventListener.Get(_restart).onClick = value; }
        get { return UIEventListener.Get(_restart).onClick; }
    }

    public UIEventListener.VoidDelegate onClose
    {
        set { UIEventListener.Get(_popupFrameBtn).onClick = value; }
        get { return UIEventListener.Get(_popupFrameBtn).onClick; }
    }

    void Awake()
    {
        _window = transform.FindChild("Window").GetComponent<UIWidget>();
        _title = transform.FindChild("Window/Title").GetComponent<UILabel>();
        _result_bg = transform.FindChild("Window/Result").GetComponent<UISprite>();
        _result = transform.FindChild("Window/Result/Label").GetComponent<UILabel>();
        _signal_pane = transform.FindChild("Window/Signals").gameObject;
        _completion_num = _signal_pane.transform.FindChild("Label").GetComponent<UILabel>();
        _signals = _signal_pane.GetComponentsInChildren<UISprite>();
        _bonus_grid = transform.FindChild("Window/Bonus/Grid").GetComponent<UIGrid>();
        _no_bonus = transform.FindChild("Window/Bonus/NoBonus").gameObject;
        _restart = transform.FindChild("Window/Restart").gameObject;
        _restart_label = transform.FindChild("Window/Restart/Label").GetComponent<UILabel>();
        _ok = transform.FindChild("Window/OK").gameObject;
        _popupFrameBtn = transform.FindChild("Window/PopupFrame/TitleBar/Close").gameObject;
        _resultSprite = transform.FindChild("Window/Result").GetComponent<UISprite>();
        _resultLabel = transform.FindChild("Window/Result/Label").GetComponent<UILabel>();
        //_window.height = 696;
        NGUITools.SetActive(_signal_pane, false);

    }

    private void SetAwards(Dictionary<uint, uint> awards)
    {
        while (_bonus_grid.transform.childCount > 0)
        {
            NGUITools.Destroy(_bonus_grid.transform.GetChild(0).gameObject);
        }
        if (awards.Count <= 0) 
            return;
        foreach (KeyValuePair<uint, uint> award in awards)
        {
            GameObject item = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/GoodsIconConsume", _bonus_grid.transform);
            LuaComponent luacom = item.GetComponent<LuaComponent>();
            //UIAtlas _atlas = ResourceLoadManager.Instance.GetResources("Atlas/icon/iconGoods") as UIAtlas;
            //UISprite sprite = item.transform.FindChild("Icon").GetComponent<UISprite>();
            //sprite.atlas = _atlas;
            //string spriteName = string.Empty;
            //if (award.Key == GlobalConst.DIAMOND_ID)
            //    sprite.spriteName = "com_property_diamond2";
            //else if (award.Key == GlobalConst.GOLD_ID)
            //    sprite.spriteName = "com_property_gold2";
            //else if (award.Key == GlobalConst.TEAM_EXP_ID)
            //    sprite.spriteName = "com_property_exp";

            //sprite.spriteName += "_edged";
            LuaInterface.LuaFunction func = luacom.table["SetData"] as LuaInterface.LuaFunction;
            func.Call(new object[] { luacom.table, (int)(award.Key), (int)(award.Value), false });
            //item.GetComponentInChildren<UISprite>().spriteName = sprite.spriteName;
            item.GetComponentInChildren<UISprite>().depth = 10;
            //item.GetComponentInChildren<UILabel>().text = "+" + award.Value;
            item.GetComponentInChildren<UILabel>().depth = 10;
        }
        _bonus_grid.Reposition();
    }

    private void SetSignals()
    {
        for (int i = 0; i < _signals.Length; ++i)
        {
            UISprite signal = _signals[i];
            if (i >= states.Length)
            {
                NGUITools.SetActive(signal.gameObject, false);
            }
            else
            {
                signal.spriteName = (states[i] == PractiseBehaviour.ObjectiveState.Completed ? "practice_y" : "practice_n");
            }
        }
    }
}
