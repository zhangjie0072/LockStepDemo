using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum MatchMsgRecvType
{
	self,
	oppo,
	all,
}

public class MatchMsg
{
	public uint				id;
	public List<uint>		conds = new List<uint>();
	public uint				menu_type;
	public uint				cd;
	public string			desc;
	public string			audio_src;
	public string			pop_text;
	public MatchMsgRecvType recvType;
}

public class MatchMsgConfig
{
	public List<MatchMsg> matchMsgs{get; set;}
	
	string name1 = GlobalConst.DIR_XML_MATCH_MSG;
	uint count = 0;
	bool isLoadFinish = false;
	private object LockObject = new object();

	public MatchMsgConfig()
	{
		matchMsgs = new List<MatchMsg>();
		ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
	}

	void LoadFinish(string vPath, object obj)
	{
		++count;
		if (count == 1)
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
		string text = ResourceLoadManager.Instance.GetConfigText(name1);
		if (text == null)
		{
			ErrorDisplay.Instance.HandleLog("LoadConfig failed: " + name1, "", LogType.Error);
			return;
		}

		try
		{
			XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MATCH_MSG, text);
			XmlNode root = doc.SelectSingleNode("Data");
			foreach (XmlNode line in root.SelectNodes("Line"))
			{
				if (CommonFunction.IsCommented(line))
					continue;

				MatchMsg matchMsg = new MatchMsg();
				matchMsg.id = uint.Parse(line.SelectSingleNode("ID").InnerText);
				string strCondition = line.SelectSingleNode("Conditions").InnerText;
				string[] conds = strCondition.Split('&');
				uint uCond = 0;
				foreach( string cond in conds )
				{
					uCond = 0;
					if( !uint.TryParse( cond, out uCond ) )
						continue;
					matchMsg.conds.Add(uCond);
				}
				matchMsg.menu_type = uint.Parse(line.SelectSingleNode("MenuType").InnerText);
				matchMsg.cd = uint.Parse(line.SelectSingleNode("CD").InnerText);
				matchMsg.desc = line.SelectSingleNode("Desc").InnerText;
				matchMsg.audio_src = line.SelectSingleNode("AudioSrc").InnerText;
				matchMsg.recvType = (MatchMsgRecvType)int.Parse(line.SelectSingleNode("RecvType").InnerText);
				matchMsg.pop_text = line.SelectSingleNode("PopText").InnerText;

				matchMsgs.Add(matchMsg);
			}
		}
		catch( XmlException cep )
		{
			Debug.LogError("Parse matchmsgconfig failed: " + cep);
		}
		
	}
}