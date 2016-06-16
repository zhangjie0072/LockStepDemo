using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AnimConfigItem
{
	public string 		name;
	public int			startFrame;
	public int			endFrame;
	public WrapMode		wrapMode;
	
	public class AnimEvent
	{
		public 	int		keyFrame;
		public	string	eventFunctionName;
		public	int		paramCnt;	
		public	float	floatParameter;
		public	string	stringParameter;
	};
	public List<AnimEvent>	animEvents;
	public AnimConfigItem()
	{
		animEvents = new List<AnimEvent>();
	}
	
	static public WrapMode 	Parse( string strWrapMode )
	{
		switch(strWrapMode)
		{
		case "loop":
			return WrapMode.Loop;
		case "once":
			return WrapMode.Once;
		case "pingpong":
			return WrapMode.PingPong;
		case "clamp":
			return WrapMode.Clamp;
		}
		return WrapMode.Default;
	}

	static public Dictionary<string, string> ParseParam( string param )
	{
		Dictionary<string, string> paramDict = new Dictionary<string, string>();
		Regex reg = new Regex(@"(\w+):(\[*(\-*\w+\.*\d*\;*)*\]*)");
		
		MatchCollection collection = reg.Matches(param);
		//Logger.Log( collection.Count );
		int count = collection.Count;
		for( int idx = 0; idx != count ; idx++)
		{
			string[] keyValue = collection[idx].Value.Split(':');
			string key 		= keyValue[0];
			string value 	= keyValue[1];
			paramDict[key] = value;
		}
		return paramDict;
	}

	static public IM.Vector3 ParseVector3( string param )
	{
		string strValue = param.Substring(1, param.Length - 2);
		string[] keyValue = strValue.Split(';');
		return new IM.Vector3( IM.Number.Parse(keyValue[0]), IM.Number.Parse(keyValue[1]), IM.Number.Parse(keyValue[2]) );
	}
};

public class PlayerActConfig
	: Singleton<PlayerActConfig>
{
	public PlayerActConfig()
	{
	}

	public bool ReadConfig( string strConfigPath, out List<AnimConfigItem> configs )
	{
		configs = new List<AnimConfigItem>();
		try
		{
			using( FileStream fs = new FileStream(strConfigPath,FileMode.Open) )
			{
				TextReader reader = new StreamReader(fs);
				while(true)
				{
					string strConfigContent = reader.ReadLine();
					if( strConfigContent == null )
						break;
					int comments = strConfigContent.IndexOf("//");
					if( comments != -1 )
						strConfigContent = strConfigContent.Substring(0, comments);
					strConfigContent = strConfigContent.Trim();
					if( strConfigContent.Length == 0 )
						continue;
					
					string[] strConfigItems = strConfigContent.Split(',');
					if( strConfigItems.Length < 4 )
					{
						Logger.LogError( "Invalid animConfig format: " + strConfigContent );
						break;
					}
					
					foreach( string strConfigItem in strConfigItems )
						strConfigItem.Trim();
					
					AnimConfigItem animConfigItem = new AnimConfigItem();
					animConfigItem.name 		= strConfigItems[0];
					animConfigItem.name			= animConfigItem.name.Trim();
					animConfigItem.startFrame 	= int.Parse(strConfigItems[1]);
					animConfigItem.endFrame 	= int.Parse(strConfigItems[2]);
					animConfigItem.wrapMode		= AnimConfigItem.Parse(strConfigItems[3]);
					
					for( int idx = 4; idx < strConfigItems.GetLength(0); idx+=2 )
					{
						AnimConfigItem.AnimEvent key = new AnimConfigItem.AnimEvent();
						_ParseFunction(key, strConfigItems[idx], strConfigItems[idx+1].Trim());
						animConfigItem.animEvents.Add(key);
					}
					
					configs.Add(animConfigItem);
				}
			}
			return true;
		}
		catch(IOException exp)
		{
			Logger.LogError( "读取动作文件配置失败: " + exp.Message );
		}
		return false;
	}

	void _ParseFunction(AnimConfigItem.AnimEvent key, string keyFrame, string funcItem )
	{
		key.keyFrame = int.Parse( keyFrame );

		string funcName = funcItem;
		string param = "";
		int funcIdx = funcItem.IndexOf("(");
		if( funcIdx != -1 )
		{
			funcName = funcItem.Substring(0, funcIdx);
			int iParam = funcItem.IndexOf(")");
			if( iParam != -1 )
			{
				param = funcItem.Substring(funcIdx + 1, funcItem.Length - funcIdx - 2); 
				key.stringParameter = param;
				key.paramCnt++;
			}
		}
		key.eventFunctionName = funcName;		
	}

}