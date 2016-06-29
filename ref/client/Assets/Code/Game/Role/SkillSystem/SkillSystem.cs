using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;
using System.Linq;
using fogs.proto.config;

public class SkillInstance
{
	/*
		1	投篮
		2	上篮
		3	扣篮
		4	盖帽
		5	篮板
		6	突破
		7	传球
		8	抢断
		9	空切
		10	扑球
		11	背打
		12	卡位
		13	挡拆
		14	要球
		15	盯防
		*/
	public SkillAttr skill;
	public uint level = 0;
	public int matchedKeyIdx = 0;

	public uint curActionId
	{
		get
		{
			return curAction.id;
		}
		set
		{
			curAction = skill.actions.Find( (SkillAction action)=>{ return action.id == value; } );
		}
	}

	public SkillAction curAction
	{
		get
		{
			if( _curAction == null )
				return skill.actions[0];
			return _curAction;
		}
		set
		{
			_curAction = value;
		}
	}
	public SkillInput curInput = new SkillInput();

	public override bool Equals (object obj)
	{
		if( obj is SkillInstance )
		{
			SkillInstance target = obj as SkillInstance;
			return (target.curActionId == this.curActionId) && (target.matchedKeyIdx == this.matchedKeyIdx) && (target.skill.SameAction(skill));
		}
		else
			return base.Equals(obj);
	}

	private SkillAction _curAction;
}

public enum SkillSpecParam
{
	eShoot_antiBlock 	= 1,//投篮被盖帽几率改变
	eShoot_rate			= 2,		//投篮命中率

	eLayup_antiBlock	= 3,	//上篮被盖帽几率改变
	eLayup_rate			= 4,		//上篮命中率
	eLayup_dist_min		= 5,	//上篮起跳最小距离条件参数
	eLayup_dist_max		= 6,	//上篮起跳最大距离条件参数

	eDunk_antiBlock		= 7,	//扣篮被盖帽几率改变
	eDunk_rate			= 8,			//扣篮命中率
	eDunk_dist_min		= 9,		//扣篮起跳最小距离条件参数
	eDunk_dist_max		= 10,		//扣篮起跳最大距离条件参数

	ePass_speed			= 11,		//传球速度
	ePass_dist_min		= 12,		//可传球最小距离条件参数
	ePass_dist_max		= 13,		//可传球最大距离条件参数

	eInterception_dist	= 14,	//截球距离
	eInterception_way	= 15,	//截球线路（高、中、低）

	eRebound_dist		= 16,		//篮板距离
	eRebound_height		= 17,	//篮板高度

	eBlock_rate			= 18,		//盖帽几率改变
	eBlock_back_dist	= 19,	//背面盖帽距离
	eBlock_front_dist	= 20,	//正面盖帽距离
	eBlock_stun_rate	= 21,	//被成功盖帽者僵直概率
						
	eBlock_passBall_min	= 22,//盖帽传球，可传球最小距离条件参数
	eBlock_passBall_max	= 23,//盖帽传球，可传球最大距离条件参数

	eBodyThrowCatch_dist	= 24,//扑球有效距离
	eBodyThrowCatch_rate	= 25,//扑球成功率
						
	eSteal_rate			= 26,		//抢断成功率
	eSteal_dist			= 27,		//抢断有效距离

	eSteal_get_ball_rate	= 28,//抢断成功得球率

	eCrossOver_crossed_falldown_rate	= 29,//被晃倒几率
	eCrossOver_crosser_falldown_rate	= 30,//撞到目标几率

	ePickAndRoll_range	= 31,//挡拆范围

	eDefendCrossOver_get_ball_rate	= 34,//抢断成功后得球几率
	eDefendCrossOver_rate = 35,//抢断成功率改变

	eInterception_get_ball_rate	= 36,	//截球得球几率
}

public enum SkillSpecParamOp
{
	eAdd,
	eMulti,
}

public class SkillSystem
{
	enum OffenseType
	{
		eNone 	= 0,
		eShot 	= 1,
		eLayup 	= 2,
		eDunk 	= 3
	}

	public IM.Number    m_startRushStamina {get; private set;}
	public IM.Number	m_rushStamina {get; private set;}

	private List<SkillInstance> m_basicMatchedSkills;
	private	Player	m_player;

	private HashSet<Command> disabledCommand = new HashSet<Command>();

	private Dictionary<SkillEffect, List<GameObject>>	m_skillEffects = new Dictionary<SkillEffect, List<GameObject>>();
	private List<KeyValuePair<GameObject, GameObject>> 	m_mapBoneEffect = new List<KeyValuePair<GameObject, GameObject>>();

	private IM.Number	m_time;
	private IM.Number	m_actionFramerate;

	private List<SkillInstance>		m_cachedSkillList = new List<SkillInstance>();

	public SkillSystem(Player player)
	{
		if(player == null)
			Debug.LogError("no player in skill system");

		m_player = player;

		m_basicMatchedSkills = new List<SkillInstance>();
		m_cachedSkillList = _GetSkillList(SkillType.ACTIVE, m_player.m_roleInfo);

		foreach( SkillInstance skillInst in m_cachedSkillList )
		{
			if( skillInst.skill.subtype == SkillSubType.NORMAL )
				continue;
			m_basicMatchedSkills.Add(skillInst);
		}

		m_startRushStamina = GameSystem.Instance.CommonConfig.GetNumber("gStart_Sprint");
		m_rushStamina = GameSystem.Instance.CommonConfig.GetNumber("gSprint");

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		foreach( SkillInstance skillInst in m_cachedSkillList )
		{
			foreach( SkillAction action in skillInst.skill.actions )
			{
				foreach( uint skillEffect in action.skillEffects )
				{
					SkillEffect sk = null;
					if( !GameSystem.Instance.SkillConfig.skillEffectItems.TryGetValue(skillEffect, out sk) )
					{
						Debug.LogError("Can not find skill effect: " + skillEffect + " in skill: " + action.id);
						continue;
					}
					GameObject goEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/" + sk.effectRes);
					if( !match.m_preloadCache.ContainsKey(goEffect) )
					{
						GameObject tmpObj = GameObject.Instantiate(goEffect) as GameObject;
						match.m_preloadCache.Add(goEffect, tmpObj);
						tmpObj.transform.position = new Vector3( 10000.0f, 10000.0f, 10000.0f );
					}
				}
				//Object goCamera = ResourceLoadManager.Instance.GetResources("Prefab/Camera/" + action.camera_animation);
			}
		}
	}

	public void ResetSkillEffects(SkillInstance skill, string strAction)
	{
		if( skill == null )
			return;

		foreach( List<GameObject> goEffects in m_skillEffects.Values )
			goEffects.Clear();
		m_skillEffects.Clear();
		m_mapBoneEffect.Clear();

		foreach( uint skillEffect in skill.curAction.skillEffects )
		{
			SkillEffect sk = null;
			if( !GameSystem.Instance.SkillConfig.skillEffectItems.TryGetValue(skillEffect, out sk) )
			{
				Debug.LogError("Can not find skill effect: " + skillEffect + " in skill: " + skill.curAction.id);
				continue;
			}
			Object resEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/" + sk.effectRes);
			if( resEffect == null )
				continue;
			sk.spawned = false;
			if( sk.tagPointOwner == 1 )
			{
				List<GameObject> goEffects = new List<GameObject>();
				foreach( string tagPoint in sk.tagPoints )
				{
					GameObject goEffect = GameObject.Instantiate(resEffect) as GameObject;
					GameObject goTagPoint = GameUtils.FindChildRecursive(m_player.gameObject.transform, tagPoint).gameObject;

					m_mapBoneEffect.Add( new KeyValuePair<GameObject, GameObject>(goEffect, goTagPoint) );
					goEffect.SetActive(false);
					goEffect.transform.localPosition = Vector3.zero;
					goEffect.transform.localRotation = Quaternion.identity;
					if( goEffect.GetComponent<ParticleSystem>() != null )
					{
						goEffect.GetComponent<ParticleSystem>().loop = false;
						//goEffect.particleSystem.playOnAwake = true;
					}
					goEffects.Add(goEffect);
				}
				m_skillEffects.Add(sk, goEffects);
			}
			else if( sk.tagPointOwner == 2 )
			{
				GameMatch match = GameSystem.Instance.mClient.mCurMatch;
				UBasket basket = match.mCurScene.mBasket;
				if( basket != null )
					basket.SetEffect((BasketState)(uint.Parse(sk.tagPoints[0])), resEffect);
			}
			else if( sk.tagPointOwner == 3 )
			{
				if( m_player.m_ball != null )
				{
					GameObject goEffect = GameObject.Instantiate(resEffect) as GameObject;
					m_player.m_ball.SetEffect((BallState)(uint.Parse(sk.tagPoints[0])), goEffect);
					goEffect.transform.parent = m_player.m_ball.transform;

					goEffect.transform.localPosition = Vector3.zero;
					//goEffect.transform.localRotation = Quaternion.identity;
					if( goEffect.GetComponent<ParticleSystem>() != null )
					{
						goEffect.GetComponent<ParticleSystem>().loop = (sk.playBackType == 1);
						//goEffect.particleSystem.playOnAwake = true;
					}
				}
			}
		}
		m_actionFramerate = m_player.animMgr.GetFrameRate(strAction);

		m_time = IM.Number.zero;
	}

	public void ClearEffects()
	{
		foreach( List<GameObject> goEffects in m_skillEffects.Values )
		{
			foreach( GameObject goEffect in goEffects )
			{
				goEffect.SetActive(false);
				GameObject.Destroy(goEffect);
			}
			goEffects.Clear();
		}
		m_skillEffects.Clear();
	}

    //渲染层
	public void LateUpdate()
	{
		foreach( KeyValuePair<GameObject, GameObject> item in m_mapBoneEffect )
		{
			GameObject goEffect = item.Key;
			GameObject goBone = item.Value;
			if( goEffect == null )
			{
				m_mapBoneEffect.Remove(item);
				return;
			}
			goEffect.transform.position = goBone.transform.position;
		}
	}

	public void Update(IM.Number fDeltaTime)
	{
		foreach( KeyValuePair<SkillEffect, List<GameObject>> skillEffects in m_skillEffects)
		{
			SkillEffect se = skillEffects.Key;
			if( m_time > (new IM.Number((int)se.startFrame) / m_actionFramerate) )
			{
				foreach( GameObject goEffects in skillEffects.Value )
				{
					if( se.moveWithTagPoint == 1 )
					{
						if( se.spawned )
							continue;
						GameObject goEffect = GameObject.Instantiate(goEffects) as GameObject;
						goEffect.transform.localScale = Vector3.one;
						goEffect.transform.rotation = Quaternion.identity;
						goEffect.transform.position = goEffects.transform.position;

						goEffect.AddComponent<UEffectSelfDestroy>();

						goEffect.SetActive(true);
						se.spawned = true;
					}
					else
						goEffects.SetActive(true);
				}
			}
			if( m_time > (new IM.Number((int)se.endFrame) / m_actionFramerate) )
			{
				foreach( GameObject goEffects in skillEffects.Value )
					goEffects.SetActive(false);
			}
		}


		m_time += fDeltaTime;
	}

	public List<SkillInstance> GetBasicSkillsByCommand( Command cmd )
	{
		List<SkillInstance> skills = new List<SkillInstance>();
		foreach( SkillInstance skillInst in m_basicMatchedSkills )
		{
			if( skillInst.skill.action_type == (uint)cmd )
				skills.Add(skillInst);
		}
		return skills;
	}

	OffenseType _CalcOffenseType(Player player, List<OffenseType> offenses)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasket	basket = match.mCurScene.mBasket;
		if( basket == null )
			return OffenseType.eNone;

        Dictionary<string, uint> data = player.m_finalAttrs;
		if( data == null )
		{
			Debug.LogError("Can not build player: " + player.m_name + " ,can not fight state by id: " + player.m_id );
			return OffenseType.eNone;
		}

		PlayGround playGround = match.mCurScene.mGround;

        Debugger.Instance.m_steamer.message = "Attr of offense type: ";
		Dictionary<OffenseType, IM.Number>	lstOffenseRates = new Dictionary<OffenseType, IM.Number>();
		foreach( OffenseType type in offenses )
		{
			Area area = Area.eInvalid;
			IM.Number offenseRate = IM.Number.zero;

			if(type == OffenseType.eLayup)
				area = playGround.GetLayupArea(player);
			else if(type == OffenseType.eDunk)
				area = playGround.GetDunkArea(player);
			else if(type == OffenseType.eShot)
				area = playGround.GetArea(player);

			if(area == Area.eNear)
			{
				if( type == OffenseType.eLayup )
					offenseRate = new IM.Number((int)data["layup_near"]);
				else if( type == OffenseType.eDunk )
					offenseRate = new IM.Number((int)data["dunk_near"]);
				else if( type == OffenseType.eShot )
					offenseRate = new IM.Number((int)data["shoot_near"]);
			}
			else if(area == Area.eMiddle)
			{
				if( type == OffenseType.eShot )
					offenseRate = new IM.Number((int)data["shoot_middle"]);
				else if( type == OffenseType.eDunk )
					offenseRate = new IM.Number((int)data["dunk_middle"]);
				else if( type == OffenseType.eLayup )
                    offenseRate = new IM.Number((int)data["layup_middle"]);
			}
			else if(area == Area.eFar)
			{
				if( type == OffenseType.eShot )
					offenseRate = new IM.Number((int)data["shoot_far"]);
			}
			lstOffenseRates.Add(type, offenseRate);
            Debugger.Instance.m_steamer.message += type + " " + offenseRate + " ";
		}

		IM.Number totalValue = IM.Number.zero;
		foreach( KeyValuePair<OffenseType, IM.Number> keyvalue in lstOffenseRates )
			totalValue += keyvalue.Value;

		IM.Number rateAdjust = IM.Number.zero;
		if (lstOffenseRates.Count > 1 && lstOffenseRates.ContainsKey(OffenseType.eDunk))
			rateAdjust = GameSystem.Instance.DunkRateConfig.GetRate(m_player.m_position);
		Debugger.Instance.m_steamer.message += "\nDunk adjustment: " + rateAdjust;

		List<OffenseType> keys = new List<OffenseType>(lstOffenseRates.Keys);
		foreach (OffenseType key in keys)
		{
			IM.Number rate = lstOffenseRates[key] / totalValue;
			if (lstOffenseRates.Count > 1)
			{
				if (key == OffenseType.eDunk)
					rate += rateAdjust;
				else
					rate -= rateAdjust / (lstOffenseRates.Count - 1);
			}

			lstOffenseRates[key] = rate;
			Debugger.Instance.m_steamer.message += " " + key + ": " + rate;
		}

		IM.Number value = IM.Random.value;
		Debugger.Instance.m_steamer.message += " Random: " + value + "\n";

		OffenseType validType = OffenseType.eShot;
        IM.Number curValue = IM.Number.zero;
		foreach( KeyValuePair<OffenseType, IM.Number> keyvalue in lstOffenseRates )
		{
			IM.Number rate = keyvalue.Value;

			curValue += rate;

			if( value > curValue )
				continue;
			validType = keyvalue.Key;
			break;
		}
		return validType;
	}

	public void DisableCommand(Command command)
	{
		disabledCommand.Add(command);
	}

	public void CancelDisableCommand(Command command)
	{
		disabledCommand.Remove(command);
	}

	bool _MatchActionCondition(SkillInstance skillInstance)
	{
		if (disabledCommand.Contains((Command)skillInstance.skill.action_type))
			return false;
		if( skillInstance.skill.condition.Count == 0 )
			return true;
		//with ball or not
		foreach( int con in skillInstance.skill.condition )
		{
			if( con == 1 && !m_player.m_bWithBall )
				return false;
			if( con == 2 && m_player.m_bWithBall )
				return false;
		}
		return true;
	}

	bool _MatchSkillSpecParam(SkillInstance skillInstance)
	{

		Command cmdType = (Command)skillInstance.skill.action_type;
		if( cmdType == Command.Pass )
		{
			if( m_player.m_passTarget == null )
				return false;

			SkillSpec passSkillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.ePass_dist_min, skillInstance);
			IM.Number fMinDistance = passSkillSpec.value;
			passSkillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.ePass_dist_max, skillInstance);
			IM.Number fMaxDistance = passSkillSpec.value;
			IM.Number fDistance = GameUtils.HorizonalDistance(m_player.position, m_player.m_passTarget.position);

			if( fDistance > fMaxDistance || fDistance < fMinDistance )
				return false;
		}
		if( cmdType == Command.Dunk || cmdType == Command.Layup )
		{
			if( m_player.m_defenseTarget != null )
			{
				Player nearestValidDefender = null;
				IM.Number fNearestDist = IM.Number.max;
				foreach(Player pl in m_player.m_defenseTarget.m_team)
				{
					if( m_player.m_AOD.GetStateByPos(pl.position) != AOD.Zone.eInvalid )
					{
						IM.Number fDist = GameUtils.HorizonalDistance(m_player.position, pl.position);
						if( fDist > fNearestDist )
							continue;
						fNearestDist = fDist;
						nearestValidDefender = pl;
					}
				}
				if( nearestValidDefender != null )
				{
					IM.Number fDist = GameUtils.HorizonalDistance(m_player.position, nearestValidDefender.position);
					if( fDist < skillInstance.skill.attrange )
						return false;
				}
			}
		}

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		if( cmdType == Command.Block )
		{
			Player attacker = ball.m_actor;
			if( attacker == null )
				attacker = ball.m_owner;
			if( attacker == null )
				return false;

			IM.Number fDistance = GameUtils.HorizonalDistance(attacker.position, m_player.position);
			IM.Vector3 dirAttackerToPlayer = GameUtils.HorizonalNormalized(m_player.position, attacker.position); 
			IM.Number ret = IM.Vector3.Dot(attacker.forward, dirAttackerToPlayer);
			if( ret < IM.Number.zero)
			{
				SkillSpec blockBackDist = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBlock_back_dist, skillInstance);
				if( fDistance > blockBackDist.value )
					return false;
			}
			else
			{
				SkillSpec blockFrontDist = m_player.GetSkillSpecialAttribute(SkillSpecParam.eBlock_front_dist, skillInstance);
				if( fDistance > blockFrontDist.value )
					return false;
			}
		}
		if( cmdType == Command.Rebound )
		{
			IM.Number fDistance = GameUtils.HorizonalDistance(ball.position, m_player.position);
			IM.Number fHeight = ball.position.y;

			SkillSpec heightSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eRebound_height, skillInstance);
			SkillSpec distSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eRebound_dist, skillInstance);
			if( fHeight > heightSpec.value )
				return false;
			if( fDistance > distSpec.value )
				return false;
		}
		return true;
	}

	bool _MatchAction(Command curCommand, SkillInstance skillInstance, bool bIgnoreDir)
	{
		List<SkillAction> matchedActions = new List<SkillAction>();

		SkillAttr skill = skillInstance.skill;
		int cnt = skill.actions.Count;
		for( int idx = 0; idx != cnt; idx++ )
		{
			SkillAction action = skill.actions[idx];
			SkillInput input = action.inputs[0];
			if( input.cmd != curCommand )
				continue;
			EDirection dir = m_player.m_inputDispatcher.GetMoveDirection(input.inputType);
			if( input.moveDir != dir || (bIgnoreDir && input.moveDir != EDirection.eNone) ) 
				continue;
			matchedActions.Add(action);
		}

		if( matchedActions.Count == 0 )
			return false;

		int iSelActionIdx = IM.Random.Range( 0, matchedActions.Count );
		SkillAction finalAction = matchedActions[iSelActionIdx];

		PlayerState	curState = m_player.m_StateMachine.m_curState;
		PlayerState_Skill skillState = curState as PlayerState_Skill;
		if( finalAction.interrupts.Count > 0 )
		{
			foreach( SkillInterrupt interrupt in finalAction.interrupts )
			{
				if( !curState.m_lstActionId.Contains( interrupt.id ) )
					continue;
				Debug.Log("Interrupt action: " + skillInstance.skill.action_type + " , action id: " + finalAction.id );
				//matched
				skillInstance.curAction = finalAction;
				skillInstance.matchedKeyIdx = iSelActionIdx;
				return true;
			}
			return false;
		}
		else if( skillState == null )
		{
			skillInstance.curAction = finalAction;
			skillInstance.matchedKeyIdx = iSelActionIdx;
			return true;
		}
		else if( skillState != null && skillState.m_bPersistent )
		{
			skillInstance.curAction = finalAction;
			skillInstance.matchedKeyIdx = iSelActionIdx;
			return true;
		}

		return false;
	}

	bool _MatchArea(GameMatch match, SkillInstance skillInstance)
	{
		SkillAttr skill = skillInstance.skill;
		if( skill.area.Count == 0 )
			return true;
		PlayGround playground = match.mCurScene.mGround;
        IM.Number fPlayerToNet = GameUtils.HorizonalDistance(match.mCurScene.mBasket.m_vShootTarget, m_player.position);
		if( skill.action_type == 2 )			//layup
		{
			Area eArea = playground.GetLayupArea(m_player);
			if( !skill.area.Contains( (uint)eArea ) )
				return false;
			SkillSpec spc = m_player.GetSkillSpecialAttribute(SkillSpecParam.eLayup_dist_min, skillInstance);
			if( fPlayerToNet < spc.value )
				return false;
			spc = m_player.GetSkillSpecialAttribute(SkillSpecParam.eLayup_dist_max, skillInstance);
			if( fPlayerToNet > spc.value )
				return false;
		}
		else if( skill.action_type == 3 )		//dunk
		{
			Area eArea = playground.GetDunkArea(m_player);
			if( !skill.area.Contains( (uint)eArea ) )
				return false;
			SkillSpec spc = m_player.GetSkillSpecialAttribute(SkillSpecParam.eDunk_dist_min, skillInstance);
			if( fPlayerToNet < spc.value )
				return false;
			spc = m_player.GetSkillSpecialAttribute(SkillSpecParam.eDunk_dist_max, skillInstance);
			if( fPlayerToNet > spc.value )
				return false;
		}
		else
		{
			Area eArea = playground.GetArea(m_player);
			if( !skill.area.Contains( (uint)eArea ) )
				return false;
		}
		return true;
	}

	bool _MatchStamina(SkillInstance skillInstance)
	{
		//ignore skill stamina
		if( (Command)skillInstance.skill.action_type == Command.PickAndRoll )
		{
			if( m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.ePickAndRoll )
				return true;
            return m_player.m_stamina.m_curStamina >= skillInstance.skill.levels[skillInstance.level].stama * IM.Number.two;
		}
		return m_player.m_stamina.m_curStamina >= new IM.Number((int)skillInstance.skill.levels[skillInstance.level].stama) ;
	}

	static public SkillInstance MatchSkillByWeight(List<SkillInstance> skills)
	{
		//Debug.Log("-----------skill weight begin-------------");

		List<KeyValuePair<IM.Number,SkillInstance> > weightedSkills = new List<KeyValuePair<IM.Number,SkillInstance> >();
		uint totalWeight = 0;
		foreach( SkillInstance skillInstance in skills )
        {
			totalWeight += skillInstance.skill.levels[skillInstance.level].weight;
        }

		IM.Number totalOdds = IM.Number.zero;
		foreach( SkillInstance skillInstance in skills )
		{
			IM.Number weight = new IM.Number((int)(skillInstance.skill.levels[skillInstance.level].weight / totalWeight));
			totalOdds += weight;
			KeyValuePair<IM.Number,SkillInstance> kv = new KeyValuePair<IM.Number, SkillInstance>(weight, skillInstance);
			weightedSkills.Add(kv);
		}
		
		if( IM.Number.Approximately(totalOdds, IM.Number.zero) )
			return null;
		
		weightedSkills.Sort( delegate(KeyValuePair<IM.Number, SkillInstance> x, KeyValuePair<IM.Number, SkillInstance> y) {
			return x.Key.CompareTo(y.Key);
		});
		//foreach( KeyValuePair<float,SkillInstance> item in weightedSkills )
		//	Debug.Log("skill: " + item.Value.curAction.action_id + ", weight: " + item.Key);

        IM.Number finalOdds = IM.Random.value;
		//Debug.Log("final Odds: " + finalOdds);

		IM.Number odds = IM.Number.zero;
		foreach( KeyValuePair<IM.Number,SkillInstance> item in weightedSkills )
		{
			odds += item.Key / totalOdds;
			if( finalOdds < odds )
				return item.Value;
		}

		return null;
	}

	public List<SkillInstance> GetSkillList(SkillType type, Command cmd = Command.None)
	{
		if( cmd != Command.None )
			return m_cachedSkillList.FindAll( (SkillInstance skillInst)=>{ return (Command)skillInst.skill.action_type == cmd;} );
		return m_cachedSkillList;
	}

	List<SkillInstance> _GetSkillList(SkillType type, RoleInfo roleInfo)
	{
		List<SkillInstance> skill_list = new List<SkillInstance>();
		foreach (SkillAttr skill in GameSystem.Instance.SkillConfig.basic_skills)
		{
			if (skill.type == type)
			{
				SkillInstance inst = new SkillInstance();
				inst.skill = skill;
				inst.level = 1;
				skill_list.Add(inst);
			}
		}

        if (roleInfo != null) 
        {
            foreach (SkillSlotProto skillSlot in roleInfo.skill_slot_info)
            {
                if (skillSlot.skill_uuid == 0)
                    continue;

                uint skillID = skillSlot.skill_id;
                uint skillLevel = 1;

                 Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_SKILL, skillSlot.skill_uuid);
                if( goods != null )
                 {
                     skillLevel = goods.GetLevel();
                 }

                SkillAttr attr = GameSystem.Instance.SkillConfig.GetSkill(skillID);
                if (attr.type == type)
                {
                    SkillInstance inst = new SkillInstance();
                    inst.skill = attr;
                    inst.level = skillLevel;
                    skill_list.Add(inst);
                }
            }
        }
		
		if( !m_player.m_bIsNPC )
		{
			RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData( m_player.m_roleInfo.id );
			MergeSkillList(type, skill_list, data.training_skill_all);
		}
		else
		{
			NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(m_player.m_id);
			MergeSkillList(type, skill_list, config.skills);
		}
		return skill_list;
	}

	static void MergeSkillList(SkillType type, List<SkillInstance> to_skill_list, List<uint> from_skill_list)
	{
		for (int i = 0; i < from_skill_list.Count; ++i)
		{
			SkillAttr skill = GameSystem.Instance.SkillConfig.GetSkill(from_skill_list[i]);
			if (skill != null && skill.type == type)
			{
				if( skill.subtype == SkillSubType.SPEC )
				{
					List<SkillAttr> targetSkills = GameSystem.Instance.SkillConfig.basic_skills.FindAll(
						inSkill => inSkill.area.SequenceEqual(skill.area) && (inSkill.action_type == skill.action_type));
					SkillAttr targetSkill = targetSkills.Find(inSkill => inSkill.SameAction(skill));
					if (targetSkill != null)
					{
						SkillInstance toRemoveInst = to_skill_list.Find(inSkillInst => inSkillInst.skill == targetSkill);
						if (to_skill_list.Remove(toRemoveInst))
							Debug.Log("special skill: " + skill.id + " replace skill: " + toRemoveInst.skill.id);
					}
					else
						Debug.Log("There is no basic skill to be replaced by skill: " + skill.id);
				}
				SkillInstance inst = new SkillInstance();
				inst.skill = skill;
				inst.level = 1;
				to_skill_list.Add(inst);
			}
		}
	}

    /**比赛中取有效技能 根据当前command*/
	public SkillInstance GetValidSkillInMatch(Command curCommand, bool isAI = false, System.Predicate<SkillInstance> filter = null)
	{
		if( curCommand == Command.None )
			return null;

		PlayerState curPlayerState = m_player.m_StateMachine.m_curState;
		if ( curPlayerState != null && !curPlayerState.IsCommandValid(curCommand))
			return null;

		List<SkillInstance> skills = m_cachedSkillList;
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;

		PlayerState	curState = m_player.m_StateMachine.m_curState;
		bool bIsSkillState = curState is PlayerState_Skill;

		List<SkillInstance> matchedSkills = new List<SkillInstance>();
		List<SkillInstance> toMatchSkills = new List<SkillInstance>();
		foreach( SkillInstance skillInstance in skills )
		{
			if (isAI && (Command)skillInstance.skill.action_type != curCommand)
				continue;
			if( !_MatchArea(curMatch,skillInstance) )
				continue;
			if( !_MatchActionCondition(skillInstance) )
				continue;
			if( !_MatchSkillSpecParam(skillInstance) )
				continue;
			//has power & has vigour
			if( !_MatchStamina(skillInstance) )
			{
				if (curMatch.mainRole == m_player)
				{
					curMatch.ShowTips((Vector3)m_player.position + Vector3.up, CommonFunction.GetConstString("MATCH_TIPS_NOT_ENOUGH_STAMINA"), GlobalConst.MATCH_TIP_COLOR_RED);
					if (isAI)
						Debug.Log(string.Format("SkillSystem, no enough stamina for skill: {0} {1}", skillInstance.skill.id, skillInstance.skill.name));
				}
				continue;
			}
			if (isAI && filter != null && !filter(skillInstance))
				continue;
			toMatchSkills.Add(skillInstance);
			//precise matching
			if(!isAI && !_MatchAction(curCommand, skillInstance, false) )
				continue;
			matchedSkills.Add(skillInstance);
		}

		if( !isAI && matchedSkills.Count == 0 )
		{
			foreach( SkillInstance skillInstance in toMatchSkills )
			{
				//unprecise matching
				if(!_MatchAction(curCommand, skillInstance, true) )
					continue;
				matchedSkills.Add(skillInstance);
			}
		}

		//filter out shoot dunk layup and offense type 
		if( !isAI && curCommand == Command.Shoot )
		{
			List<OffenseType> offenses = new List<OffenseType>();
			foreach( SkillInstance skillInstance in matchedSkills )
			{
				if( skillInstance.skill.action_type == 1 && !offenses.Contains(OffenseType.eShot) )
					offenses.Add(OffenseType.eShot);
				else if( skillInstance.skill.action_type == 2 && !offenses.Contains(OffenseType.eLayup) )
					offenses.Add(OffenseType.eLayup);
                else if (skillInstance.skill.action_type == 3 && !offenses.Contains(OffenseType.eDunk))
                    offenses.Add(OffenseType.eDunk);
			}
			if( offenses.Count > 0 )
			{
				OffenseType type = _CalcOffenseType(m_player, offenses);
				if( type != OffenseType.eNone )
				{
					//Debug.Log("OffenseType : " +  type );
					for( int i = matchedSkills.Count - 1; i >= 0; i-- )
					{
						SkillInstance skillInstance = matchedSkills[i];
						if( skillInstance.skill.action_type != (uint)type )
						{
							//Debug.Log("Remove action_type : " + skillInstance.skill.action_type + " Type value: " + (uint)skillInstance.skill.action_type);
							matchedSkills.Remove(skillInstance);
						}
					}
				}
			}
		}

		/*
		if( matchedSkills.Count != 0 )
		{
			Debug.Log("====matched skill===");
			foreach( SkillInstance instance in matchedSkills )
				Debug.Log("matched skill: " + instance.curAction.action_id);
			Debug.Log("====================");
		}
		*/

		//choose highest weight. 
		SkillInstance finalSkill = null;
		finalSkill = MatchSkillByWeight(matchedSkills);
		//if( finalSkill != null )
		//	Debug.Log("final skill: " + finalSkill.curAction.action_id);

		//choose a default skill
		if( finalSkill == null && !bIsSkillState && curCommand != Command.CutIn)	//TODO: 临时针对空切特殊处理，以后修改
		{
			foreach(SkillInstance skillInstance in m_basicMatchedSkills)
			{
				if( (Command)skillInstance.skill.action_type != curCommand )
					continue;
				if( !_MatchArea(curMatch, skillInstance) )
					continue;
				if( !_MatchActionCondition(skillInstance) )
					continue;
				//has power & has vigour
				if( !_MatchStamina(skillInstance) )
					continue;

				finalSkill = skillInstance;
				//Debug.Log("Get basic skill: ");
				break;
			}
		}
		return finalSkill;
	}
	public SkillInstance GetSkillById(int skillId)
	{
		List<SkillInstance> skills = m_cachedSkillList;
		foreach( SkillInstance skillInstance in skills )
		{
			if(skillId == skillInstance.skill.id)
				return skillInstance;
		}
		return null;
	}

	public string ParseAction(string actionId, int keyIdx, Command actionType)
	{
		string[] strActions = actionId.Split('/');
		string lHandActionId = "" , rHandActionId = "";
		
		string resultAction = "";
		if( strActions.Length > 1 )
			resultAction = strActions[keyIdx];
		else if( strActions.Length == 1 )
			resultAction = actionId;
		else
			Debug.LogError("Invalid action input.");
		
		string[] hands = resultAction.Split('&');
		if( hands.Length > 1 )
		{
			foreach( string strHand in hands )
			{
				if( strHand.StartsWith("L:") )
					lHandActionId = strHand.Substring(2);
				else if( strHand.StartsWith("R:") )
					rHandActionId = strHand.Substring(2);
			}
			/*
			if( actionType == Command.Interception )
			{
				Vector3 dirPasserToCatcher = GameUtils.HorizonalNormalized(m_catcher.position, m_passer.position);
				Vector3 dirPasserToInterceptor = GameUtils.HorizonalNormalized(m_player.position, m_passer.position);
				float fDir = Vector3.Cross(dirPasserToCatcher, dirPasserToInterceptor).y;
				return fDir < 0.0f ? lHandActionId : rHandActionId;
			}
			else
			{
			*/
				string strAction = lHandActionId;
				if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
					strAction = rHandActionId;
				return strAction;
			//}
		}
		else
			return resultAction;
	}

	public void GetAttrValueByName(string strAttrName, ref uint attrValue)
	{
		Dictionary<string, uint> skillAttr = m_player.GetSkillAttribute();
		if( skillAttr == null )
		{
			//Debug.LogError("No skillAttr for player: " + m_player.m_id);
			return;
		}
		
		uint skillAttrValue = 0;
		if( !skillAttr.TryGetValue(strAttrName, out skillAttrValue) )
		{
			//Debug.LogError("Unable to find skill attr by name: " + strAttrName);
			return;
		}
		attrValue = skillAttrValue;
	}

	public void HegdingToValue(string strAttrName, ref uint attrValue)
	{
		GetAttrValueByName(strAttrName, ref attrValue);

		AttrNameData attrItem = GameSystem.Instance.AttrNameConfigData.AttrNameDatas.Find( (AttrNameData item)=>{ return item.symbol == strAttrName; } );
		if( attrItem == null )
			return;

		Debug.Log("Player id: " + m_player.m_id + " attrName: " + strAttrName + " , attrValue0: " + attrValue);

		if( attrItem.type != AttributeType.HEDGINGLEVEL )
			return;
		AttrData attrData = m_player.m_attrData;
		HedgingConfig.hedgeLevelData data = GameSystem.Instance.HedgingConfig.GetHedgeLevelFactor(attrItem.id);
		if (data != null)
		{
			IM.PrecNumber factor = data.factor;
			uint attrID = data.oppositeID;
			string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(attrID);
			if (attrData.attrs.ContainsKey(symbol))
			{
				Debug.Log("Attr data: " + attrData.attrs[symbol] + " factor: "  + factor);
				attrValue = (uint)(attrData.attrs[symbol] * (1 + attrValue * factor)).roundToInt;
			}
		}

		Debug.Log("Player id: " + m_player.m_id + " attrName: " + strAttrName + " , attrValue1: " + attrValue);
	}
}