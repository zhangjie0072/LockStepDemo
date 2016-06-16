using System;
using System.Collections.Generic;
using fogs.proto.msg;

using UnityEngine;

public class Team
{
	public enum Side
	{
		eNone,
		eHome = 1,
		eAway = 2,
	}
	
	public IEnumerator<Player> GetEnumerator(){ return m_members.GetEnumerator(); }
	public Side m_side{get; private set;}
	
	public GameMatch.MatchRole m_role;
	public List<Player> members { get { return m_members; } }
	
	private List<Player>	m_members = new List<Player>();
	
	private IM.Number m_fightingCapacity = IM.Number.zero;
    public IM.Number fightingCapacity
	{
		get
		{
			if (m_fightingCapacity == IM.Number.zero)
			{
				foreach (Player member in members)
				{
					m_fightingCapacity += member.m_fightingCapacity;
				}
			}
			return m_fightingCapacity;
		}
	}

	int initialBallHolderIndex = 0;

	static int[] positionPriority = { 0, 4, 3, 5, 1, 2 };

	public Team(Side side)
	{
		m_side = side;
		m_role = GameMatch.MatchRole.eNone;
	}
	
	public void AddMember(Player player)
	{
		m_members.Add(player);
	}

    public void RemoveMember(Player player)
    {
        m_members.Remove(player);
    }
	
	public Player GetMember( int idx )
	{
		if( m_members.Count <= idx )
			return null;
		
		return m_members[idx];
	}
	
	public int GetMemberCount()
	{
		return m_members.Count;
	}

	public void Clear()
	{
		m_members.Clear();
	}

	public void CopyTo(Player[] players)
	{
		m_members.CopyTo(players);
	}

	public Player GetInitialBallHolder()
	{
		Player holder = GetMember(initialBallHolderIndex);
		Logger.Log(m_side + " InitialBallHolder: " + initialBallHolderIndex + " " + holder.m_name);
		++initialBallHolderIndex;
		if (initialBallHolderIndex >= GetMemberCount())
			initialBallHolderIndex = 0;
		return holder;
	}

	public List<Player> GetSortedPlayersByDistance(Player target, bool ascend = true)
	{
		List<Player> players = new List<Player>(m_members);
		players.Sort( (p1, p2)=>
		             {
			IM.Number fP1ToTarget = GameUtils.HorizonalDistance(p1.position, target.position);
			IM.Number fP2ToTarget = GameUtils.HorizonalDistance(p2.position, target.position);
			int sign = ascend ? 1 : -1;
			if( fP2ToTarget > fP1ToTarget )
				return -1 * sign;
			else
				return 1 * sign;
			}
		);
		return players;
	}

	public void SortMember(bool bByPosition = false)
	{
		if( !bByPosition )
		{
			m_members.Sort((p1, p2) =>
			{
				int prior1 = positionPriority[(int)(p1.m_position)];
				int prior2 = positionPriority[(int)(p2.m_position)];
				if (prior1 < prior2)
					return 1;
				else if (prior1 > prior2)
					return -1;
				else
					return 0;
			});
		}
		else
		{
			m_members.Sort((p1, p2) =>
			{
				int prior1 = (int)p1.m_startPos;
				int prior2 = (int)p2.m_startPos;
				if (prior1 > prior2)
					return 1;
				else if (prior1 < prior2)
					return -1;
				else
					return 0;
			});
		}

		//Logger.Log(" team seq: " + m_members[0].m_position  + " " + m_members[0].m_startPos + " " + m_members[1].m_position + " " + m_members[1].m_startPos  + " " + m_members[2].m_position + " " + m_members[2].m_startPos);
	}
}


