using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UINumber : UIPopupElement
{
	public enum Alignment
	{
		left,
		right,
		center
	}
	
	public int m_value = 0;
	private int m_iLastValue = 0;
	
	public Alignment m_alignMode = Alignment.left;
	private Alignment m_lastAlignment = Alignment.left;
	
	public MonoBehaviour m_atlas;
	
	public float m_fIntervalAdjust = 1f;
	
	//TODO:optimsed by search
	private int m_uuid = 0;
	
	private List<UISprite> m_sprites = new List<UISprite>();
	
	private int m_depth = 0;
	private int m_depthSectionCount = 20;
	
	
	// Use this for initialization
	void Start ()
	{
		//load default number atlas
		if (m_atlas == null)
            m_atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/NumberUI");
		
		if (m_atlas == null)
			Debug.LogError ("atlas can not be loaded.");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_value == m_iLastValue && m_alignMode == m_lastAlignment )
			return;
			
		if (!this.Validate ())
			return;
			
		m_iLastValue = m_value;
		m_lastAlignment = m_alignMode;
	
		Reset ();
		
		this.IntepretToNumber (m_value);
		
		this.Align(m_alignMode);
	}
	
	bool Validate ()
	{
		if (m_value < 0)
			return false;
		
		bool ret = false;
		//the parent must attached under UI Root
		Transform transX = transform.parent;
		while (transX != null) {
			if (transX.tag == "UIRoot" && transX.gameObject.layer == transform.gameObject.layer)
				ret = true;
			
			transX = transX.parent;
		}
		
		if (!ret)
			Debug.LogError ("GUINumber component owner must attached under UIRoot and layer must be the same");
		
		return ret;
	}
	
	void Reset ()
	{
		foreach( UISprite sp in m_sprites )
		{
			NGUITools.Destroy(sp.gameObject);
		}
		m_sprites.Clear();
	}
	
	void Align( Alignment alignment )
	{
		switch( alignment )
		{
		case Alignment.center:
			_AlignCenter();
			break;
		default:
			break;
		}
	}
	
	public void SetOrder( int depth )
	{
		m_depth = depth * m_depthSectionCount;
	}
	
	void _SetOrder(int iDepth)
	{
		foreach( UISprite sp in m_sprites )
			sp.depth = iDepth;
	}
	
	void _AlignCenter ()
	{
		float fTotalLength = 0.0f;
		for (int i = 0; i < transform.childCount; i++) {
			GameObject go = transform.GetChild (i).gameObject;
			UISprite sp = go.GetComponent<UISprite> ();
			if (sp == null)
				continue;
			
			fTotalLength += sp.transform.localScale.x;
		}
		
		for (int i = 0; i < transform.childCount; i++) {
			GameObject go = transform.GetChild (i).gameObject;
			go.transform.localPosition = new Vector3( go.transform.localPosition.x -0.5f*fTotalLength , go.transform.localPosition.y, go.transform.localPosition.z );
		}
	}
	
	public override void SetColor (Color color)
	{
		for (int i = 0; i < transform.childCount; i++) {
			GameObject go = transform.GetChild (i).gameObject;
			UISprite sp = go.GetComponent<UISprite> ();
			if (sp == null)
				continue;
			
			sp.color = color;
		}
	}
	
	void IntepretToNumber (int targetValue)
	{
		string strNumber = string.Format ("{0:d}", targetValue);
		float lastXPos = 0f;
		
		if( strNumber.Length > m_depthSectionCount )
			Debug.LogWarning("number value: " + strNumber.Length + "exceed: " + m_depthSectionCount);
		
		for (int idx = 0; idx != strNumber.Length; idx++) {
			int iValue = (int)char.GetNumericValue (strNumber, idx);
			//TODO:cache the sprite
			UISprite sprite = NGUITools.AddWidget<UISprite> (transform.gameObject);
			sprite.atlas = m_atlas as UIAtlas;
			sprite.spriteName = string.Format ("Time{0:d}", iValue);
			
			sprite.depth = m_depth + idx;
			sprite.MakePixelPerfect ();
			sprite.MarkAsChanged ();
			
			float fSize = sprite.transform.localScale.x;
			//cause we align center
			sprite.transform.localPosition = new Vector3 (lastXPos + fSize*0.5f, sprite.transform.localPosition.y, sprite.transform.localPosition.z);
			m_sprites.Add(sprite);
			
			lastXPos += fSize * m_fIntervalAdjust;
		}
	}
}
