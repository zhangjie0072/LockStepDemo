using System;
using LuaInterface;

public class fogs_proto_msg_FriendOperationTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("FOT_PRESEND", GetFOT_PRESEND),
		new LuaMethod("FOT_ADD", GetFOT_ADD),
		new LuaMethod("FOT_BLACK", GetFOT_BLACK),
		new LuaMethod("FOT_GETAWARDS", GetFOT_GETAWARDS),
		new LuaMethod("FOT_QUERY", GetFOT_QUERY),
		new LuaMethod("FOT_AGREE_APPLY", GetFOT_AGREE_APPLY),
		new LuaMethod("FOT_APPLY", GetFOT_APPLY),
		new LuaMethod("FOT_DEL_FRIEND", GetFOT_DEL_FRIEND),
		new LuaMethod("FOT_DEL_BLACK", GetFOT_DEL_BLACK),
		new LuaMethod("FOT_QUERY_BLACK", GetFOT_QUERY_BLACK),
		new LuaMethod("FOT_QUERY_APPLY", GetFOT_QUERY_APPLY),
		new LuaMethod("FOT_QUERY_GIFT", GetFOT_QUERY_GIFT),
		new LuaMethod("FOT_IGNORE_APPLY", GetFOT_IGNORE_APPLY),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FriendOperationType", typeof(fogs.proto.msg.FriendOperationType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_PRESEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_PRESEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_ADD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_ADD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_BLACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_BLACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_GETAWARDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_GETAWARDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_QUERY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_QUERY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_AGREE_APPLY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_AGREE_APPLY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_APPLY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_APPLY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_DEL_FRIEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_DEL_FRIEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_DEL_BLACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_DEL_BLACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_QUERY_BLACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_QUERY_BLACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_QUERY_APPLY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_QUERY_APPLY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_QUERY_GIFT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_QUERY_GIFT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_IGNORE_APPLY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FriendOperationType.FOT_IGNORE_APPLY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.FriendOperationType o = (fogs.proto.msg.FriendOperationType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

