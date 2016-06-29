using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;
using fogs.proto.msg;


public class TaskDataConfig
{
    string name1 = GlobalConst.DIR_XML_TASK_MAIN;
    string name2 = GlobalConst.DIR_XML_TASK_DAILY;
	string name3 = GlobalConst.DIR_XML_TASK_LINK;
	string name4 = GlobalConst.DIR_XML_TASK_LEVEL;
	bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	public static Dictionary<uint, TaskConfig> taskMainConfig = new Dictionary<uint, TaskConfig>();//常规任务
	public static Dictionary<uint, TaskConfig> taskLevelConfig = new Dictionary<uint, TaskConfig>();//level任务
    public static Dictionary<uint, TaskConfig> taskDailyConfig = new Dictionary<uint, TaskConfig>();//日常任务
    public static Dictionary<uint, string> taskLinkConfig = new Dictionary<uint, string>(); //任务前往链接

    public uint GetTypeById(uint id)
    {
        if( taskMainConfig.ContainsKey(id) )
        {
            TaskConfig conf;
            taskMainConfig.TryGetValue(id,out conf);
            return conf.type;
        }
        else if (taskDailyConfig.ContainsKey(id))
        {
            TaskConfig conf;
            taskDailyConfig.TryGetValue(id, out conf);
            return conf.type;
        }
        return 0;
    }

    //构造函数初始化
    public TaskDataConfig()
    {
        Initialize();
    }
    //初始化函数
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TASK_MAIN, LoadFinish);
        //ReadTaskMainData();
		ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TASK_DAILY, LoadFinish);
//		ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TASK_LEVEL, LoadFinish);
        //ReadTaskDailyData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TASK_LINK, LoadFinish);
        //ReadTaskLinkData();
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 3)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
		Debug.Log("Config reading " + name1);
		ReadTaskMainData();
		
		Debug.Log("Config reading " + name2);
		ReadTaskDailyData();
		
		//		ReadTaskLevelData();
		Debug.Log("Config reading " + name3);
		ReadTaskLinkData();
		
    }

    //读取常规任务配置
    public void ReadTaskMainData()
    {
		string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        taskMainConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TASK_MAIN, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            TaskConfig config = new TaskConfig();
            foreach (XmlElement xel in xe)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.type = value;
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;
                }
                else if (xel.Name == "title")
                {
                    config.title = xel.InnerText;
                }
                else if (xel.Name == "desc")
                {
                    config.desc = xel.InnerText;
                }
                else if (xel.Name == "award_tips")
                {
                    config.award_tips = xel.InnerText;
                }
                else if (xel.Name == "award_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.award_id = value;
                }
                else if (xel.Name=="condition_id1")
                {
                    uint.TryParse(xel.InnerText, out value);
			
                }
                else if (xel.Name=="condition_value1")
                {
                    uint.TryParse(xel.InnerText, out value);
                }
				else if (xel.Name=="precondition_value1")
				{
					uint.TryParse(xel.InnerText, out value);
					if(config.precondition.Count <=0)
					{
						TaskConditionInfo tc = new TaskConditionInfo();
						tc.condition_value = value;
						config.precondition.Add(tc);
					}
					else{
						config.precondition[0].condition_value = value;
					}
				}

				else if (xel.Name=="precondition_id1")
				{
					uint.TryParse(xel.InnerText, out value);

					if(config.precondition.Count <=0)
					{
						TaskConditionInfo tc = new TaskConditionInfo();
						tc.condition_id = value;
						config.precondition.Add(tc);
					}
					else{
						config.precondition[0].condition_id = value;
					}
				}
                else if(xel.Name=="show_process")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.show_process = value;
                }
                else if (xel.Name == "link")
                {
                    if (xel.InnerText.Contains(":"))
                    {
                        string[] entirety = xel.InnerText.Split(':');
                        config.link.Add(uint.Parse(entirety[0]));
                        config.link.Add(uint.Parse(entirety[1]));
                    }
                    else
                    {
                        config.link.Add(uint.Parse(xel.InnerText));
                    }
                }
            }
            if (!taskMainConfig.ContainsKey(config.id))
            {
                taskMainConfig.Add(config.id, config);
            }
        }
    }
    //读取日常任务配置
    public void ReadTaskDailyData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        taskDailyConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TASK_DAILY, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            TaskConfig config = new TaskConfig();
            foreach (XmlElement xel in xe)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.type = value;
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;
                }
                else if (xel.Name == "title")
                {
                    config.title = xel.InnerText;
                }
                else if (xel.Name == "desc")
                {
                    config.desc = xel.InnerText;
                }
                else if (xel.Name == "award_tips")
                {
                    config.award_tips = xel.InnerText;
                }
                else if (xel.Name == "award_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.award_id = value;
                }
                else if (xel.Name == "show_process")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.show_process = value;
                }
                else if (xel.Name == "link")
                {
                    if (xel.InnerText.Contains(":"))
                    {
                        string[] entirety = xel.InnerText.Split(':');
                        config.link.Add(uint.Parse(entirety[0]));
                        config.link.Add(uint.Parse(entirety[1]));
                    }
                    else
                    {
                        config.link.Add(uint.Parse(xel.InnerText));
                    }
                }
                else if (xel.Name == "activity")
                {
                    config.activity = uint.Parse(xel.InnerText);
                }

            }
            if (!taskDailyConfig.ContainsKey(config.id))
            {
                taskDailyConfig.Add(config.id, config);
            }
        }
    }
	//读取level任务配置
	public void ReadTaskLevelData()
	{
//		string text = ResourceLoadManager.Instance.GetConfigText(name4);
//		if (text == null)
//		{
//			Debug.LogError("LoadConfig failed: " + name4);
//			return;
//		}
//		taskLevelConfig.Clear();
//		
//		//读取以及处理XML文本的类
//		XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TASK_LEVEL, text);
//		//解析xml的过程
//		XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
//		foreach (XmlElement xe in nodelist)
//		{
//			XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
//			if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
//				continue;
//			
//			TaskConfig config = new TaskConfig();
//			foreach (XmlElement xel in xe)
//			{
//				uint value;
//				if (xel.Name == "id")
//				{
//					uint.TryParse(xel.InnerText, out value);
//					config.id = value;
//				}
//				else if (xel.Name == "type")
//				{
//					uint.TryParse(xel.InnerText, out value);
//					config.type = value;
//				}
//				else if (xel.Name == "icon")
//				{
//					config.icon = xel.InnerText;
//				}
//				else if (xel.Name == "title")
//				{
//					config.title = xel.InnerText;
//				}
//				else if (xel.Name == "desc")
//				{
//					config.desc = xel.InnerText;
//				}
//				else if (xel.Name == "award_tips")
//				{
//					config.award_tips = xel.InnerText;
//				}
//				else if (xel.Name == "award_id")
//				{
//					uint.TryParse(xel.InnerText, out value);
//					config.award_id = value;
//				}
//				else if (xel.Name == "show_process")
//				{
//					uint.TryParse(xel.InnerText, out value);
//					config.show_process = value;
//				}
//				else if (xel.Name == "link")
//				{
//					if (xel.InnerText.Contains(":"))
//					{
//						string[] entirety = xel.InnerText.Split(':');
//						config.link.Add(uint.Parse(entirety[0]));
//						config.link.Add(uint.Parse(entirety[1]));
//					}
//					else
//					{
//						config.link.Add(uint.Parse(xel.InnerText));
//					}
//				}
////				else if (xel.Name == "activity")
////				{
////					config.activity = uint.Parse(xel.InnerText);
////				}
//				
//			}
//			if (!taskLevelConfig.ContainsKey(config.id))
//			{
//				taskLevelConfig.Add(config.id, config);
//			}
//
//		}
	}
    //读取任务前往链接配置
    public void ReadTaskLinkData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name3);
            return;
        }
        taskLinkConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TASK_LINK, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            uint ID = 0;
            string Name = "";
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "ID")
                {
                    uint.TryParse(xel.InnerText, out ID);
                }
                else if (xel.Name == "UIName")
                {
                    Name = xel.InnerText;
                }
            }
            if (!taskLinkConfig.ContainsKey(ID))
            {
                taskLinkConfig.Add(ID, Name);
            }
        }
    }

    /// <summary>
    ///根据任务ID返回常规任务信息
    /// </summary>
    /// <param name="taskID">常规任务ID</param>
    /// <returns></returns>
    public TaskConfig GetTaskMainInfoByID(uint taskID)
    {
        if (taskMainConfig.ContainsKey(taskID))
        {
            return taskMainConfig[taskID];
        }
        return null;
    }
    /// <summary>
    /// 根据任务ID返回日常任务信息
    /// </summary>
    /// <param name="taskID">日常任务ID</param>
    /// <returns></returns>
    public TaskConfig GetTaskDailyInfoByID(uint taskID)
    {
        if (taskDailyConfig.ContainsKey(taskID))
        {
			
            return taskDailyConfig[taskID];
        }
		Debug.Log("no task info of id "+taskID);
        return null;
	}
	/// <summary>
	/// 根据任务ID返回level任务信息
	/// </summary>
	/// <param name="taskID">日常任务ID</param>
	/// <returns></returns>
	public TaskConfig GetTaskLevelInfoByID(uint taskID)
	{
		if (taskMainConfig.ContainsKey(taskID))
		{
			if(taskMainConfig[taskID].precondition.Count>0 && taskMainConfig[taskID].precondition[0].condition_id == 1)
				return taskMainConfig[taskID];
		}
		return null;
	}
    /// <summary>
    /// 根据常规任务ID找到奖励库ID
    /// </summary>
    /// <param name="taskID"></param>
    /// <returns></returns>
    public uint GetTaskMainAwardID(uint taskID)
    {
        if (taskMainConfig.ContainsKey(taskID))
        {
            return taskMainConfig[taskID].award_id;
        }
        return 0;
    }
    /// <summary>
    /// 根据日常任务ID找到奖励库ID
    /// </summary>
    /// <param name="taskID"></param>
    /// <returns></returns>
    public uint GetTaskDailyAwardID(uint taskID)
    {
        if (taskDailyConfig.ContainsKey(taskID))
        {
            return taskDailyConfig[taskID].award_id;
        }
        return 0;
    }
	/// <summary>
	/// 根据level任务ID找到奖励库ID
	/// </summary>
	/// <param name="taskID"></param>
	/// <returns></returns>
	public uint GetTaskLevelAwardID(uint taskID)
	{
		if (taskMainConfig.ContainsKey(taskID))
		{
			if(taskMainConfig[taskID].precondition.Count>0 && taskMainConfig[taskID].precondition[0].condition_id == 1)
				return taskMainConfig[taskID].award_id;
		}
		return 0;
	}
	public uint GetTaskPreConditionValueById(uint taskID)
	{
		if (taskMainConfig.ContainsKey(taskID))
		{
			if(taskMainConfig[taskID].precondition.Count>0 && taskMainConfig[taskID].precondition[0].condition_id == 1)
				return taskMainConfig[taskID].precondition[0].condition_value;
		}
		return 0;
	}
    public string GetLinkUIName(uint uiid)
    {
        if (taskLinkConfig.ContainsKey(uiid))
            return taskLinkConfig[uiid];

        return string.Empty;
    }

    public string GetTaskLinkUIName(uint taskID)
    {
        if (taskMainConfig.ContainsKey(taskID))
        {
            if (taskMainConfig[taskID].link.Count == 0)
                return string.Empty;

            uint uiid = taskMainConfig[taskID].link[0];
            if (taskLinkConfig.ContainsKey(uiid))
                return taskLinkConfig[uiid];
        }
        else if (taskDailyConfig.ContainsKey(taskID))
        {
            if (taskDailyConfig[taskID].link.Count == 0)
                return string.Empty;

            uint uiid = taskDailyConfig[taskID].link[0];
            if (taskLinkConfig.ContainsKey(uiid))
                return taskLinkConfig[uiid];
        }
		else if (taskLevelConfig.ContainsKey(taskID))
		{
			if (taskLevelConfig[taskID].link.Count == 0)
				return string.Empty;
			
			uint uiid = taskLevelConfig[taskID].link[0];
			if (taskLinkConfig.ContainsKey(uiid))
				return taskLinkConfig[uiid];
		}
        return string.Empty;
    }

    public uint GetTaskLinkSubID(uint taskID)
    {
        if (taskMainConfig.ContainsKey(taskID))
        {
            if (taskMainConfig[taskID].link.Count == 2)
                return taskMainConfig[taskID].link[1];
        }
        else if (taskDailyConfig.ContainsKey(taskID))
        {
            if (taskDailyConfig[taskID].link.Count == 2)
                return taskDailyConfig[taskID].link[1];
		}
		else if (taskLevelConfig.ContainsKey(taskID))
		{
			if (taskLevelConfig[taskID].link.Count == 2)
				return taskLevelConfig[taskID].link[1];
		}
        return 0;
    }
}
