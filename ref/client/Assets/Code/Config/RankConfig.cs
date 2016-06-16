
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using fogs.proto.msg;

namespace fogs.proto.config{
	public partial class RankConfig
	{
		public RankType rank_type;
		public RankSubType rank_sub_type;
		//public ConditionType condition_type;
		//public string condition_param;
	}
}

public class RankConfig
{
    string name = GlobalConst.DIR_XML_RANK;
    bool isLoadFinish = false;
    private object LockObject = new object();
	HashSet<fogs.proto.config.RankConfig> configs = new HashSet<fogs.proto.config.RankConfig>();

	public RankConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadRankConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

	public fogs.proto.config.RankConfig GetConfig(RankType rank_type)
	{
		return (from c in configs where c.rank_type == rank_type select c).FirstOrDefault();
	}

	public fogs.proto.config.RankConfig GetConfig(RankType rank_type, RankSubType rank_sub_type)
	{
		return (from c in configs where c.rank_type == rank_type && c.rank_sub_type == rank_sub_type select c).FirstOrDefault();
	}

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_RANK, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			fogs.proto.config.RankConfig data = new fogs.proto.config.RankConfig();
			data.type = uint.Parse(line.SelectSingleNode("type").InnerText);
			data.rank_type = (RankType)data.type;
			data.sub_type = uint.Parse(line.SelectSingleNode("sub_type").InnerText);
			data.rank_sub_type = (RankSubType)data.sub_type;
			data.default_display = uint.Parse(line.SelectSingleNode("default_display").InnerText);
			data.points_name = line.SelectSingleNode("points_name").InnerText;
			data.limit_condition = line.SelectSingleNode("limit_condition").InnerText;
			//string[] tokens = data.limit_condition.Split(':');
			//data.condition_type = (ConditionType)int.Parse(tokens[0]);
			//data.condition_param = tokens[1];
			data.max_item = uint.Parse(line.SelectSingleNode("max_item").InnerText);
			data.max_enable_item = uint.Parse(line.SelectSingleNode("max_enable_item").InnerText);
			data.refresh_time = uint.Parse(line.SelectSingleNode("refresh_time").InnerText);
			data.click_refresh = uint.Parse(line.SelectSingleNode("click_refresh").InnerText);
			data.display_vip = uint.Parse(line.SelectSingleNode("display_vip").InnerText);
			data.display_win_times = uint.Parse(line.SelectSingleNode("display_win_times").InnerText);

			configs.Add(data);
		}

		Logger.ConfigEnd(name);
	}
}
