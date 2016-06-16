using UnityEngine;
using System.Collections;

public class EngineFramework : MonoBehaviour
{
    public static int frameCount { get; private set; }   //Update次数
    public static int fixedUpdateCount { get; private set; }   //fixedUpdate次数

    void Awake()
    {
        DontDestroyOnLoad(this);
        GameSystem.Instance.mEngineFramework = this;
        GameSystem.Instance.Start();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
    }

    void Update()
    {
        ++frameCount;

        CheatingDeath.Instance.mAntiSpeedUp.Update(Time.deltaTime);

        GameSystem.Instance.Update();
        Scheduler.Instance.Update();
    }

    void FixedUpdate()
    {
        ++fixedUpdateCount;
        GameSystem.Instance.FixedUpdate();
    }

    void LateUpdate()
    {
        GameSystem.Instance.LateUpdate();
    }

    void OnApplicationQuit()
    {
        if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1 != null
            && GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1.IsAlive)
        {
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1.Abort();
        }
        if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2 != null
            && GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2.IsAlive)
        {
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2.Abort();
        }
        GameSystem.Instance.Exit();

        Logger.Log("Game quits normally.");
    }

    void OnLevelWasLoaded(int level)
    {
        Scheduler.Instance.ClearUpdator();
        GameSystem.Instance.mClient.OnLevelWasLoaded(level);
        //ResourceLoadManager.Instance.PreLoadAtlas();
        LuaScriptMgr.Instance.DoString("OnLevelWasLoaded(" + level + ")");
        LuaScriptMgr.Instance.OnLevelLoaded(level);
        AudioManager.Instance.OnLevelWasLoaded(level);
    }

    void OnDrawGizmos()
    {
        GameSystem.Instance.DebugDraw();
    }

    void OnGUI()
    {
        if (GameSystem.Instance.mClient.mUIManager != null)
            GameSystem.Instance.mClient.mUIManager.OnGUI();
    }

    void OnApplicationPause(bool pause)
    {
        Logger.Log("OnApplicationPause:" + pause);
        GameSystem.Instance.appPaused = pause;
        if (!pause)
            UIManager.AdaptiveUI();
    }
}
