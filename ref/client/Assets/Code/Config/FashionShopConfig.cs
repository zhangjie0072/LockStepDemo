using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;
using fogs.proto.config;

public class FashionShopConfigItem
{
    public uint _fashionID;
    public List<uint> _timeDur = new List<uint>();
    public List<uint> _costType = new List<uint>();
    public List<uint> _costNum = new List<uint>();
    public uint _isDiscount;
    public List<uint> _discountCost = new List<uint>();
    public uint _isNew;
    public int _sortValue;
    public uint _vip;

    public List<uint> _renewTimeDur = new List<uint>();
    public List<uint> _renewCostType = new List<uint>();
    public List<uint> _renewCostNum = new List<uint>();



    public static uint getGender(uint fashionID)
    {
        uint gener = 0;
        GoodsAttrConfig goodsAttrConfig = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionID);
        if (goodsAttrConfig != null)
        {
            gener = goodsAttrConfig.gender;
        }
        else
        {
            Logger.LogError("cannot find  the gender by fashionId:" + fashionID);
        }

        return gener;

    }

    public uint getGender()
    {
        return getGender(_fashionID);
    }
    public static List<uint> getFashionType( uint fashion_id )
    {
        List<uint> r = new List<uint>();

        string[] arr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashion_id).sub_category.Split('&');
        foreach (string str in arr)
        {
            r.Add(uint.Parse(str));
        }
        return r;
    }


    public string getAtlas()
    {
        List<uint> types = getFashionType();
        if( types.Count == 1 )
        {
            uint type = types[0];
            if( type == 1 || type == 2 || type == 5)
            {
                return "Atlas/icon/iconFashion";
            }
            else
            {
                 return "Atlas/icon/iconFashion_1";
            }

        }

        return "Atlas/icon/iconFashion"; ;
    }


    public bool isForver()
    {
        List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _fashionID);
        if( good.Count == 0 )
        {
            return false;
        }

        return good[0].getTimeLeft() == 0x7fffffff;
    }


    public List<uint> getFashionType()
    {
        return getFashionType( _fashionID );
    }

    public bool isMine()
    {

        List<Goods>good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION,_fashionID);
        return good.Count != 0;
    }

    public Goods getGood()
    {
        List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _fashionID);
        if( good.Count == 0 )
        {
            return null;
        }
        return good[0];
    }

    public bool isInDate()
    {
        if( !isMine() )
        {
            return false;
        }
        List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _fashionID);
        if( good.Count == 0 )
        {
            return false;
        }

        return good[0].getTimeLeft() > 0;
    }

    public bool isUsed()
    {
        Goods good = getGood();
        if( good == null )
        {
            return false;
        }

        return good.isUsed();
    }
    public bool isDressOn()
    {
        List<Goods> good = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _fashionID);
        if (good.Count == 0)
        {
            return false;
        }
        return good[0].IsEquip();
    }


   public List<uint> getActualTimeDur()
    {
       if (isMine())
       {
           return _renewTimeDur;
       }
       return _timeDur;
    }


   public List<uint> getActualDayDur()
   {
       List<uint> leftDur = new List<uint>();
       List<uint> actTimeDurs = getActualTimeDur();

       for (int i = 0; i < actTimeDurs.Count; i++ )
       {
           leftDur.Add(actTimeDurs[i]);
       }



       for (int i = 0; i < leftDur.Count;i++ )
       {
           leftDur[i] = leftDur[i] / (60 * 24);
       }
       return leftDur;
   }




    public uint getActuallyCost( int index, out uint type )
    {
        // handle type
        if (isMine())
        {
            type = _renewCostType[index];
        }
        else
        {
            type = _costType[index];
        }

        if( index < 0 )
        {
            // error condition.
            return 0;
        }

        if (isInDate())
        {
            return _renewCostNum[index];
        }

       // select discount first.
        if (_isDiscount == 1)
        {
            if (_discountCost.Count > index)
            {
                return _discountCost[index];
            }
            else
            {
                // error condition.
                return 0;
            }    
        }

        // then the normal.
        if (_costNum.Count > index)
        {
            return _costNum[index];
        }
        else
        {
            // error condition.
            return 0;
        }

        // TODO: the renew latter.

        // error condition.
        return 0;
    }
    public string getSpriteName()
    {
        return GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_fashionID).icon;
    }

    public string getTimeLeftStr()
    {
        List<Goods> goods = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_FASHION, _fashionID);
        if (goods.Count != 1)
        {
            Logger.LogError("goods Count should be one!!");
        }

        Goods good = goods[0];
        if (good != null)
        {
            if (good.getTimeLeft() != 0x7fffffff)
            {
                // return string.Format("{0}", good.getTimeLeft());

                uint time = good.getTimeLeft();

                uint day = 0;
                uint hr = 0;
                uint min = 0;

                day = time / 86400;

                time -= day * 86400;
                if (time > 0)
                {
                    hr = time / 3600;
                    time -= hr * 3600;
                    if (time > 0)
                    {
                        min = time / 60;
                    }
                }
                if (day >= 1)
                {
                    if (hr == 0 )
                    {
                        return string.Format( CommonFunction.GetConstString("UI_FASHION_LEFT_DAY"), day);
                    }
                    return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME"), day, hr);
                }
                else if (hr >= 1)
                {
                    return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME_HOUR"), hr, min);
                }

                return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME_MIN"), min);
            }
            else
            {
                return CommonFunction.GetConstString("UI_FASHION_FOREVER");
            }
        }
        return "0";
    }


    public string getName()
    {
        return GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_fashionID).name;
    }
}



public class FashionItemDisplay : FashionShopConfigItem
{

    //public string getTimeLeftStr()
    //{
    //    List<Goods> goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_FASHION, _fashionID);
    //    if (goods.Count != 1)
    //    {
    //        Logger.LogError("goods Count should be one!!");
    //    }

    //    Goods good = goods[0];
    //    if (good != null)
    //    {
    //        if (good.getTimeLeft() != 0x7fffffff)
    //        {
    //            // return string.Format("{0}", good.getTimeLeft());

    //            uint time = good.getTimeLeft();

    //            uint day = 0;
    //            uint hr = 0;
    //            uint min = 0;

    //            day = time / 86400;

    //            time -= day * 86400;
    //            if (time > 0)
    //            {
    //                hr = time / 3600;
    //                time -= hr * 3600;
    //                if (time > 0)
    //                {
    //                    min = time / 60;
    //                }
    //            }
    //            if (day >= 1)
    //            {
    //                return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME"), day, hr);
    //            }
    //            else if (hr >= 1)
    //            {
    //                return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME_HOUR"), hr, min);
    //            }

    //            return string.Format(CommonFunction.GetConstString("UI_FASHION_LEFT_TIME_MIN"), min);
    //        }
    //        else
    //        {
    //            return CommonFunction.GetConstString("UI_FASHION_FOREVER");
    //        }
    //    }
    //    return "0";
    //}


    public void setValue(FashionShopConfigItem item)
    {
        _fashionID = item._fashionID;
        _timeDur = item._timeDur;
        _costType = item._costType;
        _costNum = item._costNum;
        _isDiscount = item._isDiscount;
        _discountCost = item._discountCost;
        _isNew = item._isNew;
        _vip = item._vip;
        _sortValue = item._sortValue;

        _renewTimeDur = item._renewTimeDur;
        _renewCostType = item._renewCostType;
        _renewCostNum = item._renewCostNum;
    }
}


public class FashionShopConfig
{
    string name = GlobalConst.DIR_XML_FASHION_SHOP;
    bool isLoadFinish = false;
    private object LockObject = new object();
    //Fashion Shop
    public Dictionary<uint, FashionShopConfigItem> configs = new Dictionary<uint, FashionShopConfigItem>();
    public List<FashionShopConfigItem> configsSort = new List<FashionShopConfigItem>();

    public List<FashionShopConfigItem> configsSort_m = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> configsSort_w = new List<FashionShopConfigItem>();
    
    public List<FashionShopConfigItem> cHeadSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cClothesSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cTrouserseSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cShoesSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cBackSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cSuiteSort = new List<FashionShopConfigItem>();


    public List<FashionShopConfigItem> cHeadSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cClothesSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cTrouserseSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cShoesSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cBackSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> cSuiteSort_w = new List<FashionShopConfigItem>();
    
    //Reputation Shop
    public Dictionary<uint, FashionShopConfigItem> reputationConfigs = new Dictionary<uint, FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationConfigsSort = new List<FashionShopConfigItem>();

    public List<FashionShopConfigItem> reputationConfigsSort_m = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationConfigsSort_w = new List<FashionShopConfigItem>();

    public List<FashionShopConfigItem> reputationHeadSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationClothesSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationTrouserseSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationShoesSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationBackSort = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationSuiteSort = new List<FashionShopConfigItem>();


    public List<FashionShopConfigItem> reputationHeadSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationClothesSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationTrouserseSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationShoesSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationBackSort_w = new List<FashionShopConfigItem>();
    public List<FashionShopConfigItem> reputationSuiteSort_w = new List<FashionShopConfigItem>();
	//public Dictionary<uint, HidePart> mapHideParts = new Dictionary<uint, HidePart>();

    public FashionShopConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    public FashionShopConfigItem GetConfig(uint fashion_id)
    {
        FashionShopConfigItem data = null;
		configs.TryGetValue(fashion_id, out data);
        return data;
    }

    public FashionShopConfigItem GetReputationConfig(uint fashion_id) 
    {
        FashionShopConfigItem data = null;
        reputationConfigs.TryGetValue(fashion_id, out data);
        return data;
    }

    //public HidePart MappingPart(uint hide_id)
    //{
    //    HidePart data = null;
    //    mapHideParts.TryGetValue(hide_id, out data);
    //    return data;
    //}

    public void ReadConfig()
    {
        if (isLoadFinish == false || GameSystem.Instance.GoodsConfigData.isReadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }

        //读取以及处理XML文本的类
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);

        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            FashionShopConfigItem data = new FashionShopConfigItem();
            FashionShopConfigItem reputationData = new FashionShopConfigItem();
            int storeID = int.Parse(node_line.SelectSingleNode("store_id").InnerText);
            if (storeID == (int)StoreType.ST_FASHION)
            {
                data._fashionID = uint.Parse(node_line.SelectSingleNode("id").InnerText);

                string theValues = node_line.SelectSingleNode("time_dur").InnerText;
                string[] tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint timeDur;
                    if (uint.TryParse(token, out timeDur))
                        data._timeDur.Add(timeDur);
                }


                theValues = node_line.SelectSingleNode("cost_type").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costType;
                    if (uint.TryParse(token, out costType))
                        data._costType.Add(costType);
                }

                theValues = node_line.SelectSingleNode("cost_num").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costNum;
                    if (uint.TryParse(token, out costNum))
                        data._costNum.Add(costNum);
                }


                data._isDiscount = uint.Parse(node_line.SelectSingleNode("is_discount").InnerText);


                theValues = node_line.SelectSingleNode("discount_cost").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint discountCost;
                    if (uint.TryParse(token, out discountCost))
                        data._discountCost.Add(discountCost);
                }


                data._isNew = uint.Parse(node_line.SelectSingleNode("is_new").InnerText);
                data._vip = uint.Parse(node_line.SelectSingleNode("vip").InnerText);
                data._sortValue = int.Parse(node_line.SelectSingleNode("sort_value").InnerText);




                theValues = node_line.SelectSingleNode("renew_time_dur").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint timeDur;
                    if (uint.TryParse(token, out timeDur))
                        data._renewTimeDur.Add(timeDur);
                }


                theValues = node_line.SelectSingleNode("renew_cost_type").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costType;
                    if (uint.TryParse(token, out costType))
                        data._renewCostType.Add(costType);
                }

                theValues = node_line.SelectSingleNode("renew_cost_num").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costNum;
                    if (uint.TryParse(token, out costNum))
                        data._renewCostNum.Add(costNum);
                }


                List<uint> types = data.getFashionType();
                uint gender = data.getGender();
                if (types.Count == 1)
                {
                    uint type = types[0];
                    if (type == 1)
                    {
                        if (gender == 0)
                        {
                            cHeadSort.Add(data);
                            cHeadSort_w.Add(data);
                        }
                        else if (gender == 1)
                        {
                            cHeadSort.Add(data);
                        }
                        else if (gender == 2)
                        {
                            cHeadSort_w.Add(data);
                        }
                    }
                    else if (type == 2)
                    {
                        if (gender == 0)
                        {
                            cClothesSort.Add(data);
                            cClothesSort_w.Add(data);
                        }
                        else if (gender == 1)
                        {
                            cClothesSort.Add(data);
                        }
                        else if (gender == 2)
                        {
                            cClothesSort_w.Add(data);
                        }
                    }
                    else if (type == 3)
                    {
                        if (gender == 0)
                        {
                            cTrouserseSort.Add(data);
                            cTrouserseSort_w.Add(data);
                        }
                        else if (gender == 1)
                        {
                            cTrouserseSort.Add(data);
                        }
                        else if (gender == 2)
                        {
                            cTrouserseSort_w.Add(data);
                        }
                    }
                    else if (type == 4)
                    {
                        if (gender == 0)
                        {
                            cShoesSort.Add(data);
                            cShoesSort_w.Add(data);
                        }
                        else if (gender == 1)
                        {
                            cShoesSort.Add(data);
                        }
                        else if (gender == 2)
                        {
                            cShoesSort_w.Add(data);
                        }
                    }
                    else if (type == 5)
                    {
                        if (gender == 0)
                        {
                            cBackSort.Add(data);
                            cBackSort_w.Add(data);
                        }
                        else if (gender == 1)
                        {
                            cBackSort.Add(data);
                        }
                        else if (gender == 2)
                        {
                            cBackSort_w.Add(data);
                        }
                    }
                }
                else
                {
                    if (gender == 0)
                    {
                        cSuiteSort.Add(data);
                        cSuiteSort_w.Add(data);
                    }
                    else if (gender == 1)
                    {
                        cSuiteSort.Add(data);
                    }
                    else if (gender == 2)
                    {
                        cSuiteSort_w.Add(data);
                    }

                }



                if (gender == 0)
                {
                    configsSort_m.Add(data);
                    configsSort_w.Add(data);
                }
                else if (gender == 1)
                {
                    configsSort_m.Add(data);
                }
                else if (gender == 2)
                {
                    configsSort_w.Add(data);
                }

                configs.Add(data._fashionID, data);
                configsSort.Add(data);
            }
            else if (storeID == (int)StoreType.ST_REPUTATION) 
            {
                reputationData._fashionID = uint.Parse(node_line.SelectSingleNode("id").InnerText);

                string theValues = node_line.SelectSingleNode("time_dur").InnerText;
                string[] tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint timeDur;
                    if (uint.TryParse(token, out timeDur))
                        reputationData._timeDur.Add(timeDur);
                }


                theValues = node_line.SelectSingleNode("cost_type").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costType;
                    if (uint.TryParse(token, out costType))
                        reputationData._costType.Add(costType);
                }

                theValues = node_line.SelectSingleNode("cost_num").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costNum;
                    if (uint.TryParse(token, out costNum))
                        reputationData._costNum.Add(costNum);
                }


                reputationData._isDiscount = uint.Parse(node_line.SelectSingleNode("is_discount").InnerText);


                theValues = node_line.SelectSingleNode("discount_cost").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint discountCost;
                    if (uint.TryParse(token, out discountCost))
                        reputationData._discountCost.Add(discountCost);
                }


                reputationData._isNew = uint.Parse(node_line.SelectSingleNode("is_new").InnerText);
                reputationData._vip = uint.Parse(node_line.SelectSingleNode("vip").InnerText);
                reputationData._sortValue = int.Parse(node_line.SelectSingleNode("sort_value").InnerText);




                theValues = node_line.SelectSingleNode("renew_time_dur").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint timeDur;
                    if (uint.TryParse(token, out timeDur))
                        reputationData._renewTimeDur.Add(timeDur);
                }


                theValues = node_line.SelectSingleNode("renew_cost_type").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costType;
                    if (uint.TryParse(token, out costType))
                        reputationData._renewCostType.Add(costType);
                }

                theValues = node_line.SelectSingleNode("renew_cost_num").InnerText;
                tokens = theValues.Split('&');
                foreach (string token in tokens)
                {
                    uint costNum;
                    if (uint.TryParse(token, out costNum))
                        reputationData._renewCostNum.Add(costNum);
                }


                List<uint> types = reputationData.getFashionType();
                uint gender = reputationData.getGender();
                if (types.Count == 1)
                {
                    uint type = types[0];
                    if (type == 1)
                    {
                        if (gender == 0)
                        {
                            reputationHeadSort.Add(reputationData);
                            reputationHeadSort_w.Add(reputationData);
                        }
                        else if (gender == 1)
                        {
                            reputationHeadSort.Add(reputationData);
                        }
                        else if (gender == 2)
                        {
                            reputationHeadSort_w.Add(reputationData);
                        }
                    }
                    else if (type == 2)
                    {
                        if (gender == 0)
                        {
                            reputationClothesSort.Add(reputationData);
                            reputationClothesSort_w.Add(reputationData);
                        }
                        else if (gender == 1)
                        {
                            reputationClothesSort.Add(reputationData);
                        }
                        else if (gender == 2)
                        {
                            reputationClothesSort_w.Add(reputationData);
                        }
                    }
                    else if (type == 3)
                    {
                        if (gender == 0)
                        {
                            reputationTrouserseSort.Add(reputationData);
                            reputationTrouserseSort_w.Add(reputationData);
                        }
                        else if (gender == 1)
                        {
                            reputationTrouserseSort.Add(reputationData);
                        }
                        else if (gender == 2)
                        {
                            reputationTrouserseSort_w.Add(reputationData);
                        }
                    }
                    else if (type == 4)
                    {
                        if (gender == 0)
                        {
                            reputationShoesSort.Add(reputationData);
                            reputationShoesSort_w.Add(reputationData);
                        }
                        else if (gender == 1)
                        {
                            reputationShoesSort.Add(reputationData);
                        }
                        else if (gender == 2)
                        {
                            reputationShoesSort_w.Add(reputationData);
                        }
                    }
                    else if (type == 5)
                    {
                        if (gender == 0)
                        {
                            reputationBackSort.Add(reputationData);
                            reputationBackSort_w.Add(reputationData);
                        }
                        else if (gender == 1)
                        {
                            reputationBackSort.Add(reputationData);
                        }
                        else if (gender == 2)
                        {
                            reputationBackSort_w.Add(reputationData);
                        }
                    }
                }
                else
                {
                    if (gender == 0)
                    {
                        reputationSuiteSort.Add(reputationData);
                        reputationSuiteSort_w.Add(reputationData);
                    }
                    else if (gender == 1)
                    {
                        reputationSuiteSort.Add(reputationData);
                    }
                    else if (gender == 2)
                    {
                        reputationSuiteSort_w.Add(reputationData);
                    }

                }



                if (gender == 0)
                {
                    reputationConfigsSort_m.Add(reputationData);
                    reputationConfigsSort_w.Add(reputationData);
                }
                else if (gender == 1)
                {
                    reputationConfigsSort_m.Add(reputationData);
                }
                else if (gender == 2)
                {
                    reputationConfigsSort_w.Add(reputationData);
                }

                reputationConfigs.Add(reputationData._fashionID, reputationData);
                reputationConfigsSort.Add(reputationData);
            }

            //时装
            configsSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

            configsSort_m.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            configsSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

	        cHeadSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cClothesSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cTrouserseSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cShoesSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cBackSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cSuiteSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

	        cHeadSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cClothesSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cTrouserseSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cShoesSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cBackSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
	        cSuiteSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

            //声望
            reputationConfigsSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

            reputationConfigsSort_m.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationConfigsSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

            reputationHeadSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationClothesSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationTrouserseSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationShoesSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationBackSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationSuiteSort.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));

            reputationHeadSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationClothesSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationTrouserseSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationShoesSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationBackSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
            reputationSuiteSort_w.Sort(new Comparison<FashionShopConfigItem>(new FashionComparison().Compare));
        }
    }



    public class FashionComparison
    {
        public int Compare(FashionShopConfigItem x, FashionShopConfigItem y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (y == null)
                {
                    return -1;
                }
                else
                {
                    if (x._sortValue < y._sortValue)
                    {
                        return 1;
                    }
                    else if (x._sortValue > y._sortValue)
                    {
                        return -1;
                    }
                    else
                    {
                        if (x._fashionID > y._fashionID)
                        {
                            return -1;
                        }
                        else  if( x._fashionID < y._fashionID)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
            }
        }
    }



}
