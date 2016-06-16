using UnityEngine;
using System;
using System.Collections;
using LuaInterface;

public class SceneProc : MonoSingleton<SceneProc>
{
    public static long mTime = 0;
    void Awake()
    {
        GameObject goFrameWork = GameObject.Find("GameFramework");
        if (goFrameWork == null)
        {
            goFrameWork = new GameObject("GameFramework");
            goFrameWork.AddComponent<EngineFramework>();
        }
    }

    void Start()
    {
        Initialize();

        DynamicStringManager.Instance.Init();
    }

    void Update()
    {
        if (GameSystem.Instance.mClient.mUIManager.showTeamUpgrade 
            && !IsRestarting()
            && GameObject.Find("GoodsAcquirePopup(Clone)") == null
            )
        {
            ShowTeamUpgrade();
            GameSystem.Instance.mClient.mUIManager.showTeamUpgrade = false;
        }
    }

    private void Initialize()
    {
        if (Application.loadedLevelName == GlobalConst.SCENE_STARTUP)
        {
            RenderTextureManager.Instance.Initialize();
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.ShowUIForm();
            //GameSystem.Instance.mClient.mUIManager.LogoCtrl.UpdateResOver(true);
            //UpdateVersionManager.Instance.Initialize();
            InitializeTime();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Scheduler.Instance.AddTimer(1.0F, true, TimeCount);
        }
        if (Application.loadedLevelName == GlobalConst.SCENE_HALL)
        {
        }
        if (Application.loadedLevelName == GlobalConst.SCENE_MATCH)
        {
            //TODO: add process here.
        }
        if (Application.loadedLevelName == GlobalConst.SCENE_GAME)
        {
            //TODO: add process here.
        }
        if (GameSystem.Instance.mClient.mUIManager.showTeamUpgrade && !IsRestarting())
        {
            ShowTeamUpgrade();
            GameSystem.Instance.mClient.mUIManager.showTeamUpgrade = false;
        }
    }

    public void ShowTeamUpgrade()
    {
        if (GameSystem.Instance.TeamLevelConfigData.TeamLevelDatas[MainPlayer.Instance.Level].link.Count < 1) return;
        
       // if (Application.loadedLevelName == GlobalConst.SCENE_HALL)
        {
			UIManager.Instance.CreateUIRoot();
            GameObject obj = CommonFunction.InstantiateObject("Prefab/GUI/TeamUpgradePopup", UIManager.Instance.m_uiRootBasePanel.transform);
            obj.transform.GetComponent<UIPanel>().depth = 20;

            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Role");
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Squad");
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "RoleDetail");
            //UIManager.Instance.BringPanelForward(obj);
        }
    }


	bool IsRestarting()
	{
		LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
		LuaTable tParams = tScene["params"] as LuaTable;
		if (tParams != null)
		{
			object objRestart = tParams["reStart"];
			if (objRestart != null && (bool)(objRestart))
				return true;
		}
		return false;
	}

    private void InitializeTime()
    {
        TimeSpan ts = DateTime.UtcNow.Subtract(DateTime.Parse("1970-1-1")).Duration();
        mTime = (long)ts.TotalSeconds;
    }

    public void TimeCount()
    {
        mTime += 1;
    }
}