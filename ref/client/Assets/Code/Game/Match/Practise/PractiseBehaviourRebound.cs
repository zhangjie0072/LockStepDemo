using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;

public class PractiseBehaviourRebound : PractiseBehaviour
{
	public enum Step
	{
		Tip0,
		Shoot,
		Tip1,
		Tip2,
	}
	public Step step;

	private Player attacker;

	private bool ball_rebounding;
	private bool player_rebounding;
	private bool failed_on_rebound_over;
	private PlayerState.State curr_state;
	IM.Number minHeight;
	IM.Number maxHeight;
	IM.Number eventTime;

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eHome;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseRebound_Idle;
	}

	protected override void OnMatchSetted()
	{
		match.m_mainRole.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(match.m_mainRole.m_StateMachine, match));
		match.m_mainRole.m_alwaysForbiddenPickup = true;
		attacker = match.m_homeTeam.GetMember(1);
		attacker.m_alwaysForbiddenPickup = true;
		match.mCurScene.mBall.onHitGround += OnBallHitGround;
		match.mCurScene.mBall.onRebound += OnRebound;
		match.mCurScene.mBall.onGrab += OnGrab;
		PlayerState_Rebound.GetDefaultHeightRange(match.m_mainRole, out minHeight, out maxHeight);
		eventTime = PlayerState_Rebound.GetEventTime(match.m_mainRole);
	}

	public override bool ResetPlayerPos()
	{
        //GameObject center3PT = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/ThreePTCenter") as GameObject;
        //attacker.position = new IM.Vector3(center3PT.transform.position);
        //attacker.forward = IM.Vector3.forward;
        attacker.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position;
        attacker.forward = IM.Vector3.forward;
		IM.Vector3 basketCenter = match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		match.m_mainRole.position = basketCenter - IM.Vector3.forward * IM.Number.half;
		match.m_mainRole.forward = -IM.Vector3.forward;

		if (match.mCurScene.mBall.m_owner != null)
			match.mCurScene.mBall.m_owner.DropBall(match.mCurScene.mBall);
		attacker.GrabBall(match.mCurScene.mBall);

		return true;
	}

    public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
		if (shooter == attacker)
            return IM.Number.zero;
		else
			return rate;
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Rebound &&(!in_tutorial || step == Step.Tip1);
	}

    public override ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.BigNumber rate)
	{
		if (in_tutorial)
			return GameSystem.Instance.shootSolutionManager.GetShootSolution(25, false, 1);
		else
			return null;
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();
#if UNITY_IPHONE || UNITY_ANDROID
		UIEventListener.Get(match.m_uiController.m_btns[0].btn.gameObject).onPress += OnReboundPress;
#endif
	}

	protected override void OnStart()
	{
		base.OnStart();

		Reset();
		if (!_free_mode)
		{
			match.onTipClick = OnTipClick;
			Tip0();
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		if (match.m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eRebound)
		{
			if (curr_state != PlayerState.State.eRebound)
			{
				player_rebounding = true;
			}
		}
		else
		{
			if (player_rebounding)
				OnPlayerReboundOver();
			player_rebounding = false;
		}

		curr_state = match.m_mainRole.m_StateMachine.m_curState.m_eState;

		if (step == Step.Shoot && match.mCurScene.mBall.m_ballState == BallState.eRebound)
		{
			Vector3 velocity = (Vector3)match.mCurScene.mBall.curVel;
			if (velocity.y < 0f && InReboundRange())
			{
				Tip1();
			}
		}

		match.HighlightButton(0, in_tutorial && IsCommandValid(Command.Rebound));

#if !UNITY_IPHONE && !UNITY_ANDROID
		if (in_tutorial && step == Step.Tip1 && Input.GetKey(KeyCode.J))
			Pause(false);
#endif

		//if (Input.GetKey(KeyCode.R))
		//{
		//	OnStart();
		//	Shoot();
		//	Pause(false);
		//}
	}

	private bool InReboundRange()
	{
		UBasketball ball = match.mCurScene.mBall;
		IM.Vector3 pos;
		ball.GetPositionInAir(ball.m_fTime + eventTime, out pos);
        IM.Number ball_height = pos.y;
		return minHeight < ball_height && ball_height < maxHeight;
	}

	private void OnReboundPress(GameObject go, bool pressed)
	{
		if (in_tutorial && step == Step.Tip1)
			Pause(false);
	}

	private void OnRebound(UBasketball ball)
	{
		if (ball.m_actor == attacker)
		{
			ball_rebounding = true;
			match.HighlightButton(0, !_free_mode);
		}
	}

	private void OnBallHitGround(UBasketball ball)
	{
		if (!ball_rebounding)
			return;

		ball_rebounding = false;

		match.HighlightButton(0, false);

		if (_curr_obj_index != EXERCISE_OBJ_INDEX)
		{
			if (!failed_on_rebound_over)
			{
				//Logger.Log("**&& Failed on ball hit ground.");
				FinishObjective(false);
			}
		}
		Reset();
	}

	private void OnPlayerReboundOver()
	{
		if (ball_rebounding)
		{
			if (match.mCurScene.mBall.m_owner != match.m_mainRole)
			{
				if (_curr_obj_index != EXERCISE_OBJ_INDEX)
				{
					//Logger.Log("**&& Failed on rebound over.");
					FinishObjective(false);
					failed_on_rebound_over = true;
				}
			}
			StartCoroutine("ResetLater");
		}
	}

	private void OnGrab(UBasketball ball)
	{
		match.HighlightButton(0, false);
		if (ball.m_owner == match.m_mainRole && player_rebounding && ball_rebounding)
		{
			if (in_tutorial)
				Tip2();
			else
				FinishObjective(true);
		}
	}

	private IEnumerator ResetLater()
	{
		yield return new WaitForSeconds(0.5f);
		Reset();
	}

	private void Reset()
	{
		StopCoroutine("ResetLater");
		match.m_homeTeam.m_role = GameMatch.MatchRole.eOffense;
		match.m_awayTeam.m_role = GameMatch.MatchRole.eDefense;
		match.m_stateMachine.SetState(MatchState.State.eBegin);
		failed_on_rebound_over = false;
		player_rebounding = false;
		ball_rebounding = false;
		match.HighlightButton(0, false);
	}

	private void Tip0()
	{
		step = Step.Tip0;
		match.tip = practise.tips[0];
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.m_mainRole.m_inputDispatcher.m_enable = false;
	}

	private void Shoot()
	{
		step = Step.Shoot;
		match.HideGuideTip();
		match.m_mainRole.m_inputDispatcher.m_enable = false;
	}

	private void Tip1()
	{
		step = Step.Tip1;
		match.tip = practise.tips[1];
		match.ShowGuideTip();
		match.HideTipArrow();
		Pause();
		InputReader.Instance.enabled = true;
		InputReader.Instance.Update(match);
		match.m_uiController.UpdateBtnCmd();
	}

	private void Tip2()
	{
		step = Step.Tip2;
		match.tip = practise.tips[2];
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.m_mainRole.m_inputDispatcher.m_enable = false;
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.Tip0:
				Shoot();
				break;
			case Step.Tip2:
				FinishObjective(true);
				match.HideGuideTip();
				match.m_mainRole.m_inputDispatcher.m_enable = true;
				break;
		}
	}
}
