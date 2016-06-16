using UnityEngine;
using System;
using System.Collections.Generic;

public class Track
{
	public List<UResultPose> poses = new List<UResultPose>();
	
	public Transform 	lookAt;
	public iTweenPath 	trackPath;
	
	public float time;
}

public class TrackPlayer
{
	private float 		m_curTime = 0.0f;
	private List<Track> m_tracks = new List<Track>();
	public	bool		m_play {get; private set;}

	private Track		m_curTrack;
	private int			m_curTrackIdx = 0;
	private bool		m_bSpecifiedIdx = false;

	private const string m_strTrackComplete	= "OnComplete";

	public TrackPlayer()
	{
	}

	public void Update( float fDeltaTime )
	{
		if( !m_play )
			return;
		m_curTime += fDeltaTime;
		if( m_curTrack == null )
			return;
		if( m_curTime > m_curTrack.time )
		{
			if( m_bSpecifiedIdx )
			{
				m_play = false;
				return;
			}
			m_curTrackIdx++;
			//over
			if( m_curTrackIdx == m_tracks.Count )
			{
				m_play = false;
				return;
			}
			//new track
			_SetNewTrack(m_tracks[m_curTrackIdx]);
		}
	}

	void _SetNewTrack(Track newTrack)
	{
		m_curTime = 0.0f;
		m_curTrack = newTrack;

		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		curMatch.m_camFollowPath.Stop();
		curMatch.m_camFollowPath._path = m_curTrack.trackPath;

		curMatch.m_cam.enabled = false;
		curMatch.m_camFollowPath.enabled = true;
		
		curMatch.m_camFollowPath._path = m_curTrack.trackPath;
		curMatch.m_camFollowPath.lookAt = m_curTrack.lookAt;
		curMatch.m_camFollowPath.time = m_curTrack.time;
		curMatch.m_camFollowPath.onComplete = m_strTrackComplete;
		curMatch.m_camFollowPath.Move();

		List<Player> players = GameSystem.Instance.mClient.mPlayerManager.m_Players;
		foreach( UResultPose poseInfo in m_curTrack.poses )
		{
			Team team = poseInfo.side == 0 ? curMatch.m_homeTeam : curMatch.m_awayTeam;
			Player player = team.GetMember(poseInfo.idx);
			if( player == null )
				continue;
			uint uFind = curMatch.m_auxiliaries.Find((uint id)=>{return id == player.m_id;});
			player.gameObject.SetActive( uFind == 0 );

			player.transform.position = poseInfo.transform.position;
			player.transform.rotation = poseInfo.transform.rotation;

			player.m_StateMachine.SetState(PlayerState.State.eStand);

			PlayerState_ResultPose resultPose = player.m_StateMachine.GetState(PlayerState.State.eResultPose) as PlayerState_ResultPose;
			resultPose.pose = poseInfo.pose;
			resultPose.withBall = poseInfo.withBall;

			player.m_StateMachine.SetState(PlayerState.State.eResultPose, true);
		}
	}

	public void Play()
	{
		m_play = true;
		m_curTime = 0.0f;

		m_curTrackIdx = 0;
		if( m_tracks.Count == 0 )
			return;
		_SetNewTrack(m_tracks[0]);
		m_bSpecifiedIdx = false;
	}

	public void Play(int trackIdx)
	{
		m_play = true;
		m_curTime = 0.0f;
		
		m_curTrackIdx = 0;
		if( m_tracks.Count == 0 )
			return;
		_SetNewTrack(m_tracks[trackIdx]);
		m_bSpecifiedIdx = true;
	}

	public void LoadTrack(GameObject goTracking)
	{
		if( goTracking == null )
			return;

		int childCount = goTracking.transform.childCount;
		for( int idx = 0; idx != childCount ; idx++ )
		{
			Transform child = goTracking.transform.GetChild(idx);
			if( child == null )
				continue;
			if( !child.name.Contains("Track") )
				continue;

			Track newTrack = new Track();
			UResultPose[] resultPoses = child.GetComponentsInChildren<UResultPose>();
			newTrack.poses.AddRange(resultPoses);

			newTrack.trackPath = child.GetComponentInChildren<iTweenPath>();
			UTrack uTrack = child.GetComponent<UTrack>();
			newTrack.time = uTrack.time;
			newTrack.lookAt = GameUtils.FindChildRecursive(child, "LookAt");

			m_tracks.Add(newTrack);
		}
	}
}
