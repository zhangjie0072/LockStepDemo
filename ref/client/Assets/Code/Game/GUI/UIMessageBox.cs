using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("GameUI/UIMessageBox")]
public class UIMessageBox : UIForm
{
	public enum eResult	{
		none,
		ok,
		cancel
	}
	
	public UILabel txtMsg;
	public UILabel txtTitle;
	Message mCurMsg;
	
	public class Message	{
		public string txt;
		public GameObject goEventObj;
		public string func;
		public object data;
		public eResult result;
	}
	Queue<Message> qMsg = new Queue<Message>();
	
	static UIMessageBox mInstance;
	
	void OnDestroy()	{
		mInstance = null;
	}
	
	protected override void _OnHide ()
	{
		base._OnHide ();
	}
	
	/*
	public static void ShowMessage( string msg, GameObject goEventObj, string func, object data )	
	{
		if( mInstance == null )	{
			GameObject goForm = UIFormRes.GetUIForm( "UIMessageBox", "Prefab/GUI/Forms/UIMessageBox", "Anchor_Center" );
			if( goForm )
				mInstance = goForm.GetComponent<UIMessageBox>();
		}
		
		if( mInstance )	{
			GameSystem.Instance.mClient.mUIManager.ShowUIFormModal( mInstance, UIForm.ShowHideDirection.none );
			Message newmsg = new Message();
			newmsg.txt = msg;
			newmsg.goEventObj = goEventObj;
			newmsg.func = func;
			newmsg.data = data;
			newmsg.result = eResult.none;
			
			mInstance.qMsg.Enqueue( newmsg );
			
			mInstance._ShowOneMsg( );
		}
	}
	
	static void _MakesureFormExist( ){
		if( mInstance == null )	{
			GameObject goForm = UIFormRes.GetUIForm( "UIMessageBox", "Prefab/GUI/UIMessageBox", "Anchor_Center" );
			if( goForm )
				mInstance = goForm.GetComponent<UIMessageBox>();
		}
	}
	*/
	void _ShowOneMsg( ){
		if( qMsg.Count <= 0 )
			return;
		
		mCurMsg = qMsg.Dequeue();
		txtMsg.text = mCurMsg.txt;
	}
	
	public void OnBtnCancel()	{
		if( mCurMsg!= null )	{
			mCurMsg.result = eResult.cancel;
		}
		
		if( mCurMsg != null && mCurMsg.goEventObj )	{
			mCurMsg.goEventObj.SendMessage( mCurMsg.func, mCurMsg );
		}
		
		if( qMsg.Count > 0 )	{
			_ShowOneMsg();
		}
		else{
			GameSystem.Instance.mClient.mUIManager.HideUIForm( mInstance, UIForm.ShowHideDirection.none );
		}
	}
	
	public void OnBtnOK()	{
		if( mCurMsg!= null )	{
			mCurMsg.result = eResult.ok;
		}
		
		if( mCurMsg != null && mCurMsg.goEventObj )	{
			mCurMsg.goEventObj.SendMessage( mCurMsg.func, mCurMsg );
		}
		
		if( qMsg.Count > 0 )	{
			_ShowOneMsg();
		}
		else{
			GameSystem.Instance.mClient.mUIManager.HideUIForm( mInstance, UIForm.ShowHideDirection.none );
		}
	}
	
}
