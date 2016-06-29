using System;
using System.Collections.Generic;
using LuaInterface;

public class GameSceneWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("RegisterListener", RegisterListener),
			new LuaMethod("RemoveAllListeners", RemoveAllListeners),
			new LuaMethod("OnLevelWasLoaded", OnLevelWasLoaded),
			new LuaMethod("CreateBall", CreateBall),
			new LuaMethod("DestroyBall", DestroyBall),
			new LuaMethod("OnDunkLeaveGround", OnDunkLeaveGround),
			new LuaMethod("OnDrawGizmos", OnDrawGizmos),
			new LuaMethod("New", _CreateGameScene),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("balls", get_balls, set_balls),
			new LuaField("removeBalls", get_removeBalls, set_removeBalls),
			new LuaField("loaded", get_loaded, set_loaded),
			new LuaField("onDebugDraw", get_onDebugDraw, set_onDebugDraw),
			new LuaField("mSceneInfo", get_mSceneInfo, set_mSceneInfo),
			new LuaField("mGround", get_mGround, null),
			new LuaField("mBasket", get_mBasket, null),
			new LuaField("mBall", get_mBall, null),
			new LuaField("m_endings", get_m_endings, null),
		};

		LuaScriptMgr.RegisterLib(L, "GameScene", typeof(GameScene), regs, fields, typeof(PlayerStateMachine.Listener));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameScene(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			fogs.proto.msg.Scene arg0 = (fogs.proto.msg.Scene)LuaScriptMgr.GetNetObject(L, 1, typeof(fogs.proto.msg.Scene));
			GameScene obj = new GameScene(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameScene.New");
		}

		return 0;
	}

	static Type classType = typeof(GameScene);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_balls(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name balls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index balls on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.balls);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_removeBalls(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name removeBalls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index removeBalls on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.removeBalls);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loaded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loaded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loaded on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.loaded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDebugDraw(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDebugDraw");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDebugDraw on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDebugDraw);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mSceneInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mSceneInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mSceneInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mSceneInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mGround(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mGround");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mGround on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mGround);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mBasket(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mBasket");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mBasket on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mBasket);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_endings(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_endings");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_endings on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_endings);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_balls(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name balls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index balls on a nil value");
			}
		}

		obj.balls = (List<UBasketball>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<UBasketball>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_removeBalls(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name removeBalls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index removeBalls on a nil value");
			}
		}

		obj.removeBalls = (List<UBasketball>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<UBasketball>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_loaded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loaded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loaded on a nil value");
			}
		}

		obj.loaded = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDebugDraw(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDebugDraw");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDebugDraw on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDebugDraw = (GameScene.DEBUG_DRAW_DELEGATE)LuaScriptMgr.GetNetObject(L, 3, typeof(GameScene.DEBUG_DRAW_DELEGATE));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDebugDraw = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mSceneInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameScene obj = (GameScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mSceneInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mSceneInfo on a nil value");
			}
		}

		obj.mSceneInfo = (fogs.proto.msg.Scene)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.Scene));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		GameScene.GameSceneBuildListener arg0 = (GameScene.GameSceneBuildListener)LuaScriptMgr.GetNetObject(L, 2, typeof(GameScene.GameSceneBuildListener));
		obj.RegisterListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAllListeners(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		obj.RemoveAllListeners();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLevelWasLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLevelWasLoaded(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		UBasketball o = obj.CreateBall();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.DestroyBall(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunkLeaveGround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		obj.OnDunkLeaveGround(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrawGizmos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameScene obj = (GameScene)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameScene");
		obj.OnDrawGizmos();
		return 0;
	}
}

