using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Layup:  PlayerState_Skill
{
	private	bool	m_bRateFixed = false;
	private IM.BigNumber	m_layupRate = IM.BigNumber.zero;

	private bool	m_bMoving = false;
	private IM.Number	m_movingTime = IM.Number.zero;
    private IM.Number m_movingTimeThreshold = IM.Number.zero;

    private IM.Number m_scale;

	private IM.Vector3 m_vMovePos;

	static HedgingHelper layupNearHedging = new HedgingHelper("LayupNear");
	static HedgingHelper layupMiddleHedging = new HedgingHelper("LayupMiddle");


    static IM.BigNumber layupMiddleMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "LayupMiddle");
    static IM.BigNumber layupNearMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "LayupNear");
    static IM.BigNumber layupMiddleAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "LayupMiddle");
    static IM.BigNumber layupNearAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "LayupNear");
	public PlayerState_Layup (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eLayup;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		m_skillArea = m_match.mCurScene.mGround.GetArea(m_player);
		
		if( !m_player.m_bSimulator )
			GameMsgSender.SendLayup(m_player, m_curExecSkill, m_skillArea);

		m_movingTime = IM.Number.zero;
		m_bMoving = false;

		m_player.mStatistics.ReadyToCountShoot();
		IM.RootMotion rootMotion = m_player.animMgr.Play(m_curAction, !m_bMoving).rootMotion;
        rootMotion.Reset();

		PlayerAnimAttribute.AnimAttr layupAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Layup, m_curAction);
		if( layupAttr == null )
		{
			Logger.LogError("Current action: " + m_curAction + " in layup, skill id: " + m_curExecSkill.skill.id);
		}

        IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
		m_player.m_blockable.Init(layupAttr, frameRate);

		IM.Number fTimeFromRotateToBasket = new IM.Number(layupAttr.GetKeyFrame("RotateToBasket").frame) / frameRate;
        IM.Vector3 vPlayer2Basket = m_basket.m_vShootTarget - m_player.position;
		vPlayer2Basket.y = IM.Number.zero;
		IM.Vector3 dirPlayerToBasket = vPlayer2Basket.normalized;
		rootMotion.dirMove = dirPlayerToBasket;

        IM.Number fAngle = IM.Vector3.Angle(m_player.forward, dirPlayerToBasket);
        m_turningSpeed = IM.Math.Deg2Rad(fAngle) / fTimeFromRotateToBasket;
		m_rotateTo = RotateTo.eBasket;
		m_rotateType = RotateType.eSmooth;
		m_bMoveForward = false;

		PlayerAnimAttribute.KeyFrame_MoveToStartPos kf_mts = layupAttr.GetKeyFrame("MoveToStartPos") as PlayerAnimAttribute.KeyFrame_MoveToStartPos;
		IM.Vector3 vToBasketOffset = kf_mts.toBasketOffset;
        IM.Vector3 vRimHPos = m_basket.m_vShootTarget;
		vRimHPos.y = IM.Number.zero;

		m_vMovePos = -dirPlayerToBasket * vToBasketOffset.x + vRimHPos;
		//Debugger.Instance.DrawSphere("layup", m_vMovePos, 0.1f);
        m_movingTimeThreshold = kf_mts.frame / frameRate;

		IM.Vector3 vMovePos2Bakset = m_vMovePos - m_player.position;
        IM.Number fSameDir = new IM.Number(IM.Vector3.Dot(vMovePos2Bakset, vPlayer2Basket));
		bool bRush = false;
		if( vPlayer2Basket.magnitude < vMovePos2Bakset.magnitude || fSameDir < IM.Number.zero || kf_mts.frame == 0 )
			m_speed = IM.Vector3.zero;
		else
		{
			m_speed = (m_vMovePos - m_player.position) / (kf_mts.frame / frameRate);
			bRush = true;
		}

		PlayerAnimAttribute.KeyFrame_LayupShootPos kf_layup = layupAttr.GetKeyFrame("OnLayupShot") as PlayerAnimAttribute.KeyFrame_LayupShootPos;
		PlayerAnimAttribute.KeyFrame kf_leaveGround = layupAttr.GetKeyFrame("OnLeaveGround") as PlayerAnimAttribute.KeyFrame;
		
		IM.Vector3 vPosLayup, vPosLeaveGround;
		m_player.GetNodePosition(SampleNode.Root, m_curAction, kf_layup.frame / frameRate, out vPosLayup );
		m_player.GetNodePosition(SampleNode.Root, m_curAction, kf_leaveGround.frame / frameRate, out vPosLeaveGround );
        if (vPosLayup == vPosLeaveGround)
            Logger.LogError("PlayerState_Layup, vPosLayup equals vPosLeaveGround");

        //TODO 这段代码不知道什么意思，待探讨
		//IM.Vector3 posLeaveGround = m_player.position + IM.Vector3.DotForNumber(vPosLeaveGround, dirPlayerToBasket) * dirPlayerToBasket;
		//IM.Vector3 posLayup = m_player.position + IM.Vector3.DotForNumber(vPosLayup, dirPlayerToBasket) * dirPlayerToBasket;

		IM.Number deltaDistance = (vPosLayup - vPosLeaveGround).magnitude;
        if (deltaDistance == IM.Number.zero)
            Logger.LogError("PlayerState_Layup, DeltaDistance is 0.");
		IM.Number fOrigLayupOffset = vToBasketOffset.x - deltaDistance;
		IM.Number fPlayer2Basket = vPlayer2Basket.magnitude;
		IM.Number fLayupDist = fPlayer2Basket - fOrigLayupOffset;

		if( bRush )
			rootMotion.scale = IM.Number.one;
		else
			rootMotion.scale = IM.Math.Max( fLayupDist / deltaDistance, IM.Number.zero);

		m_bRateFixed = false;
		_CalcLayupRate();

		if( m_speed != IM.Vector3.zero )
        {
            m_bMoving = true;
            m_player.animMgr.GetPlayInfo(m_curAction).enableRootMotion = false;
        }

		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision( m_player, true );

		if (m_skillArea == Area.eNear)
			++m_player.mStatistics.data.layup_near_shoot;
		else if (m_skillArea == Area.eMiddle)
			++m_player.mStatistics.data.layup_mid_shoot;
	}

	void _CalcLayupRate()
	{
		if( m_bRateFixed )
			return;
		
		IM.Number fSideEffect = IM.Number.zero;
		SkillSideEffect effect;
		if( m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eShootRate, out effect) )
			fSideEffect = effect.value;
		//else
		//	Logger.Log("No side effect data.");

		Dictionary<string, uint> data = m_player.m_finalAttrs;
		if( data == null )
		{
			Logger.LogError("Can not build player: " + m_player.m_name + " ,can not fight state by id: " + m_player.m_id );
			return;
		}
		Dictionary<string, uint> skillAttr = m_player.GetSkillAttribute();
		Dictionary<string, uint> playerData = m_player.m_finalAttrs;

		//m_player.m_roleInfo.skill_slot_info.Find( (SkillSlotProto skillSlot)=>{ return skillSlot.id == m_curExecSkill.skill.id; } );

		uint skillValue = 0;

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
                        fDisturb = layupNearHedging.Calc(new IM.Number((int)disturb),new IM.Number((int)fAntiDisturb));
                    else if (m_skillArea == Area.eMiddle)
                        fDisturb = layupMiddleHedging.Calc(new IM.Number((int)disturb),new IM.Number((int)fAntiDisturb));

					m_bRateFixed = true;
					break;
				}
			}
		}
		else
			m_bRateFixed = true;

		if (m_skillArea == Area.eNear)
		{
			IM.Number reduceScale = m_match.GetAttrReduceScale("layup_near", m_player);
			m_player.m_skillSystem.HegdingToValue("layup_near", ref skillValue);
			//m_player.m_skillSystem.GetAttrValueByName("layup_near", ref skillValue);

			m_layupRate = ((data["layup_near"] + skillAttr["layup_near"] + skillValue) * layupNearMultiply + layupNearAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
		}
		else if (m_skillArea == Area.eMiddle)
		{
			IM.Number reduceScale = m_match.GetAttrReduceScale("layup_middle", m_player);
			m_player.m_skillSystem.HegdingToValue("layup_middle", ref skillValue);
			//m_player.m_skillSystem.GetAttrValueByName("layup_middle", ref skillValue);
			
			m_layupRate = ((data["layup_middle"] + skillAttr["layup_middle"] + skillValue) * layupMiddleMultiply + layupMiddleAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
		}

		SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eLayup_rate);
        if (skillSpec.paramOp == SkillSpecParamOp.eAdd)
            m_layupRate += skillSpec.value;
        else if (skillSpec.paramOp == SkillSpecParamOp.eMulti)
            m_layupRate *= skillSpec.value;

		m_layupRate = m_match.AdjustShootRate(m_player, m_layupRate);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		m_player.m_blockable.Update(m_time);

		if (!m_player.m_bWithBall)
			m_bRateFixed = true;
		
		if( m_bMoving )
			m_movingTime += fDeltaTime;
		if( m_movingTime > m_movingTimeThreshold && m_bMoving )
		{
			m_bMoving = false;
			m_speed = IM.Vector3.zero;
            m_player.position = m_vMovePos;
            m_player.animMgr.GetPlayInfo(m_curAction).enableRootMotion = true;
		}
		//_CalcLayupRate();

		//Debugger.Instance.DrawSphere("root", m_player.m_root.position, Color.blue);
		//Debugger.Instance.DrawSphere("pevis", m_player.m_root.position, Color.green);
	}

	public void OnLayup()
	{
		m_turningSpeed = IM.Number.zero;

		bool bShootOut = false;
		bool bOpen = false;
		if( !m_player.m_bSimulator && m_player.m_bWithBall )
		{
			/*
			bool sumValue = false;
			if (!m_ball.m_bBlockSuccess)
				sumValue = m_stateMachine.attackRandom.AdjustRate(ref m_layupRate, m_match.GetScore(2));
				*/

			ShootSolution solution;
			if( m_ball.m_bBlockSuccess && m_ball.m_shootSolution != null)
				solution = m_ball.m_shootSolution;
			else
				solution = m_match.GetShootSolution(m_match.mCurScene.mBasket, m_skillArea, m_player, m_layupRate, ShootSolution.Type.Layup);
	
			//if (!m_ball.m_bBlockSuccess && solution.m_bSuccess && sumValue)
			//	m_stateMachine.attackRandom.SumValue(m_match.GetScore(2));

			m_ball.m_shootSolution = solution;
			IM.Vector3 vPos = m_ball.position;
			uint uBallId = m_ball.m_id;
			UBasketball curBall = m_ball;
			m_player.DropBall(curBall);
			curBall.position = vPos;
			curBall.m_castedSkill = m_curExecSkill;
			curBall.OnShoot(m_player, m_skillArea, true);

			ShootSolution.SShootCurve shootCurve = curBall.m_shootSolution.m_ShootCurveList[0];
            IM.Number fFlyTime = shootCurve.fTime;

			bShootOut = true;

			bOpen = _CheckOpenShoot();
			if( bOpen )
			{
				//m_layupRate *= 1.3f;
				//m_layupRate = Mathf.Min(m_layupRate, 1.0f);
				if (m_skillArea == Area.eNear)
					++m_player.mStatistics.data.layup_near_open_shoot;
				else if (m_skillArea == Area.eMiddle)
					++m_player.mStatistics.data.layup_mid_open_shoot;
			}
			GameMsgSender.SendLayupShoot(m_player, uBallId, m_layupRate.ToUnity2(), m_skillArea, solution.m_bSuccess, (uint)solution.m_id, fFlyTime.ToUnity2(), bOpen);

			Debugger.Instance.m_steamer.message = " Final shoot rate: " + m_layupRate;
		}

		m_bRateFixed = true;
		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eLayup, (System.Object)bOpen );

		if( bShootOut )
		{
			AudioClip clip = AudioManager.Instance.GetClip("Misc/3point_01");
			if( clip != null )
				AudioManager.Instance.PlaySound(clip);
		}
	}

	public override void OnExit ()
	{
		base.OnExit ();
		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision( m_player, false );
	}

}
