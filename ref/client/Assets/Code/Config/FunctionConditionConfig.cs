
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public class FuncCondition
{
	public string name;
	public List<ConditionType> conditionTypes = new List<ConditionType>();
	public List<string> conditionParams = new List<string>();
	public string lockTip;
	public string unlockTip;
}

public class FunctionConditionConfig
{
    string name = GlobalConst.DIR_XML_FUNCTION_CONDITION;
    bool isLoadFinish = false;
    private object LockObject = new object();

	private Dictionary<string, FuncCondition> funcConditions = new Dictionary<string, FuncCondition>();
	private Dictionary<ConditionType, List<FuncCondition>> groupedConditions = new Dictionary<ConditionType, List<FuncCondition>>();

	public FunctionConditionConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public bool ValidateFunc(string funcName)
	{
		FuncCondition conditionData = GetFuncCondition(funcName);
		return conditionData != null ? ValidateFunc(conditionData) : true;
	}

	public bool ValidateFunc(FuncCondition funcCond)
	{
		for (int i = 0; i < funcCond.conditionTypes.Count; ++i)
		{
			if (!ConditionValidator.Instance.Validate(funcCond.conditionTypes[i], funcCond.conditionParams[i]))
			{
				return false;
			}
		}
		return true;
	}

	public List<FuncCondition> GetFuncConditions(ConditionType conditionType)
	{
		List<FuncCondition> list = null;
		groupedConditions.TryGetValue(conditionType, out list);
		return list;
	}

	public FuncCondition GetFuncCondition(string name)
	{
		FuncCondition conditionData = null;
		funcConditions.TryGetValue(name, out conditionData);
		return conditionData;
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
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
			FuncCondition data = new FuncCondition();
			data.name = line.SelectSingleNode("id").InnerText;
			string types = line.SelectSingleNode("type").InnerText;
			if (!string.IsNullOrEmpty(types))
			{
				string[] tokens = types.Split('&');
				foreach (string token in tokens)
				{
					data.conditionTypes.Add((ConditionType)int.Parse(token));
				}
			}
			string param = line.SelectSingleNode("param").InnerText;
			if (!string.IsNullOrEmpty(param))
			{
				data.conditionParams.AddRange(param.Split('&'));
			}
			if (data.conditionTypes.Count != data.conditionParams.Count)
				Debug.LogError("Function condition config error, wrong param num. FuncName: " + data.name);
			data.lockTip = line.SelectSingleNode("lock_tip").InnerText;
			data.unlockTip = line.SelectSingleNode("unlock_tip").InnerText;
			foreach (ConditionType type in data.conditionTypes)
			{
				List<FuncCondition> list;
				if (!groupedConditions.TryGetValue(type, out list))
				{
					list = new List<FuncCondition>();
					groupedConditions.Add(type, list);
				}
				list.Add(data);
			}
			funcConditions.Add(data.name, data);
		}
		
	}
}
