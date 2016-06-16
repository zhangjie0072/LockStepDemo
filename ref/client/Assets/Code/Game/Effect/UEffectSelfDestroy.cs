using UnityEngine;
using System.Collections.Generic;

public class UEffectSelfDestroy
	: MonoBehaviour
{
	public delegate void OnFinish ();
	public OnFinish onFinish;

	private List<ParticleSystem> m_ps = new List<ParticleSystem>();
    private GameUtils.Timer4View m_lifeTime;

	void Awake()
	{
		ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
		if( ps != null )
			m_ps.Add(ps);
		m_ps.AddRange( gameObject.GetComponentsInChildren<ParticleSystem>() );
	}

    public void SetDuration(float fDuration)
    {
        m_lifeTime = new GameUtils.Timer4View(fDuration, OnTimer, 1);
    }

	void OnTimer()
	{
		m_ps.ForEach( (ParticleSystem ps)=>{ ps.Stop(); } );
	}

	void FixedUpdate()
	{
		if( m_lifeTime != null )
			m_lifeTime.Update(Time.fixedDeltaTime);
	}

	void Update()
	{
		foreach( ParticleSystem ps in m_ps )
		{
			if( ps.isPlaying )
				return;
		}
		DestroyObject(gameObject);

		if( onFinish != null )
			onFinish();
	}
}