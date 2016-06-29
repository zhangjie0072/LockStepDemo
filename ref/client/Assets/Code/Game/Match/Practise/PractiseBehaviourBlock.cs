using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PractiseBehaviourBlock : PractiseBehaviour, PlayerActionEventHandler.Listener
{
	public enum Step
	{
		Tip0,
		Run,
		Tip1,
		Tip2,
	}
	public Step step;

	private Player attacker;

	private bool blocked;

	private Stack<PlayerActionEventHandler.AnimEvent> attackerAnim = new Stack<PlayerActionEventHandler.AnimEvent>();

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eAway;
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Block && (!in_tutorial || step == Step.Tip1);
	}

	protected override void OnMatchSetted()
	{
		base.OnMatchSetted();

		match.mainRole.eventHandler.AddEventListener(this);
		match.mainRole.m_alwaysForbiddenPickup = true;
		attacker = match.m_awayTeam.GetMember(0);
		attacker.eventHandler.AddEventListener(this);
		attacker.m_alwaysForbiddenPickup = true;
		match.mCurScene.mBall.onHitGround = OnShootOver;
	}

	public override bool ResetPlayerPos()
	{
		IM.Vector3 basketCenter = match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		match.mainRole.position = basketCenter - IM.Vector3.forward * IM.Number.half;
		match.mainRole.forward = -IM.Vector3.forward;
		IM.Number angle = IM.Random.Range(new IM.Number(90), new IM.Number(270));
		IM.Vector3 dir = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * IM.Vector3.forward;
		attacker.position = basketCenter + dir * new IM.Number(4,500);

		if (match.mCurScene.mBall.m_owner != null)
			match.mCurScene.mBall.m_owner.DropBall(match.mCurScene.mBall);
		attacker.GrabBall(match.mCurScene.mBall);

		return true;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseBlock_Idle;
	}

    public override IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
		if (!blocked)
            return IM.Number.one;
		else
			return rate;
	}

    public override IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
        return IM.Number.one;
	}

	private void OnBlockPress(GameObject go, bool pressed)
	{
		if (step == Step.Tip1)
			Pause(false);
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();
#if UNITY_IPHONE || UNITY_ANDROID
		UIEventListener.Get(match.m_uiController.m_btns[0].btn.gameObject).onPress += OnBlockPress;
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

    public override void ViewUpdate(float deltaTime)
    {
        base.ViewUpdate(deltaTime);

#if !UNITY_IPHONE && !UNITY_ANDROID
		if (in_tutorial && step == Step.Tip1 && Input.GetKey(KeyCode.J))
			Pause(false);
#endif
    }

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (step == Step.Run && EnterBlockTiming())
			Tip1();

		//for block with ball
		Player owner = match.mCurScene.mBall.m_owner;
		if (owner != null && owner != attacker && owner.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
			OnShootOver(match.mCurScene.mBall);

		//match.m_mainRole.position = attacker.position + attacker.forward/2;
		match.HighlightButton(0, in_tutorial && IsCommandValid(Command.Block));
	}

	bool EnterBlockTiming()
	{
		if (attacker.m_StateMachine.m_curState.m_eState != PlayerState.State.eLayup)
			return false;

		if (attacker.m_AOD.GetStateByPos(match.mainRole.position) == AOD.Zone.eInvalid)
			return false;
		
		//Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = match.m_mainRole.m_animAttributes.m_block;
		//int blockKey = blocks["block"].GetKeyFrame("OnBlock").frame - blocks["block"].GetKeyFrame("OnLeaveGround").frame;
		//float fEventBlockTime = (float)blockKey / match.m_mainRole.m_StateMachine.m_Animation["block"].clip.frameRate;
		
		//string shoot_id = attacker.m_StateMachine.m_curState.m_curExecSkill.curAction.action_id;
		//PlayerAnimAttribute.AnimAttr shootAnims = attacker.m_animAttributes.GetAnimAttrById(Command.Layup, shoot_id);
		//int shootOutKey = shootAnims.GetKeyFrame("OnLayupShot").frame;
		//float fEventShootOutTime = (float)shootOutKey / attacker.m_StateMachine.m_Animation["layup"].clip.frameRate;
		//float fShootEclipseTime = attacker.m_StateMachine.m_Animation[shoot_id].time;
		//float fPlayerMovingTime = fEventShootOutTime - fShootEclipseTime;
		//float fBallFlyTime = fEventBlockTime - fPlayerMovingTime;

		//return fBallFlyTime >= 0f;
		return attacker.m_blockable.blockable;
	}

	public void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (animEvent == PlayerActionEventHandler.AnimEvent.eBlock && sender == match.mainRole)
			OnBlock();
	}

	private void OnBlock()
	{
		//if (shooting)
		//{
			if (in_tutorial)
				Tip2();
			else
				FinishObjective(true);
			blocked = true;
		//}
		//else
		//{
		//	Debug.LogError("Not Shooting when blocked");
		//	while (attackerAnim.Count > 0)
		//	{
		//		Debug.LogError(attackerAnim.Pop().ToString());
		//	}
		//}
	}

	private void OnShootOver(UBasketball ball)
	{
		if (!blocked && _curr_obj_index != EXERCISE_OBJ_INDEX)
		{
			FinishObjective(false);
		}
		Reset();
	}

	private IEnumerator ResetLater()
	{
		yield return new WaitForSeconds(2);
		Reset();
	}

	private void Reset()
	{
		StopCoroutine("ResetLater");
		blocked = false;
		match.m_homeTeam.m_role = GameMatch.MatchRole.eDefense;
		match.m_awayTeam.m_role = GameMatch.MatchRole.eOffense;
		match.m_stateMachine.SetState(MatchState.State.eBegin);
	}

	private void Tip0()
	{
		step = Step.Tip0;
		match.tip = practise.tips[0];
		match.ShowGuideTip();
		match.ShowTipArrow();
        InputReader.Instance.enabled = false;
	}

	private void Run()
	{
		step = Step.Run;
		match.HideGuideTip();
        InputReader.Instance.enabled = false;
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
		InputReader.Instance.enabled = false;
	}

	private void OnTipClick(GameObject go)
	{ 
		switch (step)
		{
			case Step.Tip0:
				Run();
				break;
			case Step.Tip2:
				FinishObjective(true);
				match.HideGuideTip();
                InputReader.Instance.enabled = true;
				break;
		}
	}
}
