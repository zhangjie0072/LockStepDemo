using System;
using UnityEngine;
using LuaInterface;

public class CommonFunctionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConstString", GetConstString),
			new LuaMethod("GetLeftTimeStr", GetLeftTimeStr),
			new LuaMethod("GetLfetTime", GetLfetTime),
			new LuaMethod("IsEndTime", IsEndTime),
			new LuaMethod("GetFileMD5", GetFileMD5),
			new LuaMethod("InstantiateObject", InstantiateObject),
			new LuaMethod("ShowUpdateTip", ShowUpdateTip),
			new LuaMethod("ShowPopupMsg", ShowPopupMsg),
			new LuaMethod("ShowTip", ShowTip),
			new LuaMethod("ShowErrorMsg", ShowErrorMsg),
			new LuaMethod("ShowWaitMask", ShowWaitMask),
			new LuaMethod("HideWaitMask", HideWaitMask),
			new LuaMethod("GetObjectID", GetObjectID),
			new LuaMethod("DebugLog", DebugLog),
			new LuaMethod("PointToAngle", PointToAngle),
			new LuaMethod("PlayAnimation", PlayAnimation),
			new LuaMethod("ClearGridChild", ClearGridChild),
			new LuaMethod("ClearTableChild", ClearTableChild),
			new LuaMethod("ClearChild", ClearChild),
			new LuaMethod("LoadXmlConfig", LoadXmlConfig),
			new LuaMethod("IsCommented", IsCommented),
			new LuaMethod("SplitLines", SplitLines),
			new LuaMethod("GetDigits", GetDigits),
			new LuaMethod("GetQualityString", GetQualityString),
			new LuaMethod("GetQualitySymbolString", GetQualitySymbolString),
			new LuaMethod("Break", Break),
			new LuaMethod("SetAnimatorShade", SetAnimatorShade),
			new LuaMethod("TickTime", TickTime),
			new LuaMethod("ShowWait", ShowWait),
			new LuaMethod("StopWait", StopWait),
			new LuaMethod("SetPushInfo", SetPushInfo),
			new LuaMethod("GetListByType", GetListByType),
			new LuaMethod("New", _CreateCommonFunction),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaScriptMgr.RegisterLib(L, "CommonFunction", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCommonFunction(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CommonFunction class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CommonFunction);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConstString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = CommonFunction.GetConstString(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLeftTimeStr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		long arg0 = (long)LuaScriptMgr.GetNumber(L, 1);
		string o = CommonFunction.GetLeftTimeStr(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLfetTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		long arg0 = (long)LuaScriptMgr.GetNumber(L, 1);
		long o = CommonFunction.GetLfetTime(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsEndTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		long arg0 = (long)LuaScriptMgr.GetNumber(L, 1);
		bool o = CommonFunction.IsEndTime(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFileMD5(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = null;
		bool o = CommonFunction.GetFileMD5(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InstantiateObject(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(Transform)))
		{
			string arg0 = LuaScriptMgr.GetString(L, 1);
			Transform arg1 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			GameObject o = CommonFunction.InstantiateObject(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(Transform)))
		{
			GameObject arg0 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			Transform arg1 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			GameObject o = CommonFunction.InstantiateObject(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CommonFunction.InstantiateObject");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUpdateTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		UIEventListener.VoidDelegate arg2 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		UIEventListener.VoidDelegate arg3 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg3 = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 4, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg3 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		string arg4 = LuaScriptMgr.GetLuaString(L, 5);
		string arg5 = LuaScriptMgr.GetLuaString(L, 6);
		CommonFunction.ShowUpdateTip(arg0,arg1,arg2,arg3,arg4,arg5);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowPopupMsg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		UIEventListener.VoidDelegate arg2 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		UIEventListener.VoidDelegate arg3 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg3 = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 4, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg3 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		string arg4 = LuaScriptMgr.GetLuaString(L, 5);
		string arg5 = LuaScriptMgr.GetLuaString(L, 6);
		LuaComponent o = CommonFunction.ShowPopupMsg(arg0,arg1,arg2,arg3,arg4,arg5);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		LuaComponent o = CommonFunction.ShowTip(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowErrorMsg(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			fogs.proto.msg.ErrorID arg0 = (fogs.proto.msg.ErrorID)LuaScriptMgr.GetNetObject(L, 1, typeof(fogs.proto.msg.ErrorID));
			LuaComponent o = CommonFunction.ShowErrorMsg(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2)
		{
			fogs.proto.msg.ErrorID arg0 = (fogs.proto.msg.ErrorID)LuaScriptMgr.GetNetObject(L, 1, typeof(fogs.proto.msg.ErrorID));
			Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			LuaComponent o = CommonFunction.ShowErrorMsg(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			fogs.proto.msg.ErrorID arg0 = (fogs.proto.msg.ErrorID)LuaScriptMgr.GetNetObject(L, 1, typeof(fogs.proto.msg.ErrorID));
			Transform arg1 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			UIEventListener.VoidDelegate arg2 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UIEventListener.VoidDelegate));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg2 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					func.PCall(top, 1);
					func.EndPCall(top);
				};
			}

			LuaComponent o = CommonFunction.ShowErrorMsg(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CommonFunction.ShowErrorMsg");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowWaitMask(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		GameObject o = CommonFunction.ShowWaitMask();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideWaitMask(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CommonFunction.HideWaitMask();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjectID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		uint o = CommonFunction.GetObjectID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DebugLog(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		object arg0 = LuaScriptMgr.GetVarObject(L, 1);
		LogType arg1 = (LogType)LuaScriptMgr.GetNetObject(L, 2, typeof(LogType));
		CommonFunction.DebugLog(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PointToAngle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
		double arg2 = (double)LuaScriptMgr.GetNumber(L, 3);
		double o = CommonFunction.PointToAngle(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayAnimation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 3);
		CommonFunction.PlayAnimation(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearGridChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		CommonFunction.ClearGridChild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearTableChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		CommonFunction.ClearTableChild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		CommonFunction.ClearChild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadXmlConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			System.Xml.XmlDocument o = CommonFunction.LoadXmlConfig(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
		{
			string arg0 = LuaScriptMgr.GetString(L, 1);
			string arg1 = LuaScriptMgr.GetString(L, 2);
			System.Xml.XmlDocument o = CommonFunction.LoadXmlConfig(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(object)))
		{
			string arg0 = LuaScriptMgr.GetString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			System.Xml.XmlDocument o = CommonFunction.LoadXmlConfig(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CommonFunction.LoadXmlConfig");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommented(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		System.Xml.XmlNode arg0 = (System.Xml.XmlNode)LuaScriptMgr.GetNetObject(L, 1, typeof(System.Xml.XmlNode));
		bool o = CommonFunction.IsCommented(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SplitLines(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string[] o = CommonFunction.SplitLines(arg0);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDigits(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint[] o = CommonFunction.GetDigits(arg0,arg1);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualityString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = CommonFunction.GetQualityString(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualitySymbolString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = CommonFunction.GetQualitySymbolString(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Break(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CommonFunction.Break();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAnimatorShade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		bool arg0 = LuaScriptMgr.GetBoolean(L, 1);
		CommonFunction.SetAnimatorShade(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TickTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		double o = CommonFunction.TickTime();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowWait(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CommonFunction.ShowWait();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopWait(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CommonFunction.StopWait();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPushInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		string arg5 = LuaScriptMgr.GetLuaString(L, 6);
		CommonFunction.SetPushInfo(arg0,arg1,arg2,arg3,arg4,arg5);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetListByType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		object arg1 = LuaScriptMgr.GetVarObject(L, 2);
		LuaInterface.LuaTable o = CommonFunction.GetListByType(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

