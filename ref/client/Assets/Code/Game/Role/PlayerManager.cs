using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;

public class PlayerManager
{
	public List<Player>	m_Players;
	
	public IEnumerator<Player> GetEnumerator(){ return m_Players.GetEnumerator(); }
	
	public PlayerManager()
	{
		m_Players = new List<Player>();
	}

	public Player CreatePlayer(RoleInfo roleInfo, Team team)
	{
		Player newPlayer = new Player(roleInfo, team);
		m_Players.Add(newPlayer);
		return newPlayer;
	}

	public Player GetPlayerById(uint id)
	{
		return m_Players.Find( (Player player)=>{return id == player.m_id;} );
	}

	public Player GetPlayerByRoomId(uint id)
	{
		return m_Players.Find( (Player player)=>{return id == player.m_roomPosId;} );
	}

	public Player GetPlayerByIndex(int index)
	{
		if (index >= m_Players.Count)
			return null;
		return m_Players[index];
	}

    public void RemovePlayer(Player player)
    {
		player.Release();
        m_Players.Remove(player);
    }
	
	public void RemoveAllPlayers()
	{
		m_Players.ForEach( (Player p)=>{ p.Release(); });
		m_Players.Clear();
	}

	public void IsolateCollision( Player isoPlayer, bool bIsolated )
	{
		Collider isoCollidePlayer = isoPlayer.gameObject.GetComponent<Collider>();
		if( isoCollidePlayer == null )
			return;
		foreach( Player player in m_Players )
		{
			if(player == isoPlayer)
				continue;
			if (!player.gameObject.activeInHierarchy)
				continue;
			Collider colliderPlayer = player.gameObject.GetComponent<Collider>();
			if( colliderPlayer != null )
				Physics.IgnoreCollision(isoCollidePlayer, colliderPlayer, bIsolated);
		}
	}
}