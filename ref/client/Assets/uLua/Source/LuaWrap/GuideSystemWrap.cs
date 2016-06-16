using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class GuideSystemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("Update", Update),
			new LuaMethod("OnLevelWasLoaded", OnLevelWasLoaded),
			new LuaMethod("ModuleClear", ModuleClear),
			new LuaMethod("ReqBeginGuide", ReqBeginGuide),
			new LuaMethod("SendBeginGuideReq", SendBeginGuideReq),
			new LuaMethod("ReqEndGuide", ReqEndGuide),
			new LuaMethod("BeginGuideHandler", BeginGuideHandler),
			new LuaMethod("EndGuideHandler", EndGuideHandler),
			new LuaMethod("AddControlEffect", AddControlEffect),
			new LuaMethod("ShowTip", ShowTip),
			new LuaMethod("BeginPractiseGuide", BeginPractiseGuide),
			new LuaMethod("BeginSkillCastGuide", BeginSkillCastGuide),
			new LuaMethod("New", _CreateGuideSystem),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("guideHiding", get_guideHiding, set_guideHiding),
			new LuaField("curModule", get_curModule, null),
			new LuaField("curStep", get_curStep, null),
			new LuaField("requestingBeingGuide", get_requestingBeingGuide, null),
		};

		LuaScriptMgr.RegisterLib(L, "GuideSystem", typeof(GuideSystem), regs, fields, typeof(Singleton<GuideSystem>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGuideSystem(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GuideSystem obj = new GuideSystem();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GuideSystem.New");
		}

		return 0;
	}

	static Type classType = typeof(GuideSystem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_guideHiding(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideSystem obj = (GuideSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name guideHiding");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index guideHiding on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.guideHiding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curModule(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideSystem obj = (GuideSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curModule");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curModule on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.curModule);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideSystem obj = (GuideSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curStep on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.curStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_requestingBeingGuide(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideSystem obj = (GuideSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name requestingBeingGuide");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index requestingBeingGuide on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.requestingBeingGuide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_guideHiding(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideSystem obj = (GuideSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name guideHiding");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index guideHiding on a nil value");
			}
		}

		obj.guideHiding = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		obj.Init();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLevelWasLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLevelWasLoaded(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ModuleClear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		obj.ModuleClear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReqBeginGuide(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			obj.ReqBeginGuide(arg0);
			return 0;
		}
		else if (count == 3)
		{
			GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
			uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			bool o = obj.ReqBeginGuide(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GuideSystem.ReqBeginGuide");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendBeginGuideReq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.SendBeginGuideReq(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReqEndGuide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.ReqEndGuide(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginGuideHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.BeginGuideHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndGuideHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.EndGuideHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddControlEffect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		List<ControlEffect> arg0 = (List<ControlEffect>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<ControlEffect>));
		List<Vector2> arg1 = (List<Vector2>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Vector2>));
		Transform arg2 = (Transform)LuaScriptMgr.GetUnityObject(L, 4, typeof(Transform));
		UIWidget arg3 = (UIWidget)LuaScriptMgr.GetUnityObject(L, 5, typeof(UIWidget));
		obj.AddControlEffect(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		GuideStep arg0 = (GuideStep)LuaScriptMgr.GetNetObject(L, 2, typeof(GuideStep));
		obj.ShowTip(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginPractiseGuide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		obj.BeginPractiseGuide();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginSkillCastGuide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideSystem obj = (GuideSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GuideSystem");
		obj.BeginSkillCastGuide();
		return 0;
	}
}

