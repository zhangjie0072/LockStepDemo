using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ResourceLoadManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("UnloadDependAB", UnloadDependAB),
			new LuaMethod("LoadSound", LoadSound),
			new LuaMethod("LoadCharacter", LoadCharacter),
			new LuaMethod("LoadAloneImage", LoadAloneImage),
			new LuaMethod("LoadPrefab", LoadPrefab),
			new LuaMethod("GetAtlas", GetAtlas),
			new LuaMethod("GetResources", GetResources),
			new LuaMethod("GetConfigResource", GetConfigResource),
			new LuaMethod("GetConfigText", GetConfigText),
			new LuaMethod("GetConfigByte", GetConfigByte),
			new LuaMethod("GetLuaResource", GetLuaResource),
			new LuaMethod("New", _CreateResourceLoadManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "ResourceLoadManager", typeof(ResourceLoadManager), regs, fields, typeof(Singleton<ResourceLoadManager>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateResourceLoadManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ResourceLoadManager obj = new ResourceLoadManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ResourceLoadManager.New");
		}

		return 0;
	}

	static Type classType = typeof(ResourceLoadManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadDependAB(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		obj.UnloadDependAB();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Action<Object> arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (Action<Object>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<Object>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		Action<string> arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action<string>)LuaScriptMgr.GetNetObject(L, 4, typeof(Action<string>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.LoadSound(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadCharacter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Action<GameObject> arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (Action<GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		Action<string> arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action<string>)LuaScriptMgr.GetNetObject(L, 4, typeof(Action<string>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.LoadCharacter(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAloneImage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Action<Texture> arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (Action<Texture>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<Texture>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		Action<string> arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action<string>)LuaScriptMgr.GetNetObject(L, 4, typeof(Action<string>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.LoadAloneImage(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		GameObject o = obj.LoadPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAtlas(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		UIAtlas o = obj.GetAtlas(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResources(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(ResourceLoadManager), typeof(string), typeof(Type)))
		{
			ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
			string arg0 = LuaScriptMgr.GetString(L, 2);
			Type arg1 = LuaScriptMgr.GetTypeObject(L, 3);
			Object o = obj.GetResources(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(ResourceLoadManager), typeof(string), typeof(bool)))
		{
			ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
			string arg0 = LuaScriptMgr.GetString(L, 2);
			bool arg1 = LuaDLL.lua_toboolean(L, 3);
			Object o = obj.GetResources(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ResourceLoadManager.GetResources");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigResource(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		DelegateLoadComplete arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (DelegateLoadComplete)LuaScriptMgr.GetNetObject(L, 3, typeof(DelegateLoadComplete));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.PushVarObject(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}

		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		Object o = obj.GetConfigResource(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigText(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string o = obj.GetConfigText(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigByte(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		byte[] o = obj.GetConfigByte(arg0);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLuaResource(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceLoadManager obj = (ResourceLoadManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "ResourceLoadManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		DelegateLoadComplete arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (DelegateLoadComplete)LuaScriptMgr.GetNetObject(L, 3, typeof(DelegateLoadComplete));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.PushVarObject(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}

		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		Object o = obj.GetLuaResource(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

