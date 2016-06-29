using UnityEngine;
using System.Collections;

public class UINumberBeatEffector : UINumberEffector
{	
	public float m_sizeMultiplier = 1.0f;
	
	private UINumber m_guiNumberComp;
	AnimationCurve m_Curve;
	float m_fCurveEndTime = 0;
	float m_fTime = 0;
	
	// Use this for initialization
	public UINumberBeatEffector (GameObject target, EffectDoneListener listener, AnimationCurve curve )
		:base(target,listener)
	{
		if (target == null)
			Debug.LogError ("GUINumberBeatEffector: target is null ");
		
		m_Curve = curve;
		m_fCurveEndTime = m_Curve.keys[m_Curve.length - 1].time;
	}
	
	override public void Update ()
	{
		m_fTime += Time.deltaTime;
		if(m_Curve != null ){
			if( m_fTime >= m_fCurveEndTime ){
				m_listener.onEffectDone (this);
				return;
			}
			
			float fSize = m_Curve.Evaluate(m_fTime) * m_sizeMultiplier;
			m_effectNumber.transform.localScale = new Vector3(fSize, fSize, 1);
		}
	}
	
}
