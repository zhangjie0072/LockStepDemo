using UnityEngine;
using System.Collections;

public class Fashion_OBB_item_selectTime : MonoBehaviour {

    public delegate void selectTimeChangeDelegate(Fashion_OBB_item_selectTime selectTIme);
    public selectTimeChangeDelegate _selectTimeChangeDelegate;


    public delegate void onDropDownDelegate(uint fashionId);
    public onDropDownDelegate _onDropDownDelegate;
    int _selectIndex = -1;

    public int selectIndex
    {
        get
        {
            return _selectIndex;
        }
    }

    FashionShopConfigItem _configItem = null;
   
    public FashionShopConfigItem ConfigItem
    {
        set
        {
            _configItem = value;
            //_icon.spriteName = _configItem.getSpriteName();

            string firstTimeStr = string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_DAY"), _configItem.getActualDayDur()[0]);

            if (_configItem.getActualDayDur()[0] == 0 )
            {
                firstTimeStr = CommonFunction.GetConstString("UI_FASHION_FOREVER");
            }
            _timeDisplayLabel.text = firstTimeStr;


            for (int i = 0; i < _configItem.getActualTimeDur().Count; i++)
            {
                GameObject FFGO = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Fashion_OBB_item_selectTime_subItem") as GameObject;
                GameObject Fashion_OBB_item_selectTime_subItemGO = CommonFunction.InstantiateObject(FFGO, _selectTimeGrid.transform);

                Fashion_OBB_item_selectTime_subItem script =Fashion_OBB_item_selectTime_subItemGO.GetComponent< Fashion_OBB_item_selectTime_subItem>();
                script.Days = _configItem.getActualDayDur()[i];
                script.Index = i;
                script._onSelectDelegate += OnSubItemSelectDelegate;
                
            }




        }
    }

    void OnSubItemSelectDelegate(Fashion_OBB_item_selectTime_subItem subItem)
    {
        string firstTimeStr = string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_DAY"), subItem.Days); 
        if( subItem.Days == 0 )
        {
            firstTimeStr  = CommonFunction.GetConstString("UI_FASHION_FOREVER"); 
        }
         _timeDisplayLabel.text = firstTimeStr;


         _isShow = false;
         _selectTimeGrid.gameObject.SetActive(_isShow);

         _selectIndex = subItem.Index;

        if(_selectTimeChangeDelegate!= null)
        {
            _selectTimeChangeDelegate(this);
        }
    }

    UIButton _dropDownBtn = null;
    UILabel _timeDisplayLabel;
    UIGrid _selectTimeGrid = null;
    bool _isShow = false;
    void Awake()
    {
        _dropDownBtn = transform.GetComponent<UIButton>();
        _timeDisplayLabel = transform.FindChild("timeDisplay").FindChild("timeDisplayLabel").GetComponent<UILabel>();
        _selectTimeGrid = transform.FindChild("timeDisplay").FindChild("timeDisplayLabel").FindChild("selectTimeGrid").GetComponent<UIGrid>();


        _selectTimeGrid.gameObject.SetActive(false);
        UIEventListener.Get(_dropDownBtn.gameObject).onClick = OnDropDownClick;

    }


    void OnDropDownClick(GameObject go )
    {
        updateDropDownClick(!_isShow);
     
        if( _onDropDownDelegate!= null)
        {
            _onDropDownDelegate(_configItem._fashionID);
        }

    }

    public void updateDropDownClick( bool isShow )
    {
        _isShow = isShow;
        _selectTimeGrid.gameObject.SetActive(_isShow);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
