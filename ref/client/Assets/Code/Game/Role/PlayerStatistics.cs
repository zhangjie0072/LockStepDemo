using fogs.proto.msg;
using UnityEngine;

public class PlayerStatistics
{
	public enum StatType
	{
		Score,
		Assist,
		Rebound,
		Steal,
		Block,
		Ground,
	}

	enum GoalType
	{
		eNone,
		eGoalNear,
		eGoalMiddle,
		eGoalFar,
	}
	private	GoalType	mType;

	public delegate void Delegate(StatType type, PlayerStatistics playerStatistics);
	public Delegate onStatUpdated;

	public uint success_rebound_times
	{
		get { return data.success_rebound_times; }
		set
		{
			data.success_rebound_times = value;
			if (onStatUpdated != null)
				onStatUpdated(StatType.Rebound, this);
		}
	}
	public uint success_block_times
	{
		get { return data.success_block_times; }
		set
		{
			data.success_block_times = value;
			RefreshKing(StatType.Block);
			if (onStatUpdated != null)
				onStatUpdated(StatType.Block, this);
		}
	}
	public uint success_steal_times
	{
		get { return data.success_steal_times; }
		set
		{
			data.success_steal_times = value;
			RefreshKing(StatType.Steal);
			if (onStatUpdated != null)
				onStatUpdated(StatType.Steal, this);
		}
	}
	public uint secondary_attack
	{
		get { return data.secondary_attack; }
		set
		{
			data.secondary_attack = value;
			RefreshKing(StatType.Assist);
			if (onStatUpdated != null)
				onStatUpdated(StatType.Assist, this);
		}
	}
	public uint floor_ball
	{
		get { return data.floor_ball; }
		set
		{
			data.floor_ball = value;
			if (onStatUpdated != null)
				onStatUpdated(StatType.Ground, this);
		}
	}

	public uint skill;

	public uint near_goal
	{
		get { return data.near_score + data.layup_near_score + data.dunk_near_score; }
	}
	public uint near_score
	{
		get { return near_goal * (uint)m_curMatch.GetScore(GlobalConst.PT_2); }
	}
	public uint mid_goal
	{
		get { return data.mid_score + data.layup_mid_score + data.dunk_mid_score; }
	}
	public uint mid_score
	{
		get { return mid_goal * (uint)m_curMatch.GetScore(GlobalConst.PT_2); }
	}
	public uint far_goal
	{
		get { return data.far_score; }
	}
	public uint far_score
	{
		get { return far_goal * (uint)m_curMatch.GetScore(GlobalConst.PT_3); }
	}

	public uint total_score
	{
		get { return near_score + mid_score + far_score; } 
	}

	public uint infield_shoot
	{
		get { return data.near_shoot + data.layup_near_shoot + data.dunk_near_shoot + data.mid_shoot + data.layup_mid_shoot + data.dunk_mid_shoot; }
	}

	public uint outfield_shoot
	{
		get { return data.far_shoot; }
	}

	private Player		m_player;
	public Player player { get { return m_player; } }
	private GameMatch m_curMatch;
	public MatchRoleData data;

	public PlayerStatistics(Player player, GameMatch match)
	{
		m_player = player;
		m_curMatch = match;
		data = new MatchRoleData();
		data.role_id = m_player.m_id;
		data.index = m_player.m_roleInfo.index;
	}

	public void AddSkillUsage(uint skillId)
	{
		SkillUsageData skillUd = data.skill_data.Find( (SkillUsageData udata)=>{ return skillId == udata.skill_id; } );
		if( skillUd == null )
		{
			skillUd = new SkillUsageData();
			skillUd.skill_id = skillId;
			data.skill_data.Add( skillUd );
		}
		skillUd.usage += 1;
	}

	public void SkillUsageSuccess(uint skillId, bool bSuccess)
	{
		if( !bSuccess )
			return;
		SkillUsageData skillUd = data.skill_data.Find( (SkillUsageData udata)=>{ return skillId == udata.skill_id; } );
		if( skillUd == null )
		{
			Logger.LogError("Add SkillUsageData firstly.");
			return;
		}
		skillUd.success += 1;
	}

	public void ReadyToCountShoot()
	{
		mType = GoalType.eNone;

        IM.Vector3 pPosition = m_player.position;
        IM.Vector2 playerPos = pPosition.xz;
		if( m_curMatch.mCurScene.mGround.InFreeThrowLane(playerPos) )
			mType = GoalType.eGoalNear;
		else if( m_curMatch.mCurScene.mGround.In3PointRange(playerPos, IM.Number.zero) )
			mType = GoalType.eGoalMiddle;
		else
			mType = GoalType.eGoalFar;

		//Logger.Log("ready to count");
	}

	public void AddUpShoot()
	{
		UBasketball ball = m_curMatch.mCurScene.mBall;
		if (mType == GoalType.eGoalNear)
		{
			uint score = 1;
			if (ball.m_isLayup)
				data.layup_near_score += score;
			else if (ball.m_isDunk)
				data.dunk_near_score += score;
			else
				data.near_score += score;
		}
		else if (mType == GoalType.eGoalMiddle)
		{
			uint score = 1;
			if (ball.m_isLayup)
				data.layup_mid_score += score;
			else if (ball.m_isDunk)
				data.dunk_mid_score += score;
			else
				data.mid_score += score;
		}
		else if (mType == GoalType.eGoalFar)
			data.far_score += 1;

		RefreshKing(StatType.Score);

		if (onStatUpdated != null)
			onStatUpdated(StatType.Score, this);

		//Logger.Log("add up shoot");
	}

	public uint GetStatValue(StatType type)
	{
		switch (type)
		{
			case StatType.Assist:
				return secondary_attack;
			case StatType.Block:
				return success_block_times;
			case StatType.Ground:
				return floor_ball;
			case StatType.Rebound:
				return success_rebound_times;
			case StatType.Score:
				return total_score;
			case StatType.Steal:
				return success_steal_times;
		}
		return 0;
	}

	void SetStatKing(StatType type, uint value)
	{
		switch (type)
		{
			case StatType.Assist:
				data.assist_king = value;
				break;
			case StatType.Block:
				data.block_king = value;
				break;
			case StatType.Rebound:
				data.rebound_king = value;
				break;
			case StatType.Score:
				data.score_king = value;
				break;
			case StatType.Steal:
				data.steal_king = value;
				break;
		}
	}

	void RefreshKing(StatType type)
	{
		bool max = true;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			if (player != m_player)
			{
				if (player.mStatistics.GetStatValue(type) < GetStatValue(type))
					player.mStatistics.SetStatKing(type, 0);
				else
				{
					max = false;
					break;
				}
			}
		}
		if (max)
			SetStatKing(type, 1);
	}
}


