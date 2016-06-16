using UnityEngine;
using System;
using System.Collections.Generic;

using fogs.proto.msg;

public enum BallEvent
{
	eNone,
	eGoal,
	eCollBoard,
	eCollRim,
}

public enum BallState
{
	eNone,
	eUseBall,
	eLoseBall,
	eRebound,
	eUseBall_Shoot,
	eUseBall_Pass,
}

public class BallStateEffect
{
	public	BallState 	targetState;
	public	GameObject	goEffect;
	public	bool		bDone;

	public BallStateEffect(BallState bs, GameObject	inGoEffect)
	{
		targetState = bs;
		goEffect = inGoEffect;
		bDone = false;
	}
}

public class UBasketball : MonoBehaviour, SparkEffect.ISparkTarget
{
	public uint		m_id;

	public Area	m_shootArea;
	public BallState m_ballState;

	public Player	m_actor{get; private set;}
    public Player   m_passer;
	public Player	m_catcher;
	public Player 	m_interceptor;
    public IM.Vector3 m_interceptedPos;

	private Player _owner;
	public Player	m_owner
	{
		get { return _owner; }
		private set
		{
			if (_owner != null)
				_owner.ShowBallOwnerIndicator(false);
			_owner = value;
            if (_owner != null)
            {
                _owner.ShowBallOwnerIndicator(true);

                transform.parent = _owner.model.ball;
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.parent = null;
            }
		}
	}

	public IM.Vector3 	m_reboundPlacement{get; private set;}

	public bool 	m_pickable = true;
	public bool 	m_special;
	public bool		m_bGoal;
	public bool 	m_collidedWithRim;

	public SkillInstance m_castedSkill;

	public bool  m_bReachMaxHeight{get; private set;}

	public int m_bounceCnt { get; private set; }
	
	private Transform	m_shadow;

    [HideInInspector]
    public IM.Number m_ballRadius = new IM.Number(0, 123);

	public delegate void BallDelegate(UBasketball ball);
	public BallDelegate onHitGround;
	public BallDelegate onBackboardCollision;
	public BallDelegate onRimCollision;
	public BallDelegate onRebound;
	public BallDelegate onShoot;
	public BallDelegate onShootGoal;
	public BallDelegate onBlock;
	public BallDelegate onLost;
	public BallDelegate onIntercepted;

	public BallDelegate onGrab;
	public BallDelegate onCatch;

	public delegate void OnDunkDelegate(UBasketball ball, bool bGoal);
	public OnDunkDelegate onDunk;

	//public bool	m_bReboundSuccess = false;
	public bool	m_bBlockSuccess = false;
	//public bool m_bBodyThrowCatchSuccess = false;

	public int	m_pt = GlobalConst.PT_2;

	public  IM.Number m_fTime;
	private IM.Number m_fPrevTime;

	public Player m_picker = null;

	private IM.Number m_fPickSaverTime;

	private BallStateEffect m_stateEffect;
	//private List<GameObject>	m_effects = new List<GameObject>();

	public ShootSolution m_shootSolution
	{
		get{ return _shootSolution; }
		set{ 
			if( value == _shootSolution )
				return;
			_shootSolution = value; 
			if( _shootSolution != null )
			{
				m_bShootSolutionDirty = true; 
				m_fTime = IM.Number.zero;
				m_fPrevTime = IM.Number.zero;
			}
		}
	}
	private ShootSolution 		_shootSolution;

	public bool m_isLayup{get; private set;}
	public bool m_isDunk{get; private set;}

    private IM.Vector3 _position;
    public IM.Vector3 position
    {
        get { return _position; }
        set
        {
            _position = value;
            transform.position = (Vector3)value;    //同时设定显示层，如果想单独设置逻辑层，使用_position
        }
    }
	private IM.Vector3 _initPos;
    public IM.Vector3 initPos
    {
        get { return _initPos; }
        set
        {
            _initPos = value;
            transform.position = (Vector3)value;    //同时设定显示层
        }
    }
	public IM.Vector3 initVel;
	public IM.Vector3 curVel;
	public LoseBallSimulator 	m_loseBallSimulator;

    //球旋转（显示层）
	public float	m_fAngleSpeed;
	private Vector3 m_vRotateAxis;

	private IM.Number m_verticalVel = IM.Number.zero;

	private Material m_matOrig;
	private SparkEffect m_sparkEffect;

	//for debug
	private bool m_bShootSolutionDirty = false;
	private List<Vector3> m_shootCurveKeys = new List<Vector3>();   //用于绘制曲线
	private bool m_debugSuccess = false;

	void Awake()
	{
	}

	void Start()
	{
		m_bReachMaxHeight = false;
		m_shadow = GameUtils.FindChildRecursive(transform, "Shadow");

		m_matOrig = GetComponent<Renderer>().material;
		m_sparkEffect = new SparkEffect(this, 0.25f);
	}

	public void SetEffect(BallState state, GameObject goEffect)
	{
		if( m_stateEffect != null )
			GameObject.Destroy( m_stateEffect.goEffect );

		m_stateEffect = new BallStateEffect(state, goEffect);
		goEffect.SetActive(false);
	}

	public void RestoreMaterial()
	{
		GetComponent<Renderer>().material = m_matOrig;
	}

	public void SetSparkMaterial(Material mat)
	{
		GetComponent<Renderer>().material = mat;
	}

	public void SetInitPos( IM.Vector3 pos )
	{
		initPos = pos;
		position = pos;
	}

	public bool GetPositionInAir(IM.Number fTime, out IM.Vector3 outPos)
	{
		outPos = IM.Vector3.zero;
		if( _shootSolution == null )
		{
			ShootSolution.SShootCurve lastCurve = CompleteLastCurve();
			outPos = lastCurve.GetPosition(fTime);
			return true;
		}
		else
		{
			if(_shootSolution.GetPosition(fTime, out outPos))
				return true;

			ShootSolution.SShootCurve lastCurve = CompleteLastCurve();
			outPos = lastCurve.GetPosition(fTime - _shootSolution.m_fTime);
			return true;
		}
	}

	void _CalcReboundPlacementPos(IM.Vector3 vInitVel, IM.Vector3 vInitPos)
	{
        //float a = 0.5f * Physics.gravity.y;
        IM.Number a = IM.Number.half * IM.Vector3.gravity.y;
		IM.Number b = vInitVel.y;
		IM.Number c = vInitPos.y - m_ballRadius;
		
		IM.Number fTime_1, fTime_2;
		GameUtils.CalcSolutionQuadraticFunc(out fTime_1, out fTime_2, a, b, c);
		IM.Number fTimeColl = fTime_2 > m_fTime ? fTime_2 : fTime_1;

		IM.Vector3 finalPos = IM.Number.half * fTimeColl * fTimeColl * IM.Vector3.gravity + fTimeColl * vInitVel + vInitPos;
		m_reboundPlacement = new IM.Vector3(finalPos.x, new IM.Number(10), finalPos.z);
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match.mCurScene == null || match.mCurScene.mGround == null )
			return;
		if( m_ballState != BallState.eLoseBall && m_ballState != BallState.eRebound )
			return;
        match.mCurScene.mGround.BoundInZone(ref finalPos);
        m_reboundPlacement = new IM.Vector3(finalPos.x, new IM.Number(10), finalPos.z);
	}
	
	public void OnShoot(Player shooter, Area area, bool bLayup)
	{
		//m_emitState = EmitState.eShot;
		m_ballState = BallState.eUseBall_Shoot;
		m_collidedWithRim = false;
		m_pt = GlobalConst.PT_2;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if(match != null && match.mCurScene != null)
		{
			if( area == Area.eFar )
				m_pt = GlobalConst.PT_3;
		}	
		
		m_actor = shooter;
		//m_shootRate = fRate;
		m_isLayup = bLayup;
		m_isDunk = false;

		m_picker = null;

		if( m_shootSolution != null )
		{
			m_vRotateAxis = Vector3.Cross((Vector3)m_shootSolution.m_vInitVel, Vector3.up);
			m_fAngleSpeed = (float)m_shootSolution.m_vInitVel.magnitude;
		}


		if (onShoot != null)
			onShoot(this);
	}

	public void OnBeginDunk(Player dunker)
	{
		//m_emitState = EmitState.eDunk;
		m_ballState = BallState.eUseBall_Shoot;
		m_collidedWithRim = false;
		m_pt = GlobalConst.PT_2;
	}

	public void OnDunk(bool bGoal, IM.Vector3 vInitVel, IM.Vector3 vInitPos, Player dunker)
	{
		m_actor = dunker;
		initPos = vInitPos;
		initVel = vInitVel;

		if( !bGoal )
		{
			m_ballState = BallState.eRebound;
			m_collidedWithRim = true;
			if( onRebound != null )
				onRebound(this);
		}
		m_bGoal = bGoal;
		m_isDunk = true;
		m_isLayup = false;

		m_picker = null;

		if( onDunk != null ) onDunk(this,bGoal);

		if (!bGoal && onRimCollision != null)
			onRimCollision(this);

		_CalcReboundPlacementPos(vInitVel, vInitPos);
	}

	public void OnBlock()
	{
		m_shootSolution = null;
		m_ballState = BallState.eLoseBall;
		m_collidedWithRim = false;

		if( onBlock != null ) onBlock(this);

		_CalcReboundPlacementPos(initVel, initPos);
	}

	public void OnPass(Player passer, Player catcher, IM.Vector3 vInterceptedPos, Player interceptor = null)
	{
		//m_emitState = EmitState.ePass;
		m_ballState = BallState.eUseBall_Pass;
		m_collidedWithRim = false;
		m_actor = passer;
		m_catcher = catcher;
        m_passer = passer;
		m_interceptor = interceptor;
		m_interceptedPos = vInterceptedPos;
        //m_catcher.m_bToCatch = true;
		IM.Number fDistance = GameUtils.HorizonalDistance(passer.position, catcher.position); 
		IM.Number t = fDistance / m_actor.m_speedPassBall;
		m_verticalVel = t * IM.Number.half * (-IM.Vector3.gravity.y);
	}

	public void OnCatch()
	{
		m_ballState = BallState.eUseBall;
		m_actor = null;
		m_catcher = null;
	}
	
	public void OnGrab(Player grabber, bool bCatch)
	{
		m_shootSolution = null;

		m_bReachMaxHeight = false;
		m_owner = grabber;
		m_actor = null;
		m_ballState = BallState.eUseBall;
		m_bounceCnt = 0;
		m_fAngleSpeed = 0f;

		m_bBlockSuccess	= false;

		m_bGoal = false;
		m_picker = null;
		if (onGrab != null && !bCatch)
			onGrab(this);
		else if(onCatch != null && bCatch)
			onCatch(this);

		//Logger.Log("ball grab, player: " + grabber.m_id );
	}
	
	public void Reset()
	{
		m_castedSkill = null;

		if( transform.parent != null )
		{
			transform.parent = null;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
        else
        {
            position = IM.Vector3.zero;
        }
		
		m_owner = null;
		m_bounceCnt = 0;
		m_ballState = BallState.eLoseBall;
		m_collidedWithRim = false;
		m_actor = null;

		m_picker = null;
		m_interceptor = null;

		if( m_catcher != null )
			m_catcher.m_bToCatch = false;
		m_catcher = null;
	}

	public ShootSolution.SShootCurve CompleteLastCurve()
	{
		ShootSolution.SShootCurve lastCurve = new ShootSolution.SShootCurve();
		if( m_shootSolution != null )
			ShootSimulation.Instance.CalculateShootCurve(ref lastCurve, m_shootSolution.m_vFinPos, m_shootSolution.m_vFinVel);
		else
			ShootSimulation.Instance.CalculateShootCurve(ref lastCurve, initPos, initVel);

		IM.Number timeColl = (-lastCurve.fY_b + IM.Math.Sqrt(lastCurve.fY_b * lastCurve.fY_b - new IM.Number(4) * lastCurve.fY_a * (lastCurve.fY_c - m_ballRadius)) ) * IM.Number.half / lastCurve.fY_a;
		if( timeColl < IM.Number.zero)
			timeColl = (-lastCurve.fY_b - IM.Math.Sqrt(lastCurve.fY_b * lastCurve.fY_b - new IM.Number(4) * lastCurve.fY_a * (lastCurve.fY_c - m_ballRadius)) ) * IM.Number.half / lastCurve.fY_a;
		lastCurve.fTime = timeColl;
		return lastCurve;
	}

	void _BuildShootCurves()
	{
		m_shootCurveKeys.Clear();

		if (m_shootSolution != null)
			m_shootSolution.m_vStartPos = position;
		else
			return;
		
		IM.Number fTime = IM.Number.zero;
		IM.Number fStep = new IM.Number(0, 010);
		IM.Vector3 curPos;

		ShootSolution.SShootCurve lastCurve = CompleteLastCurve();
		while(true)
		{
			if( !m_shootSolution.GetPosition(fTime, out curPos) )
				curPos = lastCurve.GetPosition(fTime - m_shootSolution.m_fTime);
			m_shootCurveKeys.Add((Vector3)curPos);
			fTime += fStep;

			if(fTime > (m_shootSolution.m_fTime + lastCurve.fTime ))
			   break;
		}

		m_debugSuccess = m_shootSolution.m_bSuccess;
	}

	void _DrawShootSolution()
	{
		//draw shoot solution
		int iKeyCount = m_shootCurveKeys.Count;
		for( int idx = 0; idx != iKeyCount; idx++ )
		{
			if( idx + 1 >= iKeyCount )
				break;
			Debug.DrawLine(m_shootCurveKeys[idx], m_shootCurveKeys[idx+1], m_debugSuccess? Color.red : Color.green);
		}
	}

    //渲染层
    void Update()
    {
        //特效
		if( m_stateEffect != null )
		{
			if( m_ballState == m_stateEffect.targetState )
			{
				if( !m_stateEffect.bDone )
				{
					m_stateEffect.goEffect.SetActive(true);
					m_stateEffect.bDone = true;
				}
			}
			else
			{
				if( m_stateEffect.bDone )
					m_stateEffect.goEffect.SetActive(false);
			}
		}

		if( m_ballState == BallState.eLoseBall || m_ballState == BallState.eRebound || m_ballState == BallState.eNone )
			m_sparkEffect.EnableSpark(true);
		else
			m_sparkEffect.EnableSpark(false);

		if( m_sparkEffect != null )
			m_sparkEffect.Update(Time.deltaTime);

        //机会球颜色
		if (m_special)
			GetComponent<Renderer>().material.color = Color.green;

        //绘制投篮曲线
		_DrawShootSolution();
    }

    //逻辑层
	public void Update(IM.Number deltaTime)
	{
		//trick: if owner is nil
		if( m_picker == null )
			m_fPickSaverTime = IM.Number.zero;
		else if( m_owner == null )
			m_fPickSaverTime += deltaTime;
		if( m_fPickSaverTime > IM.Number.one)
			m_picker = null;

		if( m_ballState == BallState.eUseBall_Pass )
		{
			if( m_interceptor == null )
				_PassToTarget(m_catcher.rHandPos, deltaTime);
			else
				_PassToTarget(m_interceptedPos, deltaTime);

			return;
		}

		if( m_ballState != BallState.eUseBall_Shoot )
			m_bReachMaxHeight = false;

        //设置投篮曲线
		if( m_bShootSolutionDirty )
		{
			_BuildShootCurves();
			m_bShootSolutionDirty = false;
		}

		m_fPrevTime = m_fTime;
		m_fTime += deltaTime;
		
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if( curMatch == null || curMatch.mCurScene == null )
			return;
		GameScene scene = GameSystem.Instance.mClient.mCurMatch.mCurScene;
		if( m_shootSolution == null )
		{
            if (m_owner != null)
            {
				m_fTime = IM.Number.zero;
            }
			else if( (m_ballState == BallState.eLoseBall || m_ballState == BallState.eRebound) && m_loseBallSimulator != null )
			{
				LoseBallSimResult ret = m_loseBallSimulator.DoSimulate(initPos, initVel, m_fPrevTime, m_fTime);
				IM.Vector3 finalPos = ret.vecPos;
				scene.mGround.BoundInZone(ref finalPos);
				_position = finalPos;
				if( ret.bStop )
					m_fAngleSpeed = 0.0f;
				curVel = ret.vecVel;

				foreach(LoseBallEvent loseEvent in ret.events)
				{
					if( loseEvent.strEventName != "Coll_Ground" )
						continue;
					m_bounceCnt++;

                    //球撞地声音
                    string strBounceAudioSource = "02";
                    if( m_bounceCnt == 1 )
                        strBounceAudioSource = "04";
                    else if( m_bounceCnt == 2 )
                        strBounceAudioSource = "03";

                    AudioClip clip = AudioManager.Instance.GetClip("Misc/Bound_"+strBounceAudioSource);
                    if( clip != null )
                        AudioManager.Instance.PlaySound(clip,false);

					m_ballState = BallState.eLoseBall;

					m_bGoal = false;
					
					if (onHitGround != null)
						onHitGround(this);

					_CalcReboundPlacementPos(ret.vecVel, ret.vecPos);
				}
			}
			return;
		}

		if( m_owner != null )
		{
			m_fTime = IM.Number.zero;
			return;
		}

		m_bReachMaxHeight = m_fTime > (m_shootSolution.m_vInitVel.y / -IM.Vector3.gravity.y); 

		IM.Vector3 newPos;
		BallEvent eventId;
		Vector3 vecDelta;

		int iIter = 0;

		while( m_shootSolution.GetEvent(out eventId, out vecDelta, this, scene.mBasket, m_fPrevTime, m_fTime, iIter) )
		{
			iIter++;
			//Logger.Log(eventId);

			if( eventId == BallEvent.eGoal )
			{
				m_bGoal = true;
				onShootGoal(this);

				if( m_castedSkill != null && m_actor != null )
					m_actor.mStatistics.SkillUsageSuccess(m_castedSkill.skill.id, true);

				Logger.Log("Goal.");
				break;
			}
			else if( eventId == BallEvent.eCollBoard )
			{
				if( onBackboardCollision != null ) onBackboardCollision(this);

				m_vRotateAxis = Vector3.Cross(vecDelta, Vector3.up);
				m_fAngleSpeed = (float)m_shootSolution.m_vInitVel.magnitude;
				//Logger.Log("rebound: collide on board");
				break;
			}
			else if( eventId == BallEvent.eCollRim )
			{
				m_collidedWithRim = true;

				if( onRimCollision != null ) onRimCollision(this);

				m_vRotateAxis = Vector3.Cross(vecDelta, Vector3.up);
				m_fAngleSpeed = (float)m_shootSolution.m_vInitVel.magnitude;
				//Logger.Log("rebound: collide on rim");
				break;
			}
		}

		if(m_shootSolution.GetPosition(m_fTime, out newPos))
		{
			_position = newPos;
		}
		else
		{
			m_fTime 	-= m_shootSolution.m_fTime;
			initPos 	= m_shootSolution.m_vFinPos;
			initVel 	= m_shootSolution.m_vFinVel;

			if( m_shootSolution.m_bSuccess )
			{
				initVel *= new IM.Number(0, 200);
				//m_bGoal = true;
				//if( onShootGoal != null ) onShootGoal(this);
				m_ballState = BallState.eLoseBall;
			}
			else
			{
				m_ballState = BallState.eRebound;
				m_bGoal = false;
				if( onRebound != null ) onRebound(this);
			}
			m_shootSolution = null;
			_CalcReboundPlacementPos(initVel, initPos);
		}
	}

	void _PassToTarget(IM.Vector3 target, IM.Number deltaTime)
	{
		m_verticalVel += IM.Vector3.gravity.y * deltaTime;
		m_verticalVel = IM.Number.zero;

		IM.Vector3 dir = GameUtils.HorizonalNormalized(target, position);
		IM.Vector3 vel = new IM.Vector3( dir.x * m_actor.m_speedPassBall, m_verticalVel, dir.z * m_actor.m_speedPassBall );

		_position += vel * deltaTime;
	}
	
	void LateUpdate()
	{
		if( GameSystem.Instance.mClient == null )
			return;

        //用逻辑位置同步显示位置（球在人手上的时候，不以逻辑层位置同步显示层）
        if (_owner == null)
        {
            Vector3 curPos = transform.position;
            Vector3 targetPos = (Vector3)position;
            float speed = 10f;
            if ((m_ballState == BallState.eLoseBall || m_ballState == BallState.eRebound) && m_loseBallSimulator != null)
                speed = (float)curVel.magnitude;
            else if (m_ballState == BallState.eUseBall_Pass)
                speed = (float)m_passer.m_speedPassBall;
            else if (m_ballState == BallState.eUseBall_Shoot && m_shootSolution != null)
                speed = (float)m_shootSolution.m_vInitVel.magnitude;
            transform.position = Vector3.MoveTowards(curPos, targetPos, speed * Time.deltaTime);
            /*
            Logger.Log(string.Format("Set ball view pos: {0} -> {1} targetPos:{2} speed:{3}",
                curPos.ToString("F3"), transform.position.ToString("F3"), targetPos.ToString("F3"), speed.ToString("F3")));
            //*/
        }
        else
            _position = _owner.ballSocketPos;

		if( m_ballState != BallState.eUseBall && m_ballState != BallState.eNone )
			transform.Rotate(m_vRotateAxis, m_fAngleSpeed * Time.deltaTime * 150f, Space.World);

		Vector3 vBallPos = transform.position;
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match != null && match.mCurScene.mGround != null )
		{
			m_shadow.position = new Vector3(vBallPos.x, (float)match.mCurScene.mGround.mCenter.y + 0.04f, vBallPos.z);
			m_shadow.forward = Vector3.up;
		}

        //transform.FindChild("Logic").position = (Vector3)position;
	}
}