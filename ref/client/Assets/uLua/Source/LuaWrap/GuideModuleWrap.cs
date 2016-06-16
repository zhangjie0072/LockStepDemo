using System;
using System.Collections.Generic;
using LuaInterface;

public class GuideModuleWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateGuideModule),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("ID", get_ID, set_ID),
			new LuaField("uiName", get_uiName, set_uiName),
			new LuaField("conditionTypes", get_conditionTypes, set_conditionTypes),
			new LuaField("conditionParams", get_conditionParams, set_conditionParams),
			new LuaField("firstStep", get_firstStep, set_firstStep),
			new LuaField("endStep", get_endStep, set_endStep),
			new LuaField("restartStep", get_restartStep, set_restartStep),
			new LuaField("linkID", get_linkID, set_linkID),
			new LuaField("linkSubID", get_linkSubID, set_linkSubID),
			new LuaField("nextModule", get_nextModule, set_nextModule),
			new LuaField("skipConditions", get_skipConditions, set_skipConditions),
			new LuaField("skipConditionParams", get_skipConditionParams, set_skipConditionParams),
		};

		LuaScriptMgr.RegisterLib(L, "GuideModule", typeof(GuideModule), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGuideModule(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GuideModule obj = new GuideModule();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GuideModule.New");
		}

		return 0;
	}

	static Type classType = typeof(GuideModule);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.uiName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_conditionTypes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionTypes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionTypes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.conditionTypes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_conditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionParams on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.conditionParams);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_firstStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstStep on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.firstStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_endStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endStep on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.endStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_restartStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name restartStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index restartStep on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.restartStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_linkID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linkID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linkID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.linkID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_linkSubID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linkSubID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linkSubID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.linkSubID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nextModule(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nextModule");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nextModule on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.nextModule);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skipConditions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skipConditions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skipConditions on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skipConditions);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skipConditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skipConditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skipConditionParams on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skipConditionParams);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		obj.ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiName on a nil value");
			}
		}

		obj.uiName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_conditionTypes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionTypes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionTypes on a nil value");
			}
		}

		obj.conditionTypes = (List<ConditionType>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ConditionType>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_conditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionParams on a nil value");
			}
		}

		obj.conditionParams = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_firstStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstStep on a nil value");
			}
		}

		obj.firstStep = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_endStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endStep on a nil value");
			}
		}

		obj.endStep = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_restartStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name restartStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index restartStep on a nil value");
			}
		}

		obj.restartStep = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_linkID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linkID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linkID on a nil value");
			}
		}

		obj.linkID = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_linkSubID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linkSubID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linkSubID on a nil value");
			}
		}

		obj.linkSubID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_nextModule(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nextModule");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nextModule on a nil value");
			}
		}

		obj.nextModule = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skipConditions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skipConditions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skipConditions on a nil value");
			}
		}

		obj.skipConditions = (List<ConditionType>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ConditionType>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skipConditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideModule obj = (GuideModule)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skipConditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skipConditionParams on a nil value");
			}
		}

		obj.skipConditionParams = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}
}

