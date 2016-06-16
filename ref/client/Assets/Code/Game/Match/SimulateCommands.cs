using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;
//for lockstep
public class SimulateCommandManager
{
	private Queue<SimulateCommand>	m_commands = new Queue<SimulateCommand>();
	private SimulateCommand			m_curCommand;

	public SimulateCommandManager()
	{
	}

	public void AddCommand(SimulateCommand cmd)
	{
		if( cmd == null )
			return;
		//m_commands.Enqueue(cmd);
		cmd.OnEnter();
	}

	public void Reset(double fStartSrvTime)
	{
		m_commands.Clear();
	}

	/*
	public void Update(float fDeltaTime)
	{
		if( m_curCommand == null )
		{
			if( m_commands.Count != 0 )
			{
				m_curCommand = m_commands.Dequeue();
				m_curCommand.OnEnter();
				//Logger.Log("Simulate delta time : " + (m_trackLine - m_curCommand.m_startTime) );
			}
		}
		if( m_curCommand == null )
			return;

		SimulateCommand nextCommand = null;
		if( m_commands.Count != 0 )
			nextCommand = m_commands.Peek();

		if( nextCommand != null )
		{
			if( m_curCommand != null )
				m_curCommand.OnEnd();

			if( m_commands.Count != 0 )
			{
				m_curCommand = m_commands.Dequeue();
				m_curCommand.OnEnter();
				//Logger.Log("Simulate delta time : " + (m_trackLine - m_curCommand.m_startTime) );
			}
		}

		if( m_curCommand != null )
			m_curCommand.Update(fDeltaTime);
	}
	*/
}

public class SimulateCommand
{	
	public double			m_startTime;
	public PlayerState.State m_state{get; protected set;}
	

	protected Player		m_player;
	protected GameMsg		m_msg;
	protected GameMatch 	m_match;
	protected UBasketball 	m_ball;

	protected bool		m_bActionDone 	= false;
	protected bool		m_bWaitForAction = false;

	public SimulateCommand(Player player, GameMsg msg, GameMatch match)
	{
		m_player 	= player;
		m_msg		= msg;
		m_match		= match;
		m_ball		= m_match.mCurScene.mBall;

		m_startTime = m_msg.curTime;
	}

	public virtual void OnEnter()
	{
		//Logger.Log("Command: " + m_state + " time consume: " + string.Format("{0:f4}", GameSystem.Instance.mNetworkManager.m_dServerTime - m_startTime) );
	}

	public virtual void Update(float fDeltaTime)
	{
	}

	protected virtual void _OnActionDone()
	{
	}

	public void OnEnd()
	{
	}
}

public class SMC_SyncInput :
	SimulateCommand
{	
	public SMC_SyncInput(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eInput;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;

		m_player.m_curInputDir = m_msg.inputDir;
		//Logger.Log("Input is " + m_msg.inputDir);
	}
}

public class SMC_PrepareShoot :
	SimulateCommand
{	
	public SMC_PrepareShoot(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.ePrepareToShoot;
	}
	
	public override void OnEnter()
	{
		base.OnEnter();

		if( !m_player.m_bSimulator )
			return;

		Logger.Log("handle prepareShoot.");
		if( m_msg.skill != null )
		{
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 
		}

		PlayerState_PrepareToShoot prepareShoot = m_player.m_StateMachine.GetState(PlayerState.State.ePrepareToShoot) as PlayerState_PrepareToShoot;
		if( m_msg.nSuccess == 0 )
		{
			prepareShoot.mCanShoot = false;
		}
		else
		{
			m_player.position = GameUtils.Convert(m_msg.pos);
			m_player.rotation = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
			prepareShoot.mCanShoot = true;
		}

		if( m_msg.skill != null )
		{
			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;
		}
	}
}

public class SMC_Catch :
	SimulateCommand
{
	public SMC_Catch(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eCatch;
	}
	
	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;

		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		PlayerState_Catch catchState = m_player.m_StateMachine.GetState(PlayerState.State.eCatch) as PlayerState_Catch;
		catchState.m_animType = m_msg.eStateType;
		m_player.m_StateMachine.SetState(PlayerState.State.eCatch);
	}
}

public class SMC_Hold :
	SimulateCommand
{
	public SMC_Hold(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eHold;
	}
	
	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;
		
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		m_player.m_StateMachine.SetState(PlayerState.State.eHold);
	}
}


public class SMC_Shoot :
	SimulateCommand
{	
	public SMC_Shoot(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eShoot;
	}
	
	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;

		if( m_msg.eBallState == fogs.proto.msg.BallState.useBall_Shoot )
		{
			m_player.position		= GameUtils.Convert(m_msg.pos);
            m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 
			
			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;
		}
		else
		{
			UBasketball ball = m_match.mCurScene.balls[(int)m_msg.ballId];
			if( ball == null )
				ball = m_ball;

			if( ball.m_bBlockSuccess )
				return;

			ShootSolution solution = GameSystem.Instance.shootSolutionManager.GetShootSolution((int)m_msg.nCurveType);
			IM.Vector3 vPos = ball.position;
			m_player.DropBall(ball);
			ball.m_shootSolution = solution;
			ball.SetInitPos(vPos);
			ball.initVel = solution.m_vInitVel;
			ball.OnShoot(m_player, m_msg.area, false);
		}
	}
}

public class SMC_LayupFailed :
	SimulateCommand
{
	public SMC_LayupFailed(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eLayupFailed;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;

		PlayerState_LayupFailed failed = m_player.m_StateMachine.GetState(PlayerState.State.eLayupFailed) as PlayerState_LayupFailed;
		m_player.position = GameUtils.Convert(m_msg.pos);
        m_player.rotation = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up); ;
		failed.m_animType = m_msg.eStateType;

		m_player.m_StateMachine.SetState(failed);
	}
}

public class SMC_Layup :
	SimulateCommand
{	
	public SMC_Layup(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eLayup;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;
		if( m_msg.eBallState == fogs.proto.msg.BallState.useBall_Shoot )
		{
			m_player.position		= GameUtils.Convert(m_msg.pos);
            m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up); ;
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 
			
			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;
		}
		else
		{
			UBasketball ball = m_match.mCurScene.balls[(int)m_msg.ballId];
			if( ball == null )
				ball = m_ball;

			if( ball.m_bBlockSuccess )
				return;

			ShootSolution solution = GameSystem.Instance.shootSolutionManager.GetShootSolution((int)m_msg.nCurveType);
			IM.Vector3 vPos = ball.position;
			m_player.DropBall(ball);
			ball.position = vPos;
			ball.m_shootSolution = solution;
			ball.initVel = solution.m_vInitVel;
			ball.OnShoot(m_player, m_msg.area, true);
		}
	}
}

public class SMC_Dunk :
	SimulateCommand
{	
	public SMC_Dunk(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eDunk;
	}
	
	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;
		
		if( m_msg.eBallState == fogs.proto.msg.BallState.useBall_Shoot )
		{
			m_player.position				= GameUtils.Convert(m_msg.pos);
            m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 
			
			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;

			Logger.Log("dunk: useball_shoot");
		}
		else
		{
			UBasketball ball = m_match.mCurScene.balls[(int)m_msg.ballId];
			if( ball == null )
				ball = m_ball;

			if( ball.m_bBlockSuccess )
				return;

			m_player.DropBall(ball);
			
			ball.position	         = GameUtils.Convert(m_msg.ballPos);
			ball.initPos			 = GameUtils.Convert(m_msg.ballPos);
			ball.initVel			 = GameUtils.Convert(m_msg.ballVelocity);
			ball.OnDunk(m_msg.nSuccess == 1, ball.initVel, ball.initPos, m_player);

			Logger.Log("dunk: on_dunk, m_msg.nSuccess: " + m_msg.nSuccess);
		}
	}
}

public class SMC_Block :
	SimulateCommand
{	
	public SMC_Block(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBlock;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		
		if( !m_player.m_bSimulator )
			return;

		PlayerState_Block block = m_player.m_StateMachine.GetState(PlayerState.State.eBlock) as PlayerState_Block;
		block.m_blockedMoveVel 	= GameUtils.Convert(m_msg.velocity);
		m_player.position			= GameUtils.Convert(m_msg.pos);
        m_player.rotation           = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
		 

		if( m_msg.nSuccess != 0 )
		{
			Logger.Log("block success.");
			block.m_success = true;

			block.m_loseBallContext.vInitPos = GameUtils.Convert(m_msg.ballPos);
			block.m_loseBallContext.vInitVel = GameUtils.Convert(m_msg.ballVelocity);

			if( m_msg.skill.skill_id == PlayerState_Block.idBlockPassBall )
				m_player.m_passTarget = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player player)=>{ return player.m_roomPosId == m_msg.destUserID;} );
		}

		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;
	}
}

public class SMC_BodyThrowCatch :
	SimulateCommand
{	
	public SMC_BodyThrowCatch(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBodyThrowCatch;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		
		if( m_player.m_bSimulator )
		{
			m_player.position				= GameUtils.Convert(m_msg.pos);
            m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up); 
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 
			
			PlayerState_BodyThrowCatch stateCatch = m_player.m_StateMachine.GetState(PlayerState.State.eBodyThrowCatch) as PlayerState_BodyThrowCatch;
			stateCatch.m_animType 		= m_msg.eStateType;
			stateCatch.m_bSuccess		= Convert.ToBoolean(m_msg.nSuccess);
			stateCatch.m_vInitPos 		= GameUtils.Convert(m_msg.ballPos);
            stateCatch.m_vInitVelocity  = GameUtils.Convert(m_msg.ballVelocity);

			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;
		}

		bool bPickBall = Convert.ToBoolean(m_msg.nSuccess);
		UBasketball ball = m_match.mCurScene.balls.Find((UBasketball inBall)=>{return inBall.m_id == m_msg.ballId;});
		if( bPickBall )
		{
			if( ball == null )
			{
				Logger.Log("can not find ball id: " + ball.m_id);
				return;
			}
			MatchState.State eCurState=  m_match.m_stateMachine.m_curState.m_eState;
			if( eCurState == MatchState.State.ePlaying || eCurState == MatchState.State.eTipOff)
				m_player.GrabBall(ball);
			
			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePickupBall);
		}
	}
}

public class SMC_CrossOver :
	SimulateCommand
{	
	public SMC_CrossOver(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eCrossOver;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.position				= GameUtils.Convert(m_msg.pos);
        m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up); ;
		m_player.moveDirection			    = IM.Vector3.zero;

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
		 
		
		PlayerState state = m_player.m_StateMachine.GetState(PlayerState.State.eCrossOver);
		state.m_animType = m_msg.eStateType;

		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;

		m_bWaitForAction = true;
		 base.OnEnter();
	}
}

public class SMC_Defense :
	SimulateCommand
{	
	public SMC_Defense(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eDefense;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.m_dir 			= m_msg.dir;
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		m_player.m_moveType 	= MoveType.eMT_Defense;
	}
}

public class SMC_FallGround :
	SimulateCommand
{	
	public SMC_FallGround(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eFallGround;
	}

	public override void OnEnter()
	{
		m_player.position			= GameUtils.Convert(m_msg.pos);
        m_player.rotation           = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		PlayerState_FallGround state = m_player.m_StateMachine.GetState(PlayerState.State.eFallGround) as PlayerState_FallGround;
		state.m_animType			= m_msg.eStateType;
		m_player.m_StateMachine.SetState(state);
		 base.OnEnter();
	}
}

public class SMC_FallLostBall :
	SimulateCommand
{	
	public SMC_FallLostBall(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eFallLostBall;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.position				= GameUtils.Convert(m_msg.pos);
        m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		m_player.m_lostBallContext.vInitPos = GameUtils.Convert(m_msg.ballPos);
		m_player.m_lostBallContext.vInitVel = GameUtils.Convert(m_msg.ballVelocity);
		
		PlayerState_FallLostBall state = m_player.m_StateMachine.GetState(PlayerState.State.eFallLostBall) as PlayerState_FallLostBall;
		state.m_animType 			= m_msg.eStateType;
		
		m_player.m_StateMachine.SetState(PlayerState.State.eFallLostBall);

		base.OnEnter();
	}
}

public class SMC_Knocked :
	SimulateCommand
{	
	public SMC_Knocked(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eKnocked;
	}

	public override void OnEnter()
	{
		//if( !m_player.m_bSimulator )
		//	return;
		m_player.position				= GameUtils.Convert(m_msg.pos);
        m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_Knocked state = m_player.m_StateMachine.GetState(PlayerState.State.eKnocked) as PlayerState_Knocked;
		state.m_animType 			= m_msg.eStateType;
		
		m_player.m_StateMachine.SetState(PlayerState.State.eKnocked);

		base.OnEnter();
	}
}

public class SMC_RequireBall :
	SimulateCommand
{	
	public SMC_RequireBall(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eRequireBall;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		PlayerState_RequireBall state 	= m_player.m_StateMachine.GetState(PlayerState.State.eRequireBall) as PlayerState_RequireBall;
		state.m_animType 		= m_msg.eStateType;
		if( m_msg.skill != null )//run require
		{
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			 

			m_player.m_StateMachine.SetState(m_state, true);
			m_player.m_toSkillInstance = null;
		}

		if( state.m_animType == AnimType.N_TYPE_1 )
		{
			m_player.m_dir 			= m_msg.dir;
			m_player.m_moveType 	= MoveType.eMT_RequireBall;
		}
		base.OnEnter();
	}
}

public class SMC_CutIn :
	SimulateCommand
{	
	public SMC_CutIn(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eCutIn;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		PlayerState_CutIn state	= m_player.m_StateMachine.GetState(PlayerState.State.eCutIn) as PlayerState_CutIn;
		state.m_animType 		= m_msg.eStateType;
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
		 

		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;
		
		 base.OnEnter();
	}
}

public class SMC_Pass :
	SimulateCommand
{	
	public SMC_Pass(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.ePass;
	}
	
	public override void OnEnter()
	{
		if( m_player.m_bNative )
			return;
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		PlayerState_Pass state 	= m_player.m_StateMachine.GetState(PlayerState.State.ePass) as PlayerState_Pass;
		state.m_animType 		= m_msg.eStateType;
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;

        if( m_player.m_aiMgr != null && m_player.m_aiMgr.IsPvp)
        {
            Logger.Log("Ignore server pass for pvp");
            return;
        }
		 
		
		Player target 			= GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.destUserID; } );
		if( target != null )
			m_player.m_passTarget = target;

		Logger.Log("Player id: " + m_player.m_id + " pass ball to player: " + target.m_id);

		m_bWaitForAction = true;
		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;

		if( m_msg.relativePlayerId != 0 )
			state.m_interceptor	= GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.relativePlayerId; } );
		state.m_interceptedPos	= GameUtils.Convert(m_msg.ballPos);

		 base.OnEnter();
	}
}

public class SMC_Interception :
	SimulateCommand
{	
	public SMC_Interception(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eInterception;
	}
	
	public override void OnEnter()
	{
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		PlayerState_Interception state 	= m_player.m_StateMachine.GetState(PlayerState.State.eInterception) as PlayerState_Interception;
		state.m_animType 		= m_msg.eStateType;

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
		 

		state.m_passer			= GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.destUserID; } );
		state.m_catcher			= GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.relativePlayerId; } );

		state.m_bGetBall		= Convert.ToBoolean(m_msg.nSuccess);

		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;
		
		 base.OnEnter();
	}
}

public class SMC_Pick :
	SimulateCommand
{	
	public SMC_Pick(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.ePickup;
	}

	public override void OnEnter()
	{
		bool bPickBall = Convert.ToBoolean(m_msg.nSuccess);
		if( m_player.m_bSimulator )
		{
			PlayerState_Pickup pickup 	= m_player.m_StateMachine.GetState(PlayerState.State.ePickup) as PlayerState_Pickup;
			pickup.m_animType = m_msg.eStateType;
			pickup.m_bSuccess = bPickBall;
			m_player.m_StateMachine.SetState(PlayerState.State.ePickup);
		}

		UBasketball ball = m_match.mCurScene.balls.Find((UBasketball inBall)=>{return inBall.m_id == m_msg.ballId;});
		if( bPickBall )
		{
			if( ball == null )
			{
				Logger.Log("can not find ball id: " + ball.m_id);
				return;
			}

			MatchState.State eCurState=  m_match.m_stateMachine.m_curState.m_eState;
			if( eCurState == MatchState.State.ePlaying || eCurState == MatchState.State.eTipOff)
				m_player.GrabBall(ball);

			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePickupBall);
			//Logger.Log("player room id: " + m_player.m_roomPosId + " pickup ball id: " + ball.m_id);
		}
		else
		{
			Logger.Log("can not pick up ball id: " + ball.m_id);
		}
		 base.OnEnter();
	}
}

public class SMC_PickAndRoll:
	SimulateCommand
{
	public SMC_PickAndRoll(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.ePickAndRoll;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;

		m_player.position			= GameUtils.Convert(m_msg.pos);
        m_player.rotation           = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;

		if( m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.ePickAndRoll )
		{
			PlayerState_PickAndRoll pickAndRoll = m_player.m_StateMachine.GetState(PlayerState.State.ePickAndRoll) as PlayerState_PickAndRoll;
			pickAndRoll.OnSimulateAction(m_msg.eStateType);
		}
		else
		{
			m_player.m_StateMachine.SetState(m_state, true);
		}
		
		 base.OnEnter();
	}
}

public class SMC_BePickAndRoll:
	SimulateCommand
{
	public SMC_BePickAndRoll(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBePickAndRoll;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		
		m_player.position			= GameUtils.Convert(m_msg.pos);
        m_player.rotation           = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState state = m_player.m_StateMachine.GetState(PlayerState.State.eBePickAndRoll);
		state.m_animType = m_msg.eStateType;

		PlayerState_BePickedAndRolled pickAndRoll = m_player.m_StateMachine.GetState(PlayerState.State.eBePickAndRoll) as PlayerState_BePickedAndRolled;
		m_player.m_StateMachine.SetState(m_state, true);
		pickAndRoll.OnSimulateAction(m_msg.eStateType);
		
		 base.OnEnter();
	}
}

public class SMC_Rebound :
	SimulateCommand
{	
	public SMC_Rebound(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eRebound;
	}

	public override void OnEnter()
	{
		PlayerState_Rebound stateRebound = m_player.m_StateMachine.GetState(PlayerState.State.eRebound) as PlayerState_Rebound;
		if( m_msg.nSuccess == 3 )
		{
			if( m_player.m_bSimulator )
			{
				m_player.position				= GameUtils.Convert(m_msg.pos);
                m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
				m_player.m_toSkillInstance 		= m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
				m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
				m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
				 
				
				stateRebound.m_animType 	= m_msg.eStateType;
				stateRebound.m_success		= true;
				IM.Vector3 vel 				= GameUtils.Convert(m_msg.velocity);
				stateRebound.m_heightScale	= vel.y;
				stateRebound.rootMotionScale = vel.x;
				Logger.Log("rebound simulate scale: " + vel.x);

				m_player.m_StateMachine.SetState(m_state, true);
				m_player.m_toSkillInstance = null;
			}
		}
		else
		{
			/*
			if (m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound)
			{
				if (m_match.GetMatchType() != GameMatch.Type.ePVP_1PLUS && m_match.GetMatchType() != GameMatch.Type.ePVP_3On3)
					Logger.LogError("SMC_Rebound: Player state is not rebound while command executing.");
			}
			if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound )
				Logger.LogError("rebound command but not in rebound state.");
			*/
			bool bPickBall = Convert.ToBoolean(m_msg.nSuccess);
			UBasketball ball = m_match.mCurScene.mBall;
			if( bPickBall )
			{
				if( m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eRebound && !stateRebound.m_toReboundBall )
					stateRebound.m_toReboundBall = true;
				else
				{
					MatchState.State eCurState=  m_match.m_stateMachine.m_curState.m_eState;
					if( eCurState == MatchState.State.ePlaying || eCurState == MatchState.State.eTipOff)
					{
						if( ball.m_owner != null )
							Logger.LogError("can not grab ball.");
						m_player.GrabBall(ball);
					}
				}
			}
			else
			{
				Logger.Log("can not rebound ball id: " + ball.m_id);
			}
		}
		 base.OnEnter();
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone ();
		Logger.Log("rebound done.");
	}
}

public class SMC_Run :
	SimulateCommand
{	
	private bool	m_bMoveTo = false;
	private bool	m_bArrived = false;

	public SMC_Run(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eRun;
	}

	public override void OnEnter()
	{
		//Logger.Log("sender move time: " + m_msg.curTime + ", simulator move time: " + GameSystem.Instance.mNetworkManager.m_dServerTime + " deltaTime: " + 
		//          (GameSystem.Instance.mNetworkManager.m_dServerTime - m_msg.curTime) );

		if( !m_player.m_bSimulator )
			return;
		m_player.m_moveType = MoveType.eMT_Run;
		m_player.m_dir = m_msg.dir;

		m_player.m_StateMachine.SetState(PlayerState.State.eRun);
		 base.OnEnter();
	}
}

public class SMC_Rush :
	SimulateCommand
{	
	public SMC_Rush(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eRush;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.m_dir = m_msg.dir;

		//if( m_player.m_dir == 0 )
		//	m_player.m_moveHelper.MoveTo( GameUtils.Convert(m_msg.destPos) );

		m_player.m_moveType = MoveType.eMT_Rush;
		 base.OnEnter();
		
	}
}

public class SMC_Stand :
	SimulateCommand
{	
	public SMC_Stand(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eStand;
	}

	public override void OnEnter()
	{
		if( m_player == null || !m_player.m_bSimulator )
			return;
		m_player.m_dir			= -1;
		m_player.moveDirection	    = IM.Vector3.zero;
		m_player.position		= GameUtils.Convert(m_msg.pos);
        m_player.rotation       = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		m_player.m_moveType		= MoveType.eMT_Stand;

		 base.OnEnter();
	}
}

public class SMC_Steal :
	SimulateCommand
{	
	public SMC_Steal(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eSteal;
	}

	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		m_player.position			= GameUtils.Convert(m_msg.pos);
        m_player.rotation           = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		m_player.m_toSkillInstance  = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
		m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
		m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
		 

		PlayerState_Steal state		= m_player.m_StateMachine.GetState(PlayerState.State.eSteal) as PlayerState_Steal;
		state.m_bGetBall 			= Convert.ToBoolean(m_msg.nSuccess);

		if( m_msg.destUserID == 0 )
			state.stealTarget = null;
		else
		   	state.stealTarget = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.destUserID; } );
		
		m_player.m_StateMachine.SetState(m_state, true);
		m_player.m_toSkillInstance = null;

		 base.OnEnter();
	}
}

public class SMC_Stolen :
	SimulateCommand
{	
	public SMC_Stolen(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eStolen;
	}
	
	public override void OnEnter()
	{
		m_player.position				= GameUtils.Convert(m_msg.pos);
        m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		m_player.m_lostBallContext.vInitPos = GameUtils.Convert(m_msg.ballPos);
        m_player.m_lostBallContext.vInitVel = GameUtils.Convert(m_msg.ballVelocity);

		PlayerState_Stolen state		= m_player.m_StateMachine.GetState(PlayerState.State.eStolen) as PlayerState_Stolen;
		state.m_bLostBall				= Convert.ToBoolean(m_msg.nSuccess);

		m_player.m_StateMachine.SetState(PlayerState.State.eStolen);

		//m_bWaitForAction = true;
		 base.OnEnter();
	}
}

public class SMC_Crossed :
	SimulateCommand
{	
	public SMC_Crossed(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eCrossed;
	}
	
	public override void OnEnter()
	{
		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState state 	= m_player.m_StateMachine.GetState(PlayerState.State.eCrossed);
		state.m_animType 	= m_msg.eStateType;

		m_player.m_StateMachine.SetState(PlayerState.State.eCrossed);

		//m_bWaitForAction = true;
		 base.OnEnter();
	}
}

public class SMC_DefenseCross :
	SimulateCommand
{	
	public SMC_DefenseCross(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eDefenseCross;
	}
	
	public override void OnEnter()
	{
		m_player.position				= GameUtils.Convert(m_msg.pos);
        m_player.rotation               = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		PlayerState_DefenseCross cross = m_player.m_StateMachine.GetState(PlayerState.State.eDefenseCross) as PlayerState_DefenseCross;
		cross.m_animType				= m_msg.eStateType;
		cross.targetPos					= GameUtils.Convert(m_msg.destPos);
		cross.dirMove					= GameUtils.Convert(m_msg.velocity);
		cross.speed						= new IM.Number((int)m_msg.fAnimPlaySpeed);
		cross.crosser					= GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player inPlayer)=>{ return inPlayer.m_roomPosId == m_msg.destUserID; } );
		cross.time						= GameUtils.HorizonalDistance(m_player.position, cross.targetPos) / cross.speed;

		m_player.m_StateMachine.SetState(PlayerState.State.eDefenseCross);
		
		base.OnEnter();
	}
}

public class SMC_Disturb :
	SimulateCommand
{	
	public SMC_Disturb(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eDisturb;
        Logger.Log("SMC_Disturb()");
	}
	
	public override void OnEnter()
	{
        Logger.Log("SMC_Disturb.OnEnter()");
		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		m_player.m_StateMachine.SetState(PlayerState.State.eDisturb);

        base.OnEnter();
	}
}

public class SMC_BackToBack :
	SimulateCommand
{	
	public SMC_BackToBack(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackToBack;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;

		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);

		if( m_msg.skill != null )
		{
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById((int)m_msg.skill.skill_id);
			m_player.m_toSkillInstance.matchedKeyIdx = m_msg.skill.skill_matchedKeyIdx;
			m_player.m_toSkillInstance.curActionId = m_msg.skill.action_id;
			Logger.Log("skill: " + m_msg.skill.skill_id + " matched key id: " + m_msg.skill.skill_matchedKeyIdx + " action id: " + m_msg.skill.action_id);
		}

		PlayerState_BackToBack backToBack = m_player.m_StateMachine.GetState(PlayerState.State.eBackToBack) as PlayerState_BackToBack;
		m_player.m_StateMachine.SetState(PlayerState.State.eBackToBack);

		base.OnEnter();
	}
}

public class SMC_BackToBackForward :
	SimulateCommand
{	
	public SMC_BackToBackForward(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackToBackForward;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		
		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_BackToBackForward obj = m_player.m_StateMachine.GetState(m_state) as PlayerState_BackToBackForward;
		obj.m_animType = m_msg.eStateType;
		m_player.m_StateMachine.SetState(obj);
		
		base.OnEnter();
	}
}

public class SMC_BackCompete :
	SimulateCommand
{	
	public SMC_BackCompete(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackCompete;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;

		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_BackCompete obj = m_player.m_StateMachine.GetState(PlayerState.State.eBackCompete) as PlayerState_BackCompete;
		obj.m_animType = m_msg.eStateType;

		m_match.m_context.m_backToBackWinnerId = Convert.ToBoolean(m_msg.nSuccess) ? m_player.m_roomPosId : m_player.m_defenseTarget.m_roomPosId;

		m_player.m_StateMachine.SetState(PlayerState.State.eBackCompete, true);

		Player target = m_player.m_defenseTarget;
		if( target != null )
		{
			PlayerState_BackBlock backBlock = target.m_StateMachine.GetState(PlayerState.State.eBackBlock) as PlayerState_BackBlock;
			backBlock.m_competor = m_player;
		}
		
		base.OnEnter();
	}
}


public class SMC_BackBlock :
	SimulateCommand
{	
	public SMC_BackBlock(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackBlock;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_defenseTarget.m_bSimulator )
			return;

		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_BackBlock backBlock = m_player.m_StateMachine.GetState(PlayerState.State.eBackBlock) as PlayerState_BackBlock;
		backBlock.m_animType = m_msg.eStateType;
		m_player.m_StateMachine.SetState(PlayerState.State.eBackBlock, true);
		
		base.OnEnter();
	}
}


public class SMC_BackToStand :
	SimulateCommand
{	
	public SMC_BackToStand(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackToStand;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		
		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_BackToStand obj = m_player.m_StateMachine.GetState(PlayerState.State.eBackToStand) as PlayerState_BackToStand;
		obj.m_animType		= m_msg.eStateType;
		m_player.m_StateMachine.SetState(PlayerState.State.eBackToStand);
		
		base.OnEnter();
	}
}


public class SMC_BackTurnRun :
	SimulateCommand
{	
	public SMC_BackTurnRun(Player player, GameMsg m_msg, GameMatch match)
		:base(player, m_msg, match)
	{
		m_state = PlayerState.State.eBackTurnRun;
	}
	
	public override void OnEnter()
	{
		if( !m_player.m_bSimulator )
			return;
		
		m_player.position	= GameUtils.Convert(m_msg.pos);
        m_player.rotation   = IM.Quaternion.AngleAxis(new IM.Number((int)m_msg.rotate), IM.Vector3.up);
		
		PlayerState_BackTurnRun backToBack = m_player.m_StateMachine.GetState(PlayerState.State.eBackTurnRun) as PlayerState_BackTurnRun;
		backToBack.m_animType = m_msg.eStateType;
		m_player.m_StateMachine.SetState(PlayerState.State.eBackTurnRun);
		
		base.OnEnter();
	}
}



