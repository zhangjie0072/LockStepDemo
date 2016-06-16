using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;


public class TalentItem : ItemBase
{
    public string name;
    public string icon;
    public string desc;
}

public class TalentConfig : ConfigBase
{
    Dictionary<uint, TalentItem> TalentItems = null;

    public TalentConfig()
        : base(GlobalConst.DIR_XML_TALENT)
    {
        
    }

    protected override void ReadConfigParse()
    {
        this.TalentItems = XMLUtility.LoadXml<TalentItem>(this.name);
    }

    public TalentItem GetConfigData(uint id)
    {
        if (this.TalentItems == null)
            return null;

        if (this.TalentItems.ContainsKey(id))
            return this.TalentItems[id];

        return null;
    }
}