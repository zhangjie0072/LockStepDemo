using System;
using UnityEngine;
using System.Collections.Generic;

public class MatchStateOpening
	: MatchState, InputManager.Listener
{	
	private TrackPlayer m_trackPlayer = new TrackPlayer();

	private List<GameObject>	m_goEffects = new List<GameObject>();

    private GameUtils.Timer4View m_timer;

	public MatchStateOpening(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eOpening;
	}

	void OnTimer()
	{
		GameSystem.Instance.mClient.mInputManager.AddListener(this);
	}

	override public void OnEnter ( MatchState lastState )
	{
		m_goEffects.Clear();

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.Show( !(m_match is GameMatch_PVP) );
            player.m_applyLogicPostion = false;
			player.model.EnableGrey(false);
			
			player.m_enableAction = false;
			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = false;
			
			player.m_enablePickupDetector = false;
			player.m_enableMovement = false;
			player.m_enableAction = false;

			if( player.m_InfoVisualizer != null )
				player.m_InfoVisualizer.SetActive(false);
			  
			player.HideIndicator();
			
			if( player.m_aiMgr != null )
				player.m_aiMgr.m_enable = false;

			//set quality
			/*
			PotientialEffect pe = GameSystem.Instance.PotientialEffectConfig.GetConfig(player.GetQuality());
			if( pe == null )
				continue;
			GameObject resEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/" + pe.resource);
			if( resEffect == null )
				continue;
			GameObject goEffect = GameObject.Instantiate(resEffect) as GameObject;
			goEffect.transform.parent = player.m_goPlayer.transform;
			goEffect.transform.localPosition = Vector3.zero;
			goEffect.transform.localRotation = Quaternion.identity;

			m_goEffects.Add(goEffect);

			Animator animator = goEffect.GetComponent<Animator>();
			if( animator == null )
				continue;
			animator.SetBool("Flame" + pe.idx, true);
			*/
		}
		
		if (m_match.m_uiMatch != null)
		{
			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}

		GameObject resOpening = ResourceLoadManager.Instance.LoadPrefab("Prefab/Camera/Opening");
		GameObject goOpening = GameObject.Instantiate(resOpening) as GameObject;
		if( goOpening == null )
			Logger.LogError("can not find opening prefab.");

		m_trackPlayer.LoadTrack(goOpening);
		m_trackPlayer.Play();

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Openning);

		m_timer = new GameUtils.Timer4View(1f, OnTimer);
		m_timer.stop = false;
	}

	override public void Update (float deltaTime)
	{
		base.Update(deltaTime);

		m_timer.Update(deltaTime);
        
		if( m_trackPlayer != null )
			m_trackPlayer.Update((float)deltaTime);

		if( m_trackPlayer.m_play )
			return;

        m_stateMachine.SetState(State.ePlayerCloseUp);
	}
	
	override public void OnExit ()
	{
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		curMatch.m_camFollowPath.Stop();
		curMatch.m_camFollowPath.enabled = false;
		curMatch.m_cam.enabled = true;

		GameSystem.Instance.mClient.mInputManager.RemoveListener(this);

		foreach( GameObject effect in m_goEffects )
			GameObject.Destroy(effect);
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            player.m_applyLogicPostion = true;
        }
	}

	public void OnPress( int nTouchID, Vector2 vScreenPt, bool bDown, out bool bPassThrough )
	{
		bPassThrough = true;
	}
	public void OnClick( int nTouchID, Vector2 vScreenPt, out bool bPassThrough )
	{
		bPassThrough = false;
        m_stateMachine.SetState(State.ePlayerCloseUp);
	}
	public void OnDrag( int nTouchID, Vector2 vScreenPtCur, Vector2 vScreenPtDelta, out bool bPassThrough )
	{
		bPassThrough = true;
	}
	public void OnDragEnd( int nTouchID )
	{
	}
}
