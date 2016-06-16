using System;
using LuaInterface;

public class fogs_proto_msg_GoodsCategoryWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("GC_NONE", GetGC_NONE),
		new LuaMethod("GC_RESOURCE", GetGC_RESOURCE),
		new LuaMethod("GC_SKILL", GetGC_SKILL),
		new LuaMethod("GC_FAVORITE", GetGC_FAVORITE),
		new LuaMethod("GC_CONSUME", GetGC_CONSUME),
		new LuaMethod("GC_EQUIPMENT", GetGC_EQUIPMENT),
		new LuaMethod("GC_FASHION", GetGC_FASHION),
		new LuaMethod("GC_EXERCISE", GetGC_EXERCISE),
		new LuaMethod("GC_MATERIAL", GetGC_MATERIAL),
		new LuaMethod("GC_ROLE", GetGC_ROLE),
		new LuaMethod("GC_BADGE", GetGC_BADGE),
		new LuaMethod("GC_TOTAL", GetGC_TOTAL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GoodsCategory", typeof(fogs.proto.msg.GoodsCategory), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_NONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_RESOURCE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_RESOURCE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_SKILL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_SKILL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_FAVORITE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_FAVORITE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_CONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_CONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_EQUIPMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_EQUIPMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_FASHION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_FASHION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_EXERCISE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_EXERCISE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_MATERIAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_MATERIAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_ROLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_ROLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_BADGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_BADGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGC_TOTAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsCategory.GC_TOTAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.GoodsCategory o = (fogs.proto.msg.GoodsCategory)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

