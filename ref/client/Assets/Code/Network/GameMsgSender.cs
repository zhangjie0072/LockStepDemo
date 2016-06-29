using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;

public class GameMsgSender
{
	public GameMsgSender()
	{
	}

	static bool _FilterOutMsgByMatchState()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null || match.m_stateMachine.m_curState == null )
			return true;
		MatchState.State curState = match.m_stateMachine.m_curState.m_eState;
		if( curState == MatchState.State.eOpening || curState == MatchState.State.eOverTime || curState == MatchState.State.eOver )
			return true;
		return false;
	}

	public static void SendGameShortMsg(Player player, uint id, uint broadcastType)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		InstructionReq instReq = new InstructionReq();
		instReq.id = id;
		instReq.obj = broadcastType;
		instReq.char_id = player.m_roomPosId;

		NetworkConn server = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( server != null )
			server.SendPack<InstructionReq>(0, instReq, MsgID.InstructionReqID);
	}

	public static void SendGameBegin()
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null)
			return;

		GameBeginReq gameBeginReq = new GameBeginReq();
		gameBeginReq.acc_id = MainPlayer.Instance.AccountID;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<GameBeginReq>(0, gameBeginReq, MsgID.GameBeginReqID);
	}

	public static void SendTimeTracer(uint msgId, double dCurTime, NetworkConn server)
	{
		if( server == null || !server.IsConnected() )
			return;

		TimeTracer tracer = new TimeTracer();
		tracer.sendTimeStamp = dCurTime;
		tracer.id = msgId;
		server.SendPack<TimeTracer>(0, tracer, MsgID.TimeTracerID);
	}

    public static void SendPVPLoadProgress(uint perc)
    {
        if (GameSystem.Instance.mNetworkManager.m_gameConn == null)
            return;
        PVPLoadProgress obj = new PVPLoadProgress();
        obj.progress = perc;
        GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PVPLoadProgress>(0, obj, MsgID.PVPLoadProgressID);
    }

    public static void SendLoadingComplete(GameMatch.Type type)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		PVPLoadComplete obj = new PVPLoadComplete();
		obj.type = MatchType.MT_PVP_1V1_PLUS;
		if( type == GameMatch.Type.ePVP_3On3 )
			obj.type = MatchType.MT_PVP_3V3;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PVPLoadComplete>(0, obj, MsgID.PVPLoadCompleteID);
	}

    public static void SendInput(InputDirection dir, Command cmd)
    {
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
        ClientInput input = new ClientInput();
        input.acc_id = MainPlayer.Instance.AccountID;
        input.dir = (uint)dir;
        input.cmd = (uint)cmd;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<ClientInput>(0, input, MsgID.ClientInputID);
        //Debug.Log("SendInput " + dir + " " + cmd);
    }

    //丢帧请求重发
    public static void SendMissTurnReq(List<uint> turnIDs)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
        MissFrameReq req = new MissFrameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.frame_num.AddRange(turnIDs);
		conn.SendPack(0, req, MsgID.MissFrameReqID);
        Debug.Log("Request missed frame, " + turnIDs[0] + " - " + turnIDs[turnIDs.Count - 1]);
    }
    //丢帧请求重发
    public static void SendMissTurnReq(uint turnID)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
        MissFrameReq req = new MissFrameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.frame_num.Add(turnID);
		conn.SendPack(0, req, MsgID.MissFrameReqID);
        Debug.Log("Request missed frame, " + turnID);
    }

    //For virtual game server
    public static void SendTurn(PlayFrame turn)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		conn.SendPack<PlayFrame>(0, turn, MsgID.PlayFrameID);
        //Debug.Log("VirtualGameServer, SendTurn " + turn.frameNum);

        //如果使用的是虚拟连接，保证消息立即传回客户端
        if (conn.m_type == NetworkConn.Type.eVirtualServer)
            conn.Update(0f);
    }

    //For virtual game server
    public static void SendGameBeginResp(GameBeginResp resp)
    {
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<GameBeginResp>(0, resp, MsgID.GameBeginRespID);
    }

    public static void SendTurnCheckData(CheckFrame checkData)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		conn.SendPack(0, checkData, MsgID.CheckFrameID);
    }

    public static void SendTeamMatchData(TeamMatchData teamData)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		conn.SendPack(0, teamData, MsgID.TeamMatchDataID);
    }
}