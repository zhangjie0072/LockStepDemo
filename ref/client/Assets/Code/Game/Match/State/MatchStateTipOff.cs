using System.Collections.Generic;
using UnityEngine;
using fogs.proto.msg;

public class MatchStateTipOff
	: MatchState
{
	public System.Action onCounterDone;
	private bool counterDone = false;

	private GameObject m_goBeginUI;

	private static IM.Number fBallInitHeight = new IM.Number(3,500);
	private GameUtils.Timer mTimer;
	private GameUtils.Timer mTimerSound;

	public MatchStateTipOff(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eTipOff;
	}

	override public void OnEnter ( MatchState lastState )
	{
		if( m_match.m_needTipOff )
		{
            TipOffPos tipOffPos = GameSystem.Instance.MatchPointsConfig.TipOffPos;
			int homeCnt = m_match.m_homeTeam.GetMemberCount();
			for(int idx = 0; idx != homeCnt; idx++)
			{
                IM.Transform trOffensePos = tipOffPos.offenses_transform[idx];
                IM.Transform trDefensePos = tipOffPos.defenses_transform[idx];
				Player homePlayer = m_match.m_homeTeam.members[idx];
				if( homePlayer != null )
				{
					homePlayer.position = trOffensePos.position;
					homePlayer.rotation = trOffensePos.rotation;
				}
				Player awayPlayer = m_match.m_awayTeam.members[idx];
				if( awayPlayer != null )
				{
					awayPlayer.position = trDefensePos.position;
					awayPlayer.rotation = trDefensePos.rotation;
				}
			}
			m_match.m_needTipOff = false;
		}

		if( m_match.m_cam != null )
			m_match.m_cam.m_Zoom.ReleaseZoom();
		
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_bMovedWithBall = false;

			if (player.m_aiMgr != null)
				player.m_aiMgr.m_enable = false;
			
			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = true;
			
			if( player.m_pickupDetector != null )
				player.m_enablePickupDetector = true;

			if( player.m_AOD != null )
				player.m_AOD.visible = false;
			
			player.m_toSkillInstance = null;
			player.m_stamina.ResetStamina();

			player.m_StateMachine.SetState(PlayerState.State.eStand);
		}

		if (m_goBeginUI == null)
			m_goBeginUI = GameSystem.Instance.mClient.mUIManager.CreateUI("UIBeginCounter");
		if (m_goBeginUI == null)
		{
			Logger.Log("Error -- can not find ui resource " + "UIBeginCounter");
			return;
		}
		Animation anim = m_goBeginUI.GetComponentInChildren<Animation>();
		anim.Stop();
		anim.Play("counter");

		UBasketball ball = m_match.mCurScene.mBall;
		if( ball != null && ball.m_owner != null && m_match.m_uiMatch != null && m_match.EnableCounter24())
		{
			m_match.m_uiMatch.ShowCounter(true, ball.m_owner.m_team.m_side == Team.Side.eHome);
            m_match.m_count24TimeStop = true;
		}

		ball.Reset();
        IM.Number x1 = m_match.m_homeTeam.members[0].position.x;
        IM.Number x2 = m_match.m_awayTeam.members[0].position.x;
        IM.Number z =  m_match.m_homeTeam.members[0].position.z;
		ball.SetInitPos( new IM.Vector3( (x1 + x2) * IM.Number.half, fBallInitHeight, z) );
		ball.m_ballState = BallState.eNone;

		if(m_match.m_uiMatch != null && m_match.EnableCounter24())
		{
			m_match.m_uiMatch.ShowCounter(true, true);

			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}

		if (m_match.EnableEnhanceAttr())
			m_match.EnhanceAttr();

		mTimer = new GameUtils.Timer(new IM.Number(3), _OnCounterDone, 1);
        mTimerSound = new GameUtils.Timer(new IM.Number(3), PlaySound, 1);

		m_match.m_homeTeam.m_role = GameMatch.MatchRole.eNone;
		m_match.m_awayTeam.m_role = GameMatch.MatchRole.eNone;
		m_match.m_ruler.m_toCheckBallTeam = null;

		if (m_match is GameMatch_3ON3 || m_match is GameMatch_PracticeVs)
		{
			GameMatch_MultiPlayer mul = m_match as GameMatch_MultiPlayer;
			mul.SwitchMainrole(mul.m_homeTeam.members[0]);
		}
	}

	public override void OnEvent (PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if( m_match.m_stateMachine.m_curState != this )
			return;
		UBasketball ball = m_match.mCurScene.mBall;
		if (animEvent == PlayerActionEventHandler.AnimEvent.ePickupBall
		    || animEvent == PlayerActionEventHandler.AnimEvent.eRebound)
		{
			if (m_match.EnableSwitchRole())
				m_match.m_ruler.SwitchRole();
		}
	}

	/*
	public void SetAllPosition()
	{
		GameScene.PresetPos pos = m_match.mCurScene.m_tipOff;
		List<Player> homeTeamInOrder = new List<Player>();
		List<Player> awayTeamInOrder = new List<Player>();
		
		_SetPosition( m_match.m_homeTeam, pos.offense, homeTeamInOrder );
		_SetPosition( m_match.m_awayTeam, pos.defense, awayTeamInOrder );

		GameMatch_3ON3 match = m_match as GameMatch_3ON3;
        GameMatch_PracticeVs matchPracticeVs = m_match as GameMatch_PracticeVs;
		if (match != null)
		{
			match.SwitchMainrole(homeTeamInOrder[0]);
			homeTeamInOrder[0].m_team.GetInitialBallHolder();
		}
        else if( matchPracticeVs != null )
        {
            matchPracticeVs.SwitchMainrole(homeTeamInOrder[0]);
            homeTeamInOrder[0].m_team.GetInitialBallHolder();
        }
	}
	*/

	/*
	void _SetPosition(Team team, List<Transform> scenePos, List<Player> membersInOrder )
	{
		List<Player> members = new List<Player>();
		Player[] tmpMembers = new Player[3];
		team.CopyTo(tmpMembers);
		members.AddRange(tmpMembers);
		
		for( int idx = 0; idx != scenePos.Count; idx++ )
		{
			Transform trOffense = scenePos[idx];
			if( idx != 2 )
			{
				Player fitPositionPlayer = null;
				foreach(PositionType position in priorities_1)
				{
					foreach( Player player in members )
					{
						if( player == null )
							continue;

						if( player.m_position != position )
							continue;
						fitPositionPlayer = player;
						player.position = trOffense.position;
						player.rotation = trOffense.rotation;
						break;
					}
					if( fitPositionPlayer != null )
					{
						members.Remove(fitPositionPlayer);
						membersInOrder.Add(fitPositionPlayer);
						break;
					}
				}
			}
			else
			{
				if( members[0] != null )
				{
					members[0].position = trOffense.position;
					members[0].rotation = trOffense.rotation;
					membersInOrder.Add(members[0]);
				}
			}
		}
		team.Clear();
		foreach (Player member in membersInOrder)
			team.AddMember(member);
	}
	*/

	void PlaySound()
	{
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.ReadyGo);
	}

	virtual protected void _OnCounterDone()
	{
		_OnCounterDoneImp();
	}

	protected void _OnCounterDoneImp()
	{
		UBasketball ball = m_match.mCurScene.mBall;
		ball.initVel = IM.Vector3.up * IM.Number.half;
		ball.m_fTime = IM.Number.zero;
		ball.m_ballState = BallState.eRebound;
		
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = true;
			player.m_enableMovement = true;
			if (player.m_aiMgr != null && player.m_inputDispatcher == null )
				player.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;
		}
		
		counterDone = true;
		if (onCounterDone != null)
			onCounterDone();

        m_match.m_gameMatchCountStop = false;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		mTimer.Update(fDeltaTime);
		mTimerSound.Update(fDeltaTime);

		if( counterDone && m_match.mCurScene.mBall.m_ballState != BallState.eRebound )
			_OnEnableTipOff();
	}

	virtual protected void _OnEnableTipOff()
	{
		m_stateMachine.SetState(State.ePlaying);
	}

	override public void OnExit ()
	{
		counterDone = false;
		mTimer.stop = true;
		UBasketball ball = m_match.mCurScene.mBall;
        //if (ball.m_owner == null)
        //{
        //    if (m_match.EnableSwitchRole())
        //        m_match.m_ruler.SwitchRole();
        //}
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Rebound || command == Command.Rebound;
	}
}
