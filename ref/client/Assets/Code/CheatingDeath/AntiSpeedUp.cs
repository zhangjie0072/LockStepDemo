using System;

public class AntiSpeedUp
{
	public	bool		m_toWatch;
	public  bool		m_beginWatch{get; private set;}
	public  double 		m_clientTime{get; private set;}
	public 	NetworkConn mNetworkConn{get; private set;}

	public AntiSpeedUp()
	{
		m_beginWatch = false;
		m_clientTime = 0.0f;
		m_toWatch = false;
	}

	public void SetWatchTarget(NetworkConn networkConn)
	{
		mNetworkConn = networkConn;
	}

	public bool BeginWatch(NetworkConn networkConn, double fServerTime)
	{
		if( !m_toWatch )
			return false;
		if( mNetworkConn == null || mNetworkConn != networkConn )
			return false;		
		m_beginWatch = true;
		m_clientTime = fServerTime;
		return true;
	}

	public void EndWatch()
	{
		if( mNetworkConn != null )
			mNetworkConn = null;
		m_toWatch = false;
		m_beginWatch = false;
	}

	public void Update(float fDeltaTime)
	{
		if( !m_beginWatch )
			return;
		m_clientTime += (double)fDeltaTime;
	}
}