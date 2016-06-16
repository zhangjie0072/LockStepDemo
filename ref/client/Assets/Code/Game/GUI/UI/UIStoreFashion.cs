using UnityEngine;
using System.Collections;
using fogs.proto.msg;
using System.Collections.Generic;
using fogs.proto.config;
public class UIStoreFashion
{

    public static UIStoreFashion tempIntance = null;

    static public UIStoreFashion Create()
    {
        if(tempIntance == null)
        {
            tempIntance = new UIStoreFashion();
        }
        return tempIntance;
    }


    static public void Destroy()
    {
        
        tempIntance = null;
        System.GC.Collect();
        
    }

    private UIEventListener.VoidDelegate _voidOneSBuyDeleOk;
    private UIEventListener.VoidDelegate _voidOneSBuyDeleCancle;

    private UIEventListener.VoidDelegate _voidDressOnAfterBuy;
    private UIEventListener.VoidDelegate _voidNotDressOnAfterBuy;
    private int _oneSBuySelect = 0;
    private int _oneSBuyIndex = 0;


    public GameObject _goReturn = null;
    public UIGrid _selectGrid = null;
    private UIScrollView _selectScrollView = null;
    private ModelShowItem _modelShowItem = null;
    private UIButton _buySelectBtn = null;
    private UIButton _myWardrobeBtn = null;
    private UISprite _fashionShopBtn = null;
    private GameObject _rootGO = null;
    private GameObject _buyOneGO = null;

    private Dictionary<uint, UIToggle> _tabs = new Dictionary<uint, UIToggle>();


    private dressInfo _oDressInfo = new dressInfo(true);
    private dressInfo _dDressInfo = new dressInfo(false);
    private dressInfo _tDressInfo = new dressInfo(false);


    private UISprite _lkHeadSprite = null;
    private UISprite _lkClothesSprite = null;
    private UISprite _lkTrousersSprite = null;
    private UISprite _lkShoesSprite = null;
    private UISprite _playerBk = null;

    private LLoad _lload = null;

    private UIButton[] _leftBtns = new UIButton[4];
    private renewInfo _renewInfo = new renewInfo();


    private List<FashionItem> _fashionItemList = new List<FashionItem>();

    UIToggle _tabAllBtn = null;
    bool _isGoBack = false;
    private WardrobeUpdate _wardrobeUpdate = new WardrobeUpdate();
    public FDItemMgr _FDItemMgr = new FDItemMgr();
    class respInfo
    {
        public uint _pos;
        public uint _type;
    }


    List<respInfo> _respInfo = new List<respInfo>();



    public void Refresh()
    {
        _isGoBack = false;
    }


    public void update()
    {
        //for(int i = 0; i < 4; i++)
        //{
        //    Logger.Log( "_dDressInfo=" + i+" : " + _dDressInfo.getDataByIndex(i) );
        //}
    }
 

    public class dressInfo
    {
        public dressInfo(bool isOriginal )
        {
            for( int i = 0; i< 4;i++)
            {
                _datas[i] = 0;
            }
            _isOrigianl = isOriginal;
        }

       public bool setValue( int index, uint value)
        {
            if( _datas[index] != 0 )
            {
                Logger.LogWarning("setValue failed for index=" + index + " _datas[index]=" + _datas[index]);
                return false;
            }

            _datas[index] = value;
            return true;
        }


        public void clearValue(int index )
        {
            _datas[index] = 0;
        }


        public bool existValueByFashionID(uint fashionId)
        {
            bool ret = false;
            for (int i = 0; i < 4; i++)
            {
                if (_datas[i] == fashionId)
                {
                    ret = true;
                }
            }
            return ret;
        }
        public bool clearValueByFashionID(uint fashionId )
        {
            bool ret = false;
            for (int i = 0; i < 4; i++ )
            {
                if( _datas[ i ] == fashionId )
                {
                    _datas[i] = 0;
                    ret = true;
                }
            }
            return ret;
        }

        public uint getDataByIndex( int index )
        {
            return _datas[index];
        }

        public List<uint> getByTypes( List<uint> types )
        {
            List<uint> ret = new List<uint>();
            if( _isOrigianl )
            {
                Logger.LogError("original should not call getByTypes");
                return ret;
            }

            foreach( uint type in types )
            {
                if( type == 0 )
                {
                    continue;
                }
                uint fashionID = _datas[type-1];

                if( fashionID != 0 )
                {
                    ret.Add(fashionID);
                }
            }
            return ret;
        }

        private uint[] _datas = new uint[4];
        private bool _isOrigianl = false;
    }



    public UIStoreFashion()
    {
        tempIntance = this;
        _voidOneSBuyDeleOk = OneSBuyOK;
        _voidOneSBuyDeleCancle = OneSBuyCancle;
        _voidDressOnAfterBuy = DressOnAfterBuy;
        _voidNotDressOnAfterBuy = NotDressOnAfterBuy;
        //MainPlayer.Instance.FashionChange += OnFashionChange;
        initData();
    }


    private void initData()
    {
        _FDItemMgr.init();
        initDressInfo();
        
        if (_btnMap == null)
        {
            _btnMap = new BtnMap(this);
        }
        else
        {
            Logger.LogError("_btnmap should be null, please check!");
        }
    }




    public class FashionItem
    {
        public GameObject _goItem;
        private uint _fashionID;
        public FashionShopItem _script;
        private uint _state;
        public uint FashionID
        {
            get
            {
                return _fashionID;
            }

            set
            {
                _fashionID = value;
                if( _goItem != null )
                {
                    UISprite itemSprite = _goItem.transform.FindChild("item").GetComponent<UISprite>();
                    string iconStr =  GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_fashionID).icon;
                    if( iconStr != null )
                    {
                        string atlas =  _script.ConfigItem.getAtlas();
                        itemSprite.atlas = ResourceLoadManager.Instance.GetAtlas(atlas);
                        itemSprite.spriteName = iconStr;
                        itemSprite.MakePixelPerfect();
                    } 
                }
                else
                {
                    Logger.LogError("set Fashion ID failed id is:" +  value);
                }
            }

         }


        public FashionItem(UIStoreFashion uiStoreFashion, GameObject parent, FashionShopConfigItem item)
        {
            GameObject go = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/FashionShopItem") as GameObject;
           
            _goItem = CommonFunction.InstantiateObject(go,parent.transform);

            _state = (uint)uiStoreFashion._state;

            
            _script = _goItem.transform.GetComponent<FashionShopItem>();
            _script.setUiStoreFashion = uiStoreFashion;
            _script._state = _state;
            
            _script.ConfigItem = item;

            FashionID = item._fashionID;

            
            _script._onDressOnDelegate = uiStoreFashion.OnDressOn;
     

            UILabel itemName = _goItem.transform.FindChild("name").GetComponent<UILabel>();
            itemName.text =  GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_fashionID).name;
            if (_state == 0 && item.isMine() )
            {
                itemName.text = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_fashionID).name; //+ CommonFunction.GetConstString("UI_FASHION_OWEN_ALREADY");
            }


            UILabel oriLabel = _goItem.transform.FindChild("cost").FindChild("costNum").GetComponent<UILabel>();
            Transform discountTransfrom = _goItem.transform.FindChild("cost").FindChild("discount");
            UILabel discountLabel = discountTransfrom.GetComponent<UILabel>();
            UISprite costLine = discountTransfrom.FindChild("line").GetComponent<UISprite>();

	    // UISprite discountSprite = _goItem.transform.FindChild("discount").GetComponent<UISprite>();
	    // UISprite newSprite = _goItem.transform.FindChild("new").GetComponent<UISprite>();

            if( _state == 0 )
            {  
                if (item._isDiscount == 0)
                {
                    oriLabel.gameObject.SetActive(true);
                    discountLabel.gameObject.SetActive(false);

                    oriLabel.text = string.Format("{0}", item._costNum[0]);         
                    //discountSprite.gameObject.SetActive(false);

                }
                else
                {
                    oriLabel.gameObject.SetActive(false);
                    discountLabel.gameObject.SetActive(true);
                    discountLabel.text = string.Format("{0}", item._costNum[0]);
                    costLine.width = discountLabel.width + 10;
                    

                    UILabel dicountLabel = discountTransfrom.FindChild("nowCost").GetComponent<UILabel>();
                    dicountLabel.text = string.Format("{0}", item._discountCost[0]);
                   // discountSprite.gameObject.SetActive(true);
                }
            }
            else if( _state == 1 )
            {
                //discountSprite.gameObject.SetActive(false);
               // newSprite.gameObject.SetActive(false);
                oriLabel.gameObject.SetActive(false);
                //discountLabel.gameObject.SetActive(false);
            }       
        }
    }



    public static string UIName
    {
        get
        {
            return typeof(UIStoreFashion).Name;
        }
    }

    public class WardrobeUpdate
    {
        public List<Goods> _oriFashion = new List<Goods>();
        public List<Goods> _newActionFashion = new List<Goods>();

        private void dressUpdateList( List<Goods> _list, uint fashionID, bool isDressOn )
        {
            List<Goods> goodList = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, fashionID );
            if (goodList.Count != 1)
            {
               // Logger.LogError("WardrobeUpdate dressUpdate error for the goodList.Count=" + goodList.Count);
                return;
            }

            Goods good = goodList[0];
            if (isDressOn)
            {
                if( _list.IndexOf(good) >= 0 )
                {
                    uint fashionId = good.GetID();
                    FashionShopConfigItem fsci = GameSystem.Instance.FashionShopConfig.GetConfig(fashionId);
                    if( fsci.getFashionType().Count == 1 )
                    {
                        Logger.LogError("dressUpdateList should count fashionType().Count==1, fashionId=" + fashionId);
                    }
                }
                else
                {
                    _list.Add(good);
                }
            }
            else
            {
                _list.Remove(good);
            }
        }

        public void dressUpdateBoth( uint fashionID, bool isDressOn )
        {
            dressUpdateList(_oriFashion, fashionID, isDressOn);
            dressUpdateList(_newActionFashion, fashionID, isDressOn);
        }

        public void dressUpdateOri(uint fashionID,bool isDressOn )
        {
            dressUpdateList(_oriFashion, fashionID, isDressOn);
        }
        public void dressUpdate(uint fashionID, bool isDressOn)
        {
            dressUpdateList(_newActionFashion, fashionID, isDressOn);   
        }

        public void sendChanges()
        {
            FashionOperation msg = null;

            foreach (Goods oriFashion in _oriFashion)
            {   
                if (_newActionFashion.IndexOf(oriFashion) < 0)
                {

                    if (!oriFashion.IsEquip())
                    {
                        Logger.LogWarning("sendChanges uneqip but fashion id=" + oriFashion.GetID() + " is not equip, so skip");
                        continue;
                    }

                    // dress down
                    if (msg == null)
                    {
                        msg = new FashionOperation();
                    }
                    
                    FashionOperationInfo info = new FashionOperationInfo();
                    info.type = (uint)FashionOperationType.FOT_UNEQUIP;
                    info.uuid = oriFashion.GetUUID();


                    if (GlobalConst.IS_FASHION_OPEN == 1)
                    {
                        msg.role_id = MainPlayer.Instance.CaptainID;
                    }
                    msg.info.Add(info);
                }
            }

            foreach (Goods newFashion in _newActionFashion)
            {
                if( _oriFashion.IndexOf(newFashion) < 0 )
                {

                    if (newFashion.IsEquip())
                    {
                        Logger.LogWarning("sendChanges equip but fashion id=" + newFashion.GetID() + " is  equip, so skip");
                        continue;
                    }

                    // dress on
                    if (msg == null)
                    {
                        msg = new FashionOperation();
                    }
                    FashionOperationInfo info = new FashionOperationInfo();
                    info.type = (uint)FashionOperationType.FOT_EQUIP;
                    info.uuid = newFashion.GetUUID();
                    msg.info.Add(info);
                }
            }
            if (msg != null)
            {
                if (GlobalConst.IS_FASHION_OPEN == 1)
                {
                    msg.role_id = MainPlayer.Instance.CaptainID;
                }
                //PlatNetwork.Instance.UpdateFashionRequest(msg);
            }
          
        }

        public void clear()
        {
            _newActionFashion.Clear();
        }
        public void clearBoth()
        {
            _oriFashion.Clear();
            _newActionFashion.Clear();
        }
    }

    public enum state
    {
        fashionShop,
        wardrobe
    }

    state _state = state.fashionShop;


    public class BtnMap
    {
        private uint[] _fashionID = new uint[4];
        private UISprite[] _sprites = new UISprite[4];
       
        private UIStoreFashion _uif;
     
        public BtnMap( UIStoreFashion uif )
        {
            _uif = uif;
           // GameObject root = _uif._rootGO;
           // updateAsOriginal();
        }

        public void updateSprite( GameObject go )
        {
            string[] keys = new string[4] { "Head", "Clothes", "Trousers", "Shoes" };//,"Special"};

            for( int i = 0; i < 4; i++)
            {
                _sprites[i] = go.transform.FindChild("LeftKey").FindChild(keys[i]).GetComponent<UISprite>();
            }
        }


        public bool existFashion(uint fashionID )
        {
            for( int i = 0; i < 4; i++)
            {
                if( _fashionID[ i ] == fashionID )
                {
                    return true;
                }
            }
            return false;
         }


        public uint getFashionIdByIndex( int index )
        {
            return _fashionID[index];
        }

        public void updateIndex( int index, uint fashionId )
        {
            
            uint oldFashionID = _fashionID[index];
            if (oldFashionID == fashionId)
            {
                return;
            }

            if( oldFashionID != 0 )
            {
                //clear old fashionIds but not self one.

                List<uint> oldFashionTypes = FashionShopConfigItem.getFashionType(oldFashionID);
                foreach (uint oldFashionType in oldFashionTypes)
                {
                    if (oldFashionType == 0 || oldFashionType - 1 == index)
                    {
                        continue;
                    }
                    clearIndex((int)oldFashionType - 1);
                }
            }




            _fashionID[index] = fashionId;

            FashionShopConfigItem item;
            GameSystem.Instance.FashionShopConfig.configs.TryGetValue( fashionId,out item);
            
            if( item == null )
            {
                Logger.LogError("error in");
                return;
            }
            _sprites[index].spriteName = item.getSpriteName();
           // _sprites[index].MakePixelPerfect();
        }

        public void clearIndex( int index )
        {
            string[] str = new string[4] { "icon_hat", "icon_clothes", "icon_pants", "icon_shose" };
            _sprites[index].spriteName = str[index];
           // _sprites[index].MakePixelPerfect();
            _fashionID[index] = 0;
        }

        public bool clearByFashionId( uint fashionId )
        {
            bool ret =false;
            for( int i = 0; i < 4; i++ )
            {
                if( _fashionID[ i ] == fashionId )
                {
                    ret = true;
                    clearIndex(i);
                }
            }
            return ret;
        }

    }




    public class  FashionSelectedItem
    {
        public FashionSelectedItem(FashionShopConfigItem config, int selectIndex)
        {
            _config = config;
            _selectIndex = selectIndex;
        }
        public FashionShopConfigItem _config;
        public int _selectIndex;
    }


    public class FashionSelected
    {
        public Dictionary<uint, FashionSelectedItem> _dressOnItems = new Dictionary<uint, FashionSelectedItem>();
        public void  Add(FashionShopConfigItem item,int selectIndex)
        {
            FashionShopConfigItem fscitem = GameSystem.Instance.FashionShopConfig.GetConfig(item._fashionID);

            if (fscitem.isMine())
            {
                return;
            }


            _dressOnItems.Add(item._fashionID, new FashionSelectedItem(item, selectIndex));
            ClearOwned();
        }


        public int getCount()
        {
            return _dressOnItems.Count;
        }


        public void makeAllSelected()
        {
            foreach(KeyValuePair<uint,FashionSelectedItem> kv in _dressOnItems )
            {
                kv.Value._selectIndex = 0;
            }
        }


        public void ChangeState( uint fashionid,int selectIndex )
        {
            FashionSelectedItem item;
            _dressOnItems.TryGetValue(fashionid, out item);
            if( item != null )
            {
                item._selectIndex = selectIndex;
            }
            ClearOwned();
        }

        public void ClearOwned()
        {
            List<uint> removeIds = new List<uint>();
            foreach(KeyValuePair<uint,FashionSelectedItem> kv in _dressOnItems  )
            {
                uint fashionId = kv.Key;
               FashionShopConfigItem fscitem =  GameSystem.Instance.FashionShopConfig.GetConfig( fashionId );

                if( fscitem.isMine() )
                {
                    removeIds.Add(fashionId);
                }
            }

            foreach( uint fashionId in removeIds )
            {
                Remove(fashionId);
            }
        }

        public void Remove( uint fashionID )
        {
            _dressOnItems.Remove(fashionID);
            ClearOwned();
           
        }

    }
    // control the OBB select.
    public FashionSelected _fashionSelected = new FashionSelected();

    private BtnMap _btnMap = null;


    public void Initialize(GameObject root)
    {
        _rootGO = root;

        _btnMap.updateSprite(root);


        for( int i = 0; i< 4;i++)
        {
            uint fashionID = _dDressInfo.getDataByIndex(i);
            if( fashionID != 0 )
            {
                List<uint> fashionTypes = FashionShopConfigItem.getFashionType( fashionID );
                foreach(uint fashionType in fashionTypes )
                {
                    if( fashionType == 0 )
                    {
                        continue;
                    }
                    _btnMap.updateIndex( (int)fashionType-1, fashionID);
                }
            }
        }

      
        _goReturn = root.transform.FindChild("Top/ButtonBack").gameObject;
        //UIEventListener.Get(_goReturn.gameObject).onClick = OnClickBack;

        _lkHeadSprite = root.transform.FindChild("LeftKey").FindChild("Head").GetComponent<UISprite>();
        _lkClothesSprite = root.transform.FindChild("LeftKey").FindChild("Clothes").GetComponent<UISprite>();
        _lkTrousersSprite = root.transform.FindChild("LeftKey").FindChild("Trousers").GetComponent<UISprite>();
        _lkShoesSprite = root.transform.FindChild("LeftKey").FindChild("Shoes").GetComponent<UISprite>();


      //  _playerBk = root.transform.FindChild("player_bk").GetComponent<UISprite>();
        _selectScrollView = root.transform.FindChild("SelectZone/Position1/SelectScrollView").GetComponent<UIScrollView>();
        _selectGrid = root.transform.FindChild("SelectZone/Position1/SelectScrollView/SelectGrid").GetComponent<UIGrid>();
        
        _modelShowItem = root.transform.FindChild("ModelShowItem").GetComponent<ModelShowItem>();

        
        _modelShowItem.ModelID = MainPlayer.Instance.CaptainID;
      //  _modelShowItem.ShowModel();

        _modelShowItem._playerModel.layerName = "GUI";
        _modelShowItem._playerModel.EnableDrag();

        _buySelectBtn = root.transform.FindChild("BuySelected").GetComponent<UIButton>();
      //  _myWardrobeBtn = root.transform.FindChild("myWardrobe").GetComponent<UIButton>();
        _fashionShopBtn = root.transform.FindChild("MyWardrobe").GetComponent<UISprite>();
  

        UIEventListener.Get(_buySelectBtn.gameObject).onClick = OnBuySelectClick;
      //  UIEventListener.Get(_myWardrobeBtn.gameObject).onClick = OnMyWardrobeClick;
        UIEventListener.Get(_fashionShopBtn.gameObject).onClick = OnFashionShopClick;

        string[] tabNames = new string[5] { /*"tabAll",*/ "TabHead", "TabClothes", "TabTrouses", "TabShoes" , "TabSuit"};
   
        for (uint i = 0; i < 5; i++ )
        {
            UIToggle btn = root.transform.FindChild("SelectZone/Position1/TabGrid").FindChild(tabNames[i]).GetComponent<UIToggle>();
            if( i == 0 )
            {
                _tabAllBtn = btn;
            }
            UIEventListener.Get(btn.gameObject).onClick = OnTabClick;
            _tabs.Add(i,btn);
        }

        _lload = new LLoad(this);
        _lload.setUI(_selectGrid, _selectScrollView);

        covertToState(state.fashionShop);

        if (_tabAllBtn != null)
        {
            OnTabClick(_tabAllBtn.gameObject);
        }


        string[] tags = new string[4] { "Head", "Clothes", "Trousers", "Shoes" };
        for (int i = 0; i < 4; i++)
        {
            _leftBtns[i] = root.transform.FindChild("LeftKey").FindChild(tags[i]).GetComponent<UIButton>();
            _leftBtns[i].tweenTarget = null;
            UIEventListener.Get(_leftBtns[i].gameObject).onClick = leftBtnClick;
        }

    }


    public void leftBtnClick( GameObject go )
    {
        int index = -1;
        for( int i = 0; i < 4; i++)
        {
            if( go == _leftBtns[i].gameObject )
            {
                index = i;
                break;
            }
        }

        if( index<0 )
        {
            Logger.LogError("leftBtnClick can't local the click!!");
            return;
        }

        
        if( _state == state.fashionShop )
        {
            uint fashionID = _tDressInfo.getDataByIndex(index);
            if (fashionID == 0)
            {
                return;
            }

            FashionItem fashionItem = getFashionItem(fashionID);
             FDItem fdItem = _FDItemMgr.getFDItemByFashionID(fashionID);

            bool existInDdressInfo = _dDressInfo.existValueByFashionID(fashionID);
            if (existInDdressInfo && fdItem._isTry )
            {
                // already dDresson no need get down.
                return;
            }


            if (fashionItem != null)
            {
                fashionItem._script.a_isTry = !fashionItem._script.a_isTry;
                DressOnUpdate(fashionID, fashionItem._script.a_isTry);
            }
            else
            {
               
                fdItem._isTry = !fdItem._isTry;
                DressOnUpdate(fashionID, fdItem._isTry );
            }

        }
        else if( _state == state.wardrobe )
        {
            uint fashionID = _dDressInfo.getDataByIndex(index);

            if (fashionID == 0)
            {
                return;
            }

            FashionShopConfigItem fcItem = getFItem(fashionID);
            if( !fcItem.isInDate() )
            {
                CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_EXPIRE_NEED_BUY"));
                return;
            }

            FashionItem fashionItem = getFashionItem(fashionID);
            FDItem fdItem = _FDItemMgr.getFDItemByFashionID(fashionID);

            if (fashionItem != null)
            {
                fashionItem._script.a_isTry = !fashionItem._script.a_isTry;
                DressOnUpdate(fashionID, fashionItem._script.a_isTry);
            }
            else
            {

                fdItem._isTry = !fdItem._isTry;
                DressOnUpdate(fashionID, fdItem._isTry);
            }
        }

    }

    
    private void SendDressWhenLevelMyWardrobe()
    {
         _wardrobeUpdate.sendChanges();
    }

    //private void OnClickBack(GameObject go )
    //{

    //    if (_state == state.wardrobe)
    //    {
    //        SendDressWhenLevelMyWardrobe();
    //    }
    //    _isGoBack = true;
    //    GameObject.Destroy(_rootGO);
    //    //GameSystem.Instance.mClient.mUIManager.MainCtrl.ShowUIForm(14);
    //}

    public void ActionBack()
    {
        // interface to lua.
        if (_state == state.wardrobe)
        {
            SendDressWhenLevelMyWardrobe();
        }
        else
        {
            SendDressWhenLevelMyWardrobe();
        }
        
        _isGoBack = true;
    }
    private void OnBuySelectClick(GameObject go)
    {
        _fashionSelected.ClearOwned();

        if (0 == _fashionSelected._dressOnItems.Count)
        {
            CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_YOU_ARE_NOT_SELECT_FASHION"));
            return;
        }
        _fashionSelected.makeAllSelected();


        GameObject obbGO = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Fashion_OBB") as GameObject;

        GameObject oneBtnBuyGO = CommonFunction.InstantiateObject(obbGO, _rootGO.transform);
        oneBtnBuyGO.SetActive(true);
        Fashion_OBB fashionOBB = oneBtnBuyGO.transform.GetComponent<Fashion_OBB>();
        fashionOBB.setUIStoreFashion = this;

        UIManager.Instance.BringPanelForward(oneBtnBuyGO);
    }


    private uint _selectTabIndex;

     private void OnTabClick(GameObject go)
    {
        
        foreach( KeyValuePair<uint,UIToggle> item in _tabs )
        {
            item.Value.gameObject.transform.FindChild("Selected").gameObject.SetActive(false);
            //UILabel label = item.Value.gameObject.transform.FindChild("Label").GetComponent<UILabel>();
            //label.color = Color.yellow;
            //label.effectColor = Color.yellow;

            if(item.Value.gameObject == go)
            {
                _selectTabIndex = item.Key;
            }
        }

        Logger.Log("OnTabClick index=" + _selectTabIndex + " state=" + _state);


        UIButton btn = go.transform.GetComponent<UIButton>();
        go.transform.FindChild("Selected").gameObject.SetActive(true);
        //go.transform.FindChild("Label").GetComponent<UILabel>().color = Color.black;
        //go.transform.FindChild("Label").GetComponent<UILabel>().effectColor = Color.black;

        CommonFunction.ClearGridChild(_selectGrid.transform);


         if( _state == state.fashionShop )
         {
             _fashionItemList.Clear();
             _lload.Init(_selectTabIndex);
         }
         else if (_state == state.wardrobe)
         {
             _fashionItemList.Clear();
             foreach (KeyValuePair<ulong, Goods> v in MainPlayer.Instance.FashionGoodsList)
             {
                 Goods good = v.Value;
                 uint good_id = good.GetID();

                  List<uint> subCategory = FashionShopConfigItem.getFashionType(good_id);

                 if( _selectTabIndex != 0 )
                 {
                     if (_selectTabIndex == 5)
                     {
                         if (subCategory.Count <= 1)
                         {
                             continue;
                         }
                     }
                     else if (subCategory.Count != 1 || !subCategory.Contains(_selectTabIndex))
                     {
                         continue;
                     }
                 }


                 uint fashionID = (uint)good.GetID();
                 FashionShopConfigItem fhCitem;
                 GameSystem.Instance.FashionShopConfig.configs.TryGetValue(fashionID, out fhCitem);

                 FashionItemDisplay fItem = new FashionItemDisplay();
                 fItem._costNum = fhCitem._costNum;

                 fItem.setValue(fhCitem);

                 fItem._fashionID = fashionID;
             
                 FashionItem item = new FashionItem(this, _selectGrid.gameObject, fItem);
                 item.FashionID = (uint)good.GetID();
                 _selectGrid.AddChild(item._goItem.transform);
                 _fashionItemList.Add(item);
             }       
         }


        _selectGrid.Reposition();
        _selectScrollView.ResetPosition();

      //  coverToStateAction();
    }
 

    private void OnMyWardrobeClick(GameObject go)
    {
        _wardrobeUpdate.clear();
        covertToState(state.wardrobe);
    }

    private void OnFashionShopClick(GameObject go)
    {
        SendDressWhenLevelMyWardrobe();
        covertToState(state.fashionShop);

        Logger.Log("FashionShipItem.cs called OnFashionShopClick");
    }


    public class renewInfo
    {

        private List<renewInfoItem> _infos = new List<renewInfoItem>();

        public void AddInfo( uint id, uint day, string name )
        {
            renewInfoItem item = new renewInfoItem();
            item._id = id;
            item._day = day;
            item._name = name;

            _infos.Add(item);
        }

        public void clear()
        {
            _infos.Clear();
        }

        public void popBox()
        {
            foreach( renewInfoItem item in _infos)
            {
                string tips = null;
               if( item._day == 0 )
               {
                   tips = string.Format(CommonFunction.GetConstString("UI_FASHION_RENEW_PROMPT_FOREVER"), item._name);
               }
               else
               {
                   tips = string.Format(CommonFunction.GetConstString("UI_FASHION_RENEW_PROMPT"), item._name,item._day);
               }
               CommonFunction.ShowTip(tips);
            }
            clear();
        }


        private class renewInfoItem
        {
            public uint _id;
            public uint _day;
            public string _name;
        }

    }

    private class LLoad 
    {
        public LLoad(UIStoreFashion uif)
        {
            _uif = uif;
        }
        private uint _maxSize;
        private uint _curSize;
        private UIStoreFashion _uif;
        private uint _curSelectIndex;
        private uint _curSubCategory;
        private uint _sizeInc = 12;

        public UIGrid _selectGrid = null;
        private UIScrollView _selectScrollView = null;



        void OnDragFinished()
        {
          // if (_selectScrollView.RestrictWithinBounds(false,false,true) )
            {
                Increase();
               //Logger.Log("UIStoreFashion OnDragFinished() value=");
            }
            
           
        }

        public void setUI( UIGrid grid,UIScrollView scrollView )
        {
            _selectGrid = grid;
            _selectScrollView =scrollView;
           _selectScrollView.onDragFinished = OnDragFinished;
        }


        public void Init( uint curSelectIndex )
        {
            _curSelectIndex = curSelectIndex;
            uint[] ar = new uint[5] { 1, 2, 3, 4,0 };
            _curSubCategory = ar[_curSelectIndex];

            uint count = 0;
            foreach (var vItem in GameSystem.Instance.FashionShopConfig.configsSort)
            {
                uint fID = vItem._fashionID;

                List<uint> subCategory = vItem.getFashionType();

                uint gender = vItem.getGender();
                if ( gender != 0  && gender != (int)MainPlayer.Instance.Captain.m_gender)
                {
                    continue;
                }

     
                if (_curSubCategory == 0 )
                {
                    if (subCategory.Count <= 1)
                  {
                      continue;
                  }
                }
                else if (subCategory.Count != 1 || !subCategory.Contains(_curSubCategory))
                {
                    continue;
                }
                count++;
            }
            _maxSize = count;
            _curSize = 0;

            Increase();
        }

        public void Increase()
        {
            if (_uif._state != UIStoreFashion.state.fashionShop)
            {
                return;
            }

            if( _curSize >= _maxSize )
            {
                return;
            }

            uint incur = _maxSize - _curSize;
            if (incur > _sizeInc)
            {
                incur = _sizeInc;
            }

            List<FashionShopConfigItem> sortList = GameSystem.Instance.FashionShopConfig.configsSort;
            uint playerGender = (uint)MainPlayer.Instance.Captain.m_gender;

            if (1 == _curSubCategory)
            {
                if (playerGender == 1)
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cHeadSort;
                }
                else
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cHeadSort_w;
                }
            }
            else if (2 == _curSubCategory)
            {
                if (playerGender == 1)
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cClothesSort;
                }
                else
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cClothesSort_w;
                }
            }
            else if (3 == _curSubCategory)
            {
                if (playerGender == 1)
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cTrouserseSort;
                }
                else
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cTrouserseSort_w;
                }
            }
            else if (4 == _curSubCategory)
            {
                if (playerGender == 1)
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cShoesSort;
                }
                else
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cShoesSort_w;
                }

            }
            else if (0 == _curSubCategory)
            {
                if (playerGender == 1)
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cSuiteSort;
                }
                else
                {
                    sortList = GameSystem.Instance.FashionShopConfig.cSuiteSort_w;
                }
            }

            for( uint cur = _curSize; cur < _curSize + incur; cur++)
            {
                FashionShopConfigItem fcItem = sortList[(int)cur];
                FashionItem item = new FashionItem(_uif, _selectGrid.gameObject, fcItem);
                item.FashionID = fcItem._fashionID;
                _selectGrid.AddChild(item._goItem.transform);
                _uif._fashionItemList.Add(item);
            }
            _selectGrid.Reposition();
            //_selectScrollView.ResetPosition();
            _curSize += incur; 
        }
    }

    //private void coverToStateAction()
    //{
    //    if (_state == state.wardrobe)
    //    {
    //        Logger.Log("covertToState wardrobe");
    //        _wardrobeUpdate.clearBoth();

    //        for (int i = 0; i < 4; i++)
    //        {
    //            uint fashionId = _tDressInfo.getDataByIndex(i);
    //            bool existInDDresss = _dDressInfo.existValueByFashionID(fashionId);
    //            if (fashionId != 0 && !existInDDresss)
    //            {
    //                DressOnUpdate(fashionId, false);
    //            }
    //            _tDressInfo.clearValueByFashionID(fashionId);
    //        }

    //        for (int i = 0; i < 4; i++)
    //        {
    //            uint fashionId = _dDressInfo.getDataByIndex(i);
    //            if (fashionId != 0)
    //            {
    //                _wardrobeUpdate.dressUpdateBoth(fashionId, true);
    //                _tDressInfo.setValue(i, fashionId);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        _wardrobeUpdate.clearBoth();
    //        Logger.Log("covertToState fashionShop");
    //        _FDItemMgr.setAllNotTry();

    //        for (int i = 0; i < 4; i++)
    //        {
    //            uint fashionId = _dDressInfo.getDataByIndex(i);
    //            if (fashionId > 0)
    //            {
    //                _FDItemMgr.getFDItemByFashionID(fashionId)._isTry = true;
    //            }
    //        }

    //    }

    //    _buySelectBtn.gameObject.SetActive(_state == state.fashionShop);
    //    //_myWardrobeBtn.gameObject.SetActive(_state == state.fashionShop);
    //    _fashionShopBtn.gameObject.SetActive(_state == state.wardrobe);
    //    //_playerBk.gameObject.SetActive(_state == state.fashionShop);
    //}


 

    private void covertToState( state st)
    {
        if (st == state.wardrobe)
        {
            Logger.Log("covertToState wardrobe");
            _wardrobeUpdate.clearBoth();

            for (int i = 0; i < 4; i++)
            {
                uint fashionId = _tDressInfo.getDataByIndex(i);
                bool existInDDresss = _dDressInfo.existValueByFashionID(fashionId);
                if (fashionId != 0 && !existInDDresss)
                {
                    DressOnUpdate(fashionId, false);
                }

                Logger.Log("to Wardrobe _tDressInfo try clear fashionId=" + fashionId);
                _tDressInfo.clearValueByFashionID(fashionId);
            }

            for (int i = 0; i < 4; i++)
            {
                Logger.Log("to Wardrobe _tDressInfo index=" + i + "fashionId=" + _tDressInfo.getDataByIndex(i));
            }

            for (int i = 0; i < 4; i++)
            {
                uint fashionId = _dDressInfo.getDataByIndex(i);
                if (fashionId != 0)
                {
                    _wardrobeUpdate.dressUpdateBoth(fashionId, true);
                    _tDressInfo.setValue(i, fashionId);
                }
            }

            _FDItemMgr.setAllNotTry();

            for (int i = 0; i < 4; i++)
            {
                uint fashionId = _dDressInfo.getDataByIndex(i);
                if (fashionId > 0)
                {
                    _FDItemMgr.getFDItemByFashionID(fashionId)._isTry = true;
                }
            }
        }
        else
        {
            Logger.Log("covertToState fashionShop");

            _wardrobeUpdate.clearBoth();
            _FDItemMgr.setAllNotTry();

            for( int i = 0; i < 4; i++ )
            {
                uint fashionId = _dDressInfo.getDataByIndex(i);
                if( fashionId > 0 )
                {
                    _FDItemMgr.getFDItemByFashionID(fashionId)._isTry = true;
                }
            }


            for (int i = 0; i < 4; i++)
            {
                uint fashionId = _dDressInfo.getDataByIndex(i);
                if (fashionId != 0)
                {
                    _wardrobeUpdate.dressUpdateBoth(fashionId, true);
                    _tDressInfo.setValue(i, fashionId);
                }
            }
        }

        _buySelectBtn.gameObject.SetActive(st == state.fashionShop);
       // _myWardrobeBtn.gameObject.SetActive(st == state.fashionShop);
        _fashionShopBtn.gameObject.SetActive(st == state.wardrobe);
       // _playerBk.gameObject.SetActive(st == state.fashionShop);

        _state = st;
        OnTabClick(_tabAllBtn.gameObject);
    }

    public void OnFashionOperationFailed()
    {
        _renewInfo.clear();
    }
    public void OnFashionOperation( FashionOperationResp resp )
    {
        Logger.Log("OnFashionOperation called");
     
        if( _isGoBack )
        {
            return;
        }

        foreach (FashionOperationRespInfo info in resp.resp_info)
        {
            Goods good;
            MainPlayer.Instance.FashionGoodsList.TryGetValue(info.uuid, out good );

            string[] typeStr = new string[4]{"equipment","unequip","renew","delete"};
            Logger.Log(GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(good.GetID()).name + "is for " + typeStr[info.type - 1]);
        }


        foreach (FashionOperationRespInfo info in resp.resp_info)
        {
            Goods good;
            MainPlayer.Instance.FashionGoodsList.TryGetValue(info.uuid, out good );

            if( good == null )
            {
                Logger.LogError("OnFashionOperation error for cannot find good for uuid=" + info.uuid);
                continue;
            }

            uint fashionID = good.GetID();
            FashionShopConfigItem fItem = getFItem(fashionID);
            List<uint> fashionTypes = fItem.getFashionType();

            uint infoType = info.type;

            if( infoType == (uint)FashionOperationType.FOT_EQUIP)
            {
                FashionItem tFashionItem = getFashionItem(fashionID);
                if (_tDressInfo.existValueByFashionID(fashionID))
                {
                    if (tFashionItem!= null)
                    {
                        tFashionItem._script.a_isTry = true;
                        tFashionItem._script.a_isDressOn = true;
                    }
                }
                else
                {


                    tFashionItem._script.a_isTry = true;
                    tFashionItem._script.a_isDressOn = true;
                    DressOnUpdate(fashionID, true);
                    if(state.wardrobe ==_state)
                    {
                        _wardrobeUpdate.dressUpdateOri(fashionID, true);
                    }
                }

                // clear dDressInfo.
                List<uint> oldDFashionIds = _dDressInfo.getByTypes(fashionTypes);
                for (int i = 0; i < oldDFashionIds.Count; i++)
                {
                    uint oldDFashionId = oldDFashionIds[i];
                    if (oldDFashionId == fashionID)
                    {
                        continue;
                    }
                    _dDressInfo.clearValueByFashionID(oldDFashionId);

                    FashionItem oldDFashionItem = getFashionItem(oldDFashionId);
                    if (oldDFashionItem != null)
                    {
                        oldDFashionItem._script.a_isTry = false;
                        oldDFashionItem._script.a_isDressOn = false;
                    }

                }

                // no need to update for it update alreay.
                // reset new dDressInfo.
                foreach (uint type in fashionTypes)
                {
                    if (type == 0)
                    {
                        continue;
                    }
                    _dDressInfo.setValue((int)type - 1, fashionID);
                }
            }
            else if ( infoType == (uint) FashionOperationType.FOT_UNEQUIP )
            {
                // no need to call dress off for it dressed off already.
                //DressOnUpdate(good.GetID(), good.IsEquip());
                
                // remove the 'dressed' flag.
                FashionItem fashionItem = getFashionItem(good.GetID());

                setFashionItemDress(good.GetID(), false);
                setFashionItemTry(good.GetID(), false);
        

                foreach (uint type in fashionTypes)
                {
                    if (type == 0)
                    {
                        continue;
                    }
                    _dDressInfo.clearValue((int)type - 1);
                }
            }
            else if (infoType == (uint)FashionOperationType.FOT_RENEW)
            {
                FashionItem tFashionItem = getFashionItem(fashionID);
                if( tFashionItem!=null )
                {
                    tFashionItem._script.updateForever();
                }
     
            }
            _renewInfo.popBox();
        }
    }




    void DressOnAfterBuy( GameObject go )
    {
        foreach (respInfo rInfo in _respInfo)
        {
       
            uint pos = rInfo._pos - 1;
            if (pos >= GameSystem.Instance.FashionShopConfig.configsSort.Count)
            {
                Logger.LogError("@DressOnAfterBuy pos >= GameSystem.Instance.FashionShopConfig.configsSort.Count pos =" + pos);
                continue;
            }
            uint fashionID = GameSystem.Instance.FashionShopConfig.configsSort[(int)pos]._fashionID;

            List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, fashionID);
            if (good.Count != 1)
            {
                Logger.LogError("@DressOnAfterBuy good.Count=" + good.Count + " for fashion id=" + fashionID);
                continue;
            }

            int captainGender = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(MainPlayer.Instance.CaptainID).gender;
            GoodsAttrConfig goodsAttrConfig = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionID);
            uint gender = 0;
            if (goodsAttrConfig!= null)
            {
                gender = goodsAttrConfig.gender;
            }
            if ( gender != 0)
            {
                if (captainGender != gender )
                {
                    CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_DRY_DRESSON_FAILED_FOR_GENDER"));
                    // _toggle.value = false;
                    DestoryPopBuyUI();
                    return;
                }
            } 
        }



        FashionOperation msg = new FashionOperation();
    

        //foreach(KeyValuePair<uint, FashionSelectedItem> kv in _fahionSelected._dressOnItems )
        foreach( respInfo rInfo in _respInfo )
        {
            FashionOperationInfo info = new FashionOperationInfo();
            uint pos = rInfo._pos -1;
            if( pos >= GameSystem.Instance.FashionShopConfig.configsSort.Count )
            {
                Logger.LogError("@DressOnAfterBuy pos >= GameSystem.Instance.FashionShopConfig.configsSort.Count pos ="+ pos);
                continue;
            }
            uint fashionID = GameSystem.Instance.FashionShopConfig.configsSort[(int)pos]._fashionID;

            //FashionSelectedItem item = kv.Value;
            info.type = (uint)FashionOperationType.FOT_EQUIP;
            List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, fashionID);
            if( good.Count != 1 )
            {
                Logger.LogError("@DressOnAfterBuy good.Count=" + good.Count + " for fashion id=" + fashionID);
                continue;
            }
            info.uuid = good[0].GetUUID();
            msg.info.Add(info);
        }

        if (GlobalConst.IS_FASHION_OPEN == 1)
        {
            msg.role_id = MainPlayer.Instance.CaptainID;
        }
        //PlatNetwork.Instance.UpdateFashionRequest(msg);
        DestoryPopBuyUI();
        OnTabClick(_tabAllBtn.gameObject);
    }


 

    void DestoryPopBuyUI()
    {
        Transform t = _rootGO.transform.FindChild("FashionBuy(Clone)");
        if (t != null)
        {
            GameObject.Destroy(t.gameObject);
        }

        t = _rootGO.transform.FindChild("Fashion_OBB(Clone)");
        if (t != null)
        {
            GameObject.Destroy(t.gameObject);
        }
    }
    void NotDressOnAfterBuy( GameObject  go )
    {
        DestoryPopBuyUI();
    
        OnTabClick(_tabAllBtn.gameObject);
    }

    void OneSBuyOK( GameObject go )
    {
        if( _oneSBuyIndex >= GameSystem.Instance.FashionShopConfig.configsSort.Count )
        {
            Logger.LogError("index out of range local index=" + _oneSBuyIndex + " local type=" + _oneSBuySelect);
            return;
        }

        FashionShopConfigItem configItem = GameSystem.Instance.FashionShopConfig.configsSort[_oneSBuyIndex];
        
        uint costType;
        uint costNum = configItem.getActuallyCost(_oneSBuySelect, out costType);

        if (costType == 1)
        {
            if (MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree < costNum)
            {
                CommonFunction.ShowTip(CommonFunction.GetConstString("NOT_ENOUGH_DIAMOND"));
                return;
            }
        }
        else if (costType == 2)
        {
            if (MainPlayer.Instance.Gold < costNum)
            {
                CommonFunction.ShowTip(CommonFunction.GetConstString("NOT_ENOUGH_MONEY"));
                return;
            }
        }
        else
        {
            Logger.LogError("cost type is not 1 or 2, is"+ costType);
            return;
        }

        if( configItem.isMine() )
        {
            _renewInfo.clear();
            FashionOperation msg = new FashionOperation();
            {
                FashionOperationInfo info = new FashionOperationInfo();
                info.type = (uint)FashionOperationType.FOT_RENEW;
                info.uuid = configItem.getGood().GetUUID();
                info.subtype = (uint)_oneSBuySelect + 1;
                msg.info.Add(info);

                _renewInfo.AddInfo(configItem._fashionID, configItem.getActualDayDur()[_oneSBuySelect], configItem.getName());
            }

            if (GlobalConst.IS_FASHION_OPEN == 1)
            {
                msg.role_id = MainPlayer.Instance.CaptainID;
            }
            //PlatNetwork.Instance.UpdateFashionRequest(msg);
            DestoryPopBuyUI();
        }
        else
        {
            BuyStoreGoods msg = new BuyStoreGoods();
            msg.store_id = StoreType.ST_FASHION;
            BuyStoreGoodsInfo info = new BuyStoreGoodsInfo();
            info.pos = (uint)_oneSBuyIndex + 1;
            info.type = (uint)_oneSBuySelect + 1;
            msg.info.Add(info);

			/*
            PlatNetwork.Instance.BuyStoreGoodsRequest(msg);
			*/
        }
    }

    void OneSBuyCancle( GameObject go )
    {
        //GameObject.Destroy(_rootGO.transform.FindChild("FashionBuy(Clone)").gameObject);
    }


    private void OneSBuyOK( FashionBuy fashonBuy)
    {

        _oneSBuySelect = fashonBuy._select;
        FashionShopConfigItem item = fashonBuy.ConfigItem;

        FashionShopConfigItem outItem;
        GameSystem.Instance.FashionShopConfig.configs.TryGetValue(item._fashionID, out outItem);

        _oneSBuyIndex = GameSystem.Instance.FashionShopConfig.configsSort.IndexOf(outItem);

        uint costType;
        item.getActuallyCost(_oneSBuySelect, out costType);
        List<uint> timeDur = item.getActualTimeDur();


        bool isForever = timeDur[_oneSBuySelect] == 0;

        string queryString = null;

        if( item.isInDate() )
        {
            if (costType == 1)
            {
                queryString = CommonFunction.GetConstString("UI_FASHION_RENEW_COMFIROM_DIAMOND");
                if( isForever )
                {
                    queryString = CommonFunction.GetConstString("UI_FASHION_RENEW_COMFIROM_DIAMOND_FOREVER");
                }

            }
            else if (costType == 2)
            {
                queryString = CommonFunction.GetConstString("UI_FASHION_RENEW_COMFIROM_GOLD");
                 if( isForever )
                 {
                     queryString = CommonFunction.GetConstString("UI_FASHION_RENEW_COMFIROM_GOLD_FOREVER");
                 }
            }
            else
            {
                Logger.LogError("the cost type is not right @OnSBuyOne");
            }
        }
        else
        {
            if (costType == 1)
            {
                queryString = CommonFunction.GetConstString("UI_FASHION_BUY_COMFIROM_DIAMOND");
                if( isForever )
                {
                    queryString = CommonFunction.GetConstString("UI_FASHION_BUY_COMFIROM_DIAMOND_FOREVER");
                }

            }
            else if (costType == 2)
            {
                queryString = CommonFunction.GetConstString("UI_FASHION_BUY_COMFIROM_GOLD");
                if( isForever )
                {
                    queryString = CommonFunction.GetConstString("UI_FASHION_BUY_COMFIROM_GOLD_FOREVER");
                }
            }
            else
            {
                Logger.LogError("the cost type is not right @OnSBuyOne");
            }
        }


        uint type;
        if (isForever )
        {
            queryString = string.Format(queryString, item.getActuallyCost(_oneSBuySelect, out type), item.getName());
        }
        else
        {
            queryString = string.Format(queryString, item.getActuallyCost(_oneSBuySelect, out type),
           item.getActualDayDur()[_oneSBuySelect], item.getName(), item.getName());
        }
       

        CommonFunction.ShowPopupMsg(queryString,null,_voidOneSBuyDeleOk,_voidOneSBuyDeleCancle);
    }


    public void OnBuyStoreGoodsResp(BuyStoreGoodsResp resp)
    {
        string popString = CommonFunction.GetConstString("UI_FASHION_CONFIROM_DRESS_AFTER_BUY");
        CommonFunction.ShowPopupMsg(popString, null, _voidDressOnAfterBuy, _voidNotDressOnAfterBuy);

        _respInfo.Clear();
        for (int i = 0; i < resp.info.Count; i++)
        {
            string msg = "buy " + i + ", pos=" + resp.info[i].pos + "type=" + resp.info[i].type;

            respInfo info = new respInfo();
            info._pos = resp.info[i].pos;
            info._type = resp.info[i].type;
            _respInfo.Add(info);

            Logger.LogWarning(msg);
        }

        _fashionSelected.ClearOwned();
    }

    private void initDressInfo()
    {
        uint captainID = MainPlayer.Instance.CaptainID;

        RoleShape roleShape = GameSystem.Instance.RoleShapeConfig.GetConfig(captainID);
        _oDressInfo.setValue(0, roleShape.hair_id);
        _oDressInfo.setValue(1, roleShape.upper_id);
        _oDressInfo.setValue(2, roleShape.lower_id);
        _oDressInfo.setValue(3, roleShape.shoes_id);
        
        foreach(KeyValuePair<ulong,Goods> kv in MainPlayer.Instance.FashionGoodsList )
        {
            Goods good = kv.Value;
            if( good.IsEquip() )
            {
                List<uint> types = FashionShopConfigItem.getFashionType(good.GetID());
                foreach( uint type in types )
                {
                    if( type == 0)
                    {
                        continue;
                    }
                    _dDressInfo.setValue((int)type - 1, (uint)good.GetID());
                }
            }
        }    
    }

    public void OnBuyOne(FashionShopConfigItem item)
    {
        GameObject go = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/FashionBuy") as GameObject;
		_buyOneGO = CommonFunction.InstantiateObject (go, _rootGO.transform);
        _buyOneGO.SetActive(true);

        FashionBuy script = _buyOneGO.transform.GetComponent<FashionBuy>();
        script.ConfigItem = item;
        script._onBuySelectOne += OneSBuyOK;
        UIManager.Instance.BringPanelForward(_buyOneGO);
    }


    private uint _curFashionID = 0;
    public void OnClickItem( uint fashionId )
    {
        if( _curFashionID == fashionId )
        {
            return;
        }
        PlaySoundManager.PlaySoundOneShot("Audio/UI_button");
        FashionShopConfigItem fItem = getFItem(fashionId);

        if (fItem.isInDate())
        {
          //  fItem.isDressOn()

            foreach (FashionItem item in _fashionItemList)
            {
                if (item.FashionID == fashionId)
                {
                    item._script.VisibleSelect(true,false);
                }
                else
                {
                    item._script.VisibleSelect(false);
                }
            }
           
        }
        else
        {
            foreach (FashionItem item in _fashionItemList)
            {
                if (item.FashionID == fashionId)
                {
                    item._script.VisibleSelect(true);
                }
                else
                {
                    item._script.VisibleSelect(false);
                }
            }
        }
    }

    public void OnFashionChange( Goods good )
    {
        if( _isGoBack )
        {
            return;
        }
        uint fashionID = good.GetID();
        FashionShopConfigItem fItem = getFItem(fashionID);
        List<uint> fashionTypes = fItem.getFashionType();

        FashionItem fashionItem = getFashionItem(fashionID);
        if (fashionItem != null)
        {
            fashionItem._script.updateForever();
        }
       


        if( !good.IsEquip() )
        {
            if (good.getTimeLeft() == 0)
            {
                // not equip
                // need check it result other promblems.
                // if does, the reason should below.


                // debug info
                {
                    Logger.Log("_btnMap" + _btnMap);
                }


                bool needDressDown = true;
                // check need dress down.
                List<uint> dFashionIds= _tDressInfo.getByTypes(fashionTypes);
                if (dFashionIds.Count != 0)
                {
                    if (dFashionIds.IndexOf(fashionID) < 0)
                    {
                        // not in the dFashions.
                        needDressDown = false;
                    }
                   
                }

                if( !_dDressInfo.existValueByFashionID(good.GetID()) )
                {
                    needDressDown = false;
                }
                if (needDressDown )
                {
                    dressDown(good.GetID());


                    foreach (uint type in fashionTypes)
                    {
                        if (type == 0)
                        {
                            continue;
                        }
                        uint oldFashionId = _btnMap.getFashionIdByIndex((int)type - 1);
                        if (oldFashionId != 0)
                        {
                            _fashionSelected.Remove(oldFashionId);
                        }
                        _btnMap.clearIndex((int)type - 1);
                    }

                }
               
                // DressOnUpdate(good.GetID(), good.IsEquip());


                // remove the 'dressed' flag.
                foreach (FashionItem item in _fashionItemList)
                {
                    if (item.FashionID == good.GetID())
                    {
                        item._script.a_isDressOn = false;
                    }
                }
                _dDressInfo.clearValueByFashionID(good.GetID());
            }      
        }
        else
        {
       
        }
    }


    FashionItem getFashionItem( uint fashionId )
    {
        for( int i = 0; i < _fashionItemList.Count; i++)
        {
            if( _fashionItemList[i].FashionID == fashionId )
            {
                return _fashionItemList[i];
            }
        }
        return null;
    }

    bool setFashionItemTry(uint fashionId, bool isTry )
    {
        FashionItem fashionItem = getFashionItem(fashionId);
        FDItem fdItem = _FDItemMgr.getFDItemByFashionID(fashionId);

        if( fashionItem != null )
        {
            fashionItem._script.a_isTry = isTry;
            return true;
        }
        fdItem._isTry = isTry;
        return false;
    }

    bool setFashionItemDress(uint fashionId, bool isDress )
    {
        FashionItem fashionItem = getFashionItem(fashionId);
        FDItem fdItem = _FDItemMgr.getFDItemByFashionID(fashionId);

        if (fashionItem != null)
        {
            fashionItem._script.a_isDressOn = isDress;
            return true;
        }
        fdItem._isDressOn = isDress;
        return false;
    }




    public FashionShopConfigItem getFItem( uint fashionId )
    {
        FashionShopConfigItem fItem;
        GameSystem.Instance.FashionShopConfig.configs.TryGetValue(fashionId, out fItem);
        return fItem;
    }
   
    public void DressOnUpdate( uint fashionID, bool isActionDressOn )
    {
        FashionShopConfigItem fItem = getFItem(fashionID);
        if( fItem == null )
        {
            Logger.LogError("@DressOnUpdate failed for cannot find fashionID=" + fashionID );
            return;
        }

       List<uint> fashionTypes = fItem.getFashionType();
       if (isActionDressOn)
        {
            List<uint> tDress = _tDressInfo.getByTypes(fashionTypes);
           foreach( uint fid in tDress )
           {
               _modelShowItem._playerModel.DressDownFashion(fid);
               _tDressInfo.clearValueByFashionID(fid);

               setFashionItemTry(fid, false);

              // if (_state == state.wardrobe)
               {
                   _wardrobeUpdate.dressUpdate(fid, false);
               }

               _fashionSelected.Remove(fid);
           }

           // comment for the suit.
          // if( tDress.Count == 0 )
           {
               List<uint> dDress = _dDressInfo.getByTypes(fashionTypes);
               foreach (uint fid in dDress)
               {
                   if (fid == fashionID || !_btnMap.existFashion(fid))
                   {
                       continue;
                   }
                   if (tDress.IndexOf(fid)>=0)
                   {
                       continue;
                   }

                   _modelShowItem._playerModel.DressDownFashion(fid);
                   // _dDressInfo.clearValueByFashionID(fid);

                   //if (_state == state.wardrobe)
                   {
                       _wardrobeUpdate.dressUpdate(fid, false);
                   }
               }
           }

           
           if (!fItem.isDressOn())
            {
                _fashionSelected.Add(fItem, 0);
            }
           _modelShowItem._playerModel.DressOnFashion(fashionID );

           foreach( uint ftype in fashionTypes )
           {
               if( 0 == ftype )
               {
                   continue;
               }
               _tDressInfo.setValue((int)ftype - 1, fashionID);
               _btnMap.updateIndex((int)ftype - 1, fashionID);
           }

           if (_state == state.wardrobe)
           {
               _wardrobeUpdate.dressUpdate(fashionID, true);

               // hande the fashionShopItem display.
               List<uint> dFashionIds = _dDressInfo.getByTypes(fashionTypes);
               foreach (uint dFashionId in dFashionIds)
               {

                   if (dFashionId != fashionID)
                   {
                       setFashionItemTry(dFashionId, false);
                       setFashionItemDress(dFashionId, false);
                   }
               }
           }
           else
           {
               _wardrobeUpdate.dressUpdate(fashionID, true);
           }
        }
        else
        {
           // dress down.
            if (!fItem.isDressOn())
            {
                _fashionSelected.Remove( fashionID);
            }
            
          //  if (_state == state.wardrobe)
            {
                _wardrobeUpdate.dressUpdate(fItem._fashionID, false);
            }
            dressDown(fashionID);
        }


       Logger.Log("after dressUp action fahionid=" + fashionID + " isActionDressOn=" + isActionDressOn);

       for (int i = 0; i < 4; i++)
       {
           Logger.Log("tDressInfo[" + i + "]=" + _tDressInfo.getDataByIndex(i));
       }

    }


    void dressDown(uint fashionID)
    {

        FashionShopConfigItem fItem = getFItem(fashionID);
        if (fItem == null)
        {
            Logger.LogError("@dressDown failed for cannot find fashionID=" + fashionID);
            return;
        }

        List<uint> fashionTypes = fItem.getFashionType();
        _modelShowItem._playerModel.DressDownFashion(fashionID);

        bool clearTag = _tDressInfo.clearValueByFashionID(fashionID);

        if (clearTag)
        {
            List<uint> dFashionIds = _dDressInfo.getByTypes(fashionTypes);
            if (dFashionIds.Count == 0)
            {
                // in try, but not in dress.
                foreach (uint type in fashionTypes)
                {
                    if (0 == type)
                    {
                        continue;
                    }

                    uint oldFashionId = _btnMap.getFashionIdByIndex((int)type - 1);
                    if (oldFashionId != 0)
                    {
                        _fashionSelected.Remove(oldFashionId);
                    }
                    _btnMap.clearIndex((int)type - 1);
                }
            }
            else
            {
                // in try, and also in dress.
                // recover dress on.
                foreach (uint dFashionId in dFashionIds)
                {
                    if (dFashionId == fashionID)
                    {
                        // clear as origianl.
                        _btnMap.clearByFashionId(dFashionId);
                        continue;
                    }
                    FashionShopConfigItem dFItem = getFItem(dFashionId);

                    foreach (uint type in dFItem.getFashionType())
                    {
                        if (0 == type)
                        {
                            continue;
                        }


                        // get rid of olds which slots the suites.
                        uint oldFashionId = _btnMap.getFashionIdByIndex((int)type - 1);
                        if (oldFashionId != 0 && oldFashionId != fashionID)
                        {
                            _modelShowItem._playerModel.DressDownFashion(oldFashionId);
                           
                            FashionItem fsId = getFashionItem(oldFashionId);
                            fsId._script.a_isTry = false;


                            _tDressInfo.clearValueByFashionID(oldFashionId);
                            _fashionSelected.Remove(oldFashionId);
                        }

                        _btnMap.updateIndex((int)type - 1, dFashionId);
                        _tDressInfo.setValue((int)type - 1, dFashionId);
                        FashionItem fsItem = getFashionItem(dFashionId);
                        if( fsItem!= null)
                        {
                            fsItem._script.a_isTry = true;
                            if( _state == state.wardrobe )
                            {
                                fsItem._script.a_isDressOn = true;
                            }
                        }
                    }

                    _modelShowItem._playerModel.DressOnFashion(dFashionId);
                    

                    if (_state == state.wardrobe)
                    {
                        _wardrobeUpdate.dressUpdate(dFashionId, true);
                    }
                }
            }
        }
        else
        {

            // on dress.
            //if( _state == state.fashionShop )
            //{

            Logger.Log("clearTag error!!!!--not find in tDress for dress down fashionID=" + fashionID);

            for (int i = 0; i < 4;i++ )
            {
                Logger.Log("tDressInfo[" + i + "]=" + _tDressInfo.getDataByIndex(i));
            }



             clearTag = _dDressInfo.existValueByFashionID(fashionID);
            // }
            //else if( _state == state.wardrobe)
            //{
            //    clearTag = _tDressInfo.clearValueByFashionID(fashionID);
            //}
        }



        if (!clearTag)
        {
            Logger.LogError("clearTag error!!!!");
            for (int i = 0; i < 4;i++ )
            {
                Logger.LogError("_dDressInfo[" + i + "]=" + _dDressInfo.getDataByIndex(i));
            }
        }
    }


    public void OnDressOn(FashionShopItem item)
    {
        DressOnUpdate(item.ConfigItem._fashionID, item.a_isTry);
    }


}