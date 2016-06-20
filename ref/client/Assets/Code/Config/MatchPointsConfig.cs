using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public struct ThreePTCenter
{
    public IM.Transform transform;
}

public struct BeginPos
{
    //----------defense pos----
    public List<IM.Transform> defenses_transform;
    //--------- offense pos----
    public List<IM.Transform> offenses_transform;
}

public struct BlockStormPos
{
    public List<IM.Transform> npc_transforms;
}

public struct FreeThrowCenter
{
    public IM.Transform transform;
}

public struct GrabPointPos
{
    public IM.Transform npc_transform;
    public IM.Transform mainRole_transform;
}

public struct GrabZonePos
{
    // len = 5;
    public List<IM.Transform> balls_transform;
    public IM.Transform npc_transform;
    public IM.Transform mainRole_transform;
    public List<IM.Transform> zones_transform;
}

public struct MassBallPos
{
    public IM.Transform npc_transform;
    public IM.Transform mainRole_transform;
}

//目前没用
public struct NPCGuanzong01
{

}

public struct PractiseMovePos
{
    public IM.Transform npc1_transform;
    public IM.Transform npc2_transform;
    public IM.Transform mainRole1_transform;
    public IM.Transform mainRole2_transform;
    public IM.Transform ball_transform;
    public IM.Transform move1_transform;
    public IM.Transform move2_transform;
}

public struct ReboundStormPos
{
    public List<IM.Transform> shoots_transform;
    public IM.Transform mainRole_transform;
    public IM.Transform npc_transform;
}

public struct TipOffPos
{
    //----------defense pos----
    public List<IM.Transform> defenses_transform;
    //--------- offense pos----
    public List<IM.Transform> offenses_transform;
}

public struct TwoDefenderPos
{
    public IM.Transform attacker_transform;
    public IM.Transform defense0_transform;
    public IM.Transform defense1_transform;
}

//目前没用
public struct WayPoints
{
    public List<IM.Transform> transforms;
}


/**比赛位置信息配置*/
public class MatchPointsConfig {
    string name1    = GlobalConst.DIR_XML_MATCHPOINTS_3PTCENTER;
    string name2    = GlobalConst.DIR_XML_MATCHPOINTS_BEGINPOS;
    string name3    = GlobalConst.DIR_XML_MATCHPOINTS_BLOCKSTORM_POS;
    string name4    = GlobalConst.DIR_XML_MATCHPOINTS_FREETHROWCENTER;
    string name5    = GlobalConst.DIR_XML_MATCHPOINTS_GRABPOINT_POS;
    string name6    = GlobalConst.DIR_XML_MATCHPOINTS_GRABZONE_POS;
    string name7    = GlobalConst.DIR_XML_MATCHPOINTS_MASSBALL_POS;
    //string name8    = GlobalConst.DIR_XML_MATCHPOINTS_NPC_GUANZONG01;
    string name9    = GlobalConst.DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS;
    string name10   = GlobalConst.DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS;
    string name11   = GlobalConst.DIR_XML_MATCHPOINTS_TIPOFFPOS;
    string name12   = GlobalConst.DIR_XML_MATCHPOINTS_TWODEFENDER_POS;
    //string name13   = GlobalConst.DIR_XML_MATCHPOINTS_WAYPOINTS;


    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    //configs vars ----
    public ThreePTCenter ThreePTCenter;
    public BeginPos BeginPos;
    public BlockStormPos BlockStormPos;
    public FreeThrowCenter FreeThrowCenter;
    public GrabPointPos GrabPointPos;
    public GrabZonePos GrabZonePos;
    public MassBallPos MassBallPos;
    public PractiseMovePos PractiseMovePos;
    public ReboundStormPos ReboundStormPos;
    public TipOffPos TipOffPos;
    public TwoDefenderPos TwoDefenderPos;
    
    public MatchPointsConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name4, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name5, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name6, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name7, LoadFinish);
        //ResourceLoadManager.Instance.GetConfigResource(name8, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name9, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name10, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name11, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name12, LoadFinish);
        //ResourceLoadManager.Instance.GetConfigResource(name13, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 11)
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

        Read3PTCenterConfig();
        ReadBeginPosConfig();
        ReadBlockStromPosConfig();
        ReadFreeThrowCenterConfig();
        ReadGrabPointPosConfig();
        ReadGrabZonePosConfig();
        ReadMassBallConfig();
        ReadPractiseMovePosConfig();
        ReadRedboundStormConfig();
        ReadTipOffPosConfig();
        ReadTwoDefenderPosConfig();
        //ReadWayPointsConfig();
    }

    private void Read3PTCenterConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name1);
            return;
        }

        if (ThreePTCenter.transform == null)
            ThreePTCenter.transform = new IM.Transform();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name1, text);
        XmlNode root = xmlDoc.LastChild;
        ParseTransformInfo(root, ThreePTCenter.transform);
    }

    private void ReadBeginPosConfig()
    {

        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name2);
            return;
        }

        if (BeginPos.defenses_transform == null)
            BeginPos.defenses_transform = new List<IM.Transform>();
        if (BeginPos.offenses_transform == null)
            BeginPos.offenses_transform = new List<IM.Transform>();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name2, text);
        XmlNode root = xmlDoc.LastChild;
        XmlNode defenseNode = root.SelectSingleNode("DefenseNode");
        XmlNodeList defenseList = defenseNode.ChildNodes;
        int deCount = defenseList.Count;
        for(int i = 0; i< deCount; ++i)
        {
            XmlNode node = defenseList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            BeginPos.defenses_transform.Add(tempTrans);
        }

        XmlNode offenseNode = root.SelectSingleNode("OffenseNode");
        XmlNodeList offenseList = offenseNode.ChildNodes;
        int offCount = offenseList.Count;
        for(int i = 0; i < offCount; ++i)
        {
            XmlNode node = offenseList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            BeginPos.offenses_transform.Add(tempTrans);
        }
    }

    private void ReadBlockStromPosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name3);
            return;
        }

        if (BlockStormPos.npc_transforms == null)
            BlockStormPos.npc_transforms = new List<IM.Transform>();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name3, text);
        XmlNode root = xmlDoc.LastChild;
        XmlNodeList npcs = root.ChildNodes;
        int npcCount = npcs.Count;
        for (int i = 0; i < npcCount; ++i)
        {
            XmlNode node = npcs.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            BlockStormPos.npc_transforms.Add(tempTrans);
        }

    }

    private void ReadFreeThrowCenterConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name4);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name4);
            return;
        }

        if (FreeThrowCenter.transform == null)
            FreeThrowCenter.transform = new IM.Transform();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name4, text);
        XmlNode root = xmlDoc.LastChild;
        ParseTransformInfo(root, FreeThrowCenter.transform);
    }

    private void ReadGrabPointPosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name5);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name5);
            return;
        }

        if (GrabPointPos.npc_transform == null)
            GrabPointPos.npc_transform = new IM.Transform();
        if (GrabPointPos.mainRole_transform == null)
            GrabPointPos.mainRole_transform = new IM.Transform();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name5, text);
        XmlNode root = xmlDoc.LastChild;

        XmlNode npcNode = root.SelectSingleNode("NPC");
        XmlNode mainRoleNode = root.SelectSingleNode("MainRole");

        ParseTransformInfo(npcNode, GrabPointPos.npc_transform);
        ParseTransformInfo(mainRoleNode, GrabPointPos.mainRole_transform);
    }
    
    private void ReadGrabZonePosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name6);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name6);
            return;
        }

        if (GrabZonePos.balls_transform == null)
            GrabZonePos.balls_transform = new List<IM.Transform>();
        if (GrabZonePos.zones_transform == null)
            GrabZonePos.zones_transform = new List<IM.Transform>();
        if (GrabZonePos.mainRole_transform == null)
            GrabZonePos.mainRole_transform = new IM.Transform();
        if (GrabZonePos.npc_transform == null)
            GrabZonePos.npc_transform = new IM.Transform();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name6, text);
        XmlNode root = xmlDoc.LastChild;
        //--- BallNode-----
        XmlNode ballNode = root.SelectSingleNode("BallNode");
        XmlNodeList ballNodeList = ballNode.ChildNodes;
        int ballCount = ballNodeList.Count;
        for (int i = 0; i < ballCount; ++i)
        {
            XmlNode node = ballNodeList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            GrabZonePos.balls_transform.Add(tempTrans);
        }

        XmlNode zoneNode = root.SelectSingleNode("ZoneNode");
        XmlNodeList zoneList = zoneNode.ChildNodes;
        int offCount = zoneList.Count;
        for (int i = 0; i < offCount; ++i)
        {
            XmlNode node = zoneList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            GrabZonePos.zones_transform.Add(tempTrans);
        }

        XmlNode mainRoleNode = root.SelectSingleNode("MainRole");
        ParseTransformInfo(mainRoleNode, GrabZonePos.mainRole_transform);
     
        XmlNode npcNode = root.SelectSingleNode("NPC");
        ParseTransformInfo(npcNode, GrabZonePos.npc_transform);
    }

    private void ReadMassBallConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name7);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name7);
            return;
        }

        if (MassBallPos.mainRole_transform == null)
            MassBallPos.mainRole_transform = new IM.Transform();
        if (MassBallPos.npc_transform == null)
            MassBallPos.npc_transform = new IM.Transform();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name7, text);
        XmlNode root = xmlDoc.LastChild;
        XmlNode npcNode = root.SelectSingleNode("NPC");
        XmlNode mainRoleNode = root.SelectSingleNode("MainRole");

        ParseTransformInfo(npcNode, MassBallPos.npc_transform);
        ParseTransformInfo(mainRoleNode, MassBallPos.mainRole_transform);
    }

    private void ReadPractiseMovePosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name9);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name9);
            return;
        }

        PractiseMovePos.ball_transform = new IM.Transform();
        PractiseMovePos.npc1_transform = new IM.Transform();
        PractiseMovePos.npc2_transform = new IM.Transform();
        PractiseMovePos.mainRole1_transform = new IM.Transform();
        PractiseMovePos.mainRole2_transform = new IM.Transform();
        PractiseMovePos.move1_transform = new IM.Transform();
        PractiseMovePos.move2_transform = new IM.Transform();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name9, text);
        XmlNode root = xmlDoc.LastChild;

        XmlNode ballNode = root.SelectSingleNode("Ball");
        XmlNode npc1Node = root.SelectSingleNode("NPC1");
        XmlNode npc2Node = root.SelectSingleNode("NPC2");
        XmlNode mainRole1Node = root.SelectSingleNode("MainRole1");
        XmlNode mainRole2Node = root.SelectSingleNode("MainRole2");
        XmlNode move1Node = root.SelectSingleNode("move1");
        XmlNode move2Node = root.SelectSingleNode("move2");

        ParseTransformInfo(ballNode, PractiseMovePos.ball_transform);
        ParseTransformInfo(npc1Node, PractiseMovePos.npc1_transform);
        ParseTransformInfo(npc2Node, PractiseMovePos.npc2_transform);
        ParseTransformInfo(mainRole1Node, PractiseMovePos.mainRole1_transform);
        ParseTransformInfo(mainRole2Node, PractiseMovePos.mainRole2_transform);
        ParseTransformInfo(move1Node, PractiseMovePos.move1_transform);
        ParseTransformInfo(move2Node, PractiseMovePos.move2_transform);
    }

    private void ReadRedboundStormConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name10);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name10);
            return; 
        }

        ReboundStormPos.mainRole_transform = new IM.Transform();
        ReboundStormPos.npc_transform = new IM.Transform();
        ReboundStormPos.shoots_transform = new List<IM.Transform>();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name10, text);
        XmlNode root = xmlDoc.LastChild;

        XmlNode npcNode = root.SelectSingleNode("NPC");
        ParseTransformInfo(npcNode, ReboundStormPos.npc_transform);

        XmlNode mainRoleNode = root.SelectSingleNode("MainRole");
        ParseTransformInfo(mainRoleNode, ReboundStormPos.mainRole_transform);

        XmlNode shootNode = root.SelectSingleNode("ShootNode");
        XmlNodeList shoots = shootNode.ChildNodes;
        int shootCount = shoots.Count;
        for (int i = 0; i < shootCount; ++i)
        {
            XmlNode node = shoots.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            ParseTransformInfo(node, tempTrans);
            ReboundStormPos.shoots_transform.Add(tempTrans);
        }
    }

    private void ReadTipOffPosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name11);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name11);
            return;
        }

        if (TipOffPos.defenses_transform == null)
            TipOffPos.defenses_transform = new List<IM.Transform>();
        if (TipOffPos.offenses_transform == null)
            TipOffPos.offenses_transform = new List<IM.Transform>();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name11, text);
        XmlNode root = xmlDoc.LastChild;
        XmlNode defenseNode = root.SelectSingleNode("DefenseNode");
        XmlNodeList defenseList = defenseNode.ChildNodes;
        int deCount = defenseList.Count;
        for (int i = 0; i < deCount; ++i)
        {
            XmlNode node = defenseList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            XmlNode pointNode = node.SelectSingleNode("Point");
            XmlNode rotationNode = node.SelectSingleNode("Rotation");
            tempTrans.SetPosition(IM.Vector3.Parse(pointNode.InnerText));
            tempTrans.SetRotaion(IM.Quaternion.Parse(rotationNode.InnerText));
            TipOffPos.defenses_transform.Add(tempTrans);
        }

        XmlNode offenseNode = root.SelectSingleNode("OffenseNode");
        XmlNodeList offenseList = offenseNode.ChildNodes;
        int offCount = offenseList.Count;
        for (int i = 0; i < offCount; ++i)
        {
            XmlNode node = offenseList.Item(i);
            IM.Transform tempTrans = new IM.Transform();
            XmlNode pointNode = node.SelectSingleNode("Point");
            XmlNode rotationNode = node.SelectSingleNode("Rotation");
            tempTrans.SetPosition(IM.Vector3.Parse(pointNode.InnerText));
            tempTrans.SetRotaion(IM.Quaternion.Parse(rotationNode.InnerText));
            TipOffPos.offenses_transform.Add(tempTrans);
        }
    }

    private void ReadTwoDefenderPosConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name12);
        if (text == null)
        {
            Logger.LogError("LoadConfig Failed:" + name12);
            return;
        }

        TwoDefenderPos.attacker_transform = new IM.Transform();
        TwoDefenderPos.defense0_transform = new IM.Transform();
        TwoDefenderPos.defense1_transform = new IM.Transform();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name12, text);
        XmlNode root = xmlDoc.LastChild;

        XmlNode attackerNode = root.SelectSingleNode("Attacker");
        ParseTransformInfo(attackerNode, TwoDefenderPos.attacker_transform);

        XmlNode defender0Node = root.SelectSingleNode("Defender0");
        ParseTransformInfo(defender0Node, TwoDefenderPos.defense0_transform);

        XmlNode defender1Node = root.SelectSingleNode("Defender1");
        ParseTransformInfo(defender1Node, TwoDefenderPos.defense1_transform);
    }

    private void ReadWayPointsConfig()
    {

    }

    private void ParseTransformInfo(XmlNode node , IM.Transform iMtrans)
    {
        XmlNode pointNode = node.SelectSingleNode("Point");
        iMtrans.SetPosition(IM.Vector3.Parse(pointNode.InnerText));
        XmlNode rotationNode = node.SelectSingleNode("Rotation");
        iMtrans.SetRotaion(IM.Quaternion.Parse(rotationNode.InnerText));
    }
}
