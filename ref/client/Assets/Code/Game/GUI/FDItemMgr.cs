using UnityEngine;
using System.Collections;
using fogs.proto.msg;
using System.Collections.Generic;



public class FDItem
{
    public uint _gender;
    public bool _isDressOn = false;
    public bool _isTry = false;
    public bool _isExpired = false;
    public uint _fashionID=0;


    public void init( FashionShopConfigItem item )
    {
        _gender = item.getGender();
        _isDressOn = item.isDressOn();
        _isTry = false;
        _isExpired = item.isInDate();
        _fashionID = item._fashionID;
    }

}


public class FDItemMgr
{
    private List<FDItem> _FDItems = new List<FDItem>();


    public void init()
    {
        _FDItems.Clear();
	    foreach( FashionShopConfigItem fitem in GameSystem.Instance.FashionShopConfig.configsSort )
        {
            FDItem item = new FDItem();
            item.init(fitem);
            _FDItems.Add(item);
        }
    }
    public  FDItem getFDItemByFashionID( uint fashionId)
    {
        foreach (FDItem item in _FDItems)
        {
            if (item._fashionID == fashionId)
            {
                return item;
            }
        }

        Logger.LogError("cannnot find in FDItemMgr for fashionId=" + fashionId);
        return null;
    }


    public void setAllNotTry()
    {
        foreach( FDItem fditem in _FDItems )
        {
            fditem._isTry = false;
        }
    }
  
}

