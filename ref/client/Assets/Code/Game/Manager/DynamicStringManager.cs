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
            Logger.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            NoticePopupString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Logger.Log("LoadNoticePopupString=>" + NoticePopupString);
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
            Logger.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            ContactUsString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Logger.Log("LoadContactUsString=>" + ContactUsString);
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
            Logger.LogWarning("网络故障或没有找到文件=>" + url);
            yield return null;
        }
        else
        {
            LoginServerClosedString = System.Text.Encoding.UTF8.GetString(load.bytes);
            Logger.Log("LoadContactUsString=>" + LoginServerClosedString);
        }
    }
    #endregion
}
