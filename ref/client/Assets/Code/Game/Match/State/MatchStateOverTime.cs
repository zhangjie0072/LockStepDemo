using System;
using UnityEngine;
using fogs.proto.msg;

public class MatchStateOverTime
	: MatchState, InputManager.Listener
{	
	public TrackPlayer m_trackPlayer = new TrackPlayer();
	//private GameUtils.Timer		m_timer;
	private GameObject			m_uiOverTime;
	private bool				m_bPlayOverTime = false;
	private bool				m_bSendGameBegin = false;

	private	GameObject 			m_goDrawGameMsg;

	public MatchStateOverTime(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eOverTime;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_smcManager = new SimulateCommandManager();
			player.moveDirection = IM.Vector3.zero;
			player.m_toSkillInstance = null;
			player.m_bToCatch = false;
			player.m_bOnGround = true;
			player.m_passTarget = null;
			player.DropBall(m_match.mCurScene.mBall);
		}

		m_match.m_bOverTime = true;
		m_bSendGameBegin = false;

		m_goDrawGameMsg = UIManager.Instance.CreateUI("msg/Msg_DrawGame");
		m_goDrawGameMsg.SetActive(false);

		_OnPlayTrack();
	}

	void _ShowRule()
	{
		GameObject.Destroy(m_goDrawGameMsg);

		m_uiOverTime = UIManager.Instance.CreateUI("UIOvertime");
		UIFinishEvent finEvent = m_uiOverTime.GetComponent<UIFinishEvent>();
		if( finEvent == null )
			finEvent = m_uiOverTime.AddComponent<UIFinishEvent>();
		finEvent.onFinish += _OnUIOverTimeFinish;
		NGUITools.BringForward(m_uiOverTime);
	}

	void _OnUIOverTimeFinish()
	{
		if( m_uiOverTime != null )
		{
			GameObject.Destroy(m_uiOverTime.gameObject);
			m_uiOverTime = null;
		}

		if( m_match.leagueType == GameMatch.LeagueType.ePVP || 
			(m_match.leagueType == GameMatch.LeagueType.eRegular1V1 && m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS) ||
			(m_match.leagueType == GameMatch.LeagueType.eQualifyingNew && m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS))
		{
			if( !m_bSendGameBegin )
			{
				GameMsgSender.SendGameBegin();
				m_bSendGameBegin = true;
			}
		}
		else
			m_stateMachine.SetState(State.ePlayerCloseUp);
	}

	void _OnPlayTrack()
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.Show( !(m_match is GameMatch_PVP) );
			
			player.model.EnableGrey(false);
			
			player.m_enableAction = false;
			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = false;
			
			player.m_enablePickupDetector = false;
			player.m_enableMovement = false;
			player.m_enableAction = false;
			
			if( player.m_InfoVisualizer != null )
				player.m_InfoVisualizer.SetActive(false);
			if( player.m_AOD != null )
				player.m_AOD.visible = false;
			
			player.HideIndicator();
			
			if( player.m_aiMgr != null )
				player.m_aiMgr.m_enable = false;
		}
		
		if (m_match.m_uiMatch != null)
		{
			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}
		
		GameObject resOverTime = ResourceLoadManager.Instance.LoadPrefab("Prefab/Camera/OverTime");
		GameObject goOverTime = GameObject.Instantiate(resOverTime) as GameObject;
		if( goOverTime == null )
			Logger.LogError("can not find goOverTime prefab.");

		m_goDrawGameMsg.SetActive(true);
		NGUITools.BringForward(m_goDrawGameMsg);

		m_trackPlayer.LoadTrack(goOverTime);
		m_trackPlayer.Play( m_match.m_mainRole.m_team.m_side == Team.Side.eHome ? 0 : 1);

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.MatchOverTime);

		GameObject.Destroy(m_uiOverTime);
	}
	
	void OnTimer()
	{
		GameSystem.Instance.mClient.mInputManager.AddListener(this);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_trackPlayer != null )
			m_trackPlayer.Update((float)fDeltaTime);
		
		if( m_trackPlayer.m_play )
			return;

		if( !m_bPlayOverTime )
		{
			_ShowRule();
			m_bPlayOverTime = true;
		}
	}
	
	override public void OnExit ()
	{
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if( curMatch != null && curMatch.m_camFollowPath != null )
		{
			curMatch.m_camFollowPath.Stop();
			curMatch.m_camFollowPath.enabled = false;
			curMatch.m_cam.enabled = true;
		}
		if( m_uiOverTime != null )
			GameObject.Destroy(m_uiOverTime);

		m_match.m_needTipOff = true;
	}

	public void OnPress( int nTouchID, Vector2 vScreenPt, bool bDown, out bool bPassThrough )
	{
		bPassThrough = true;
	}
	public void OnClick( int nTouchID, Vector2 vScreenPt, out bool bPassThrough )
	{
		bPassThrough = false;
	}
	public void OnDrag( int nTouchID, Vector2 vScreenPtCur, Vector2 vScreenPtDelta, out bool bPassThrough )
	{
		bPassThrough = true;
	}
	public void OnDragEnd( int nTouchID )
	{
	}
}
