using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class AIConditionValidator : Singleton<AIConditionValidator>
{
	public enum Type
	{
		FinalTime,					    	//进攻时间小于X秒
		NeedCheckBall,				    	//需要带出3分线
		TipOff,						    	//跳球
		BallRebounding,				    	//篮板
		BallOnGround,				    	//地板球
		BallFlying,					    	//飞行球
		ShootingGoal,				    	//进球了
		DoubleDribble,				    	//二次运球锁定
		NoDefender,					    	//空位
		HasDefender,						//被贴身
		DefenderAroundMe,			    	//自身范围X米内有防守方
		StaminaRemain,				    	//剩余体力大于X
		MateOutOf3PT,				    	//有队员在3分线外
		NearToBasket,				    	//距离篮筐小于X米
		NearToBallOwnerMate,				//距离持球队友小于X米
		NearestTo3PT,				    	//离3分线最近
		NearestToBallPlacement,		    	//离球落点最近
		NearToBallPlacement,				//距离球落点X米
		FarFromNearesterOfBallPlacement,	//距离离球落点最近球员超过X米
		PlayerRequireBall,				    //玩家要球
		NPCRequireBall,					    //NPC要球
		AttackerWithBall,				    //进攻方持球
		AttackerPreparingToShoot,		    //进攻方假投篮
		AttackerShooting,				    //进攻方投篮
		AttackerLayup,					    //进攻方上篮
		AttackerDunk,					    //进攻方扣篮
		AttackerClutchMove,				    //进攻方背打移动
		AttackerClutchCross,				//进攻方背打突破
		AttackerCross,					    //进攻方突破
		AttackerRush,					    //进攻方加速跑
		CanRebound,						    //可抢篮板
		InAOD,							    //处于防守区域内
		InStealArea,						//处于抢断范围
		InShootNearArea,					//在篮下投篮区域
		InShootMiddleArea,				    //在中距投篮区域
		InShootFarArea,					    //在远投区域
		InLayupNearArea,					//在篮下上篮区域
		InLayupMiddleArea,				    //在中距上篮区域
		InDunkNearArea,					    //在篮下扣篮区域
		InDunkMiddleArea,				    //在中距扣篮区域
		InPosition,						    //在职业位置
		Count,
	}

	GameMatch match { get { return GameSystem.Instance.mClient.mCurMatch; } }

	Dictionary<Type, MethodInfo> funcs = new Dictionary<Type, MethodInfo>();

	public AIConditionValidator()
	{
		for (int i = 0; i < (int)Type.Count; ++i)
		{
			Type t = (Type)i;
			MethodInfo method = GetType().GetMethod(t.ToString());
			if (method != null)
				funcs.Add(t, method);
			//else
			//	Logger.LogError("There's no validate func for condition type: " + t.ToString());
		}
	}

	public bool Validate(Type condType, Player player, float param)
	{
		MethodInfo method;
		if (!funcs.TryGetValue(condType, out method))
		{
			Logger.LogError("There's no validate func for condition type: " + condType.ToString());
			return false;
		}

		return (bool)method.Invoke(this, new System.Object[] { player, param });
	}

	public bool FinalTime(Player player, float param)
	{
		return match.IsFinalTime(new IM.Number(3));
	}

	public bool NeedCheckBall(Player player, float param)
	{
		return match.m_ruler.m_bToCheckBall;
	}

	public bool TipOff(Player player, float param)
	{
		return match.m_stateMachine.m_curState.m_eState == MatchState.State.eTipOff;
	}

	public bool BallRebounding(Player player, float param)
	{
		return match.mCurScene.mBall.m_ballState == BallState.eRebound;
	}

	public bool BallOnGround(Player player, float param)
	{
		return match.mCurScene.mBall.m_bounceCnt > 0;
	}

	public bool BallFlying(Player player, float param)
	{
		return match.mCurScene.mBall.m_ballState == BallState.eUseBall_Shoot;
	}

	public bool DoubleDribble(Player player, float param)
	{
		PlayerState.State state = player.m_StateMachine.m_curState.m_eState;
		return state == PlayerState.State.eHold && player.m_bMovedWithBall;
	}

	public bool PlayerRequireBall(Player player, float param)
	{
		Player m_mainRole = match.m_mainRole;
		if (m_mainRole.m_team != player.m_team)
			return false;
		return m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall;
	}

	public bool NPCRequireBall(Player player, float param)
	{
		Player m_mainRole = match.m_mainRole;
		foreach (Player p in player.m_team.members)
		{
			if (p.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall && p != m_mainRole)
				return true;
		}
		return false;
	}

	public bool NoDefender(Player player, float param)
	{
		Player defender = null;
		if (player.m_defenseTarget != null)
		{
			Team defenserTeam = player.m_defenseTarget.m_team;
			foreach (Player p in defenserTeam.members)
			{
				if (player.m_AOD.GetStateByPos(p.position) != AOD.Zone.eInvalid)
				{
					defender = p;
					break;
				}
			}
		}

		return defender == null;
	}

	public bool HasDefender(Player player, float param)
	{
		return !NoDefender(player, param);
	}

	public bool DefenderAroundMe(Player player, float param)
	{
		foreach (var d in player.m_defenseTarget.m_team.members)
		{
			if (GameUtils.HorizonalDistance(d.position, player.position) < new IM.Number(3))
				return true;
		}
		return false;
	}

	public bool NearToBasket(Player player, IM.Number param)
	{
		IM.Vector3 basket = match.mCurScene.mBasket.m_rim.center;
		return GameUtils.HorizonalDistance(basket, player.position) < new IM.Number(2,100);
	}

	public bool NearToBallOwnerMate(Player player, float param)
	{
		Player owner = match.mCurScene.mBall.m_owner;
		if (owner != null)
		{
			if (owner.m_team == player.m_team)
			{
                return GameUtils.HorizonalDistance(owner.position, player.position) < new IM.Number(2, 100);
			}
		}
		return false;
	}

	public bool StaminaRemain(Player player, IM.Number stamina)
	{
        return player.m_stamina.m_curStamina >= new IM.Number(10);
	}

	public bool MateOutOf3PT(Player player, IM.Number param)
	{
		foreach (Player mate in player.m_team.members)
		{
			if (mate != player)
			{
				if (!match.mCurScene.mGround.In3PointRange(mate.position.xz))
					return true;
			}
		}
		return false;
	}

	public bool NearestTo3PT(Player player, IM.Number param)
	{
		PlayGround ground = match.mCurScene.mGround;
        IM.Number myDist = ground.GetDistTo3PTLine(player.position.xz);
		if (myDist < IM.Number.zero)
			return true;
		foreach (Player mate in player.m_team.members)
		{
			if (mate != player && ground.GetDistTo3PTLine(mate.position.xz) < myDist)
				return false;
		}
		return true;
	}

    public bool NearestToBallPlacement(Player player, IM.Number param)
	{
		UBasketball ball = match.mCurScene.mBall;
        IM.Number myDist = GameUtils.HorizonalDistance(player.position, ball.position);
		foreach (Player p in player.m_team.members)
		{
			if (p != player && GameUtils.HorizonalDistance(p.position, ball.position) < myDist)
				return false;
		}
		foreach (Player p in player.m_defenseTarget.m_team.members)
		{
			if (GameUtils.HorizonalDistance(p.position, ball.position) < myDist)
				return false;
		}
		return true;
	}

    public bool NearToBallPlacement(Player player, IM.Number param)
	{
		UBasketball ball = match.mCurScene.mBall;
        IM.Number myDist = GameUtils.HorizonalDistance(player.position, ball.position);
		return myDist <= new IM.Number(3);
	}

    public bool FarFromNearesterOfBallPlacement(Player player, IM.Number param)
	{
		UBasketball ball = match.mCurScene.mBall;
		Player nearester = null;
        IM.Number nearestDist = IM.Number.max;
		foreach (Player p in player.m_team.members)
		{
            IM.Number dist = GameUtils.HorizonalDistance(p.position, ball.position);
			if (dist < nearestDist)
			{
				nearestDist = dist;
				nearester = p;
			}
		}
		foreach (Player p in player.m_defenseTarget.m_team.members)
		{
            IM.Number dist = GameUtils.HorizonalDistance(p.position, ball.position);
			if (dist < nearestDist)
			{
				nearestDist = dist;
				nearester = p;
			}
		}
        IM.Number distToNearester = GameUtils.HorizonalDistance(player.position, nearester.position);
		return distToNearester < new IM.Number(3);
	}

    public bool ShootingGoal(Player player, IM.Number param)
	{
		return match.mCurScene.mBall.m_bGoal;
	}

    public bool InAOD(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_AOD.GetStateByPos(player.position) != AOD.Zone.eInvalid;
		}
		return false;
	}

	public bool InStealArea(Player player, IM.Number param)
	{
		UBasketball ball = match.mCurScene.mBall;
		//distance
		IM.Number distThreashold = IM.Number.one;
		if (GameUtils.HorizonalDistance(player.position, ball.position) > distThreashold)
			return false;

		//is in front of target
		IM.Vector3 dir = player.position - player.m_defenseTarget.position;
		dir.Normalize();
		IM.Number angle = IM.Vector3.Angle(player.m_defenseTarget.forward, dir);
		if (angle > new IM.Number(90))
			return false;

		return true;
	}

	//public bool InShootNearArea(Player player, float param)
	//{

	//}

    public bool AttackerWithBall(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_bWithBall;
		}
		return false;
	}

    public bool AttackerPreparingToShoot(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.ePrepareToShoot;
		}
		return false;
	}

    public bool AttackerShooting(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eShoot;
		}
		return false;
	}

    public bool AttackerLayup(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eLayup;
		}
		return false;
	}

    public bool AttackerDunk(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eDunk;
		}
		return false;
	}

    public bool AttackerClutchMove(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			PlayerState state = defenseTarget.m_StateMachine.m_curState;
			return state.m_eState == PlayerState.State.eBackCompete;
		}
		return false;
	}

    public bool AttackerClutchCross(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eBackTurnRun;
		}
		return false;
	}

    public bool AttackerCross(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eCrossOver;
		}
		return false;
	}

    public bool AttackerRush(Player player, IM.Number param)
	{
		Player defenseTarget = player.m_defenseTarget;
		if (defenseTarget != null)
		{
			return defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eRush;
		}
		return false;
	}
}
