using System;
using LuaInterface;

public class fogs_proto_msg_EnhanceExerciseRespWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_EnhanceExerciseResp),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("result", get_result, set_result),
			new LuaField("role_id", get_role_id, set_role_id),
			new LuaField("exercise", get_exercise, null),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.EnhanceExerciseResp", typeof(fogs.proto.msg.EnhanceExerciseResp), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_EnhanceExerciseResp(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.EnhanceExerciseResp obj = new fogs.proto.msg.EnhanceExerciseResp();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.EnhanceExerciseResp.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.EnhanceExerciseResp);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_result(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EnhanceExerciseResp obj = (fogs.proto.msg.EnhanceExerciseResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name result");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index result on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.result);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EnhanceExerciseResp obj = (fogs.proto.msg.EnhanceExerciseResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.role_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_exercise(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EnhanceExerciseResp obj = (fogs.proto.msg.EnhanceExerciseResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exercise");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exercise on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.exercise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_result(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EnhanceExerciseResp obj = (fogs.proto.msg.EnhanceExerciseResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name result");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index result on a nil value");
			}
		}

		obj.result = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EnhanceExerciseResp obj = (fogs.proto.msg.EnhanceExerciseResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_id on a nil value");
			}
		}

		obj.role_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

