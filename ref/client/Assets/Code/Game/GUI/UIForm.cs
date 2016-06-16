using UnityEngine;
using System.Collections;
using ProtoBuf;
using fogs.proto.msg;
public class UIForm : MonoBehaviour
{	
	private bool mShow = false;

    public delegate void VoidDelegate();
    public VoidDelegate onShow;
	
	public enum ShowHideDirection{
		none,
		left,
		right,
	}
	
	void Awake( ){
		_OnAwake();
	}
	
	virtual protected void _OnAwake() {	}
	
	protected void _DoShowUI( ShowHideDirection dir )	{
		StopAllCoroutines();
		NGUITools.SetActive(gameObject, true);
		gameObject.AddMissingComponent<UIManagedPanel>();
        NGUITools.BringForward(gameObject);
		if( GetComponent<Animation>() && GetComponent<Animation>().enabled ){
			GetComponent<Animation>().Rewind();
			if( dir == ShowHideDirection.left )
				GetComponent<Animation>().Play( "UIRollInL" );
			else if( dir == ShowHideDirection.right )
				GetComponent<Animation>().Play( "UIRollInR" );
		}
	}
	
	public void DoShowUI( ShowHideDirection dir ) {
		
		if( mShow )
			return;
		mShow = true;
		
		_DoShowUI( dir );

		_OnShow();

        if (onShow != null)
            onShow();
	}
	
	
	public void DoHideUI( ShowHideDirection dir ) {
		if( !mShow )
			return;
		mShow = false;
		
		if( GetComponent<Animation>() && GetComponent<Animation>().enabled && dir != ShowHideDirection.none ){
			StartCoroutine( "crRollOutAndHide", dir );
		}
		else{
			gameObject.SetActive( false );
		}
		
		_OnHide();
	}
	
	IEnumerator crRollOutAndHide( ShowHideDirection dir ){
		if( GetComponent<Animation>() && GetComponent<Animation>().enabled ){
			GetComponent<Animation>().Rewind();
			if( dir == ShowHideDirection.left )
				GetComponent<Animation>().Play( "UIRollOutL" );
			else if( dir == ShowHideDirection.right )
				GetComponent<Animation>().Play( "UIRollOutR" );
			
			
			yield return new WaitForSeconds( 1 );
			
			gameObject.SetActive( false );
		}
	}

    // SDK回调函数
    public void HandleResp(string str)
    {
        Logger.Log("LoginInterface: transform=" + transform.name + "str=" + str);

#if ANDROID_SDK || IOS_SDK
        LoginNetwork.Instance.HandleSDKInfo(str, '&', transform.name.Equals("UILogo(Clone)"));
#endif
    }
	virtual protected void _OnShow( ) {}
	virtual protected void _OnHide( ) {}
}
