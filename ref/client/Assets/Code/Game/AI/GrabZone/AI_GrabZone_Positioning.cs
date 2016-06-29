using UnityEngine;
using System.Collections.Generic;

public class AI_GrabZone_Positioning : AIState
{
	GameMatch_GrabZone match;

	public AI_GrabZone_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eGrabZone_Positioning;
		match = m_match as GameMatch_GrabZone;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_moveTarget = SeekMoveTarget();
	}
	
	public override void ArriveAtMoveTarget()
	{
		m_system.SetTransaction(AIState.Type.eGrabZone_Shoot);
	}

	private IM.Vector3 SeekMoveTarget()
	{
		AI_GrabZone_Shoot shootState = m_system.GetState(Type.eGrabZone_Shoot) as AI_GrabZone_Shoot;
		int currZone = match.DetectZone(m_player.position);
		//Sort zones that not mine by distance.
		List<KeyValuePair<int, IM.Number>> list = new List<KeyValuePair<int, IM.Number>>();
		for (int i = 1; i <= GameMatch_GrabZone.ZONE_COUNT; ++i)
		{
			if (match.zoneOwnership[i] == 2 || i == shootState.shootingZone)
				continue;
			IM.Vector3 pos = match.zonePosition[i];
			IM.Number dist = (i == currZone) ? IM.Number.zero : GameUtils.HorizonalDistance(pos, m_player.position);
			list.Add(new KeyValuePair<int, IM.Number>(i, dist));
		}
		list.Sort((KeyValuePair<int, IM.Number> left, KeyValuePair<int, IM.Number> right) =>
		{ return left.Value < right.Value ? -1 : (left.Value > right.Value ? 1 : 0); });

		int targetZone = currZone;
		if (list.Count > 0)
		{
			if (match.level == GameMatch.Level.Easy)
			{
				targetZone = list[IM.Random.Range(0, list.Count - 1)].Key;
			}
			else if (match.level == GameMatch.Level.Normal)
			{
				targetZone = list[0].Key;
			}
			else if (match.level == GameMatch.Level.Hard)
			{
				KeyValuePair<int, IM.Number> firstRival = list.Find(
					(KeyValuePair<int, IM.Number> pair) => { return match.zoneOwnership[pair.Key] == 1; });
				KeyValuePair<int, IM.Number> firstNeutral = list.Find(
					(KeyValuePair<int, IM.Number> pair) => { return match.zoneOwnership[pair.Key] == 0; });
				if (firstRival.Key == 0 && firstNeutral.Key == 0)
				{
					targetZone = currZone;
				}
				else if (firstRival.Key == 0)
				{
					targetZone = firstNeutral.Key;
				}
				else if (firstNeutral.Key == 0)
				{
					targetZone = firstRival.Key;
				}
				else
				{
					IM.Number playerSpeed = m_player.m_bWithBall ?
						m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed:
						m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
					if ((firstRival.Value - firstNeutral.Value) / playerSpeed < match.ZONE_ARRIVE_TIME_DIFF_THRESHOLD)
						targetZone = firstRival.Key;
					else
						targetZone = firstNeutral.Key;
				}
			}
		}

		if (targetZone == currZone)
			return m_player.position;
		else
			return match.zonePosition[targetZone];
	}

	public override void OnPlayerCollided(Player colPlayer)
	{
		base.OnPlayerCollided(colPlayer);
		Debug.Log("AI_GrabZone_Positioning.OnPlayerCollided.");
		m_system.SetTransaction(AIState.Type.eGrabZone_AvoidDefender);
	}
}