using UnityEngine;
using System.Collections.Generic;

public class PlayerState_Block : PlayerState_Skill
{
	public enum FailReason
	{
		None,
		NoShooter,
		InvalidArea,
		TooEarly,
		TooLate,
		InvalidBallShotState,
		Random,
	}

	public const int idBlockGrabBall = 3600;
	public const int idBlockPassBall = 3300;

	public 	bool			m_success = false;
	public FailReason m_failReason { get; private set; }
	public	ShootSolution	m_failedShootSolution;
	//public	Vector3			m_blockedBallDir;
	public	LostBallContext	m_loseBallContext = new LostBallContext();

	public	IM.Vector3			m_blockedMoveVel;

	private IM.Number 	m_heightScale = IM.Number.one;

	private bool	m_bFailDown = false;

	PseudoRandomGroup random = new PseudoRandomGroup();
	static HedgingHelper shootNearHedging = new HedgingHelper("BlockShootNear");
	static HedgingHelper shootMiddleHedging = new HedgingHelper("BlockShootMiddle");
	static HedgingHelper shootFarHedging = new HedgingHelper("BlockShootFar");
	static HedgingHelper layupNearHedging = new HedgingHelper("BlockLayupNear");
	static HedgingHelper layupMiddleHedging = new HedgingHelper("BlockLayupMiddle");
	static HedgingHelper dunkNearHedging = new HedgingHelper("BlockDunkNear");
	static HedgingHelper dunkMiddleHedging = new HedgingHelper("BlockDunkMiddle");

	public PlayerState_Block (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBlock;
	}

	public static bool InBlockArea(Player shooter, Player blocker, IM.Vector3 vNet)
	{
		IM.Number fDistance = GameUtils.HorizonalDistance(shooter.position, blocker.position);

		List<SkillInstance> blocks = blocker.m_skillSystem.GetBasicSkillsByCommand(Command.Block);

		SkillSpec skillSpec = blocker.GetSkillSpecialAttribute(SkillSpecParam.eBlock_back_dist, blocks[0]);
		if( fDistance < skillSpec.value )
			return true;
		skillSpec = blocker.GetSkillSpecialAttribute(SkillSpecParam.eBlock_front_dist, blocks[0]);
		if( fDistance > skillSpec.value)
			return false;
		IM.Vector3 dirShootToNet 		= GameUtils.HorizonalNormalized( vNet, shooter.position );
		IM.Vector3 dirShootToBlocker 	= GameUtils.HorizonalNormalized( blocker.position, shooter.position );
		return IM.Vector3.Angle(dirShootToNet, dirShootToBlocker) < new IM.Number(90);
	}

    bool _CalcBlockShoot(Player shooter, out bool bBlockable, out bool bBlockInRange, out IM.Number fBlockRate, out IM.Number fBlockValue, out IM.Vector3 attackerPos, out IM.Vector3 vBallDir, out bool bValid)
	{
		bBlockable = false;
		attackerPos = IM.Vector3.zero;
		fBlockRate = IM.Number.zero;
		fBlockValue = IM.Number.zero;
		bBlockInRange = false;
		vBallDir = GetBallVelocity();
		bValid = false;

		if (shooter == null)
		{
			Logger.Log("Block failed, shooter is null.");
			m_failReason = FailReason.NoShooter;
			return false;
		}

		attackerPos = shooter.position;

		if( !shooter.m_blockable.blockable )
		{
			if (shooter.m_blockable.tooEarly)
				m_failReason = FailReason.TooEarly;
			else if (shooter.m_blockable.tooLate)
				m_failReason = FailReason.TooLate;
			Debugger.Instance.m_steamer.message = "Out of block range.";
			return false;
		}
		bBlockable = true;

		/*
		AOD.Zone zone = shooter.m_AOD.GetStateByPos(m_player.position);
		if( zone == AOD.Zone.eInvalid )
		{
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			return false;
		}
		*/
        if (!InBlockArea(shooter, m_player, m_basket.m_vShootTarget))
		{
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			m_failReason = FailReason.InvalidArea;
			return false;
		}

		bBlockInRange = true;

		Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = m_player.m_animAttributes.m_block;
		int blockKey = blocks[m_player.animMgr.GetOriginName(m_curAction)].GetKeyFrame("OnBlock").frame;
        IM.Number fEventBlockTime = blockKey / m_player.animMgr.GetFrameRate(m_curAction);
		
		Dictionary<string, PlayerAnimAttribute.AnimAttr> shootAnims = shooter.m_animAttributes.m_shoot;
		SkillInstance shooterSkill = shooter.m_StateMachine.m_curState.m_curExecSkill;
		string shoot_id = shooter.m_skillSystem.ParseAction(shooterSkill.curAction.action_id, shooterSkill.matchedKeyIdx, Command.Shoot);

		int shootOutKey = shootAnims[shooter.animMgr.GetOriginName(shoot_id)].GetKeyFrame("OnShoot").frame;
        IM.Number fEventShootOutTime = shootOutKey / shooter.animMgr.GetFrameRate(shoot_id);
        IM.Number fShootEclipseTime = shooter.animMgr.curPlayInfo.time;
		if (fShootEclipseTime > fEventShootOutTime)
		{
			Logger.Log("PlayerState_Block: block failed, ball has been shot.");
			m_failReason = FailReason.InvalidBallShotState;
			return false;
		}

		IM.Number fBallFlyTime = fEventBlockTime - (fEventShootOutTime - fShootEclipseTime);
		if (fBallFlyTime < IM.Number.zero)
		{
			Logger.Log("PlayerState_Block: block failed, ball will not be shot when block time.");
			m_failReason = FailReason.InvalidBallShotState;
			return false;
		}

        IM.Number fSideEffect = IM.Number.one;
		SkillSideEffect effect;
		if( !m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eBlockRate, out effect) )
			Logger.Log("No side effect data.");
		else
            fSideEffect = effect.value;

		Dictionary<string, uint> data = shooter.m_finalAttrs;

		uint uBlockSkill = 0;
		m_player.m_skillSystem.HegdingToValue("addn_block", ref uBlockSkill);

		IM.Number blockAttr = new IM.Number((int)(m_player.m_finalAttrs["block"] + uBlockSkill));
		IM.Number reduceScale = m_match.GetAttrReduceScale("block", m_player);
		blockAttr *= reduceScale;
		if( (Command)shooterSkill.skill.action_type == Command.Shoot )
		{
			uint anti_block = 0;
			shooter.m_skillSystem.HegdingToValue("addn_anti_block", ref anti_block);

			IM.Number fAntiBlock = new IM.Number((int)(data["anti_block"] + anti_block));

			switch(shooterSkill.skill.area[0])
			{
			case 3:		//near
                fBlockRate = shootNearHedging.Calc(blockAttr, fAntiBlock);
				break;
			case 2:		//middle
                fBlockRate = shootMiddleHedging.Calc(blockAttr, fAntiBlock);
				break;
			case 1:		//far
				fBlockRate = shootFarHedging.Calc(blockAttr, fAntiBlock);
				break;
			default:
				break;
			}
			fBlockRate += fSideEffect;
		}

		SkillSpec skillSpc = shooter.GetSkillSpecialAttribute(SkillSpecParam.eShoot_antiBlock);
        if (skillSpc.paramOp == SkillSpecParamOp.eAdd)
            fBlockRate += skillSpc.value;
        else if (skillSpc.paramOp == SkillSpecParamOp.eMulti)
            fBlockRate *= skillSpc.value;

		SkillSpec skill = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBlock_rate);
        if (skill.paramOp == SkillSpecParamOp.eAdd)
            fBlockRate += skill.value;
        else if (skill.paramOp == SkillSpecParamOp.eMulti)
            fBlockRate *= skill.value;

		fBlockRate = m_match.AdjustBlockRate(shooter, m_player, fBlockRate);
		bool sumValue = random[shooter].AdjustRate(ref fBlockRate);

		bValid = true;

		if( shooterSkill.curAction.block_key != null )
		{
			Logger.Log("m_curExecSkill.curInput.moveDir: " + m_curExecSkill.curInput.moveDir);
			Logger.Log("shooterSkill.curAction.block_key.moveDir: " + shooterSkill.curAction.block_key.moveDir);

			if( m_curExecSkill.curInput.moveDir == shooterSkill.curAction.block_key.moveDir )
			{
				fBlockRate *= GlobalConst.MATCHED_KEY_BLOCK_RATE_ADJUST;
				Logger.Log("Match block key.");
			}
		}

		fBlockValue = IM.Random.value;
		if (fBlockValue > fBlockRate)
		{
			m_failReason = FailReason.Random;
			return false;
		}

		if (sumValue)
			random[shooter].SumValue();

		return true;
	}

	void _BeginBlockShoot(ShootSolution failedShootSolution, Player shooter, out IM.Vector3 ballPos)
	{
		ballPos = IM.Vector3.zero;

		m_ball.m_shootSolution = failedShootSolution;
		/*
		if( m_ball.m_shootSolution.m_bSuccess )
		{
			Logger.LogError("block shoot success, but ball goals in.");
			return;
		}
		*/

		Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = m_player.m_animAttributes.m_block;
		int blockKey = blocks[m_player.animMgr.GetOriginName(m_curAction)].GetKeyFrame("OnBlock").frame;
        IM.Number fEventBlockTime = blockKey / m_player.animMgr.GetFrameRate(m_curAction);

		SkillInstance shooterSkill = shooter.m_StateMachine.m_curState.m_curExecSkill;
		Dictionary<string, PlayerAnimAttribute.AnimAttr> shootAnims = shooter.m_animAttributes.m_shoot;
		string shoot_id = shooter.m_skillSystem.ParseAction(shooterSkill.curAction.action_id, shooterSkill.matchedKeyIdx, Command.Shoot);

		int shootOutKey = shootAnims[shooter.animMgr.GetOriginName(shoot_id)].GetKeyFrame("OnShoot").frame;
        IM.Number fEventShootOutTime = shootOutKey / shooter.animMgr.GetFrameRate(shoot_id);
        IM.Number fShootEclipseTime = shooter.animMgr.curPlayInfo.time;
		if( fShootEclipseTime > fEventShootOutTime )
			fShootEclipseTime = fEventShootOutTime;
		
		IM.Number fBallFlyTime = fEventBlockTime - (fEventShootOutTime - fShootEclipseTime);
		if( fBallFlyTime < IM.Number.zero )
			fBallFlyTime = IM.Number.zero;

        IM.Vector3 ballPos1;
		m_player.GetNodePosition(SampleNode.Ball, shoot_id, fEventShootOutTime, out ballPos1);
        m_ball.m_shootSolution.m_vStartPos = ballPos1;

		IM.Vector3 vBallPosBlock;
		if( m_ball.GetPositionInAir(fBallFlyTime, out vBallPosBlock) )
		{

			IM.Vector3 tempblockAnimBall;
			m_player.GetNodePosition(SampleNode.Ball, m_curAction, fEventBlockTime, out tempblockAnimBall);
			IM.Vector3 temproot;
			m_player.GetNodePosition(SampleNode.Root, m_curAction, fEventBlockTime, out temproot);

            IM.Vector3 blockAnimBall = tempblockAnimBall;
            IM.Vector3 root = temproot;

			IM.Vector3 deltaPos2Root = blockAnimBall - root;
			deltaPos2Root.y = IM.Number.zero;

			m_heightScale = vBallPosBlock.y / (blockAnimBall.y * m_player.scale.y);
			IM.Vector3 rootPosWhenBlock = vBallPosBlock - deltaPos2Root;
			IM.Vector3 dirJump = (rootPosWhenBlock - m_player.position) / fEventBlockTime;
			dirJump.y = IM.Number.zero;

			//if( !bTurnBack )
			m_speed = dirJump;
			//else
			//	m_speed = Vector3.zero;

			m_ball.m_bBlockSuccess = true;

            ballPos = vBallPosBlock;
		}
		else
		{
			Logger.LogError("reboundTime out of the curve, too slow");
			return;
		}
	}

    bool _CalcBlockLayup(Player shooter, out bool bBlockable, out bool bBlockInRange, out IM.Number fBlockRate, out IM.Number fBlockValue, out IM.Vector3 attackerPos, out IM.Vector3 vBallDir, out bool bValid)
	{
		bBlockable = false;
		attackerPos = IM.Vector3.zero;
		fBlockRate = IM.Number.zero;
        fBlockValue = IM.Number.zero;
		bBlockInRange = false;
		vBallDir = GetBallVelocity();
		bValid = false;

		if (shooter == null)
		{
			m_failReason = FailReason.NoShooter;
			return false;
		}
		
		attackerPos = shooter.position;
		
		if( !shooter.m_blockable.blockable )
		{
			Debugger.Instance.m_steamer.message = "Out of block range.";
			Logger.Log("Out of block range.");
			if (shooter.m_blockable.tooEarly)
				m_failReason = FailReason.TooEarly;
			else if (shooter.m_blockable.tooLate)
				m_failReason = FailReason.TooLate;
			return false;
		}
		bBlockable = true;

		/*
		AOD.Zone zone = shooter.m_AOD.GetStateByPos(m_player.position);
		if( zone == AOD.Zone.eInvalid )
		{
			bBlockInRange = false;
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			return false;
		}
*/

        if (!InBlockArea(shooter, m_player, m_basket.m_vShootTarget))
		{
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			m_failReason = FailReason.InvalidArea;
			return false;
		}


		bBlockInRange = true;

        IM.Number fSideEffect = IM.Number.zero;
		SkillSideEffect effect;
		if( !m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eBlockRate, out effect) )
			Logger.Log("No side effect data.");
		else
			fSideEffect = effect.value;
		
		Dictionary<string, uint> data = shooter.m_finalAttrs;
		Dictionary<string, uint> shooterSkillAttr = shooter.GetSkillAttribute();

        IM.Number blockAttr = new IM.Number((int)m_player.m_finalAttrs["block"]);
        IM.Number reduceScale = m_match.GetAttrReduceScale("block", m_player);
		blockAttr *= reduceScale;
		SkillInstance shooterSkill = shooter.m_StateMachine.m_curState.m_curExecSkill;
		{
			if( (Command)shooterSkill.skill.action_type == Command.Layup )
			{
				uint anti_block = 0;
				shooter.m_skillSystem.HegdingToValue("addn_anti_block", ref anti_block);

                IM.Number fAntiBlock = new IM.Number((int)(data["anti_block"] + anti_block));
				switch(shooterSkill.skill.area[0])
				{
				case 3:		//near
                    fBlockRate = layupNearHedging.Calc(blockAttr, fAntiBlock);
					break;
				case 2:		//middle
                    fBlockRate = layupMiddleHedging.Calc(blockAttr, fAntiBlock);
					break;
				default:
					break;
				}

				fBlockRate += fSideEffect;
			}
		}

		SkillSpec skill = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBlock_rate);
		if( skill.paramOp == SkillSpecParamOp.eAdd )
            fBlockRate += skill.value;
		else if( skill.paramOp == SkillSpecParamOp.eMulti )
            fBlockRate *= skill.value;

		SkillSpec skillSpc = shooter.GetSkillSpecialAttribute(SkillSpecParam.eLayup_antiBlock);
		if( skillSpc.paramOp == SkillSpecParamOp.eAdd )
            fBlockRate += skillSpc.value;
		else if( skillSpc.paramOp == SkillSpecParamOp.eMulti )
            fBlockRate *= skillSpc.value;

		fBlockRate = m_match.AdjustBlockRate(shooter, m_player, fBlockRate);

		bool sumValue = random[shooter].AdjustRate(ref fBlockRate);
		bValid = true;
		
		fBlockValue = IM.Random.value;
		if (fBlockValue > fBlockRate)
		{
			m_failReason = FailReason.Random;
			return false;
		}

		if (sumValue)
			random[shooter].SumValue();
		
		return true;
	}

	bool _BeginBlockLayup(ShootSolution failedShootSolution, Player shooter, out IM.Vector3 ballPos)
	{
		m_ball.m_shootSolution = failedShootSolution;
		ballPos = IM.Vector3.zero;

		Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = m_player.m_animAttributes.m_block;
		int blockKey = blocks[m_player.animMgr.GetOriginName(m_curAction)].GetKeyFrame("OnBlock").frame;
		IM.Number fEventBlockTime = blockKey / m_player.animMgr.GetFrameRate(m_curAction);

		SkillInstance shooterSkill = shooter.m_StateMachine.m_curState.m_curExecSkill;
		string shoot_id = shooter.m_skillSystem.ParseAction(shooterSkill.curAction.action_id, shooterSkill.matchedKeyIdx, Command.Layup);

		PlayerAnimAttribute.AnimAttr shootAnims = shooter.m_animAttributes.GetAnimAttrById(Command.Layup, shoot_id);
		int shootOutKey = shootAnims.GetKeyFrame("OnLayupShot").frame;
        IM.Number fEventShootOutTime = shootOutKey / shooter.animMgr.GetFrameRate(shoot_id);
        IM.Number fShootEclipseTime = shooter.animMgr.curPlayInfo.time;
        IM.Number fPlayerMovingTime = fEventShootOutTime - fShootEclipseTime;
        IM.Number fBallFlyTime = fEventBlockTime - fPlayerMovingTime;

		IM.Vector3 vRootBegin, vRootBlock, vRootShoot;
		m_player.GetNodePosition(SampleNode.Root, shoot_id, fShootEclipseTime, out vRootBegin);
		m_player.GetNodePosition(SampleNode.Root, shoot_id, fShootEclipseTime + fEventBlockTime, out vRootBlock);
		m_player.GetNodePosition(SampleNode.Root, shoot_id, fEventShootOutTime, out vRootShoot);
		
		IM.Vector3 playerPosWhenLayup = shooter.position + vRootShoot - vRootBegin;
		IM.Vector3 playerPosWhenBlock = shooter.position + vRootBlock - vRootBegin;
		//ball still in player hand
		if( fBallFlyTime < IM.Number.zero )
		{
			IM.Vector3 dirJump = (playerPosWhenBlock - m_player.position) / fEventBlockTime;
            dirJump.y = IM.Number.zero; 
			m_speed = dirJump;

			IM.Vector3 vBallPosWhenLayup;
			m_player.GetNodePosition(SampleNode.Ball, shoot_id, fShootEclipseTime + fEventBlockTime, out vBallPosWhenLayup);
			ballPos = vBallPosWhenLayup;
		}
		else
		{
			IM.Vector3 vBallPosWhenLayup;
			m_player.GetNodePosition(SampleNode.Ball, shoot_id, fEventShootOutTime, out vBallPosWhenLayup);
			IM.Vector3 delta = vBallPosWhenLayup - vRootShoot;
			vBallPosWhenLayup = playerPosWhenLayup + delta;
			//Debugger.Instance.DrawSphere("vBallPosWhenLayup", vBallPosWhenLayup, Color.red);

			m_ball.m_shootSolution = GameSystem.Instance.shootSolutionManager.GetShootSolution(m_basket.m_rim.center, playerPosWhenLayup, false);
			m_ball.m_shootSolution.m_vStartPos = vBallPosWhenLayup;

			IM.Vector3 vBallPosBlock;
			if( !m_ball.GetPositionInAir( fBallFlyTime, out vBallPosBlock) )
			{
				Debugger.Instance.m_steamer.message = "can not block, not shoot curve info.";
				return false;
			}
			ballPos = vBallPosBlock;

			IM.Vector3 blockAnimBall;
			m_player.GetNodePosition(SampleNode.Ball, m_curAction, fEventBlockTime, out blockAnimBall);
			IM.Vector3 root;
			m_player.GetNodePosition(SampleNode.Root, m_curAction, fEventBlockTime, out root);

			m_heightScale = vBallPosBlock.y /(blockAnimBall.y * m_player.scale.y);
			IM.Vector3 deltaPos2Root = blockAnimBall - root;
			deltaPos2Root.y = IM.Number.zero;
			IM.Vector3 rootPosWhenBlock = vBallPosBlock - deltaPos2Root;
			rootPosWhenBlock.y = IM.Number.zero;

			IM.Vector3 dirJump = (rootPosWhenBlock - m_player.position) / fEventBlockTime;
			m_speed = dirJump;
		}

		m_ball.m_bBlockSuccess = true;

		return true;
	}

	bool _CalcBlockDunk(Player shooter, out bool bBlockable, out bool bBlockInRange, out IM.Number fBlockRate, out IM.Number fBlockValue, out IM.Vector3 attackerPos, out IM.Vector3 vBallDir, out bool bValid)
	{
		bBlockable = false;
		attackerPos = IM.Vector3.zero;
		fBlockRate = IM.Number.zero;
		fBlockValue = IM.Number.zero;
		bBlockInRange = false;
		bValid = false;
		//vBallDir = GameUtils.HorizonalNormalized( shooter.position, m_player.position );
		vBallDir = GetBallVelocity();

		Player dunker = m_ball.m_owner;
		IM.Vector3 dirDunker2Player = m_player.position - dunker.position;
		dirDunker2Player.y = IM.Number.zero;

		if( !dunker.m_blockable.blockable )
		{
			Debugger.Instance.m_steamer.message = "Out of block range.";
			if (dunker.m_blockable.tooEarly)
				m_failReason = FailReason.TooEarly;
			else if (dunker.m_blockable.tooLate)
				m_failReason = FailReason.TooLate;
			return false;
		}

		/*
		AOD.Zone zone = shooter.m_AOD.GetStateByPos(m_player.position);
		if( zone == AOD.Zone.eInvalid )
		{
			bBlockInRange = false;
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			return false;
		}
		*/
        if (!InBlockArea(shooter, m_player, m_basket.m_vShootTarget))
		{
			Debugger.Instance.m_steamer.message = "Block failed, not in AOD";
			m_failReason = FailReason.InvalidArea;
			return false;
		}
		bBlockInRange = true;

		Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = m_player.m_animAttributes.m_block;
		int blockKey = blocks[m_curAction].GetKeyFrame("OnBlock").frame;
		IM.Number fEventBlockTime = blockKey / m_player.animMgr.GetFrameRate(m_curAction);
		SkillInstance dunkSkill = dunker.m_StateMachine.m_curState.m_curExecSkill;
		string dunk_id = dunker.m_skillSystem.ParseAction(dunkSkill.curAction.action_id, dunkSkill.matchedKeyIdx, Command.Dunk);
		
		PlayerAnimAttribute.AnimAttr dunkAnims = dunker.m_animAttributes.GetAnimAttrById(Command.Dunk, dunk_id);
		int dunkInKey = dunkAnims.GetKeyFrame("OnDunk").frame;
        IM.Number fEventDunkInTime = dunkInKey / dunker.animMgr.GetFrameRate(dunk_id);

        IM.Number fDunkEclipseTime = dunker.animMgr.curPlayInfo.time;
		IM.Number fDunkFlyTime = fEventDunkInTime - fDunkEclipseTime;
		if( fDunkFlyTime < fEventBlockTime )
		{
			Debugger.Instance.m_steamer.message = "block too late, dunk goal!";
			m_failReason = FailReason.InvalidBallShotState;
			return false;
		}

        IM.Number fSideEffect = IM.Number.zero;
		SkillSideEffect effect;
		if( !m_curExecSkill.skill.side_effects.TryGetValue((int)SkillSideEffect.Type.eBlockRate, out effect) )
			Logger.Log("No side effect data.");
		else
			fSideEffect = effect.value;

		Dictionary<string, uint> data = dunker.m_finalAttrs;
		Dictionary<string, uint> dunkerSkillAttr = dunker.GetSkillAttribute();

		IM.Number blockAttr = new IM.Number((int)(m_player.m_finalAttrs["block"]));
        IM.Number reduceScale = m_match.GetAttrReduceScale("block", m_player);
		blockAttr *= reduceScale;

		if( (Command)dunkSkill.skill.action_type == Command.Dunk )
		{
			uint anti_block = 0;
			shooter.m_skillSystem.HegdingToValue("addn_anti_block", ref anti_block);

            IM.Number fAntiBlock = new IM.Number((int)(data["anti_block"] + anti_block));
			switch(dunkSkill.skill.area[0])
			{
			case 3:	//near
				fBlockRate = dunkNearHedging.Calc(blockAttr, fAntiBlock);
				break;
			case 2:	//middle
				fBlockRate = dunkMiddleHedging.Calc(blockAttr, fAntiBlock);
				break;
			}

			fBlockRate += fSideEffect;
		}

		SkillSpec skillSpc = shooter.GetSkillSpecialAttribute(SkillSpecParam.eDunk_antiBlock);
        if (skillSpc.paramOp == SkillSpecParamOp.eAdd)
            fBlockRate += skillSpc.value;
        else if (skillSpc.paramOp == SkillSpecParamOp.eMulti)
            fBlockRate *= skillSpc.value;

		skillSpc = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBlock_rate);
		if( skillSpc.paramOp == SkillSpecParamOp.eAdd )
            fBlockRate += skillSpc.value;
		else if( skillSpc.paramOp == SkillSpecParamOp.eMulti )
            fBlockRate *= skillSpc.value;

		fBlockRate = m_match.AdjustBlockRate(shooter, m_player, fBlockRate);

		bool sumValue = random[shooter].AdjustRate(ref fBlockRate);

		fBlockValue = IM.Random.value;
		
		Debugger.Instance.m_steamer.message = "block rate: " + fBlockRate;
		Debugger.Instance.m_steamer.message += "probability: " + fBlockValue;

		bValid = true;

		m_ball.m_bBlockSuccess = fBlockValue < fBlockRate;
		if (!m_ball.m_bBlockSuccess)
			m_failReason = FailReason.Random;

		if (m_ball.m_bBlockSuccess && sumValue)
			random[shooter].SumValue();
							
		return m_ball.m_bBlockSuccess;
	}

	bool _BeginBlockDunk(Player dunker, out IM.Vector3 vBallPos) 
	{
		vBallPos = IM.Vector3.zero;
		SkillInstance dunkSkill = dunker.m_StateMachine.m_curState.m_curExecSkill;

		Dictionary<string, PlayerAnimAttribute.AnimAttr> blocks = m_player.m_animAttributes.m_block;
		int blockKey = blocks[m_curAction].GetKeyFrame("OnBlock").frame;
		IM.Number fEventBlockTime = blockKey / m_player.animMgr.GetFrameRate(m_curAction);
		string dunk_id = dunker.m_skillSystem.ParseAction(dunkSkill.curAction.action_id, dunkSkill.matchedKeyIdx, Command.Dunk);
		
		PlayerAnimAttribute.AnimAttr dunkAnims = dunker.m_animAttributes.GetAnimAttrById(Command.Dunk, dunk_id);
		int dunkInKey = dunkAnims.GetKeyFrame("OnDunk").frame;
		IM.Number fEventDunkInTime = dunkInKey / dunker.animMgr.GetFrameRate(dunk_id);

        IM.Number fDunkEclipseTime = dunker.animMgr.curPlayInfo.time;
        IM.Number fDunkFlyTime = fEventDunkInTime - fDunkEclipseTime;
		if( fDunkFlyTime < fEventBlockTime )
		{
			Debugger.Instance.m_steamer.message = "block too late, dunk goal!";
			return false;
		}
		
		IM.Vector3 vRootBegin, vRootBlock;
		m_player.GetNodePosition(SampleNode.Root, dunk_id, fDunkEclipseTime, out vRootBegin);
		m_player.GetNodePosition(SampleNode.Root, dunk_id, fDunkEclipseTime + fEventBlockTime, out vRootBlock);
		
		IM.Vector3 dunkPosBlocked = dunker.position + vRootBlock - vRootBegin;
		//Vector3 dunkPosBlocked = dunker.m_StateMachine.m_curState.m_speed * fDunkFlyTime + dunker.position;
		IM.Vector3 dunkAnimBall;
		if( !m_player.GetNodePosition(SampleNode.Ball, dunk_id, fDunkEclipseTime + fEventBlockTime, out dunkAnimBall) )
			return false;
		dunkAnimBall.y /= dunker.scale.y;
		dunkPosBlocked.y = dunkAnimBall.y;
		vBallPos = dunkAnimBall;

		IM.Vector3 dirPlayer2Ball = GameUtils.HorizonalNormalized(dunkPosBlocked, m_player.position);
		if(IM.Vector3.Angle(dirPlayer2Ball, m_player.forward) > new IM.Number(90) )
		{
			m_player.FaceTo(dunker.position);
			m_speed = IM.Vector3.zero;
		}
		else
		{
			m_player.FaceTo(dunkPosBlocked);
			m_speed = (dunkPosBlocked - m_player.position) / fEventBlockTime;
            m_speed.y = IM.Number.zero;
		}
		
		m_heightScale = dunkPosBlocked.y / (dunkAnimBall.y * m_player.scale.y); 
		
		return true;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		m_failReason = FailReason.None;

		Player attacker = m_ball.m_actor;
		if( attacker == null )
			attacker = m_ball.m_owner;
	
		//main player
		bool bBlockable = false, bBlockInRange = false;
        IM.Number fBlockRate = IM.Number.zero, fBlockValue = IM.Number.zero;
		IM.Vector3 attackerPos = IM.Vector3.zero;
		bool bValid = false;

        m_success = false;
        IM.Vector3 vBallVel = IM.Vector3.zero;
        IM.Vector3 vBallPos = IM.Vector3.zero;

        if( attacker != null )
        {
            m_player.FaceTo( attacker.position );

            //shooter is blocked.. set a ball solution to him
            m_failedShootSolution = GameSystem.Instance.shootSolutionManager.GetShootSolution(m_basket.m_vShootTarget, attacker.position, false);
            if( m_failedShootSolution == null )
                Logger.LogError("No shoot solution can be set to block.");

            m_bMoveForward 	= false;
            m_speed 		= IM.Vector3.zero;
            m_bFailDown 	= false;

            if( !m_ball.m_bBlockSuccess && m_ball.m_picker == null )
            {
                m_heightScale = IM.Number.one;

                if( m_ball != null && !m_ball.m_bReachMaxHeight && 
                    (m_ball.m_ballState == BallState.eUseBall_Shoot || m_ball.m_ballState == BallState.eUseBall))
                {
                    if(attacker.m_StateMachine.m_curState.m_eState == State.eShoot)
                    {
                        m_success = _CalcBlockShoot(attacker, out bBlockable, out bBlockInRange, out fBlockRate, out fBlockValue, out attackerPos, out vBallVel, out bValid);
                        if( m_success )
                            _BeginBlockShoot(m_failedShootSolution, attacker, out vBallPos);
                    }
                    else if(attacker.m_StateMachine.m_curState.m_eState == State.eLayup)
                    {
                        m_success = _CalcBlockLayup(attacker, out bBlockable, out bBlockInRange, out fBlockRate, out fBlockValue, out attackerPos, out vBallVel, out bValid);
                        if( m_success )
                            _BeginBlockLayup(m_failedShootSolution, attacker, out vBallPos);
                    }
                    else if(attacker.m_StateMachine.m_curState.m_eState == State.eDunk)
                    {
                        m_success = _CalcBlockDunk(attacker, out bBlockable, out bBlockInRange, out fBlockRate, out fBlockValue, out attackerPos, out vBallVel, out bValid);
                        if( m_success )
                            _BeginBlockDunk(attacker, out vBallPos);
                    }
                    else
                    {
                        Logger.Log("Block failed, cur attacker state: " + attacker.m_StateMachine.m_curState.m_eState);
                        m_success = false;
                    }
                    string rateLog = "Block rate: " + fBlockRate + " value: " + fBlockValue;
                    Debugger.Instance.m_steamer.message = rateLog;
                    Logger.Log(rateLog);
                    Logger.Log("block success: " + m_success);
                    Logger.Log("block ball pos: " + vBallPos + ", vel: " + vBallVel);
                    if (!m_success)
                        Logger.Log("Block fail reason: " + m_failReason);

                    m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, m_success);
                }
                else 
                {
                    m_success = false;
                    Logger.Log("496 failed.");
                    m_failReason = FailReason.TooLate;
                }
            }//if( !m_ball.m_bBlockSuccess )
        }

        if( m_curExecSkill.skill.id == idBlockPassBall || m_curExecSkill.skill.id == idBlockGrabBall )
        {
            if( m_ball.m_picker != null || m_ball.m_bGoal )
                m_success = false;
            else
                m_ball.m_picker = m_player;
        }

        if( !m_success )
        {
            m_speed = m_player.forward;
            m_bMoveForward 	= false;

            if( attacker != null && attacker.m_StateMachine.m_curState.m_eState == State.eDunk 
               && GameUtils.HorizonalDistance(m_player.position, attacker.position) < IM.Number.one )
                m_bFailDown = IM.Random.value > IM.Number.half;
        }

        if( !m_success )
        {
            List<SkillInstance> basicBlockSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Block);
            m_curExecSkill = basicBlockSkills[0];
        }
        else
        {
            //m_curExecSkill = _DecideBlockSkill();
            bool bBlockPass = false;
            if( m_curExecSkill.skill.id == idBlockPassBall )
            {
                int value = Random.Range(0,2);
                bBlockPass = true;
                if(value == 0)
                {
                    Player catcher = PassHelper.ChoosePassTarget(m_player);
                    if(catcher != null)
                        m_player.m_passTarget = catcher;
                }
            }
            m_loseBallContext.vInitPos = vBallPos;
            m_loseBallContext.vInitVel = vBallVel;
        }

		m_player.animMgr.Play(m_curAction, false);

		++m_player.mStatistics.data.block_times;
		if (bValid)
			++m_player.mStatistics.data.valid_block_times;
	}

	public override void LateUpdate (IM.Number fDeltaTime)
	{
		//base.LateUpdate (fDeltaTime);

		if( !m_player.m_bOnGround )
		{
            IM.Number curTime = m_player.animMgr.curPlayInfo.time;
			IM.Vector3 pevis;
			m_player.GetNodePosition(SampleNode.Pelvis, m_curAction, curTime, out pevis);
			IM.Number fDeltaHeight = m_player.pelvisPos.y * m_heightScale - pevis.y;
		}
	}

	public void OnBlock()
	{
		if( m_ball == null )
			return;

		if( m_ball.m_bGoal )
			return;

		Player attacker = m_ball.m_actor;
		if( attacker == null )
			attacker = m_ball.m_owner;
		
		if( attacker == null )
			return;

		if( m_bFailDown )
		{
			m_stateMachine.SetState(State.eFallGround);
			return;
		}
		if( !m_success )
			return;

        m_speed *= new IM.Number(0, 100);
		{
            if (GameSystem.Instance.mClient.mInputManager.isNGDS)
            {
                //ÉèÖÃÂí´ïÕð¶¯
                GameSystem.Instance.mClient.mInputManager.sendShock = true;
            }
			GameObject blockEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/Hit_1");
			if( blockEffect != null )
			{
				GameObject goBlockEffect = GameObject.Instantiate(blockEffect) as GameObject;
				goBlockEffect.transform.position = (Vector3)m_player.ballSocketPos;
				goBlockEffect.GetComponent<ParticleSystem>().Play();
			}

			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eBlock);
			if( m_curExecSkill.skill.id == idBlockGrabBall )
			{
				attacker.DropBall(m_ball);
				m_player.GrabBall(m_ball);
				m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePickupBall);
			}
			else
			{
				_ExecBlock(m_loseBallContext, m_curExecSkill.skill.id == idBlockPassBall);
			}

			_BlockToFallDown(attacker);
		}
	}

	IM.Vector3 GetBallVelocity()
	{
		IM.Number power = IM.Random.Range(IM.Number.one, new IM.Number(5));
        IM.Number angle = IM.Random.value < new IM.Number(0, 300) ? IM.Random.Range(-new IM.Number(135), new IM.Number(135)) : IM.Random.Range(new IM.Number(135), new IM.Number(225));
		IM.Vector3 dir = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * m_player.forward;
		dir.y = IM.Number.half;
		Logger.Log("Block velocity: power:" + power + " angle:" + angle);
		return dir * power;
	}

	void _BlockToFallDown(Player attacker)
	{
		//block reaction to attacker

        if( attacker.m_StateMachine.m_curState.m_eState == State.eDunk )
        {
            attacker.m_StateMachine.SetState(PlayerState.State.eFallGround);
        }
        else if( attacker.m_StateMachine.m_curState.m_eState == PlayerState.State.eLayup && IM.Random.value < new IM.Number(0,350) )
        {
            attacker.m_StateMachine.SetState(PlayerState.State.eFallGround);
        }
	}
	
	void _ExecBlock(LostBallContext lostBallContext, bool bPassBall)
	{
		if( bPassBall )
		{
			m_player.eventHandler.OnPassBall();
			Logger.Log("block On pass");
		}
		else
		{
			Player attacker = m_ball.m_actor;
			if( attacker == null )
				attacker = m_ball.m_owner;

			attacker.DropBall(m_ball);
			m_ball.transform.position 	= (Vector3)m_player.rHandPos;
			m_ball.initPos			= lostBallContext.vInitPos;
			m_ball.initVel			= lostBallContext.vInitVel;
			m_ball.m_bBlockSuccess		= true;
			m_ball.OnBlock();
		}
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_bMoveForward 	= false;
		m_success 		= false;
		m_bFailDown 	= false;
	}
}
