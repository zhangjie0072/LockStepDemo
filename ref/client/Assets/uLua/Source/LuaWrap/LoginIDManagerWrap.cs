using System;
using LuaInterface;

public class LoginIDManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetFirstLoginState", SetFirstLoginState),
			new LuaMethod("GetFirstLoginState", GetFirstLoginState),
			new LuaMethod("SetAccount", SetAccount),
			new LuaMethod("GetAccount", GetAccount),
			new LuaMethod("SetUserName", SetUserName),
			new LuaMethod("GetUserName", GetUserName),
			new LuaMethod("SetPassword", SetPassword),
			new LuaMethod("GetPassword", GetPassword),
			new LuaMethod("SetFristGuid", SetFristGuid),
			new LuaMethod("GetFristGuid", GetFristGuid),
			new LuaMethod("SetServerIP", SetServerIP),
			new LuaMethod("GetServerIP", GetServerIP),
			new LuaMethod("SetPlatDisplayServerID", SetPlatDisplayServerID),
			new LuaMethod("GetPlatDisplayServerID", GetPlatDisplayServerID),
			new LuaMethod("SetPlatServerID", SetPlatServerID),
			new LuaMethod("GetPlatServerID", GetPlatServerID),
			new LuaMethod("SetPlatServerName", SetPlatServerName),
			new LuaMethod("GetPlatServerName", GetPlatServerName),
			new LuaMethod("SetLastLevel", SetLastLevel),
			new LuaMethod("GetLastLevel", GetLastLevel),
			new LuaMethod("SetAnnouceVersion", SetAnnouceVersion),
			new LuaMethod("GetAnnounceVersion", GetAnnounceVersion),
			new LuaMethod("SetFirstPractice", SetFirstPractice),
			new LuaMethod("GetFirstPractice", GetFirstPractice),
			new LuaMethod("New", _CreateLoginIDManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "LoginIDManager", typeof(LoginIDManager), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLoginIDManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LoginIDManager obj = new LoginIDManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LoginIDManager.New");
		}

		return 0;
	}

	static Type classType = typeof(LoginIDManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFirstLoginState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetFirstLoginState(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFirstLoginState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		bool o = LoginIDManager.GetFirstLoginState();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAccount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LoginIDManager.SetAccount(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAccount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LoginIDManager.GetAccount();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetUserName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LoginIDManager.SetUserName(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUserName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LoginIDManager.GetUserName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPassword(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LoginIDManager.SetPassword(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPassword(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LoginIDManager.GetPassword();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFristGuid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetFristGuid(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFristGuid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		int o = LoginIDManager.GetFristGuid();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetServerIP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LoginIDManager.SetServerIP(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetServerIP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LoginIDManager.GetServerIP();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPlatDisplayServerID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetPlatDisplayServerID(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlatDisplayServerID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		uint o = LoginIDManager.GetPlatDisplayServerID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPlatServerID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetPlatServerID(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlatServerID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		uint o = LoginIDManager.GetPlatServerID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPlatServerName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LoginIDManager.SetPlatServerName(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlatServerName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = LoginIDManager.GetPlatServerName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLastLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetLastLevel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLastLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		uint o = LoginIDManager.GetLastLevel();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAnnouceVersion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetAnnouceVersion(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnnounceVersion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		float o = LoginIDManager.GetAnnounceVersion();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFirstPractice(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		LoginIDManager.SetFirstPractice(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFirstPractice(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		uint o = LoginIDManager.GetFirstPractice(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

