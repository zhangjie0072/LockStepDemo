using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


public class AIConfig
{
    string name = GlobalConst.DIR_XML_AI;
    string nameAIName = GlobalConst.DIR_XML_AI_NAME;
    bool isLoadFinish = false;

    uint count = 0;
    private object LockObject = new object();
	public class AI
	{
		public uint ID;
		public IM.Number delay;		//思考延迟
		//误差
		public IM.Number devTime;			//时间
		public IM.Number devBallHeight;		//球高度
		public IM.Number devAngleAttacker;	//与进攻者角度（扇形区）
		public IM.Number devDistAOD;		//与进攻者距离（扇形区）
        public IM.Number devDistFront;		//与进攻者距离（正面区）
        public IM.Number devDistBall;		//与球的距离
        public IM.Number devDistBlock;		//与进攻者距离（盖帽）
        public IM.Number devTimeBlock;		//盖帽的当前时间
        public IM.Number devFavArea;		//幸运区
        public IM.Number devAngleDefender;	//防守者的角度
        public IM.Number devRateDefCross;	//正确防守突破的机率
	}

	public Dictionary<uint, AI> configs = new Dictionary<uint, AI>();
    public List<string> names = new List<string>(); 


	public AIConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(nameAIName, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if( count == 2)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
	public string GetRandAIName()
	{
		if(names.Count<=0 ) 
			return "";
		int idx = UnityEngine.Random.Range(0,names.Count-1);
		return names[idx];
	}
	public AI GetConfig(uint ID)
	{
		AI config;
		configs.TryGetValue(ID, out config);
		return config;
	}

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		UnityEngine.Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_AI, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			AI data = new AI();
			data.ID = uint.Parse(line.SelectSingleNode("AIID").InnerText);
			data.delay = IM.Number.Parse(line.SelectSingleNode("delay").InnerText) / 1000;
            data.devTime = IM.Number.Parse(line.SelectSingleNode("time").InnerText);
            data.devBallHeight = IM.Number.Parse(line.SelectSingleNode("ball_height").InnerText);
            data.devAngleAttacker = IM.Number.Parse(line.SelectSingleNode("angle_attacker").InnerText);
            data.devDistAOD = IM.Number.Parse(line.SelectSingleNode("dist_AOD").InnerText);
            data.devDistFront = IM.Number.Parse(line.SelectSingleNode("dist_front").InnerText);
            data.devDistBall = IM.Number.Parse(line.SelectSingleNode("dist_ball").InnerText);
            data.devDistBlock = IM.Number.Parse(line.SelectSingleNode("dist_block").InnerText);
            data.devTimeBlock = IM.Number.Parse(line.SelectSingleNode("time_block").InnerText);
            data.devFavArea = IM.Number.Parse(line.SelectSingleNode("favorite_area").InnerText);
            data.devAngleDefender = IM.Number.Parse(line.SelectSingleNode("angle_defender").InnerText);
            data.devRateDefCross = IM.Number.Parse(line.SelectSingleNode("rate_def_cross").InnerText);

			configs.Add(data.ID, data);
		}

		
		// Read AIName
		UnityEngine.Debug.Log("Config reading " + nameAIName);
		ReadAIName();
	}

    void ReadAIName()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(nameAIName );
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + nameAIName);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_AI_NAME, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			uint id = uint.Parse(line.SelectSingleNode("id").InnerText);
			string name = line.SelectSingleNode("name").InnerText;
			names.Add(name);
		}

    }
}
