using System;
using UnityEngine;

public class MatchResultCameraControl
	:UCamCtrl_FollowPath.Listener
{
	public bool	m_finished{ get; private set; }

	private GameMatch m_match;
	private GameScene.Ending m_ending;

	private const string strWinFunc 	= "OnWinCamMoveComplete";
	private const string strLoseFunc 	= "OnLoseCamMoveComplete";

	public MatchResultCameraControl ( GameMatch match, GameScene.Ending ending, bool bWin )
	{
		m_match = match;
		m_ending = ending;

		if( m_match.m_camFollowPath == null )
		{
			Logger.LogError("m_camFollowPath == null.");
			return;
		}

		m_match.m_camFollowPath.AddListeners(this);

		m_match.m_cam.enabled = false;

		m_match.m_camFollowPath.enabled = true;

		m_match.m_camFollowPath._path = bWin? m_ending.winPath : m_ending.losePath;
		m_match.m_camFollowPath.lookAt = bWin? m_ending.winLookAt: m_ending.loseLookAt;
		m_match.m_camFollowPath.time = m_ending.time;
		m_match.m_camFollowPath.onComplete = strLoseFunc;
		m_match.m_camFollowPath.Move();

		m_finished = false;
	}
	
	public void OnComplete(UCamCtrl_FollowPath.PathType type)
	{
		m_match.m_camFollowPath.enabled = false;
		m_finished = true;
		m_match.m_cam.enabled = true;
	}
}