using UnityEngine;
using fogs.proto.msg;

public class PlayerState_GoalPose : PlayerState
{
	public PlayerState_GoalPose (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eGoalPose;
		m_mapAnimType.Add(AnimType.N_TYPE_0, "posGoal");
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
        //TODO:目前照相机上不存在position的IM.Vector3数据，选暂定先面向原点，后面想办法改回来
        //IM.Vector3 camPos = new IM.Vector3(m_match.m_cam.transform.position);
        IM.Vector3 camPos = IM.Vector3.zero;
		m_player.FaceTo(camPos);

		m_animType = AnimType.N_TYPE_0;
		m_curAction = m_mapAnimType[m_animType];

		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePoseGoal);
		m_player.animMgr.CrossFade(m_curAction, false);
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone();
		m_stateMachine.SetState(PlayerState.State.eStand);
	}
}
