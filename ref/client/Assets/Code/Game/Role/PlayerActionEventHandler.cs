using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PlayerActionEventHandler
{
	
	public enum AnimEvent
	{
		ePrepareToShoot,
		eShoot,
		ePickupBall,
		eRebound,
		ePass,
		eCatch,
		ePoseGoal,
		eRequireBall,
		eDunk,
		eStolen,
		eSteal,
		eLayup,
		eBlock,
		eCrossOver,
		eCollided,
		eIntercepted,
	}
	public interface Listener
	{
		void OnEvent( AnimEvent animEvent, Player sender, System.Object context = null );
	}
	private List<Listener>	m_listeners = new List<Listener>();

	private Player _owner;

	public AudioSource 	m_Audio;
	
	public PlayerActionEventHandler(Player owner) {
        this._owner = owner;
        //InitComponents();
	}

	virtual public void InitComponents() {
        //m_Audio = GetComponent<AudioSource>();
        //if( m_Audio == null )	
        //{
        //    m_Audio = gameObject.AddComponent<AudioSource>();
        //    m_Audio.maxDistance = 30.0f;
        //}
	}
	
	public void AddEventListener( Listener lsn )
	{
		if( lsn != null )
			m_listeners.Add(lsn);
	}

	public void RemoveEventListener( Listener lsn )
	{
		if (lsn != null)
			m_listeners.Remove(lsn);
	}

	public void NotifyAllListeners( AnimEvent animEvent, System.Object context = null )
	{
		m_listeners.ForEach(delegate(Listener lsn){
			lsn.OnEvent(animEvent, _owner, context);
		}
		);
	}

	public void OnKnockedRecover()
	{
		PlayerState_Knocked knocked = _owner.m_StateMachine.m_curState as PlayerState_Knocked;
		if( knocked == null )
			return;
		knocked.m_bKnockedRecover = true;
	}

	public void OnShoot(string param)
	{
		if( _owner == null || !_owner.m_bWithBall )
			return;
		PlayerState_Shoot shootState = _owner.m_StateMachine.m_curState as PlayerState_Shoot;
		if( shootState != null )
			shootState.OnShoot();
	}

	public void OnLayupShot(string param)
	{
		if( _owner == null || !_owner.m_bWithBall )
			return;
		PlayerState_Layup layupState = _owner.m_StateMachine.m_curState as PlayerState_Layup;
		if( layupState == null )
			return;
		layupState.OnLayup();
	}

	public void OnRebound()
	{
		PlayerState_Rebound rebound = _owner.m_StateMachine.m_curState as PlayerState_Rebound;
		if( rebound == null )
			return;
		rebound.OnRebound();
	}
	
	public void OnFoot()
	{
		/*
		AudioClip clip = AudioManager.Instance.GetClip("Misc/Run_01");
		if( clip != null )
			AudioManager.Instance.PlaySound(clip, false, 0.2f);
			*/
	}
	
	public void OnBallGrounded()
	{
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.BallOnGround);
	}
	
	public void OnGround()
	{
		_owner.m_bOnGround = true;
		_owner.m_StateMachine.m_curState.OnGround();
	}
	
	public void OnLeaveGround()
	{
		_owner.m_bOnGround = false;
		_owner.m_StateMachine.m_curState.OnLeaveGround();
	}

    public void OnBeginShoot(string param)
    {
        PlayerState_Shoot state = _owner.m_StateMachine.m_curState as PlayerState_Shoot;
        if (state != null)
            state.BeginShoot(uint.Parse(param));
    }

	/*
	public void OnGetUp()
	{
		PlayerState_FallGround fallGround = m_Owner.m_StateMachine.m_curState as PlayerState_FallGround;
		if( fallGround != null )
			fallGround.m_step = PlayerState_FallGround.Step.riseUp;
	}
	*/

	public void OnSteal()
	{
		PlayerState_Steal steal = _owner.m_StateMachine.m_curState as PlayerState_Steal;
		if( steal != null )
			steal.OnSteal();
	}
	
	public void OnPickUp()
	{
		/*
		UBasketball ball = m_Owner.m_pickupDetector.ballToPickup;
		if (ball == null)
			return;

		if( ball.m_owner != null )
			return;

		PlayerState_Pickup pickupState = m_Owner.m_StateMachine.GetState(PlayerState.State.ePickup) as PlayerState_Pickup;
		if( pickupState == null || !pickupState.m_bSuccess )
			return;

		Debug.Log( m_Owner.m_id + " get ball." );
		m_Owner.GrabBall(ball);

		m_Owner.m_pickupDetector.ballToPickup = null;

		m_listeners.ForEach(delegate(Listener lsn){
			lsn.OnEvent(AnimEvent.ePickupBall, m_Owner);
		}
		);
		
		AudioClip clip = AudioManager.Instance.GetClip("Misc/Catch_01");
		if( clip != null )
			AudioManager.Instance.PlaySound(clip);
			*/
	}

	public void OnDunk()
	{
		PlayerState_Dunk dunkState = _owner.m_StateMachine.m_curState as PlayerState_Dunk;
		if( dunkState != null )
			dunkState.OnDunk();
	}
	
	public void OnLoseBall()
	{
	}

	public void OnBlock()
	{
		PlayerState_Block blockState = _owner.m_StateMachine.m_curState as PlayerState_Block;
		if( blockState != null )
			blockState.OnBlock();
	}

	public void OnStep()
	{
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.FootStep);
	}

	public void OnPrepareToShoot()
	{
		PlayerState ps = _owner.m_StateMachine.m_curState;
		if( ps.m_eState != PlayerState.State.ePrepareToShoot )
			return;

		PlayerState_PrepareToShoot preparation = ps as PlayerState_PrepareToShoot;
		preparation.OnPrepareToShoot();

		m_listeners.ForEach(delegate(Listener lsn){
			lsn.OnEvent(AnimEvent.ePrepareToShoot, _owner);
		}
		);
	}
	
	public void OnPassBall()
	{
		PlayerState_Pass passState = _owner.m_StateMachine.m_curState as PlayerState_Pass;
		if( passState == null )
			return;
		passState.OnPass();
	}

	public void OnCatchBall()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		PlayerState curState = _owner.m_StateMachine.m_curState;
		if( curState.m_eState == PlayerState.State.eCatch )
		{
			PlayerState_Catch catchState = curState as PlayerState_Catch;
			catchState.OnCatch(ball);
		}
		/*
		else if( curState.m_eState == PlayerState.State.eBodyThrowCatch )
		{
			PlayerState_BodyThrowCatch catchState = curState as PlayerState_BodyThrowCatch;
			catchState.OnCatch(ball);
		}
		*/
	}
	
	public void OnRequireBall()
	{
		m_listeners.ForEach(delegate(Listener lsn){
			lsn.OnEvent(AnimEvent.eRequireBall, _owner);
			}
			);
	}
	
	public void OnHipGrounded()
	{
		GameObject groundDownEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/GroundDown");
		if( groundDownEffect != null )
		{
			GameObject goGroundDownEffect = GameObject.Instantiate(groundDownEffect) as GameObject;
            goGroundDownEffect.transform.position = (Vector3)_owner.position;
			goGroundDownEffect.GetComponent<ParticleSystem>().Play();
		}

		PlayerState_FallLostBall lostBall = _owner.m_StateMachine.m_curState as PlayerState_FallLostBall;
		if(lostBall == null)
			return;
		lostBall.m_step = PlayerState_FallLostBall.Step.eHipGrounded;
	}

	public void OnFaceToBasket()
	{
		_owner.m_StateMachine.m_curState.OnFaceToBasket();
	}

	public void BeginMove(string param)
	{
		PlayerState state = _owner.m_StateMachine.m_curState;

		Dictionary<string,string> dictParam = AnimConfigItem.ParseParam(param);
		string strSpeedValue, strAccelValue;
		if( dictParam.TryGetValue("Speed", out strSpeedValue) )
			state.m_speed = AnimConfigItem.ParseVector3(strSpeedValue);
		if( dictParam.TryGetValue("Accel", out strAccelValue) )
			state.m_accelerate = AnimConfigItem.ParseVector3(strAccelValue);
	}

	public void EndMove()
	{
		PlayerState state = _owner.m_StateMachine.m_curState;
		state.m_speed = IM.Vector3.zero;
	}

	public void SwitchHand()
	{
		if( _owner.m_eHandWithBall == Player.HandWithBall.eLeft )
			_owner.m_eHandWithBall = Player.HandWithBall.eRight;
		else if( _owner.m_eHandWithBall == Player.HandWithBall.eRight )
			_owner.m_eHandWithBall = Player.HandWithBall.eLeft;
	}

	public void OnLostBall()
	{
		PlayerState_Stolen stolenState = _owner.m_StateMachine.m_curState as PlayerState_Stolen;
		if( stolenState != null )
			stolenState.OnLostBall();
		else
			_owner.OnLostBall();
	}

	public void BeginRotate(string param)
	{
		PlayerState state = _owner.m_StateMachine.m_curState;
		
		Dictionary<string,string> dictParam = AnimConfigItem.ParseParam(param);
		if( dictParam["Type"] == "direct" )
			state.m_rotateType = RotateType.eDirect;
		else if( dictParam["Type"] == "smooth" )
			state.m_rotateType = RotateType.eSmooth;

		if( dictParam["To"] == "basket" )
			state.m_rotateTo = RotateTo.eBasket;
		else if( dictParam["To"] == "defenser" )
			state.m_rotateTo = RotateTo.eDefenseTarget;
		else if( dictParam["To"] == "ball" )
			state.m_rotateTo = RotateTo.eBall;

		string strSpeed;
		if( dictParam.TryGetValue("Speed", out strSpeed) )
			state.m_turningSpeed = IM.Number.Parse(strSpeed);
	}
	
	public void EndRotate()
	{
		PlayerState state = _owner.m_StateMachine.m_curState;
		state.m_rotateTo = RotateTo.eNone;
		state.m_turningSpeed = IM.Number.zero;
	}

	public void GrabBall()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		if( ball.m_owner != null )
			ball.m_owner.DropBall(ball);

		_owner.GrabBall(ball);
	}

	public void ActionBegin(string param)
	{
		/*
		Dictionary<string,string> dictParam = AnimConfigItem.ParseParam(param);
		string strId;
		if( !dictParam.TryGetValue("ID", out strId) )
			return;
		int id = int.Parse(strId);
		PlayerState state = m_Owner.m_StateMachine.m_curState;
		if( !state.m_lstActionId.Contains(id) )
			state.m_lstActionId.Add(id);
			*/
	}

	public void ActionEnd(string param)
	{
		/*
		Dictionary<string,string> dictParam = AnimConfigItem.ParseParam(param);
		string strId;
		if( !dictParam.TryGetValue("ID", out strId) )
			return;
		int id = int.Parse(strId);
		PlayerState state = m_Owner.m_StateMachine.m_curState;
		if( state.m_lstActionId.Contains(id) )
			state.m_lstActionId.Remove(id);
			*/
	}
}