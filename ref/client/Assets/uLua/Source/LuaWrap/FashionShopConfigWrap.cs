using System;
using System.Collections.Generic;
using LuaInterface;

public class FashionShopConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("GetReputationConfig", GetReputationConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateFashionShopConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
			new LuaField("configsSort", get_configsSort, set_configsSort),
			new LuaField("configsSort_m", get_configsSort_m, set_configsSort_m),
			new LuaField("configsSort_w", get_configsSort_w, set_configsSort_w),
			new LuaField("cHeadSort", get_cHeadSort, set_cHeadSort),
			new LuaField("cClothesSort", get_cClothesSort, set_cClothesSort),
			new LuaField("cTrouserseSort", get_cTrouserseSort, set_cTrouserseSort),
			new LuaField("cShoesSort", get_cShoesSort, set_cShoesSort),
			new LuaField("cBackSort", get_cBackSort, set_cBackSort),
			new LuaField("cSuiteSort", get_cSuiteSort, set_cSuiteSort),
			new LuaField("cHeadSort_w", get_cHeadSort_w, set_cHeadSort_w),
			new LuaField("cClothesSort_w", get_cClothesSort_w, set_cClothesSort_w),
			new LuaField("cTrouserseSort_w", get_cTrouserseSort_w, set_cTrouserseSort_w),
			new LuaField("cShoesSort_w", get_cShoesSort_w, set_cShoesSort_w),
			new LuaField("cBackSort_w", get_cBackSort_w, set_cBackSort_w),
			new LuaField("cSuiteSort_w", get_cSuiteSort_w, set_cSuiteSort_w),
			new LuaField("reputationConfigs", get_reputationConfigs, set_reputationConfigs),
			new LuaField("reputationConfigsSort", get_reputationConfigsSort, set_reputationConfigsSort),
			new LuaField("reputationConfigsSort_m", get_reputationConfigsSort_m, set_reputationConfigsSort_m),
			new LuaField("reputationConfigsSort_w", get_reputationConfigsSort_w, set_reputationConfigsSort_w),
			new LuaField("reputationHeadSort", get_reputationHeadSort, set_reputationHeadSort),
			new LuaField("reputationClothesSort", get_reputationClothesSort, set_reputationClothesSort),
			new LuaField("reputationTrouserseSort", get_reputationTrouserseSort, set_reputationTrouserseSort),
			new LuaField("reputationShoesSort", get_reputationShoesSort, set_reputationShoesSort),
			new LuaField("reputationBackSort", get_reputationBackSort, set_reputationBackSort),
			new LuaField("reputationSuiteSort", get_reputationSuiteSort, set_reputationSuiteSort),
			new LuaField("reputationHeadSort_w", get_reputationHeadSort_w, set_reputationHeadSort_w),
			new LuaField("reputationClothesSort_w", get_reputationClothesSort_w, set_reputationClothesSort_w),
			new LuaField("reputationTrouserseSort_w", get_reputationTrouserseSort_w, set_reputationTrouserseSort_w),
			new LuaField("reputationShoesSort_w", get_reputationShoesSort_w, set_reputationShoesSort_w),
			new LuaField("reputationBackSort_w", get_reputationBackSort_w, set_reputationBackSort_w),
			new LuaField("reputationSuiteSort_w", get_reputationSuiteSort_w, set_reputationSuiteSort_w),
		};

		LuaScriptMgr.RegisterLib(L, "FashionShopConfig", typeof(FashionShopConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFashionShopConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FashionShopConfig obj = new FashionShopConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionShopConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(FashionShopConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configsSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configsSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configsSort_m(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort_m");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort_m on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configsSort_m);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configsSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configsSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cHeadSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cHeadSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cHeadSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cHeadSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cClothesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cClothesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cClothesSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cClothesSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cTrouserseSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cTrouserseSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cTrouserseSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cTrouserseSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cShoesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cShoesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cShoesSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cShoesSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cBackSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cBackSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cBackSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cBackSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cSuiteSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cSuiteSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cSuiteSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cSuiteSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cHeadSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cHeadSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cHeadSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cHeadSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cClothesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cClothesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cClothesSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cClothesSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cTrouserseSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cTrouserseSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cTrouserseSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cTrouserseSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cShoesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cShoesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cShoesSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cShoesSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cBackSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cBackSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cBackSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cBackSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cSuiteSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cSuiteSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cSuiteSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cSuiteSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationConfigs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationConfigs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationConfigsSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationConfigsSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationConfigsSort_m(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort_m");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort_m on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationConfigsSort_m);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationConfigsSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationConfigsSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationHeadSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationHeadSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationHeadSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationHeadSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationClothesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationClothesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationClothesSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationClothesSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationTrouserseSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationTrouserseSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationTrouserseSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationTrouserseSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationShoesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationShoesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationShoesSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationShoesSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationBackSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationBackSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationBackSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationBackSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationSuiteSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationSuiteSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationSuiteSort on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationSuiteSort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationHeadSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationHeadSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationHeadSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationHeadSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationClothesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationClothesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationClothesSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationClothesSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationTrouserseSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationTrouserseSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationTrouserseSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationTrouserseSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationShoesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationShoesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationShoesSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationShoesSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationBackSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationBackSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationBackSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationBackSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reputationSuiteSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationSuiteSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationSuiteSort_w on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reputationSuiteSort_w);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		obj.configs = (Dictionary<uint,FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configsSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort on a nil value");
			}
		}

		obj.configsSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configsSort_m(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort_m");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort_m on a nil value");
			}
		}

		obj.configsSort_m = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configsSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configsSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configsSort_w on a nil value");
			}
		}

		obj.configsSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cHeadSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cHeadSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cHeadSort on a nil value");
			}
		}

		obj.cHeadSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cClothesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cClothesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cClothesSort on a nil value");
			}
		}

		obj.cClothesSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cTrouserseSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cTrouserseSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cTrouserseSort on a nil value");
			}
		}

		obj.cTrouserseSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cShoesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cShoesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cShoesSort on a nil value");
			}
		}

		obj.cShoesSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cBackSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cBackSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cBackSort on a nil value");
			}
		}

		obj.cBackSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cSuiteSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cSuiteSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cSuiteSort on a nil value");
			}
		}

		obj.cSuiteSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cHeadSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cHeadSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cHeadSort_w on a nil value");
			}
		}

		obj.cHeadSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cClothesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cClothesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cClothesSort_w on a nil value");
			}
		}

		obj.cClothesSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cTrouserseSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cTrouserseSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cTrouserseSort_w on a nil value");
			}
		}

		obj.cTrouserseSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cShoesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cShoesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cShoesSort_w on a nil value");
			}
		}

		obj.cShoesSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cBackSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cBackSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cBackSort_w on a nil value");
			}
		}

		obj.cBackSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cSuiteSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cSuiteSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cSuiteSort_w on a nil value");
			}
		}

		obj.cSuiteSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationConfigs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigs on a nil value");
			}
		}

		obj.reputationConfigs = (Dictionary<uint,FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationConfigsSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort on a nil value");
			}
		}

		obj.reputationConfigsSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationConfigsSort_m(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort_m");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort_m on a nil value");
			}
		}

		obj.reputationConfigsSort_m = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationConfigsSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationConfigsSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationConfigsSort_w on a nil value");
			}
		}

		obj.reputationConfigsSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationHeadSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationHeadSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationHeadSort on a nil value");
			}
		}

		obj.reputationHeadSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationClothesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationClothesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationClothesSort on a nil value");
			}
		}

		obj.reputationClothesSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationTrouserseSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationTrouserseSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationTrouserseSort on a nil value");
			}
		}

		obj.reputationTrouserseSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationShoesSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationShoesSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationShoesSort on a nil value");
			}
		}

		obj.reputationShoesSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationBackSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationBackSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationBackSort on a nil value");
			}
		}

		obj.reputationBackSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationSuiteSort(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationSuiteSort");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationSuiteSort on a nil value");
			}
		}

		obj.reputationSuiteSort = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationHeadSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationHeadSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationHeadSort_w on a nil value");
			}
		}

		obj.reputationHeadSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationClothesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationClothesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationClothesSort_w on a nil value");
			}
		}

		obj.reputationClothesSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationTrouserseSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationTrouserseSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationTrouserseSort_w on a nil value");
			}
		}

		obj.reputationTrouserseSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationShoesSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationShoesSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationShoesSort_w on a nil value");
			}
		}

		obj.reputationShoesSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationBackSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationBackSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationBackSort_w on a nil value");
			}
		}

		obj.reputationBackSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reputationSuiteSort_w(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfig obj = (FashionShopConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reputationSuiteSort_w");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reputationSuiteSort_w on a nil value");
			}
		}

		obj.reputationSuiteSort_w = (List<FashionShopConfigItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FashionShopConfigItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionShopConfig obj = (FashionShopConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionShopConfigItem o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetReputationConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionShopConfig obj = (FashionShopConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionShopConfigItem o = obj.GetReputationConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfig obj = (FashionShopConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfig");
		obj.ReadConfig();
		return 0;
	}
}

