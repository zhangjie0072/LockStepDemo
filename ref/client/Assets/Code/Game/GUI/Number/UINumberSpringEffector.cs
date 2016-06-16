using UnityEngine;
using System.Collections;

public class UINumberSpringEffector : UINumberEffector
{
	/*
	public float m_lifeTime;
	public float m_beginDisappearTime;
	public float m_gravity = 9.8f;
	*/
	public Vector3 m_numberWorldPos = Vector3.zero;
	
	public float m_globalNumberSizeAdjust = 1.0f;
	
	public AnimationCurve m_colorRedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 1f) });
	public AnimationCurve m_colorGreenCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 1f) });
	public AnimationCurve m_colorBlueCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 1f) });
	public AnimationCurve m_colorAlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	
	public AnimationCurve m_scaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 40f) });
	
	public AnimationCurve m_offsetXCurve = new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	public AnimationCurve m_offsetYCurve = new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	
	//private float m_animatedAlphaValue = 1.0f;
	//private Vector2 m_curSpeed = Vector2.zero;
	private UIPopupElement m_guiNumberComp;
	//private float m_DisappearDuration = 0.0f;
	private float m_eclipseTime = 0.0f;
	
	private Camera m_UICamera;
	
	private float m_fLifeTime = 1.0f;
	private int m_iDirection = 1;
	private Vector3 m_spawnOffset = Vector3.zero;
	
	// Use this for initialization
	public UINumberSpringEffector ( GameObject target, EffectDoneListener listener ,Vector3 numberWorldSpawnOffset, float fRamdonSpawnRange, int iDirection )
		:base(target,listener)
	{
		m_guiNumberComp = m_effectNumber.GetComponent<UIPopupElement> ();
		
		m_UICamera = NGUITools.FindCameraForLayer( LayerMask.NameToLayer ("GUI") );
		
		//m_curSpeed = initSpeed;
		m_spawnOffset = new Vector3(Random.Range(0,fRamdonSpawnRange) + numberWorldSpawnOffset.x, Random.Range(0,fRamdonSpawnRange) + numberWorldSpawnOffset.y,0f);
		m_iDirection = iDirection;
	}
	
	override public void Update ()
	{
		if (m_guiNumberComp == null)
			return;
	
		m_eclipseTime += Time.deltaTime;
		
		Keyframe[] offsetsX = m_offsetXCurve.keys;
		Keyframe[] offsetsY = m_offsetXCurve.keys;
		
		Keyframe[] redColor		= m_colorRedCurve.keys;
		Keyframe[] blueColor 	= m_colorBlueCurve.keys;
		Keyframe[] greenColor	= m_colorGreenCurve.keys;
		Keyframe[] alphasColor  = m_colorAlphaCurve.keys;
		
		Keyframe[] scales = m_scaleCurve.keys;
		
		float offsetXEnd = offsetsX[offsetsX.Length - 1].time;
		float offsetYEnd = offsetsY[offsetsY.Length - 1].time;
		
		float scalesEnd = scales[scales.Length - 1].time;
		float RedEnd 	= redColor[redColor.Length - 1].time;
		float BlueEnd 	= blueColor[blueColor.Length - 1].time;
		float GreenEnd 	= greenColor[greenColor.Length - 1].time;
		float AlphaEnd = alphasColor[alphasColor.Length - 1].time;
		
		m_fLifeTime = Mathf.Max(scalesEnd, Mathf.Max(Mathf.Max(offsetXEnd,offsetYEnd),AlphaEnd));
		m_fLifeTime = Mathf.Max(RedEnd, m_fLifeTime);
		m_fLifeTime = Mathf.Max(BlueEnd, m_fLifeTime);
		m_fLifeTime = Mathf.Max(GreenEnd, m_fLifeTime);
		
		_ApplySpringEffect ();
		
		//handle finish
		if( m_eclipseTime > m_fLifeTime )
			m_listener.onEffectDone(this);
	}
	
	void _ApplySpringEffect ()
	{
		const float REF_SEC_PER_FRAME = 0.033f;
		
		float fFrameEllaps = Time.deltaTime / REF_SEC_PER_FRAME;
		
		float fOffsetX = m_offsetXCurve.Evaluate(m_eclipseTime);
		float fOffsetY = m_offsetYCurve.Evaluate(m_eclipseTime);
	
		float fScale = m_scaleCurve.Evaluate(m_eclipseTime) * m_globalNumberSizeAdjust;
		
		float fColorRed 	 = m_colorRedCurve.Evaluate(m_eclipseTime);
		float fColorGreen 	 = m_colorGreenCurve.Evaluate(m_eclipseTime);
		float fColorBlue	 = m_colorBlueCurve.Evaluate(m_eclipseTime);
		float fColorAlpha 	 = m_colorAlphaCurve.Evaluate(m_eclipseTime);
		
		m_numberWorldPos.x += fOffsetX * m_iDirection * fFrameEllaps;
		m_numberWorldPos.y += fOffsetY * fFrameEllaps;
		
		//transform to NGUI space
		Vector3 pos = Camera.main.WorldToViewportPoint( m_numberWorldPos + m_spawnOffset );
		//bool isVisible = ( pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f) ;
		bool isVisible = true;
		// If visible, update the position
		if (isVisible)
		{
			m_effectNumber.transform.position = m_UICamera.ViewportToWorldPoint(pos);
			m_effectNumber.transform.localScale = new Vector3(fScale,fScale,fScale);
				
			m_guiNumberComp.SetColor( new Color(fColorRed,fColorGreen,fColorBlue,fColorAlpha) );
			
			pos = m_effectNumber.transform.localPosition;
			pos.x = Mathf.FloorToInt(pos.x);
			pos.y = Mathf.FloorToInt(pos.y);
			pos.z = 0f;
			
			m_effectNumber.transform.localPosition = pos;
		}
		else
			m_listener.onEffectDone(this);
	}
	
	/*
	void _ApplySpringEffect ()
	{		
		m_curSpeed.y -= m_gravity * Time.deltaTime;
		m_numberWorldPos.x += m_curSpeed.x * Time.deltaTime;
		m_numberWorldPos.y += m_curSpeed.y * Time.deltaTime;
		
		Vector3 pos = Camera.mainCamera.WorldToViewportPoint( m_numberWorldPos );
		bool isVisible = (Camera.mainCamera.isOrthoGraphic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f) ;
		// If visible, update the position
		if (isVisible)
		{
			m_effectNumber.transform.position = m_UICamera.ViewportToWorldPoint(pos);
			
			pos = m_effectNumber.transform.localPosition;
			pos.x = Mathf.FloorToInt(pos.x);
			pos.y = Mathf.FloorToInt(pos.y);
			pos.z = 0f;
			m_effectNumber.transform.localPosition = pos;
		}
		else
			m_listener.onEffectDone(this);

		if (m_eclipseTime > m_beginDisappearTime) {
			if (Mathf.Equals (m_DisappearDuration, 0.0f))
				m_DisappearDuration = m_lifeTime - m_beginDisappearTime;
			
			m_animatedAlphaValue -= Time.deltaTime / m_DisappearDuration;
		}
		
		m_guiNumberComp.SetAlpha (m_animatedAlphaValue);
	}
	*/
	
	
	
}
