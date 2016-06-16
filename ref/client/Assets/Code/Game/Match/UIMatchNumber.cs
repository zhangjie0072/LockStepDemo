using System;

using UnityEngine;
using System.Collections;

public class UIMatchNumber
	: MonoBehaviour, UINumberEffector.EffectDoneListener
{
	public float m_SpringNumberIntervalAdjustment = 1.0f;
	
	public Vector2 m_SpringNumberPosOffset = Vector2.zero;
	public float m_SpringNumberSize = 1.0f;
	
	public Vector2 m_SpringNumberWorldSpawnOffset = Vector2.zero;
	public float m_SpringRamdonSpawnRange = 5.0f;
	
	public AnimationCurve m_springEffectorOffsetX = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 40f) });
	public AnimationCurve m_springEffectorOffsetY = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 40f) });
	public AnimationCurve m_springEffectorScale = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.25f, 1f) });
	
	public AnimationCurve m_springEffectorRed	= new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	public AnimationCurve m_springEffectorGreen	= new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	public AnimationCurve m_springEffectorBlue	= new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	
	public AnimationCurve m_springEffectorAlpha	= new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });
	
	
	public MonoBehaviour m_pfNumAtlas;
	public float m_fCriticalSizeMul = 1.1f;
	public GameObject m_resNumber;
	
	private GameObject m_NumberRoot;
	
	private ArrayList  m_SpringNumbers;
	
	private int		m_iDepth = 1000;
	
	// Use this for initialization
	void Start ()
	{
		m_NumberRoot = GameObject.FindGameObjectWithTag ("Number");
		if (m_NumberRoot == null)
			Logger.LogError ("UI Root is null");
		
		if( m_resNumber == null ){
            m_resNumber = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Number") as GameObject;
			if (m_resNumber == null)
				Logger.LogError ("Can not find the number resource");
		}
		
		m_SpringNumbers = new ArrayList ();
		
	}

	class NumberCompare : IComparer
	{
		public int Compare (object x, object y)
		{
			GameObject lhs = x as GameObject;
			GameObject rhs = y as GameObject;
			if ( lhs == rhs )
			{
				return 0;
			}
			return -1;
		}
	};
	
	public void onEffectDone (UINumberEffector effector)
	{
		GameObject number = effector.GetEffectNumber ();
		{
			for( int iIdx = 0; iIdx != m_SpringNumbers.Count; iIdx++ )
			{
				if ( number != m_SpringNumbers[iIdx] as GameObject )
					continue;
				
				m_SpringNumbers.Remove (number);
				Destroy (number);
				
				break;
			}
		}
	}

	void Update ()
	{
		for( int iIdx = m_SpringNumbers.Count - 1; iIdx >= 0; iIdx-- )
		{
			GameObject number = m_SpringNumbers[iIdx] as GameObject;
			if( number != null )
			{
				UIPopupElement go = number.GetComponent<UIPopupElement> ();
				go.m_effector.Update ();
			}
		}
		
	}
	
	void LateUpdate()
	{
		
	}
	
	public void DrawUINumberSpring ( Vector3 vWorldPos, int iNum, int iDirection )
	{
		GameObject goPopup_elem = _SpawnUINumber (m_resNumber, iNum );
		goPopup_elem.transform.localScale = Vector3.one * m_SpringNumberSize;
		goPopup_elem.transform.parent = m_NumberRoot.transform;
		UIPopupElement popup_elem = goPopup_elem.GetComponent<UIPopupElement> ();
		
		if( popup_elem == null )return;
		
		UINumber guiNum = popup_elem as UINumber;
		if( guiNum ){
			guiNum.m_fIntervalAdjust = m_SpringNumberIntervalAdjustment;
		}
		popup_elem.m_effector = new UINumberSpringEffector( goPopup_elem, this, m_SpringNumberWorldSpawnOffset, m_SpringRamdonSpawnRange, iDirection);
		UINumberSpringEffector se = popup_elem.m_effector as UINumberSpringEffector;
		//se.m_alpha = m_springEffectorAlpha;
		//se.m_position = m_springEffectorPosition;
		//se.m_scale = m_springEffectorScale;
		se.m_offsetXCurve = m_springEffectorOffsetX;
		se.m_offsetYCurve = m_springEffectorOffsetY;
		
		se.m_colorRedCurve   = m_springEffectorRed;	
		se.m_colorGreenCurve = m_springEffectorGreen;	
		se.m_colorBlueCurve  = m_springEffectorBlue;	
		se.m_colorAlphaCurve = m_springEffectorAlpha;
		
		se.m_scaleCurve = m_springEffectorScale;
		
		se.m_globalNumberSizeAdjust = m_SpringNumberSize;
		
		Vector3 targetWPos = vWorldPos;
		targetWPos.x += m_SpringNumberPosOffset.x;
		targetWPos.y += m_SpringNumberPosOffset.y;
		targetWPos.z = 0;
		
		se.m_numberWorldPos = targetWPos;
		
		m_SpringNumbers.Add (goPopup_elem);
	}
	
	
	GameObject _SpawnUINumber ( GameObject res, int iNum )
	{
		GameObject guiNumber = Instantiate (res) as GameObject;
		
		guiNumber.transform.parent = m_NumberRoot.transform;
		guiNumber.transform.localPosition = Vector3.zero;
		guiNumber.transform.localScale = Vector3.one;
		guiNumber.layer = LayerMask.NameToLayer ("GUI");
		
		UINumber numberComp = guiNumber.GetComponent<UINumber> ();
		if( numberComp ){
			numberComp.m_alignMode = UINumber.Alignment.center;
			numberComp.m_value = Mathf.RoundToInt( iNum );
			numberComp.m_atlas = m_pfNumAtlas;
			
			numberComp.SetOrder(m_iDepth++);
		}
		
		m_iDepth %= 1000;
		m_iDepth += 1000;
		
		return guiNumber;
	}
}