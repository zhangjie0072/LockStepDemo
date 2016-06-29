using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager
{
    public static UIManager Instance
    {
        get
        {
            if (GameSystem.Instance.mClient != null)
                return GameSystem.Instance.mClient.mUIManager;
            else
                return null;
        }
    }
    //modal
    public List<UIForm> lstModalForms = new List<UIForm>();
    //modalless
    public List<UIForm> lstForms = new List<UIForm>();
    public GameMatch.LeagueType curLeagueType = GameMatch.LeagueType.eNone;

    public List<FormChangeListener> lstListeners = new List<FormChangeListener>();

    float fEllapsSinceNoForm = 0;

    public UICamera m_uiCamera { get; private set; }
    public GameObject m_uiRootBasePanel;

    public static float m_ratioScale = 1f;

    public UIRoot m_uiRoot;
    private GameObject m_resRoot;
    private GameObject m_mask;
    private GameObject m_bgPanel;
	private GameObject m_uiWait;

    protected Dictionary<string, Object> m_UICache;

    private delegate void UICallMethod(uint param);
    private Dictionary<string, UICallMethod> UICallList = new Dictionary<string, UICallMethod>();

    //public UILogoControl LogoCtrl;
    public UILoginControl LoginCtrl;
    public UILoadingControl LoadingCtrl;

    public bool showTeamUpgrade = false;
    public bool isInMatchLoading = false;

    private float m_framerateAcc = 0.0f;
    private int m_framerateCnt = 0;
    private float _updateInterval = 1.0f;
    private float m_timeThreshold = 0.0f;
    private float m_avgFPS = 0.0f;
    public float AvgFPS
    {
        get
        {
            return m_avgFPS;
        }
    }

    private List<UIPanel> panelList = new List<UIPanel>();

    public UIManager()
    {
        if (m_resRoot == null)
        {
            System.DateTime time = System.DateTime.Now;
            m_resRoot = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIRoot") as GameObject;
            if (m_resRoot == null)
                Debug.LogError("load Prefab/GUI/UIRoot error!!!");

            UIRoot uiRoot = m_resRoot.GetComponent<UIRoot>();
            AdaptiveUI(uiRoot);

            Debug.Log("【Time】Client>>>UIManager>>>Load Prefab/GUI/UIRoot=>" + (System.DateTime.Now - time).TotalSeconds.ToString());
        }

        m_UICache = new Dictionary<string, Object>();

        Initialize();
    }

    public void Initialize()
    {
        //LogoCtrl = new UILogoControl();
        //UICallList.Add(UILogo.UIName, LogoCtrl.ShowUIForm);

        LoginCtrl = new UILoginControl();
        UICallList.Add(UILogin.UIName, LoginCtrl.ShowUIForm);

        LoadingCtrl = new UILoadingControl();
        UICallList.Add(UILoading.UIName, LoadingCtrl.ShowUIForm);
    }

    public void RemoveAll()
    {
        lstModalForms.Clear();
        lstForms.Clear();
        lstListeners.Clear();
        m_UICache.Clear();

        UnintializeUI();
    }

    public void UnintializeUI()
    {
        //LogoCtrl.Uninitialize();
        LoginCtrl.Uninitialize();
        LoadingCtrl.Uninitialize();
    }

    public void ShowSpecifiedUI(string strUIName, uint param)
    {
        if (UICallList.ContainsKey(strUIName))
            UICallList[strUIName](param);
        else
        {
            LuaInterface.LuaTable topPanelMgr = LuaScriptMgr.Instance.GetLuaTable("TopPanelManager");
            LuaInterface.LuaFunction funcShowPanel = topPanelMgr["ShowPanel"] as LuaInterface.LuaFunction;
            funcShowPanel.Call(new object[] { topPanelMgr, strUIName, param });
        }
    }

    public class ActiveFormChangedEvent
    {
        public UIForm old;
        public UIForm active;
        public ActiveFormChangedEvent(UIForm old_, UIForm active_)
        {
            old = old_;
            active = active_;
        }
    }

    public bool IsUIActived()
    {
        return (lstModalForms.Count != 0) || (lstForms.Count != 0);
    }

    public interface FormChangeListener
    {
        bool OnActiveFormChanged(ActiveFormChangedEvent evFormChange);
    }

    public void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == GlobalConst.SCENE_STARTUP)
            return;

        lstModalForms.Clear();
        lstForms.Clear();
        //mCurUIForm = null;	//这里切场景，不触发Changed事件
        lstListeners.Clear();
        ShowMask();
    }
    public void DestroyBasePanelObjects()
    {
        int destroyNum = 0;
        int crossSceneNum = 0;
        foreach (Transform tm in m_uiRootBasePanel.transform)
        {
            if( tm.tag == "CrossScene")
            {
                crossSceneNum++;
            }
            //if (tm.tag != "CrossScene")
            //{
            //    Object.DestroyImmediate(tm.gameObject);
            //    destroyNum++;
            //}
        }

        int index = 0;
        while( m_uiRootBasePanel.transform.childCount > crossSceneNum )
        {
            Transform t = m_uiRootBasePanel.transform.GetChild(index);
            if( t.tag != "CrossScene")
            {
                Object.DestroyImmediate(t.gameObject);
            }
            else
            {
                index++;
            }
        }
    }

    public static void AdaptiveUI()
    {
        AdaptiveUI(null);
    }

    private static void AdaptiveUI(UIRoot uiRoot)
    {
        if (uiRoot == null)
        {
            uiRoot = GameObject.FindObjectOfType<UIRoot>();
        }

        if (uiRoot == null)
        {
            return;
        }

        float DesignWidth = 1280;
        float DesignHeight = 720;
        uiRoot.scalingStyle = UIRoot.Scaling.FixedSize;

        float height, width;
        if (Screen.height > Screen.width)
        {
            width = Screen.height;
            height = Screen.width;
        }
        else
        {
            width = Screen.width;
            height = Screen.height;
        }

        #region 上线不留黑边的方式进行设备适配
        /********************************
         * 设计分辨率为 1280 * 720
         * 宽高比支持的极端比例 4:3  16:9 
         * 按宽高比进行缩放，宽高各占权重50%
         * *****************************/
        float manualHeight = DesignHeight + (DesignWidth / width * height - DesignHeight) * 0.5f;
        uiRoot.manualHeight = (int)manualHeight;
        #endregion
		/*
        bool result = height / width > DesignHeight / DesignWidth;
        if (result)
        {
            uiRoot.manualHeight = Mathf.RoundToInt(DesignWidth / width * height);
            m_ratioScale = (height / width) / (DesignHeight / DesignWidth);
        }
        else
        {
            uiRoot.manualHeight = (int)DesignHeight;
        }

        Debug.Log(string.Format("----------AdaptiveUI() height={0} width={1} manualHeight={2}", Screen.height, Screen.width, uiRoot.manualHeight));
*/
    }

    public GameObject CreateUI(string strResPath, Transform parent = null)
    {
        Object res = null;
        if (m_uiRootBasePanel == null)
        {
            CreateUIRoot();
        }

        if (strResPath.Contains("Prefab/GUI/"))
            strResPath = strResPath;
        else if (strResPath.StartsWith("Items"))
            strResPath = "Prefab/" + strResPath;
        else if (!strResPath.Contains("Prefab/GUI/")
            && (!strResPath.Contains("Prefab/GUI/")))
            strResPath = "Prefab/GUI/" + strResPath;

        if (!m_UICache.TryGetValue(strResPath, out res))
        {
            //if (strResPath.Contains("UILogo"))
            //    res = ResourceLoadManager.Instance.GetResources(strResPath) as GameObject;
            //else
                res = ResourceLoadManager.Instance.LoadPrefab(strResPath) as GameObject;
            if (res == null)
                return null;
            else
                m_UICache.Add(strResPath, res);
        }
        GameObject ui = GameObject.Instantiate(res) as GameObject;
        ui.transform.parent = (parent != null ? parent : m_uiRootBasePanel.transform);
        ui.transform.localScale = Vector3.one;
        ui.transform.localPosition = (res as GameObject).transform.localPosition;

        if (ui.GetComponent<UIPanel>() == null)
        {
            UIWidget widget = NGUITools.FindInParents<UIWidget>(ui.transform.parent);
            if (ui.GetComponentInChildren<UIWidget>() != null && widget != null)
                NGUITools.AdjustDepth(ui, widget.depth + 1);
        }
        return ui;
    }

    public void CreateUIRoot(bool forceRecreate = false)
    {
        GameObject goUIRoot = null;
        if (!forceRecreate)
            goUIRoot = GameObject.FindGameObjectWithTag("UIRoot");
        if (goUIRoot == null || goUIRoot.GetComponent<UIRoot>() == null)
        {
            goUIRoot = GameObject.Instantiate(m_resRoot) as GameObject;
        }

        GameObject.DontDestroyOnLoad(goUIRoot);

        m_uiRoot = goUIRoot.GetComponent<UIRoot>();
        if (m_uiRoot == null)
            m_uiRoot = goUIRoot.AddComponent<UIRoot>();

        m_uiCamera = goUIRoot.GetComponentInChildren<UICamera>();

        Transform panel = GameUtils.FindChildRecursive(m_uiRoot.transform, "BasePanel");
        m_uiRootBasePanel = panel.gameObject;
        //m_mask = panel.FindChild("Mask").gameObject;
        //m_bgPanel = panel.FindChild("BgPanel").gameObject;

        ShowMask();
		
		//add ui wait
		
		if(m_uiWait == null)
		{
			GameObject uiWait = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIWait");
			m_uiWait = GameObject.Instantiate(uiWait) as GameObject;
		}
    }

    public void ShowMask(bool isDelayHideMask = true)
    {
        string levelName = Application.loadedLevelName;
        bool showMask = true;
		if (levelName == "Scene20101" || levelName == "Scene20201"
			|| levelName == "Scene20301" || levelName == "Scene20401"
			|| levelName == "Scene20501" || levelName == "Scene20502"
			|| levelName == "Scene20601" || levelName == "Scene20701"
			|| levelName == "Scene20801" || levelName == "Scene20901"
			|| levelName == "Scene21001" || levelName == "Scene20902")
        {
            if( !isInMatchLoading )
            {
                // Make it delay hide the mask, in UIChallengeLoading's destroy.
                // If not, it hides mask while UI still stays loading in pvp.
                showMask = false;
            }
        }
        ForceShowMask(showMask);
    }

    public void ForceShowMask(bool showMask)
    {
        if (m_mask != null)
            m_mask.SetActive(showMask);
        if (m_bgPanel != null)
            m_bgPanel.SetActive(showMask);
    }


    public void DisplayError(string errStr)
    {
        ErrorDisplay.Instance.HandleLog(errStr, null, LogType.Log);
    }

    public void ShowUIForm(UIForm fr, UIForm.ShowHideDirection dir)
    {
        UIForm frActiveOld = GetActiveForm();

        if (lstForms.Contains(fr))
        {
            lstForms.Remove(fr);
        }

        UIForm frOldNonModal = null;
        if (lstForms.Count > 0)
        {
            frOldNonModal = lstForms[lstForms.Count - 1];
        }

        lstForms.Add(fr);

        fr.DoShowUI(dir);

        if (frOldNonModal)
            frOldNonModal.DoHideUI(UIForm.ShowHideDirection.left);

        UIForm frActive = GetActiveForm();
        if (frActiveOld != frActive)
        {
            _OnActiveFormChanged(frActiveOld, frActive);
        }
    }

    public void ShowUIFormModal(UIForm fr, UIForm.ShowHideDirection dir)
    {
        UIForm frActiveOld = GetActiveForm();

        if (lstModalForms.Contains(fr))
        {
            lstModalForms.Remove(fr);
        }

        if (lstModalForms.Count > 0)
        {
            UIForm frOld = lstModalForms[lstModalForms.Count - 1];
            frOld.DoHideUI(UIForm.ShowHideDirection.left);
        }

        lstModalForms.Add(fr);
        fr.DoShowUI(dir);

        UIForm frActive = GetActiveForm();
        if (frActiveOld != frActive)
        {
            _OnActiveFormChanged(frActiveOld, frActive);
        }
    }

    public void HideUIForm(UIForm fr, UIForm.ShowHideDirection dir)
    {
        UIForm frActiveOld = GetActiveForm();

        if (lstModalForms.Contains(fr))
        {
            //modal
            UIForm frLast = lstModalForms[lstModalForms.Count - 1];

            if (frLast == fr)
            {
                fr.DoHideUI(dir);
                lstModalForms.Remove(fr);

                if (lstModalForms.Count > 0)
                {
                    frLast = lstModalForms[lstModalForms.Count - 1];
                    frLast.DoShowUI(UIForm.ShowHideDirection.left);
                }
            }
            else
            {
                lstModalForms.Remove(fr);
            }
        }
        else
        {
            //non-modal
            if (lstForms.Count <= 0)
                return;

            UIForm frLast = lstForms[lstForms.Count - 1];

            if (frLast == fr)
            {
                fr.DoHideUI(dir);
                lstForms.Remove(fr);

                if (lstForms.Count > 0)
                {
                    frLast = lstForms[lstForms.Count - 1];
                    frLast.DoShowUI(UIForm.ShowHideDirection.left);
                }

            }
            else
            {
                lstModalForms.Remove(fr);
            }

        }

        UIForm frActive = GetActiveForm();
        if (frActiveOld != frActive)
        {
            _OnActiveFormChanged(frActiveOld, frActive);
        }
    }

    public void HideAllNonModal()
    {
        if (lstForms.Count <= 0)
            return;

        UIForm frActiveOld = GetActiveForm();

        UIForm frLast = lstForms[lstForms.Count - 1];

        if (frLast)
        {
            frLast.DoHideUI(UIForm.ShowHideDirection.right);
        }

        lstForms.Clear();

        UIForm frActive = GetActiveForm();
        if (frActiveOld != frActive)
        {
            _OnActiveFormChanged(frActiveOld, frActive);
        }
    }

    public UIForm GetActiveForm()
    {
        if (lstModalForms.Count > 0)
        {
            return lstModalForms[lstModalForms.Count - 1];
        }

        return GetActiveNonModalForm();
    }

    public UIForm GetActiveNonModalForm()
    {
        if (lstForms.Count > 0)
        {
            return lstForms[lstForms.Count - 1];
        }

        return null;
    }

    void _OnActiveFormChanged(UIForm last_form, UIForm cur_form)
    {

        foreach (FormChangeListener lsn in lstListeners)
        {
            lsn.OnActiveFormChanged(new ActiveFormChangedEvent(last_form, cur_form));
        }
    }

    public void AddFormChangeListener(FormChangeListener lsn)
    {
        lstListeners.Add(lsn);
    }

    public bool IsActiveForm(UIForm fm)
    {
        if (fm == null)
            return false;
        return GetActiveForm() == fm;
    }

    public bool IsActiveNonModalForm(UIForm fm)
    {
        if (fm == null)
            return false;
        return GetActiveNonModalForm() == fm;
    }

    public void OnUpdate(float time)
    {
        UIForm fm = GetActiveForm();
        if (fm == null)
            fEllapsSinceNoForm += time;
        else
            fEllapsSinceNoForm = 0;

        m_timeThreshold -= time;
        m_framerateAcc += Time.timeScale / time;
        ++m_framerateCnt;

        if (m_timeThreshold < 0)
        {
            m_avgFPS = m_framerateAcc / m_framerateCnt;
            m_timeThreshold = _updateInterval;
            m_framerateAcc = 0f;
            m_framerateCnt = 0;
        }
    }

    public bool IsBlockingClick()
    {
        return fEllapsSinceNoForm < 0.1f;
    }

    public void RegisterPanel(UIPanel panel)
    {
        if (panel.GetComponent<UIScrollView>() != null)
        {
            //Debug.LogWarning("Don't register a UIScrollView as a ManagedPanel. " + panel.gameObject.name);
            return;
        }
        UIPanel[] panels = panel.GetComponentsInChildren<UIPanel>(true);
        foreach (UIPanel p in panels)
        {
            if (!panelList.Contains(p))
            {
                panelList.Add(p);
            }
        }
    }

    public void UnregisterPanel(UIPanel panel)
    {
        UIPanel[] panels = panel.GetComponentsInChildren<UIPanel>(true);
        foreach (UIPanel p in panels)
        {
            if (panelList.Contains(p))
            {
                panelList.Remove(p);
            }
        }
    }

    public void BringPanelForward(GameObject go)
    {
        UIManagedPanel panel = go.GetComponent<UIManagedPanel>();
        if (panel == null)
        {
            Debug.LogError("BringPanelForward: Can not use BringPanelForward without UIManagedPanel. Name: " + go.name);
            return;
        }
        /*
        if (!panelList.Contains(panel))
        {
            Debug.LogError("BringPanelForward: The panel is not managed, check whether it's top level panel parent has component UIManagedPanel. Name: " + go.name);
            return;
        }
        */
        NGUITools.AdjustDepth(go, 1000);
        NormalizePanelDepths();
    }

    public void BringWidgetForward(GameObject go)
    {
        if (go.GetComponent<UIPanel>() != null)
        {
            Debug.LogError("BringWidgetForward: Can not use BringWidgetForward with UIPanel. Name: " + go.name);
            return;
        }
        UIWidget widget = go.GetComponent<UIWidget>();
        if (widget == null)
        {
            Debug.LogError("BringWidgetForward: Can not use BringWidgetForward without UIWidget. Name: " + go.name);
            return;
        }
        AdjustDepth(go, 1000);
        NormalizeWidgetDepths(NGUITools.FindInParents<UIPanel>(widget.gameObject));
    }

    public void AdjustDepth(GameObject go, int adjustment)
    {
        if (go.GetComponent<UIPanel>() == null)
        {
            List<UIWidget> widgets = new List<UIWidget>();
            UIWidget widget = go.GetComponent<UIWidget>();
            if (widget != null)
                widgets.Add(widget);
            GetWidgetsUnderSamePanel(go, widgets);
            for (int i = 0, imax = widgets.Count; i < imax; ++i)
            {
                UIWidget w = widgets[i];
                w.depth = w.depth + adjustment;
            }
        }
        else
        {
            Debug.LogError("UIManager.AdjustDepth: Only support widget");
        }
    }

    public int GetMinDepth(GameObject go)
    {
        int minDepth = int.MaxValue;
        List<UIWidget> widgets = new List<UIWidget>();
        UIWidget widget = go.GetComponent<UIWidget>();
        if (widget != null)
            widgets.Add(widget);
        GetWidgetsUnderSamePanel(go, widgets);
        if (widgets.Count == 0)
            return 0;
        for (int i = 0, imax = widgets.Count; i < imax; ++i)
        {
            UIWidget w = widgets[i];
            minDepth = Mathf.Min(minDepth, w.depth);
        }
        return minDepth;
    }

    private void NormalizePanelDepths()
    {
        int size = panelList.Count;
        if (size > 0)
        {
            panelList.Sort(UIPanel.CompareFunc);

            int start = 0;
            int current = panelList[0].depth;

            for (int i = 0; i < size; ++i)
            {
                UIPanel p = panelList[i];

                if (p.depth == current)
                {
                    p.depth = start;
                }
                else
                {
                    current = p.depth;
                    p.depth = ++start;
                }
            }
        }
    }

    public void NormalizeWidgetDepths(UIPanel panel)
    {
        List<UIWidget> list = new List<UIWidget>();
        GetWidgetsUnderSamePanel(panel.gameObject, list);
        int size = list.Count;

        if (size > 0)
        {
            list.Sort(UIWidget.FullCompareFunc);

            int start = 0;
            int current = list[0].depth;

            for (int i = 0; i < size; ++i)
            {
                UIWidget w = list[i];

                if (w.depth == current)
                {
                    w.depth = start;
                }
                else
                {
                    current = w.depth;
                    w.depth = ++start;
                }
            }
        }
    }

    private void GetWidgetsUnderSamePanel(GameObject parent, List<UIWidget> widgets)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.GetComponent<UIPanel>() != null)
                continue;
            else
            {
                UIWidget widget = child.GetComponent<UIWidget>();
                if (widget != null)
                    widgets.Add(widget);
                GetWidgetsUnderSamePanel(child.gameObject, widgets);
            }
        }
    }

    public void JumpToUI(string sceneName)
    {
        SceneManager.Instance.ChangeScene(sceneName);
    }

    void _PrintLatency(NetworkConn server, ref float fStartPosY)
    {
        if (server == null || server.m_profiler == null)
            return;

        string strPingInfo = "server: " + server.m_type + " ,latency: " + server.m_profiler.m_avgLatency * 1000.0f + " ms";
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(strPingInfo));
        GUI.color = Color.yellow;
        if (server.m_profiler.m_avgLatency > 100.0f)
            GUI.color = Color.red;
        else if (server.m_profiler.m_avgLatency < 60.0f)
            GUI.color = Color.green;
        GUI.Label(new Rect(0.0f, fStartPosY, nameSize.x, nameSize.y), strPingInfo);
        fStartPosY += nameSize.y;
    }

    public void OnGUI()
    {
        if (Debugger.Instance.m_bEnableDebugMsg)
        {
            float fStartPosY = 0.0f;
            NetworkManager mgr = GameSystem.Instance.mNetworkManager;
            if (mgr != null)
            {
                _PrintLatency(mgr.m_platConn, ref fStartPosY);
                _PrintLatency(mgr.m_gameConn, ref fStartPosY);
            }

            {
                string strFrameInfo = "FPS: " + m_avgFPS;
                Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(strFrameInfo));
                GUI.color = Color.red;
                GUI.Label(new Rect(0.0f, fStartPosY, nameSize.x, nameSize.y), strFrameInfo);
                fStartPosY += nameSize.y;
            }
        }
    }
}