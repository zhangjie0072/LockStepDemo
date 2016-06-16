using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Rebound : PlayerState_Skill
{
	public bool m_success = false;
	public bool tooLate = false;	// true: too late, false: too early
	public IM.Number m_heightScale = IM.Number.zero;
    public IM.Number rootMotionScale = IM.Number.one;

	public bool m_toReboundBall = false;

	private IM.Number m_fEventTime = IM.Number.zero;

	public PlayerState_Rebound (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eRebound;

		/*
		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_0, "rebound");
		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_1, "reboundL");
		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_2, "reboundR");
		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_3, "reboundB");
		m_animType = AnimType.N_TYPE_0;
		*/
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		tooLate = false;

		//Logger.Log("rebound action is: " + m_curAction );

		if( !m_player.m_bSimulator )
		{
			m_heightScale = IM.Number.one;
			m_success = true;
			if( m_ball.m_bGoal )
				m_success = false;
			if( m_ball.m_picker != null )
				m_success = false;
			if( m_ball.m_ballState != BallState.eRebound )
				m_success = false;

			Dictionary<string, uint> skillAttr = m_player.GetSkillAttribute();
			Dictionary<string, uint> data = m_player.m_finalAttrs;
			if( data == null )
			{
				Logger.LogError("Can not find data.");
				m_success = false;
			}
			if( !m_match.m_ruler.CanRebound() || m_ball.m_owner != null )
			{
				m_success = false;
				tooLate = false;
				Logger.Log(m_player.m_name + " Rebound failed, ball haven't been reached the highest position or ball has owner");
			}

			//Logger.Log("Rebound distance:" + m_player.m_fReboundDist);
			IM.Number fDistPlayer2Ball = GameUtils.HorizonalDistance(m_player.position, m_ball.position);

			IM.Number reboundDist = PlayerState_Rebound.GetMaxDist(m_player);
			if( fDistPlayer2Ball > reboundDist)
			{
				m_success = false;
				Logger.Log("player to ball distance: " + fDistPlayer2Ball + " farther than rebound distance: " + reboundDist);
			}

			IM.Number minHeight, maxHeight;
			GetHeightRange(m_player, out minHeight, out maxHeight);

			IM.Number fEventTime = GetEventTime(m_player, m_curAction);

			if( m_success )
			{
				IM.Vector3 vBallPosRebound;
                IM.Number fCurTime = m_ball.m_fTime;
                IM.Number fHighestTime = m_ball.CompleteLastCurve().GetHighestTime();
				if (fCurTime + fEventTime < fHighestTime)	//������
				{
					m_success = false;
					tooLate = false;
					Logger.Log("Rebound failed, ball not in falling.");
				}
				else if( m_ball.GetPositionInAir( (fCurTime + fEventTime), out vBallPosRebound) )
				{
					if( vBallPosRebound.y > minHeight && vBallPosRebound.y < maxHeight)
					{
						//Debugger.Instance.DrawSphere("Rebound", vBallPosRebound, Color.red);
						m_player.FaceTo(vBallPosRebound);
						IM.Vector3 reboundAnimBall;
						m_player.GetNodePosition(SampleNode.Ball, m_curAction, fEventTime, out reboundAnimBall);
						m_heightScale = vBallPosRebound.y / reboundAnimBall.y;

						IM.Vector3 root;
						m_player.GetNodePosition(SampleNode.Root, m_curAction, fEventTime, out root);
						IM.Vector3 root2Ball = reboundAnimBall - root;

						IM.Number fDistPlayerToReboundPos = GameUtils.HorizonalDistance(vBallPosRebound - root2Ball, m_player.position);
						IM.Number fDistOrigReboundPos = GameUtils.HorizonalDistance(root, m_player.position);
						rootMotionScale = fDistPlayerToReboundPos / fDistOrigReboundPos;
						//Logger.Log("root motion scale: " + m_player.m_rootMotion.m_scale);
					}
					else
					{
						m_success = false;
						tooLate = vBallPosRebound.y <= minHeight;
						Logger.Log("Rebound failed, ball height: " + m_ball.transform.position.y + " height range: min: " + minHeight + " ,max: " + maxHeight);
					}
				}
				else
				{
					m_success = false;
					tooLate = true;
					Logger.Log("reboundTime out of the curve, too slow");
				}
			}
			else
				m_player.FaceTo(m_ball.position);

			//m_curAction = m_mapAnimType[m_animType];
			IM.Vector3 vRoundScale = new IM.Vector3(rootMotionScale, m_heightScale, rootMotionScale);

			uint skillValue = 0;
			m_player.m_skillSystem.HegdingToValue("addn_rebound", ref skillValue);

			GameMsgSender.SendRebound(m_player, m_success, m_curExecSkill, (Vector3)vRoundScale, (float)fEventTime, m_player.m_finalAttrs["rebound"] + skillValue, m_success);
			++m_player.mStatistics.data.rebound_times;
			if (m_success)
				++m_player.mStatistics.data.valid_rebound_times;
		}
		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision(m_player, true);
		m_fEventTime = GetEventTime(m_player, m_curAction);
		IM.RootMotion rootMotion = m_player.animMgr.Play(m_curAction, true).rootMotion;
        rootMotion.scale = rootMotionScale;
        rootMotion.Reset();
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone();

		if( m_player.m_bWithBall && m_player.m_moveType == MoveType.eMT_Stand)
			m_stateMachine.SetState(PlayerState.State.eHold);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_player.m_bWithBall )
			return;
			
		if( m_toReboundBall && m_time > m_fEventTime )
			_OnRebound();
 	}

	public override void OnGround ()
	{
		base.OnGround ();
		m_player.animMgr.GetRootMotion(m_curAction).scale = IM.Number.one;
	}

	public void OnRebound()
	{
		//Debugger.Instance.DrawSphere("ReboundBone", m_ball.transform.position, Color.white);
		//m_toReboundBall = true;
		//OnRebound();
	}

	void _OnRebound()
	{
		if( !m_toReboundBall )
			return;

		if( m_ball.m_owner != null )
			m_ball.m_owner.DropBall(m_ball);

		if (m_ball.m_actor != null && m_ball.m_actor.m_team != m_player.m_team )
		{
			MatchState.State curState = m_match.m_stateMachine.m_curState.m_eState;
			if( curState == MatchState.State.ePlaying)
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Rebound);
		}
		m_player.GrabBall(m_ball);

		m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.GrabBall);
		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eRebound);

		Logger.Log( m_player.m_id + " rebound success and grab ball" );
		m_toReboundBall = false;
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_success = false;
		m_heightScale = IM.Number.zero;
		m_animType = fogs.proto.msg.AnimType.N_TYPE_0;
		m_toReboundBall = false;
		m_player.animMgr.GetRootMotion(m_curAction).scale = IM.Number.one;
		GameSystem.Instance.mClient.mPlayerManager.IsolateCollision( m_player, false );
	}

	public static IM.Number GetEventTime(Player player, string action)
	{
        string originAction = player.animMgr.GetOriginName(action);
		Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = player.m_animAttributes.m_rebound;
		PlayerAnimAttribute.AnimAttr attr = null;
		if( !rebounds.TryGetValue(originAction, out attr) )
		{
			Logger.LogError("Can not find AnimAttr in clip: " + originAction);
			return IM.Number.zero;
		}

		PlayerAnimAttribute.KeyFrame reboundFrame = attr.GetKeyFrame("OnRebound");
		if( reboundFrame == null )
		{
			Logger.LogError("Can not find OnRebound key in clip: " + originAction);
			return IM.Number.zero;
		}
		int reboundKey = reboundFrame.frame;
        return reboundKey / player.animMgr.GetFrameRate(action);
	}

	public static IM.Number GetEventTime(Player player)
	{
		List<SkillInstance> skills = player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound);
		string action = skills[0].skill.actions[0].action_id;
		return GetEventTime(player, action);
	}

	public static void GetHeightRange(Player player, out IM.Number minHeight, out IM.Number maxHeight, SkillInstance skillInst = null)
	{
		minHeight = new IM.Number(1,600);
		SkillSpec skillspec = player.GetSkillSpecialAttribute(SkillSpecParam.eRebound_height, skillInst);
        maxHeight = skillspec.value;
	}

	public static void GetDefaultHeightRange(Player player, out IM.Number minHeight, out IM.Number maxHeight)
	{
		List<SkillInstance> skills = player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound);
		GetHeightRange(player, out minHeight, out maxHeight, skills[0]);
	}

	public static IM.Number GetMaxDist(Player player, SkillInstance skillInst = null)
	{
		SkillSpec skillspec = player.GetSkillSpecialAttribute(SkillSpecParam.eRebound_dist, skillInst);
        return skillspec.value;
	}

	public static IM.Number GetDefaultMaxDist(Player player)
	{
		List<SkillInstance> skills = player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound);
		return GetMaxDist(player, skills[0]);
	}
}

