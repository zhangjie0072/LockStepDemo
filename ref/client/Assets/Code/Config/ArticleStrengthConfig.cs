using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ArticleStrength
{
    public class Section
    {
        public IM.Number end_time;
        public uint section;
        public IM.Number value;
    }

    public PositionType position;
    public List<Section> sections = new List<Section>();
}

public class ArticleStrengthConfig
{
    string name = GlobalConst.DIR_XML_ARTICLE_STRENGTH;
    bool isLoadFinish = false;
    private object LockObject = new object();

    private Dictionary<PositionType, ArticleStrength> configs = new Dictionary<PositionType, ArticleStrength>();
    private Color[] colors = 
    {
         new Color32(0x59, 0x13, 0x11, 0xFF),
         new Color32(0xFA, 0xC4, 0x35, 0xFF),
         new Color32(0x1C, 0x96, 0x00, 0xFF),
    };

    public ArticleStrengthConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

    public ArticleStrength GetConfig(PositionType position)
    {
        ArticleStrength data = null;
        configs.TryGetValue(position, out data);
        return data;
    }

    public Color GetSectionColor(uint section)
    {
        if (section > colors.Length || section == 0)
            return Color.white;
        return colors[section - 1];
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
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ARTICLE_STRENGTH, text);
        XmlNode node_data = xmlDoc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
			if (CommonFunction.IsCommented(node_line))
				continue;
            ArticleStrength data = new ArticleStrength();
            data.position = (PositionType)(int.Parse(node_line.SelectSingleNode("position").InnerText));
            for (int i = 1; ; ++i)
            {
                XmlNode node_subsection = node_line.SelectSingleNode("subsection_" + i);
                if (node_subsection == null)
                    break;
                ArticleStrength.Section section = new ArticleStrength.Section();
                section.end_time = IM.Number.Parse(node_subsection.InnerText);
                section.section = uint.Parse(node_line.SelectSingleNode("colour_" + i).InnerText);
                section.value = IM.Number.Parse(node_line.SelectSingleNode("value_" + i).InnerText);
                data.sections.Add(section);
            }
            configs.Add(data.position, data);
		}
		Logger.ConfigEnd(name);
    }
}
