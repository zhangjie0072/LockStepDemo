using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;

public class GameMsgSender
{
	public GameMsgSender()
	{
	}

	static bool _FilterOutMsgByMatchState()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null || match.m_stateMachine.m_curState == null )
			return true;
		MatchState.State curState = match.m_stateMachine.m_curState.m_eState;
		if( curState == MatchState.State.eOpening || curState == MatchState.State.eOverTime || curState == MatchState.State.eOver )
			return true;
		return false;
	}

	public static void SendGameShortMsg(Player player, uint id, uint broadcastType)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		InstructionReq instReq = new InstructionReq();
		instReq.id = id;
		instReq.obj = broadcastType;
		instReq.char_id = player.m_roomPosId;

		NetworkConn server = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( server != null )
			server.SendPack<InstructionReq>(0, instReq, MsgID.InstructionReqID);
	}

	public static void SendStand(Player player)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Stand stand		= new Stand();
		stand.pos 		= GameUtils.Convert( player.position );
		stand.rotate	= player.rotation.eulerAngles.y.ToUnity2();
		stand.char_id 	= player.m_roomPosId;

		stand.time_stamp = System.DateTime.Now.Ticks;
		

		NetworkConn server = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( server != null )
			server.SendPack<Stand>(0, stand, MsgID.StandID);
	}

	public static void SendCrossOver(Player player, SkillInstance skillInst)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		CrossOver crossover = new CrossOver();
		crossover.char_id 	= player.m_roomPosId;
		crossover.pos 		= GameUtils.Convert( player.position );
        crossover.rotate = player.rotation.eulerAngles.y.ToUnity2();
		crossover.time_stamp = System.DateTime.Now.Ticks;
		

		crossover.skill		= new Skill();
		crossover.skill.skill_id			= skillInst.skill.id;
		crossover.skill.action_id			= skillInst.curActionId;
		crossover.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;

		crossover.stateType = AnimType.N_TYPE_0;
		if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<CrossOver>(0, crossover, MsgID.CrossOverID);
	}

	public static void SendCrossed(Player player, Player crosser, AnimType animType)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Crossed crossed = new Crossed();
		crossed.char_id 	= player.m_roomPosId;
		crossed.cross_char_id	= crosser.m_roomPosId;
		crossed.pos 		= GameUtils.Convert( player.position );
        crossed.rotate = player.rotation.eulerAngles.y.ToUnity2();
		crossed.stateType 	= animType;
		crossed.time_stamp = System.DateTime.Now.Ticks;
		
		if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Crossed>(0, crossed, MsgID.CrossedID);
	}

	public static void SendDefenseCross(Player player, Player crosser, AnimType animType, float fPlayBackSpeed, Vector3 targetPos, Vector3 vMoveDir)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		DefenseCross defCross = new DefenseCross();
		defCross.char_id 	= player.m_roomPosId;
		defCross.cross_char_id = crosser.m_roomPosId;
		defCross.pos 		= GameUtils.Convert(player.position);
        defCross.rotate = player.rotation.eulerAngles.y.ToUnity2();
		defCross.stateType  = animType;
		defCross.playbackSpeed = fPlayBackSpeed;
		defCross.targetPos	= GameUtils.Convert(targetPos);
		defCross.velocity	= GameUtils.Convert(vMoveDir);
		defCross.time_stamp = System.DateTime.Now.Ticks;
		
		if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<DefenseCross>(0, defCross, MsgID.DefenseCrossID);
	}

	public static void SendDisturb(Player player)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Disturb disturb = new Disturb();
		disturb.char_id 	= player.m_roomPosId;
		disturb.pos 		= GameUtils.Convert( player.position );
        disturb.rotate = player.rotation.eulerAngles.y.ToUnity2();
		disturb.time_stamp  = System.DateTime.Now.Ticks;

        if (GameSystem.Instance.mNetworkManager.m_gameConn != null)
        {
            GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Disturb>(0, disturb, MsgID.DisturbID);
            Logger.Log("Send disturb");
        }
	}

	public static void SendLayupFailed(Player player, AnimType type)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		LayupFailed layupFailed = new LayupFailed();
		layupFailed.char_id 	= player.m_roomPosId;

		layupFailed.pos 		= GameUtils.Convert( player.position );
        layupFailed.rotate = player.rotation.eulerAngles.y.ToUnity2();
		layupFailed.time_stamp  = System.DateTime.Now.Ticks;
		layupFailed.actionType	= type;

		if (GameSystem.Instance.mNetworkManager.m_gameConn != null)
		{
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<LayupFailed>(0, layupFailed, MsgID.LayupFailedID);
			Logger.Log("Send disturb");
		}
	}

	public static void SendMove(Player player, MoveType moveType, AnimType type)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		if( _FilterOutMsgByMatchState() || conn is VirtualNetworkConn )
			return;

		Move move 		= new Move();
		move.actionType = type;
		move.char_id 	= player.m_roomPosId;
		move.dir 		= player.m_dir;
		move.type		= moveType;
		move.pos 		= GameUtils.Convert( player.position );
        move.rotate = player.rotation.eulerAngles.y.ToUnity2();
		move.time_stamp = System.DateTime.Now.Ticks;

		if( player.m_moveHelper != null )
			move.dest_pos	= GameUtils.Convert( player.m_moveHelper.targetPosition.ToUnity2() );
		 
		NetworkConn server = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( server != null )
		{
			for( int i = 0; i != 1; i++ )
			server.SendPack<Move>(0, move, MsgID.MoveID);
		}
	}

	public static void SendPickBall(Player player, uint uBallId, AnimType type, BallState state)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		MatchState.State curState = match.m_stateMachine.m_curState.m_eState;
		if( _FilterOutMsgByMatchState() && curState != MatchState.State.eTipOff)
			return;

		PickBall pickBall	= new PickBall();
		pickBall.char_id 	= player.m_roomPosId;
		pickBall.actionType = type;
		pickBall.pos		= GameUtils.Convert(player.position);
        pickBall.rotate = player.rotation.eulerAngles.y.ToUnity2();
		pickBall.ball_id	= uBallId;
		pickBall.time_stamp = System.DateTime.Now.Ticks;
		

		pickBall.ball_state = Convert.ToUInt32(state == BallState.eLoseBall);

		Logger.Log("Send pick up ball");
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PickBall>(0, pickBall, MsgID.PickBallID);
	}

	public static void SendShootSkill(Player player, SkillInstance skillInst, Area area, uint ballId, bool bOpen)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack shootSkill	= new Attack();
		shootSkill.state		= CharacterState.eShoot;
		shootSkill.char_id 		= player.m_roomPosId;
		
		shootSkill.skill			= new Skill();
		shootSkill.skill.skill_id = skillInst.skill.id;
		shootSkill.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		shootSkill.skill.action_id = skillInst.curAction.id;
		
		shootSkill.pos			= GameUtils.Convert(player.position);
        shootSkill.rotate = player.rotation.eulerAngles.y.ToUnity2();
		shootSkill.area			= area;
		shootSkill.ballState	= fogs.proto.msg.BallState.useBall_Shoot;

		shootSkill.open			= Convert.ToUInt32(bOpen);

		shootSkill.time_stamp = System.DateTime.Now.Ticks;
		
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, shootSkill, MsgID.AttackID);
	}

	public static void SendShoot(Player player, SkillInstance skillInstance, Area skillArea, uint ballId, bool bGoal, IM.Number prob, uint curveId, IM.Number force_bar, IM.Number fBallFlyTime, bool bOpen)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack shoot		= new Attack();
		shoot.state			= CharacterState.eShoot;
		shoot.char_id 		= player.m_roomPosId;
		shoot.pos			= GameUtils.Convert(player.position);
        shoot.rotate = player.rotation.eulerAngles.y.ToUnity2();
		shoot.area			= skillArea;
		shoot.ballCurve		= curveId;
		shoot.prob			= prob.ToUnity2();
		shoot.time_stamp = System.DateTime.Now.Ticks;
		

		shoot.skill			= new Skill();
		shoot.skill.skill_id	= skillInstance.skill.id;
		shoot.skill.skill_matchedKeyIdx	= skillInstance.matchedKeyIdx;
		shoot.skill.action_id = skillInstance.curAction.id;
		

		shoot.force_bar		= force_bar.ToUnity2();
		shoot.ballState		= fogs.proto.msg.BallState.loseBall;
		shoot.goal			= Convert.ToUInt32(bGoal);
		shoot.ballId		= ballId;
		shoot.ballFlyTime	= fBallFlyTime.ToUnity2();

		shoot.open			= Convert.ToUInt32(bOpen);

		//player.m_lastReceiveShootTime = (float)GameSystem.Instance.mNetworkManager.m_profiler.m_fServerTime;
		//Logger.Log("sendShoot");

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, shoot, MsgID.AttackID);
	}

	public static void SendLayupShoot(Player player, uint ballId, float prob, Area area, bool bGoal, uint curveId, float fBallFlyTime, bool bOpen)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack shoot		= new Attack();
		shoot.state			= CharacterState.eLayup;
		shoot.char_id 		= player.m_roomPosId;
		shoot.pos			= GameUtils.Convert(player.position);
        shoot.rotate = player.rotation.eulerAngles.y.ToUnity2();
		shoot.area			= area;
		shoot.ballCurve		= curveId;
		shoot.prob			= prob;
		shoot.ballState		= fogs.proto.msg.BallState.loseBall;
		shoot.goal			= Convert.ToUInt32(bGoal);
		shoot.ballId		= ballId;
		shoot.ballFlyTime	= fBallFlyTime;
		shoot.open			= Convert.ToUInt32(bOpen);

		shoot.time_stamp = System.DateTime.Now.Ticks;
		

		if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, shoot, MsgID.AttackID);
	}

	public static void SendDunkShoot(Player player, uint ballId, float prob, Area area, bool bGoal, Vector3 ballPos, Vector3 ballVel, bool bOpen)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack shoot		= new Attack();
		shoot.state			= CharacterState.eDunk;
		shoot.char_id 		= player.m_roomPosId;
		shoot.area			= area;
		shoot.prob			= prob;
		shoot.goal			= Convert.ToUInt32(bGoal);
		shoot.ballId		= ballId;
		shoot.ballFlyTime	= 0f;
		shoot.time_stamp = System.DateTime.Now.Ticks;
		

		shoot.ballInitVel	= GameUtils.Convert(ballVel);
		shoot.ballInitPos	= GameUtils.Convert(ballPos);
		shoot.ballState		= fogs.proto.msg.BallState.loseBall;

		shoot.open			= Convert.ToUInt32(bOpen);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, shoot, MsgID.AttackID);
	}

	
	public static void SendPrepareShoot(Player player, SkillInstance skill, Area area, AnimType type, bool bOpen)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack shoot		= new Attack();
		shoot.state			= CharacterState.ePrepareToShoot;
		shoot.char_id 		= player.m_roomPosId;

		shoot.skill			= new Skill();
		shoot.skill.skill_id	= skill.skill.id;
		shoot.pos			= GameUtils.Convert(player.position);
        shoot.rotate = player.rotation.eulerAngles.y.ToUnity2();
		shoot.area			= area;
		shoot.time_stamp = System.DateTime.Now.Ticks;
		
		shoot.open			= Convert.ToUInt32(bOpen);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, shoot, MsgID.AttackID);
	}

	public static void SendFakeShoot(Player player, bool bCancel)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		PrepareToShoot prepareShoot	= new PrepareToShoot();
		prepareShoot.char_id 		= player.m_roomPosId;
		prepareShoot.cancel			= Convert.ToUInt32(bCancel);
		prepareShoot.time_stamp		= GameSystem.Instance.mNetworkManager.m_dServerTime;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PrepareToShoot>(0, prepareShoot, MsgID.PrepareToShootID);
	}

	public static void SendLayup(Player player, SkillInstance skillInst, Area area)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack layup		= new Attack();
		layup.state			= CharacterState.eLayup;
		layup.char_id 		= player.m_roomPosId;

		layup.skill			= new Skill();
		layup.skill.skill_id = skillInst.skill.id;
		layup.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		layup.skill.action_id = skillInst.curAction.id;

		layup.pos			= GameUtils.Convert(player.position);
        layup.rotate = player.rotation.eulerAngles.y.ToUnity2();
		layup.area			= area;
		layup.ballState		= fogs.proto.msg.BallState.useBall_Shoot;
		layup.time_stamp = System.DateTime.Now.Ticks;
		
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, layup, MsgID.AttackID);
	}

	public static void SendBlockSuccess(Player player)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BlockSuccess bs = new BlockSuccess();
		bs.char_id = player.m_roomPosId;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BlockSuccess>(0, bs, MsgID.BlockSuccessID);
	}

	public static void SendBlock(Player player, Vector3 vVel, bool blockable, float blockRate, 
	                             float blockValue, SkillInstance skillInst, bool bSuccess, 
	                             Vector3 dirBlockedBall, Vector3 vBlockBallPos, bool bBlockPass, bool bValid)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Block block			= new Block();
		block.char_id 		= player.m_roomPosId;

		block.skill			= new Skill();
		block.skill.skill_id = skillInst.skill.id;
		block.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		block.skill.action_id = skillInst.curAction.id;
		
		block.pos			= GameUtils.Convert(player.position);
        block.rotate = player.rotation.eulerAngles.y.ToUnity2();
		block.block_rate	= blockRate;
		block.block_value	= blockValue;
		block.blockable		= Convert.ToUInt32(blockable);
		block.success		= Convert.ToUInt32(bSuccess);

		block.ballPos		= GameUtils.Convert(vBlockBallPos);
		block.ballDir		= GameUtils.Convert(dirBlockedBall);
		block.velocity		= GameUtils.Convert(vVel);
		block.time_stamp = System.DateTime.Now.Ticks;
		

		block.valid			= Convert.ToUInt32(bValid);

		if( bBlockPass )
			block.passTarget = player.m_passTarget.m_roomPosId;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Block>(0, block, MsgID.BlockID);
	}

	public static void SendInterception(Player player, SkillInstance skillInst, Player passer, Player catcher, AnimType type, bool bGetBall, Vector3 vBallInitPos, Vector3 vBallInitVel)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Interception interception = new Interception();
		interception.time_stamp = System.DateTime.Now.Ticks;
		
		interception.char_id 			= player.m_roomPosId;
		interception.passer_char_id 	= passer.m_roomPosId;
		interception.catcher_char_id 	= passer.m_roomPosId;
		interception.pos				= GameUtils.Convert(player.position);
        interception.rotate             = player.rotation.eulerAngles.y.ToUnity2();
		interception.actionType			= type;

		interception.skill						= new Skill();
		interception.skill.skill_id 			= skillInst.skill.id;
		interception.skill.skill_matchedKeyIdx 	= skillInst.matchedKeyIdx;
		interception.skill.action_id 			= skillInst.curAction.id;
		
		interception.actionType			= type;
		interception.blockedBallPos		= GameUtils.Convert(vBallInitPos);
		interception.blockedBallDir		= GameUtils.Convert(vBallInitVel);

		interception.getBall			= Convert.ToUInt32(bGetBall);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Interception>(0, interception, MsgID.InterceptionID);
	}

	public static void SendPass(Player player, SkillInstance skillInst, Player interceptor, Vector3 vInterceptedBallPos, AnimType type, Player target)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Pass pass 			= new Pass();
		pass.char_id		= player.m_roomPosId;
		pass.target_char_id	= target.m_roomPosId;
		pass.pos			= GameUtils.Convert(player.position);
        pass.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		pass.actionType		= type;

		pass.skill			= new Skill();
		pass.skill.skill_id	= skillInst.skill.id;
		pass.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;
		pass.skill.action_id = skillInst.curAction.id;
		pass.time_stamp = System.DateTime.Now.Ticks;
		

		pass.interceptor = interceptor == null ? 0 : interceptor.m_roomPosId;
		pass.ballInterceptedPos = GameUtils.Convert(vInterceptedBallPos);
		pass.ballInterceptedDir = new SVector3();

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Pass>(0, pass, MsgID.PassBallID);
	}

	public static void SendCatch(Player player, SkillInstance skillInst, AnimType type)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Catch catchball 	= new Catch();
		catchball.char_id	= player.m_roomPosId;
		catchball.pos		= GameUtils.Convert(player.position);
        catchball.rotate    = player.rotation.eulerAngles.y.ToUnity2();
		catchball.actionType = type;
		catchball.time_stamp = System.DateTime.Now.Ticks;
		
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Catch>(0, catchball, MsgID.CatchBallID);
	}

	public static void SendPickAndRoll(Player player, SkillInstance skillInst, AnimType animType)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		PickAndRoll	pickRoll = new PickAndRoll();
		pickRoll.char_id		= player.m_roomPosId;
		pickRoll.pos			= GameUtils.Convert(player.position);
        pickRoll.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		pickRoll.actionType		= animType;
		
		pickRoll.skill			= new Skill();
		pickRoll.skill.skill_id	= skillInst.skill.id;
		pickRoll.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;
		pickRoll.skill.action_id = skillInst.curAction.id;
		pickRoll.time_stamp = System.DateTime.Now.Ticks;
		

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PickAndRoll>(0, pickRoll, MsgID.PickAndRoleID);
	}

	public static void SendBePickAndRolled(Player player, AnimType type)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		BePickAndRoll	pickRoll = new BePickAndRoll();
		pickRoll.char_id		= player.m_roomPosId;
		pickRoll.pos			= GameUtils.Convert(player.position);
        pickRoll.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		pickRoll.actionType		= type;
		
		pickRoll.time_stamp = System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BePickAndRoll>(0, pickRoll, MsgID.BePickAndRoleID);
	}

	public static void SendHold(Player player)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Hold hold			= new Hold();
		hold.char_id 		= player.m_roomPosId;
		hold.pos			= GameUtils.Convert(player.position);
        hold.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		hold.movedWithBall	= Convert.ToUInt32(player.m_bMovedWithBall);
		hold.time_stamp = System.DateTime.Now.Ticks;
		
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Hold>(0, hold, MsgID.HoldID);
	}


	public static void SendDown(Player player, DownType downType, AnimType type, Vector3 vBallInitPos, Vector3 vBallInitVel)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Down down			= new Down();
		down.char_id 		= player.m_roomPosId;
		down.pos			= GameUtils.Convert(player.position);
        down.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		down.actionType		= type;

		down.ballInitPos	= GameUtils.Convert(vBallInitPos);
		down.ballInitVel	= GameUtils.Convert(vBallInitVel);
		down.downType		= downType;
		down.time_stamp = System.DateTime.Now.Ticks;
		
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Down>(0, down, MsgID.DownID);
	}

	public static void SendFallDown(Player player)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		FallDown fall		= new FallDown();
		fall.char_id 		= player.m_roomPosId;
		fall.pos			= GameUtils.Convert(player.position.ToUnity2());
		fall.rotate			= player.rotation.eulerAngles.y.ToUnity2();
		fall.time_stamp = System.DateTime.Now.Ticks;
		

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<FallDown>(0, fall, MsgID.FallDownID);
	}

	public static void SendRequireBall(Player player, SkillInstance skillInst, AnimType animType )
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		RequireBall	requireBall = new RequireBall();
		requireBall.char_id	= player.m_roomPosId;
		requireBall.pos		= GameUtils.Convert(player.position);
        requireBall.rotate  = player.rotation.eulerAngles.y.ToUnity2();
		requireBall.time_stamp = System.DateTime.Now.Ticks;
		

		requireBall.skill	= new Skill();
		requireBall.skill.skill_id = skillInst.skill.id;
		requireBall.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		requireBall.skill.action_id = skillInst.curAction.id;
		
		requireBall.actionType = animType;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<RequireBall>(0, requireBall, MsgID.RequireBallID);
	}

	public static void SendDunk(Player player, SkillInstance skillInst, Area area)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		
		Attack dunk			= new Attack();
		dunk.state			= CharacterState.eDunk;
		dunk.char_id 		= player.m_roomPosId;

		dunk.skill			= new Skill();
		dunk.skill.skill_id	= skillInst.skill.id;
		dunk.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;
		dunk.skill.action_id = skillInst.curAction.id;

		dunk.pos			= GameUtils.Convert(player.position);
        dunk.rotate         = player.rotation.eulerAngles.y.ToUnity2();
		dunk.area			= area;
		dunk.ballState		= fogs.proto.msg.BallState.useBall_Shoot;
		dunk.time_stamp = System.DateTime.Now.Ticks;
		

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Attack>(0, dunk, MsgID.AttackID);
	}

	public static void SendSteal(Player player, Player target, SkillInstance skillInst, bool bGetBall, bool bValid )
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Steal steal			= new Steal();
		steal.char_id 		= player.m_roomPosId;
		steal.pos			= GameUtils.Convert(player.position);
        steal.rotate        = player.rotation.eulerAngles.y.ToUnity2();
		steal.valid			= Convert.ToUInt32(bValid);

		if( target != null )
			steal.target_char_id = target.m_roomPosId;

		steal.skill			= new Skill();
		steal.skill.skill_id = skillInst.skill.id;
		steal.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		steal.skill.action_id = skillInst.curAction.id;
		steal.time_stamp = System.DateTime.Now.Ticks;
		
		steal.actionType	= AnimType.B_TYPE_0;
		steal.getBall		= Convert.ToUInt32(bGetBall);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Steal>(0, steal, MsgID.StealID);
	}

	public static void SendStolen(Player stealer, Player stolener, bool bLostBall, Vector3 vBallPos, Vector3 dirBallVel)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null || conn is VirtualNetworkConn )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		Stolen stolen		= new Stolen();
		stolen.char_id 		= stolener.m_roomPosId;
		stolen.stolen_char_id = stealer.m_roomPosId;
		stolen.pos			= GameUtils.Convert(stolener.position);
        stolen.rotate       = stolener.rotation.eulerAngles.y.ToUnity2();
		stolen.ballPos		= GameUtils.Convert(vBallPos);
		stolen.ballDir		= GameUtils.Convert(dirBallVel);

		stolen.time_stamp = System.DateTime.Now.Ticks;
		

		stolen.lostBall		= Convert.ToUInt32(bLostBall);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Stolen>(0, stolen, MsgID.StolenID);
	}

	public static void SendRebound(Player player, bool bSuccess, SkillInstance skillInst, Vector3 vMove, float reboundTime, uint attr, bool bValid)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		MatchState.State curState = match.m_stateMachine.m_curState.m_eState;
		if( _FilterOutMsgByMatchState() && curState != MatchState.State.eTipOff)
			return;

		Rebound rebound		= new Rebound();
		rebound.char_id 	= player.m_roomPosId;
		rebound.pos			= GameUtils.Convert(player.position);
        rebound.rotate      = player.rotation.eulerAngles.y.ToUnity2();
		rebound.success		= Convert.ToUInt32(bSuccess);
		rebound.velocity	= GameUtils.Convert(vMove);
		rebound.actionType	= AnimType.B_TYPE_0;
		rebound.time		= reboundTime;
		rebound.attr		= attr;

		rebound.time_stamp = System.DateTime.Now.Ticks;
		

		rebound.skill		= new Skill();
		rebound.skill.skill_id	= skillInst.skill.id;
		rebound.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		rebound.skill.action_id = skillInst.curAction.id;

		rebound.valid			= Convert.ToUInt32(bValid);

		Logger.Log("rebound attr: " + attr);

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Rebound>(0, rebound, MsgID.ReboundID);
	}

	public static void SendBodyThrowCatch(Player player, bool bSuccess, SkillInstance skillInst, Vector3 throwDir, Vector3 ballPos, Vector3 ballDir, bool bValid)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		MatchState.State curState = match.m_stateMachine.m_curState.m_eState;
		if( _FilterOutMsgByMatchState() )
			return;

		BodyThrowCatch bodyThrowCatch 	= new BodyThrowCatch();
		bodyThrowCatch.char_id 			= player.m_roomPosId;
		bodyThrowCatch.pos				= GameUtils.Convert(player.position);
        bodyThrowCatch.rotate           = player.rotation.eulerAngles.y.ToUnity2();
		bodyThrowCatch.success			= Convert.ToUInt32(bSuccess);
		bodyThrowCatch.velocity			= GameUtils.Convert(throwDir);
		bodyThrowCatch.actionType		= AnimType.B_TYPE_0;
		bodyThrowCatch.time_stamp = System.DateTime.Now.Ticks;
		
		bodyThrowCatch.skill			= new Skill();
		bodyThrowCatch.skill.skill_id	= skillInst.skill.id;
		bodyThrowCatch.skill.skill_matchedKeyIdx = skillInst.matchedKeyIdx;
		bodyThrowCatch.skill.action_id = skillInst.curAction.id;

		bodyThrowCatch.ballPos			= GameUtils.Convert(ballPos);
		bodyThrowCatch.ballVelocity		= GameUtils.Convert(ballDir);

		bodyThrowCatch.valid			= Convert.ToUInt32(bValid);
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BodyThrowCatch>(0, bodyThrowCatch, MsgID.BodyThrowCatchID);
	}

	public static void SendGameBegin()
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null)
			return;

		GameBeginReq gameBeginReq = new GameBeginReq();
		gameBeginReq.acc_id = MainPlayer.Instance.AccountID;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<GameBeginReq>(0, gameBeginReq, MsgID.GameBeginReqID);
	}

	public static void SendCutIn(Player player, SkillInstance skillInst)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;

		CutIn cutIn 			= new CutIn();
		cutIn.char_id 			= player.m_roomPosId;
		cutIn.pos				= GameUtils.Convert(player.position);
        cutIn.rotate            = player.rotation.eulerAngles.y.ToUnity2();
		cutIn.actionType		= AnimType.N_TYPE_0;

		cutIn.time_stamp = System.DateTime.Now.Ticks;
		
		cutIn.skill				= new Skill();
		cutIn.skill.skill_id	= skillInst.skill.id;
		cutIn.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;
		cutIn.skill.action_id = skillInst.curAction.id;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<CutIn>(0, cutIn, MsgID.CutInID);
	}

	public static void SendGameGoal(Player goaler, uint pt, bool bCritical)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;

		GameGoal goal = new GameGoal();
		goal.index = goaler.m_roomPosId;
		goal.score = pt;
		goal.kill_goal = Convert.ToUInt32( bCritical );
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<GameGoal>(0, goal, MsgID.GameGoalID);
	}

	public static void SendTimeTracer(uint msgId, double dCurTime, NetworkConn server)
	{
		if( server == null || !server.IsConnected() )
			return;

		TimeTracer tracer = new TimeTracer();
		tracer.sendTimeStamp = dCurTime;
		tracer.id = msgId;
		server.SendPack<TimeTracer>(0, tracer, MsgID.TimeTracerID);
	}

    public static void SendPVPLoadProgress(uint perc)
    {
        if (GameSystem.Instance.mNetworkManager.m_gameConn == null)
            return;
        PVPLoadProgress obj = new PVPLoadProgress();
        obj.progress = perc;
        GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PVPLoadProgress>(0, obj, MsgID.PVPLoadProgressID);
    }

    public static void SendLoadingComplete(GameMatch.Type type)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		PVPLoadComplete obj = new PVPLoadComplete();
		obj.type = MatchType.MT_PVP_1V1_PLUS;
		if( type == GameMatch.Type.ePVP_3On3 )
			obj.type = MatchType.MT_PVP_3V3;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<PVPLoadComplete>(0, obj, MsgID.PVPLoadCompleteID);
	}

	public static void SendTipOff()
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		BeginTipOffReq obj = new BeginTipOffReq();
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BeginTipOffReq>(0, obj, MsgID.BeginTipOffReqID);
	}

	public static void SendSyncInput(Player player, int input)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		SyncInput obj = new SyncInput();
		obj.char_id = player.m_roomPosId;
		obj.time_stamp = System.DateTime.Now.Ticks;
		obj.input = input;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<SyncInput>(0, obj, MsgID.InputID);
	}

	public static void SendBackToBack(Player player, SkillInstance skillInst)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackToBack btb 		= new BackToBack();
		btb.char_id 		= player.m_roomPosId;
		btb.pos				= GameUtils.Convert(player.position);
        btb.rotate          = player.rotation.eulerAngles.y.ToUnity2();
		btb.actionType		= AnimType.B_TYPE_0;
		btb.time_stamp 		= System.DateTime.Now.Ticks;

		btb.skill			= new Skill();
		btb.skill.skill_id	= skillInst.skill.id;
		btb.skill.skill_matchedKeyIdx	= skillInst.matchedKeyIdx;
		btb.skill.action_id = skillInst.curAction.id;

		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackToBack>(0, btb, MsgID.BackToBackID);
	}

	public static void SendBackToBackForward(Player player, AnimType type)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackToBackForward bc 	= new BackToBackForward();
		bc.char_id 				= player.m_roomPosId;
		bc.pos					= GameUtils.Convert(player.position);
        bc.rotate               = player.rotation.eulerAngles.y.ToUnity2();
		bc.actionType			= type;
		bc.time_stamp 			= System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackToBackForward>(0, bc, MsgID.BackToBackForwardID);
	}

	public static void SendBackCompete(Player player, AnimType type, bool bCompeteWin)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackCompete bc 		= new BackCompete();
		bc.char_id 			= player.m_roomPosId;
		bc.pos				= GameUtils.Convert(player.position);
        bc.rotate           = player.rotation.eulerAngles.y.ToUnity2();
		bc.actionType		= type;
		bc.nSuccess			= Convert.ToUInt32(bCompeteWin);
		bc.time_stamp 		= System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackCompete>(0, bc, MsgID.BackCompeteID);
	}

	public static void SendBackBlock(Player player, AnimType type, bool bCompeteWin)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackBlock bc 		= new BackBlock();
		bc.char_id 			= player.m_roomPosId;
		bc.pos				= GameUtils.Convert(player.position);
        bc.rotate           = player.rotation.eulerAngles.y.ToUnity2();
		bc.actionType		= type;
		bc.nSuccess			= Convert.ToUInt32(bCompeteWin);
		bc.time_stamp 		= System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackBlock>(0, bc, MsgID.BackBlockID);
	}

	public static void SendBackToStand(Player player, AnimType type)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackToStand bc 		= new BackToStand();
		bc.char_id 			= player.m_roomPosId;
		bc.pos				= GameUtils.Convert(player.position);
        bc.rotate           = player.rotation.eulerAngles.y.ToUnity2();
		bc.actionType		= type;
		bc.time_stamp 		= System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackToStand>(0, bc, MsgID.BackToStandID);
	}

	public static void SendBackTurnRun(Player player, AnimType type)
	{
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		if( _FilterOutMsgByMatchState() )
			return;
		BackTurnRun bc 		= new BackTurnRun();
		bc.char_id 			= player.m_roomPosId;
		bc.pos				= GameUtils.Convert(player.position);
        bc.rotate           = player.rotation.eulerAngles.y.ToUnity2();
		bc.actionType		= type;
		bc.time_stamp 		= System.DateTime.Now.Ticks;
		
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<BackTurnRun>(0, bc, MsgID.BackTurnRunID);
	}

    public static void SendInput(InputDirection dir, Command cmd)
    {
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
        ClientInput input = new ClientInput();
        input.acc_id = MainPlayer.Instance.AccountID;
        input.dir = (uint)dir;
        input.cmd = (uint)cmd;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<ClientInput>(0, input, MsgID.ClientInputID);
        //Logger.Log("SendInput " + dir + " " + cmd);
    }

    //For virtual game server
    public static void SendTurn(FrameInfo turn)
    {
        NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn == null )
			return;
		conn.SendPack<FrameInfo>(0, turn, MsgID.FrameInfoID);
        //Logger.Log("VirtualGameServer, SendTurn " + turn.frameNum);

        //如果使用的是虚拟连接，保证消息立即传回客户端
        if (conn.m_type == NetworkConn.Type.eVirtualServer)
            conn.Update(0f);
    }

    //For virtual game server
    public static void SendGameBeginResp(GameBeginResp resp)
    {
		if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
			return;
		GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<GameBeginResp>(0, resp, MsgID.GameBeginRespID);
    }
}