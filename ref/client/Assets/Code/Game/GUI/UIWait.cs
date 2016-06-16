using UnityEngine;
using System.Collections;
/// <summary>
/// wait event ui.add to scene in UIManager.CreateUIRoot()
/// </summary>
public class UIWait : MonoBehaviour {
	static UIWait _instance;
	float m_ntimeout = 0.0f;
	float m_ntimeoutCount;
	short showWaitMsgGount;
	float m_nConfigDelay;
	// Use this for initialization
	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad(this);
		
		//auto resolution 
		if(UIManager.Instance!=null && UIManager.Instance.m_uiRoot!=null)
		{
			transform.localScale = UIManager.Instance.m_uiRoot.transform.localScale;
		}
		showWaitMsgGount = 0;
		gameObject.name = "UIWait";
//		gameObject.SetActive(false);
	}
	void Start()
	{
		if(showWaitMsgGount<=0)
		{
			
			if(GameSystem.Instance.CommonConfig!=null )
			{
				m_nConfigDelay = GameSystem.Instance.CommonConfig.GetFloat("UIWaitDelay");
			}
			gameObject.SetActive(false);
		}
	}
	// Update is called once per frame
	void Update () {
		
		if(m_ntimeout>0)
		{
			m_ntimeoutCount+=Time.deltaTime;
			if(m_ntimeoutCount>m_ntimeout)
			{
				stopWait();
			}
		}
		//lost connection to either login server or game server
		if(!GameSystem.Instance.mNetworkManager.connLogin || !GameSystem.Instance.mNetworkManager.connGS )
		{
			showWaitMsgGount = 0;
			gameObject.SetActive(false);
		}
	}
	void startWait()
	{
		if(!gameObject.activeInHierarchy)
		{
			//auto resolution 
			if(UIManager.Instance!=null && UIManager.Instance.m_uiRoot!=null)
			{
				transform.localScale = UIManager.Instance.m_uiRoot.transform.localScale;
			}
			else{
				// no ui root
				return;
			}
			gameObject.SetActive(true);
		}
		m_ntimeoutCount = 0.0f;
		showWaitMsgGount++;
	}
	void stopWait()
	{
		if(showWaitMsgGount>0)
		{			
			showWaitMsgGount--;
			if(showWaitMsgGount<=0)
			{
				gameObject.SetActive(false);
			}
		}
	}
//	void hide()
//	{
//		gameObject.SetActive(false);
//	}
	/// <summary>
	/// Shows the wait ui.
	/// if last wait ui is not closed,this action will cover it
	/// in the abstract this case will not occur
	/// 传入时间已废弃，读取配置时间,instead by ShowWait()
	/// </summary>
	/// <param name="msg_id">Msg_id.</param>
	/// <param name="delay">Delay.</param>
	/// <param name="timeout">Timeout.</param>
	public static void ShowWait(float ActionDelay,float timeout = 120)
	{
		if(_instance == null)
			return;
		_instance.m_ntimeout = timeout;
		_instance.Invoke("startWait",_instance.m_nConfigDelay);

	}
	public static void ShowWait()
	{
		ShowWait(0);
	}
	public static void StopWait()
	{
//		Debug.Log("StopWait");
		if(_instance!=null)
		{
			if(_instance.gameObject.activeInHierarchy)
			{
				_instance.stopWait();
//				_instance.Invoke("stopWait",0.5f);
			}
		}
	}
	void OnApplicationQuit()
	{
		Destroy(gameObject);
	}
}
