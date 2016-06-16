using System;
using System.Collections.Generic;
using LuaInterface;

public class NPCDataConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadNPCData", ReadNPCData),
			new LuaMethod("ReadTourNPC", ReadTourNPC),
			new LuaMethod("ReadNPCmodify", ReadNPCmodify),
			new LuaMethod("GetConfigData", GetConfigData),
			new LuaMethod("GetTourNPCAttrData", GetTourNPCAttrData),
			new LuaMethod("GetQualifyingNPCAttrData", GetQualifyingNPCAttrData),
			new LuaMethod("GetNPCAttrData", GetNPCAttrData),
			new LuaMethod("GetShapeID", GetShapeID),
			new LuaMethod("GetName", GetName),
			new LuaMethod("GetIcon", GetIcon),
			new LuaMethod("New", _CreateNPCDataConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("NPCDatas", get_NPCDatas, set_NPCDatas),
			new LuaField("attrs", get_attrs, set_attrs),
			new LuaField("TourNpcs", get_TourNpcs, set_TourNpcs),
			new LuaField("NPCmodify", get_NPCmodify, set_NPCmodify),
		};

		LuaScriptMgr.RegisterLib(L, "NPCDataConfig", typeof(NPCDataConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNPCDataConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NPCDataConfig obj = new NPCDataConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NPCDataConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(NPCDataConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NPCDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NPCDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TourNpcs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourNpcs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourNpcs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TourNpcs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NPCmodify(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCmodify");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCmodify on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NPCmodify);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NPCDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCDatas on a nil value");
			}
		}

		obj.NPCDatas = (Dictionary<uint,fogs.proto.config.NPCConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.NPCConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		obj.attrs = (Dictionary<uint,AttrData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,AttrData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TourNpcs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourNpcs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourNpcs on a nil value");
			}
		}

		obj.TourNpcs = (List<NPCproperty>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<NPCproperty>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NPCmodify(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NPCDataConfig obj = (NPCDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCmodify");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCmodify on a nil value");
			}
		}

		obj.NPCmodify = (Dictionary<uint,List<PropertyModify>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<PropertyModify>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadNPCData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		obj.ReadNPCData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTourNPC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		obj.ReadTourNPC();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadNPCmodify(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		obj.ReadNPCmodify();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.NPCConfig o = obj.GetConfigData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTourNPCAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		AttrData o = obj.GetTourNPCAttrData(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualifyingNPCAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		AttrData o = obj.GetQualifyingNPCAttrData(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNPCAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		AttrData o = obj.GetNPCAttrData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShapeID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetShapeID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetName(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIcon(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NPCDataConfig obj = (NPCDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NPCDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetIcon(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

