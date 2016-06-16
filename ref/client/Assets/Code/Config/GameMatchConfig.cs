using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;


public class GroundData 
{
    public IM.Vector2 mHalfSize;
    public IM.Number m3PointRadius;
    public IM.Number m3PointBaseLine;
    public IM.Vector2 m3PointCenter;
    public IM.Number m2PointRadius;
    public IM.Number mFreeThrowLane;
    public IM.Vector3 mCenter;
    public IM.Vector3 mBasketPos;
}

public class PlayerAttributes 
{
    public IM.Number initWithOutRunSpeed;
    public IM.Number initWithRunSpeed;
    public IM.Number initWithOutRushSpeed;
    public IM.Number initWithRushSpeed;
    public IM.Number initDefenseSpeed;
    public IM.Number initBackToBackSpeed;
    public IM.Number turningSpeed;
    public IM.Number speedPassBall;
    public Dictionary<string, Dictionary<string, PlayerAnimAttribute.AnimAttr>> animItemAttrs 
        = new Dictionary<string, Dictionary<string, PlayerAnimAttribute.AnimAttr>>();
}

public class GameMatchData 
{
    public GameMatch.Type type;
    public uint sceneId;
    public IM.Number matchTime;
    public IM.Number oppoColorMulti;
    public List<GameMatch.Config.TeamMember> npcList = new List<GameMatch.Config.TeamMember>();
    public GameMatch.Config.TeamMember mainRole = null;
    public List<GameMatch.Config.TeamMember> remoteList = new List<GameMatch.Config.TeamMember>();
}

public class GameMatchConfig
{
    private string name1 = GlobalConst.DIR_XML_PLAYGROUND;
    private string name2 = GlobalConst.DIR_XML_ANIMINFO;
    private string name3 = GlobalConst.DIR_XML_MATCH_COMMON;
    private string name4 = GlobalConst.DIR_XML_MATCH_GUIDE;
    private string name5 = GlobalConst.DIR_XML_MATCH_SINGLE;
    //private string name6 = GlobalConst.DIR_XML_MATCH_PVP;
    private string name6 = GlobalConst.DIR_XML_MATCH_READY;
    private string name7 = GlobalConst.DIR_XML_MATCH_FREE_PRACTICE;
    private string name8 = GlobalConst.DIR_XML_MATCH_MULTIPLY;
    private uint count = 0;
    private bool isLoadFinish = false;

    public static GroundData groundData = new GroundData();
    public static PlayerAttributes playerAttributes = new PlayerAttributes();
    public static GameMatchData gameMatchData = null;
    public static Dictionary<GameMatch.Type, GameMatchData> gameMatchDatas = new Dictionary<GameMatch.Type, GameMatchData>();
	public GameMatchConfig()
	{
        Initialize();
	}

	/*
	public void LoadCameraConfig(string strConfigPath, UCamCtrl_MatchNew camera)
	{
		try
		{
            //读取以及处理XML文本的类
            XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(strConfigPath);
            //解析xml的过程			
			XmlNode root = xmlDoc.ChildNodes[0];
			if( root == null )
				return;
			
			foreach( XmlNode node in root.ChildNodes )
			{
				if( node.NodeType != XmlNodeType.Element ) 
					continue;
				XmlElement element = node as XmlElement;
				if( node.Name == "HalfSizeBound" )
					camera.m_HalfSizeBound = XmlUtils.XmlGetAttr_Vec3(element);
				else if( node.Name == "RadiusThreshold" )
				{
					camera.m_RadiusThresholdMaxV = XmlUtils.XmlGetAttr_Float(element, "max");
					camera.m_RadiusThresholdMinV = XmlUtils.XmlGetAttr_Float(element, "min");
				}
				else if( node.Name == "LookAngleVertical" )
				{
					camera.m_MaxLookAngleV = XmlUtils.XmlGetAttr_Float(element, "max");
					camera.m_MinLookAngleV = XmlUtils.XmlGetAttr_Float(element, "min");
				}
				else if( node.Name == "DistanceToTarget" )
				{
					camera.m_MaxDistToTarget = XmlUtils.XmlGetAttr_Float(element, "max");
					camera.m_MinDistToTarget = XmlUtils.XmlGetAttr_Float(element, "min");
				}
				else if( node.Name == "AngleToTarget" )
				{
					camera.m_MaxAngleToTarget = XmlUtils.XmlGetAttr_Float(element, "max");
					camera.m_MinAngleToTarget = XmlUtils.XmlGetAttr_Float(element, "min");
				}
				else if( node.Name == "MoveSpeedHorizonal" )
					camera.m_MoveSpeedH = XmlUtils.XmlGetAttr_Float(element, "speed");
			}
		}
		catch( XmlException exp )
		{
			Logger.Log("load camera config failed: " + exp.Message );
		}
	}
	*/

    public void Initialize() 
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name4, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name5, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name6, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name7, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name8, LoadFinish);
        //ResourceLoadManager.Instance.GetConfigResource(name9, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 8)
        {
            isLoadFinish = true;
            GameSystem.Instance.loadConfigCnt += 1;
        }
    }

    public void ReadConfig() 
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        GameSystem.Instance.readConfigCnt += 1;

		Logger.ConfigBegin(name1);
		ReadGroundConfig();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadPlayerAttributesConfig();
		Logger.ConfigEnd(name2);
        ReadMatchConfig();
    }

    public void ReadGroundConfig() 
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        try
        {
            //读取以及处理XML文本的类
            XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PLAYGROUND, text);
            //解析xml的过程
            XmlNode root = xmlDoc.ChildNodes[0];
            if (root == null)
                return;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;
                XmlElement element = node as XmlElement;
                if (node.Name == "HalfSize")
                    groundData.mHalfSize = XmlUtils.XmlGetAttr_Vec2(element);
                else if (node.Name == "Pt3Radius")
                {
                    groundData.m3PointRadius = XmlUtils.XmlGetAttr_Number(element, "x");
                }
                else if (node.Name == "Pt3BaseLine")
                {
                    groundData.m3PointBaseLine = XmlUtils.XmlGetAttr_Number(element, "x");
                }
                else if (node.Name == "Pt3Center")
                {
                    groundData.m3PointCenter = XmlUtils.XmlGetAttr_Vec2(element);
                }

                else if (node.Name == "Pt2PointRadius")
                {
                    groundData.m2PointRadius = XmlUtils.XmlGetAttr_Number(element, "x");
                }

                else if (node.Name == "FreeThrowLane")
                {
                    groundData.mFreeThrowLane = XmlUtils.XmlGetAttr_Number(element, "x");
                }

                else if (node.Name == "Center")
                {
                    groundData.mCenter = XmlUtils.XmlGetAttr_Vec3(element);
                }
                else if (node.Name == "Basket")
                {
                    groundData.mBasketPos = XmlUtils.XmlGetAttr_Vec3(element);
                }
            }
        }
        catch (XmlException exp)
        {
            Logger.Log("load camera config failed: " + exp.Message);
        }
    }

    public void ReadPlayerAttributesConfig() 
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }

        try
        {
            //读取以及处理XML文本的类
            XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ANIMINFO, text);
            //解析xml的过程
            XmlNode root = xmlDoc.ChildNodes[0];
            if (root == null)
                return;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;
                XmlElement element = node as XmlElement;
                if (node.Name == "RunSpeed")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "WithoutBall")
                            playerAttributes.initWithOutRunSpeed = IM.Number.Parse(child.InnerText);
                        if (child.Name == "WithBall")
                            playerAttributes.initWithRunSpeed = IM.Number.Parse(child.InnerText);
                    }
                }
                else if (node.Name == "RushSpeed")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "WithoutBall")
                            playerAttributes.initWithOutRushSpeed = IM.Number.Parse(child.InnerText);
                        if (child.Name == "WithBall")
                            playerAttributes.initWithRushSpeed = IM.Number.Parse(child.InnerText);
                    }
                }
                else if (node.Name == "DefenseSpeed")
                {
                    playerAttributes.initDefenseSpeed = IM.Number.Parse(node.InnerText);
                }
                else if (node.Name == "BackToBackRunSpeed")
                {
                    playerAttributes.initBackToBackSpeed = IM.Number.Parse(node.InnerText);
                }
                else if (node.Name == "TurningSpeed")
                {
                    playerAttributes.turningSpeed = XmlUtils.XmlGetAttr_Number(element, "x");
                }
                else if (node.Name == "PassBallSpeed")
                {
                    playerAttributes.speedPassBall = XmlUtils.XmlGetAttr_Number(element, "x");
                }
                else if (node.Name == "AnimAttribute")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        ReadAnimAttribute(child);
                    }

                }
            }
        }
        catch (XmlException exp)
        {
            Logger.LogError("load player config failed: " + exp.Message);
        }
    }

    void ReadAnimAttribute(XmlNode node) 
    {
        Dictionary<string, PlayerAnimAttribute.AnimAttr> attrs = new Dictionary<string, PlayerAnimAttribute.AnimAttr>();
        foreach (XmlNode animItem in node.ChildNodes)
        {
            PlayerAnimAttribute.AnimAttr animAttr = new PlayerAnimAttribute.AnimAttr();
            XmlElement animItemElem = animItem as XmlElement;
            animAttr.strAnim = animItemElem.GetAttribute("name");
            XmlNodeList lstNodes = animItemElem.SelectNodes("./KeyEvent");
            animAttr.keyFrame = new List<PlayerAnimAttribute.KeyFrame>();

            PlayerAnimAttribute.KeyFrame kf = new PlayerAnimAttribute.KeyFrame("null", 0);
            foreach (XmlNode keyEvent in lstNodes)
            {
                XmlElement keyEventElem = keyEvent as XmlElement;
                string id = keyEventElem.GetAttribute("id");
                int frame = int.Parse(keyEventElem.GetAttribute("x"));

                if (id == "MoveToStartPos")
                {
                    IM.Vector3 param =IM.Vector3.zero;
                    XmlElement elem = keyEventElem.SelectSingleNode("./Param") as XmlElement;
                    if (elem != null)
                        param = XmlUtils.XmlGetAttr_Vec3(elem);
                    kf = new PlayerAnimAttribute.KeyFrame_MoveToStartPos(id, frame, param);
                }
                else if (id == "OnLayupShot")
                {
                    IM.Vector3 param = IM.Vector3.zero;
                    XmlElement elem = keyEventElem.SelectSingleNode("./Param") as XmlElement;
                    if (elem != null)
                        param = XmlUtils.XmlGetAttr_Vec3(elem);
                    kf = new PlayerAnimAttribute.KeyFrame_LayupShootPos(id, frame, param);
                }
                else if (id == "blockable")
                {
                    int blockframe = 0;
                    XmlElement elem = keyEventElem.SelectSingleNode("./Param") as XmlElement;
                    if (elem != null)
                        blockframe = XmlUtils.XmlGetAttr_Int(elem, "x");
                    kf = new PlayerAnimAttribute.KeyFrame_Blockable(id, frame, blockframe);
                }
                else if (id == "DefenderSpasticity")
                {
                    int length = 0;
                    XmlElement elem = keyEventElem.SelectSingleNode("./Param") as XmlElement;
                    if (elem != null)
                        length = XmlUtils.XmlGetAttr_Int(elem, "x");
                    kf = new PlayerAnimAttribute.KeyFrame_DefenderSpasticity(id, frame, length);
                }
                else if (id == "RotateToBasketAngle")
                {
                    IM.Number angle = IM.Number.zero;
                    XmlElement elem = keyEventElem.SelectSingleNode("./Param") as XmlElement;
                    if (elem != null)
                        angle = XmlUtils.XmlGetAttr_Number(elem, "x");
                    kf = new PlayerAnimAttribute.KeyFrame_RotateToBasketAngle(id, frame, angle);
                }
                else
                    kf = new PlayerAnimAttribute.KeyFrame(id, frame);

                animAttr.keyFrame.Add(kf);
            }
            if (attrs.ContainsKey(animAttr.strAnim))
                Logger.LogError("AnimInfo same key: " + animAttr.strAnim);
            attrs.Add(animAttr.strAnim, animAttr);
        }
        playerAttributes.animItemAttrs.Add(node.Name, attrs);
    }

    public void ReadMatchConfig() 
    {
        for (int i = 3; i < 9; i++)
        {
            string name = "";
            switch(i)
            {
                case 3:
                    name = name3;
                    break;
                case 4:
                    name = name4;
                    break;
                case 5:
                    name = name5;
                    break;
                case 6:
                    name = name6;
                    break;
                case 7:
                    name = name7;
                    break;
                case 8:
                    name = name8;
                    break;
                //case 9:
                //    name = name9;
                //    break;
			}
			Logger.ConfigBegin(name);
            string text = ResourceLoadManager.Instance.GetConfigText(name);
            if (text == null)
            {
                Logger.LogError("LoadConfig failed: " + name);
                return;
            }
            try
            {
                //读取以及处理XML文本的类
                XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name, text);
                //解析xml的过程
                XmlNode root = xmlDoc.ChildNodes[0];
                if (root == null)
                    return;

                GameMatchData matchData = new GameMatchData();
                XmlElement rootElm = root as XmlElement;
                if (name != GlobalConst.DIR_XML_MATCH_COMMON)
                {
                    int type = XmlUtils.XmlGetAttr_Int(rootElm, "type", 1);
                    matchData.type = (GameMatch.Type)type;
                }
                else
                {
                    ReadMatchCommon(root, matchData);
                    continue;
                }

                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (node.Name == "Scene")
                        matchData.sceneId = uint.Parse(node.InnerText);
                    if (node.Name == "Team")
                    {
                        foreach (XmlNode teamNode in node.ChildNodes)
                        {
                            if (teamNode.Name == "Home")
                                ReadTeamMember(teamNode, Team.Side.eHome, matchData);
                            else if (teamNode.Name == "Away")
                                ReadTeamMember(teamNode, Team.Side.eAway, matchData);
                        }
                    }

                    if (node.Name == "Time")
                        matchData.matchTime = XmlUtils.XmlGetAttr_Number(element, "x");

                    if (node.Name == "OppoColorMulti")
                        matchData.oppoColorMulti = XmlUtils.XmlGetAttr_Number(element, "x");
                }

                gameMatchDatas.Add(matchData.type, matchData);
            }
            catch (XmlException exp)
            {
                Logger.Log("load match config failed: " + exp.Message);
			}
			Logger.ConfigEnd(name);
        }
    }
	
	void ReadTeamMember(XmlNode node, Team.Side team, GameMatchData matchData)
	{
		foreach( XmlNode child in node.ChildNodes )
		{
			XmlElement element = child as XmlElement;
			GameMatch.Config.TeamMember tm = new GameMatch.Config.TeamMember();
			tm.pos 	= XmlUtils.XmlGetAttr_Int(element, "pos");
			tm.id 	= XmlUtils.XmlGetAttr_String(element, "id");
			tm.team = team;
			
			if( element.Name == "NPC" )
				matchData.npcList.Add(tm);
			else if( child.Name == "MainRole" )
				matchData.mainRole = tm;
			else if( child.Name == "RemotePlayer" )
				matchData.remoteList.Add(tm);
			else
			{
				Logger.Log( ToString() + ": Invalid config." );
				return;
			}
		}
	}

    void ReadMatchCommon(XmlNode root, GameMatchData matchData) 
    {
        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.NodeType != XmlNodeType.Element)
                continue;
            XmlElement element = node as XmlElement;
            if (node.Name == "Scene")
                matchData.sceneId = uint.Parse(node.InnerText);
            if (node.Name == "Team")
            {
                foreach (XmlNode teamNode in node.ChildNodes)
                {
                    if (teamNode.Name == "Home")
                        ReadTeamMember(teamNode, Team.Side.eHome, matchData);
                    else if (teamNode.Name == "Away")
                        ReadTeamMember(teamNode, Team.Side.eAway, matchData);
                }
            }

            if (node.Name == "Time")
                matchData.matchTime = XmlUtils.XmlGetAttr_Number(element, "x");

            if (node.Name == "OppoColorMulti")
                matchData.oppoColorMulti = XmlUtils.XmlGetAttr_Number(element, "x");
        }
        gameMatchData = matchData;
    }

	public void LoadPlayGround(ref PlayGround playground)
	{
		try
        {
            playground.mHalfSize = groundData.mHalfSize;
            playground.mBasketPos = groundData.mBasketPos;
            playground.m3PointRadius = groundData.m3PointRadius;
            playground.m3PointBaseLine = groundData.m3PointBaseLine;
            playground.m3PointCenter = groundData.m3PointCenter;
            playground.m2PointRadius = groundData.m2PointRadius;
            playground.mFreeThrowLane = groundData.mFreeThrowLane;
            playground.mCenter = groundData.mCenter;
		}
		catch( Exception exp )
		{
			Logger.Log("load playground failed: " + exp.Message);
		}
	}
	
	public void LoadPlayerAttributes(Player player)
	{
		try
        {	
            player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_initSpeed = playerAttributes.initWithOutRunSpeed;
            player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_initSpeed = playerAttributes.initWithRunSpeed;
            player.mMovements[(int)PlayerMovement.Type.eRushWithoutBall].mAttr.m_initSpeed = playerAttributes.initWithOutRushSpeed;
            player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_initSpeed = playerAttributes.initWithRushSpeed;
            player.mMovements[(int)PlayerMovement.Type.eDefense].mAttr.m_initSpeed = playerAttributes.initDefenseSpeed;
            player.mMovements[(int)PlayerMovement.Type.eBackToBackRun].mAttr.m_initSpeed = playerAttributes.initBackToBackSpeed;
			
            foreach( PlayerMovement movement in player.mMovements )
				movement.mAttr.m_TurningSpeed = playerAttributes.turningSpeed;

			player.m_speedPassBall = playerAttributes.speedPassBall;

            foreach (KeyValuePair<string, Dictionary<string, PlayerAnimAttribute.AnimAttr>> animItem in playerAttributes.animItemAttrs)
            {
                if (animItem.Key == "Dunk_Near")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
	                {
		                if(player.m_animAttributes.m_dunkNear.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_dunkNear.Add(attr.Key, attr.Value);
	                }
                else if (animItem.Key == "Dunk_Far")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
	                {
                        if (player.m_animAttributes.m_dunkFar.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_dunkFar.Add(attr.Key, attr.Value);
	                }
                else if (animItem.Key == "Layup_Near")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_layupNear.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_layupNear.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Layup_Far")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_layupFar.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_layupFar.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Rebound")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_rebound.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_rebound.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Catch")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_catch.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_catch.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Block")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_block.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_block.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Shoot")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_shoot.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_shoot.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "CrossOver")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_crossOver.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_crossOver.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "BackToBack")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_backToBack.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_backToBack.Add(attr.Key, attr.Value);
                    }
                else if (animItem.Key == "Interception")
                    foreach (KeyValuePair<string, PlayerAnimAttribute.AnimAttr> attr in animItem.Value)
                    {
                        if (player.m_animAttributes.m_interception.ContainsKey(attr.Key))
                            Logger.LogError("AnimInfo same key: " + attr.Key);
                        player.m_animAttributes.m_interception.Add(attr.Key, attr.Value);
                    }
            }

		}
		catch( Exception exp )
		{
			Logger.LogError("load player attributes failed: " + exp.Message );
		}
	}

    public void LoadMatchConfig(ref GameMatch.Config config, GameMatch.Type matchType = GameMatch.Type.eNone)
	{
		try
        {
            if (matchType == GameMatch.Type.eNone)
            {
                config.sceneId = gameMatchData.sceneId;
                config.NPCs = gameMatchData.npcList;
                config.MainRole = gameMatchData.mainRole;
                config.RemotePlayers = gameMatchData.remoteList;
                config.MatchTime = gameMatchData.matchTime;
                config.OppoColorMulti = gameMatchData.oppoColorMulti;
            }
            else 
            {
                GameMatchData data = null;
                gameMatchDatas.TryGetValue(matchType, out data);
                if (data != null)
                {
                    config.type = data.type;
                    config.sceneId = data.sceneId;
                    config.NPCs = data.npcList;
                    config.MainRole = data.mainRole;
                    config.RemotePlayers = data.remoteList;
                    config.MatchTime = data.matchTime;
                    config.OppoColorMulti = data.oppoColorMulti;
                }
                else 
                {
                    Logger.LogError("Match Config is null!");
                }
            }
		}
		catch( Exception exp )
		{
			Logger.Log("load match config failed: " + exp.Message );
		}
	}

	
}