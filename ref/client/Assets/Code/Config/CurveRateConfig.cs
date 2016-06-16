using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class CurveRateConfig
{
    string name = GlobalConst.DIR_XML_CURVE_RATE;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public class HeightRate
    {
		public IM.Number minHeight;
		public IM.Number maxHeight;
		public IM.Number rate;
    }

    public Dictionary<int, List<HeightRate>> configData = new Dictionary<int, List<HeightRate>>();

    public CurveRateConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
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
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        
        configData.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CURVE_RATE, text);
        XmlNode root = xmlDoc.SelectSingleNode("Data");
        foreach (XmlNode line in root.SelectNodes("Line"))
        {
			if (CommonFunction.IsCommented(line))
				continue;

			int sector = int.Parse(line.SelectSingleNode("sector").InnerText);
			HeightRate heightRate = new HeightRate();
			heightRate.minHeight = IM.Number.Parse(line.SelectSingleNode("min_height").InnerText);
			heightRate.maxHeight = IM.Number.Parse(line.SelectSingleNode("max_height").InnerText);
			heightRate.rate = IM.Number.Parse(line.SelectSingleNode("rate").InnerText);
			List<HeightRate> rates = null;
			if (!configData.TryGetValue(sector, out rates))
			{
				rates = new List<HeightRate>();
				configData.Add(sector, rates);
			}
			rates.Add(heightRate);
		}
		Logger.ConfigEnd(name);
    }

    public List<HeightRate> GetHeightRates(int sector)
    {
        List<HeightRate> heightRates;
        configData.TryGetValue(sector, out heightRates);
        return heightRates;
    }

	public HeightRate GetHeightRange(int sector, IM.Number rate)
	{
		List<HeightRate> rates = GetHeightRates(sector);
		if (rates != null)
		{
			IM.Number totalRate = IM.Number.zero;
			foreach (HeightRate heightRate in rates)
			{
				totalRate += heightRate.rate;
				if (rate < totalRate)
					return heightRate;
			}
		}

		return null;
	}
}
