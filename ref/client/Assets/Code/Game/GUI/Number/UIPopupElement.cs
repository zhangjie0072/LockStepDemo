using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPopupElement : MonoBehaviour
{
	public UINumberEffector m_effector;
	
	public virtual void SetColor (Color color)
	{
		for (int i = 0; i < transform.childCount; i++) {
			GameObject go = transform.GetChild (i).gameObject;
			UISprite sp = go.GetComponent<UISprite> ();
			if (sp == null)
				continue;
			
			sp.color = color;
		}
	}
	
}
