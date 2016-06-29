using UnityEngine;
using System.Collections;

/// <summary>
/// 管理从HTTP服务器动态获取文本信息
/// </summary>
public class DynamicStringManager
{
    protected static DynamicStringManager _instance;
    public static DynamicStringManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DynamicStringManager();
            }

            return _instance;
        }

        private set { }
    }

    protected DynamicStringManager()
    {

    }

    public void Init()
    {
        SceneProc.Instance.StartCoroutine(LoadNoticePopupString());
        SceneProc.Instance.StartCoroutine(LoadContactUsString());
		SceneProc.Instance.StartCoroutine(LoadLoginServerClosedString());
		SceneProc.Instance.StartCoroutine(LoadActivityAssets());
    }

    #region NoticePopup 公告信息
    private string _noticePopupString;
    public string NoticePopupString
    {
        get { return _noticePopupString; }
        protected set { _noticePopupString = value; }
    }

    IEnumerator LoadNoticePopupString()
    {
        string url = ResPath.CHECKVERSIONADDRESS + "NoticePopup.txt";
        WWW load = new WWW(url);
        yield return load;

        if (!string.IsNullOrEmpty(load.error))
        {
            Debug.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            NoticePopupString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Debug.Log("LoadNoticePopupString=>" + NoticePopupString);
        }
    }
    #endregion

    #region ContactUs 联系我们
    private string _contactUsString;
    public string ContactUsString
    {
        get { return _contactUsString; }
        protected set { _contactUsString = value; }
    }

    IEnumerator LoadContactUsString()
    {
        string url = ResPath.CHECKVERSIONADDRESS + "ContactUs.txt";
        WWW load = new WWW(url);
        yield return load;

        if (!string.IsNullOrEmpty(load.error))
        {
            Debug.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            ContactUsString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Debug.Log("LoadContactUsString=>" + ContactUsString);
        }
    }
    #endregion

    #region Login 服务器未开启时的提示信息
    private string _loginServerClosedString = "当前网络不可用，请检查网络。";
    public string LoginServerClosedString
    {
        get { return _loginServerClosedString; }
        protected set { _loginServerClosedString = value; }
    }
    IEnumerator LoadLoginServerClosedString()
    {
        string url = ResPath.CHECKVERSIONADDRESS + "ServerTips.txt";
        WWW load = new WWW(url);
        yield return load;

        if (!string.IsNullOrEmpty(load.error))
        {
            Debug.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            LoginServerClosedString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Debug.Log("LoadContactUsString=>" + LoginServerClosedString);
        }
    }
    #endregion
	#region 活动公告
	public System.Collections.Generic.List<AssetBundle> ActivityAnnoucementList = new System.Collections.Generic.List<AssetBundle>();
	public LuaInterface.LuaTable  getActivityLuaTable()
	{
		return ActivityAnnoucementList.toLuaTable();
	}
	IEnumerator LoadActivityAssets()
	{
//		for(int i = 0;i<3 ;i++)
//		{
////			GameObject baseGo = ResourceLoadManagerBase.Instance.BaseGetResources("Resources/testAsset"+i+".assetbundle",typeof(GameObject)) as GameObject;
//			byte[] data = System.IO.File.ReadAllBytes("Assets/Resources/testAsset"+i+".assetbundle");
//			AssetBundle ab = AssetBundle.LoadFromMemory(data);
//			if(ab!=null)
//			{
//				ActivityAnnoucementList.Add(ab);
////				Debug.LogWarning("AssetBundle asset.name"+ab.name);
////				GameObject go = GameObject.Instantiate(ab.mainAsset) as GameObject;
////				go.AddComponent<UICloseOnClick>();
//			}
//			else{
//				Debug.LogWarning("assetBundle load fail "+i);
//			}
//
//		}
//		yield return null;

		string url = ResPath.CHECKVERSIONADDRESS+"/update/test/activityConfig.xml";
		WWW load = new WWW(url);
		yield return load;

		if (!string.IsNullOrEmpty(load.error))
		{
			Debug.LogWarning("网络故障或没有找到文件=>" + url);
			yield return null;
		}
		else
		{
			if(load.assetBundle != null)
			{
				ActivityAnnoucementList.Add(load.assetBundle);
			}
			Debug.Log("activityConfig=>" + url);
		}
		System.Collections.Generic.Dictionary<string,int> urllist = ActivityConfigInfo(load.text);
		if(urllist!=null)
		{
			foreach(System.Collections.Generic.KeyValuePair<string,int> dt in urllist)
			{
				if(!string.IsNullOrEmpty(dt.Key))
				{
					load = WWW.LoadFromCacheOrDownload(dt.Key,dt.Value);
					yield return load;

					if (!string.IsNullOrEmpty(load.error))
					{
						Debug.LogWarning("网络故障或没有找到文件=>" + dt.Key);
						yield return null;
					}
					else
					{
						if(load.assetBundle != null)
						{
							ActivityAnnoucementList.Add(load.assetBundle);
						}
						Debug.Log("ActivityAnnoucementList=>" + dt.Key+",version "+dt.Value);
					}
				}

			}
		}

	}
	/// <summary>
	/// 根据服务器下载配置内容来解析需要下载的活动数据
	/// </summary>
	/// <returns>The config info.</returns>
	/// <param name="content">Content.</param>
	private System.Collections.Generic.Dictionary<string,int> ActivityConfigInfo(string content)
	{
		if(string.IsNullOrEmpty(content))
			return null;
		System.Collections.Generic.Dictionary<string,int> dic = new System.Collections.Generic.Dictionary<string, int>();
		System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
		xmlDoc.LoadXml(content);
		//解析xml的过程
		System.Xml.XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
		foreach (System.Xml.XmlElement xe in nodelist)
		{
			int version = 0;
			string url = string.Empty;
			string name;
			int open = 0;
			foreach (System.Xml.XmlElement xel in xe)
			{
				if (xel.Name == "id")
				{
				}
				else if (xel.Name == "version")
				{
					if(!int.TryParse(xel.InnerText, out version))
						version = -1;

				}
				else if (xel.Name == "url")
				{
					url = xel.InnerText;
				}
				else if (xel.Name == "name")
				{
					name = xel.InnerText;
				}
				else if(xel.Name == "open")
				{
					int.TryParse(xel.InnerText,out open);
				}
			}
			if(open == 1 )
				dic.Add(url,version);

		}
		return dic;
	}
	#endregion
}
