using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RoleShape
{
	public uint		roleShapeId;
	public uint		height;
	public uint		body_id;
	public uint		hair_id;
	public uint		upper_id;
	public uint		lower_id;
	public uint		shoes_id;
    public uint     back_id;
}

public class RoleShapeConfig
{
    string name = GlobalConst.DIR_XML_ROLE_SHAPE;
    bool isLoadFinish = false;
    private object LockObject = new object();

	public Dictionary<uint, RoleShape> configs = new Dictionary<uint, RoleShape>();

	public RoleShapeConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public RoleShape GetConfig(uint roleShapeId)
    {
		RoleShape data = null;
		configs.TryGetValue(roleShapeId, out data);
        return data;
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }

        //读取以及处理XML文本的类
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);

        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
			if (CommonFunction.IsCommented(node_line))
				continue;
			RoleShape data = new RoleShape();
			data.roleShapeId = uint.Parse(node_line.SelectSingleNode("role_shape_id").InnerText);
			data.height = uint.Parse(node_line.SelectSingleNode("height").InnerText);
			data.body_id = uint.Parse(node_line.SelectSingleNode("body_id").InnerText);
			data.hair_id = uint.Parse(node_line.SelectSingleNode("hair_id").InnerText);
			data.upper_id = uint.Parse(node_line.SelectSingleNode("upper_id").InnerText);
			data.lower_id = uint.Parse(node_line.SelectSingleNode("lower_id").InnerText);
			data.shoes_id = uint.Parse(node_line.SelectSingleNode("shoes_id").InnerText);
            data.back_id = uint.Parse(node_line.SelectSingleNode("back_id").InnerText);
            
			configs.Add(data.roleShapeId, data);
        }

		
    }
}
