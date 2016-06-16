using UnityEngine;

public class PlayerState_IdlePose:  PlayerState
{
	private string m_strIdlePoseLoop = "pos1";
	private string m_strIdlePose = "pos2";
	
	private IM.Number	m_fIdleLoopToPoseTime = new IM.Number(3);
	private IM.Number	m_fInternalTimeCounter = IM.Number.zero;
	
	public PlayerState_IdlePose (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eIdlePose;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
        m_player.animMgr.CrossFade(m_strIdlePoseLoop, false);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.animMgr.IsPlaying(m_strIdlePoseLoop) )
		{
			m_fInternalTimeCounter += fDeltaTime;
			if( m_fInternalTimeCounter > m_fIdleLoopToPoseTime )
			{
                m_player.animMgr.CrossFade(m_strIdlePose, false);
                m_fInternalTimeCounter = IM.Number.zero;
			}
			else
			{
				m_fInternalTimeCounter += fDeltaTime;
			}
		}
		else
		{
            if (!m_player.animMgr.IsPlaying(m_strIdlePose))
                m_player.animMgr.CrossFade(m_strIdlePoseLoop, false);
		}
	}
}
