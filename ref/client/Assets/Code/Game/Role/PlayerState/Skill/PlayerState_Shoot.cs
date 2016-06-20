using UnityEngine;
using System.Collections.Generic;

using fogs.proto.msg;

public class PlayerState_PrepareToShoot : PlayerState_Skill
{
	public enum ShootState
	{
		ePrepare,
		eShoot,
	}

	public bool		mCanShoot;

	public SkillInstance mCachedShootSkill = null;

	public PlayerState_PrepareToShoot (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.ePrepareToShoot;
	}

	public void OnPrepareToShoot()
	{
		if(m_player.m_bForceShoot || mCanShoot)
		{
			m_player.m_toSkillInstance = mCachedShootSkill;
			m_player.m_StateMachine.SetState(State.eShoot);
			m_player.m_toSkillInstance = null;
		}
        else
        {
            m_player.animMgr.SetSpeed(-IM.Number.one);
        }
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		if(lastState.m_eState == State.eRun || lastState.m_eState == State.eRush)
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.RunToShoot);

		PlayerAnimAttribute.AnimAttr shootAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Shoot, m_curAction);
		if( shootAttr == null )
		{
			Logger.LogError("Current action: " + m_curAction + " in shoot id: " + m_curExecSkill.skill.id);
		}

		if( shootAttr.GetKeyFrame("RotateToBasket") != null )
		{
            IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
			IM.Number fTimeFromRotateToBasket = shootAttr.GetKeyFrame("RotateToBasket").frame / frameRate;
            IM.Vector3 dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
			//m_player.m_rootMotion.m_dirMove = dirPlayerToBasket;

			IM.Number fAngle = IM.Vector3.Angle(m_player.forward, dirPlayerToBasket);
            m_turningSpeed = IM.Math.Deg2Rad(fAngle) / fTimeFromRotateToBasket;
			m_rotateTo = RotateTo.eBasket;
			m_rotateType = RotateType.eSmooth;
		}

        m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();

		mCanShoot = true;

		mCachedShootSkill = m_curExecSkill;

		m_skillArea = m_match.mCurScene.mGround.GetArea(m_player);
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone();
		if( !mCanShoot && m_player.m_moveType == MoveType.eMT_Stand)
			m_stateMachine.SetState(PlayerState.State.eHold);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if(mCanShoot )
		{
			if( (m_player.m_toSkillInstance == null || m_player.m_toSkillInstance.skill.action_type != (uint)Command.Shoot)
			   && !m_player.m_bForceShoot)
			{
				mCanShoot = false;
			}
			m_curExecSkill = m_player.m_toSkillInstance;
		}
	}
	
	public override void OnExit ()
	{
		base.OnExit();

		m_player.m_bForceShoot = false;
		mCachedShootSkill = null;
	}
}

public class PlayerState_Shoot : PlayerState_Skill
{
	static HedgingHelper shootNearHedging = new HedgingHelper("ShootNear");
	static HedgingHelper shootMiddleHedging = new HedgingHelper("ShootMiddle");
	static HedgingHelper shootFarHedging = new HedgingHelper("ShootFar");

    static IM.BigNumber shootFarMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "ShootFar");
    static IM.BigNumber shootMiddleMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "ShootMiddle");
    static IM.BigNumber shootNearMultiply = GameSystem.Instance.HedgingConfig.GetRatio("multiply", "ShootNear");
    static IM.BigNumber shootFarAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "ShootFar");
    static IM.BigNumber shootMiddleAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "ShootMiddle");
    static IM.BigNumber shootNearAdd = GameSystem.Instance.HedgingConfig.GetRatio("add", "ShootNear");

	public PlayerState_Shoot (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eShoot;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		bool bFromPrepareShoot = lastState.m_eState == State.ePrepareToShoot;
		m_skillArea = m_match.mCurScene.mGround.GetArea(m_player);

		if( !bFromPrepareShoot )
		{
			if(lastState.m_eState == State.eRun || lastState.m_eState == State.eRush)
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.RunToShoot);
				
            IM.RootMotion rootMotion = m_player.animMgr.GetRootMotion(m_curAction);
			rootMotion.Reset();
            IM.Vector3 dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
			rootMotion.dirMove = dirPlayerToBasket;
		}

        if (m_curAction.Length == 0)
            Logger.Log("no shoot animation.");
        else
            m_player.animMgr.Play(m_curAction, true);

		m_ball.m_shootArea = (Area)m_curExecSkill.skill.area[0];
		m_ball.m_ballState = BallState.eUseBall_Shoot;

		m_player.mStatistics.ReadyToCountShoot();

		PlayerAnimAttribute.AnimAttr shootAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Shoot, m_curAction);

		int iPrepareFrame = 0;
		PlayerAnimAttribute.KeyFrame prepareFrame = shootAttr.GetKeyFrame("OnPrepareToShoot");
		if( prepareFrame != null )
			iPrepareFrame = prepareFrame.frame;

        IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
		m_player.m_blockable.Init(shootAttr, frameRate, iPrepareFrame);

		if( m_skillArea == Area.eFar )
			++m_player.mStatistics.data.far_shoot;
		else if (m_skillArea == Area.eNear)
			++m_player.mStatistics.data.near_shoot;
		else if (m_skillArea == Area.eMiddle)
			++m_player.mStatistics.data.mid_shoot;
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);
		m_player.m_blockable.Update(m_time);
	}

	public override void OnExit ()
	{
		base.OnExit();
		m_player.m_bForceShoot = false;

		if (m_player.m_InfoVisualizer != null && m_player.m_InfoVisualizer.m_strengthBar != null)
            m_player.shootStrength.End();
	}

    public void BeginShoot(uint frameToShoot)
    {
		if (m_player.m_InfoVisualizer != null && m_player.m_InfoVisualizer.m_strengthBar != null && m_player == m_match.m_mainRole )
        {
            IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
            IM.Number seconds = frameToShoot / frameRate;
            m_player.shootStrength.total_seconds = seconds;
            m_player.shootStrength.Begin(); 
        }
    }

	public void OnShoot()
	{
		bool bOpen = false;
		if(m_player.m_bWithBall )
		{
			IM.Number rate_adjustment = IM.Number.zero;
			if (m_player.m_InfoVisualizer != null && m_player.m_InfoVisualizer.m_strengthBar != null)
			{
				m_player.shootStrength.Stop();
				rate_adjustment = m_player.shootStrength.rate_adjustment;
			}

            IM.Vector3 target = m_basket.m_vShootTarget;
			IM.BigNumber fShootRate = IM.BigNumber.one;
			Dictionary<string, uint> data = m_player.m_finalAttrs;

			Player disturber = null;
			if (m_player.m_defenseTarget != null)
			{
				foreach (Player defenser in m_player.m_defenseTarget.m_team.members)
				{
					if (m_player.m_AOD.GetStateByPos(defenser.position) == AOD.Zone.eInvalid)
						continue;
					disturber = defenser;
					break;
				}
			}

			IM.Number fSideEffect = IM.Number.zero;
			SkillSideEffect effect;
			if( !m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eShootRate, out effect) )
				Logger.Log("No side effect data.");
			else
				fSideEffect = effect.value;

			IM.Number fDisturb = IM.Number.zero;
			if( disturber != null )
			{
				Dictionary<string, uint> disturberData = disturber.m_finalAttrs;
				uint disturb = disturberData["disturb"];
				uint disturb_attr = 0;
				disturber.m_skillSystem.HegdingToValue("addn_disturb", ref disturb_attr);
				disturb += disturb_attr;
				
				uint fAntiDisturb = data["anti_disturb"];
				uint anti_disturb_attr = 0;
				m_player.m_skillSystem.HegdingToValue("addn_anti_disturb", ref anti_disturb_attr);
                fAntiDisturb += anti_disturb_attr;

				if (m_skillArea == Area.eFar)
				{
                    fDisturb = shootFarHedging.Calc(new IM.Number((int)disturb), new IM.Number((int)fAntiDisturb));
				}
				else if (m_skillArea == Area.eNear)
				{
                    fDisturb = shootNearHedging.Calc(new IM.Number((int)disturb), new IM.Number((int)fAntiDisturb));
				}
				else if (m_skillArea == Area.eMiddle)
				{
                    fDisturb = shootMiddleHedging.Calc(new IM.Number((int)disturb), new IM.Number((int)fAntiDisturb));
				}

                if (disturber.m_StateMachine.m_curState.m_eState == PlayerState.State.eDefense)
                    fDisturb *= IM.Number.one;
                else if (disturber.m_StateMachine.m_curState.m_eState == PlayerState.State.eBlock)
                    fDisturb *= IM.Number.one;
                else
                {
                    fDisturb = IM.Number.zero;
                    //Logger.Log("Can't disturb, state: " + disturber.m_StateMachine.m_curState.m_eState);
                }
			}
			Debugger.Instance.m_steamer.message = " Disturb: " + fDisturb + " ";

            IM.Number fDistToTarget = GameUtils.HorizonalDistance(m_player.position, m_basket.m_vShootTarget);
			Debugger.Instance.m_steamer.message += "Distance to basket: " + fDistToTarget;
            uint uShootData = 0;
            uint uShootSkill = 0;
            uint uShootFarData = 0;

			if( m_skillArea == Area.eFar )
			{
                data.TryGetValue("shoot_far_dist", out uShootFarData);
                IM.Number maxDistance = uShootFarData * new IM.Number(0,005) + new IM.Number(9,150);
				//long distance influent
				IM.Number fLongDistanceInfluent = IM.Number.one;
				if( fDistToTarget > maxDistance)
					fLongDistanceInfluent = new IM.Number(0,100);
				IM.Number reduceScale = m_match.GetAttrReduceScale("shoot_far", m_player);
                data.TryGetValue("shoot_far", out uShootData);
				m_player.m_skillSystem.HegdingToValue("shoot_far", ref uShootSkill);
				//m_player.m_skillSystem.GetAttrValueByName("shoot_far", ref uShootSkill);

				fShootRate = ((uShootData + uShootSkill) * reduceScale * shootFarMultiply + shootFarAdd) * (IM.Number.one - fDisturb) * fLongDistanceInfluent + fSideEffect;
			}
			else if (m_skillArea == Area.eNear)
			{
				IM.Number reduceScale = m_match.GetAttrReduceScale("shoot_near", m_player);
                data.TryGetValue("shoot_near", out uShootData);
				m_player.m_skillSystem.HegdingToValue("shoot_near", ref uShootSkill);
				//m_player.m_skillSystem.GetAttrValueByName("shoot_near", ref uShootSkill);

				fShootRate = ((uShootData + uShootSkill) * shootNearMultiply + shootNearAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
			}
			else if (m_skillArea == Area.eMiddle)
			{
                IM.Number reduceScale = m_match.GetAttrReduceScale("shoot_middle", m_player);
                data.TryGetValue("shoot_middle", out uShootData);
				m_player.m_skillSystem.HegdingToValue("shoot_middle", ref uShootSkill);
				//m_player.m_skillSystem.GetAttrValueByName("shoot_middle", ref uShootSkill);
				
				fShootRate = ((uShootData + uShootSkill) * shootMiddleMultiply + shootMiddleAdd) * reduceScale * (IM.Number.one - fDisturb) + fSideEffect;
			}
			fShootRate = IM.Math.Max(fShootRate + rate_adjustment, IM.Number.zero);

			SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eShoot_rate);
            if (skillSpec.paramOp == SkillSpecParamOp.eAdd)
                fShootRate += skillSpec.value;
            else if (skillSpec.paramOp == SkillSpecParamOp.eMulti)
                fShootRate *= skillSpec.value;

			fShootRate = m_match.AdjustShootRate(m_player, (IM.Number)fShootRate);
			//bool sumValue = false;
			//if (!m_ball.m_bBlockSuccess)
			//	sumValue = m_stateMachine.attackRandom.AdjustRate(ref fShootRate, m_match.GetScore(m_skillArea == Area.eFar ? 3 : 2));


          

			//if (!m_ball.m_bBlockSuccess && solution.m_bSuccess && sumValue)
			//	m_stateMachine.attackRandom.SumValue(m_match.GetScore(m_skillArea == Area.eFar ? 3 : 2));
			bOpen = _CheckOpenShoot();
			if( bOpen )
			{
                fShootRate *= new IM.Number(1, 300);
				fShootRate = IM.Math.Min(fShootRate, IM.Number.one);
				if( m_skillArea == Area.eFar )
					++m_player.mStatistics.data.far_open_shoot;
				else if (m_skillArea == Area.eNear)
					++m_player.mStatistics.data.near_open_shoot;
				else if (m_skillArea == Area.eMiddle)
					++m_player.mStatistics.data.mid_open_shoot;
			}

			if( m_match.m_bTimeUp )
				fShootRate = IM.Number.zero;

			Debug.LogWarning("shoot rate==>>>" + fShootRate.ToString());
			ShootSolution solution;
			if (m_ball.m_bBlockSuccess && m_ball.m_shootSolution != null)
				solution = m_ball.m_shootSolution;
			else
				solution = m_match.GetShootSolution(m_match.mCurScene.mBasket, m_skillArea, m_player, fShootRate, ShootSolution.Type.Shoot);

			m_ball.m_shootSolution = solution;
			Logger.Log("shoot ball success: " + solution.m_bSuccess );

			IM.Vector3 vPos = m_ball.position;
			uint ballId = m_ball.m_id;
			UBasketball curBall = m_ball;
			m_player.DropBall(curBall);
			curBall.position = vPos;
			curBall.OnShoot(m_player, m_skillArea, false);

			ShootSolution.SShootCurve shootCurve = curBall.m_shootSolution.m_ShootCurveList[0];
			IM.Number fFlyTime = shootCurve.fTime;

			curBall.m_castedSkill = m_curExecSkill;
			
			Debugger.Instance.m_steamer.message = " Final shoot rate: " + fShootRate;
		}

		m_player.eventHandler.NotifyAllListeners( PlayerActionEventHandler.AnimEvent.eShoot, (System.Object)bOpen );

		if( m_skillArea == Area.eFar )
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Shot3Pt);
		else
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Shot2Pt);

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Shoot);
	}
}
