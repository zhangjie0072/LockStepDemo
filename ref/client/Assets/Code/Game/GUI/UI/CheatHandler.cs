using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class CheatHandler : MonoBehaviour
{

    bool _isInitialized = false;

    private UIButton _btnClose;
    private UIButton _btnConfirm;
    private UILabel _lblV1;
    private UILabel _lblV2;
    private UILabel _lblV3;

    private string _cmdStr;

	private GameObject btnTestMatch;
	private GameObject btnBaseMatch;
	private GameObject btnAddAllGoods;
	private UIToggle tgEnableLog;
	private UIToggle tgDisplayLog;

    void Awake()
    {
        if (_isInitialized == false)
        {
            _isInitialized = true;

            _btnClose = transform.FindChild("CloseBtn").GetComponent<UIButton>();
            _btnConfirm = transform.FindChild("Confirm").GetComponent<UIButton>();
            _lblV1 = transform.FindChild("Val1/Text").GetComponent<UILabel>();
            _lblV2 = transform.FindChild("Val2/Text").GetComponent<UILabel>();
            _lblV3 = transform.FindChild("Val3/Text").GetComponent<UILabel>();
			btnTestMatch = transform.FindChild("TestMatch").gameObject;
			btnBaseMatch = transform.FindChild("BaseMatch").gameObject;
			btnAddAllGoods = transform.FindChild("AddAllGoods").gameObject;
			tgEnableLog = transform.FindChild("EnableLog").GetComponent<UIToggle>();
			tgDisplayLog = transform.FindChild("DisplayLog").GetComponent<UIToggle>();
        }
    }

    // Use this for initialization
    void Start()
    {
		tgEnableLog.value = Logger.EnableLog;
		tgEnableLog.onChange.Add(new EventDelegate(() => Logger.EnableLog = tgEnableLog.value));
		tgDisplayLog.value = ErrorDisplay.Instance.enabled;
		tgDisplayLog.onChange.Add(new EventDelegate(() => ErrorDisplay.Instance.enabled = tgDisplayLog.value));

        UIEventListener.Get(_btnClose.gameObject).onClick = OnCloseClick;
        UIEventListener.Get(_btnConfirm.gameObject).onClick = OnConfirmClick;
		UIEventListener.Get(btnTestMatch).onClick = OnTestMatch;
		UIEventListener.Get(btnBaseMatch).onClick = OnBaseMatch;
		UIEventListener.Get(btnAddAllGoods).onClick = AddAllGoods;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //关闭
    private void OnCloseClick(GameObject go)
    {
		NGUITools.Destroy(gameObject);
    }

    //确认
    private void OnConfirmClick(GameObject go)
    {
        if (_lblV1.text == "")
        {
            return;
        }
        uint v1 = 0;
        uint v2 = 0;
        uint v3 = 0;

        uint.TryParse(_lblV1.text, out v1);
        uint.TryParse(_lblV2.text, out v2);
        uint.TryParse(_lblV3.text, out v3);

        string cmd = _cmdStr + "(" + v1 + "," + v2 + ");";

        GMCommondExcu msg = new GMCommondExcu();
        msg.commond_str = cmd;
        PlatNetwork.Instance.GMCommondExcuReq(msg);
    }


    public void Initialize()
    {
        //TODO: add process here.
    }

    public void SetLevel(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "SetLevel";
    }
    public void SetExp(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "SetExp";
    }
    public void SetHp(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "SetHp";
    }
    public void SetVip(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "SetVip";
    }

    public void AddGoods(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "AddGoods";
    }
    public void PassCareer(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "PassCareer";
    }
    public void PassTour(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "PassTour";
    }
    public void AddTask(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "AddTask";
    }
    public void FinishTask(UIToggle toggle)
    {
        if (toggle.value)
            _cmdStr = "FinishTask";
    }

	void OnTestMatch(GameObject go)
	{
		uint sceneId;
		if (uint.TryParse(_lblV1.text, out sceneId))
			GameMatch_Ready.sceneId = sceneId;
		GameSystem.Instance.mClient.CreateNewMatch(GameMatch.Type.eReady);
	}

	void OnBaseMatch(GameObject go)
	{
        GameSystem.Instance.mClient.CreateNewMatch(GameMatch.Type.eGuide);
	}

	void AddAllGoods(GameObject go)
	{
		for (int i = 1; i <= 2; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 999999)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 9111; i <= 9455; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 10)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 4024; i <= 4024; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 9999)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 10; i <= 10; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 99999)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3000; i <= 3003; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3005; i <= 3006; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3100; i <= 3100; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3102; i <= 3102; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3104; i <= 3106; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3201; i <= 3204; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3206; i <= 3215; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3300; i <= 3300; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3303; i <= 3303; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3400; i <= 3400; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3600; i <= 3600; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 3700; i <= 3704; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 6500; i <= 6500; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 6502; i <= 6502; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 1)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
		for (int i = 5; i <= 5; ++i)
		{
			GMCommondExcu msg = new GMCommondExcu();
			msg.commond_str = "AddGoods(" + i + ", 358520)";
			PlatNetwork.Instance.GMCommondExcuReq(msg);
		}
	}
}