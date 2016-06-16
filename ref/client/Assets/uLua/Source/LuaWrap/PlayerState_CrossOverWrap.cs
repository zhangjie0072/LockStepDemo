using System;
using LuaInterface;

public class PlayerState_CrossOverWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("CalcCrossRate", CalcCrossRate),
			new LuaMethod("New", _CreatePlayerState_CrossOver),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("crossed", get_crossed, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_CrossOver", typeof(PlayerState_CrossOver), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_CrossOver(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_CrossOver obj = new PlayerState_CrossOver(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_CrossOver.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_CrossOver);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_crossed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_CrossOver obj = (PlayerState_CrossOver)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name crossed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index crossed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.crossed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_CrossOver obj = (PlayerState_CrossOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_CrossOver");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_CrossOver obj = (PlayerState_CrossOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_CrossOver");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalcCrossRate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.Number o = PlayerState_CrossOver.CalcCrossRate(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}
}

