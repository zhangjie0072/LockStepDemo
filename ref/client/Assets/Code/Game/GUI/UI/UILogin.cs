using UnityEngine;
using System.Collections;

public class UILogin
{
    public static string UIName { get { return typeof(UILogin).Name; } }

    public GameObject goBg;
    public UIButton ButtonOK;
    public UIButton ButtonNotice;
    public UIButton ButtonCancle;
    public UIButton ButtonSwitch;
#if ANDROID_SDK || IOS_SDK
//    public Transform InputAccount;
#else
    public UIInput InputAccount;
#endif
    public UILabel lblText;
    public UILabel lblTips;
    public GameObject goServer;
    public UILabel lblServerLabel;
    public UILabel lblVersion;
    public UILabel lbSwitch;
	public GameObject goInput;
	public UIButton BtnInputOk;

    public void Initialize(GameObject root)
    {
        GameSystem.Instance.PreLoadConfig();

        ButtonNotice = root.transform.FindChild("TopLeft/ButtonNotice").GetComponent<UIButton>();
        ButtonCancle = root.transform.FindChild("TopRight/ButtonCancel").GetComponent<UIButton>();
        ButtonSwitch = root.transform.FindChild("TopRight/ButtonChange").GetComponent<UIButton>();

        //ButtonNotice.gameObject.SetActive(false);

        Debug.Log("1927 - Set visiable false in UiLogin");
        ButtonCancle.gameObject.SetActive(false);
        ButtonSwitch.gameObject.SetActive(false);

        goBg = root.transform.FindChild("Background").gameObject;
        ButtonOK = root.transform.FindChild("ButtonOK").GetComponent<UIButton>();
#if ANDROID_SDK || IOS_SDK
//        InputAccount = root.transform.FindChild("InputAccount");
//        GameObject.Destroy(root.transform.FindChild("InputAccount").GetComponent<UIInput>());
#else
		goInput = UIManager.Instance.CreateUI("Prefab/GUI/UIInputAccount",root.transform);
		InputAccount = goInput.transform.FindChild("Window/Input").GetComponent<UIInput>();
		BtnInputOk = goInput.transform.FindChild("Window/ButtonOK").GetComponent<UIButton>();
		goInput.SetActive(false);
#endif
		lblText = root.transform.FindChild("Account").GetComponent<UILabel>();
        lblTips = root.transform.FindChild("Tips").GetComponent<UILabel>();
//        lblTips.gameObject.SetActive(false);

        goServer = root.transform.FindChild("Server").gameObject;
        lblServerLabel = goServer.transform.FindChild("ServerLabel").GetComponent<UILabel>();
		
//		lbSwitch = goInput.transform.FindChild("Window/Input/InputAccount/Switch").GetComponent<UILabel>();
        lblVersion = root.transform.FindChild("BottomRight/Version").GetComponent<UILabel>();

    }
    public void Uninitialize()
    {
        //TODO:
    }
}