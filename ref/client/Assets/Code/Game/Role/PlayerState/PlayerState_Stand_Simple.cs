using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Stand_Simple : PlayerState_Stand
{
	public PlayerState_Stand_Simple (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
	}
	
	protected override string DetermineAction()
	{
		string curAction = "stand";
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft  )
			curAction = "standWithBallL";
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight  )
			curAction = "standWithBallR";
		return curAction;
	}
}
