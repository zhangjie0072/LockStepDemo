using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class MatchStatePlaying_PVP : MatchStatePlaying, GameMatch.Count24Listener
{
    private UnityEngine.Object mResIndicator;
    private GameObject mGoIndicator;

    private bool mRefreshCounter = false;
    private GameObject m_goRebPlacementTip;

	private List<UBasketball> m_finalHitBalls = new List<UBasketball>();

	public MatchStatePlaying_PVP(MatchStateMachine owner)
        : base(owner)
    {
        m_eState = MatchState.State.ePlaying;
    }

	public override void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate (fDeltaTime);

		UBasketball ball = m_match.mCurScene.mBall;
		if( ball == null )
			return;
		GameMatch_PVP pvpMatch = m_match as GameMatch_PVP;

		if(m_match.m_config.type == GameMatch.Type.ePVP_3On3)
		{
		}
		else
		{
			if( ball.m_ballState == BallState.eUseBall_Pass )
			{
				Player interceptor = ball.m_interceptor;
				if( interceptor == null )
				{
					Player owner = ball.m_catcher;
					pvpMatch.SwitchMainrole(owner);
				}
				else
				{
                    pvpMatch.SwitchMainrole(interceptor);
                    pvpMatch.SwitchMainrole(interceptor.m_defenseTarget);
				}
			}
			else if( ball.m_ballState == BallState.eUseBall )
			{
                pvpMatch.SwitchMainrole(ball.m_owner);
			}
		}
        //处理掉线托管的玩家
        GameMatch_PVP match = m_match as GameMatch_PVP;
		foreach(uint acc_id in match.droppedAccount)
		{
            Player mainRole = match.GetMainRole(acc_id);
            if (mainRole != null)
            {
                mainRole.operMode = Player.OperMode.AI;
                mainRole.m_aiMgr.IsPvp = true;
                mainRole.m_takingOver = true;
                Debug.Log(string.Format("PVP player taking over, {0} {1}", mainRole.m_team.m_side, mainRole.m_id));
            }
		}
        match.droppedAccount.Clear();
	}
}

