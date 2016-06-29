using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Interception : PlayerState_Skill
{
	private string m_cachedAction;

	private IM.Number	m_fActDistance = IM.Number.zero;
	private IM.Number 	m_fEventTime = IM.Number.zero;
	private bool	m_bOnEvent = false;
	private bool	m_bActing = false;

	public Player	m_passer;
	public Player	m_catcher;

	public bool		m_bGetBall = false;
	public bool		m_bSendMsg = false;

	public PlayerState_Interception (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = PlayerState.State.eInterception;
	}

	public bool CanIntercept(SkillInstance intercept, Player passer, Player catcher, out IM.Vector3 ballPosWhenIntercepted)
	{
		ballPosWhenIntercepted = IM.Vector3.zero;
		
		PlayerState.State curState = m_player.m_StateMachine.m_curState.m_eState;
		if( curState != State.eRun && curState != State.eStand && curState != State.eDefense && curState != State.eRush )
			return false;

		IM.Vector3 dirPasserToCatcher = GameUtils.HorizonalNormalized(catcher.position, passer.position);
		IM.Vector3 dirOriginFace = m_player.forward;

		m_player.forward = -dirPasserToCatcher;

		string action = _ParseAction(intercept.curAction.action_id, intercept.matchedKeyIdx);
		PlayerAnimAttribute.AnimAttr animAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Interception, action);
		if (animAttr == null)
			Debug.LogError("Can not find animAttr: " + m_curAction);
		
		PlayerAnimAttribute.KeyFrame keyFrame = animAttr.GetKeyFrame("OnBlock");
        IM.Number frameRate = m_player.animMgr.GetFrameRate(action);
        IM.Number fEventTime = keyFrame.frame / frameRate;
		IM.Number fBallFlyDistance = passer.m_speedPassBall * fEventTime;
		IM.Number fDistPlayer2Passer = GameUtils.HorizonalDistance(passer.position, m_player.position);
		if( fDistPlayer2Passer < fBallFlyDistance )
		{
			m_player.forward = dirOriginFace;
			return false;
		}

        if (!m_player.GetNodePosition(SampleNode.Ball, action, fEventTime, out ballPosWhenIntercepted))
        {
            m_player.forward = dirOriginFace;
            return false;
        }

		m_player.forward = dirOriginFace;
		return true;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		IM.Vector3 dirPasserToCatcher = GameUtils.HorizonalNormalized(m_catcher.position, m_passer.position);
        if (m_bSendMsg)
            m_player.forward = -dirPasserToCatcher;
			
		m_player.m_enablePickupDetector = false;
		m_player.animMgr.GetRootMotion(m_curAction).Reset();
		
		m_player.m_enableMovement = false;
		m_player.m_enableAction = false;
		
		PlayerAnimAttribute.AnimAttr animAttr = m_player.m_animAttributes.GetAnimAttrById(Command.Interception, m_curAction);
		if (animAttr == null)
			Debug.LogError("Can not find animAttr: " + m_curAction);
		
		PlayerAnimAttribute.KeyFrame keyFrame = animAttr.GetKeyFrame("OnBlock");
        IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
        m_fEventTime = keyFrame.frame / frameRate;
		
		IM.Vector3 ballPosWhenBlocked;
        if (!m_player.GetNodePosition(SampleNode.Ball, m_curAction, m_fEventTime, out ballPosWhenBlocked))
			Debug.LogError("Can not get bone position");
		
		Debugger.Instance.DrawSphere("block", (Vector3)ballPosWhenBlocked, Color.yellow);
		
		m_fActDistance = m_passer.m_speedPassBall * m_fEventTime;
		m_bOnEvent = false;
		m_bActing = false;
		
		m_cachedAction = m_curAction;
		m_curAction = "";

		Debug.Log("animType: " + m_animType);
		++m_player.mStatistics.data.interception;
	}

	protected override string _OnIntepretAction (string lHandActionId, string rHandActionId)
	{
		if( m_bSendMsg )
		{
			IM.Vector3 dirPasserToCatcher = GameUtils.HorizonalNormalized(m_catcher.position, m_passer.position);
			IM.Vector3 dirPasserToInterceptor = GameUtils.HorizonalNormalized(m_player.position,m_passer.position);
			IM.Number fDir = IM.Vector3.Cross(dirPasserToCatcher, dirPasserToInterceptor).y;
			m_animType = fDir < IM.Number.zero ? AnimType.N_TYPE_0 : AnimType.N_TYPE_1;

			Debug.Log("animType: " + m_animType);
		}

		return m_animType == AnimType.N_TYPE_0 ? lHandActionId : rHandActionId;
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);
		if( !m_bActing && m_ball.m_ballState == BallState.eUseBall_Pass
		   && GameUtils.HorizonalDistance(m_ball.m_interceptedPos, m_ball.position) < m_fActDistance )
		{
			m_curAction = m_cachedAction;
			m_player.animMgr.Play(m_curAction, true);
			m_bActing = true;
			m_time = IM.Number.zero;
		}

		if( m_bActing && m_time > m_fEventTime && !m_bOnEvent )
		{
			m_bOnEvent = true;
			_OnBlock();
		}
	}

	private void _OnBlock()
	{
		Debug.Log("onblock");

		if( m_ball.m_owner != null )
		{
			m_ball.m_owner.DropBall(m_ball);
			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eStolen);
		}

		if( m_bGetBall )
		{
			m_player.GrabBall(m_ball, true);
			m_match.m_ruler.m_toCheckBallTeam = m_player.m_team;
			
			m_player.m_StateMachine.SetState(State.eHold);
			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePickupBall);
		}
		else
		{
			m_ball.m_ballState = BallState.eLoseBall;
			m_ball.SetInitPos(m_player.ballSocketPos);
			m_ball.initVel = m_passer.forward;
		}
		
		if( m_ball.onIntercepted != null )
			m_ball.onIntercepted(m_ball);

		m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);
		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eIntercepted);
	}

	public override void OnExit ()
	{
		m_player.m_enableMovement = true;
		m_player.m_enableAction = true;

		m_bOnEvent = false;
		m_bActing = false;
		m_bSendMsg = false;

		m_player.m_enablePickupDetector = true;
	}
}
