using UnityEngine;
using System.Collections;

public class NetLoading : MonoBehaviour
{
	public static NetLoading Instance
	{
		get
		{
			if (_Instance == null)
				CreateNetLoading();
			return _Instance;
		}
	}
	private static NetLoading _Instance = null;

	public float MaxWaitTime = 30f;
	public float ReLoginWaitTime = 1.5f;
	private UILabel info;
	private bool _isReLogin;
	public bool isReLogin
	{
		get { return _isReLogin; }
		set 
		{
			if (_isReLogin != value)
			{
				_isReLogin = value;
				remainTime = isReLogin ? ReLoginWaitTime : MaxWaitTime;
				if (info != null)
					info.gameObject.SetActive(!isReLogin);
			}
			//Logger.Log("Set isReLogin to:" + value);
		}
	}
	[HideInInspector]
    public bool play
	{
		get { return _play; }
		set 
		{
			if (!_play && value)
			{
				Logger.Log("Show NetLoading");
				remainTime = isReLogin ? ReLoginWaitTime : MaxWaitTime;
				gameObject.SetActive(true);
				info.gameObject.SetActive(!isReLogin);
			}
			else if (_play && !value)
			{
				Logger.Log("Hide NetLoading");
				gameObject.SetActive(false);
			}
			GameSystem.Instance.mClient.pause = value;
			_play = value;
		}
	}
    private bool _play = false;
	private float remainTime;

	public System.Action onTimeOut;

    void Awake()
    {
		_Instance = this;

		info = transform.FindChild("Info").GetComponent<UILabel>();
		//Vector3 position = transform.localPosition;
		//position.z = -500;
		//transform.localPosition = position;
		GetComponent<UIPanel>().depth = 20000;
		gameObject.SetActive(play);
		info.gameObject.SetActive(!isReLogin);
    }

	void OnDestroy()
	{
		_Instance = null;
	}
	
    void Update()
    {
		if (play)
		{
			remainTime -= Time.unscaledDeltaTime;
			info.text = string.Format(CommonFunction.GetConstString("RECONNECTING_COUNTDOWN"), Mathf.CeilToInt(remainTime));
			if (remainTime <= 0)
			{
				play = false;
				if (onTimeOut != null)
					onTimeOut();
			}
		}
    }

	static void CreateNetLoading()
	{
		UIManager.Instance.CreateUIRoot();
		Transform parent = UIManager.Instance.m_uiRootBasePanel.transform;
		GameObject netLoadingPrefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/NetLoading") as GameObject;
		GameObject obj = CommonFunction.InstantiateObject(netLoadingPrefab, parent);
		obj.transform.localPosition = netLoadingPrefab.transform.localPosition;
	}
}
