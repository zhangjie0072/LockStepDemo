using UnityEngine;
using System.Collections.Generic;

public class PassHelper
{
	public PassHelper ()
	{
	}

	public static Player ChoosePassTarget(Player passer)
	{
		Player catcher = null;
		IM.Number minAngle = IM.Number.max;
		foreach(Player member in passer.m_team.members)
		{
			if(member == passer)
				continue;
			PlayerState ps = member.m_StateMachine.m_curState;
			if( ps.m_eState == PlayerState.State.eRebound
			   || ps.m_eState == PlayerState.State.eBlock
			   || ps.m_eState == PlayerState.State.eFallGround
			   )
				continue;


			IM.Vector3 dirPasser2Member = GameUtils.HorizonalNormalized(member.position, passer.position);
			IM.Number angle = IM.Vector3.Angle(passer.moveDirection, dirPasser2Member);
			if( angle > minAngle )
				continue;
			minAngle = angle;
			catcher = member;
		}
		return catcher;
	}
}

