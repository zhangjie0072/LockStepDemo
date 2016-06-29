using System.Collections.Generic;
using UnityEngine;

public class ReboundHelper
{
	class Info
	{
		public Player player;
		public uint reboundValue;
	}

	List<Info> rebounders = new List<Info>();
	bool closed = false;
	IM.Number validTime;

	static HedgingHelper hedging = new HedgingHelper("Rebound");

	public void AddRebounder(Player rebounder, SkillInstance skillInst)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;

		if (ball.m_ballState != BallState.eRebound)
			return;
		if (rebounders.Count > 0 && closed)
			return;
		if (rebounders.Find(info => info.player.m_roomPosId == rebounder.m_roomPosId) != null)
			return;

		uint reboundValue = rebounder.m_finalAttrs["rebound"];

		Info rebounderInfo = new Info();
		rebounderInfo.player = rebounder;
		rebounderInfo.reboundValue = reboundValue;
		rebounders.Add(rebounderInfo);

		if (rebounders.Count == 1)	//first rebounder
		{
            SkillAction curAction = skillInst.curAction;
			string action =  rebounder.m_skillSystem.ParseAction(curAction.action_id, skillInst.matchedKeyIdx, Command.Rebound);
            IM.Number frameRate = rebounder.animMgr.GetFrameRate(action);
			Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = rebounder.m_animAttributes.m_rebound;
			int reboundKey = rebounds[rebounder.animMgr.GetOriginName(action)].GetKeyFrame("OnRebound").frame;
			validTime = reboundKey / frameRate;
		}
	}

	public void Update(IM.Number deltaTime)
	{
		if (rebounders.Count == 0)
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;

		if (!closed)
		{
			validTime -= deltaTime;
			if (validTime < IM.Number.zero)		//close
			{
				closed = true;

				//select rebounder
				rebounders.Sort((info1, info2) =>
				{
					if (info1.reboundValue < info2.reboundValue)
						return -1;
					else if (info1.reboundValue > info2.reboundValue)
						return 1;
					else
						return 0;
				});

				Info selectedRebounder = null;
				if (rebounders.Count > 0)
				{
					uint totalValue = 0;
					foreach (Info info in rebounders)
						totalValue += info.reboundValue;
					uint value = (uint)IM.Random.Range(0, totalValue);
					Debug.Log("rebound value: " + value );
					foreach( Info pl in rebounders )
						Debug.Log("player :" + pl.player.m_id + " rebound : " + pl.reboundValue );

					uint finalOdd = 0;
					foreach( Info pl in rebounders )
					{
						finalOdd += pl.reboundValue;
						if( value < finalOdd )
						{
							selectedRebounder = pl;
							break;
						}
					}
				}

				if (selectedRebounder != null)
				{
					foreach (Info info in rebounders)
					{
						bool canPick = (selectedRebounder == info && ball.m_ballState == BallState.eRebound && ball.m_picker == null);
                        if (canPick)
                        {
                            ball.m_picker = selectedRebounder.player;

                            PlayerState_Rebound stateRebound = ball.m_picker.m_StateMachine.GetState(PlayerState.State.eRebound) as PlayerState_Rebound;
                            if (ball.m_picker.m_StateMachine.m_curState.m_eState == PlayerState.State.eRebound && !stateRebound.m_toReboundBall)
                                stateRebound.m_toReboundBall = true;
                            else
                            {
                                MatchState.State eCurState = match.m_stateMachine.m_curState.m_eState;
                                if (eCurState == MatchState.State.ePlaying || eCurState == MatchState.State.eTipOff)
                                {
                                    if (ball.m_owner != null)
                                        Debug.LogError("can not grab ball.");
                                    ball.m_picker.GrabBall(ball);
                                }
                            }
                        }

						string trace = "Rebound: player: " + info.player.m_id + " " + info.player.m_name +
							" value:" + info.reboundValue + " canPick:" + canPick;
						Debugger.Instance.m_steamer.message += "\n" + trace + "\n";
						Debug.Log(trace);
					}
				}
			}
		}

		if (ball.m_ballState != BallState.eRebound)
		{
			rebounders.Clear();
			closed = false;
		}
	}
}
