using UnityEngine;
using fogs.proto.msg;

public class ShootHelper
{
	public const int	m_iNearShot 	= 7000;
	public const int	m_iMiddleShot 	= 7001;
	public const int	m_iFarShot 		= 7002;

	public ShootHelper()
	{
	}

	public static void ShootByInput(Player player)
	{
		if(player.m_toSkillInstance.skill.subtype == SkillSubType.BASIC
		   || player.m_toSkillInstance.skill.subtype == SkillSubType.SPEC)
			player.m_StateMachine.SetState(PlayerState.State.ePrepareToShoot);
		else
			player.m_StateMachine.SetState(PlayerState.State.eShoot);
	}

	public static SkillInstance ShootByArea(Player player, GameMatch match)
	{
		Area eArea = match.mCurScene.mGround.GetArea(player);
		SkillInstance skillInstance = null;
		if( eArea == Area.eNear )
			skillInstance = player.m_skillSystem.GetSkillById(m_iNearShot);
		else if( eArea == Area.eMiddle )
			skillInstance = player.m_skillSystem.GetSkillById(m_iMiddleShot);
		else if( eArea == Area.eFar )
			skillInstance = player.m_skillSystem.GetSkillById(m_iFarShot);

		return skillInstance;
	}

}