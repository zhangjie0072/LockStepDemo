using UnityEngine;

public class AI_PractiseBaseGuide_Positioning : AIState
{

	public AI_PractiseBaseGuide_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_Positioning;
	}
	
	override public void OnEnter ( AIState lastState )
	{
        m_moveTarget = m_match.mCurScene.mBasket.m_vShootTarget;
        IM.Vector3 target = m_moveTarget;
        target.y = IM.Number.zero;
		m_player.m_moveType = fogs.proto.msg.MoveType.eMT_Run;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (m_match.mCurScene.mGround.GetLayupArea(m_player) == fogs.proto.msg.Area.eNear)
		{
			m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Layup);
		}
	}
}