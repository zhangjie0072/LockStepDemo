using UnityEngine;
using System.Collections.Generic;

public class PlayerState_BodyThrowCatch : PlayerState_Skill
{
	public 	IM.Vector3 	m_dirThrow;
	public 	bool		m_bSuccess = false;
	public	bool		m_bCollideBall = false;

	public	IM.Vector3		m_vInitPos;
	public	IM.Vector3		m_vInitVelocity;

	public PlayerState_BodyThrowCatch (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBodyThrowCatch;

		m_validStateTransactions.Remove(Command.Steal);
		m_validStateTransactions.Remove(Command.Shoot);
		m_validStateTransactions.Remove(Command.Rebound);
		m_validStateTransactions.Remove(Command.RequireBall);
		m_validStateTransactions.Remove(Command.PickAndRoll);
		m_validStateTransactions.Remove(Command.Pass);
		m_validStateTransactions.Remove(Command.Layup);
		m_validStateTransactions.Remove(Command.Dunk);
		m_validStateTransactions.Remove(Command.BackToBack);
		m_validStateTransactions.Remove(Command.Block);
		m_validStateTransactions.Remove(Command.BodyThrowCatch);
		m_validStateTransactions.Remove(Command.CrossOver);
		m_validStateTransactions.Remove(Command.CutIn);
		m_validStateTransactions.Remove(Command.BackToBack);
		m_validStateTransactions.Remove(Command.Defense);
		m_validStateTransactions.Remove(Command.Boxout);
		m_validStateTransactions.Remove(Command.MoveStep);
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		bool bValid = false;

        m_player.FaceTo( m_ball.position );
        
        m_dirThrow = GameUtils.HorizonalNormalized(m_ball.position, m_player.position);

        m_bSuccess = true;

        if( m_ball.m_picker != null )
            m_bSuccess = false;

        /*
        Dictionary<string, PlayerAnimAttribute.AnimAttr> catches = m_player.m_animAttributes.m_catch;
        PlayerAnimAttribute.AnimAttr catchAnim = catches[m_curAction];
        PlayerAnimAttribute.KeyFrame catchFrame = catchAnim.GetKeyFrame("OnCatch");
        if( catchFrame == null )
            return;
        AnimationClip catchClip = m_stateMachine.m_Animation[m_curAction].clip;
        float fEventTime = catchFrame.frame / catchClip.frameRate;
        Vector3 vBallPosWhenCatch;
        m_player.GetAnimatedBonePosition(m_curAction, "ball", fEventTime, out vBallPosWhenCatch);
        float fDistPlayer2Ball = GameUtils.HorizonalDistance(vBallPosWhenCatch, m_player.position);
        */

        Dictionary<string, uint> data = m_player.m_finalAttrs;
        if( data == null )
				Debug.LogError("Can not build player: " + m_player.m_name + " ,can not find state by id: " + m_player.m_id );

        IM.Number fDistCurPlayer2Ball = GameUtils.HorizonalDistance(m_ball.position, m_player.position);
        IM.Number fMaxDist = GetMaxDistance(m_player);
        Debugger.Instance.m_steamer.message = "Body throw catch: cur distance: " + fDistCurPlayer2Ball + " maxCatchDistance: " + fMaxDist;
        if( fDistCurPlayer2Ball > fMaxDist )
            m_bSuccess = false;

        if( m_ball.position.y > IM.Number.one)
        {
            m_bSuccess = false;
            Debugger.Instance.m_steamer.message = "Body throw catch: Ball height: " + m_ball.transform.position.y + " higher than MaxHeight: " + 1.0f;
        }

        bValid = m_bSuccess;
        SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBodyThrowCatch_rate);
        IM.Number fProb = skillSpec.value;
        IM.Number fProbValue = IM.Random.value;
        if( fProbValue > fProb )
        {
            m_bSuccess = false;
            Debugger.Instance.m_steamer.message = "Body throw catch: Probability: " + fProb + " actual prob: " + fProbValue;
        }

        m_vInitPos = m_ball.position;
        m_vInitVelocity = m_dirThrow * new IM.Number(1,500);
        m_vInitVelocity.y = IM.Number.two;

        if (m_bSuccess)
        {
            m_player.GrabBall(m_ball);
            m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePickupBall);
            ++m_player.mStatistics.data.success_body_throw_catch_times;
        }

		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();

		m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, m_bSuccess);
		
		++m_player.mStatistics.data.body_throw_catch_times;
		if (bValid)
			++m_player.mStatistics.data.valid_body_throw_catch_times;

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.BodyThrowCatch);
	}

	public static IM.Number GetMaxDistance(Player player)
	{
		List<SkillInstance> bodyThrowCatch = player.m_skillSystem.GetBasicSkillsByCommand(Command.BodyThrowCatch);
		SkillSpec skillSpec = player.GetSkillSpecialAttribute(SkillSpecParam.eBodyThrowCatch_dist, bodyThrowCatch[0]);
        return skillSpec.value;
		//return player.m_finalAttrs["bodythrowcatch_distance"] * 0.0285f;
	}

	/*
	public void OnCatch(UBasketball ball)
	{
		if( ball.m_owner != null )
			return;

		if( m_bSuccess )
		{
			m_player.GrabBall(ball);
			m_player.m_eventHandler.NotifyAllListeners(UPlayerActionEventHandler.AnimEvent.eCatch);
			ball.OnCatch();
		}
		//else
		//{
		//	ball.m_ballState 	= BallState.eLoseBall;
		//	ball.m_vInitPos 	= m_vInitPos;
		//	ball.m_vInitVel		= m_vInitVelocity;
		//}
	}
	*/
}