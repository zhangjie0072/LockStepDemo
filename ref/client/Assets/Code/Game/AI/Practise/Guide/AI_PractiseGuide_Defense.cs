using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseGuide_Defense : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_Defense(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Defense;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		if (behaviour.GetBehaviour(system.index) != PractiseStepBehaviour.Defense)
		{
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
			return;
		}
		else
		{
            IM.Vector3 dirTarget2Basket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, m_player.m_defenseTarget.position);
			IM.Vector3 targetPos = m_player.m_defenseTarget.position + dirTarget2Basket * IM.Number.two;
			if (GameUtils.HorizonalDistance(targetPos, m_player.position) > new IM.Number(0,100))
				m_moveTarget = targetPos;
			else
			{
				m_player.m_moveHelper.StopMove();
				m_player.m_moveType = fogs.proto.msg.MoveType.eMT_Defense;
			}

			List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
			if( basicSkills.Count != 0 )
				m_player.m_toSkillInstance = basicSkills[0];
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}