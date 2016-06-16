using System;

public class AntiSpeedUp
{
	public  bool	m_beginWatch{get; private set;}
	public  double 	m_clientTime{get; private set;}

	public AntiSpeedUp()
	{
		m_beginWatch = false;
		m_clientTime = 0.0f;
	}

	public void BeginWatch(double fServerTime)
	{
		m_beginWatch = true;
		m_clientTime = fServerTime;
	}

	public void ResetWatch()
	{
		m_beginWatch = false;
		m_clientTime = 0.0f;
	}

	public void Update(float fDeltaTime)
	{
		if( !m_beginWatch )
			return;
		m_clientTime += fDeltaTime;
	}
}