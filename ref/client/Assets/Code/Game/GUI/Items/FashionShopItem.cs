using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using fogs.proto.config;


public class FashionShopItem :MonoBehaviour 
{
    //public GameObject _goItem;
    //private uint _fashionID;

    //public UIToggle _toggle = null;
    public UIButton _button = null;
    private UISprite _isDressOnSprite = null;
    private UIButton _buyBtn = null;
    private UISprite _costTypeSprite = null;


    private GameObject _costGo = null;
    private UISprite _buyIconSprite = null; // action key
    private GameObject _infoGO = null;

    private GameObject _newGo = null;
    private GameObject _discountGo = null;

    public uint _state;

    private FashionShopConfigItem _configItem;

    private UIStoreFashion _uiStoreFashion;
    private UISprite _clockSprite = null;
    private UISprite _backgroundSprite = null;
    private GameObject _boxCollider;
    private UISprite _spriteSelect = null;
    private GameObject _goVip = null;
    private UILabel _labelVip = null;


    public delegate void OnDressOnDelegate(FashionShopItem item);
    public OnDressOnDelegate _onDressOnDelegate;

    UILabel _leftTimeDetail;
    UILabel _leftTimeLabel;
   // UISprite _isOwnedSprite;



    private uint _leftTime;
   // private uint _gender;

    //private bool _isDressOn = false;
    //private bool _isTry = false;

    private bool _isExpired = false;
    private bool _clientDressedOn = false;
    private int _clickCounter = 0;

    FDItem _fdItem = null;


    private UIEventListener.VoidDelegate _voidStartDress;
    private UIEventListener.VoidDelegate _voidStartDressCancle;


    public bool a_isTry
    {
        get
        {
            return _fdItem._isTry;
        }
        set
        {
            _fdItem._isTry = value;

            if(_state == 1 )
            {
                //a_isDressOn = value;

                if (_isDressOnSprite != null)
                {
                    _isDressOnSprite.gameObject.SetActive(_fdItem._isTry);
                }
            }
        }
    }


    public bool a_isDressOn
    {
        get
        {
            return _fdItem._isDressOn;
        }

        set
        {
            List<Goods> goods = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _configItem._fashionID);

            if (1 == _state)
            {
                _fdItem._isDressOn = value;
                if (_isDressOnSprite != null )
                {
                    _isDressOnSprite.gameObject.SetActive(_fdItem._isDressOn);
                }
                else
                {
                    Logger.LogWarning("FashionShopItem is_aDresson _isDressOnSprite is null for state ==1");
                }
                
            }
            else if (0 == _state )//&& _configItem.isDressOn())
            {
                if( value )
                {
                    // set dresson must the fashion already dressed for data.
                    if( !_configItem.isDressOn())
                    {
                        return;
                    }
                }
                _fdItem._isDressOn = value;

                if( _isDressOnSprite != null )
                {
                    _isDressOnSprite.gameObject.SetActive(_fdItem._isDressOn);
                }
                else
                {
                    Logger.LogWarning("FashionShopItem is_aDresson _isDressOnSprite is null for state ==0");
                }
            }
            else
            {
                // not change so return.
                return;
            }

            if (_isDressOnSprite!= null)
            {
                _isDressOnSprite.gameObject.SetActive(_fdItem._isDressOn);
            }
            else
            {
                Logger.LogWarning("FashionShopItem is_aDresson _isDressOnSprite is null");
            }

            // also update the owned.
           // updateOwnedSprite();
        }
    }


    private void startDress(GameObject go )
    {
        _clientDressedOn = true;
        dressUpdate(!_fdItem._isTry);
    }

    public void startDressCancle( GameObject go )
    {

    }


    public UIStoreFashion setUiStoreFashion
    {
        set
        {
            _uiStoreFashion = value;
        }
    }

    public FashionShopConfigItem ConfigItem
    {
        get
        {
            return _configItem;
        }

        set
        {
            _configItem = value;

            _fdItem = _uiStoreFashion._FDItemMgr.getFDItemByFashionID(_configItem._fashionID);

            if( _state == 0 )
            {
                if (_configItem._costType[0] == 1)
                {
                    _costTypeSprite.spriteName = "com_property_diamond2";
                }
                else if (_configItem._costType[0] == 2)
                {
                    _costTypeSprite.spriteName = "com_property_gold2";
                }
             //   _buyIconSprite.spriteName = "icon_buy";


                _discountGo.SetActive(false);
                _newGo.SetActive(false);
                if (_configItem._isDiscount == 1 )
                {
                    _discountGo.SetActive(true);
                    _newGo.SetActive(false);
                }
                else if( _configItem._isNew == 1 )
                {
                    _discountGo.SetActive(false);
                    _newGo.SetActive(true);
                }
                
                List<Goods> goods = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _configItem._fashionID);


                _fdItem._isDressOn = _configItem.isDressOn();
                //_fdItem._isTry = _fdItem._isDressOn;

                if (_fdItem._isDressOn )
                {
                    _isDressOnSprite.gameObject.SetActive(_fdItem._isDressOn);
                }
                else
                {

                }

                if (_configItem.isMine())
                {
                    _costGo.SetActive(false);
                    _infoGO.SetActive(true);

                    _discountGo.SetActive(false);
                    _newGo.SetActive(false);
                  //  FashionItemDisplay fiDisplay = (FashionItemDisplay)_configItem;
                    _leftTimeDetail = _infoGO.transform.FindChild("leftTime_detail").GetComponent<UILabel>();
                    _leftTimeDetail.text = _configItem.getTimeLeftStr();
                }
                else
                {
                    _costGo.SetActive(true);
                    _infoGO.SetActive(false);
                }

              //  _clockSprite.gameObject.SetActive(false);
                setVip(_configItem._vip);
            }
            else if( _state == 1 )
            {
                FashionItemDisplay fiDisplay = (FashionItemDisplay)_configItem;

              //  _buyIconSprite.spriteName = "icon_renew";
                _leftTimeDetail = _infoGO.transform.FindChild("leftTime_detail").GetComponent<UILabel>();
                _leftTimeDetail.text = _configItem.getTimeLeftStr();

                _fdItem._isDressOn = _configItem.isDressOn();
                _isDressOnSprite.gameObject.SetActive(_fdItem._isTry);
                //
               // _fdItem._isTry = _fdItem._isDressOn;

                if( _configItem.isUsed() )
                {
                    setDescripColor(Color.red);

                    _clockSprite.gameObject.SetActive(true);

                    if( !_configItem.isInDate() )
                    {
                        updateExpired(true);
                    }
                }
                else
                {
                    setDescripColor(Color.yellow);
                    _clockSprite.gameObject.SetActive(false);
                }
                _discountGo.SetActive(false);
                _newGo.SetActive(false);
            }

            updateForever();


            //_costGo.SetActive(_state == 0);
            //_infoGO.SetActive(_state == 1);

            //GoodsAttrConfig goodsAttrConfig = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_configItem._fashionID);
            //if( goodsAttrConfig!= null )
            //{
            //    _gender = goodsAttrConfig.gender;
            //}
            //else
            //{
            //    Logger.LogError("cannot find the fashion in goods, the fashion id is +" + _configItem._fashionID);
            //}

            //updateOwnedSprite();
            

        }
    }

    //private void updateOwnedSprite()
    //{
    //    if( _state == 1 )
    //    {
    //        _isOwnedSprite.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        List<Goods> goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_FASHION, _configItem._fashionID);
    //        _isOwnedSprite.gameObject.SetActive(!_fdItem._isDressOn && goods.Count != 0);
    //    }
    //}

    public void updateForever()
    {
        if (_configItem.isForver())
        {
            _clockSprite.gameObject.SetActive(false);
           // _buyBtn.gameObject.SetActive(false);
        }
        else
        {
            _clockSprite.gameObject.SetActive(true);
        }

    }
    private void setDescripColor( Color color )
    {
        _leftTimeDetail.color = color;
        _leftTimeDetail.effectColor = color;
        _leftTimeLabel.color = color;
        _leftTimeLabel.effectColor  =color;
    }

    void onFashionChange( Goods good )
    {


    }

    public void setVip(uint vip)
    {
        if(vip == 0)
        {
            NGUITools.SetActive(_goVip, false);
            return;
        }
        NGUITools.SetActive(_goVip, true);
        _labelVip.text = string.Format("{0}", vip);
    }

    void Awake()
    {
        //_toggle = transform.GetComponent<UIToggle>();
        _spriteSelect = transform.FindChild("Sele").GetComponent<UISprite>();
        _goVip = transform.FindChild("VIP").gameObject;
        _labelVip = transform.FindChild("VIP/Label").GetComponent<UILabel>();


        _button = transform.GetComponent<UIButton>();
        _isDressOnSprite = transform.FindChild("isDressOn").GetComponent<UISprite>();

       // _buyBtn = transform.FindChild("buy").GetComponent<UIButton>();
        _costTypeSprite = transform.FindChild("Cost").GetComponent<UISprite>();

        _newGo = transform.FindChild("New").gameObject;
        _discountGo = transform.FindChild("Discount").gameObject;


        _costGo = transform.FindChild("Cost").gameObject;
        //_buyIconSprite = transform.FindChild("buy").FindChild("icon").GetComponent<UISprite>();

        _infoGO = transform.FindChild("Info").gameObject;

       // _isOwnedSprite = transform.FindChild("isOwned").GetComponent<UISprite>();
        _clockSprite = transform.FindChild("Info/Clock").GetComponent<UISprite>();
        _backgroundSprite = transform.GetComponent<UISprite>();

        _leftTimeLabel = _infoGO.transform.FindChild("LeftTime").GetComponent<UILabel>();

        //MainPlayer.Instance.FashionChange += onFashionChange;


        _voidStartDress = startDress;
        _voidStartDressCancle = startDressCancle;




    }


    void Start()
    {
       // UIEventListener.Get(_buyBtn.gameObject).onClick = OnBuyClick;
       // UIEventListener.Get(_button.gameObject).onClick = OnButtonClick;
        UIEventListener.Get(gameObject).onClick = Click;
    }

    void Update()
    {
        if (_leftTimeDetail!=null)
        {
            if( _configItem.isInDate() )
            {
               // FashionItemDisplay fiDisplay = (FashionItemDisplay)_configItem;
                _leftTimeDetail.text = _configItem.getTimeLeftStr();
                
                updateExpired(false);
            }
            else
            {
                updateExpired(true);
                
            }
        }

        if (_configItem.isMine())
        {
            _discountGo.SetActive(false);
            _newGo.SetActive(false);
        }
    }

    void updateExpired( bool isExpired )
    {
        if( _isExpired == isExpired )
        {
            return;
        }
        if (isExpired )
        {
            setDescripColor(Color.red);
        }
        else
        {
            setDescripColor(Color.black);
        }

        if (isExpired )
        {
            _leftTimeDetail.text = CommonFunction.GetConstString("UI_FASHION_EXPIRE");
            _leftTimeDetail.effectColor = Color.red;
            _clockSprite.gameObject.SetActive(false);
            _leftTimeLabel.gameObject.SetActive(false);
            _button.normalSprite = "bg_pure_round_grey";

        }
        else
        {
            _leftTimeDetail.text = _configItem.getTimeLeftStr();
            //_leftTimeLabel.gameObject.SetActive(true);
            _button.normalSprite = "bg_pure_round_brown";

            _leftTimeDetail.text = _configItem.getTimeLeftStr();

            updateForever();

            
        }
        _isExpired = isExpired;



    }
    
    private void OnBuyClick(GameObject go )
    {
        _uiStoreFashion.OnBuyOne(_configItem);
    }

    public void dressUpdate( bool isTry )
    {

        //a_isDressOn = isDressOn;
        a_isTry = isTry;
                 
        if (_onDressOnDelegate != null)
        {
            _onDressOnDelegate(this);
        }
    }

    public void VisibleSelect( bool visible ,bool update_value = true )
    {
        NGUITools.SetActive(this._spriteSelect.gameObject, visible);
        if (update_value )
        {
            if (visible)
            {
                _clickCounter++;
            }
            else
            {
                _clickCounter = 0;
            }

            if (_clickCounter == 2)
            {
                _uiStoreFashion.OnBuyOne(_configItem);
                _clickCounter = 0;
            }
        }

    }
   
    private void Click( GameObject go )
    {
        _uiStoreFashion.OnClickItem(_fdItem._fashionID);

        int captainGender = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(MainPlayer.Instance.CaptainID).gender;
        if (_fdItem._gender != 0)
        {
            if (captainGender != _fdItem._gender)
            {
                CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_DRY_DRESSON_FAILED_FOR_GENDER"));
                // _toggle.value = false;
                return;
            }
        }

        //if (0 == _state && a_isTry )
        //{
        //    dressUpdate(false);
        //    return;
        //}

        if (!_clientDressedOn && 1 == _state && !_configItem.isUsed() && !_fdItem._isDressOn && !_configItem.isForver())
        {
            CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("UI_FASHON_START_TO_DRESS"), null, _voidStartDress
                , _voidStartDressCancle);

            return;
        }


       // dressUpdate(!_fdItem._isTry);
        if (_configItem.isMine() || _clickCounter != 0 )
        {
            dressUpdate(!a_isTry);
        }






    }
    //private void OnButtonClick(GameObject go )
    //{
    //    if( _state == 1 && !_configItem.isInDate())
    //    {
    //        CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_EXPIRE_NEED_BUY"));
    //        return;
    //    }
       
        
    //    int captainGender = GameSystem.Instance.RoleBaseConfigData.GetConfigData(MainPlayer.Instance.CaptainID).gender;
    //    if (_fdItem._gender != 0)
    //    {
    //        if (captainGender != _fdItem._gender)
    //        {
    //            CommonFunction.ShowTip(CommonFunction.GetConstString("UI_FASHION_DRY_DRESSON_FAILED_FOR_GENDER"));
    //           // _toggle.value = false;
    //            return;
    //        }
    //    }

    //    if (0 == _state && _configItem.isDressOn() )
    //    {
    //        return;
    //    }
        
    //    if (!_clientDressedOn && 1 == _state && !_configItem.isUsed() && !_fdItem._isDressOn && !_configItem.isForver())
    //    {
    //        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("UI_FASHON_START_TO_DRESS"), null, _voidStartDress
    //            , _voidStartDressCancle);

    //        return;
    //    }


    //    dressUpdate(!_fdItem._isTry);

    //}


  



}