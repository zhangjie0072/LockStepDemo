using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class DebugConfig
{
    string name = GlobalConst.DIR_XML_DEBUG;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public DebugConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public void ReadConfig()
	{
		try
		{
            if (isLoadFinish == false)
                return;
            isLoadFinish = false;
            lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

			Logger.ConfigBegin(name);
            string text = ResourceLoadManager.Instance.GetConfigText(name);
			//¶ÁÈ¡ÒÔ¼°´¦ÀíXMLÎÄ±¾µÄÀà
			XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_DEBUG, text);
			//½âÎöxmlµÄ¹ý³Ì	
			XmlNode root = xmlDoc.ChildNodes[0];
			if( root == null )
				return;
			foreach( XmlNode node in root.ChildNodes )
			{
				if( node.NodeType != XmlNodeType.Element ) 
					continue;
				XmlElement element = node as XmlElement;
				if( node.Name == "EnableAI" )
				{
					Debugger.Instance.m_bEnableAI = bool.Parse(element.InnerText);
				}
				else if( node.Name == "EnableDebugMsg" )
				{
					Debugger.Instance.m_bEnableDebugMsg = bool.Parse(element.InnerText);
				}
				else if( node.Name == "EnableDefenders" )
				{
					Debugger.Instance.m_bEnableDefenderAction = bool.Parse(element.InnerText);
				}
				else if( node.Name == "EnableTiming" )
				{
					Debugger.Instance.m_bEnableTiming = bool.Parse(element.InnerText);
				}
			}
			Logger.ConfigEnd(name);
		}
		catch( XmlException exp )
		{
			Logger.Log("load debug config failed: " + exp.Message );
		}
	}
}
