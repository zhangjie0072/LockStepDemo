using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMode
{
	public uint ID;
	public GameMatch.Level level;
	public GameMatch.Type matchType;
	public uint time;
	public string scene;
	public Dictionary<PositionType, uint> mappedNPC;
	public List<uint>[] unmappedNPC = new List<uint>[3];
	public string additionalInfo;
	public IM.Number AIDelay;
	public IM.Number rushStamina;
	public IM.Number repositionDist;
	public Dictionary<uint, float> skillProbs;
	public string extraLevelInfo;

	public uint GetMappedNPC(PositionType pt)
	{
		if (mappedNPC.ContainsKey(pt))
			return mappedNPC[pt];
		return 0;
	}
}

public class GameModeConfig
{
    string name1 = GlobalConst.DIR_XML_GAME_MODE;
    string name2 = GlobalConst.DIR_XML_COMBO_BONUS;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	Dictionary<uint, GameMode> modes = new Dictionary<uint, GameMode>();
	Dictionary<GameMatch.Type, Dictionary<uint, float>> comboBonus = new Dictionary<GameMatch.Type, Dictionary<uint, float>>();

	public GameModeConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
		//LoadConfig();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
		//LoadComboBonus();
	}

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
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

		Logger.ConfigBegin(name1);
		LoadConfig();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		LoadComboBonus();
		Logger.ConfigEnd(name2);
    }

	public GameMode GetGameMode(uint ID)
	{
		GameMode gameMode = null;
		modes.TryGetValue(ID, out gameMode);
		return gameMode;
	}

	public float GetComboBonus(GameMatch.Type matchType, uint combo)
	{
		float bonus = 0f;
		Dictionary<uint, float> bonusConfig = null;
		if (comboBonus.TryGetValue(matchType, out bonusConfig))
		{
			foreach (KeyValuePair<uint, float> pair in bonusConfig)
			{
				if (pair.Key > combo)
					break;
				bonus = pair.Value;
			}
		}
		return bonus;
	}

    private void LoadConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        modes.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GAME_MODE, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			GameMode data = new GameMode();
			data.ID = uint.Parse(line.SelectSingleNode("id").InnerText);
			data.level = (GameMatch.Level)(int.Parse(line.SelectSingleNode("level").InnerText));
			data.matchType = (GameMatch.Type)(int.Parse(line.SelectSingleNode("type").InnerText));
			data.time = uint.Parse(line.SelectSingleNode("time").InnerText);
			data.scene = line.SelectSingleNode("scene").InnerText;
			data.additionalInfo = line.SelectSingleNode("additional_info").InnerText;
			#region Parse NPC config
			data.mappedNPC = new Dictionary<PositionType, uint>();
			uint id;
			if (uint.TryParse(line.SelectSingleNode("npc_C").InnerText, out id))
			{
				data.mappedNPC[PositionType.PT_C] = id;
			}
			if (uint.TryParse(line.SelectSingleNode("npc_PF").InnerText, out id))
			{
				data.mappedNPC[PositionType.PT_PF] = id;
			}
			if (uint.TryParse(line.SelectSingleNode("npc_PG").InnerText, out id))
			{
				data.mappedNPC[PositionType.PT_PG] = id;
			}
			if (uint.TryParse(line.SelectSingleNode("npc_SF").InnerText, out id))
			{
				data.mappedNPC[PositionType.PT_SF] = id;
			}
			if (uint.TryParse(line.SelectSingleNode("npc_SG").InnerText, out id))
			{
				data.mappedNPC[PositionType.PT_SG] = id;
			}
			for (int i = 1; i <= 3; ++i)
			{
				string npcs = line.SelectSingleNode("unmapped_npc" + i).InnerText;
				if (!string.IsNullOrEmpty(npcs))
				{
					List<uint> NPCs = new List<uint>();
					foreach (string token in npcs.Split('&'))
					{
						uint npcID;
						if (uint.TryParse(token, out npcID))
						{
							NPCs.Add(npcID);
						}
					}
					data.unmappedNPC[i - 1] = NPCs;
				}
			}
			#endregion
			data.AIDelay = IM.Number.Parse(line.SelectSingleNode("AI_delay").InnerText);
			data.rushStamina = IM.Number.Parse(line.SelectSingleNode("rush_stamina").InnerText);
			data.repositionDist = IM.Number.Parse(line.SelectSingleNode("reposition_dist").InnerText);
			string[] skillProbs = line.SelectSingleNode("skill_prob").InnerText.Split('&');
			foreach (string skillProb in skillProbs)
			{
				if (!string.IsNullOrEmpty(skillProb))
				{
					string[] tokens = skillProb.Split(':');
					data.skillProbs.Add(uint.Parse(tokens[0]), float.Parse(tokens[1]));
				}
			}
			data.extraLevelInfo = line.SelectSingleNode("extra_level_info").InnerText;

			modes.Add(data.ID, data);
		}
	}

    private void LoadComboBonus()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_COMBO_BONUS, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			GameMatch.Type matchType = (GameMatch.Type)(int.Parse(line.SelectSingleNode("type").InnerText));
			Dictionary<uint, float> bonusConfig = null;
			if (!comboBonus.TryGetValue(matchType, out bonusConfig))
			{
				bonusConfig = new Dictionary<uint, float>();
				comboBonus.Add(matchType, bonusConfig);
			}
			uint combo = uint.Parse(line.SelectSingleNode("combo").InnerText);
			float ratio = float.Parse(line.SelectSingleNode("bonus_ratio").InnerText);
			bonusConfig.Add(combo, ratio);
		}
	}
}
