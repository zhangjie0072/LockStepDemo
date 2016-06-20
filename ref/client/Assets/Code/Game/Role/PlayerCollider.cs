using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCollider
{
	private Player m_Owner;
	private Dictionary<Player, bool> isColliderStayed = new Dictionary<Player, bool>();

	PseudoRandom random = new PseudoRandom();
	static HedgingHelper hedging = new HedgingHelper("Collide");

	private IM.Number m_fPickAndRollValidAngle = new IM.Number(30);

	public PlayerCollider(Player player)
	{
		m_Owner = player;
	}

	public void Update(IM.Number fDeltaTime)
	{
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		foreach( Player player in pm )
		{
			if( m_Owner == player )
				continue;

            IM.Number fDist = GameUtils.HorizonalDistance(m_Owner.position,player.position);
            IM.Number radius1 = (new IM.Number(1, 500) + GlobalConst.CHARACTER_SKIN_WIDTH) * m_Owner.scale.z;
            IM.Number radius2 = (new IM.Number(1, 500) + GlobalConst.CHARACTER_SKIN_WIDTH) * player.scale.z;
			if( fDist < ( radius1 + radius2) )
				_OnTriggerStay(player);
			else
				isColliderStayed[player] = false;

			PlayerState_PickAndRoll curPlayerState = m_Owner.m_StateMachine.m_curState as PlayerState_PickAndRoll;
			PlayerState_PickAndRoll curCollidePlayerState = player.m_StateMachine.m_curState as PlayerState_PickAndRoll;

			if( curPlayerState == null && curCollidePlayerState == null )
				continue;

			if( m_Owner.m_team.m_side != player.m_team.m_side )
			{
                IM.Number fSkillDistance = IM.Number.zero;
				if( curPlayerState != null )
				{
					fSkillDistance = curPlayerState.m_fInfluRadius;
				}
				else if( curCollidePlayerState != null )
				{
					fSkillDistance = curCollidePlayerState.m_fInfluRadius;
				}
				else 
					continue;

				if( GameUtils.HorizonalDistance(m_Owner.position, player.position) > fSkillDistance )
					continue;

				if( curPlayerState != null && !player.m_bWithBall )
				{
					if( player.moveDirection == IM.Vector3.zero )
						continue;
                    IM.Vector3 dirOwnerToPR = GameUtils.HorizonalNormalized(m_Owner.position, player.position);
					if( IM.Vector3.Angle(dirOwnerToPR, player.moveDirection) > m_fPickAndRollValidAngle )
						continue;
					curPlayerState.OnCollided(player);
				}
				else if( curCollidePlayerState != null && !m_Owner.m_bWithBall)
				{
					if( curCollidePlayerState.m_bEnablePickAndRoll)
					{
						if( m_Owner.moveDirection == IM.Vector3.zero )
							continue;

                        IM.Vector3 dirOwnerToPR = GameUtils.HorizonalNormalized(player.position, m_Owner.position);
                        if (IM.Vector3.Angle(dirOwnerToPR, m_Owner.moveDirection) > m_fPickAndRollValidAngle)
							continue;

						m_Owner.m_StateMachine.SetState(PlayerState.State.eBePickAndRoll);
						PlayerState_BePickedAndRolled pkState = m_Owner.m_StateMachine.m_curState as PlayerState_BePickedAndRolled;
						pkState.OnCollided(player);
						return;
					}
				}
			}
		}
	}

	void _OnTriggerStay (Player player)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if (match == null)
			return;

		if (match.GetMatchType() != GameMatch.Type.eBullFight &&
			match.GetMatchType() != GameMatch.Type.eCareer3On3 &&
			match.GetMatchType() != GameMatch.Type.eAsynPVP3On3 &&
			match.GetMatchType() != GameMatch.Type.e3AIOn3AI &&
			match.GetMatchType() != GameMatch.Type.e3On3 &&
			match.GetMatchType() != GameMatch.Type.ePVP_3On3 &&
			match.GetMatchType() != GameMatch.Type.ePVP_1PLUS &&
			match.GetMatchType() != GameMatch.Type.eUltimate21 &&
			match.GetMatchType() != GameMatch.Type.eGrabZone &&
			match.GetMatchType() != GameMatch.Type.ePractise)
			return;
		if( match.m_mainRole != m_Owner && !m_Owner.m_bIsAI )
			return;
 
		if( m_Owner.m_StateMachine.m_curState.m_eState == PlayerState.State.eFallLostBall 
		   || m_Owner.m_StateMachine.m_curState.m_eState == PlayerState.State.eKnocked )
			return;

		if( m_Owner.m_aiMgr != null && m_Owner.m_aiMgr.m_curState != null )
			m_Owner.m_aiMgr.m_curState.OnPlayerCollided(player);

		if (match.GetMatchType() == GameMatch.Type.eGrabZone ||
			match.GetMatchType() == GameMatch.Type.ePractise)
			return;

		bool isStayed = false;
		isColliderStayed.TryGetValue(player, out isStayed);
		if (isStayed)
			return;
		isColliderStayed[player] = true;

		PlayerState.State curPlayerState = m_Owner.m_StateMachine.m_curState.m_eState;
		PlayerState.State curCollidePlayerState = player.m_StateMachine.m_curState.m_eState;
		IM.Vector3 dirPlayerToCollide = GameUtils.HorizonalNormalized(m_Owner.position, player.position);
		IM.Number fAngle = IM.Vector3.Angle(m_Owner.forward, dirPlayerToCollide);
		if( fAngle < new IM.Number(60) 
		   && m_Owner.m_team.m_side != player.m_team.m_side
		   )
		{
			if( 
				curPlayerState == PlayerState.State.eBackTurnRun ||
				curPlayerState == PlayerState.State.eCutIn ||
				curPlayerState == PlayerState.State.eRush )
			{
				//突破/空切倒地几率=0.2+（防守者身体强度-突破者身体强度）*0.0004
                Dictionary<string, uint> playerData = m_Owner.m_finalAttrs;
				Dictionary<string, uint> colPlayerData = player.m_finalAttrs;
				if( playerData == null || colPlayerData == null)
				{
					Logger.LogError("Can not build player: " + m_Owner.m_name + " ,can not fight state by id: " + m_Owner.m_id );
					Logger.LogError("Can not build player: " + player.m_name + " ,can not fight state by id: " + player.m_id );
					return;
				}

				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				uint strength = 0;
				player.m_skillSystem.HegdingToValue("addn_strength", ref strength);
				IM.Number colPlayerStrength = (colPlayerData["strength"] + strength) * curMatch.GetAttrReduceScale("strength", player);

				m_Owner.m_skillSystem.HegdingToValue("addn_strength", ref strength);
				IM.Number playerStrength = (playerData["strength"] + strength) *curMatch.GetAttrReduceScale("strength", m_Owner);
                IM.Number fRate = hedging.Calc(playerStrength, colPlayerStrength);
				fRate = IM.Number.one - fRate;

                IM.Number fRandom = IM.Random.value;
				if( curPlayerState == PlayerState.State.eBackTurnRun )
				{
                    IM.Number uRate = fRate;
                    bool sumValue = random.AdjustRate(ref uRate);
					m_Owner.m_StateMachine.SetState(fRandom < fRate ? PlayerState.State.eFallLostBall : PlayerState.State.eKnocked);
					if (fRandom < fRate && sumValue)
						random.SumValue();
				}
				else if (curPlayerState == PlayerState.State.eCutIn)
				{
					fRate *= IM.Number.half;
					m_Owner.m_StateMachine.SetState(fRandom < fRate ? PlayerState.State.eFallGround : PlayerState.State.eKnocked);
				}
				else if (curPlayerState == PlayerState.State.eRush)
				{
					fRate *= new IM.Number(0,300);
					//防守者不被撞，无人持球时不被撞
					if (match.mCurScene.mBall.m_owner != null && m_Owner.m_team.m_role == GameMatch.MatchRole.eOffense)
					{
						if (fRandom < fRate)
							m_Owner.m_StateMachine.SetState(m_Owner.m_bWithBall ? PlayerState.State.eFallLostBall : PlayerState.State.eFallGround);
						else
							m_Owner.m_StateMachine.SetState(PlayerState.State.eKnocked);
					}
				}

				//Debugger.Instance.m_steamer.message = "Player: " + m_Owner.m_name + " strength: " + playerData["strength"];
				//Debugger.Instance.m_steamer.message += " Collided Player: " + colPlayer.m_Owner.m_name + " strength: " + colPlayerData["strength"];
				//Debugger.Instance.m_steamer.message += " Collide fall down rate: " + fRate + " Random: " + fRandom;
			}
		}
	}
}