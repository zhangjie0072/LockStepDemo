using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using fogs.proto.msg;

public class AttrReduceConfig
{
    string name = GlobalConst.DIR_XML_ATTR_REDUCE;
    bool isLoadFinish = false;
    private object LockObject = new object();
	public class Entry
	{
		public GameMatch.LeagueType leagueType;
		public uint sectionID;
		public KeyValuePair<uint, uint> levelRange;
		public PositionType position;
		public Dictionary<string, AttrReduceItem> items = new Dictionary<string, AttrReduceItem>();

		public class Comparer : IEqualityComparer<Entry>
		{
			public bool Equals(Entry l, Entry r)
			{
				return l.leagueType == r.leagueType 
					&& l.sectionID == r.sectionID
					&& l.levelRange.Key == r.levelRange.Key
					&& l.levelRange.Value == r.levelRange.Value
					&& l.position == r.position;
			}
			public int GetHashCode(Entry e)
			{
				int hCode = (int)e.leagueType ^ (int)e.sectionID ^ (int)e.levelRange.Key ^ (int)e.levelRange.Value ^ (int)e.position;
				return hCode.GetHashCode();
			}
		}
	}

	public class AttrReduceItem
	{
		public string attr;
		public uint requireValue;
		public IM.Number scaleFactor;
	}

	private HashSet<Entry> config = new HashSet<Entry>(new Entry.Comparer());

	public AttrReduceConfig()
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
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ATTR_REDUCE, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;
			Entry entry = new Entry();
			entry.leagueType = (GameMatch.LeagueType)(int.Parse(line.SelectSingleNode("league_type").InnerText));
			entry.sectionID = uint.Parse(line.SelectSingleNode("section").InnerText);
			string[] tokens = line.SelectSingleNode("level_range").InnerText.Split(',');
			uint minLevel = uint.Parse(tokens[0]);
			uint maxLevel = minLevel;
			if (tokens.Length > 1)
				maxLevel = uint.Parse(tokens[1]);
			if (minLevel == 0 && maxLevel == 0)
				maxLevel = uint.MaxValue;
			entry.levelRange = new KeyValuePair<uint, uint>(minLevel, maxLevel);
			entry.position = (PositionType)(int.Parse(line.SelectSingleNode("position").InnerText));
			for (int i = 0; ; ++i)
			{
				XmlNode attr = line.SelectSingleNode("attr" + ((i == 0) ? "" : i.ToString()));
				if (attr == null || string.IsNullOrEmpty(attr.InnerText))
					break;
				tokens = attr.InnerText.Split(',');
				AttrReduceItem item = new AttrReduceItem();
				item.attr = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(uint.Parse(tokens[0]));
				item.requireValue = uint.Parse(tokens[1]);
				item.scaleFactor = IM.Number.Parse(tokens[2]);
				entry.items.Add(item.attr, item);
			}
			config.Add(entry);
		}
		Logger.ConfigEnd(name);
	}

	public Dictionary<string, AttrReduceItem> GetItems(GameMatch.LeagueType leagueType, uint sectionID, uint level, PositionType position)
	{
		return (from e in config
				where e.leagueType == leagueType &&
					e.sectionID == sectionID &&
					e.levelRange.Key <= level && level <= e.levelRange.Value &&
					e.position == position
				select e.items).FirstOrDefault();
	}
}
