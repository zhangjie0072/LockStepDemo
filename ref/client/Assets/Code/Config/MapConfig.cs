using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class MapGroupData
{
	public uint id;
	public string groupName;
	public List<uint> groupIDs = new List<uint>();
	public string describe;
	public uint attrID;
	public uint attrNum;
}

public class MapConfig
{
	string name = GlobalConst.DIR_XML_MAP;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<uint, MapGroupData> mapConfig = new Dictionary<uint, MapGroupData>();
    public List<uint> mapConfigList = new List<uint>();

    public MapConfig()
    {
        Initialize();
    }

    public void Initialize() 
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_MAP, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    public void ReadConfig() 
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name);
		ReadData();
		Logger.ConfigEnd(name);
    }

    void ReadData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(GlobalConst.DIR_XML_MAP);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + GlobalConst.DIR_XML_MAP);
            return;
        }
        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MAP, text);
        XmlNode root = doc.SelectSingleNode("Data");
        foreach (XmlNode line in root.SelectNodes("Line"))
        {
            if (CommonFunction.IsCommented(line))
                continue;
            MapGroupData groupData = new MapGroupData();
            uint index = uint.Parse(line.SelectSingleNode("id").InnerText);
            groupData.id = index;
            groupData.groupName = line.SelectSingleNode("squad_name").InnerText;
            string roleIds = line.SelectSingleNode("squad_ids").InnerText;
            if (roleIds != null && roleIds != "")
            {
                string[] roles = roleIds.Split('&');
                foreach (var id in roles)
                {
                    groupData.groupIDs.Add(uint.Parse(id));
                }
            }
            groupData.attrID = uint.Parse(line.SelectSingleNode("attr_id").InnerText);
            groupData.attrNum = uint.Parse(line.SelectSingleNode("attr_num").InnerText);
            groupData.describe = line.SelectSingleNode("describe").InnerText;

            mapConfig.Add(index, groupData);
            mapConfigList.Add(index);
        }
        //SortMapList();
    }

    public MapGroupData GetMapGroupDataByID(uint index) 
    {
        MapGroupData data;
        mapConfig.TryGetValue(index, out data);
        if (data != null)
            return data;
        return null;
    }

    public void SortMapList(List<uint> mapConfigList) 
    {
        uint a, b, c;
        for (int i = 0; i < mapConfigList.Count - 2; i++)
        {
            a = mapConfigList[i];
            b = mapConfigList[i + 1];
            if ((i + 1) % 2 == 0 && (i + 1) % 4 != 0)
            {
                c = mapConfigList[i];
                mapConfigList[i] = mapConfigList[i + 1];
                mapConfigList[i + 1] = c;
            }
        }
    }
}
