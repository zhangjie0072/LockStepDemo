using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPopup_Miss :UIPopupElement
{	
	UISprite mPic;
	
	void Start(){
		mPic = GetComponentInChildren<UISprite>();
		if( mPic )
			mPic.MakePixelPerfect();
		//mPic = GetComponent<UISprite>();
	}
	
	public override void SetColor (Color color)
	{
		if( mPic ){
			mPic.color = color;
		}
	}
	
}
