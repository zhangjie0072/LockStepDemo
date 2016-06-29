using System;
using UnityEngine;

public class GameRuler
{
	public IM.Number prepareTime {get;private set;}
	
	public bool m_bToCheckBall{ get{return m_toCheckBallTeam != null;} private set{} }
	public Team m_toCheckBallTeam;

	private IM.Number m_fAssistTime = new IM.Number(3);
	public Player	m_assistor{get;private set;}
	public Player	m_assistee{get;private set;}

	private	GameMatch		m_match;

    private static IM.Number F_KEEP_DIST_DEFENSE = IM.Number.half;
    private static IM.Number F_KEEP_DIST_OFFENSE = IM.Number.half;
    public  GameUtils.Timer timer;
	public GameRuler(GameMatch match)
	{
        prepareTime = new IM.Number(3);
		m_match = match;
        
	}
	
	public bool CanRebound()
	{
		UBasketball	ball = m_match.mCurScene.mBall;
		if( ball == null )
			return false;
		return ball.m_ballState == BallState.eRebound;
	}

	public void ConstrainMovementOnBegin(IM.Number fCurTime)
	{
		if( prepareTime < fCurTime)
			return;
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
            IM.Vector2 curPos = new IM.Vector2(player.position.x, player.position.z);

			GameScene scene = m_match.mCurScene;

            IM.Vector2 vPt3Center = scene.mGround.mCenter.xz + scene.mGround.m3PointCenter;
			IM.Vector2 dirCharToPt3Center = (curPos - vPt3Center).normalized;
			
			if( player.m_team.m_role == GameMatch.MatchRole.eDefense )
			{
				if(!scene.mGround.In3PointRange(curPos, -F_KEEP_DIST_DEFENSE))
				{
					curPos = dirCharToPt3Center * (scene.mGround.m3PointRadius - F_KEEP_DIST_DEFENSE) + vPt3Center;
                    player.position = new IM.Vector3(curPos.x, player.position.y, curPos.y);
				}
			}
			else
			{
				if(scene.mGround.In3PointRange(curPos, -F_KEEP_DIST_OFFENSE))
				{
                    curPos = dirCharToPt3Center * (scene.mGround.m3PointRadius + F_KEEP_DIST_DEFENSE) + vPt3Center;
                    player.position = new IM.Vector3(curPos.x, player.position.y, curPos.y);
				}
			}
		}
	}

	public void SwitchRole()
	{
        //TODO 针对PVP修改
        /*
		Team mainTeam 		= m_match.m_mainRole.m_team;
		Team defenseTeam 	= m_match.m_mainRole.m_defenseTarget.m_team;

		if( m_bToCheckBall && mainTeam.m_role == GameMatch.MatchRole.eOffense )
		{
			MatchState.State eCurState = m_match.m_stateMachine.m_curState.m_eState;
			if( eCurState == MatchState.State.ePlaying)
			{
				m_bToCheckBall = false; 
                foreach (Player member in mainTeam.members)
					member.m_enableAction = true;
			}
        }
        */

		if (m_match.m_homeTeam.m_role == GameMatch.MatchRole.eNone)
		{
			Player player = m_match.mCurScene.mBall.m_owner;
			if (player == null)
			{
                Debug.LogError("Ball owner is null.");
                /*
				mainTeam.m_role = GameMatch.MatchRole.eOffense;
				defenseTeam.m_role = GameMatch.MatchRole.eDefense;
                */
			}
			else
			{
				player.m_team.m_role = GameMatch.MatchRole.eOffense;
				player.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;
			}
		}
		else
		{
			foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
			{
				if (player.m_team.m_role == GameMatch.MatchRole.eDefense)
					player.m_team.m_role = GameMatch.MatchRole.eOffense;
				else
					player.m_team.m_role = GameMatch.MatchRole.eDefense;
			}
		}
	}

	void OnAssistTimeUp()
	{
		m_assistor = null;
		m_assistee = null;
	}

	public void SetAssist(Player assistor, Player assistee)
	{
        //Scheduler.Instance.AddTimer( m_fAssistTime, false, OnAssistTimeUp );
        if (timer == null)
            timer = new GameUtils.Timer(m_fAssistTime, OnAssistTimeUp);
        else
            timer.Reset();
		m_assistor = assistor;
		m_assistee = assistee;
	}

	public void ResetAssist()
	{
        timer = null;
		m_assistor = null;
		m_assistee = null;
	}
}