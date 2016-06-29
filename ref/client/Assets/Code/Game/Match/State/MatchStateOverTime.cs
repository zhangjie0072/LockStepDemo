using System;
using UnityEngine;
using fogs.proto.msg;

public class MatchStateOverTime : MatchState
{
    static IM.Number DURATION = new IM.Number(2, 500) + new IM.Number(2, 667);

	public TrackPlayer m_trackPlayer = new TrackPlayer();
	//private GameUtils.Timer		m_timer;
	private GameObject			m_uiOverTime;
	private bool				m_bPlayOverTime = false;

	private	GameObject 			m_goDrawGameMsg;
    private GameUtils.Timer _timer;

	public MatchStateOverTime(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eOverTime;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.moveDirection = IM.Vector3.zero;
			player.m_toSkillInstance = null;
			player.m_bToCatch = false;
			player.m_bOnGround = true;
			player.m_passTarget = null;
			player.DropBall(m_match.mCurScene.mBall);
		}

		m_match.m_bOverTime = true;

		m_goDrawGameMsg = UIManager.Instance.CreateUI("msg/Msg_DrawGame");
		m_goDrawGameMsg.SetActive(false);

		_OnPlayTrack();

        _timer = new GameUtils.Timer(DURATION, OnOverTimeFinish);
	}

	void _ShowRule()
	{
		GameObject.Destroy(m_goDrawGameMsg);

		m_uiOverTime = UIManager.Instance.CreateUI("UIOvertime");
		NGUITools.BringForward(m_uiOverTime);
	}

	void OnOverTimeFinish()
	{
		if( m_uiOverTime != null )
		{
			GameObject.Destroy(m_uiOverTime.gameObject);
			m_uiOverTime = null;
		}

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
			Debug.LogError("can not find goOverTime prefab.");

		m_goDrawGameMsg.SetActive(true);
		NGUITools.BringForward(m_goDrawGameMsg);

		m_trackPlayer.LoadTrack(goOverTime);
		m_trackPlayer.Play( m_match.mainRole.m_team.m_side == Team.Side.eHome ? 0 : 1);

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.MatchOverTime);

		GameObject.Destroy(m_uiOverTime);
	}

	override public void ViewUpdate (float deltaTime)
	{
		base.ViewUpdate(deltaTime);

		if( m_trackPlayer != null )
			m_trackPlayer.Update(deltaTime);
		
		if( m_trackPlayer.m_play )
			return;

		if( !m_bPlayOverTime )
		{
			_ShowRule();
			m_bPlayOverTime = true;
		}
	}

    public override void GameUpdate(IM.Number fDeltaTime)
    {
        base.GameUpdate(fDeltaTime);
        _timer.Update(fDeltaTime);
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
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        foreach(Player player in pm)
        {
            player.m_applyLogicPostion = true;
        }
	}
}
