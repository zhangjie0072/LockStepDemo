using UnityEngine;
using System.Collections;


[AddComponentMenu("GameUI/UIDragPageNum")]
public class UIDragPageNum : MonoBehaviour
{
	public GameObject goDuplicate;
	
	int nCurPage;	//
	int nTotalPage;	//
	
	UISprite[] arPageIcon;
	
	void Start()	{
		if( goDuplicate )
			goDuplicate.SetActive( false );
	}
	
	public void SetPage( int page, int totalpage )	{
		int noldp = nCurPage;
		int noldpt = nTotalPage;
		
		nCurPage = page;
		nTotalPage = totalpage;
		
		nCurPage = Mathf.Min( nCurPage, nTotalPage );
		
		if( noldp != nTotalPage )
			_RecreateUI();
		else if( noldp != nCurPage )	{
			_UpdatePageUI();
		}
	}
	
	void _RecreateUI(){
		if( goDuplicate == null )
			return;
		
		UIGrid gr = GetComponent<UIGrid>();
		if( gr == null || goDuplicate == null )
			return;
		
		//clear
		if( arPageIcon != null )	{
			foreach( UISprite gop in arPageIcon )	{
				GameObject.DestroyImmediate( gop.gameObject );
			}
			arPageIcon = new UISprite[0];
		}
		
		arPageIcon = new UISprite[nTotalPage];
		for( int i=0; i<nTotalPage; i++ )	{
			GameObject go = GameObject.Instantiate( goDuplicate ) as GameObject;
			go.SetActive( true );
			Vector3 vOldScale = go.transform.localScale;
			go.transform.parent = gr.transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = vOldScale;
			arPageIcon[i] = go.GetComponent<UISprite>();
		}
		gr.Reposition();
		
		_UpdatePageUI();
	}
	
	void _UpdatePageUI( )	{
		for( int i=0; i<arPageIcon.Length; i++ )	{
			if( arPageIcon[i] )	{
				if( i == nCurPage )	{
					arPageIcon[i].spriteName = "Dot_White";
				}
				else
					arPageIcon[i].spriteName = "Dot_Gray";
			}
		}
	}
	
}
