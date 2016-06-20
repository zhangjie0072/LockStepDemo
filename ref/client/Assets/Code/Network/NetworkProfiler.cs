using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;

public class NetworkProfiler
{
	public 	double			m_avgLatency{get; private set;}


	private NetworkConn 	m_server;
    private GameUtils.Timer4View m_timer;
	private	uint			m_msgId = 0;
	private double			m_curTime;

	private uint			m_cnt = 0;
	private double			m_totalLatency;
	private bool			m_init = false;

	public  long			m_dataUsage{get; private set;}
	private bool			m_recDataUsage = false;

	public NetworkProfiler( NetworkConn server )
	{
		m_server = server;
		m_timer = new GameUtils.Timer4View(1f, _OnTimer);
		m_dataUsage = 0;
	}

	public void Init()
	{
		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		if( m_server == null || mgr == null )
			return;
		m_init = true;
		if( m_server.m_type == NetworkConn.Type.eGameServer )
			mgr.m_gameMsgHandler.RegisterHandler(MsgID.TimeTracerID, 	_OnTimeTracer);
		else if( m_server.m_type == NetworkConn.Type.eLoginServer )
			mgr.m_loginMsgHandler.RegisterHandler(MsgID.TimeTracerID, 	_OnTimeTracer);
		else if( m_server.m_type == NetworkConn.Type.ePlatformServer )
			mgr.m_platMsgHandler.RegisterHandler(MsgID.TimeTracerID, 	_OnTimeTracer);
	}

	void _OnTimeTracer(Pack pack)
	{
		TimeTracer timeTracer = Serializer.Deserialize<TimeTracer>(new MemoryStream(pack.buffer));
		//Logger.Log("cycle pack cost: " + (m_curTime - timeTracer.sendTimeStamp) );
		m_totalLatency += m_curTime - timeTracer.sendTimeStamp;
		m_cnt++;

		if( m_cnt > 5 )
		{
			m_avgLatency = m_totalLatency / 5.0;
			m_cnt = 0;
			m_totalLatency = 0.0;
		}
	}

	public void BeginRecordDataUsage()
	{
		m_dataUsage = 0;
		m_recDataUsage = true;
	}

	public void RecvDataCount(long byteCnt)
	{
		if( !m_recDataUsage )
			return;
		m_dataUsage += byteCnt;
	}

	public void EndRecordDataUsage()
	{
		m_dataUsage = 0;
		m_recDataUsage = false;
	}

	public void FixedUpdate(float fDeltaTime)
	{
		if( !m_init )
			return;

		m_curTime += fDeltaTime;
		if( m_timer != null )
			m_timer.Update(fDeltaTime);
	}

	void _OnTimer()
	{
		GameMsgSender.SendTimeTracer(m_msgId, m_curTime, m_server);
		m_msgId++;
		if( m_msgId > 10000 )
			m_msgId = 0;
	}
}