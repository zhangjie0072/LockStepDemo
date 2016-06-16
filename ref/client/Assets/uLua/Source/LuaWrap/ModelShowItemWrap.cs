using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ModelShowItemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("HideExcept", HideExcept),
			new LuaMethod("ResumeHidden", ResumeHidden),
			new LuaMethod("ShowModel", ShowModel),
			new LuaMethod("New", _CreateModelShowItem),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("_playerModel", get__playerModel, set__playerModel),
			new LuaField("scaleValue", get_scaleValue, set_scaleValue),
			new LuaField("_layerName", get__layerName, set__layerName),
			new LuaField("Mute", get_Mute, set_Mute),
			new LuaField("ModelID", get_ModelID, set_ModelID),
			new LuaField("ModelScale", null, set_ModelScale),
			new LuaField("PlayNeedBall", null, set_PlayNeedBall),
			new LuaField("Rotation", null, set_Rotation),
			new LuaField("IsFashion", null, set_IsFashion),
		};

		LuaScriptMgr.RegisterLib(L, "ModelShowItem", typeof(ModelShowItem), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateModelShowItem(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ModelShowItem class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(ModelShowItem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__playerModel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _playerModel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _playerModel on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._playerModel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scaleValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleValue on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scaleValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__layerName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _layerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _layerName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._layerName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mute(IntPtr L)
	{
		LuaScriptMgr.Push(L, ModelShowItem.Mute);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ModelID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ModelID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ModelID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ModelID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__playerModel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _playerModel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _playerModel on a nil value");
			}
		}

		obj._playerModel = (PlayerModel)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerModel));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scaleValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleValue on a nil value");
			}
		}

		obj.scaleValue = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__layerName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _layerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _layerName on a nil value");
			}
		}

		obj._layerName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Mute(IntPtr L)
	{
		ModelShowItem.Mute = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ModelID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ModelID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ModelID on a nil value");
			}
		}

		obj.ModelID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ModelScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ModelScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ModelScale on a nil value");
			}
		}

		obj.ModelScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PlayNeedBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PlayNeedBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PlayNeedBall on a nil value");
			}
		}

		obj.PlayNeedBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Rotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Rotation on a nil value");
			}
		}

		obj.Rotation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IsFashion(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ModelShowItem obj = (ModelShowItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsFashion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsFashion on a nil value");
			}
		}

		obj.IsFashion = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideExcept(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ModelShowItem arg0 = (ModelShowItem)LuaScriptMgr.GetUnityObject(L, 1, typeof(ModelShowItem));
		ModelShowItem.HideExcept(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResumeHidden(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ModelShowItem.ResumeHidden();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowModel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ModelShowItem obj = (ModelShowItem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ModelShowItem");
		obj.ShowModel();
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

