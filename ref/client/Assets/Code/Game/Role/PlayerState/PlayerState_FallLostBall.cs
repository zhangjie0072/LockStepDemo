using UnityEngine;
using fogs.proto.msg;

public class PlayerState_FallLostBall : PlayerState
{
	public enum Step
	{
		eFall,
		eHipGrounded
	}
	public Step	m_step;

	public PlayerState_FallLostBall (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eFallLostBall;
		m_mapAnimType.Add(AnimType.N_TYPE_0, "fallLostBall");
	}

	override public void OnEnter ( PlayerState lastState )
	{
		m_step = Step.eFall;
		m_animType = AnimType.N_TYPE_0;
		
        m_player.m_lostBallContext.vInitPos = m_ball.position;
        m_player.m_lostBallContext.vInitVel = m_player.forward;

		Debug.Log("m_animType: " + m_animType);
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, false);
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.FallGround);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		if( m_step == Step.eFall )
			m_player.Move(fDeltaTime, -(m_player.forward * IM.Number.two));
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone();
		m_player.m_StateMachine.SetState(State.eStand);

		m_match.mCurScene.mBall.onLost -= OnBallLost;
	}

	void OnBallLost(UBasketball ball)
	{
		if (ball.m_owner != null)
			Debug.LogError("Ball owner: " + ball.m_owner.m_name);
		ball.position = m_player.position;
        //TODO:ºóÃæÐèÒªÐÞ¸Ä£¬²»ÄÜÊ¹ÓÃUnityµÄVector3²ÎÓëÔËËã
		ball.GetComponent<Rigidbody>().velocity = (Vector3)(-m_player.forward * new IM.Number(2));
	}
}


