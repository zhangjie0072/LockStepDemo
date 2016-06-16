using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class LotteryConfig
{
    string name = GlobalConst.DIR_XML_LOTTERY;
    bool isLoadFinish = false;
    private object LockObject = new object();

	private Dictionary<uint, List<fogs.proto.config.LotteryConfig>> configs = new Dictionary<uint, List<fogs.proto.config.LotteryConfig>>();

	public LotteryConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

	public fogs.proto.config.LotteryConfig GetLottery(uint type, uint level)
	{
		List<fogs.proto.config.LotteryConfig> lotteries;
		if (configs.TryGetValue(type, out lotteries))
		{
			foreach (fogs.proto.config.LotteryConfig lottery in lotteries)
			{
				if (lottery.level_min <= level && level <= lottery.level_max)
					return lottery;
			}
		}
		return null;
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
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_LOTTERY, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;
			fogs.proto.config.LotteryConfig data = new fogs.proto.config.LotteryConfig();
			data.type = uint.Parse(line.SelectSingleNode("type").InnerText);
			data.level_min = uint.Parse(line.SelectSingleNode("level_min").InnerText);
			data.level_max = uint.Parse(line.SelectSingleNode("level_max").InnerText);
			data.goods_id = uint.Parse(line.SelectSingleNode("goods_id").InnerText);
			data.consume_id = uint.Parse(line.SelectSingleNode("consume_id").InnerText);
			data.consume_num_single = uint.Parse(line.SelectSingleNode("consume_num_single").InnerText);
			data.consume_num_multi = uint.Parse(line.SelectSingleNode("consume_num_multi").InnerText);
			data.normal_award_pack = uint.Parse(line.SelectSingleNode("normal_award_pack").InnerText);
			data.special_award_pack = uint.Parse(line.SelectSingleNode("special_award_pack").InnerText);
			List<fogs.proto.config.LotteryConfig> lotteries;
			if (!configs.TryGetValue(data.type, out lotteries))
			{
				lotteries = new List<fogs.proto.config.LotteryConfig>();
				configs.Add(data.type, lotteries);
			}
			lotteries.Add(data);
		}
		Logger.ConfigEnd(name);
	}
}
