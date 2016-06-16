using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class Fashion_OBB : MonoBehaviour {

    private UIEventListener.VoidDelegate _voidBuyYes;
    private UIEventListener.VoidDelegate _voidBuyNo;

    private UIStoreFashion _uiStoreFashion;
    private UIGrid _itemGrid;

    UILabel _diamondCostLabel = null;
    UILabel _goldCostLabel = null;
    UIButton _buyButton = null;
    List<Fashion_OBB_item> _obbItems = new List<Fashion_OBB_item>();


    void BuyYes( GameObject go )
    {
        BuyStoreGoods msg = new BuyStoreGoods();
        FashionOperation msgOper = null;

        msg.store_id = StoreType.ST_FASHION;

        uint goldCost = 0;
        uint diamondCost = 0;

        foreach (KeyValuePair<uint, UIStoreFashion.FashionSelectedItem> item in _uiStoreFashion._fashionSelected._dressOnItems)
        {
            FashionShopConfigItem configItem = item.Value._config;
            Fashion_OBB_item obbItem = GetOBBItem(configItem._fashionID);

            if (obbItem == null || !obbItem._isSelect )
            {
                continue;
            }
            if( item.Value._selectIndex < 0 )
            {
                continue;
            }

            uint costType;
            uint costNum = configItem.getActuallyCost(item.Value._selectIndex, out costType);

            if (costType == 1)
            {
                diamondCost += costNum;
            }
            else if (costType == 2)
            {
                goldCost += costNum;
            }


            if (configItem.isInDate())
            {
                if (null == msgOper)
                {
                    msgOper = new FashionOperation();
                }

                FashionOperationInfo info = new FashionOperationInfo();
                info.type = (uint)FashionOperationType.FOT_RENEW;
                info.uuid = configItem.getGood().GetUUID();
                info.subtype = (uint)item.Value._selectIndex + 1;
                msgOper.info.Add(info);
                if( GlobalConst.IS_FASHION_OPEN == 1)
                {
                    msgOper.role_id = MainPlayer.Instance.CaptainID;
                }

            }
            else
            {
                if (item.Value._selectIndex >= 0)
                {
                    uint type;
                    uint cost = item.Value._config.getActuallyCost(item.Value._selectIndex, out type);

                    int pos = GameSystem.Instance.FashionShopConfig.configsSort.IndexOf(item.Value._config);

                    BuyStoreGoodsInfo info = new BuyStoreGoodsInfo();
                    info.pos = (uint)pos + 1;
                    info.type = (uint)item.Value._selectIndex + 1;
                    msg.info.Add(info);
                }
            }

        }


        if ((diamondCost > MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree) || goldCost > MainPlayer.Instance.Gold)
        {
            CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_MONEY_NOT_ENGOUGH"));
            return;
        }

		/*
        PlatNetwork.Instance.BuyStoreGoodsRequest(msg);
        if (msgOper != null)
        {
            PlatNetwork.Instance.UpdateFashionRequest(msgOper);
        }
		*/
    }

    void BuyNo( GameObject go )
    {

    }


    void buyClick( GameObject go )
    {
        uint buyCount = 0;
        foreach (KeyValuePair<uint, UIStoreFashion.FashionSelectedItem> item in _uiStoreFashion._fashionSelected._dressOnItems)
        {
            if( item.Value._selectIndex >= 0 )
            {
                buyCount++;
            }
        }
        if (0 == buyCount)
        {
            CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_YOU_ARE_NOT_SELECT_FASHION"));
            return;
        }



        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("UI_FASHON_CONFIROM_OBB"), null, _voidBuyYes, _voidBuyNo);
        return; 
        
  
    }
    public  void onDropDownDelegate(uint fashionId)
    {
        foreach( Fashion_OBB_item item in _obbItems )
        {
            if( item.ConfigItem._fashionID != fashionId )
            {
                item.closeDropDown();
            }
        }
    }

    public UIStoreFashion setUIStoreFashion
    {
        set
        {
            _uiStoreFashion = value;

            _obbItems.Clear();
            foreach (KeyValuePair<uint, UIStoreFashion.FashionSelectedItem> item in _uiStoreFashion._fashionSelected._dressOnItems)
            {
                GameObject go = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Fashion_OBB_item") as GameObject;

                GameObject oneInGO = CommonFunction.InstantiateObject(go, _itemGrid.transform);
                Fashion_OBB_item script = oneInGO.GetComponent<Fashion_OBB_item>();
                oneInGO.SetActive(true);
                script.ConfigItem = item.Value._config;
                script._onItemChangeDelegate = OnItemChangeDelegate;
                script._onDropDownDelegate = onDropDownDelegate;
                script.updateDropDown();

                _obbItems.Add(script);
           
            }   
        }
    }


    Fashion_OBB_item GetOBBItem( uint fashionID )
    {
        foreach( Fashion_OBB_item item in _obbItems )
        {
            if( item._configItem != null && item._configItem._fashionID == fashionID )
            {
                return item;
            }
        }
        return null;
    }


    void OnCloseClick( GameObject go )
    {
        GameObject.Destroy(transform.gameObject);
    }
    void UpdateCost()
    {
        uint diamondCost = 0;
        uint goldCost = 0;
        foreach (KeyValuePair<uint, UIStoreFashion.FashionSelectedItem> item in _uiStoreFashion._fashionSelected._dressOnItems)
        {

            Fashion_OBB_item obbItem = GetOBBItem(item.Value._config._fashionID);

            if (obbItem == null || !obbItem._isSelect)
            {
                continue;
            }
            if( item.Value._selectIndex >= 0 )
            {
                uint type;
                uint cost = item.Value._config.getActuallyCost(item.Value._selectIndex,out type);

                if( type ==1)
                {
                    diamondCost += cost;
                }
                else if( 2 == type )
                {
                    goldCost += cost;
                }         
            }
        }



        _diamondCostLabel.text = string.Format("{0}", diamondCost);
      //  _goldCostLabel.text = string.Format("{0}", goldCost);
    }
    void OnItemChangeDelegate(Fashion_OBB_item item)
    {
        _uiStoreFashion._fashionSelected.ChangeState(item.ConfigItem._fashionID, item._selectIndex);
        UpdateCost();
    }



    void Awake()
    {
        _itemGrid = transform.FindChild("itemGrid").GetComponent<UIGrid>();
        _diamondCostLabel = transform.FindChild("total/diamondNum").GetComponent<UILabel>();
        //_goldCostLabel = transform.FindChild("total").FindChild("goldNum").GetComponent<UILabel>();
        _buyButton = transform.FindChild("buy").GetComponent<UIButton>();


        
        UIEventListener.Get(_buyButton.gameObject).onClick = buyClick;

        _voidBuyYes = BuyYes;
        _voidBuyNo = BuyNo;

        
    }
	// Use this for initialization
	void Start () {
       // _itemGrid = transform.FindChild("itemGrid").GetComponent<UIGrid>();
        UpdateCost();
        Vector3 pos = transform.localPosition;
        pos.z = -500;
        transform.localPosition = pos;

        UIEventListener.Get(transform.FindChild("PopupFrame/TitleBar/Close").gameObject).onClick = OnCloseClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
