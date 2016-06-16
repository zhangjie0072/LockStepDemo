using UnityEngine;
using System.Collections;

public class UINumberEffector 
{
	public interface EffectDoneListener
	{
		void onEffectDone( UINumberEffector effector );
	};
	
	protected EffectDoneListener m_listener;
	protected GameObject m_effectNumber;
	
	// Use this for initialization
	public UINumberEffector ( GameObject target, EffectDoneListener listener )
	{
		m_effectNumber = target;
		m_listener = listener;
	}
	
	public GameObject GetEffectNumber()
	{
		return m_effectNumber;
	}
	
	public virtual void Update ()
	{
	}
	
	public virtual void LateUpdate ()
	{
	}
}
