using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateVersionScript : MonoBehaviour
{
    /// <summary>
    /// Loading条中的文字信息
    /// </summary>
    private string loadInfo = "";

    private float ProgressPeracent = 0.0f;

    private bool isShowUpdateCheckingTips = true;

    /// <summary>
    /// Loading条中的文字信息
    /// </summary>
    public UILabel labLoading = null;

    /// <summary>
    /// Loading条下的文字提示信息
    /// </summary>
    public UILabel labCheckingTips = null;
    private string checkingTipsString = "";

    /// <summary>
    /// 版本号
    /// </summary>
    public UILabel labVersion = null;

    /// <summary>
    /// 进度条
    /// </summary>
    public UIProgressBar ProgressBar = null;

    public GameObject MessageTips = null;

    /// <summary>
    /// Logo图片
    /// </summary>
    public GameObject goLogo = null;
    private static GameObject goLogoPanel = null;

    public static void SetLogoActive(bool isActive)
    {
        if (goLogoPanel != null)
            goLogoPanel.SetActive(isActive);
    }

    // Use this for initialization
    void Start()
    {
        UIManager.AdaptiveUI();

        GlobalConst.IS_DEVELOP = false;

        goLogoPanel = goLogo;
        UpdateVersionScript.SetLogoActive(true);

        labLoading = this.transform.FindChild("Update/Progress/Tips").GetComponent<UILabel>();
        labCheckingTips = this.transform.FindChild("Update/UpdateCheckingTips").GetComponent<UILabel>();
        labVersion = this.transform.FindChild("Update/Version").GetComponent<UILabel>();
        ProgressBar = this.transform.FindChild("Update/Progress/BarBack").GetComponent<UIProgressBar>();

        MessageTips.SetActive(false);

        int local_version = VersionUpdateManager.VersionTag;
        if (local_version != 0)
            GlobalConst.RES_VERSION = VersionUpdateManager.VersionIntToStr(local_version);
        this.ChangeVersionLabel(VersionUpdateManager.VersionStrToInt(GlobalConst.RES_VERSION));

        VersionUpdateManager.Instance.onDownLoadPeracent = OnDownLoadPeracentChanged;
        VersionUpdateManager.Instance.onDecompressionPeracent = OnDecompressionPeracent;
        VersionUpdateManager.Instance.onUpdateFinished = OnUpdateFinished;
        VersionUpdateManager.Instance.onUpdateFaild = OnUpdateFaild;
        VersionUpdateManager.Instance.onUpdateTips = OnUpdateTips;

        VersionUpdateManager.AddUpdateCmd(UpdateCmdType.GetStreamAssetInfo);
    }

    // Update is called once per frame
    void Update()
    {
        lock (VersionUpdateManager.updateCmdList)
        {
            int index = VersionUpdateManager.updateCmdList.Count - 1;
            for (; index >= 0; index--)
            {
                UpdateCmd cmd = VersionUpdateManager.updateCmdList[index];
                switch(cmd.cmdType)
                {
                    case UpdateCmdType.GetStreamAssetInfo:
                        this.StartCoroutine(VersionUpdateManager.Instance.GetStreamAssetInfo());
                        break;
                    case UpdateCmdType.StreamAssetInit:
                        this.StartCoroutine(VersionUpdateManager.Instance.StreamAssetInit());
                        break;
                    case UpdateCmdType.LoadVersionList:
                        this.StartCoroutine(VersionUpdateManager.Instance.LoadVersionList());
                        break;
                    case UpdateCmdType.UpdateVersionFinish:
                        Debug.Log("load level Stratup");
                        Application.LoadLevel("Startup");
                        break;
                    case UpdateCmdType.UpdateMessageTips:
                        this.ShowMessageTips((UpdateVersionTipsScript.TipMessageStruct)cmd.obj);
                        break;
                    case UpdateCmdType.ChangeVersionLabel:
                        this.ChangeVersionLabel((int)cmd.obj);
                        break;
                    default:
                        break;
                }

                VersionUpdateManager.updateCmdList.RemoveAt(index);
            }
        }

        if (labLoading != null)
        {
            labLoading.text = loadInfo;
        }

        if (ProgressBar != null)
        {
            ProgressBar.value = ProgressPeracent;
        }

        if(labCheckingTips != null )
        {
            labCheckingTips.gameObject.SetActive(isShowUpdateCheckingTips);
            labCheckingTips.text = checkingTipsString;
        }
    }

    private void ChangeVersionLabel(int res_version)
    {
        if (labVersion != null)
        {
            string resVersionString = VersionUpdateManager.VersionIntToStr(res_version);
            GlobalConst.RES_VERSION = resVersionString;

            string str = string.Format("游戏版本：{0}\n资源版本：{1}",
                GlobalConst.GAME_VERSION,
                GlobalConst.RES_VERSION);

            labVersion.text = str;
        }
    }

    private void ShowMessageTips(UpdateVersionTipsScript.TipMessageStruct obj)
    {
        if (MessageTips != null)
        {
            var script = MessageTips.GetComponent<UpdateVersionTipsScript>();
            if (script != null)
            {
                script.SetMessage(obj.message, obj.msgType);
            }

            MessageTips.SetActive(true);
            SetLogoActive(false);
        }
    }


    public void OnUpdateTips(UpdateVersionTipsScript.TipsMessageType msgType, long size)
    {
        string str = "";
        switch (msgType)
        {
            case UpdateVersionTipsScript.TipsMessageType.VersionUpdateBig:
                str = string.Format("发现游戏大版本更新。\r\n你确认现在更新吗？");
                break;
            case UpdateVersionTipsScript.TipsMessageType.VersionUpdateSmall:
                str = string.Format("发现新的更新包，共计{0}\r\n请在wifi环境下更新，土豪随意！", Downloader.GetSizeStr(size));
                break;
            case UpdateVersionTipsScript.TipsMessageType.VersionUpdateFaild:
                str = string.Format("更新失败，请检查你的网络环境后重试！");
                break;
            default:
                break;
        }

        UpdateVersionTipsScript.TipMessageStruct s = new UpdateVersionTipsScript.TipMessageStruct();
        s.message = str;
        s.msgType = msgType;
        VersionUpdateManager.AddUpdateCmd(UpdateCmdType.UpdateMessageTips, s);
    }

    public void OnDecompressionPeracent(float peracent)
    {
        peracent = peracent * 0.99f;//解压占用99%，1%正在初始化场景资源

        loadInfo = string.Format("正在解压：{0}",
            peracent.ToString("0.00%"));

        ProgressPeracent = peracent;

        isShowUpdateCheckingTips = true;
        checkingTipsString = "解压过程不消耗您的流量";
    }

    public void OnDownLoadPeracentChanged(long currentSize, long totalSize)
    {
        float peracent = (float)currentSize / (float)totalSize;

        loadInfo = string.Format("下载资源：{0} {1}/{2}",
            peracent.ToString("0.00%"),
            Downloader.GetSizeStr(currentSize),
            Downloader.GetSizeStr(totalSize));

        ProgressPeracent = peracent;

        isShowUpdateCheckingTips = false;
    }

    public void OnUpdateFinished(int ver)
    {
        ProgressPeracent = 0.99f;
        isShowUpdateCheckingTips = false;

        loadInfo = string.Format("正在初始化场景资源...");
        VersionUpdateManager.AddUpdateCmd(UpdateCmdType.UpdateVersionFinish);
    }

    public void OnUpdateFaild()
    {
        loadInfo = string.Format("更新失败，请检查你的网络环境");

        if( VersionUpdateManager.Instance.onUpdateTips !=null )
            VersionUpdateManager.Instance.onUpdateTips(UpdateVersionTipsScript.TipsMessageType.VersionUpdateFaild, 0);

        isShowUpdateCheckingTips = false;
    }

    void OnApplicationPause(bool pause)
    {
        Logger.Log("UpdateVersionScript/OnApplicationPause:" + pause);
        if (!pause)
            UIManager.AdaptiveUI();
    }
}


