using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BasketState
{
	eShootGoal 		= 1,
	eShootNoGoal 	= 2,
	eDunkGoal		= 3,
	eDunkNoGoal		= 4,
}

public class BasketStateEffect
{
	public	BasketState 	targetState;
	public	Object			m_resEffect;
	public	GameObject		m_goEffect;
	
	public BasketStateEffect(BasketState bs, Object	resEffect)
	{
		targetState = bs;
		m_resEffect = resEffect;
		m_goEffect = null;
	}
}

public class UBasket : MonoBehaviour
{
    /**篮板*/
	public class Backboard 
	{
		public IM.Vector3	center;
		public IM.Number	width;
		public IM.Number	height;
	}
    /**篮框*/
	public class Rim 
	{
		public IM.Vector3	center;
		public IM.Number	radius;
	}

	public Backboard	m_backboard{ get; private set;}
	public Rim			m_rim{ get; private set;}

	public IM.Vector3	m_vShootTarget{ get{ return m_rim.center; } private set{} }

	public delegate void BasketEventDelegate(UBasket basket, UBasketball ball);
	public BasketEventDelegate onGoal;
	public BasketEventDelegate onNoGoal;
	public BasketEventDelegate onRimCollision;

	public delegate void BasketEventDunkDelegate(UBasket basket, UBasketball ball, bool goal);
	public BasketEventDunkDelegate onDunk;

	//public delegate void OnBackboardCollision();
	//public delegate void OnRimCollision();

	private ParticleSystem m_collisionSpark;

	private List<BasketStateEffect> m_stateEffects = new List<BasketStateEffect>();

	private BasketState		m_state;

	public void Build(IM.Vector3 basketPos)
	{
		ResourceLoadManager.Instance.LoadPrefab("Prefab/Effect/E_Spark1");

        //逻辑数据
		m_backboard = new Backboard();
		m_backboard.width  = new IM.Number(1, 900);
		m_backboard.height = new IM.Number(1, 170);
        m_backboard.center = basketPos + new IM.Vector3(IM.Number.zero, new IM.Number(3, 535), -new IM.Number(0, 300));

		m_rim = new Rim();
		m_rim.radius = new IM.Number(0, 425) * IM.Number.half;
		m_rim.center = basketPos + new IM.Vector3(IM.Number.zero, new IM.Number(3,100), IM.Number.zero);
	}

	public void SetEffect( BasketState state, Object resEffect )
	{
		m_stateEffects.Add( new BasketStateEffect(state, resEffect) );
	}

	void _UpdateEffect()
	{
		if( m_stateEffects.Count == 0 )
			return;
		if( m_state == BasketState.eDunkNoGoal || m_state == BasketState.eShootNoGoal )
		{
			m_stateEffects.ForEach( (BasketStateEffect effect)=>
			                       {
				if( effect.m_goEffect != null )
					Destroy(effect.m_goEffect);
			});
			m_stateEffects.Clear();
			return;
		}

		m_stateEffects.ForEach( (BasketStateEffect effect)=>
		                       {
			if( m_state == effect.targetState )
			{
				GameObject goEffect = GameObject.Instantiate(effect.m_resEffect) as GameObject;
				goEffect.AddComponent<UEffectSelfDestroy>();
				Transform root = GameUtils.FindChildRecursive(transform, "root");
				if( root != null )
				{
					goEffect.transform.parent = root;
					goEffect.transform.localPosition = Vector3.zero;
					goEffect.transform.localRotation = Quaternion.identity;
					goEffect.transform.localScale = Vector3.one;
				}
				if( goEffect.GetComponent<ParticleSystem>() != null )
					goEffect.GetComponent<ParticleSystem>().loop = false;
				goEffect.SetActive(true);
				effect.m_goEffect = goEffect;
			}
		});
		m_stateEffects.Clear();
	}

	public void OnGoal(UBasketball ball)
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play("shootGoal");

		m_state = BasketState.eShootGoal;
		_UpdateEffect();

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Goal);
		if(onGoal != null) onGoal(this, ball);
	}

	public void OnNoGoal(UBasketball ball)
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play("shootNoGoal");

		m_state = BasketState.eShootNoGoal;
		_UpdateEffect();

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.ShootNoGoal);
        if (GameSystem.Instance.mClient.mInputManager.isNGDS)
        {
            //设置马达震动
            GameSystem.Instance.mClient.mInputManager.sendShock = true;
        }

		if(onNoGoal != null) onNoGoal(this, ball);
	}

	public void OnBackboardCollision(UBasketball ball)
	{
		AudioClip clip = AudioManager.Instance.GetClip("Misc/Rim_01");
		if( clip != null )
			AudioManager.Instance.PlaySound(clip);
        if (GameSystem.Instance.mClient.mInputManager.isNGDS)
        {
            //设置马达震动
            GameSystem.Instance.mClient.mInputManager.sendShock = true;
        }
	}

	public void OnRimCollision(UBasketball ball)
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play("shootNoGoal");

		AudioClip clip = AudioManager.Instance.GetClip("Misc/Rim_01");
		if( clip != null )
			AudioManager.Instance.PlaySound(clip);
        if (GameSystem.Instance.mClient.mInputManager.isNGDS)
        {
            //设置马达震动
            GameSystem.Instance.mClient.mInputManager.sendShock = true;
        }
		ShowCollisionSpark(ball);

		if( onRimCollision != null )
			onRimCollision(this, ball);
	}

	public void OnDunk(UBasketball ball, bool bGoal)
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play("grabDunk");

		m_state = bGoal ? BasketState.eDunkGoal : BasketState.eDunkNoGoal;
		_UpdateEffect();

        if (GameSystem.Instance.mClient.mInputManager.isNGDS)
        {
            //设置马达震动
            GameSystem.Instance.mClient.mInputManager.sendShock = true;
        }

		if (bGoal)
		{
			if (onGoal != null) 
				onGoal(this, ball);
		}
		else
		{
			if (onNoGoal != null) 
				onNoGoal(this, ball);
			PlaySoundManager.Instance.PlaySound(MatchSoundEvent.DunkNoGoal);
		}
	}

	void ShowCollisionSpark(UBasketball ball)
	{
		if (m_collisionSpark == null)
		{
            Object prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Effect/E_Spark1");
			m_collisionSpark = (Object.Instantiate(prefab) as GameObject).GetComponent<ParticleSystem>();
			m_collisionSpark.transform.parent = transform;
			m_collisionSpark.transform.localScale = Vector3.one;
			m_collisionSpark.transform.localRotation = Quaternion.identity;
		}

		IM.Vector3 dirRimToBall = ball.position - m_rim.center;
		dirRimToBall.Normalize();
		dirRimToBall.y = IM.Number.zero;
		IM.Vector3 sparkPos = m_rim.center + dirRimToBall * m_rim.radius;
		m_collisionSpark.transform.position = (Vector3)sparkPos;
		m_collisionSpark.Play();
	}
}