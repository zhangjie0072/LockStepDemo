using System;
using LuaInterface;

public class NewComerTrialInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateNewComerTrialInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("total_score", get_total_score, set_total_score),
			new LuaField("awards_flag", get_awards_flag, set_awards_flag),
		};

		LuaScriptMgr.RegisterLib(L, "NewComerTrialInfo", typeof(fogs.proto.msg.NewComerTrialInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNewComerTrialInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.NewComerTrialInfo obj = new fogs.proto.msg.NewComerTrialInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.NewComerTrialInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.NewComerTrialInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_total_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerTrialInfo obj = (fogs.proto.msg.NewComerTrialInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.total_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awards_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerTrialInfo obj = (fogs.proto.msg.NewComerTrialInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards_flag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awards_flag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_total_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerTrialInfo obj = (fogs.proto.msg.NewComerTrialInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_score on a nil value");
			}
		}

		obj.total_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awards_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerTrialInfo obj = (fogs.proto.msg.NewComerTrialInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards_flag on a nil value");
			}
		}

		obj.awards_flag = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

