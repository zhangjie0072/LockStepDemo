using UnityEngine;
using System.Collections.Generic;

public class PlayerState_ResultPose : PlayerState
{
	public string pose;
	public bool withBall;

	private UBasketball 	m_tmpBall;

	public PlayerState_ResultPose (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eResultPose;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		if( withBall )
		{
			GameObject goBall = GameObject.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/basketBall")) as GameObject;
			m_tmpBall = goBall.GetComponent<UBasketball>();
			if( m_tmpBall == null )
				m_tmpBall = goBall.AddComponent<UBasketball>();

			m_player.GrabBall(m_tmpBall);
		}

		m_player.mSparkEffect.EnableSpark(false);

		m_curAction = pose;
		if(string.IsNullOrEmpty(m_curAction))
			m_stateMachine.SetState(State.eStand);
		else
			m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);
		if( m_ball == m_match.mCurScene.mBall )
			m_player.DropBall(m_ball);
	}

	public override void OnExit ()
	{
		if( m_tmpBall != null )
		{
			m_player.DropBall(m_tmpBall);
			GameObject.Destroy(m_tmpBall.gameObject);
		}

		m_tmpBall = null;

		m_player.animMgr.Stop();
	}

}
