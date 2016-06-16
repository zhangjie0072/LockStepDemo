using UnityEngine;
using System.Collections.Generic;

public class PractiseItem : MonoBehaviour
{
    private GameObject _widget;
    private UILabel _title;
    private UILabel _intro;
    private GameObject _ok;
    private UIGrid _bonus_grid;
    private List<GameObject> _awards = new List<GameObject>();
    private GameObject _mark;
    private int _bonus_depth;

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
            _intro.text = _practise.intro;
            SetAwards(_practise.awards);
        }
    }

    public bool completed
    {
        set 
        {
            NGUITools.SetActive(_mark, value); 
            if (value)
            {
                foreach (GameObject go in _awards)
                {
                    go.GetComponentInChildren<UISprite>().spriteName = 
                        go.GetComponentInChildren<UISprite>().spriteName.Replace("edged", "gray");
                }
            }
            else
            {
                foreach (GameObject go in _awards)
                {
                    go.GetComponentInChildren<UISprite>().spriteName =
                        go.GetComponentInChildren<UISprite>().spriteName.Replace("gray", "edged");
                }
            }
        }
        get { return NGUITools.GetActive(_mark); }
    }

    public delegate void Delegate(PractiseItem item);
    public Delegate onEnter;

    void Awake()
    {
        _widget = transform.FindChild("Widget").gameObject;
        _title = transform.FindChild("Widget/Title").GetComponent<UILabel>();
        _intro = transform.FindChild("Widget/Intro").GetComponent<UILabel>();
        _ok = transform.FindChild("Widget/OK").gameObject;
        _bonus_grid = transform.FindChild("Widget/BonusGrid").GetComponent<UIGrid>();
        _mark = transform.FindChild("Widget/Mark").gameObject;
        _bonus_depth = _mark.GetComponent<UIWidget>().depth - 1;
        UIEventListener.Get(_widget).onClick = OnClick;
        UIEventListener.Get(_ok).onClick = OnClick;
    }

    private void OnClick(GameObject go)
    {
        if (onEnter != null)
            onEnter(this);
    }

    private void SetAwards(Dictionary<uint, uint> awards)
    {
        while (_bonus_grid.transform.childCount > 0)
        {
            NGUITools.Destroy(_bonus_grid.transform.GetChild(0).gameObject);
        }
        _awards.Clear();

        foreach (KeyValuePair<uint, uint> award in awards)
        {
            GameObject item = GameSystem.Instance.mClient.mUIManager.CreateUI("NumericBonusItem", _bonus_grid.transform);

            string spriteName = string.Empty;
            if (award.Key == GlobalConst.DIAMOND_ID)
                spriteName = "diamond";
            else if (award.Key == GlobalConst.GOLD_ID)
                spriteName = "gold";
            else if (award.Key == GlobalConst.TEAM_EXP_ID)
                spriteName = "exp";

            if (completed)
                spriteName += "_gray";
            else
                spriteName += "_edged";

            item.GetComponentInChildren<UISprite>().spriteName = spriteName;
            item.GetComponentInChildren<UISprite>().depth = _bonus_depth;
            item.GetComponentInChildren<UILabel>().text = "+" + award.Value;
            item.GetComponentInChildren<UILabel>().depth = _bonus_depth;
            _awards.Add(item);
        }
        _bonus_grid.Reposition();
    }
}
