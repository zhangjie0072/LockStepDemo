using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using fogs.proto.msg;

public class AIUtils
{
	public static void PositionAndShoot(AIState.Type positionState, AIState.Type shootState, GameMatch match, Player player)
	{
		PlayGround ground = match.mCurScene.mGround;
		Area currArea = ground.GetArea(player);
		bool outside = false;
		if (currArea == Area.eFar)
			outside = !ground.In3PointRange(player.position.xz, ground.mDist3PtOutside);
		Area bestArea = GetBestShootArea(player);
		if (currArea == bestArea && !outside)
		{
			player.m_aiMgr.SetTransaction(shootState);
		}
		else
		{
			IM.Vector2 v3PtCenter = new IM.Vector2( ground.mCenter.x + ground.m3PointCenter.x, ground.mCenter.z + ground.m3PointCenter.y );
			IM.Vector2 playerPos = player.position.xz;
			IM.Vector2 basketPos = match.mCurScene.mBasket.m_vShootTarget.xz;
			IM.Number fDistanceToNet = IM.Vector2.Distance(playerPos, basketPos);
			IM.Number fDistTo3PtCenter = IM.Vector2.Distance(playerPos, v3PtCenter);
			IM.Vector2 dirOut = (playerPos - v3PtCenter).normalized;
			IM.Number angle = IM.Vector2.Angle( dirOut, -IM.Vector2.up );
			IM.Number dist = IM.Number.zero;
			IM.Vector2 dir = IM.Vector2.up;
			if (bestArea == Area.eNear)
			{
				dist = fDistanceToNet - match.mCurScene.mGround.mFreeThrowLane;
				dir = (basketPos - playerPos).normalized;
			}
			else if (bestArea == Area.eMiddle)
			{
				if (currArea == Area.eNear)
				{
					dist = match.mCurScene.mGround.mFreeThrowLane - dist;
					dir = (playerPos - basketPos).normalized;
				}
				else
				{
					if (angle > ground.m3PointMaxAngle)
					{
						dist = IM.Math.Abs(playerPos.x) - ground.m3PointBaseLine;
						dir = playerPos.x > IM.Number.zero ? -IM.Vector2.right : IM.Vector2.right;
					}
					else
					{
						dist = fDistTo3PtCenter - ground.m3PointRadius;
						dir = -dirOut;
					}
				}
			}
			else if (bestArea == Area.eFar)
			{
				if (outside)
				{
					if( angle > ground.m3PointMaxAngle )
					{
						dist = IM.Math.Abs(playerPos.x) - (ground.m3PointBaseLine + ground.mDist3PtOutside);
						dir = playerPos.x > IM.Number.zero ? -IM.Vector2.right : IM.Vector2.right;
					}
					else
					{
						dist = fDistTo3PtCenter - (ground.m3PointRadius + ground.mDist3PtOutside);
						dir = -dirOut;
					}
				}
				else
				{
					if( angle > ground.m3PointMaxAngle)
					{
						dist =  ground.m3PointBaseLine - IM.Math.Abs(playerPos.x);
						dir = playerPos.x > IM.Number.zero ? IM.Vector2.right : -IM.Vector2.right;
					}
					else
					{
						dist =  ground.m3PointRadius - fDistTo3PtCenter;
						dir = dirOut;
					}
				}
			}

			if (dist < match.gameMode.repositionDist)
			{
				AIState state = player.m_aiMgr.GetState(positionState);
				state.m_moveTarget = player.position + new IM.Vector3(dir.x, IM.Number.zero, dir.y) * dist;
				player.m_aiMgr.SetTransaction(state);
			}
			else
				player.m_aiMgr.SetTransaction(shootState);
		}
	}

	public static Area GetBestShootArea(Player player)
	{
		IM.Number far = new IM.Number((int)player.m_finalAttrs["shoot_far"]);
		IM.Number middle = new IM.Number((int)player.m_finalAttrs["shoot_middle"]);
        IM.Number near =new IM.Number((int)player.m_finalAttrs["shoot_near"]);
		if (far > middle)
			return near > far ? Area.eNear : Area.eFar;
		else
			return near > middle ? Area.eNear : Area.eMiddle;
	}

	public static bool CanArriveBefore(Player player1, Player player2, IM.Vector3 target)
	{
		IM.Number speed1 = player1.m_bWithBall ?
			player1.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed :
			player1.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
		IM.Number speed2 = player2.m_bWithBall ?
			player2.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed :
			player2.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
		IM.Number dist1 = GameUtils.HorizonalDistance(player1.position, target);
		IM.Number dist2 = GameUtils.HorizonalDistance(player2.position, target);
		return (dist1 / speed1) < (dist2 / speed2);
	}

	public static List<KeyValuePair<UBasketball, IM.Number>> SortBallListByDistance(List<UBasketball> ballList, IM.Vector3 position)
	{
		List<KeyValuePair<UBasketball, IM.Number>> listOrdered = new List<KeyValuePair<UBasketball, IM.Number>>();
		foreach (UBasketball ball in ballList)
		{
			if (ball.m_ballState == BallState.eLoseBall && ball.m_owner == null)
			{
				IM.Number dist = GameUtils.HorizonalDistance(position, ball.position);
                listOrdered.Add(new KeyValuePair<UBasketball, IM.Number>(ball, dist));
			}
		}
		listOrdered.Sort(DistComparer);
		return listOrdered;
	}

	private static int DistComparer(KeyValuePair<UBasketball, IM.Number> ball1, KeyValuePair<UBasketball, IM.Number> ball2)
	{
		if (ball1.Value < ball2.Value)
			return -1;
		else if (ball1.Value > ball2.Value)
			return 1;
		else
			return 0;
	}

	public static bool IsSpecial(KeyValuePair<UBasketball, IM.Number> ball)
	{
		return ball.Key.m_special;
	}

	public static bool NotSpecial(KeyValuePair<UBasketball, IM.Number> ball)
	{
		return !ball.Key.m_special;
	}

	//Used by rebound storm
	public static IM.Number GetNPCReboundBallHeight(IM.Number npcMaxHeight, IM.Number playerMaxHeight, IM.Number ballMaxHeight,
		IM.Number npcHeightScale, IM.Number playerHeightScale, IM.Number ballHeightScale)
	{
		IM.Number scaledNPCHeight = npcMaxHeight * npcHeightScale;
		IM.Number scaledPlayerHeight = playerMaxHeight * playerHeightScale;
		IM.Number scaledBallHeight = ballMaxHeight * ballHeightScale;

		if (ballMaxHeight > scaledNPCHeight)
		{
			if (scaledNPCHeight >= playerMaxHeight)
			{
				return scaledPlayerHeight;
			}
			else
			{
				return scaledNPCHeight;
			}
		}
		else
		{
			if (ballMaxHeight >= playerMaxHeight)
			{
				return scaledPlayerHeight;
			}
			else
			{
				return scaledBallHeight;
			}
		}
	}

	//Used by other match
    public static IM.Number GetNPCReboundBallHeight(IM.Number npcMaxHeight, IM.Number ballHeight, IM.Number npcHeightScale, IM.Number ballHeightScale)
	{
        IM.Number scaledNPCHeight = npcMaxHeight * npcHeightScale;
        IM.Number scaledBallHeight = ballHeight * ballHeightScale;

		if (ballHeight >= npcMaxHeight)
		{
			return scaledNPCHeight;
		}
		else
		{
			return scaledBallHeight;
		}
	}

	public static void AttackByPosition(Player player, IM.Number totalWeight)
	{
		AISystem system = player.m_aiMgr;
		IM.Number usedWeight = IM.Number.zero;
		if( player.CanLayup() && player.m_skillSystem.GetValidSkillInMatch(Command.Layup, true) != null)
		{
			IM.Number layupWeight = IM.Number.zero;
			switch (player.m_position)
			{
				case PositionType.PT_C:
					layupWeight = new IM.Number(0,200);
					break;
				case PositionType.PT_PF:
					layupWeight = new IM.Number(0,400);
					break;
				case PositionType.PT_SF:
					layupWeight = new IM.Number(0,250);
					break;
				case PositionType.PT_SG:
                    layupWeight = new IM.Number(0, 300);
					break;
				case PositionType.PT_PG:
                    layupWeight = new IM.Number(0, 500);
					break;
			}
			system.SetTransaction(AIState.Type.eLayup, totalWeight * layupWeight);
			usedWeight += layupWeight;
		}
		if( player.CanDunk() && player.m_skillSystem.GetValidSkillInMatch(Command.Dunk, true) != null)
		{
			IM.Number dunkWeight = IM.Number.zero;
			switch (player.m_position)
			{
				case PositionType.PT_C:
                    dunkWeight = new IM.Number(0, 400);
					break;
				case PositionType.PT_PF:
                    dunkWeight = new IM.Number(0, 200);
					break;
				case PositionType.PT_SF:
                    dunkWeight = new IM.Number(0, 500);
					break;
				case PositionType.PT_SG:
                    dunkWeight = new IM.Number(0, 600);
					break;
				case PositionType.PT_PG:
                    dunkWeight = new IM.Number(0, 400);
					break;
			}
			system.SetTransaction(AIState.Type.eDunk, totalWeight * dunkWeight);
			usedWeight += dunkWeight;
		}
		if( player.CanShoot() )
		    system.SetTransaction(AIState.Type.eShoot, totalWeight * (IM.Number.one - usedWeight));
	}

	public static bool IsAttacking(Player player)
	{
		return player.m_StateMachine.m_curState.m_eState == PlayerState.State.ePrepareToShoot ||
			player.m_StateMachine.m_curState.m_eState == PlayerState.State.eShoot ||
			player.m_StateMachine.m_curState.m_eState == PlayerState.State.eLayup ||
			player.m_StateMachine.m_curState.m_eState == PlayerState.State.eDunk;
	}

	public static bool InAttackableDistance(GameMatch match, Player player)
	{
		if (player.m_position == PositionType.PT_C || player.m_position == PositionType.PT_PF)
		{
			Area area = match.mCurScene.mGround.GetArea(player);
			return area != Area.eFar;
		}
		else
			return GameUtils.HorizonalDistance(player.position, match.mCurScene.mBasket.m_vShootTarget) <= new IM.Number(9,150);
	}

	public static Player ChoosePassTarget(Player player)
	{
#if !UNITY_IPHONE
		return (from t in player.m_team.members
				where t != player
				orderby t.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall descending,
				t.IsDefended() ascending,
				GameUtils.HorizonalDistance(t.position, player.position) ascending
				select t).FirstOrDefault();
#else
		Player passTarget = null;
		List<Player> ordered = new List<Player>();
		foreach (Player p in player.m_team.members)
		{
			if (p != player)
				ordered.Add(p);
		}
		ordered.Sort((p1, p2) =>
		{
			int req1 = (p1.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall ? 1 : 0);
			int req2 = (p2.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall ? 1 : 0);
			if (req1 != req2)
			{
				return req1 > req2 ? -1 : 1;
			}
			else
			{
				int def1 = p1.IsDefended() ? 1 : 0;
				int def2 = p2.IsDefended() ? 1 : 0;
				if (def1 != def2)
				{
					return def1 < def2 ? -1 : 1;
				}
				else
				{
					float dist1 = GameUtils.HorizonalDistance(p1.position, player.position);
					float dist2 = GameUtils.HorizonalDistance(p2.position, player.position);
					return dist1 == dist2 ? 0 : (dist1 < dist2 ? -1 : 1);
				}
			}
		});
		return ordered.Count > 0 ? ordered[0] : null;
#endif
	}

	public static Player ChoosePassTargetCheckBall(Player player, GameMatch match)
	{
		List<Player> teammates = new List<Player>(player.m_team.members);
		teammates.Remove(player);
		if (teammates.Count == 0)
			return null;

		// Teammates in far area.
		List<Player> matesFar = new List<Player>();
		foreach (Player mate in teammates)
		{
			if (match.mCurScene.mGround.GetArea(mate) == Area.eFar)
				matesFar.Add(mate);
		}
		if (matesFar.Count > 0)	// There are teammates in far area, pass to most prior mate.
		{
			return AIUtils.GetMostPriorPassTarget(matesFar, player);
		}
		else	// No teammates in far area.
		{
			// Teammates in PG or SG's favor area.
			List<Player> matesOutSide = new List<Player>();
			RoadPathManager.SectorArea areaPG = Player.positionFavorSectors[PositionType.PT_PG];
			RoadPathManager.SectorArea areaSG = Player.positionFavorSectors[PositionType.PT_SG];
			foreach (Player mate in player.m_team.members)
			{
				if (RoadPathManager.Instance.InSectors(areaPG, mate.position) ||
					RoadPathManager.Instance.InSectors(areaSG, mate.position))
					matesOutSide.Add(mate);
			}
			if (matesOutSide.Count > 0)	// There are teammates in PG or SG's favor area, pass to most prior mate if its not myself.
			{
				Player passTarget = AIUtils.GetMostPriorPassTarget(matesOutSide, player);
				return passTarget == player ? null : passTarget;
			}
			else	// No teammates in PG or SG's favor area.
			{
				List<Player> teammatesPGSG = new List<Player>();
				foreach (Player mate in teammates)
				{
					if (mate.m_matchPosition == PositionType.PT_PG || mate.m_matchPosition == PositionType.PT_SG)
						teammatesPGSG.Add(mate);
				}
				if (player.m_matchPosition == PositionType.PT_C)
				{
					if (teammatesPGSG.Count > 0)	// Pass to most prior PG or SG.
					{
						return AIUtils.GetMostPriorPassTarget(teammatesPGSG, player);
					}
					else	// Pass to nearest non-C teammate.
					{
						Player nearestNonC = null;
						IM.Number minDist = IM.Number.max;
						foreach (Player mate in teammates)
						{
							if (mate.m_matchPosition != PositionType.PT_C)
							{
								IM.Number curDist = GameUtils.HorizonalDistance(mate.position, player.position);
								if (curDist < minDist)
								{
									minDist = curDist;
									nearestNonC = mate;
								}
							}
						}
						return nearestNonC;
					}
				}
				else if (player.m_matchPosition == PositionType.PT_PF)
				{
					if (teammatesPGSG.Count > 0)	// Pass to most prior PG or SG.
					{
						return AIUtils.GetMostPriorPassTarget(teammatesPGSG, player);
					}
					else	// Pass to nearest non-PF teammate.
					{
						Player nearestNonPF = null;
                        IM.Number minDist = IM.Number.max;
						foreach (Player mate in teammates)
						{
							if (mate.m_matchPosition != PositionType.PT_PF)
							{
								IM.Number curDist = GameUtils.HorizonalDistance(mate.position, player.position);
								if (curDist < minDist)
								{
									minDist = curDist;
									nearestNonPF = mate;
								}
							}
						}
						return nearestNonPF;
					}
				}
				else
					return null;
			}
		}
	}

    /**取最优传球目标*/
	public static Player GetMostPriorPassTarget(List<Player> players, Player passer)
	{
		if (players.Count == 1)
			return players[0];

        IM.Number maxPrior = IM.Number.min;
		Player mostPriorPlayer = null;
		foreach (Player player in players)
		{
			IM.Number curPrior = IM.Number.zero;
			// favorite area
            IM.Number weight = IM.Number.half;
			bool inFavorSector = RoadPathManager.Instance.InSectors(player.m_favorSectors, player.position);
			if (inFavorSector)
			{
				if (player.m_matchPosition == PositionType.PT_C || player.m_matchPosition == PositionType.PT_SG)
					curPrior += 2 * weight;
				else
					curPrior += 1 * weight;
			}
			else
			{
				if (player.m_matchPosition == PositionType.PT_PG)
					curPrior += 1 * weight;
				else
					curPrior += 0 * weight;
			}
			// distance to rival player
			weight = new IM.Number(0,200);
			IM.Number minDist2NearestRival;
			GetNearestRival(player, out minDist2NearestRival);
			if (new IM.Number(8) <= minDist2NearestRival)
				curPrior += 3 * weight;
			else if (new IM.Number(5) < minDist2NearestRival && minDist2NearestRival < new IM.Number(8))
				curPrior += 1 * weight;
			else
				curPrior += 0 * weight;
			// distance to defense target
            weight = IM.Number.one;
            IM.Number dist2DefenseTarget = GameUtils.HorizonalDistance(player.position, player.m_defenseTarget.position);
			if (dist2DefenseTarget >= new IM.Number(6))
				curPrior += 1 * weight;
			else
				curPrior += 0 * weight;
			// defense target unmovable
			weight = new IM.Number(0,200);
			bool defenseTargetUnmovable = !player.m_defenseTarget.m_enableMovement;
			if (defenseTargetUnmovable)
				curPrior += 1 * weight;
			else
				curPrior += 0 * weight;
			// distance to passer
			weight = new IM.Number(0,300);
			IM.Number dist2passer = GameUtils.HorizonalDistance(player.position, passer.position);
			if (dist2passer >= new IM.Number(10))
				curPrior += -2 * weight;
			else if (dist2passer > new IM.Number(5) && dist2passer < new IM.Number(10))
				curPrior += -1 * weight;
			else
				curPrior += 0 * weight;
			// fighting capacity(mate)
			weight = IM.Number.half;
			IM.Number mateFCSum = IM.Number.zero;
			int mateCount = 0;
			foreach (Player mate in players)
			{
				if (mate != player)
				{
					mateFCSum += mate.m_fightingCapacity;
					++mateCount;
				}
			}
			IM.Number mateFCAverage = mateFCSum / mateCount;
			IM.Number ratio = player.m_fightingCapacity / mateFCAverage;
			if (ratio >= IM.Number.two)
				curPrior += 3 * weight;
			else if (ratio >= new IM.Number(1,500) && ratio < IM.Number.two)
				curPrior += 2 * weight;
			else if (ratio >= IM.Number.one && ratio < new IM.Number(1,500))
				curPrior += 1 * weight;
			else if (ratio >= IM.Number.half && ratio < IM.Number.one)
				curPrior += 0 * weight;
			else
				curPrior += -1 * weight;
			// fighting capacity(rival)
			weight = IM.Number.half;
			ratio = player.m_fightingCapacity /player.m_defenseTarget.m_fightingCapacity;
			if (ratio >= IM.Number.two)
				curPrior += 3 * weight;
			else if (ratio >= new IM.Number(1,500) && ratio < IM.Number.two)
				curPrior += 2 * weight;
			else if (ratio >= IM.Number.one && ratio < new IM.Number(1,500))
				curPrior += 1 * weight;
			else if (ratio >= IM.Number.half && ratio < IM.Number.one)
				curPrior += 0 * weight;
			else
				curPrior += -1 * weight;

			if (curPrior > maxPrior)
			{
				maxPrior = curPrior;
				mostPriorPlayer = player;
			}
		}
		return mostPriorPlayer;
	}

	public static Player GetNearestRival(Player player, out IM.Number dist)
	{
        IM.Number minDist = IM.Number.max;
		Player nearestRival = null;
		foreach (Player rival in player.m_defenseTarget.m_team.members)
		{
            IM.Number curDist = GameUtils.HorizonalDistance(player.position, rival.position);
			if (curDist < minDist)
			{
				minDist = curDist;
				nearestRival = rival;
			}
		}
		dist = minDist;
		return nearestRival;
	}

	static int[] priority = { 0, 4, 3, 5, 1, 2 };
    /**追球*/
	public static bool ShouldTraceBall(UBasketball ball, Player player)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;

		if (match.m_stateMachine.m_curState.m_eState == MatchState.State.eTipOff)
		{
			if (player.m_team.GetMember(0) != player)
				return false;
		}

		IM.Vector3 targetPos;
		if (ball.m_ballState == BallState.eRebound)
		{
			targetPos = ball.position;
			targetPos.y = IM.Number.zero;
		}
		else if (ball.m_ballState == BallState.eLoseBall)
		{
			targetPos = ball.position;
			targetPos.y = IM.Number.zero;

			Player nearestPlayer = null;
			IM.Number minTime = IM.Number.max;
			foreach (Player mate in player.m_team.members)
			{
				IM.Number dist = GameUtils.HorizonalDistance(mate.position, targetPos);
				if (mate.m_position == PositionType.PT_PG || mate.m_position == PositionType.PT_SG)
					dist -= PlayerState_BodyThrowCatch.GetMaxDistance(mate);
                IM.Number speed = mate.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
				IM.Number time = dist / speed;
				if (time < minTime)
				{
					minTime = time;
					nearestPlayer = mate;
				}
			}
			return player == nearestPlayer;
		}
		else if (ball.m_ballState == BallState.eUseBall_Shoot && ball.m_owner == null)
		{
			targetPos = match.mCurScene.mBasket.m_vShootTarget;
			targetPos.y = IM.Number.zero;
		}
		else
			return false;

		int playerPriority = priority[(int)player.m_position];
		IM.Number distPlayer = GameUtils.HorizonalDistance(targetPos, player.position);
		foreach (Player mate in player.m_team.members)
		{
			if (mate != player)
			{
				int matePriority = priority[(int)mate.m_position];
				IM.Number distMate = GameUtils.HorizonalDistance(targetPos, mate.position);
                if (matePriority >= playerPriority && (distPlayer - distMate) > IM.Number.half)
					return false;
			}
		}
		return true;
	}
	
    /**是否可以盖帽*/
	public static bool CanBlock(Player player, Player target, IM.Number deviation, IM.Number timingRatio, IM.Vector3 vShootTarget)
	{
		if( !PlayerState_Block.InBlockArea(target, player, vShootTarget) )
			return false;

		//Debug.Log("CanBlock, Dev:" + deviation + " Timing:" + timingRatio);
		PlayerState curState = target.m_StateMachine.m_curState;
		if (AIUtils.IsAttacking(target))
		{
			//假投篮收手时不盖
			if (curState.m_eState == PlayerState.State.ePrepareToShoot &&
				target.animMgr.GetSpeed(curState.curAction) < IM.Number.zero)
				return false;
			Command cmd = Command.None;
			if (curState.m_eState == PlayerState.State.ePrepareToShoot ||
				curState.m_eState == PlayerState.State.eShoot)
				cmd = Command.Shoot;
			else if (curState.m_eState == PlayerState.State.eLayup)
				cmd = Command.Layup;
			else if (curState.m_eState == PlayerState.State.eDunk)
				cmd = Command.Dunk;
			IM.Number frameRate = target.animMgr.GetFrameRate(curState.curAction);
			var animAttr = target.m_animAttributes.GetAnimAttrById(cmd, curState.curAction);
			var frameBlockable = animAttr.GetKeyFrame("blockable") as PlayerAnimAttribute.KeyFrame_Blockable;
			IM.Number beginTime = frameBlockable.frame / frameRate;
			IM.Number endTime = beginTime + frameBlockable.blockFrame / frameRate;
			if (curState.m_eState == PlayerState.State.eShoot)
			{
				var framePrepare = animAttr.GetKeyFrame("OnPrepareToShoot");
				if (framePrepare != null)
				{
					beginTime -= framePrepare.frame / frameRate;
					endTime -= framePrepare.frame / frameRate;
				}
			}
			//Debug.Log("CanBlock, Before deviation, BeginTime:" + beginTime + " EndTime:" + endTime);
			IM.Number length = endTime - beginTime;
			IM.Number timeDev = length * deviation;
			beginTime -= timeDev;
			endTime -= timeDev;
			IM.Number curTime = curState.time;
			bool canBlock = (beginTime + length * timingRatio) <= curTime && curTime <= endTime;
			//Debug.Log("CanBlock, " + beginTime + " " + endTime + " " + length + " " + curTime + " " + canBlock);
			return canBlock;
		}
		return false;
	}

	static int[] prValues = {0, 4, 2, 3, 1, 1};
	// return: 0 - can't, 1 - can, 2 - move to pick and roll pos
    /**是否能挡拆*/
	public static int CanPickAndRoll(Player player)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		int result = 0;
		if (player.m_team.m_role == GameMatch.MatchRole.eOffense && !player.m_bWithBall && ball.m_owner != null)
		{
			// teammate in pick and roll
			foreach (Player mate in player.m_team.members)
			{
				if (mate != player && mate.m_StateMachine.m_curState.m_eState == PlayerState.State.ePickAndRoll)
					return result;
			}
			Area area = match.mCurScene.mGround.GetArea(ball.m_owner);
			if (area == Area.eNear || area == Area.eSpecial)
				return result;
			area = match.mCurScene.mGround.GetArea(player);
			if (area == Area.eNear || area == Area.eSpecial)
				return result;
			Player defender = ball.m_owner.m_defenseTarget;
			int myPR = prValues[(int)player.m_position];
			int defenderPR = prValues[(int)defender.m_position];
			if (myPR < defenderPR)
				return result;
			IM.Vector3 dirDefender2Player = GameUtils.HorizonalNormalized(player.position, defender.position);
			IM.Vector3 dirMove = ball.m_owner.moveDirection;
			IM.Number angle = IM.Vector3.Angle(dirMove, dirDefender2Player);
			IM.Number dist = GameUtils.HorizonalDistance(player.position, defender.position);
			if (angle <= new IM.Number(45) && dist <= new IM.Number(0,700))
				result = 1;
			else if (angle <= new IM.Number(30) && dist <= new IM.Number(3))
				result = 2;
		}
		return result;
	}

	public static IM.Number GetStealRate(Player stealer, Player target, GameMatch match)
	{
		if (stealer.m_position == PositionType.PT_C)
			return IM.Number.zero;
		else if (stealer.m_position != PositionType.PT_PG)
		{
			int idx = RoadPathManager.Instance.CalcSectorIdx(target.position);
			if (idx < 0)
				return IM.Number.zero;
			int columns = RoadPathManager.Instance.m_AngleList.Count;
			int rows = RoadPathManager.Instance.m_DistanceList.Count;
			int row = idx / columns;
			if (row < 3 || row >= rows - 2)	// Do not steal in near area and far area.
                return IM.Number.zero;
			/*
			Area targetArea = match.mCurScene.mGround.GetArea(target);
			if (targetArea != Area.eMiddle)
				return 0f;
			*/
		}
		bool canSteal = false;
        IM.Number stealRate = IM.Number.zero;
		if (target.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold || 
			target.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
		{
			canSteal = true;
			stealRate = new IM.Number(0,50);
		}
		else if (target.m_StateMachine.m_curState.m_eState == PlayerState.State.ePrepareToShoot)
		{
			if (!target.m_bForceShoot)
			{
				canSteal = true;
				stealRate = new IM.Number(0,200);
			}
		}
		if (canSteal)
		{
			bool hasBlocker = false;
			foreach (Player player in stealer.m_team.members)
			{
                if (player != stealer && PlayerState_Block.InBlockArea(target, player, match.mCurScene.mBasket.m_vShootTarget))
				{
					hasBlocker = true;
					break;
				}
			}
			if (hasBlocker)
			{
				stealRate += new IM.Number(0,50);
			}
			return stealRate;
		}
		return IM.Number.zero;
	}

    /**选择突破技能（分向上突破，向左突破，向右突破)*/
	public static SkillInstance ChooseCrossSkill(List<SkillInstance> basicSkills, bool horiCross, bool left)
	{
		foreach (SkillInstance skillInst in basicSkills)
		{
			foreach (SkillAction action in skillInst.skill.actions)
			{
				for (int i = 0; i < action.inputs.Count; ++i)
				{
					SkillInput input = action.inputs[i];
					bool matched = false;
					switch (input.moveDir)
					{
						case EDirection.eLeft:
							matched = horiCross && left;
							break;
						case EDirection.eRight:
							matched = horiCross && !left;
							break;
						case EDirection.eForward:
							if (!horiCross)
							{
								matched = (left && input.moveAngleRange.x >= 180 && input.moveAngleRange.y >= 180) ||
									(!left && input.moveAngleRange.x < 180 && input.moveAngleRange.y < 180);
							}
							break;
					}
					if (matched)
					{
						skillInst.curAction = action;
						skillInst.matchedKeyIdx = i;
						return skillInst;
					}
				}
			}
		}
		return null;
	}

    /**选择切入技能*/
	public static SkillInstance ChooseCutInSkill(List<SkillInstance> basicSkills, bool left)
	{
		foreach (SkillInstance skillInst in basicSkills)
		{
			foreach (SkillAction action in skillInst.skill.actions)
			{
				for (int i = 0; i < action.inputs.Count; ++i)
				{
					SkillInput input = action.inputs[i];
					bool matched = false;
					switch (input.moveDir)
					{
						case EDirection.eLeft:
							matched = left;
							break;
						case EDirection.eRight:
							matched = !left;
							break;
					}
					if (matched)
					{
						skillInst.curAction = action;
						skillInst.matchedKeyIdx = i;
						return skillInst;
					}
				}
			}
		}
		return null;
	}
}
