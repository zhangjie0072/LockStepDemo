using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class StealConfig
{
    string name = GlobalConst.DIR_XML_STEAL_STATE_RATIO;
    bool isLoadFinish = false;
    private object LockObject = new object();

	Dictionary<PlayerState.State, IM.Number> ratios = new Dictionary<PlayerState.State, IM.Number>();

	public StealConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadRatio();
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
        ReadRatio();
		Logger.ConfigEnd(name);
    }

    void ReadRatio()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_STEAL_STATE_RATIO, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;
			PlayerState.State state = (PlayerState.State)(int.Parse(line.SelectSingleNode("state").InnerText));
			IM.Number ratio = IM.Number.Parse(line.SelectSingleNode("ratio").InnerText);
			ratios.Add(state, ratio);
		}
	}

	public IM.Number GetRatio(PlayerState.State state)
	{
		IM.Number ratio;
		if (ratios.TryGetValue(state, out ratio))
			return ratio;
		return IM.Number.zero;
	}
}
