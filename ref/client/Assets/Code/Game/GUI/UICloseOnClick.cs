using UnityEngine;
using System.Collections;

public class UICloseOnClick : MonoBehaviour {

	// Use this for initialization
	public delegate void OnClosed();
	public OnClosed closeCallBack;
	void Start () {
		UIEventListener.Get(gameObject).onClick = SelfClicked;
//		WWW www = new WWW("http:...");
//		WWW.LoadFromCacheOrDownload("lll",0);
//		Hash128 h128 ;
//		AssetBundle ab = www.assetBundle;
//		GameObject go = GameObject.Instantiate(ab.mainAsset);
//		if(go)
//		{
//			go.AddComponent(UICloseOnClick);
//		}

	}
	void SelfClicked(GameObject go)
	{
		if(transform.parent!=null)
		{
			GameObject.DestroyImmediate(transform.parent.gameObject);
		}
		else
		{
			GameObject.DestroyImmediate(gameObject);
		}
		if (closeCallBack!=null)
		{
			closeCallBack();
		}
	}
}
