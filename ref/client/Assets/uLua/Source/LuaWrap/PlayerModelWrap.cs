using System;
using UnityEngine;
using LuaInterface;

public class PlayerModelWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("EnableDrag", EnableDrag),
			new LuaMethod("RestoreMaterial", RestoreMaterial),
			new LuaMethod("SetSparkMaterial", SetSparkMaterial),
			new LuaMethod("SetMainColor", SetMainColor),
			new LuaMethod("EnableGrey", EnableGrey),
			new LuaMethod("_DressUp", _DressUp),
			new LuaMethod("_DressDown", _DressDown),
			new LuaMethod("_FittingUp", _FittingUp),
			new LuaMethod("_FittingDown", _FittingDown),
			new LuaMethod("GetBone", GetBone),
			new LuaMethod("DressOnFashion", DressOnFashion),
			new LuaMethod("DressDownFashion", DressDownFashion),
			new LuaMethod("Init", Init),
			new LuaMethod("OnLoadPlayerBody", OnLoadPlayerBody),
			new LuaMethod("New", _CreatePlayerModel),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("gameObject", get_gameObject, set_gameObject),
			new LuaField("mModelTagPts", get_mModelTagPts, set_mModelTagPts),
			new LuaField("root", get_root, set_root),
			new LuaField("ball", get_ball, set_ball),
			new LuaField("head", get_head, set_head),
			new LuaField("layerName", null, set_layerName),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerModel", typeof(PlayerModel), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerModel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			PlayerModel obj = new PlayerModel(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerModel.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerModel);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameObject(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameObject on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gameObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mModelTagPts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mModelTagPts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mModelTagPts on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.mModelTagPts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_root(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.root);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ball(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ball");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ball on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ball);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_head(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name head");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index head on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.head);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameObject(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameObject on a nil value");
			}
		}

		obj.gameObject = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mModelTagPts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mModelTagPts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mModelTagPts on a nil value");
			}
		}

		obj.mModelTagPts = LuaScriptMgr.GetArrayObject<ModelTagPoint>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_root(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}

		obj.root = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ball(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ball");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ball on a nil value");
			}
		}

		obj.ball = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_head(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name head");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index head on a nil value");
			}
		}

		obj.head = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layerName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerModel obj = (PlayerModel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerName on a nil value");
			}
		}

		obj.layerName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		obj.EnableDrag();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RestoreMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		obj.RestoreMaterial();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSparkMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 2, typeof(Material));
		obj.SetSparkMaterial(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetMainColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.SetMainColor(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableGrey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.EnableGrey(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _DressUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj._DressUp(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _DressDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj._DressDown(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _FittingUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj._FittingUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _FittingDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj._FittingDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBone(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = obj.GetBone(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DressOnFashion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.DressOnFashion(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DressDownFashion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.DressDownFashion(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		RoleShape arg0 = (RoleShape)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleShape));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.Init(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLoadPlayerBody(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerModel obj = (PlayerModel)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerModel");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.OnLoadPlayerBody(arg0);
		return 0;
	}
}

