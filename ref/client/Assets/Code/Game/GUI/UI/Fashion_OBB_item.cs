using UnityEngine;
using System.Collections;

public class Fashion_OBB_item : MonoBehaviour {

   
    UISprite _icon;
    UILabel _nameLabel;
    UILabel _costNumLabel;
    UIButton _timeSelectBtn = null;
    UIButton _checkBoxBtn = null;
    UISprite _costTypeSprite = null;
    UIGrid _selectSubItemGrid = null;
    GameObject _timeSelectFatherGO = null;
    GameObject _timeSelectGO = null;

    UISprite _selectSprite = null;

    public Fashion_OBB_item_selectTime.onDropDownDelegate _onDropDownDelegate;
    bool _displayMultiple = false;
    Fashion_OBB_item_selectTime _selectTimeScript = null;

    public int _selectIndex;

    public delegate void OnItemChangeDelegate(Fashion_OBB_item item);
    public OnItemChangeDelegate _onItemChangeDelegate;

    public  FashionShopConfigItem _configItem = null;




    public bool _isSelect = true;
    public FashionShopConfigItem ConfigItem
    {
        set
        {
            _configItem = value;
            _icon.atlas = ResourceLoadManager.Instance.GetAtlas(_configItem.getAtlas());
            _icon.spriteName = _configItem.getSpriteName();
            _icon.MakePixelPerfect();
            _icon.width = 80;
            _icon.height = 80;


            _nameLabel.text = _configItem.getName();

            _selectTimeScript.ConfigItem = _configItem;

            OnSelectDayChange(0);
        }
        get
        {
            return _configItem;
        }
    }

    void Awake()
    {
        _icon = transform.FindChild("icon").GetComponent<UISprite>();
        _nameLabel = transform.FindChild("name").GetComponent<UILabel>();
        _costTypeSprite = transform.FindChild("cost/costType").GetComponent<UISprite>();
        _costNumLabel = transform.FindChild("cost/costNum").GetComponent<UILabel>();


        _timeSelectFatherGO = transform.FindChild("qixian/timeSelectFather").gameObject;
        _checkBoxBtn = transform.FindChild("checkBox").GetComponent<UIButton>();
        UIEventListener.Get(_checkBoxBtn.gameObject).onClick = OnCheckBoxClick;

        GameObject selectTimeItemGO = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Fashion_OBB_item_selectTime") as GameObject;

        _timeSelectGO = CommonFunction.InstantiateObject(selectTimeItemGO, _timeSelectFatherGO.transform);
        _selectTimeScript = _timeSelectGO.GetComponent<Fashion_OBB_item_selectTime>();
        _selectTimeScript._selectTimeChangeDelegate = selectTimeChangeDelegate;
        _selectTimeScript._onDropDownDelegate = _onDropDownDelegate;
        
        _selectSprite = transform.FindChild("checkBox").FindChild("select").GetComponent<UISprite>();

    }


    public void closeDropDown()
    {
        _selectTimeScript.updateDropDownClick(false);
    }

    public void updateDropDown()
    {
        _selectTimeScript._onDropDownDelegate = _onDropDownDelegate;
    }



    void OnCheckBoxClick( GameObject go )
    {
        _isSelect = !_isSelect;

        if (_isSelect)
        {
            OnSelectDayChange(0);
            
        }
        else
        {
            OnSelectDayChange(-1);
        }
        _selectSprite.gameObject.SetActive(_isSelect);

        if (_onItemChangeDelegate != null)
        {
            _onItemChangeDelegate(this);
        }


    }


    private void OnSelectDayChange( int index )
    {
        _selectIndex = index;
        if( index < 0 )
        {
            return;
        }
        uint type;
        _costNumLabel.text = string.Format("{0}",_configItem.getActuallyCost(index, out type));
        if( type == 1 )
        {
            _costTypeSprite.spriteName = "com_property_diamond2";
        }
        else if ( type == 2 )
        {
            _costTypeSprite.spriteName = "com_property_gold2";
        }

    }


    void selectTimeChangeDelegate(Fashion_OBB_item_selectTime selectTIme)
    {
        // TODO: on the select time change...
        OnSelectDayChange(selectTIme.selectIndex);
       
        if( _onItemChangeDelegate!= null)
        {
            _onItemChangeDelegate(this);
        }

    }

    void OnClickDropDown( GameObject go )
    {
        if( !_displayMultiple )
        {
            _displayMultiple = true;
        }
        else
        {
            _displayMultiple = false;
        }

    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
