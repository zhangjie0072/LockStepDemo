using System;
using LuaInterface;

public class BlockableWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("Update", Update),
			new LuaMethod("Clear", Clear),
			new LuaMethod("New", _CreateBlockable),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("blockable", get_blockable, null),
			new LuaField("tooEarly", get_tooEarly, null),
			new LuaField("tooLate", get_tooLate, null),
		};

		LuaScriptMgr.RegisterLib(L, "Blockable", typeof(Blockable), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBlockable(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			Blockable obj = new Blockable(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Blockable.New");
		}

		return 0;
	}

	static Type classType = typeof(Blockable);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_blockable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Blockable obj = (Blockable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name blockable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index blockable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.blockable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tooEarly(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Blockable obj = (Blockable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tooEarly");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tooEarly on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tooEarly);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tooLate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Blockable obj = (Blockable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tooLate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tooLate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tooLate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Blockable obj = (Blockable)LuaScriptMgr.GetNetObjectSelf(L, 1, "Blockable");
		PlayerAnimAttribute.AnimAttr arg0 = (PlayerAnimAttribute.AnimAttr)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerAnimAttribute.AnimAttr));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		obj.Init(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Blockable obj = (Blockable)LuaScriptMgr.GetNetObjectSelf(L, 1, "Blockable");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Blockable obj = (Blockable)LuaScriptMgr.GetNetObjectSelf(L, 1, "Blockable");
		obj.Clear();
		return 0;
	}
}

