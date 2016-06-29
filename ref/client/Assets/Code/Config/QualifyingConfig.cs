using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;


public class QualifyingAwardsData
{

    public uint id;
    public uint value;
    public uint type_count;
}

public class DataById
{
    public uint rank_min;
    public uint rank_max;
    public List<QualifyingAwardsData> databyid = new List<QualifyingAwardsData>();

}

public class RobotPlayer
{
    public uint star;
    public uint aptitude;
}

public class RobotTeam
{
    public uint rank_min;
    public uint rank_max;
    public uint AI_id;
    public uint level;
    public uint basic;
    public float hedging;
    public List<RobotPlayer> robotTeam = new List<RobotPlayer>();
}

public class QualifyingConsume
{
    public uint buy_times;
    public uint consume_type;
    public uint consume_value;
}


public class QualifyingConfig
{
    string name1 = GlobalConst.DIR_XML_QUALIFYING_DAYWARDS;
    string name2 = GlobalConst.DIR_XML_QUALIFYING_ROBOT;
    string name3 = GlobalConst.DIR_XML_QUALIFYING_CONSUME;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    //public List<DataById> RankAwardsData = new List<DataById>();
    public List<DataById> DayAwardsData = new List<DataById>();
    public List<RobotTeam> RobotPlayerData = new List<RobotTeam>();
	public List<QualifyingConsume> qualifyingConsumes = new List<QualifyingConsume>();

    public QualifyingConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //ReadQualifyingAwards();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        //ReadQualifyingRobot();
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
		//ReadConsume();
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
		ReadQualifyingAwards();
		Debug.Log("Config reading " + name2);
		
		ReadQualifyingRobot();
		Debug.Log("Config reading " + name3);
		
		ReadConsume();
		
    }


    //public VipData GetVipData(uint level)
    //{
    //    VipData vipData = null;
    //    Vipdatas.TryGetValue(level, out vipData);
    //    return vipData;
    //}

    //void ReadQualifyingAwards()
    //{
    //    AwardsData.Clear();
    //    XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_QIALIFYING_RANKAWARDS);
    //    XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
    //    //string ranking1 = "" ;
    //    //uint ranking = 0;
    //    //uint id = 0;
    //    DataById DataById = new DataById();
    //    DataById.databyid.Clear();
    //    DataById.rank_min = 1;
    //    DataById.rank_max = 1;
    //    uint min = 0;
    //    uint max = 0;
    //    foreach (XmlElement xe in nodelist)
    //    {
    //        if (CommonFunction.IsCommented(xe))
    //            continue;
    //        string ranking = xe.SelectSingleNode("ranking").InnerText;
    //        if (ranking.IndexOf(",") == -1)
    //        {
    //            min = uint.Parse(ranking);
    //            max = uint.Parse(ranking);
    //        }
    //        else
    //        {
    //            string[] temp = ranking.Split(',');
    //            min = uint.Parse(temp[0]);
    //            max = uint.Parse(temp[1]);
    //        }
    //        if ((DataById.rank_min != min) || (DataById.rank_max != max))
    //        {     
    //            AwardsData.Add(DataById);
    //            DataById = new DataById();
    //            DataById.rank_min = min;
    //            DataById.rank_max = max;

    //        }
    //        QualifyingAwardsData x = new QualifyingAwardsData();
    //        x.id = uint.Parse(xe.SelectSingleNode("id").InnerText);
    //        x.value = uint.Parse(xe.SelectSingleNode("value").InnerText);
    //        x.type_count = uint.Parse(xe.SelectSingleNode("type_count").InnerText);
    //        DataById.databyid.Add(x);

    //    }
    //    AwardsData.Add(DataById);

    //    GetAwardsData(7);
    //}

    void ReadQualifyingAwards()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        //排名奖励
        DayAwardsData.Clear();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_QUALIFYING_DAYWARDS, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        uint min = 0;
        uint max = 0;
        foreach (XmlElement xe in nodelist)
        {
            DataById DataById = new DataById();
            if (CommonFunction.IsCommented(xe))
                continue;
            string ranking = xe.SelectSingleNode("ranking").InnerText;
            if (ranking.IndexOf(",") == -1)
            {
                min = uint.Parse(ranking);
                max = uint.Parse(ranking);
            }
            else
            {
                string[] temp = ranking.Split(',');
                min = uint.Parse(temp[0]);
                max = uint.Parse(temp[1]);
            }
            DataById.rank_min = min;
            DataById.rank_max = max;
            for (int i = 0; i < 3; i++)
            {
                string str = "id" + (i + 1);
                string val = "value" + (i + 1);
                if (xe.SelectSingleNode(str) == null || xe.SelectSingleNode(str).InnerText == "") break;
                QualifyingAwardsData x = new QualifyingAwardsData();
                x.id = uint.Parse(xe.SelectSingleNode(str).InnerText);
                x.value = uint.Parse(xe.SelectSingleNode(val).InnerText);
                DataById.databyid.Add(x);
            }
            DayAwardsData.Add(DataById);

            
        }
    }


    public void ReadConsume()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name3);
            return;
        }
        
        qualifyingConsumes.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_QUALIFYING_CONSUME, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            QualifyingConsume consume = new QualifyingConsume();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "buy_times")
                {
                    uint.TryParse(xel.InnerText, out consume.buy_times);
                }
                else if (xel.Name == "consume_type")
                {
                    uint.TryParse(xel.InnerText, out consume.consume_type);
                }
                else if(xel.Name == "consume_value")
                {
                    uint.TryParse(xel.InnerText, out consume.consume_value);
                }
            }

            qualifyingConsumes.Add(consume);
        } 
    }

    //读取机器人配置
    void ReadQualifyingRobot()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        RobotPlayerData.Clear();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_QUALIFYING_ROBOT, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            uint min, max;
            if (CommonFunction.IsCommented(xe))
                continue;
            RobotTeam data = new RobotTeam();
            string ranking = xe.SelectSingleNode("ranking").InnerText;
            if (ranking.IndexOf(",") == -1)
            {
                min = uint.Parse(ranking);
                max = uint.Parse(ranking);
            }
            else
            {
                string[] temp = ranking.Split(',');
                min = uint.Parse(temp[0]);
                max = uint.Parse(temp[1]);
            }
            data.rank_min = min;
            data.rank_max = max;
            data.AI_id = uint.Parse(xe.SelectSingleNode("AI").InnerText);
            data.level = uint.Parse(xe.SelectSingleNode("level").InnerText);
            string modify = xe.SelectSingleNode("modify").InnerText;
            string[] sep = modify.Split('&');
            if (sep.Length == 2)
            {
                data.basic = uint.Parse(sep[0]);
                data.hedging = float.Parse(sep[1]);
            }
            else
            {
                data.basic = 0;
                data.hedging = 1;
            }

            for (int i = 0; i < 3; i++)
            {
                string star = "star" + (i + 1);
                //string aptitude = "aptitude" + (i + 1);
                RobotPlayer player = new RobotPlayer();
                player.star = uint.Parse(xe.SelectSingleNode(star).InnerText);
                //player.aptitude = uint.Parse(xe.SelectSingleNode(aptitude).InnerText);
                data.robotTeam.Add(player);
            }
            RobotPlayerData.Add(data);
        }

    }
    //根据排名读取配置
    public DataById GetAwardsData(uint rank)
    {
        DataById data = new DataById();
        //for (int i = 0; i < DayAwardsData.Count; i++)
        //{
        //    if (rank >= DayAwardsData[i].rank_min && rank <= DayAwardsData[i].rank_max)
        //    {
        //        data = DayAwardsData[i];
        //        break;
        //    }
        //}
        //return data;
        Debug.Log("---------------qualifydata:" + DayAwardsData.Find(x => rank >= x.rank_min && rank <= x.rank_max).rank_min);
        return DayAwardsData.Find(x => rank >= x.rank_min && rank <= x.rank_max);

    }

    public List<RobotPlayer> GetRobotPlayer(uint level)
    {
        return RobotPlayerData.Find(x => x.level == level).robotTeam;
    }

    public RobotTeam GetRobotPlayerAttr(uint level)
    {
        return RobotPlayerData.Find(x => x.level == level);
    }

    public uint RobotTeamAI(uint rank)
    {
        return RobotPlayerData.Find(x => rank >= x.rank_min && rank <= x.rank_max).AI_id;
    }

	public QualifyingConsume GetQualifyingConsume(uint times)
    {
        return qualifyingConsumes.Find(x => x.buy_times == times);     
    }

}

