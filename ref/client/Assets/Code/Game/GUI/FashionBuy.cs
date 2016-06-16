using UnityEngine;
using System.Collections;

public class FashionBuy : MonoBehaviour {


    private FashionShopConfigItem _configItem;


    public int _select = -1;

    public delegate void OnBuySelectOne(FashionBuy fashionB);
    public OnBuySelectOne _onBuySelectOne = null;


    public FashionShopConfigItem ConfigItem
    {
        get
        {
            return _configItem;
        }
        set
        {
            _configItem = value;
            uint fashionId = _configItem._fashionID;

            // Set Name
            UILabel itemName = transform.FindChild("itemName").GetComponent<UILabel>();
            itemName.text = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId).name;

            // Set Icon.
            UISprite itemSprite = transform.FindChild("itemIcon").GetComponent<UISprite>();
            string iconStr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId).icon;
            itemSprite.spriteName = iconStr;
            itemSprite.atlas = ResourceLoadManager.Instance.GetAtlas(_configItem.getAtlas());
            itemSprite.MakePixelPerfect();

            UIGrid grid = transform.FindChild("gridSelectTimes").GetComponent<UIGrid>();

            for( int i =0; i < _configItem._costType.Count; i++ )
            {
                GameObject newSelectTimeItem = UIManager.Instance.CreateUI("FashionBuy_selectTime", grid.transform);

                // TOOD: set the icon latter.
                // set the icon.


                // set the cost.
                UILabel costNumLabel = newSelectTimeItem.transform.FindChild("Property/Num").GetComponent<UILabel>();

                uint costType;
                uint cost = _configItem.getActuallyCost(i, out costType);
                
     
                string costStr = string.Format("{0}", cost);
                costNumLabel.text = costStr;

                UISprite typeIcon = newSelectTimeItem.transform.FindChild("Property/Icon").GetComponent<UISprite>();

                if (1 == costType )
                {
                    typeIcon.spriteName = "com_property_diamond2";
                }
                else if (2 == costType )
                {
                    typeIcon.spriteName = "com_property_gold2";
                }
               


                UILabel dayLabel = newSelectTimeItem.transform.FindChild("day").GetComponent<UILabel>();
                uint day = _configItem.getActualDayDur()[i];
                if (day == 0)
                {
                    dayLabel.text = CommonFunction.GetConstString("UI_FASHION_FORVER_DURING");
                }
                else
                {
                    string dayStr = string.Format(CommonFunction.GetConstString("UI_FASHION_BUY_DAY"), day);
                    if( _configItem.isMine())
                    {
                        dayStr = string.Format(CommonFunction.GetConstString("UI_FASHION_RENEW_DAYS"), day);
                    }
                    
                    dayLabel.text = dayStr;
                }




                UIButton buyBtn = newSelectTimeItem.transform.FindChild("buyBtn").GetComponent<UIButton>();
                UIEventListener.Get(buyBtn.gameObject).onClick = OnClickSelectBuy;
                grid.AddChild(newSelectTimeItem.transform);

            }


        }
    }



    void OnClickSelectBuy( GameObject go )
    {
        UILabel costLabel =go.transform.parent.FindChild("Property/Num").GetComponent<UILabel>();
        int value = int.Parse(costLabel.text);


        int vCount = _configItem._timeDur.Count;
        for( int i = 0; i < vCount; i++)
        {
            uint type;
            uint cost = _configItem.getActuallyCost(i, out type);
            if( value == cost )
            {
                _select = i;
                break;
            }
        }


        //if (_configItem._isDiscount == 1 )
        //{
        //    for (int i = 0; i < _configItem._discountCost.Count; i++)
        //    {
        //        if (value == _configItem._discountCost[i])
        //        {
        //            _select = i;
        //            break;
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < _configItem._costNum.Count; i++)
        //    {
        //        if (value == _configItem._costNum[i])
        //        {
        //            _select = i;
        //            break;
        //        }
        //    }

        //}



        if( _onBuySelectOne != null )
        {
            _onBuySelectOne(this);
        }
    }

    void OnClickClose(GameObject go )
    {
        GameObject.Destroy(transform.gameObject);
    }

    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () {
        UIButton closeBtn = transform.FindChild("PopupFrame/TitleBar/Close").GetComponent<UIButton>();
        UIEventListener.Get(closeBtn.gameObject).onClick = OnClickClose;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
