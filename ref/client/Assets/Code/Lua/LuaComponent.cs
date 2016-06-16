using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;

public class LuaComponent : MonoBehaviour
{
	public class ScriptInfo
	{
		public string file;
		public string scriptName;
		public LuaTable metaTable;
		public LuaFunction funcAwake;
		public LuaFunction funcStart;
		public LuaFunction funcUpdate;
		public LuaFunction funcFixedUpdate;
		public LuaFunction funcOnDestroy;
		public LuaFunction funcOnClose;
		public LuaFunction funcOnEnable;
		public LuaFunction funcOnDisable;
		public int instanceIndex = 0;
		//public Queue<int> recycledIndex = new Queue<int>();
	}
	[System.Serializable]
	public struct Param
	{
		public string name;
		public string value;
	}
	public string scriptFile;
	public Param[] parameters;
	public LuaTable table;

	ScriptInfo info = null;
	string tableName;
	//int index;
	Animator animator;
	bool animatorDisabledByFinish;
	bool initialEnabled;
	bool animatorShadeEnabled;
    bool isTriggerClose = false;

	static Dictionary<string, ScriptInfo> scriptInfos = new Dictionary<string, ScriptInfo>();
    static LuaCSFunction funcAnimClose = new LuaCSFunction(LuaComponent.AnimClose);

	void Awake()
	{
		animator = GetComponent<Animator>();
		if (animator != null)
			initialEnabled = animator.enabled;

		if (!scriptInfos.TryGetValue(scriptFile, out info))
		{
			//load script file
			LuaTable metaTable = null;
			string scriptName = "";
			try
			{
				object[] retVals = LuaScriptMgr.Instance.DoFile("Custom/" + scriptFile);
				if (retVals != null && retVals.Length > 0)
				{
					metaTable = retVals[0] as LuaTable;
				}
			}
			catch (LuaException exception)
			{
				Logger.LogError(exception.Message + " File: " + scriptName + " GameObject: " + name);
			}
			if (metaTable == null)
				Logger.LogError("The script: " + scriptFile + " didn't return a Meta Table.");
			metaTable.Set("__index", metaTable);
			scriptName = metaTable["uiName"] as string;
			if (string.IsNullOrEmpty(scriptName))
				Logger.LogError("The script: " + scriptFile + " didn't has field 'uiName'.");

			info = new ScriptInfo();
			info.file = scriptFile;
			info.scriptName = scriptName;
			info.metaTable = metaTable;
			object o = metaTable["Awake"];
			if (o != null)
				info.funcAwake = o as LuaFunction;
			o = metaTable["Start"];
			if (o != null)
				info.funcStart = o as LuaFunction;
			o = metaTable["Update"];
			if (o != null)
				info.funcUpdate = o as LuaFunction;
			o = metaTable["FixedUpdate"];
			if (o != null)
				info.funcFixedUpdate = o as LuaFunction;
			o = metaTable["OnDestroy"];
			if (o != null)
				info.funcOnDestroy = o as LuaFunction;
			o = metaTable["OnClose"];
			if (o != null)
				info.funcOnClose = o as LuaFunction;
			o = metaTable["OnEnable"];
			if (o != null)
				info.funcOnEnable = o as LuaFunction;
			o = metaTable["OnDisable"];
			if (o != null)
				info.funcOnDisable = o as LuaFunction;
			scriptInfos.Add(scriptFile, info);

			if (animator != null)
				metaTable.Set("AnimClose", funcAnimClose);
		}

		//create instance
		table = CreateInstanceTable();
		table.SetMetaTable(info.metaTable);
		table.Set("transform", transform);
		table.Set("gameObject", gameObject);
		//table.Set("com", this);
		//set parameters
		if (parameters != null)
		{
			foreach (Param param in parameters)
			{
				double d;
				bool b;
				if (double.TryParse(param.value, out d))
					table.Set(param.name, d);
				else if (bool.TryParse(param.value, out b))
					table.Set(param.name, b);
				else
					table.Set(param.name, param.value);
			}
		}

        //Time.realtimeSinceStartup

		if (info.funcAwake != null)
			info.funcAwake.Call(table);
	}

	LuaTable CreateInstanceTable()
	{
		LuaTable t = null;
		//if (info.recycledIndex.Count > 0)
		//	index = info.recycledIndex.Dequeue();
		//else
		//	index = info.instanceIndex++;
		//tableName = info.scriptName + "_" + index;
		
		//LuaScriptMgr.Instance.DoString(tableName + " = {}");
		//t = LuaScriptMgr.Instance.GetLuaTable(tableName);
		t = LuaScriptMgr.Instance.lua.NewTable();

		return t;
	}

	void Start()
	{
		if (info.funcStart != null)
			info.funcStart.Call(table);
		table.Set("started", true);
		LuaFunction funcOnStarted = table["onStarted"] as LuaFunction;
		if (funcOnStarted != null)
			funcOnStarted.Call();
	}

	//Lua中是否有必要Update？
	//void Update()
	//{
	//	if (info.funcUpdate != null)
	//		info.funcUpdate.Call(table);
	//}

	void FixedUpdate()
	{
		if (info.funcFixedUpdate != null)
			info.funcFixedUpdate.Call(table);
	}

	void OnDestroy()
	{
		if (info.funcOnDestroy != null)
			info.funcOnDestroy.Call(table);

		table.Release();
		//Logger.Log("LuaComponent.OnDestroy " + tableName);

		//info.recycledIndex.Enqueue(index);
		if (animatorShadeEnabled)
			CommonFunction.SetAnimatorShade(false);
        LuaInterface.LuaTable topPanelMgr = LuaScriptMgr.Instance.GetLuaTable("TopPanelManager");
        LuaInterface.LuaFunction SetLuaComNil = topPanelMgr["SetLuaComNil"] as LuaInterface.LuaFunction;
        SetLuaComNil.Call(new object[] { topPanelMgr, info.scriptName });
	}

	void OnEnable()
	{
        StartCoroutine(PlayStartAnimation());
    }

	void OnDisable()
	{
        if (info.funcOnDisable != null)
			info.funcOnDisable.Call(table);
	}

    IEnumerator PlayStartAnimation()
    {
        yield return new WaitForEndOfFrame();
        if (animatorDisabledByFinish && initialEnabled)
        {
            animator.enabled = true;
            animatorDisabledByFinish = false;
        }
        if (info.funcOnEnable != null)
            info.funcOnEnable.Call(table);
    }

    IEnumerator DisableAnimator()
	{
		yield return new WaitForEndOfFrame();
		animator.enabled = false;
		animatorDisabledByFinish = true;
	}

	void OnFinish()
	{
		if (animator != null)
		{
			bool inTrans = animator.IsInTransition(0);
			if (!inTrans && !isTriggerClose )
			{
				StartCoroutine(DisableAnimator());
			}
		}
		CommonFunction.SetAnimatorShade(false);
	}

	void OnClose()
	{
		CommonFunction.SetAnimatorShade(false);
		animatorShadeEnabled = false;
		if (info.funcOnClose != null)
			info.funcOnClose.Call(table);
        isTriggerClose = false;
	}

	static int AnimClose(System.IntPtr L)
	{
		object obj = LuaScriptMgr.GetVarTable(L, -1);
		LuaTable t = (LuaTable)obj;
		GameObject go = (GameObject)(t["gameObject"]);
		Animator animator = go.GetComponent<Animator>();
		animator.enabled = true;
        animator.SetBool("Close", true);
        CommonFunction.SetAnimatorShade(true);
		go.GetComponent<LuaComponent>().animatorShadeEnabled = true;
		go.GetComponent<LuaComponent>().isTriggerClose= true;
		return 0;
	}
}