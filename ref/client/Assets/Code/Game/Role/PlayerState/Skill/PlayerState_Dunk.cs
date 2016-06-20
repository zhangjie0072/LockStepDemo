using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Dunk : PlayerState_Skill
{
	private IM.BigNumber	m_dunkRate = IM.Number.zero;

	private	bool			m_bRateFixed = false;

	private bool			m_bMoving = false;
	private IM.Number		m_movingTime = IM.Number.zero;
	private IM.Number		m_movingTimeThreshold = IM.Number.zero;

	private IM.Vector3 		m_vMovePos;

	private bool			m_bLeaveGround = false;
	private IM.Number		m_leaveGroundTime = IM.Number.zero;
	private IM.Number		m_fFlyTimeOnAir = IM.Number.zero;

	static HedgingHelper dunkNearHedging = new HedgingHelper("DunkNear");
	static HedgingHelper dunkMiddleHedging = new HedgingHelper("DunkMiddle");

    static IM.BigNumber dunkMiddleMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "DunkMiddle");
    static IM.BigNumber dunkNearMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "DunkNear");
    static IM.BigNumber dunkMiddleAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "DunkMiddle");
    static IM.BigNumber dunkNearAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "DunkNear");
	
	public PlayerState_Dunk (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eDunk;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		m_skillArea = m_match.mCurScene.mGround.GetArea(m_player);
		
		m_bLeaveGround = false;
		m_movingTime = IM.Number.zero;
		m_bMoving = false;
        m_fFlyTimeOnAir = IM.Number.zero;

		PlayerAnimAttribute.AnimAttr dunkAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Dunk, m_curAction);
		if( dunkAttr == null )
		{
			Logger.LogError("Current action: " + m_curAction + " in dunk id: " + m_curExecSkill.skill.id);
		}

        IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
		m_player.m_blockable.Init(dunkAttr, frameRate);

        IM.RootMotion rootMotion = m_player.animMgr.GetRootMotion(m_curAction);
		rootMotion.Reset();

        IM.Vector3 vPlayer2Basket = m_basket.m_vShootTarget - m_player.position;
		vPlayer2Basket.y = IM.Number.zero;
		IM.Vector3 dirPlayerToBasket = vPlayer2Basket.normalized;
		rootMotion.dirMove = dirPlayerToBasket;

		PlayerAnimAttribute.KeyFrame rotKeyFrame = dunkAttr.GetKeyFrame("RotateToBasket");
		if( rotKeyFrame != null )
		{
			IM.Number fTimeFromRotateToBasket = rotKeyFrame.frame / frameRate;
			IM.Number fAngle = IM.Vector3.Angle(m_player.forward, dirPlayerToBasket);
			m_turningSpeed = IM.Math.Deg2Rad(fAngle) / fTimeFromRotateToBasket;
			m_rotateTo = RotateTo.eBasket;
			m_rotateType = RotateType.eSmooth;
			m_bMoveForward = false;
		}
		
		PlayerAnimAttribute.KeyFrame_MoveToStartPos kf_mts = dunkAttr.GetKeyFrame("MoveToStartPos") as PlayerAnimAttribute.KeyFrame_MoveToStartPos;
		IM.Vector3 vToBasketOffset = kf_mts.toBasketOffset;
        IM.Vector3 vRimHPos = m_basket.m_vShootTarget;
		vRimHPos.y = IM.Number.zero;

        m_movingTimeThreshold = kf_mts.frame / frameRate;
		
		m_vMovePos = -dirPlayerToBasket * vToBasketOffset.x + vRimHPos;
		IM.Vector3 vMovePos2Bakset = m_vMovePos - m_player.position;
		int fSameDir = IM.Vector3.Dot(vMovePos2Bakset,  vPlayer2Basket);
		bool bRush = false;
		if( vPlayer2Basket.magnitude < vMovePos2Bakset.magnitude || fSameDir < 0.0f || kf_mts.frame == 0 )
		{
			m_speed = IM.Vector3.zero;
			bRush = false;
		}
		else
		{
			m_speed = (m_vMovePos - m_player.position) / (kf_mts.frame / frameRate);
			bRush = true;
		}

		/////
		PlayerAnimAttribute.KeyFrame kf_dunk		 = dunkAttr.GetKeyFrame("OnDunk");
		PlayerAnimAttribute.KeyFrame kf_leaveGround  = dunkAttr.GetKeyFrame("OnLeaveGround");
		
		IM.Vector3 vPosDunk, vPosLeaveGround;
		m_player.GetNodePosition(SampleNode.Root, m_curAction, kf_dunk.frame / frameRate, out vPosDunk );

		m_leaveGroundTime = kf_leaveGround.frame / frameRate;
		m_fFlyTimeOnAir = AnimationSampleManager.Instance.GetAnimData(m_curAction).duration - m_leaveGroundTime;

		m_player.GetNodePosition(SampleNode.Root, m_curAction, m_leaveGroundTime, out vPosLeaveGround );

		vPosLeaveGround = m_player.position + IM.Vector3.DotForNumber(vPosLeaveGround, dirPlayerToBasket) * dirPlayerToBasket;
		vPosDunk = m_player.position + IM.Vector3.DotForNumber(vPosDunk, dirPlayerToBasket) * dirPlayerToBasket;
		
		IM.Vector3 deltaDistance = vPosDunk - vPosLeaveGround;
		IM.Number fOrigDunkOffset = vToBasketOffset.x - deltaDistance.magnitude;
		IM.Number fPlayer2Basket = vPlayer2Basket.magnitude;
		IM.Number fDunkDist = fPlayer2Basket - fOrigDunkOffset;

		if( bRush )
			rootMotion.scale = IM.Number.one;
		else
		{
			if( fDunkDist > IM.Number.zero )
				rootMotion.scale = IM.Math.Max( fDunkDist / deltaDistance.magnitude, new IM.Number(0,200));
			else
				rootMotion.scale = fPlayer2Basket / vToBasketOffset.x; 

			Logger.Log("scale: " + rootMotion.scale);
		}

		m_bRateFixed = false;
		_CalcDunkRate();

		m_ball.OnBeginDunk(m_player);

		m_stateMachine.m_listeners.ForEach( (PlayerStateMachine.Listener lsn)=>{lsn.OnBeginDunk(m_player, kf_dunk.frame / frameRate);} );

		m_player.mStatistics.ReadyToCountShoot();

		if( m_speed != IM.Vector3.zero )
			m_bMoving = true;

		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision( m_player, true );

		m_player.animMgr.Play(m_curAction, !m_bMoving);

		bool defended = m_player.IsDefended();
		if (m_skillArea == Area.eNear)
		{
			++m_player.mStatistics.data.dunk_near_shoot;
			if (defended)
				++m_player.mStatistics.data.dunk_near_open_shoot;
		}
		else if (m_skillArea == Area.eMiddle)
		{
			++m_player.mStatistics.data.dunk_mid_shoot;
			if (defended)
				++m_player.mStatistics.data.dunk_mid_open_shoot;
		}
	}

	void _CalcDunkRate()
	{
		if( m_bRateFixed )
			return;

		IM.Number fSideEffect = IM.Number.zero;
		SkillSideEffect effect;
        if (m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eShootRate, out effect))
            fSideEffect = effect.value;
		//else
		//	Logger.Log("No side effect data.");

		Dictionary<string, uint> skillAttr = m_player.GetSkillAttribute();
		Dictionary<string, uint> data = m_player.m_finalAttrs;
		if( data == null )
		{
			Logger.LogError("Can not build player: " + m_player.m_name + " ,can not find state by id: " + m_player.m_id );
			return;
		}

		IM.Number fDisturb = IM.Number.zero;
		if (m_player.m_defenseTarget != null)
		{
			foreach (Player defenser in m_player.m_defenseTarget.m_team.members)
			{
				if (m_player.m_AOD.GetStateByPos(defenser.position) != AOD.Zone.eInvalid)
				{
					Dictionary<string, uint> defenderData = defenser.m_finalAttrs;

					uint disturb = defenderData["disturb"];
					uint disturb_attr = 0;
					defenser.m_skillSystem.HegdingToValue("addn_disturb", ref disturb_attr);
					//defenser.m_skillSystem.GetAttrValueByName("addn_disturb", ref disturb_attr);
					disturb += disturb_attr;

					uint fAntiDisturb = data["anti_disturb"];
					uint anti_disturb_attr = 0;
					m_player.m_skillSystem.HegdingToValue("addn_anti_disturb", ref anti_disturb_attr);
					//m_player.m_skillSystem.GetAttrValueByName("addn_anti_disturb", ref anti_disturb_attr);
                    fAntiDisturb += anti_disturb_attr;

                    if (m_skillArea == Area.eNear)
                        fDisturb = dunkNearHedging.Calc(new IM.Number((int)disturb), new IM.Number((int)fAntiDisturb));
                    else if (m_skillArea == Area.eMiddle)
                        fDisturb = dunkMiddleHedging.Calc(new IM.Number((int)disturb), new IM.Number((int)fAntiDisturb));

					m_bRateFixed = true;
					break;
				}
			}
		}
		else
			m_bRateFixed = true;

        uint uDunkData = 0;
        uint uDunkSkill = 0;
		
		if (m_skillArea == Area.eNear)
		{
			IM.Number reduceScale = m_match.GetAttrReduceScale("dunk_near", m_player);
            data.TryGetValue("dunk_near", out uDunkData);
			m_player.m_skillSystem.HegdingToValue("dunk_near", ref uDunkSkill);
			//m_player.m_skillSystem.GetAttrValueByName("dunk_near", ref uDunkSkill);

			m_dunkRate = ((uDunkData + uDunkSkill) * dunkNearMultiply + dunkNearAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
		}
		else if (m_skillArea == Area.eMiddle)
		{
			IM.Number reduceScale = m_match.GetAttrReduceScale("dunk_middle", m_player);
            data.TryGetValue("dunk_middle", out uDunkData);
			m_player.m_skillSystem.HegdingToValue("dunk_middle", ref uDunkSkill);
			//m_player.m_skillSystem.GetAttrValueByName("dunk_middle", ref uDunkSkill);
            
			m_dunkRate = ((uDunkData + uDunkSkill) * dunkMiddleMultiply + dunkMiddleAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
		}

		SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eDunk_rate);
		if( skillSpec.paramOp == SkillSpecParamOp.eAdd )
            m_dunkRate += skillSpec.value;
		else if( skillSpec.paramOp == SkillSpecParamOp.eMulti )
            m_dunkRate *= skillSpec.value;

		m_dunkRate = IM.Math.Max(m_dunkRate, IM.Number.zero);

		m_dunkRate = m_match.AdjustShootRate(m_player, m_dunkRate);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		m_player.m_blockable.Update(m_time);

		if (!m_player.m_bWithBall)
			m_bRateFixed = true;

		if( m_time > m_leaveGroundTime && !m_bLeaveGround )
		{
			m_bLeaveGround = true;
			m_stateMachine.m_listeners.ForEach( (PlayerStateMachine.Listener lsn)=>{lsn.OnDunkLeaveGround(m_player, m_fFlyTimeOnAir);} );
		}

		if( m_bMoving )
			m_movingTime += fDeltaTime;
		if( m_movingTime > m_movingTimeThreshold && m_bMoving )
		{
			m_bMoving = false;
			m_speed = IM.Vector3.zero;
			m_player.position = m_vMovePos;
		}

		//_CalcDunkRate();
	}

	public override void LateUpdate (IM.Number fDeltaTime)
	{
        if (!m_bMoving)
            m_player.animMgr.GetPlayInfo(m_curAction).enableRootMotion = true;
		base.LateUpdate (fDeltaTime);
	}

	public override void OnExit ()
	{
		base.OnExit();

		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision( m_player, false );

		m_stateMachine.m_listeners.ForEach( delegate(PlayerStateMachine.Listener lsn ) {
				lsn.OnDunkDone(m_player);
		});
	}

	public void OnDunk()
	{
        m_turningSpeed = IM.Number.zero;
		m_bRateFixed = true;

		if(m_player.m_bWithBall )
		{
			IM.Number prob = IM.Random.value;
			m_dunkRate = m_match.AdjustShootRate(m_player, m_dunkRate);

			//bool sumValue = m_stateMachine.attackRandom.AdjustRate(ref m_dunkRate, m_match.GetScore(2));
			
			Debugger.Instance.m_steamer.message += " Dunk rate: " ;
			Debugger.Instance.m_steamer.message += m_dunkRate;
			Debugger.Instance.m_steamer.message += " Probability: ";
			Debugger.Instance.m_steamer.message += prob;
			
			IM.Vector3 vBallVel = IM.Vector3.zero; 
			bool bGoal = prob < m_dunkRate;
			//goal
			if( bGoal )
			{
				IM.Number fPower = IM.Number.half;
				vBallVel = m_player.forward * fPower;
                vBallVel.y = -IM.Number.half;

				/*
				if (sumValue)
					m_stateMachine.attackRandom.SumValue(m_match.GetScore(2));
					*/

				Debugger.Instance.m_steamer.message += " .Dunk goal.\n";
			}
			//no goal
			else
			{
				IM.Number fPower = IM.Random.Range(IM.Number.two, new IM.Number(5));
				vBallVel = ( IM.Quaternion.AngleAxis(IM.Random.Range(-new IM.Number(30),new IM.Number(30)), IM.Vector3.up) * m_player.forward + IM.Vector3.up ) * fPower;
				vBallVel.y = new IM.Number(3);
				
				Debugger.Instance.m_steamer.message += " .Dunk no goal.\n";
			}

			IM.Vector3 vPos = m_ball.position;
			uint uBallId = m_ball.m_id;
			UBasketball curBall = m_ball;
			m_player.DropBall(curBall);
            curBall.position = m_basket.m_vShootTarget;

			bool bOpen = true;
			if (m_player.m_defenseTarget != null)
			{
				foreach (Player defenser in m_player.m_defenseTarget.m_team.members)
				{
					if (m_player.m_AOD.GetStateByPos(defenser.position) != AOD.Zone.eInvalid)
					{
						bOpen = false;
						break;
					}
				}
			}

			m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, bGoal);
			
			curBall.OnDunk(bGoal, vBallVel, m_basket.m_vShootTarget, m_player);
		}
		m_stateMachine.m_listeners.ForEach( (PlayerStateMachine.Listener lsn)=>{lsn.OnDunkDone(m_player);} );

		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eDunk);
	}

}