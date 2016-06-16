using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AnimationRespWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AddResp", AddResp),
			new LuaMethod("RemoveResp", RemoveResp),
			new LuaMethod("OnResp", OnResp),
			new LuaMethod("New", _CreateAnimationResp),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("respDel", get_respDel, set_respDel),
		};

		LuaScriptMgr.RegisterLib(L, "AnimationResp", typeof(AnimationResp), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAnimationResp(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AnimationResp class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(AnimationResp);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_respDel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationResp obj = (AnimationResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name respDel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index respDel on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.respDel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_respDel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationResp obj = (AnimationResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name respDel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index respDel on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.respDel = (AnimationResp.RespDel)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationResp.RespDel));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.respDel = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddResp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AnimationResp obj = (AnimationResp)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationResp");
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 2);
		GameObject arg1 = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		obj.AddResp(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveResp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AnimationResp obj = (AnimationResp)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationResp");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.RemoveResp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnResp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AnimationResp obj = (AnimationResp)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationResp");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.OnResp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

