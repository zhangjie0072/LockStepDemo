using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Knocked_NoHold : PlayerState_Knocked
{
	public PlayerState_Knocked_NoHold (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_rateHoldBall = IM.Number.zero;
	}
}


