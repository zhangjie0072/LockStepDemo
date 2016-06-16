using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fogs.proto.msg;
using fogs.proto.config;


public class Goods
{
    ulong uuid;
    uint id;
    uint suit_id;
    string name;
    string subName;
    string icon;
    GoodsCategory category;
    EquipmentType subCategory;
    BadgeCG badgeCategory;
    List<uint> _fashionSubCategory = new List<uint>();
    uint num;
    uint level;
    GoodsQuality quality;
    bool is_equip;
    uint _timeLeft;
    bool _isUsed;
	uint _exp;
    bool canUse = false;
    bool canSell = false;
    List<uint> fashionAttrList = new List<uint>();

    public void Init(GoodsProto data)
    {
        //uuid = data.uuid;
        //id = data.id;
        //category = data.category;
        //num = data.num;
        //level = data.level;
        //quality = data.quality;
        //is_equip = data.is_equip == 1 ? true : false;
        //_timeLeft = data.time_left;
        //_isUsed = data.is_used == 1;
        //_exp = data.exp;
        //fashionAttrList = data.fashion_attr_id;
        uuid = data.uuid;
        id = data.id;
        category = data.category;
        num = data.num == 0 ? num : data.num;
        level = data.level == 0 ? level : data.level;
        quality = data.quality == GoodsQuality.GQ_NONE ? quality : data.quality;
        is_equip = data.is_equip == 1 ? true : false;
        _timeLeft = data.time_left == 0 ? _timeLeft : data.time_left;
        _isUsed = data.is_used == 1 ? true : false;
        _exp = data.exp == 0 ? _exp : data.exp;
        fashionAttrList = data.fashion_attr_id;

        GoodsAttrConfig config = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(id);
        if (config != null)
        {
            suit_id = config.suit_id;
            icon = config.icon;
            uint subUint = 0;
            canUse = config.can_use == 1 ? true : false;
            canSell = config.can_sell == 1 ? true : false;

            if (category == GoodsCategory.GC_EQUIPMENT || category == GoodsCategory.GC_CONSUME)
            {
                if (config.sub_category != "")
                {
                    subUint = uint.Parse(config.sub_category);
                }
                subCategory = (EquipmentType)subUint;
            }
            if (category == GoodsCategory.GC_BADGE)
            {
                if (config.sub_category != "")
                {
                    subUint = uint.Parse(config.sub_category);
                }
                badgeCategory = (BadgeCG)subUint;
            }

            if (category == GoodsCategory.GC_FASHION)
            {
                _fashionSubCategory.Clear();
                string[] strA = config.sub_category.Split('&');
                foreach( string s in strA )
                {
                    subUint = uint.Parse(s);
                    _fashionSubCategory.Add(subUint);
                }    
            }

            name = config.name;
            if (suit_id != 0) //套装
            {
                subName = CommonFunction.GetConstString("STR_SUIT_PART" + subUint);
            }
            else
                subName = "";
        }
    }

    public ulong GetUUID()
    {
        return uuid;
    }


    public uint getTimeLeft()
    {
        return _timeLeft;
    }

    public void decreaseTimeLeft()
    {
        if( _timeLeft > 0 && _timeLeft != 0x7fffffff )
        {
            _timeLeft--;
        }
    }


    public bool isUsed()
    {
        return _isUsed;
    }
    public uint GetID()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public string GetSubName()
    {
        return subName;
    }

    public string GetIcon()
    {
        return icon;
    }

    public GoodsCategory GetCategory()
    {
        return category;
    }

    public EquipmentType GetSubCategory()
    {
        return subCategory;
    }

    public BadgeCG GetBadgeCategory()
    {
        return badgeCategory;
    }

    public List<uint> GetFashionSubCategory()
    {
        return _fashionSubCategory;
    }

    public uint GetNum()
    {
        return num;
    }

    public void SetNum(uint newNum)
    {
        num = newNum;
    }

    public uint GetLevel()
    {
        return level;
    }

    public void SetLevel(uint newLevel)
    {
        level = newLevel;
    }

    public GoodsQuality GetQuality()
    {
        return quality;
    }

    public void SetQuality(GoodsQuality newQuality)
    {
        quality = newQuality;
    }

    public bool IsEquip()
    {
        return is_equip;
    }

    public void Equip()
    {
        is_equip = true;
    }

    public void Unequip()
    {
        is_equip = false;
    }

	public uint GetExp()
	{
		return _exp;
	}

	public void SetExp(uint exp)
	{
		_exp = exp;
	}

    public bool CanUser()
    {
        return canUse;
    }

    public bool CanSell()
    {
        return canSell;
    }

    public uint GetSuitID()
    {
        return suit_id;
    }

    public bool IsSuit()
    {
        return suit_id == 0 ? false : true;
    }

    public bool IsInjectExp()
    {
        return GetCategory() == GoodsCategory.GC_CONSUME && (int)GetSubCategory()==3;
    }

    public string GetFashionAtlas(uint fashionid) 
    {
        List<uint> types = new List<uint>();

        string[] arr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionid).sub_category.Split('&');
        foreach (string str in arr)
        {
            types.Add(uint.Parse(str));
        }
        if (types.Count == 1)
        {
            uint type = types[0];
            if (type == 1 || type == 2 || type == 5)
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

    public List<uint> GetFashionAttrIDList() 
    {
        return fashionAttrList;
    }
};