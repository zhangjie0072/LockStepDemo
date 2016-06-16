using UnityEngine;
using System.Collections;

public class SceneManager : MonoSingleton<SceneManager>
{
    private string _NowSceneName;
    private string _BeforeSceneName;
    public UnityEngine.AsyncOperation mLoadingState = null;
    private LoadSceneState _crrentSceneState = LoadSceneState.NotLoading;

    public delegate void ChangeSceneOver(string name);
    public ChangeSceneOver ChangeOver;

    public void Init()
    {
        _NowSceneName = "";
        _BeforeSceneName = "";
    }

    public string NowSceneName
    {
        get { return _NowSceneName; }
    }

    public string BeforeSceneName
    {
        get { return _BeforeSceneName; }
    }

    private bool IsCanChangeScene(string vSceneName)
    {
        bool vResult = false;
        if (string.IsNullOrEmpty(vSceneName))
            return vResult;
        if (_NowSceneName == vSceneName)
            return vResult;
        return vResult = true;
    }

    public bool ChangeScene(string vSceneName, ChangeSceneOver vChangeOver = null)
    {
        //NetLoading.Instance.Initialize();
        //NetLoading.Instance.play = true;
        bool vResult = false;
        if (_crrentSceneState != LoadSceneState.NotLoading)
            return vResult;
        if (!IsCanChangeScene(vSceneName))
            return vResult;
        mLoadingState = null;
        ChangeOver += vChangeOver;
        StartCoroutine(LoadScene(vSceneName, vChangeOver));
        vResult = true;
        LuaScriptMgr.Instance.LuaGC(new string[] {"transform", "gameObject", "uiAnimator"});
        Resources.UnloadUnusedAssets();
        //System.GC.Collect();LoadLevelAsync方法自身会调用一次gc
        return vResult;
    }

    IEnumerator LoadScene(string vSceneName, ChangeSceneOver vChangeOver)
    {
        // modify by Conglin
        //Application.LoadLevelAsync(vSceneName);
        mLoadingState = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(vSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
        //ResourceLoadManager.Instance.UnloadDependAB();
        if (null == mLoadingState)
        {
            yield break;
        }
        else
        {
            _BeforeSceneName = _NowSceneName;
            _NowSceneName = vSceneName;
            HintManager.Instance.OpenLoad();
        }
        _crrentSceneState = LoadSceneState.Loading;
        while (!mLoadingState.isDone)
        {
            yield return 0;
        }
        if (ChangeOver != null)
        {
            ChangeOver(vSceneName);
            ChangeOver -= vChangeOver;
        }
        PlayerSceneMusic(vSceneName);
        _crrentSceneState = LoadSceneState.NotLoading;
        HintManager.Instance.CloseLoad();
        mLoadingState = null;
    }

    public bool GoBackToScene()
    {
        return ChangeScene(_BeforeSceneName);
    }

    public void PlayerSceneMusic(string vName)
    {
        switch (vName)
        {
            case GlobalConst.SCENE_STARTUP:
            case GlobalConst.SCENE_HALL:
            case GlobalConst.SCENE_MATCH:
                {
                    PlaySoundManager.PlaySound(GlobalConst.MUS_BGMHALL);
                    break;
                }
            case GlobalConst.SCENE_GAME:
                {
                    PlaySoundManager.PlaySound(GlobalConst.MUS_BGMGAME);
                    break;
                }
        }
    }
}