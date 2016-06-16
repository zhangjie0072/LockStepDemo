using System;
using System.Collections.Generic;
using LuaInterface;

public class FriendDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("FriendListChangedDelegate", FriendListChangedDelegate),
			new LuaMethod("Init", Init),
			new LuaMethod("SendUpdateFriendList", SendUpdateFriendList),
			new LuaMethod("OnFriendOperationResp", OnFriendOperationResp),
			new LuaMethod("RegisterOnListChanged", RegisterOnListChanged),
			new LuaMethod("UnRegisterOnListChanged", UnRegisterOnListChanged),
			new LuaMethod("ListChanged", ListChanged),
			new LuaMethod("GetList", GetList),
			new LuaMethod("GetListCount", GetListCount),
			new LuaMethod("IsFriend", IsFriend),
			new LuaMethod("New", _CreateFriendData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onListChanged", get_onListChanged, set_onListChanged),
			new LuaField("_changedCallBackList", get__changedCallBackList, set__changedCallBackList),
			new LuaField("friend_list", get_friend_list, set_friend_list),
			new LuaField("apply_list", get_apply_list, set_apply_list),
			new LuaField("black_list", get_black_list, set_black_list),
			new LuaField("gift_list", get_gift_list, set_gift_list),
			new LuaField("present_times", get_present_times, set_present_times),
			new LuaField("get_gift_times", get_get_gift_times, set_get_gift_times),
			new LuaField("maxFriendsApply", get_maxFriendsApply, set_maxFriendsApply),
			new LuaField("Instance", get_Instance, null),
		};

		LuaScriptMgr.RegisterLib(L, "FriendData", typeof(FriendData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFriendData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FriendData obj = new FriendData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FriendData.New");
		}

		return 0;
	}

	static Type classType = typeof(FriendData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onListChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onListChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onListChanged on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onListChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__changedCallBackList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _changedCallBackList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _changedCallBackList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._changedCallBackList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_friend_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name friend_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index friend_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.friend_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_apply_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.apply_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_black_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name black_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index black_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.black_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gift_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.gift_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_present_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name present_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index present_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.present_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_get_gift_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_gift_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_gift_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.get_gift_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxFriendsApply(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxFriendsApply");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxFriendsApply on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxFriendsApply);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, FriendData.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onListChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onListChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onListChanged on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onListChanged = (FriendData.OnListChanged)LuaScriptMgr.GetNetObject(L, 3, typeof(FriendData.OnListChanged));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onListChanged = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__changedCallBackList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _changedCallBackList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _changedCallBackList on a nil value");
			}
		}

		obj._changedCallBackList = (List<FriendData.OnListChanged>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<FriendData.OnListChanged>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_friend_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name friend_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index friend_list on a nil value");
			}
		}

		obj.friend_list = (List<fogs.proto.msg.FriendInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FriendInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_apply_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_list on a nil value");
			}
		}

		obj.apply_list = (List<fogs.proto.msg.FriendInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FriendInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_black_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name black_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index black_list on a nil value");
			}
		}

		obj.black_list = (List<fogs.proto.msg.FriendInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FriendInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gift_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift_list on a nil value");
			}
		}

		obj.gift_list = (List<fogs.proto.msg.FriendInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FriendInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_present_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name present_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index present_times on a nil value");
			}
		}

		obj.present_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_get_gift_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_gift_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_gift_times on a nil value");
			}
		}

		obj.get_gift_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxFriendsApply(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FriendData obj = (FriendData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxFriendsApply");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxFriendsApply on a nil value");
			}
		}

		obj.maxFriendsApply = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FriendListChangedDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		FriendData.OnListChanged o = FriendData.FriendListChangedDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		obj.Init();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendUpdateFriendList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		obj.SendUpdateFriendList();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFriendOperationResp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		fogs.proto.msg.FriendOperationResp arg0 = (fogs.proto.msg.FriendOperationResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FriendOperationResp));
		obj.OnFriendOperationResp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterOnListChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		FriendData.OnListChanged arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (FriendData.OnListChanged)LuaScriptMgr.GetNetObject(L, 2, typeof(FriendData.OnListChanged));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.RegisterOnListChanged(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnRegisterOnListChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		FriendData.OnListChanged arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (FriendData.OnListChanged)LuaScriptMgr.GetNetObject(L, 2, typeof(FriendData.OnListChanged));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.UnRegisterOnListChanged(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ListChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		fogs.proto.msg.FriendOperationType arg0 = (fogs.proto.msg.FriendOperationType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FriendOperationType));
		obj.ListChanged(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		fogs.proto.msg.FriendOperationType arg0 = (fogs.proto.msg.FriendOperationType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FriendOperationType));
		LuaInterface.LuaTable o = obj.GetList(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetListCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		fogs.proto.msg.FriendOperationType arg0 = (fogs.proto.msg.FriendOperationType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FriendOperationType));
		int o = obj.GetListCount(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsFriend(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FriendData obj = (FriendData)LuaScriptMgr.GetNetObjectSelf(L, 1, "FriendData");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsFriend(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

