using System;
using System.Collections.Generic;
using UnityEngine;

public class UTimeCounter
	:MonoBehaviour
{
	public interface Listener
	{
		void OnTimeUp();
	}
	private List<Listener>	m_listeners = new List<Listener>();

	public float 		time;
	public bool			stop = true;
	
	private UISprite	m1, m2, s1, s2;
	private UISprite	dot, colon;

	public void AddListener(Listener newLsn)
	{
		foreach( Listener lsn in m_listeners )
		{
			if( lsn == newLsn )
				return;
		}

		m_listeners.Add(newLsn);
	}
	public void RemoveAllListeners()
	{
		m_listeners.Clear();
	}

	void Awake()
	{
		Transform trans;
		trans = GameUtils.FindChildRecursive(transform, "M1");
		if( trans != null )
			m1 = trans.GetComponent<UISprite>();
		
		trans = GameUtils.FindChildRecursive(transform, "M2");
		if( trans != null )
			m2 = trans.GetComponent<UISprite>();
		
		trans = GameUtils.FindChildRecursive(transform, "S1");
		if( trans != null )
			s1 = trans.GetComponent<UISprite>();
		
		trans = GameUtils.FindChildRecursive(transform, "S2");
		if( trans != null )
			s2 = trans.GetComponent<UISprite>();

		trans = GameUtils.FindChildRecursive(transform, "dot");
		if( trans != null )
			dot = trans.GetComponent<UISprite>();

		trans = GameUtils.FindChildRecursive(transform, "colon");
		if( trans != null )
			colon = trans.GetComponent<UISprite>();
	}
	
	void FixedUpdate()
	{
		if( !stop && time > 0.0f)
		{
			time -= Time.fixedDeltaTime;
			if( time <= 0.0f )
			{
				stop = true;
				m_listeners.ForEach(delegate(Listener lsn){
					lsn.OnTimeUp();
				});
			}
		}

		_UpdateUIByTime( time );
	}
	
	void _UpdateUIByTime( float time )
	{
		int sec = Mathf.Max((int)time % 60, 0);
		int miniSec = Mathf.Max( (int)((time - (int)time) * 100.0f), 0);
		int min = Mathf.Max((int)time / 60, 0);

		int iM1, iM2, iS1, iS2 = 0;
		if( min == 0 && sec < 10 )
		{
			iM1 = 0;
			iM2 = sec;
			iS1 = miniSec / 10;
			iS2 = miniSec % 10;

			if( m1 != null )
				m1.color = Color.red;
			if( m2 != null )
				m2.color = Color.red;
			if( s1 != null )
			{
				s1.color = Color.red;
				s1.transform.localScale = Vector3.one * 0.7f;
			}
			if( s2 != null )
			{
				s2.color = Color.red;
				s2.transform.localScale = Vector3.one * 0.7f;
			}

			if( dot != null )
			{
				dot.gameObject.SetActive(true);
				dot.color = Color.red;
			}
			if( colon != null )
			{
				colon.gameObject.SetActive(false);
				colon.color = Color.red;
			}
		}
		else
		{
			iM1 = min / 10;
			iM2 = min % 10;
			iS1 = sec / 10;
			iS2 = sec % 10;

			if( m1 != null )
				m1.color = Color.white;
			if( m2 != null )
				m2.color = Color.white;
			if( s1 != null )
			{
				s1.color = Color.white;
				s1.transform.localScale = Vector3.one;
			}

			if( s2 != null )
			{
				s2.color = Color.white;
				s2.transform.localScale = Vector3.one;
			}

			if( dot != null )
			{
				dot.gameObject.SetActive(false);
				dot.color = Color.white;
				dot.MakePixelPerfect();
			}
			if( colon != null )
			{
				colon.gameObject.SetActive(true);
				colon.color = Color.white;
				colon.MakePixelPerfect();
			}
		}

		if( m1 != null )
		{
			m1.spriteName = "gameInterface_figure_white" + Convert.ToString(iM1);
			m1.MakePixelPerfect();
		}
		
		if( m2 != null )
		{
			m2.spriteName = "gameInterface_figure_white" + Convert.ToString(iM2);
			m2.MakePixelPerfect();
		}
		
		if( s1 != null )
		{
			s1.spriteName = "gameInterface_figure_white" + Convert.ToString(iS1);
			s1.MakePixelPerfect();
		}
		
		if( s2 != null )
		{
			s2.spriteName = "gameInterface_figure_white" + Convert.ToString(iS2);
			s2.MakePixelPerfect();
		}
	}
}


