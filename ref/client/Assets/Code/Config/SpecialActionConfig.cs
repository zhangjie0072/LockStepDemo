using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SpecialActionConfig
{
    string name1 = GlobalConst.DIR_XML_POSITION_ACTION;
    string name2 = GlobalConst.DIR_XML_ROLE_ACTION;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	Dictionary<PositionType, Dictionary<string, string>> positionAction = new Dictionary<PositionType, Dictionary<string, string>>();
	Dictionary<uint, Dictionary<string, string>> roleAction = new Dictionary<uint, Dictionary<string, string>>();

	public SpecialActionConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
		//ReadPositionAction();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
		//ReadRoleAction();
	}
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
    public void ParseConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
		UnityEngine.Debug.Log("Config reading " + name1);
        ReadPositionAction();
		
		UnityEngine.Debug.Log("Config reading " + name2);
		ReadRoleAction();
		
    }

    void ReadPositionAction()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_POSITION_ACTION, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			PositionType position = (PositionType)(int.Parse(line.SelectSingleNode("position").InnerText));
			Dictionary<string, string> actionMap = null;
			if (!positionAction.TryGetValue(position, out actionMap))
			{
				actionMap = new Dictionary<string, string>();
				positionAction.Add(position, actionMap);
			}
			actionMap[line.SelectSingleNode("original_action").InnerText] = line.SelectSingleNode("special_action").InnerText;
		}
	}

    void ReadRoleAction()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROLE_ACTION, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			uint roleID = uint.Parse(line.SelectSingleNode("role_id").InnerText);
			Dictionary<string, string> actionMap = null;
			if (!roleAction.TryGetValue(roleID, out actionMap))
			{
				actionMap = new Dictionary<string, string>();
				roleAction.Add(roleID, actionMap);
			}
			actionMap[line.SelectSingleNode("original_action").InnerText] = line.SelectSingleNode("special_action").InnerText;
		}
	}

	public string GetPositionAction(PositionType position, string originalAction)
	{
		Dictionary<string, string> actionMap = null;
		if (positionAction.TryGetValue(position, out actionMap))
		{
			string specialAction;
			if (actionMap.TryGetValue(originalAction, out specialAction))
				return specialAction;
		}
		return originalAction;
	}

	public Dictionary<string, string> GetPositionActions(PositionType position)
	{
		Dictionary<string, string> actionMap = null;
		positionAction.TryGetValue(position, out actionMap);
		return actionMap;
	}

	public string GetRoleAction(uint roleID, string originalAction)
	{
		Dictionary<string, string> actionMap = null;
		if (roleAction.TryGetValue(roleID, out actionMap))
		{
			string specialAction;
			if (actionMap.TryGetValue(originalAction, out specialAction))
				return specialAction;
		}
		return originalAction;
	}

	public Dictionary<string, string> GetRoleActions(uint roleID)
	{
		Dictionary<string, string> actionMap = null;
		roleAction.TryGetValue(roleID, out actionMap);
		return actionMap;
	}
}
