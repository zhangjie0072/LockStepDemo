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
	private GameUtils.Timer	m_timer;
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
		m_timer = new GameUtils.Timer(1f, _OnTimer);
		m_dataUsage = 0;
	}

	public void Init()
	{
		NetworkManager mgr = NetworkManager.Instance;
		if( m_server == null || mgr == null )
			return;
		m_init = true;
	}

	void _OnTimeTracer(Pack pack)
	{
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
		m_msgId++;
		if( m_msgId > 10000 )
			m_msgId = 0;
	}
}