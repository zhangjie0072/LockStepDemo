using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;
using fogs.proto.msg;

public class TrialData
{
    public uint index;
    public uint id;
	public string icon;
    public string activity;
	public string title;
    public uint score;
	public short showProgress = 1;
	public uint award_id;
//    public Dictionary<uint, uint> awards = new Dictionary<uint,uint>();
    public List<uint> link = new List<uint>();
}


public class TrialConfig
{
    string name = GlobalConst.DIR_XML_TRAL;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public static Dictionary<uint, List<TrialData>> trialConfig = new Dictionary<uint, List<TrialData>>();
   

    //构造函数初始化
    public TrialConfig()
    {
        Initialize();
    }
    //初始化函数
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TRAL, LoadFinish);

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
		Debug.Log("Config reading " + name);
        ReadTrialData();
		
       
    }

    public void ReadTrialData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        trialConfig.Clear();
        
        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TRAL, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            List<TrialData> data = new List<TrialData>();
            TrialData config = new TrialData();
            Dictionary<uint, uint> awards = new Dictionary<uint,uint>();
            uint day = 0;
            uint key = 0;
            uint value = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "day")
                {
                   day = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "id")
                {
                    config.id = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "index")
                {
                    config.index = uint.Parse(xel.InnerText);
				} 
				if (xel.Name == "icon")
				{
					config.icon = xel.InnerText;
				}
				else if (xel.Name == "title")
				{
					config.title = xel.InnerText;
				}
                else if (xel.Name == "activity")
                {
                    config.activity = xel.InnerText;
                }
                else if (xel.Name == "score")
                {
                    config.score = uint.Parse(xel.InnerText);
                }
//                else if (xel.Name == "award_id1")
//                { 
//                    key = uint.Parse(xel.InnerText);
//                }
//                else if (xel.Name == "award_value1")
//                {
//                    value = uint.Parse(xel.InnerText);
//                    config.awards.Add(key, value);                   
//                }
//                else if (xel.Name == "award_id2")
//                {
//                    key = uint.Parse(xel.InnerText);
//                }
//                else if (xel.Name == "award_value2")
//                {
//                     value = uint.Parse(xel.InnerText);
//                     if (!config.awards.ContainsKey(key))
//                     config.awards.Add(key, value); 
//                }
//                else if(xel.Name=="award_id3")
//                {
//                     key = uint.Parse(xel.InnerText);
//
//                }
//                else if(xel.Name=="award_value3")
//                {
//                    value = uint.Parse(xel.InnerText);
//                    if (!config.awards.ContainsKey(key))
//                    config.awards.Add(key, value);
				//				}
				else if(xel.Name=="award_id")
				{
					uint award_id;
					if(uint.TryParse(xel.InnerText,out award_id))
					{
						config.award_id = award_id;
					}
					else{
						config.award_id = 0;
					}

				}
				else if(xel.Name=="showProgress")
				{
					short show;
					if(short.TryParse(xel.InnerText,out show))
					{
						config.showProgress = show;
					}
					else{
						config.showProgress = 1;
					}

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
            if (!trialConfig.ContainsKey(day))
            {
                data.Add(config);
                trialConfig.Add(day, data);
            }
            else
            {
                trialConfig[day].Add(config);
            }
        }
    }

    public List<TrialData> GetTrialListByDay(uint day)
    {

        return trialConfig[day];
    }

    public TrialData GetTrialDataByIndex(uint day, int index)
    {
        if (trialConfig.ContainsKey(day))
            if (trialConfig[day].Count >= index)
                return trialConfig[day][index - 1];
        return null;
    }

    public uint GetTotalScore()
    {
        uint score = 0;
        uint index = 1;
        foreach (KeyValuePair<uint, List<TrialData>> x in trialConfig)
        {
            if (index > 7)
                break;
            for (int i = 0; i < x.Value.Count; ++i)
            {
                score = score + x.Value[i].score;
            }
            ++index;
        }
        return score;
    }
}
