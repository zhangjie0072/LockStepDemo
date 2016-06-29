using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Pass : PlayerState_Skill
{
	static HedgingHelper passHedging = new HedgingHelper("Pass");

	public IM.Vector3 m_interceptedPos;
	public Player  m_interceptor = null;

	public PlayerState_Pass (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.ePass;

		m_mapAnimType[AnimType.B_TYPE_0] = "passBallLRChest";
		m_mapAnimType[AnimType.B_TYPE_1] = "passBallLRHead";

		m_animType = AnimType.B_TYPE_0;
	}
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		Player passTarget = m_player.m_passTarget;
		if( passTarget == null )
		{
			m_stateMachine.SetState(State.eStand);
			return;
		}
	
		m_interceptor = null;

        /*
        SkillInstance passBasicSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Pass)[0];
        if( m_curExecSkill.skill.id == passBasicSkill.skill.id )
        {
            float fDistance = GameUtils.HorizonalDistance(passTarget.position, m_player.position);
            if( fDistance < 10.0f )
                m_animType = AnimType.B_TYPE_0;
            else
                m_animType = AnimType.B_TYPE_1;
            m_curAction = m_mapAnimType[m_animType];
        }
        */

        m_player.FaceTo(passTarget.position);

			//Debug.Log("Send pass, player id: " + m_player.m_id + " to player: " + passTarget.m_id );
        //find the players out between passer and catchers
        Player defenseTarget = m_player.m_defenseTarget;
        if( defenseTarget != null )
        {
            List<Player> defensers = defenseTarget.m_team.GetSortedPlayersByDistance(m_player, true);
            foreach( Player defenser in defensers )
					Debug.Log("defenser: " + defenser.m_id);

            foreach( Player defenser in defensers )
            {
                SkillInstance interceptionSkill = null;

                List<SkillInstance> lstSkillInstances = defenser.m_skillSystem.GetSkillList(SkillType.ACTIVE);
                foreach( SkillInstance skillInst in lstSkillInstances )
                {
                    Command type = (Command)skillInst.skill.action_type;
                    if( type == Command.Interception )
                    {
                        interceptionSkill = skillInst;
                        break;
                    }
                }
                if( interceptionSkill == null )
                    continue;

                //传球者到接球者方向
                IM.Vector3 dirPasserToTarget = GameUtils.HorizonalNormalized(passTarget.position, m_player.position);

                //防守者在传球者身后
                IM.Vector3 dirPasserToDef = GameUtils.HorizonalNormalized(defenser.position, m_player.position);
                IM.Number proj = IM.Vector3.Dot(dirPasserToDef, dirPasserToTarget);
                if( proj < IM.Number.zero )
                    continue;
                //防守者在接球者身后
                IM.Vector3 dirTargetToDef = GameUtils.HorizonalNormalized(defenser.position,passTarget.position);
                proj = IM.Vector3.Dot(dirTargetToDef, -dirPasserToTarget);
                if( proj < IM.Number.zero )
                    continue;

                //计算防守者与传球线垂直距离
                IM.Number angle = IM.Vector3.AngleRad(dirPasserToTarget, dirPasserToDef);      //传球方向与防守者方向夹角
                IM.Number distDefenser = GameUtils.HorizonalDistance(defenser.position, m_player.position);     //防守者距离
                IM.Number fVDist = distDefenser * IM.Math.Sin(angle);   //垂直距离

                SkillSpec skillParam = defenser.GetSkillSpecialAttribute(SkillSpecParam.eInterception_dist, interceptionSkill);
                IM.Number fInterceptionRate = IM.Number.zero;
					Debug.Log("Player id: " + m_player.m_id + " distance to plane: " + fVDist);

                if( fVDist > skillParam.value )
                    continue;

                Dictionary<string, uint> passTargetData = m_player.m_finalAttrs;
                Dictionary<string, uint> defenserData = defenser.m_finalAttrs;

                uint skillPassValue = 0;
                m_player.m_skillSystem.HegdingToValue("addn_pass", ref skillPassValue);

                uint skillInterceptionValue = 0;
                defenser.m_skillSystem.HegdingToValue("addn_interception", ref skillInterceptionValue);

                fInterceptionRate = passHedging.Calc(new IM.Number((int)(defenserData["interception"] + skillInterceptionValue)),new IM.Number((int)(passTargetData["pass"] + skillPassValue)));

					Debug.Log("Interception rate: " + fInterceptionRate);

                if( IM.Random.value < fInterceptionRate )
                {
                    SkillSpec passSkillParam = defenser.GetSkillSpecialAttribute(SkillSpecParam.eInterception_get_ball_rate, interceptionSkill);

                    PlayerState_Interception intercetionState = defenser.m_StateMachine.GetState(State.eInterception) as PlayerState_Interception;
                    intercetionState.m_passer = m_player;
                    intercetionState.m_catcher = passTarget;
                    intercetionState.m_bGetBall = IM.Random.value < passSkillParam.value;

                    if( !intercetionState.CanIntercept(interceptionSkill, m_player, passTarget, out m_interceptedPos) )
                        continue;
                    defenser.m_toSkillInstance = interceptionSkill;
                    m_interceptor = defenser;

                    intercetionState.m_bSendMsg = true;
                    m_interceptor.m_StateMachine.SetState(intercetionState);
                    defenser.m_toSkillInstance = null;

                    break;
                }
            }
        }

		Dictionary<string, uint> skillAttr = m_player.GetSkillAttribute();
		passTarget.m_catchHelper.SetCatchMotionByPasser(m_player, skillAttr["pass"]);
		
		m_player.animMgr.Play(m_curAction, false);
		AudioClip clip = AudioManager.Instance.GetClip("Misc/Pass_01");
		if( clip != null )
			AudioManager.Instance.PlaySound(clip);
	}
	
	public void OnPass ()
	{
		//if( m_ball.m_interceptor != null )
		//	return;

		Player catchPlayer = m_player.m_passTarget;
		if( catchPlayer == null )
			return;

		if( !m_player.m_bWithBall )
			return;

		IM.Vector3 startPos = m_ball.position;
		m_player.DropBall(m_ball);
		m_ball.position = startPos;
		m_ball.OnPass(m_player, catchPlayer, m_interceptedPos, m_interceptor);
		m_ball.m_castedSkill = m_curExecSkill;

		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.ePass);
	}

	public override void OnExit ()
	{
		base.OnExit();
		m_player.m_passTarget = null;
	}
}