using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_None
	: PlayerState
{
	public PlayerState_None (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
	}
}