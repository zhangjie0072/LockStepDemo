using UnityEngine;
using System.Collections.Generic;

public class Blockable
{
	private IM.Number m_fBlockableBeginTime;
	private IM.Number m_fBlockableEndTime;
	private IM.Number m_fCurTime;

	public bool blockable { get { return m_fBlockableBeginTime < m_fCurTime && m_fCurTime < m_fBlockableEndTime; } }
	public bool tooEarly { get { return m_fCurTime <= m_fBlockableBeginTime; } }
	public bool tooLate { get { return m_fCurTime >= m_fBlockableEndTime; } }

	private Player	m_player;

	public Blockable(Player player)
	{
		m_player = player;
	}

	public void Init(PlayerAnimAttribute.AnimAttr animAttr, IM.Number framerate, int beginFrameOffset = 0) 
	{
		m_fBlockableBeginTime = IM.Number.zero;
        m_fBlockableEndTime = IM.Number.zero;

		PlayerAnimAttribute.KeyFrame_Blockable blockableFrame = animAttr.GetKeyFrame("blockable") as PlayerAnimAttribute.KeyFrame_Blockable;
		if( blockableFrame != null )
		{
			m_fBlockableBeginTime = (blockableFrame.frame - beginFrameOffset) / framerate;
			m_fBlockableEndTime = m_fBlockableBeginTime + blockableFrame.blockFrame / framerate;
		}
	}

	public void Update(IM.Number fCurTime)
	{
		m_fCurTime = fCurTime;
	}

	public void Clear()
	{
        m_fBlockableBeginTime = IM.Number.zero;
        m_fBlockableEndTime = IM.Number.zero;
		m_fCurTime = IM.Number.zero;
	}
}
