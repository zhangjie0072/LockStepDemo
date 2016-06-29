using UnityEngine;
using System.Collections;

public class UpdateVersionTipsScript : MonoBehaviour
{
    public UIButton btnOk;
    public UIButton btnCancel;
    public UILabel labTips;

    public TipsMessageType messageType = TipsMessageType.NONE;

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(btnOk.gameObject).onClick += OnOk;
        UIEventListener.Get(btnCancel.gameObject).onClick += OnCancel;
    }

    public void SetMessage(string msg, TipsMessageType msgType)
    {
        if (labTips != null)
            labTips.text = msg;

        messageType = msgType;
    }

    private void OnOk(GameObject go)
    {
        Debug.Log("ok");

        switch (messageType)
        {
            case TipsMessageType.VersionUpdateBig:
                Application.OpenURL("http://www.baidu.com");
                break;
            case TipsMessageType.VersionUpdateSmall:
                VersionUpdateManager.Instance.StartUpdate();
                break;
            case TipsMessageType.VersionUpdateFaild:
                VersionUpdateManager.isRetry = true;
                VersionUpdateManager.AddUpdateCmd(UpdateCmdType.GetStreamAssetInfo);
                break;
            default:
                break;
        }

        this.gameObject.SetActive(false);
    }

    private void OnCancel(GameObject go)
    {
        Debug.Log("cancel");
        Application.Quit();
    }

    public struct TipMessageStruct
    {
        public string message;
        public TipsMessageType msgType;
    }

    public enum TipsMessageType
    {
        NONE,
        VersionUpdateSmall,
        VersionUpdateBig,
        VersionUpdateFaild,
    }
}



