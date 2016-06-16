using fogs.proto.msg;
using System.Collections.Generic;
using UnityEngine;

public class ReboundHelper
{
	class Info
	{
		public Rebound msg;
		public Player player;
		public uint reboundValue;
	}

	List<Info> rebounders = new List<Info>();
	bool closed = false;
	float validTime;

	static HedgingHelper hedging = new HedgingHelper("Rebound");

	public void AddRebounder(Rebound msg)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;

		if (ball.m_ballState != BallState.eRebound)
			return;
		if (rebounders.Count > 0 && closed)
			return;
		if (rebounders.Find(info => info.msg.char_id == msg.char_id) != null)
			return;

		Player rebounder = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find(
			player => player.m_roomPosId == msg.char_id);

		uint reboundValue = rebounder.m_finalAttrs["rebound"];

		Info rebounderInfo = new Info();
		rebounderInfo.msg = msg;
		rebounderInfo.player = rebounder;
		rebounderInfo.reboundValue = reboundValue;
		rebounders.Add(rebounderInfo);

		if (rebounders.Count == 1)	//first rebounder
		{
			//string action = rebounder.m_StateMachine.GetState(PlayerState.State.eRebound).GetActionByType(msg.actionType);

			SkillAttr skillAttr = GameSystem.Instance.SkillConfig.GetSkill( msg.skill.skill_id );
			SkillAction curAction = skillAttr.actions.Find( (SkillAction skillAction)=>{ return skillAction.id == msg.skill.action_id; });
			string action =  rebounder.m_skillSystem.ParseAction(curAction.action_id, msg.skill.skill_matchedKeyIdx, Command.Rebound);
            IM.Number frameRate = rebounder.animMgr.GetFrameRate(action);
			Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = rebounder.m_animAttributes.m_rebound;
			int reboundKey = rebounds[rebounder.animMgr.GetOriginName(action)].GetKeyFrame("OnRebound").frame;
			validTime = (float)reboundKey / (float)frameRate;
		}
	}

	public void Update()
	{
		if (rebounders.Count == 0)
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;

		if (!closed)
		{
			validTime -= Time.deltaTime;
			if (validTime < 0f)		//close
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
					/*
					selectedRebounder = rebounders[0];
					for (int i = 1; i < rebounders.Count; ++i)
					{
						Info rebounder = rebounders[i];
						float rate = hedging.Calc(selectedRebounder.reboundValue, rebounder.reboundValue);
						if (Random.value > rate)
							selectedRebounder = rebounder;
					}
					*/
					uint totalValue = 0;
					foreach (Info info in rebounders)
						totalValue += info.reboundValue;
					uint value = (uint)Random.Range(0, totalValue);
					Logger.Log("rebound value: " + value );
					foreach( Info pl in rebounders )
						Logger.Log("player :" + pl.player.m_id + " rebound : " + pl.reboundValue );

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
							ball.m_picker = selectedRebounder.player;

						GameMsg msg = new GameMsg();
						msg.senderID = selectedRebounder.msg.char_id;
						msg.eStateType = selectedRebounder.msg.actionType;
						msg.pos = selectedRebounder.msg.pos;
						msg.rotate = selectedRebounder.msg.rotate;
						msg.eState = CharacterState.eRebound;
						msg.velocity = selectedRebounder.msg.velocity;
						msg.nSuccess = canPick ? 1u : 0u;

						Player rebounder = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find(
							player => player.m_roomPosId == msg.senderID);
						SimulateCommand cmd = match.GetSmcCommandByGameMsg(rebounder, msg);
						if( cmd != null && rebounder.m_smcManager != null )
							rebounder.m_smcManager.AddCommand(cmd);

						string trace = "Rebound: player: " + info.player.m_id + " " + info.player.m_name +
							" value:" + info.reboundValue + " success:" + msg.nSuccess;
						Debugger.Instance.m_steamer.message += "\n" + trace + "\n";
						Logger.Log(trace);
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
