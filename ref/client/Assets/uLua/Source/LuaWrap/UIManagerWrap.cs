using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class UIManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("RemoveAll", RemoveAll),
			new LuaMethod("UnintializeUI", UnintializeUI),
			new LuaMethod("ShowSpecifiedUI", ShowSpecifiedUI),
			new LuaMethod("IsUIActived", IsUIActived),
			new LuaMethod("OnLevelWasLoaded", OnLevelWasLoaded),
			new LuaMethod("DestroyBasePanelObjects", DestroyBasePanelObjects),
			new LuaMethod("AdaptiveUI", AdaptiveUI),
			new LuaMethod("CreateUI", CreateUI),
			new LuaMethod("CreateUIRoot", CreateUIRoot),
			new LuaMethod("ShowMask", ShowMask),
			new LuaMethod("ForceShowMask", ForceShowMask),
			new LuaMethod("DisplayError", DisplayError),
			new LuaMethod("ShowUIForm", ShowUIForm),
			new LuaMethod("ShowUIFormModal", ShowUIFormModal),
			new LuaMethod("HideUIForm", HideUIForm),
			new LuaMethod("HideAllNonModal", HideAllNonModal),
			new LuaMethod("GetActiveForm", GetActiveForm),
			new LuaMethod("GetActiveNonModalForm", GetActiveNonModalForm),
			new LuaMethod("AddFormChangeListener", AddFormChangeListener),
			new LuaMethod("IsActiveForm", IsActiveForm),
			new LuaMethod("IsActiveNonModalForm", IsActiveNonModalForm),
			new LuaMethod("OnUpdate", OnUpdate),
			new LuaMethod("IsBlockingClick", IsBlockingClick),
			new LuaMethod("RegisterPanel", RegisterPanel),
			new LuaMethod("UnregisterPanel", UnregisterPanel),
			new LuaMethod("BringPanelForward", BringPanelForward),
			new LuaMethod("BringWidgetForward", BringWidgetForward),
			new LuaMethod("AdjustDepth", AdjustDepth),
			new LuaMethod("GetMinDepth", GetMinDepth),
			new LuaMethod("NormalizeWidgetDepths", NormalizeWidgetDepths),
			new LuaMethod("JumpToUI", JumpToUI),
			new LuaMethod("OnGUI", OnGUI),
			new LuaMethod("New", _CreateUIManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("lstModalForms", get_lstModalForms, set_lstModalForms),
			new LuaField("lstForms", get_lstForms, set_lstForms),
			new LuaField("curLeagueType", get_curLeagueType, set_curLeagueType),
			new LuaField("lstListeners", get_lstListeners, set_lstListeners),
			new LuaField("m_uiRootBasePanel", get_m_uiRootBasePanel, set_m_uiRootBasePanel),
			new LuaField("m_ratioScale", get_m_ratioScale, set_m_ratioScale),
			new LuaField("m_uiRoot", get_m_uiRoot, set_m_uiRoot),
			new LuaField("LoginCtrl", get_LoginCtrl, set_LoginCtrl),
			new LuaField("LoadingCtrl", get_LoadingCtrl, set_LoadingCtrl),
			new LuaField("showTeamUpgrade", get_showTeamUpgrade, set_showTeamUpgrade),
			new LuaField("isInMatchLoading", get_isInMatchLoading, set_isInMatchLoading),
			new LuaField("Instance", get_Instance, null),
			new LuaField("m_uiCamera", get_m_uiCamera, null),
			new LuaField("AvgFPS", get_AvgFPS, null),
		};

		LuaScriptMgr.RegisterLib(L, "UIManager", typeof(UIManager), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UIManager obj = new UIManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UIManager.New");
		}

		return 0;
	}

	static Type classType = typeof(UIManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lstModalForms(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstModalForms");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstModalForms on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.lstModalForms);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lstForms(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstForms");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstForms on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.lstForms);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curLeagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curLeagueType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curLeagueType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.curLeagueType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lstListeners(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstListeners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstListeners on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.lstListeners);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiRootBasePanel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiRootBasePanel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiRootBasePanel on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiRootBasePanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ratioScale(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIManager.m_ratioScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiRoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiRoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiRoot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiRoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LoginCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LoginCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LoginCtrl on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LoginCtrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LoadingCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LoadingCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LoadingCtrl on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LoadingCtrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showTeamUpgrade(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showTeamUpgrade");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showTeamUpgrade on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showTeamUpgrade);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isInMatchLoading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isInMatchLoading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isInMatchLoading on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isInMatchLoading);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, UIManager.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiCamera on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AvgFPS(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AvgFPS");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AvgFPS on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.AvgFPS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lstModalForms(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstModalForms");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstModalForms on a nil value");
			}
		}

		obj.lstModalForms = (List<UIForm>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<UIForm>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lstForms(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstForms");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstForms on a nil value");
			}
		}

		obj.lstForms = (List<UIForm>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<UIForm>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_curLeagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curLeagueType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curLeagueType on a nil value");
			}
		}

		obj.curLeagueType = (GameMatch.LeagueType)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.LeagueType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lstListeners(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lstListeners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lstListeners on a nil value");
			}
		}

		obj.lstListeners = (List<UIManager.FormChangeListener>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<UIManager.FormChangeListener>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiRootBasePanel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiRootBasePanel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiRootBasePanel on a nil value");
			}
		}

		obj.m_uiRootBasePanel = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_ratioScale(IntPtr L)
	{
		UIManager.m_ratioScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiRoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiRoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiRoot on a nil value");
			}
		}

		obj.m_uiRoot = (UIRoot)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIRoot));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LoginCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LoginCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LoginCtrl on a nil value");
			}
		}

		obj.LoginCtrl = (UILoginControl)LuaScriptMgr.GetNetObject(L, 3, typeof(UILoginControl));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LoadingCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LoadingCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LoadingCtrl on a nil value");
			}
		}

		obj.LoadingCtrl = (UILoadingControl)LuaScriptMgr.GetNetObject(L, 3, typeof(UILoadingControl));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showTeamUpgrade(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showTeamUpgrade");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showTeamUpgrade on a nil value");
			}
		}

		obj.showTeamUpgrade = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isInMatchLoading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIManager obj = (UIManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isInMatchLoading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isInMatchLoading on a nil value");
			}
		}

		obj.isInMatchLoading = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.RemoveAll();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnintializeUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.UnintializeUI();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowSpecifiedUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.ShowSpecifiedUI(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsUIActived(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		bool o = obj.IsUIActived();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLevelWasLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLevelWasLoaded(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyBasePanelObjects(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.DestroyBasePanelObjects();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdaptiveUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		UIManager.AdaptiveUI();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		GameObject o = obj.CreateUI(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateUIRoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.CreateUIRoot(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowMask(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.ShowMask(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceShowMask(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.ForceShowMask(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DisplayError(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.DisplayError(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUIForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm arg0 = (UIForm)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIForm));
		UIForm.ShowHideDirection arg1 = (UIForm.ShowHideDirection)LuaScriptMgr.GetNetObject(L, 3, typeof(UIForm.ShowHideDirection));
		obj.ShowUIForm(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUIFormModal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm arg0 = (UIForm)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIForm));
		UIForm.ShowHideDirection arg1 = (UIForm.ShowHideDirection)LuaScriptMgr.GetNetObject(L, 3, typeof(UIForm.ShowHideDirection));
		obj.ShowUIFormModal(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideUIForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm arg0 = (UIForm)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIForm));
		UIForm.ShowHideDirection arg1 = (UIForm.ShowHideDirection)LuaScriptMgr.GetNetObject(L, 3, typeof(UIForm.ShowHideDirection));
		obj.HideUIForm(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideAllNonModal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.HideAllNonModal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetActiveForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm o = obj.GetActiveForm();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetActiveNonModalForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm o = obj.GetActiveNonModalForm();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddFormChangeListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIManager.FormChangeListener arg0 = (UIManager.FormChangeListener)LuaScriptMgr.GetNetObject(L, 2, typeof(UIManager.FormChangeListener));
		obj.AddFormChangeListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActiveForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm arg0 = (UIForm)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIForm));
		bool o = obj.IsActiveForm(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActiveNonModalForm(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIForm arg0 = (UIForm)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIForm));
		bool o = obj.IsActiveNonModalForm(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.OnUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsBlockingClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		bool o = obj.IsBlockingClick();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterPanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIPanel arg0 = (UIPanel)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIPanel));
		obj.RegisterPanel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnregisterPanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIPanel arg0 = (UIPanel)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIPanel));
		obj.UnregisterPanel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BringPanelForward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.BringPanelForward(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BringWidgetForward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.BringWidgetForward(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdjustDepth(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.AdjustDepth(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMinDepth(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		int o = obj.GetMinDepth(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int NormalizeWidgetDepths(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		UIPanel arg0 = (UIPanel)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIPanel));
		obj.NormalizeWidgetDepths(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int JumpToUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.JumpToUI(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIManager obj = (UIManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIManager");
		obj.OnGUI();
		return 0;
	}
}

