using UnityEngine;
using System.Collections;

public class Fashion_OBB_item_selectTime_subItem : MonoBehaviour {


    public delegate void OnSelectDelegate(Fashion_OBB_item_selectTime_subItem subItem);
    public OnSelectDelegate _onSelectDelegate;

    uint _days;
    int _index = -1;



    public int Index
    {
        set
        {
            _index = value;
        }
        get
        {
            return _index;
        }
    }

    public uint Days
    {
        set
        {
            _days = value;
            UILabel Label = transform.FindChild("Label").GetComponent<UILabel>();

            if( _days == 0 )
            {
                Label.text = CommonFunction.GetConstString("UI_FASHION_FOREVER"); 
            }
            else
            {
                string timeStr = string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_DAY"), _days);
                Label.text = timeStr;
            }

        }
        get
        {
            return _days;
        }
    }


    void OnSelectClick( GameObject go )
    {
        if( _onSelectDelegate != null )
        {
            _onSelectDelegate(this);
        }
    }

	// Use this for initialization
	void Start () {
        UIButton btn = transform.GetComponent<UIButton>();
        UIEventListener.Get(btn.gameObject).onClick = OnSelectClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
