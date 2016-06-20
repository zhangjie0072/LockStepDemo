using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;
using fogs.proto.config;

public struct LostBallContext
{
	public IM.Vector3 vInitPos;
	public IM.Vector3 vInitVel;
}

public class Player :MatchStateMachine.Listener, IM.IRootMotionTarget
{
    
	public PlayerStatistics mStatistics;
	public PlayerMovement[] mMovements;
	public interface PlayerBuildListener
	{
		void OnPlayerComplete(Player player);
	}
	
	public enum HandWithBall
	{
		eNone,
		eLeft,
		eRight
	}

	public GenderType			m_gender;

	public Team					m_team{ get; set; }
	
    //玩家基础属性
	public uint m_id { get { return m_roleInfo.id; } }
	public string				m_name { get; set; }
	public int 					m_shapeID { get; private set; }

    public string 				m_teamName;
    public PositionType	 		m_position;
	private PositionType _matchPosition = PositionType.PT_NONE;
	public PositionType m_matchPosition
	{
		get
		{
			if (_matchPosition == PositionType.PT_NONE)
				return m_position;
			else
				return _matchPosition;
		}
		set
		{
			_matchPosition = value;
		}
	}
	public RoleInfo 			m_roleInfo;

	public uint					m_roomPosId{ get{ return m_roleInfo.index; } }

	private bool				_enableAction;	
	public	bool				m_enableAction
	{
		get
		{
			return	_enableAction;
		}
		
		set
		{
			_enableAction = value;
			if( m_pickupDetector != null )
				m_pickupDetector.m_enable = value;
		}
	}

	public	MoveType			m_moveType = MoveType.eMT_Stand;

	public	bool				m_enableMovement;

	private int				_dir;
	public	int				m_dir
	{
		get{return _dir;}
		set{
			_dir = value;
			if( _dir == -1 )
				moveDirection = IM.Vector3.zero;
			else
				moveDirection = IM.Quaternion.Euler(IM.Number.zero, new IM.Number(value) * GlobalConst.ROTATE_ANGLE_SEC, IM.Number.zero) * IM.Vector3.forward;
		}
	}

    private IM.Number velocity;     //当前移动速度
    private IM.Number turningSpeed; //当前转向速度

    public bool m_bIsNPC { get { return m_roleInfo.id > 10000; } }

    public IM.Vector3 moveDirection
    {
        get { return moveCtrl.moveDirection; }
        set { moveCtrl.moveDirection = value; }
    }

    public IM.Vector3 position
    {
        get { return moveCtrl.position; }
        set
        {
            moveCtrl.position = value;
            //同时设置显示层（非平滑移动）
            transform.position = (Vector3)value;    
        }
    }

    public IM.Vector3 forward
    {
        get { return moveCtrl.forward; }
        set
        {
            moveCtrl.forward = value;
            //同时设置显示层（非平滑转向）
            transform.rotation = (Quaternion)rotation;
        }
    }

    public IM.Vector3 up
    {
        get { return moveCtrl.up; }
    }

    public IM.Vector3 right
    {
        get { return moveCtrl.right; }
    }

    public IM.Quaternion rotation
    {
        get { return moveCtrl.rotation; }
        set
        {
            moveCtrl.rotation = value;
            //同时设置显示层（非平滑转向）
            transform.rotation = (Quaternion)value;
        }
    }

    
    public IM.Vector3 scale
    {
        get { return moveCtrl.scale; }
        set {
            moveCtrl.scale = value;
            gameObject.transform.localScale = (Vector3)value;
        }
    }

	public	IM.Number				m_lastReceiveShootTime;

	public	GameMatch.Config.TeamMember		m_config;

	public bool m_defTargetSwitched;
	public	Player				m_defenseTarget;

    public 	AttrData 			m_attrData;
	private Dictionary<string, uint> _finalAttrs = new Dictionary<string, uint>();
	public Dictionary<string, uint> m_finalAttrs { get { return _finalAttrs.Count == 0 ? m_attrData.attrs : _finalAttrs; } }
	public IM.Number m_fightingCapacity { get { return m_attrData.fightingCapacity; } }

	//TODO:skill involved
	public IM.Number m_fReboundDist { get { return new IM.Number(3); } }
	
	//config
	public	bool				m_bForceShoot = false;

	public	bool				m_bToDirectShoot = false;

	public	bool				m_bToCatch = false;

	public SkillInstance		m_toSkillInstance
	{
		get{return _toSkillInstance;} 
		set{_toSkillInstance = value;} 
	}
	private SkillInstance		_toSkillInstance;
	
	public	Player				m_passTarget = null;
	
	public  bool				m_bMovedWithBall = false;

    public IM.Number m_speedPassBall = new IM.Number(10);
												 
	public	IM.Number			m_dunkDistance	 = IM.Number.two;
	public	IM.Number			m_LayupDistance	 = new IM.Number(4);

	public Stamina				m_stamina {get; private set;}
	public FightStatus			m_startPos = FightStatus.FS_NONE;

	private bool				m_dirty = true;
	private bool				m_tired = false;

	public	HandWithBall		m_eHandWithBall;
	public	bool 				m_bWithBall
	{ 
		get
		{
			return m_eHandWithBall != HandWithBall.eNone;
		}
		private set
		{
			if( !value )
				m_eHandWithBall = HandWithBall.eNone;
			else
				m_eHandWithBall = HandWithBall.eRight;
		}
	}
	public UBasketball m_ball {get; private set;}

	public	bool				m_bNative
	{
		get
		{
			return m_roleInfo.acc_id == MainPlayer.Instance.AccountID;
		}
		set{}
	}

	public	bool				m_bOnGround = true;
	
	public 	PlayerStateMachine				m_StateMachine{ get; private set; }
	public 	PlayerActionEventHandler		eventHandler{ get; private set; }
	public 	PlayerCollider					m_collider{ get; private set; }
	public 	PickupDetector					m_pickupDetector{ get; private set; }
    public PlayerMoveCollider               m_moveCollider { get; private set; }

	Animation m_animation;
	Dictionary<string, string> positionActions;
	Dictionary<string, string> roleActions;
	static string resAnimPath = "Object/Player/tongyong-nv/animation/";
	static string resSpecialAnimPath = "Object/Player/SpecialAction/";

	//to player hud
	private Object				m_resTeamIndicator;
	private	GameObject			m_goTeamIndicator;
	private static Object m_resBallOwnerIndicator;
	private GameObject m_goBallOwnerIndicator;

	public 	CatchHelper			m_catchHelper;
	
	public bool 				m_alwaysForbiddenPickup;
	public bool					m_enablePickupDetector = true;
	
    static IM.Quaternion PRESET_ROOT_ROT = IM.Quaternion.Euler(-IM.Math.QTR_CIRCLE, IM.Number.zero, IM.Number.zero);
    //Root pos and angle
    public IM.Quaternion rootLocalRotation = PRESET_ROOT_ROT;
    public IM.Vector3 rootLocalPos;
    public IM.Vector3 rootPos;
    //Ball socket pos
    public IM.Vector3 ballSocketLocalPos;
    public IM.Vector3 ballSocketPos;
    //RHand pos
    public IM.Vector3 rHandLocalPos;
    public IM.Vector3 rHandPos;
    //Pelvis pos
    public IM.Vector3 pelvisLocalPos;
    public IM.Vector3 pelvisPos;

	public	AISystem			m_aiMgr;
	public	AISystem_Assist		m_aiAssist;
	public static Dictionary<PositionType, RoadPathManager.SectorArea> positionFavorSectors;
	public RoadPathManager.SectorArea m_favorSectors { get { return positionFavorSectors[m_position]; } }

	public static Dictionary<PositionType, RoadPathManager.SectorArea> positionBounceSectors;
	public RoadPathManager.SectorArea m_bounceSectors { get { return positionBounceSectors[m_position]; } }
	public	bool				m_bIsAI{private set{} get{ return m_aiMgr != null;} }

	public 	InputDispatcher 	m_inputDispatcher;
	public	int					m_curInputDir;

	private List<PlayerBuildListener> 		m_listeners = new List<PlayerBuildListener>();

	public AOD					m_AOD;
	public PlayerInfoVisualizer m_InfoVisualizer;
	public PlayerAnimAttribute	m_animAttributes;

    public PlayerModel model;
    public GameObject gameObject { get { return model != null ? model.gameObject : null; } }
    public Transform transform { get { return model != null ? model.gameObject.transform : null; } }

    public MoveController moveCtrl = new MoveController();
	public SkillSystem			m_skillSystem{ get; private set; }

	private GameObject			m_goEffectTired;
	//public List<GameObject> 	m_goEffectListCrossOver = new List<GameObject>();

	public List<Vector2>		m_takenSectorRanges = new List<Vector2>();

    public AnimationManager animMgr;

	//shoot or dunk can be blocked
	public Blockable m_blockable;

    public ShootStrength shootStrength;

	public LostBallContext		m_lostBallContext;
	
	public	MoveToHelper		m_moveHelper{ private set; get; }

	public SparkEffect			mSparkEffect{ get; private set; }

	public bool					m_toTakeOver = false;
	public bool					m_takingOver = false;
    public bool                 m_applyLogicPostion = true; 
	public void RegisterListener( PlayerBuildListener listener )
	{ 
		if( m_listeners.Contains(listener) )
			return;
		m_listeners.Add(listener);
	}
	
	public void RemoveListener( PlayerBuildListener listener )
	{
		if( !m_listeners.Contains(listener) )
			return;
		m_listeners.Remove(listener);
	}
	
	public void RemoveAllListeners()
	{
		m_listeners.Clear();
	}

	public Player( RoleInfo roleInfo, Team team )
	{
		m_roleInfo = roleInfo;

		m_blockable = new Blockable(this);

		m_team = team;
		team.AddMember(this);
		
		m_enableAction = true;
		m_enableMovement = true;
		
		uint id = roleInfo.id;
        //if( id == 1001 )
        //{
        //    id = 1500; // TODO:avoid 1001
        //}
		if (id < 10000)
		{
			RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id);
			m_name = data.name;
			m_position = (PositionType)data.position;
			m_shapeID = data.shape;
			m_gender = (GenderType)data.gender;
		}
		else
		{
			NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(id);
            RoleBaseData2 caData = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(config.shape);
			if (caData == null)
				Logger.LogError("Player ctor: role base config data not found. Role ID: " + config.shape);
			m_name = config.name;
			m_position = (PositionType)config.position;
			m_shapeID = (int)config.shape;
            m_gender = (GenderType)caData.gender;
		}

        shootStrength = new ShootStrength(m_position);
		
        /*
		if (id == MainPlayer.Instance.CaptainID)
		{
			m_attrData = MainPlayer.Instance.GetRoleAttrs(m_roleInfo);
		}
		else
		{
			m_attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(id);
		}*/
        //m_attrData = GameSystem.Instance.AttrDataConfigData.GetCaptainAttrData(id);
        //if (m_attrData == null)
        //    m_attrData = GameSystem.Instance.AttrDataConfigData.GetRoleAttrData(id, m_roleInfo.quality);
		
		m_animAttributes = new PlayerAnimAttribute();
		
		mMovements = new PlayerMovement[(int)PlayerMovement.Type.eMax];
		mMovements[(int)PlayerMovement.Type.eRunWithBall] = new PlayerMovement(PlayerMovement.Type.eRunWithBall);
		mMovements[(int)PlayerMovement.Type.eRunWithoutBall] = new PlayerMovement(PlayerMovement.Type.eRunWithoutBall);
		mMovements[(int)PlayerMovement.Type.eRushWithBall] = new PlayerMovement(PlayerMovement.Type.eRushWithBall);
		mMovements[(int)PlayerMovement.Type.eRushWithoutBall] = new PlayerMovement(PlayerMovement.Type.eRushWithoutBall);
		mMovements[(int)PlayerMovement.Type.eDefense] = new PlayerMovement(PlayerMovement.Type.eDefense);
		mMovements[(int)PlayerMovement.Type.eBackToBackRun] = new PlayerMovement(PlayerMovement.Type.eBackToBackRun);
		
		m_lostBallContext = new LostBallContext();
		m_moveHelper = new MoveToHelper(this);
		m_dir = 0;
	}

	//private void CalcFinalAttr()
	//{
	//	if (m_attrData == null)
	//	{
	//		Debug.DebugBreak();
	//	}
	//	m_finalAttrs = new Dictionary<string, uint>();
	//	foreach (KeyValuePair<string, uint> pair in m_attrData.attrs)
	//	{
	//		m_finalAttrs.Add(pair.Key, pair.Value);
	//	}

	//	/*		//暂时取消被动技能
	//	foreach (SkillSlotProto slot in m_roleInfo.skill_slot_info)
	//	{
	//		if (slot.id / 1000 == 1)	//passive skill
	//		{
	//			Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_SKILL, slot.skill_uuid);
	//			if (goods != null)
	//			{
	//				SkillAttr skillAttr = GameSystem.Instance.SkillConfig.GetSkill(goods.GetID());
	//				if (skillAttr != null)
	//				{
	//					SkillLevel skillLevel = skillAttr.levels[goods.GetLevel()];
	//					foreach (KeyValuePair<string, uint> pair in skillLevel.additional_attrs)
	//					{
	//						m_finalAttrs[pair.Key] += pair.Value;
	//					}
	//				}
	//			}
	//		}
	//	}*/

	//	//foreach (TattooSlotProto slot in m_roleInfo.tattoo_slot_info)
	//	//{
	//	//	Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TATTOO, slot.tattoo_uuid);
	//	//	if (goods != null)
	//	//	{
	//	//		TattooLvConfigData tattoo = GameSystem.Instance.TattooConfig.GetTattooConfig(goods.GetID(), goods.GetLevel());
	//	//		if (tattoo != null)
	//	//		{
	//	//			foreach (KeyValuePair<uint, uint> pair in tattoo.addn_attr)
	//	//			{
	//	//				string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(pair.Key);
	//	//				m_finalAttrs[symbol] += pair.Value;
	//	//			}
	//	//		}
	//	//	}
	//	//}

	//	// Training Not use in this version!!!
	//	//foreach (ExerciseInfo info in m_roleInfo.exercise)
	//	//{
	//	//    TrainingLevelConfigData training = GameSystem.Instance.TrainingConfig.GetTrainingLevelConfig(info.id, info.quality);
	//	//    foreach (KeyValuePair<uint, uint> pair in training.addn_attr)
	//	//    {
	//	//        string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(pair.Key);
	//	//        m_finalAttrs[symbol] += pair.Value;
	//	//    }
	//	//}
	//}

	public static void BuildFavorSectors()
	{
		if (positionFavorSectors != null)
			return;

		positionFavorSectors = new Dictionary<PositionType, RoadPathManager.SectorArea>();
		positionBounceSectors = new Dictionary<PositionType, RoadPathManager.SectorArea>();

		int colomns = RoadPathManager.Instance.m_AngleList.Count;
		int rows = RoadPathManager.Instance.m_DistanceList.Count;
		
		RoadPathManager.SectorArea favorSectors = new RoadPathManager.SectorArea();
		favorSectors.start.x = IM.Number.zero;
		favorSectors.start.y = IM.Number.zero;

		favorSectors.range.x = new IM.Number(colomns);
		favorSectors.range.y = new IM.Number(3);
		positionFavorSectors.Add(PositionType.PT_C, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
        favorSectors.start.x = IM.Number.zero;
        favorSectors.start.y = IM.Number.zero;

        favorSectors.range.x = new IM.Number(colomns);
        favorSectors.range.y = new IM.Number(5);
		positionBounceSectors.Add(PositionType.PT_C, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
        favorSectors.range.x = new IM.Number(colomns);
        favorSectors.range.y = new IM.Number(2);

		favorSectors.start.x = IM.Number.zero;
        favorSectors.start.y = new IM.Number(rows) - favorSectors.range.y;
		positionFavorSectors.Add(PositionType.PT_PG, favorSectors);
		positionFavorSectors.Add(PositionType.PT_SG, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
        favorSectors.range.x = new IM.Number(colomns);
        favorSectors.range.y = new IM.Number(3);

        favorSectors.start.x = IM.Number.zero;
        favorSectors.start.y = new IM.Number(rows) - favorSectors.range.y;
		positionBounceSectors.Add(PositionType.PT_PG, favorSectors);
		positionBounceSectors.Add(PositionType.PT_SG, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
        favorSectors.start.x = IM.Number.zero;
        favorSectors.start.y = new IM.Number(2);

        favorSectors.range.x = new IM.Number(colomns);
        favorSectors.range.y = new IM.Number(2);
		positionFavorSectors.Add(PositionType.PT_PF, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
        favorSectors.start.x = IM.Number.zero;
		favorSectors.start.y = IM.Number.one;

        favorSectors.range.x = new IM.Number(colomns);
		favorSectors.range.y = new IM.Number(4);
		positionBounceSectors.Add(PositionType.PT_PF, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
		favorSectors.start.x = IM.Number.zero;
		favorSectors.start.y = new IM.Number(4);

        favorSectors.range.x = new IM.Number(colomns);
		favorSectors.range.y = new IM.Number(2);
		positionFavorSectors.Add(PositionType.PT_SF, favorSectors);

		favorSectors = new RoadPathManager.SectorArea();
		favorSectors.start.x = IM.Number.zero;
		favorSectors.start.y = new IM.Number(3);

        favorSectors.range.x = new IM.Number(colomns);
        favorSectors.range.y = new IM.Number(4);
		positionBounceSectors.Add(PositionType.PT_SF, favorSectors);
	}

	public void FaceTo( IM.Vector3 vTarget )
	{
		IM.Vector3 dir =  vTarget - position;
		dir = new IM.Vector3(dir.x, IM.Number.zero, dir.z);
		if( dir == IM.Vector3.zero )
			return;
		forward = dir.normalized;
	}

	public void Show(bool bStand = true)
	{
		gameObject.SetActive(true);
		m_InfoVisualizer.SetActive(true);

		//GameMatch_PVP match = GameSystem.Instance.mClient.mCurMatch as GameMatch_PVP;
		//if( match != null && match.m_stateMachine.m_curState.m_eState != MatchState.State.e

		if( bStand )
			m_StateMachine.SetState(PlayerState.State.eStand, true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		m_InfoVisualizer.SetActive(false);
	}


	public void ShowIndicator(Color color, bool enableAnim)
	{
		if( m_resTeamIndicator == null )
            m_resTeamIndicator = ResourceLoadManager.Instance.LoadPrefab("prefab/indicator/circle");

		if (m_goTeamIndicator == null)
		{
			m_goTeamIndicator = GameObject.Instantiate(m_resTeamIndicator) as GameObject;
			m_goTeamIndicator.transform.parent = transform;
			m_goTeamIndicator.transform.localPosition = new Vector3(0.0f, 0.03f, 0.0f);
		}
		m_goTeamIndicator.SetActive(true);
		Animator animator = m_goTeamIndicator.GetComponent<Animator>();
		if( animator != null )
			animator.enabled = enableAnim;
		
		Renderer renderer = m_goTeamIndicator.GetComponentInChildren<Renderer>();
        if (renderer != null)
            if (GameSystem.Instance.mClient.mCurMatch.mCurScene.mGround.In3PointRange(position.xz, IM.Number.zero))
                renderer.material.color = color;
            else
                renderer.material.color = Color.red;
	}

	public void HideIndicator()
	{
		if (m_goTeamIndicator != null)
			m_goTeamIndicator.SetActive(false);
	}

    public void UpdateIndicator(Color color)
    {
        if (m_goTeamIndicator != null && m_goTeamIndicator.activeSelf)
        {
            Renderer renderer = m_goTeamIndicator.GetComponentInChildren<Renderer>();
            if (renderer != null)
                renderer.material.color = color;
        }
    }

	public void ShowBallOwnerIndicator(bool show = true)
	{
		if (show)
		{
			if (m_resBallOwnerIndicator == null)
				m_resBallOwnerIndicator = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/RebPlacement");
			if (m_goBallOwnerIndicator == null)
			{
				m_goBallOwnerIndicator = GameObject.Instantiate(m_resBallOwnerIndicator) as GameObject;
				m_goBallOwnerIndicator.transform.parent = transform;
				m_goBallOwnerIndicator.transform.localPosition = new Vector3(0f, 0.01f, 0f);
			}
			m_goBallOwnerIndicator.SetActive(true);
		}
		else
		{
			if (m_goBallOwnerIndicator != null)
				m_goBallOwnerIndicator.SetActive(false);
		}
	}

	void _UpdatePickUp()
	{
		bool bEnable = false;
		if( m_StateMachine != null && m_StateMachine.m_curState != null )
			bEnable = (m_StateMachine.m_curState.m_eState == PlayerState.State.eStand 
			           || m_StateMachine.m_curState.m_eState == PlayerState.State.eDefense
			           || m_StateMachine.m_curState.m_eState == PlayerState.State.eRun
			           || m_StateMachine.m_curState.m_eState == PlayerState.State.eRush ) && !m_bWithBall ;

		if( m_pickupDetector != null )
			m_pickupDetector.m_enable = m_enablePickupDetector && bEnable && !m_alwaysForbiddenPickup;
	}
	
	void _FilterOutAllExclusiveOper(bool bCanCross, bool bCanPass)
	{
		if( m_toSkillInstance == null )
			return;
		Command cmd = (Command)m_toSkillInstance.skill.action_type;
		if ( cmd == Command.PickAndRoll )
			return;
		if (!(bCanPass && cmd == Command.Pass))
			m_toSkillInstance = null;
	}

    //渲染层
	public void LateUpdate()
	{
		if(gameObject == null )
			return;

        //防守扇形区域
		if( m_AOD != null )
			m_AOD.Update();

		m_skillSystem.LateUpdate();

        //逻辑位置同步显示位置（跟随）
        if (m_applyLogicPostion)
        {
            model.root.localPosition = (Vector3)rootLocalPos;
            model.root.localRotation = (Quaternion)rootLocalRotation;
            //位置跟随
            Vector3 targetPos = (Vector3)position;
            Vector3 curPos = transform.position;
            transform.position = Vector3.MoveTowards(curPos, targetPos, (float)velocity * Time.deltaTime);
            //transform.position = (Vector3)position;
            /*
            if (Time.timeScale != 0f)
            {
                Logger.Log(string.Format("Set player view pos: {0} -> {1} targetPos:{2} speed:{3} dt:{4}",
                    curPos.ToString("F3"), transform.position.ToString("F3"), targetPos.ToString("F3"),
                    velocity.ToString(), Time.deltaTime.ToString("F3")));
            }
            //*/
            //朝向跟随
            Quaternion targetRot = (Quaternion)rotation;
            Quaternion curRot = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(curRot, targetRot, (float)turningSpeed * Mathf.Rad2Deg * Time.deltaTime);
            //transform.rotation = (Quaternion)rotation;
            /*
            if (Time.timeScale != 0f)
            {
                Logger.Log(string.Format("Set player view rot: {0} -> {1} targetRot:{2} speed:{3} dt:{4}",
                    curRot.ToString("F3"), transform.rotation.ToString("F3"), targetRot.ToString("F3"),
                    turningSpeed.ToString(), Time.deltaTime.ToString("F3")));
            }
            //*/

            //transform.FindChild("Logic").position = (Vector3)position;
            //transform.FindChild("Logic").rotation = (Quaternion)rotation;
        }
	}

    //逻辑层
    public void LateUpdate(IM.Number deltaTime)
    {
		if( m_StateMachine.m_curState != null )
			m_StateMachine.m_curState.LateUpdate(deltaTime);

        animMgr.Update(deltaTime);
    }

    //渲染帧
    public void Update()
    {
		if( gameObject == null )
			return;

        //体力不足特效
		if( m_stamina != null && m_goEffectTired != null )
		{
			if( (m_stamina.m_curStamina / m_stamina.m_maxStamina < Stamina.fInsufficientValue) && !m_tired )
			{
				m_tired = true;
				m_goEffectTired.GetComponent<ParticleSystem>().Play();
			}

			if((m_stamina.m_curStamina / m_stamina.m_maxStamina > Stamina.fInsufficientValue))
			{
				m_tired = false;
				m_goEffectTired.GetComponent<ParticleSystem>().Stop();
			}
		}
		
        //刷新球员信息显示
		if( m_InfoVisualizer != null )
			m_InfoVisualizer.Update();

        //特效
		if( mSparkEffect != null )
		{
			mSparkEffect.Update(Time.deltaTime);
			mSparkEffect.EnableSpark(m_ball != null);
		}
    }

    //逻辑帧
	public void Update(IM.Number fDeltaTime)
	{
        //与其他玩家碰撞
		if( m_collider != null )
			m_collider.Update(fDeltaTime);

        //拾球
		if( m_pickupDetector != null )
			m_pickupDetector.Update(fDeltaTime);
        //拾球开关
		_UpdatePickUp();

        //体力恢复
		if( m_stamina != null )
			m_stamina.RecoverStamina(fDeltaTime);

		//输入
        if (m_inputDispatcher != null)
            m_inputDispatcher.Update(fDeltaTime);

        //投篮力度
        if (shootStrength != null)
            shootStrength.Update(fDeltaTime);

		//TODO: bug.
		if( !m_bWithBall ) 
			m_bMovedWithBall = false;

        //接球
		if( m_catchHelper != null )
			m_catchHelper.Update(fDeltaTime);
		
        //AI
		if( m_aiMgr != null &&
			(m_inputDispatcher == null || !m_inputDispatcher.m_enable ||
			m_inputDispatcher.inTakeOver || m_inputDispatcher.disableAIOnAction))
			m_aiMgr.Update( fDeltaTime );

        //辅助AI
		if( m_aiAssist != null)
			m_aiAssist.Update( fDeltaTime );

        //过滤操作
		if (!m_enableAction)
			_FilterOutAllExclusiveOper(false, true);
		
		if( !m_enableMovement )
			moveDirection = IM.Vector3.zero;

        //移动控制
		if( m_moveHelper != null )
			m_moveHelper.Update(fDeltaTime);

        //状态机
		m_StateMachine.Update(fDeltaTime);

		/*
		if (m_toSkillInstance != null)
		{
			Command skillCmd = (Command)(m_toSkillInstance.skill.action_type);
			PlayerState.State state = m_StateMachine.m_curState.m_eState;
			if (skillCmd == Command.Pass && state != PlayerState.State.ePass)
				Logger.LogError("Pass skill haven't been executed, current state: " + state + " ID:" + m_id);
		}
		*/

		m_toSkillInstance = null;
	}

	public void GrabBall(UBasketball basketball, bool bCatch = false)
	{
		if( basketball == null)
			return;
		
		m_bWithBall = true;
		m_ball = basketball;
		basketball.OnGrab(this, bCatch);
		//Logger.Log("player id: " + m_id + " grab ball.");
	}
	
	public void DropBall(UBasketball basketball)
	{
		m_bWithBall = false;
		m_ball = null;

		if( basketball == null )
			return;
		basketball.Reset();

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( player.m_AOD != null && player.m_AOD.visible )
				player.m_AOD.visible = false;
		}
		//Logger.Log("player id: " + m_id + " drop ball.");
	}

	public void Move(IM.Number deltaTime, IM.Vector3 velocity)
	{
		if( velocity == IM.Vector3.zero || IM.Number.Approximately(deltaTime, IM.Number.zero) )
			return;

        IM.Vector3 deltaMove = velocity * deltaTime;
        deltaMove = m_moveCollider.AdjustDeltaMove(deltaMove);
		moveCtrl.Move(deltaMove);
        this.velocity = deltaMove.magnitude / deltaTime;
	}

	public void MoveTowards(IM.Vector3 dirFaceTo, IM.Number turningSpeed, IM.Number deltaTime, IM.Vector3 velocity)
	{
		IM.Number step = turningSpeed * deltaTime;
		dirFaceTo.y = IM.Number.zero;

        //平滑转向
		IM.Vector3 newDir = IM.Vector3.RotateTowards(forward, dirFaceTo, step, IM.Number.zero);
        rotation = IM.Quaternion.LookRotation(newDir);
		Move(deltaTime, velocity);
        this.turningSpeed = turningSpeed;
	}

    public bool IsDefended(IM.Number devAngle, IM.Number devDist)
	{
		return GetDefender(devAngle, devDist) != null;
	}

    public bool IsDefended()
    {
        return IsDefended(IM.Number.zero, IM.Number.zero);
    }

    public bool IsDefended(IM.Number devAngle)
    {
        return IsDefended(devAngle, IM.Number.zero);
    }


	public Player GetDefender(IM.Number devAngle, IM.Number devDist)
	{
		if( m_defenseTarget == null )
			return null;
		foreach(Player player in m_defenseTarget.m_team.members)
		{
			if( m_AOD.GetStateByPos(player.position, devAngle, devDist) != AOD.Zone.eInvalid )
				return player;
		}
		return null;
	}

    public Player GetDefender()
    {
        return GetDefender(IM.Number.zero, IM.Number.zero);
    }

    public Player GetDefender(IM.Number devAngle)
    {
        return GetDefender(devAngle, IM.Number.zero);
    }
    

	public Player GetNearestDefender()
	{
		if( m_defenseTarget == null )
			return null;
		Player defender = null;
		IM.Number dist = IM.Number.max;
		foreach(Player player in m_defenseTarget.m_team.members)
		{
			IM.Number curDist = GameUtils.HorizonalDistance(player.position, position);
			if(curDist < dist)
			{
				defender = player;
				dist = curDist;
			}
		}
		return defender;
	}

	public bool CanDunk()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null )
			return false;
		if( match.mCurScene == null || match.mCurScene.mBasket == null )
			return false;
		//return GameUtils.HorizonalDistance( position, match.mCurScene.mBasket.transform.position ) < m_dunkDistance;
		return true;
	}

	public bool CanLayup()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null )
			return false;
		if( match.mCurScene == null || match.mCurScene.mBasket == null )
			return false;

		//return GameUtils.HorizonalDistance( position, match.mCurScene.mBasket.transform.position ) < m_LayupDistance;
		return true;
	}

	public Dictionary<string, uint> GetSkillAttribute(SkillInstance curExcSkill = null)
	{
		if( curExcSkill == null )
			curExcSkill = m_StateMachine.m_curState.m_curExecSkill;

		if( curExcSkill == null )
		{
			//Logger.LogError("can not find player: " + player.m_id + " current execute skill.");
			return null;
		}

		SkillLevel curLevel = curExcSkill.skill.levels[curExcSkill.level];
		Dictionary<string, uint> skillAttr = curLevel.additional_attrs;
		if( skillAttr == null )
		{
			Logger.LogError("Can not build player: " + m_name + " ,can not fight skill data by skill id: " + curExcSkill.skill.id );
			return null;
		}

		Logger.Log("Skill attr, ID:" + curExcSkill.skill.id);
		foreach (KeyValuePair<string, uint> pair in skillAttr)
		{
			Logger.Log(pair.Key + ": " + pair.Value);
		}
		
		return skillAttr;
	}

	public SkillSpec GetSkillSpecialAttribute(SkillSpecParam skillSpecParam, SkillInstance curExcSkill = null)
	{
		if( curExcSkill == null )
			curExcSkill = m_StateMachine.m_curState.m_curExecSkill;
		
		if( curExcSkill == null )
		{
			//Logger.LogError("can not find player: " + player.m_id + " current execute skill.");
			return null;
		}
		
		SkillLevel curLevel = curExcSkill.skill.levels[curExcSkill.level];
		Dictionary<uint, SkillSpec> skillAttr = curLevel.parameters;
		if( skillAttr == null )
		{
			Logger.LogError("Can not build player: " + m_name + " ,can not fight skill data by skill id: " + curExcSkill.skill.id );
			return null;
		}

		SkillSpec result = null;
		if( !skillAttr.TryGetValue( (uint)skillSpecParam, out result ) )
		{
			Logger.LogError("Can not find skill param: " + skillSpecParam + ". Current skill: " + curExcSkill.skill.id );
			return null;
		}

		return skillAttr[(uint)skillSpecParam];
	}


	public bool CanRebound( UBasketball ball )
	{
		if( ball.m_bGoal )
			return false;

		//still on basket
		//if( ball.m_ballState == UBasketball.State.eRebound && ball.m_shootSolution != null )
		//	return false;

		if( ball.m_ballState != BallState.eRebound )
			return false;

		//if( ball.m_bReboundSuccess )
		//	return false;

		SkillInstance skillInstance = m_skillSystem.GetBasicSkillsByCommand(Command.Rebound)[0];
		Dictionary<string, uint> skillAttr = GetSkillAttribute(skillInstance);
		if( skillAttr == null )
			return false;

		Dictionary<string, uint> data = m_finalAttrs;
		if( data == null )
		{
			Logger.LogError("Can not find data.");
			return false;
		}
		IM.Number fDistPlayer2Ball = GameUtils.HorizonalDistance(position, ball.position);
		if( fDistPlayer2Ball > m_fReboundDist )
			return false;
		
		//choose action
		SkillInstance basicRebound = m_skillSystem.GetBasicSkillsByCommand(Command.Rebound)[0];
		string basicActionId = basicRebound.skill.actions[0].action_id;
        IM.Number frameRate = animMgr.GetFrameRate(basicActionId);
		Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = m_animAttributes.m_rebound;
		int reboundKey = rebounds[animMgr.GetOriginName(basicActionId)].GetKeyFrame("OnRebound").frame;
		IM.Number fEventTime = reboundKey / frameRate;
		IM.Vector3 vBallPosRebound;
		//float fCurTime = ball.m_shootSolution == null? ball.m_fTime : ball.m_shootSolution.m_fTime;
        IM.Number fCurTime = ball.m_fTime;
        IM.Number fHighestTime = ball.CompleteLastCurve().GetHighestTime();
		if (fCurTime + fEventTime < fHighestTime)	//只能抢下落球
			return false;
		if( !ball.GetPositionInAir(fCurTime + fEventTime, out vBallPosRebound) )
			return false;

        IM.Number minHeight = IM.Number.zero, maxHeight = IM.Number.zero;
		PlayerState_Rebound.GetHeightRange(this, out minHeight, out maxHeight, basicRebound);

		if(vBallPosRebound.y < minHeight || vBallPosRebound.y > maxHeight)
			return false;

		return true;
	}

	public void InitAnimStateMachine()
	{
        if (!CreateAnimation(true))
        {
            return;
        }
        animMgr = new AnimationManager(this);
		m_StateMachine = new PlayerStateMachine(this);
	}

    public bool CreateAnimation(bool loadAll)
    {
        if (gameObject == null)
            return false;

        m_animation = gameObject.GetComponent<Animation>();
        if (m_animation == null)
            m_animation = gameObject.AddComponent<Animation>();
        m_animation.cullingType = AnimationCullingType.AlwaysAnimate;
        m_animation.playAutomatically = false;

		//special animations
		positionActions = GameSystem.Instance.SpecialActionConfig.GetPositionActions(m_position);
		roleActions = GameSystem.Instance.SpecialActionConfig.GetRoleActions(m_roleInfo.id);

		if (loadAll)
		{
			//load animations
            Object[] resAnimations = null;
            if (GlobalConst.IS_DEVELOP)
            {
                resAnimations = Resources.LoadAll<AnimationClip>(resAnimPath);
            }
            else
            {
                string path = "object/object_player_tongyong-nv_animation_all.assetbundle";
                var assetBundle = ResourceLoadManagerBase.Instance.GetLoadAssetBundle(path);
                resAnimations = assetBundle.LoadAllAssets(typeof(AnimationClip));
            }
			foreach (Object resAnim in resAnimations)
			{
				AnimationClip clip = resAnim as AnimationClip;
				string specialAction = GetSpecialAction(clip.name);
				if (!string.IsNullOrEmpty(specialAction))
				{
					AnimationClip specialClip = LoadAnimation(specialAction, true);
					m_animation.AddClip(specialClip, clip.name);
					//Restore clip name to original
					AnimationClip addedClip = m_animation.GetComponent<Animation>()[clip.name].clip;
					addedClip.name = specialClip.name;
				}
				else
					m_animation.AddClip(clip, clip.name);
			}
		}

        return true;
    }

	public void PlayAnimation(string action)
	{
        if (animMgr != null)
            Logger.LogError("Player.PlayAnimation can not be called out of match.");
		if (m_animation.GetComponent<Animation>()[action] == null)
		{
			string specialAction = GetSpecialAction(action);
			if (!string.IsNullOrEmpty(specialAction))
			{
				AnimationClip specialClip = LoadAnimation(specialAction, true);
				m_animation.AddClip(specialClip, action);
				//Restore clip name to original
				AnimationClip addedClip = m_animation.GetComponent<Animation>()[action].clip;
				addedClip.name = specialClip.name;
			}
			else
			{
				AnimationClip clip = LoadAnimation(action, false);
				m_animation.AddClip(clip, clip.name);
			}
		}
		m_animation.GetComponent<Animation>().Play(action);
	}

	string GetSpecialAction(string name)
	{
		string specialAction = string.Empty;
		if (roleActions == null || !roleActions.TryGetValue(name, out specialAction))
		{
			if (positionActions != null)
				positionActions.TryGetValue(name, out specialAction);
		}
		return specialAction;
	}

	AnimationClip LoadAnimation(string name, bool isSpecial)
	{
        string path = (isSpecial ? resSpecialAnimPath : resAnimPath) + name;
        AnimationClip clip = ResourceLoadManager.Instance.GetResources(path) as AnimationClip;
		if (clip == null)
			Logger.LogError("Action not exist. " + name);
		return clip;
	}

	public void BuildGameData(IM.Number modelScale)
	{
		if( IM.Number.Approximately(modelScale, IM.Number.zero) )
			modelScale = IM.Number.one;
		
		IM.Number modelScaleInv = IM.Number.one / modelScale;

		Dictionary<string, uint> data = m_finalAttrs;
		if( data == null )
		{
			Logger.LogError("Can not build player: " + m_name + " ,can not fight state by id: " + m_id );
			return;
		}
		
		//dunk
		uint fAttriDunkFar = data["dunk_middle"];
		//m_dunkDistance = fAttriDunkFar * 0.0054f + 1.8f;
		m_dunkDistance = new IM.Number(20);
		
		//layup
		uint fAttriLayupFar = data["layup_middle"];
		//m_LayupDistance = fAttriLayupFar * 0.0054f + 1.8f;
		m_LayupDistance = new IM.Number(5);


		//move
		int attrSpeed = (int)data["speed"];
		PlayerMovement runWithoutBall = mMovements[(int)PlayerMovement.Type.eRunWithoutBall];
        //attrSpeed * 0.00648f + 2.88f;
        //const int F_SQR = (int)(0.00648f * IM.Math.SQR_FACTOR);
        //const int C_SQR = (int)(2.88f * IM.Math.SQR_FACTOR);
        //IM.Number standardRunSpeedWOB = IM.Number.Raw(IM.Math.RndDiv(attrSpeed * F_SQR + C_SQR, IM.Math.FACTOR));
        IM.Number standardRunSpeedWOB = (IM.Number)(attrSpeed * new IM.BigNumber(0, 006480) + new IM.Number(2, 880));
		runWithoutBall.mAttr.m_curSpeed = standardRunSpeedWOB;
		runWithoutBall.mAttr.m_playSpeed = runWithoutBall.mAttr.m_curSpeed / runWithoutBall.mAttr.m_initSpeed * modelScaleInv;
		
		PlayerMovement runWithBall = mMovements[(int)PlayerMovement.Type.eRunWithBall];
		//float fStandardRunSpeedWB = fStandardRunSpeedWOB * 0.85f;
		IM.Number standardRunSpeedWB = standardRunSpeedWOB;
		runWithBall.mAttr.m_curSpeed = standardRunSpeedWB;
		runWithBall.mAttr.m_playSpeed = runWithBall.mAttr.m_curSpeed / runWithBall.mAttr.m_initSpeed * modelScaleInv;
		
		PlayerMovement rushWithBall = mMovements[(int)PlayerMovement.Type.eRushWithBall];
		rushWithBall.mAttr.m_curSpeed = standardRunSpeedWB * new IM.Number(1, 300);
		rushWithBall.mAttr.m_playSpeed = rushWithBall.mAttr.m_curSpeed / rushWithBall.mAttr.m_initSpeed * modelScaleInv;
		
		PlayerMovement rushWithoutBall = mMovements[(int)PlayerMovement.Type.eRushWithoutBall];
		rushWithoutBall.mAttr.m_curSpeed = standardRunSpeedWOB * new IM.Number(1, 300);
		rushWithoutBall.mAttr.m_playSpeed = rushWithoutBall.mAttr.m_curSpeed / rushWithoutBall.mAttr.m_initSpeed * modelScaleInv;
		
		PlayerMovement defense = mMovements[(int)PlayerMovement.Type.eDefense];
		defense.mAttr.m_curSpeed = standardRunSpeedWOB ;
		defense.mAttr.m_playSpeed = defense.mAttr.m_curSpeed / defense.mAttr.m_initSpeed * modelScaleInv;
	}

	public void OnLostBall()
	{
		//UBasketball ball = match.mCurScene.mBall;
		//if( ball == null )
		//	return;
		if( m_ball == null )
			return;
		UBasketball ball = m_ball;
		DropBall(m_ball);

        ball.position       = m_lostBallContext.vInitPos;
		ball.initPos 		= m_lostBallContext.vInitPos;
		ball.initVel 		= m_lostBallContext.vInitVel;
	}

	public void Build()
	{
		//CalcFinalAttr();

		if( gameObject == null )
			return;
		if( !m_dirty )
			return;

        eventHandler = new PlayerActionEventHandler(this);

        InitAnimStateMachine();

		m_collider = new PlayerCollider(this);
        m_moveCollider = new PlayerMoveCollider(this);	
		//trigger to pick up ball
		m_pickupDetector = new PickupDetector(this);
		m_pickupDetector.m_enable = false;
		
		HideIndicator();

		m_skillSystem = new SkillSystem(this);
		if( m_attrData != null )
			m_stamina = new Stamina(this, (int)m_attrData.attrs["ph"] );
		
		GameObject resTired = ResourceLoadManager.Instance.LoadPrefab("Prefab/Effect/Tired");
		if( resTired != null )
		{
			m_goEffectTired = GameObject.Instantiate(resTired) as GameObject;
			m_goEffectTired.transform.parent = model.head;
			m_goEffectTired.transform.localPosition = Vector3.zero;
			m_goEffectTired.transform.localScale = Vector3.one;
			m_goEffectTired.transform.localRotation = Quaternion.identity;
			//m_goEffectTired.SetActive(false);
		}
		mSparkEffect = new SparkEffect(model, 0.1f, 1);
				
		m_takenSectorRanges.Add(new Vector2(-1f, 1f));
		m_takenSectorRanges.Add(new Vector2(0f, 1f));
		m_takenSectorRanges.Add(new Vector2(1f, 1f));
		m_takenSectorRanges.Add(new Vector2(-1f, 0f));
		m_takenSectorRanges.Add(new Vector2(0f, 0f));
		m_takenSectorRanges.Add(new Vector2(1f, 0f));
		m_takenSectorRanges.Add(new Vector2(0f, -1f));
		m_takenSectorRanges.Add(new Vector2(1f, -1f));
		m_takenSectorRanges.Add(new Vector2(-1f,-1f));
		
		m_dirty = false;

		Logger.Log( "====================================\n"
		              + "player id: " + m_id + " data info: \n"
		              + "lv: " + m_roleInfo.level + "\n"
		              + "quality: " + m_roleInfo.quality + "\n"
		              + "star: " + m_roleInfo.star + "\n");
		string attrData = "";
		foreach (KeyValuePair<string, uint> pair in m_attrData.attrs)
			attrData += pair.Key + " : " + pair.Value + "\n";
		Logger.Log(attrData);
		Logger.Log("====================================");
	}

	/*
	public void CreateEffect(string effectType)
	{
		if (effectType == "CrossOverTail")
		{
			GameObject resCrossOverTail = ResourceLoadManager.Instance.LoadPrefab("Prefab/Effect/E_Trail1");
			if( resCrossOverTail != null )
			{
				GameObject go = GameObject.Instantiate(resCrossOverTail) as GameObject;
				go.SetActive(false);
				m_goEffectListCrossOver.Add(go);
			}
		}
	}
	*/

    public void Release()
    {
		Renderer renderer = gameObject.GetComponent<Renderer>();
		if( renderer != null && renderer.material != null )
		{
			GameObject.Destroy( renderer.material.mainTexture );
			GameObject.Destroy( renderer.material );
		}
        GameObject.Destroy(gameObject);

		if( m_InfoVisualizer != null )
		{
        	NGUITools.Destroy(m_InfoVisualizer.m_goPlayerInfo);
        	NGUITools.Destroy(m_InfoVisualizer.m_goState);
		}
        m_team.RemoveMember(this);
    }

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
	}

	/// <summary>
	/// 返回球员品阶
	/// </summary>
	/// <returns></returns>
	public uint GetQuality()
	{
		return m_roleInfo.quality;
	}
	/// <summary>
	/// 设置球员品阶
	/// </summary>
	/// <param name="new_quality"></param>
	public void SetQuality(uint new_quality)
	{
		if (new_quality>(uint)QualityType.QT_S_PLUS)
		{
			m_roleInfo.quality= (uint)QualityType.QT_S_PLUS;
		}
		m_roleInfo.quality = new_quality;
	}

	public void EnhanceAttr(IM.Number factor)
	{
		Logger.Log("Enhance attribute of " + m_name + ", factor: " + factor);
		_finalAttrs.Clear();
		if (factor != 1)
		{
			foreach (KeyValuePair<string, uint> pair in m_attrData.attrs)
			{
				AttrNameData attrData = GameSystem.Instance.AttrNameConfigData.GetAttrData(pair.Key);
				uint value;
				if (attrData.type == AttributeType.HEDGING)
					value = (uint)(pair.Value * factor);
				else
					value = pair.Value;
				_finalAttrs.Add(pair.Key, value);
				//Logger.Log(pair.Key + ": " + value);
			}
		}
	}

    void IM.IRootMotionTarget.ResetRoot()
    {
        rootLocalPos = IM.Vector3.zero;
        rootLocalRotation = PRESET_ROOT_ROT;
    }

    void IM.IRootMotionTarget.Apply(IM.Vector3 movement, IM.Quaternion rotation)
    {
        IM.Number deltaTime = IM.Number.Raw(TurnController.GAME_UPDATE_LENGTH * IM.Math.FACTOR / 1000);

        this.turningSpeed = rotation.eulerAngles.y / deltaTime;
        this.rotation *= rotation;

        IM.Number length = movement.magnitude;
        IM.Number speed = length / deltaTime;
        Move(deltaTime, movement.normalized * speed);
    }

    IM.Quaternion IM.IRootMotionTarget.GetInitRotation()
    {
        return rotation;
    }

    public IM.Vector3 TransformNodePosition(SampleNode node, IM.Vector3 localPosition)
    {
        if (node != SampleNode.Root)
            localPosition = rootLocalRotation * localPosition;     //Transform by root
        return position + rotation * localPosition * scale;       //Transform by player
    }

    public bool GetNodePosition(SampleNode node, string clip, IM.Number time, out IM.Vector3 position)
    {
        if (AnimationManager.GetNodePosition(node, clip, time, out position))
        {
            position = TransformNodePosition(node, position);
            return true;
        }
        position = IM.Vector3.zero;
        return false;
    }
}