using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class MatchStatePlaying : MatchState,GameMatch.Count24Listener
{
    private UnityEngine.Object mResIndicator;
    private GameObject mGoIndicator;

    private bool mRefreshCounter = false;
    private GameObject m_goRebPlacementTip;

	private List<UBasketball> m_finalHitBalls = new List<UBasketball>();

    public MatchStatePlaying(MatchStateMachine owner)
        : base(owner)
    {
        m_eState = MatchState.State.ePlaying;
    }

    override public void OnEnter(MatchState lastState)
    {
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			player.m_enableAction = true;
			player.m_enableMovement = true;
			player.m_enablePickupDetector = true;
		}

        if (m_match.m_uiMatch != null)
        {
            m_match.m_count24Time = m_match.gameMatchTime;
            m_match.m_count24TimeStop = false;
            m_match.m_gameMatchCountStop = false;
        }

        m_match.m_bTimeUp = false;
        m_match.m_b24TimeUp = false;

		m_match.m_cam.m_PositionImmediately = false;
    }

    public void OnMatchTimeUp()
    {
		m_finalHitBalls.Clear();
		foreach (UBasketball ball in m_match.mCurScene.balls)
		{
			if (ball.m_ballState == BallState.eUseBall_Shoot)
				m_finalHitBalls.Add(ball);
		}
        m_match.m_bTimeUp = true;
        m_match.m_b24TimeUp = false;
    }

    void GameMatch.Count24Listener.OnTimeUp()
    {
        m_match.m_b24TimeUp = true;
    }

    void _ShowCheckBallIndicator(bool bShow)
    {
        if (mResIndicator == null)
            mResIndicator = ResourceLoadManager.Instance.LoadPrefab("Prefab/indicator/pre_3pt");
        if (mGoIndicator == null)
            mGoIndicator = GameObject.Instantiate(mResIndicator) as GameObject;

        if (bShow)
        {
            mGoIndicator.SetActive(true);
            Animation anim = mGoIndicator.GetComponent<Animation>();
            int iAnimCnt = anim.GetClipCount();
            AnimationClip clip = anim.GetClip("pt");
            anim.Play();
            if (m_match.m_uiMatch != null)
                m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eCheckBall, true);
        }
        else
        {
            mGoIndicator.SetActive(false);
            if (m_match.m_uiMatch != null)
                m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eCheckBall, false);
        }
    }

	public override void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
    {
		if( m_match.m_stateMachine.m_curState != this )
			return;

        UBasketball ball = m_match.mCurScene.mBall;
        if (animEvent == PlayerActionEventHandler.AnimEvent.ePickupBall
           || animEvent == PlayerActionEventHandler.AnimEvent.eRebound)
        {
            if (animEvent == PlayerActionEventHandler.AnimEvent.eRebound)
            {
                sender.mStatistics.success_rebound_times++;
                //Logger.Log("rebound");
            }
			else
			{
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.GrabBall);
			}

            m_match.m_b24TimeUp = false;

            if (sender.m_team.m_role == GameMatch.MatchRole.eDefense ||
                sender.m_team.m_role == GameMatch.MatchRole.eNone)
            {
				if (!m_match.m_b24TimeUp)
				{
                    GameMatch.MatchRole senderTeamRole = sender.m_team.m_role;

					if (m_match.EnableSwitchRole())
						m_match.m_ruler.SwitchRole();

					foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
						player.m_enableAction = true;

					if (senderTeamRole ==  GameMatch.MatchRole.eDefense && m_match.EnableCheckBall())
						m_match.m_ruler.m_toCheckBallTeam = m_match.m_offenseTeam;

					mRefreshCounter = false;
					if (m_match.m_uiMatch != null && m_match.EnableCounter24())
					{
						m_match.m_uiMatch.ShowCounter(true, sender.m_team.m_side == m_match.m_mainRole.m_team.m_side );
                        m_match.m_count24TimeStop = false;
					}
				}
            }
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.eBlock)
        {
            sender.mStatistics.success_block_times++;
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Block);
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.BlockBall);
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.eShoot )
        {
			bool bOpenShoot = (bool)context;
			if (bOpenShoot)
				m_match.ShowPlayerTip(sender, "gameInterface_tip_shots");
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.ePass)
        {
            //if( ball.m_interceptor == null )
            //    ball.m_catcher.m_bToCatch = true;
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.PassBall);
        }

		if (animEvent == PlayerActionEventHandler.AnimEvent.eIntercepted)
		{
			if( m_match.m_ruler.m_bToCheckBall && sender.m_team.m_role == GameMatch.MatchRole.eDefense )
			{
				foreach (Player player in m_match.m_ruler.m_toCheckBallTeam )
					player.m_enableAction = true;
				m_match.m_ruler.m_toCheckBallTeam = null;
			}
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.InterceptSuc);
		}

        if (animEvent == PlayerActionEventHandler.AnimEvent.eDunk)
        {
			//if (m_match.m_cam != null && m_match.m_cam.m_Shake != null)
			//	m_match.m_cam.m_Shake.AddCamShake(Vector3.one * 0.2f, 0.5f, m_match.m_cam.transform.position);
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.eSteal)
        {
			//steal get ball
			if( sender.m_bWithBall )
			{
				if (m_match.m_uiMatch != null && m_match.EnableCounter24())
				{
					m_match.m_uiMatch.ShowCounter(true, sender.m_team.m_side == m_match.m_mainRole.m_team.m_side );
                    m_match.m_count24TimeStop = false;
				}
			}
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.StealSuc);
            sender.mStatistics.success_steal_times++;
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.eStolen)
        {
        }

        if (animEvent == PlayerActionEventHandler.AnimEvent.eCatch)
        {
            if (sender.m_defenseTarget != null)
            {
                if (sender.m_AOD.GetStateByPos(sender.m_defenseTarget.position) != AOD.Zone.eInvalid)
                    m_match.ShowTips(sender.m_defenseTarget.model.head.position + Vector3.up, CommonFunction.GetConstString("MATCH_TIPS_PERFECT_DEFFENSE"), new Color32(0, 108, 0, 255));
            }

            m_match.m_ruler.SetAssist(ball.m_passer, ball.m_catcher);
        }
    }

    override public void Update(IM.Number fDeltaTime)
    {
        base.Update(fDeltaTime);

		RoadPathManager.Instance.Update(GameSystem.Instance.mClient.mPlayerManager.m_Players);

        UBasketball ball = m_match.mCurScene.mBall;
		if (ball == null && m_goRebPlacementTip != null)
			m_goRebPlacementTip.SetActive(false);

		if (ball == null)
		{
			_OnTimeUp();
			return;
		}

		if (m_match.m_uiMatch != null)
			mRefreshCounter = ball.m_collidedWithRim;

		if (ball.m_owner == null)
		{
			_ShowCheckBallIndicator(false);
			if (m_match.m_ruler.m_bToCheckBall)
			{
				foreach (Player member in m_match.m_offenseTeam.members)
					member.m_enableAction = true;
			}
		}
		else
		{
			if (m_match.m_ruler.m_bToCheckBall)
			{
				IM.Vector3 curPos = ball.m_owner.position;
                if (m_match.mCurScene.mGround.In3PointRange(curPos.xz, IM.Number.zero))
                {
                    foreach (Player member in ball.m_owner.m_team.members)
                        member.m_enableAction = false;
					if( m_match.m_ruler.m_toCheckBallTeam == m_match.m_mainRole.m_team )
                    	_ShowCheckBallIndicator(true);
                }
                else
                {
                    foreach (Player member in ball.m_owner.m_team.members)
                        member.m_enableAction = true;
					m_match.m_ruler.m_toCheckBallTeam = null;
                }

				if( m_match.m_ruler.m_toCheckBallTeam != m_match.m_mainRole.m_team )
					_ShowCheckBallIndicator(false);
            }
            else
                _ShowCheckBallIndicator(false);
        }

        if (m_goRebPlacementTip != null)
        {
			if (ball.m_ballState == BallState.eRebound || ball.m_bGoal)
            {
                m_goRebPlacementTip.SetActive(true);
                m_goRebPlacementTip.transform.position = (Vector3)ball.m_reboundPlacement + Vector3.up * 0.01f;
            }
            else
                m_goRebPlacementTip.SetActive(false);
        }
        else
        {
            UnityEngine.Object resPlacementTip = ResourceLoadManager.Instance.LoadPrefab("Prefab/indicator/RebPlacement");
            m_goRebPlacementTip = GameObject.Instantiate(resPlacementTip) as GameObject;
            m_goRebPlacementTip.SetActive(false);
        }
        //if (m_match.m_mainRole != null)
        //    {
        //        if (m_match.mCurScene.mGround.In3PointRange(GameUtils.StripV3Y(m_match.m_mainRole.position), 0.0f))
        //        {
        //            m_match.m_mainRole.UpdateIndicator(Color.yellow);
        //        }
        //        else
        //        {
        //            m_match.m_mainRole.UpdateIndicator(Color.red);
        //        }
        //}
		_OnTimeUp();
    }

	virtual protected void _OnTimeUp()
	{
		if (m_match.m_bTimeUp)
		{
			bool finalHitOver = true;
			foreach (UBasketball b in m_finalHitBalls)
			{
				if (b.m_ballState == BallState.eUseBall_Shoot || b.m_ballState == BallState.eRebound)
				{
					finalHitOver = false;
					break;
				}
			}
			if (finalHitOver)
			{
				m_stateMachine.SetState(MatchState.State.eOver);
			}
		}
		else if (m_match.m_b24TimeUp)
		{
        	UBasketball ball = m_match.mCurScene.mBall;
			if (ball.m_ballState == BallState.eUseBall || ball.m_ballState == BallState.eLoseBall)
				m_stateMachine.SetState(MatchState.State.eFoul);
		}
	}

    bool _IsCritialGoal(int score)
    {
        int scoreDis = m_match.m_awayScore - m_match.m_homeScore;
        if (score < Mathf.Abs(scoreDis))
            return false;

        if (m_match.m_awayScore >= m_match.m_homeScore && (m_match.m_homeScore + score > m_match.m_awayScore))
            return true;

        if (m_match.m_awayScore <= m_match.m_homeScore && (m_match.m_awayScore + score > m_match.m_homeScore))
            return true;

        return false;
    }

    override public void OnExit()
    {
        _ShowCheckBallIndicator(false);
        if (m_match.m_uiMatch != null)
            m_match.m_count24TimeStop = true;

        m_match.m_ruler.ResetAssist();

		if (m_goRebPlacementTip != null)
			m_goRebPlacementTip.SetActive(false);
    }
}

