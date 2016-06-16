using UnityEngine;
using System;
using System.Collections;
using fogs.proto.msg;
using LuaInterface;
using System.Collections.Generic;
public class UILoadingControl
{
    GameObject goUI;
    public UILoading _ui;

    bool isInitialized = false;
    bool runChange = false;

    public void Initialize()
    {
        if (_ui == null)
            _ui = new UILoading();

        goUI = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UILoading");
        _ui.Initialize(goUI);

        Scheduler.Instance.AddUpdator(Update);
        GameSystem.Instance.showLoading = true;

        isInitialized = true;


        string str = GameSystem.Instance.CommonConfig.GetString("gLoadingBg");
        string[] items = str.Split('&');
        int nt = 0;
        Dictionary<int,string> bgDic = new Dictionary<int,string>();

        List<int>bgList = new List<int>();
        
        foreach( string item in items )
        {
            string[] info = item.Split(':');
            string bg = info[0];
            int weight = int.Parse(info[1]);
            if( weight == 0 )
            {
                continue;
            }
            nt += weight;
            bgDic.Add(nt, bg);
            bgList.Add(nt);
        }
        int randNum = UnityEngine.Random.Range(1, nt);
        int key = -1;
        for( int i = 0; i < bgList.Count; i++)
        {
            if( randNum <= bgList[i] )
            {
                key = bgList[i];
                break;
            }
        }
        string bgStr = bgDic[key];
        _ui._bg.mainTexture = ResourceLoadManager.Instance.GetResources("Texture/" + bgStr, true) as Texture; 
    }

    public void ShowUIForm(uint param = 0)
    {
        if (isInitialized == false)
        {
            Initialize();
        }
        if (goUI)
        {
            UIForm form = goUI.GetComponent<UIForm>();
            GameSystem.Instance.mClient.mUIManager.ShowUIForm(form, UIForm.ShowHideDirection.none);

			GuideSystem.Instance.Init();
        }
		UIWait.StopWait();
    }

    public void FinishAnim()
    {

    }

    public void Update()
    {
        UpdateLoadProcess();

        if (runChange == false && GameSystem.Instance.loadConfigFinish) // && GameSystem.Instance.mNetworkManager.InNormalState())
        {
            runChange = true;
            Scheduler.Instance.RemoveUpdator(Update);
            GameSystem.Instance.pushConfig.SetConfigForPhoneSys();
            if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1.IsAlive)
            {
                GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1.Abort();
            }
            if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2.IsAlive)
            {
                GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2.Abort();
            }

            PlatNetwork.Instance.EnterGame();
        }
    }

    private void UpdateLoadProcess()
    {
        _ui._loadingBar.value = (GameSystem.Instance.readConfigCnt-32) / 40.2f;
        _ui._barValue.text = (int)(_ui._loadingBar.value * 100) + "%";
    }


    public void Uninitialize()
    {
		if (isInitialized)
        {
            if (_ui != null)
            {
                _ui.Uninitialize();
                _ui = null;
            }
			isInitialized = false;
		}
    }
}
