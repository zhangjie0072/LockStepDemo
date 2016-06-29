using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;

public class GameUtilsWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetDirection", GetDirection),
			new LuaMethod("HorizonalDistance", HorizonalDistance),
			new LuaMethod("Convert", Convert),
			new LuaMethod("String2Float", String2Float),
			new LuaMethod("String2FloatList", String2FloatList),
			new LuaMethod("String2IntList", String2IntList),
			new LuaMethod("String2Vector2", String2Vector2),
			new LuaMethod("String2Vector3", String2Vector3),
			new LuaMethod("Vector22String", Vector22String),
			new LuaMethod("Vector32String", Vector32String),
			new LuaMethod("Vector2Cross", Vector2Cross),
			new LuaMethod("StripV3Y", StripV3Y),
			new LuaMethod("DummyV2Y", DummyV2Y),
			new LuaMethod("FindChildRecursive", FindChildRecursive),
			new LuaMethod("SetRenderQueue", SetRenderQueue),
			new LuaMethod("SetLayerRecursive", SetLayerRecursive),
			new LuaMethod("SetWidgetColorRecursive", SetWidgetColorRecursive),
			new LuaMethod("CreateBounds", CreateBounds),
			new LuaMethod("HorizonalNormalized", HorizonalNormalized),
			new LuaMethod("RotateTowards", RotateTowards),
			new LuaMethod("DrawSectors", DrawSectors),
			new LuaMethod("DrawWireArc", DrawWireArc),
			new LuaMethod("ReSkinning", ReSkinning),
			new LuaMethod("CalcSolutionQuadraticFunc", CalcSolutionQuadraticFunc),
			new LuaMethod("CombineSkin", CombineSkin),
			new LuaMethod("AngleToDir", AngleToDir),
			new LuaMethod("DirToAngle", DirToAngle),
			new LuaMethod("GetStringLength", GetStringLength),
			new LuaMethod("New", _CreateGameUtils),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("red", get_red, set_red),
			new LuaField("yellow", get_yellow, set_yellow),
			new LuaField("green", get_green, set_green),
		};

		LuaScriptMgr.RegisterLib(L, "GameUtils", typeof(GameUtils), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameUtils(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameUtils obj = new GameUtils();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameUtils.New");
		}

		return 0;
	}

	static Type classType = typeof(GameUtils);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_red(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameUtils.red);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_yellow(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameUtils.yellow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_green(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameUtils.green);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_red(IntPtr L)
	{
		GameUtils.red = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_yellow(IntPtr L)
	{
		GameUtils.yellow = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_green(IntPtr L)
	{
		GameUtils.green = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDirection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
		EDirection o = GameUtils.GetDirection(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HorizonalDistance(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Vector3)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = GameUtils.HorizonalDistance(arg0,arg1);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float o = GameUtils.HorizonalDistance(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameUtils.HorizonalDistance");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Convert(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			fogs.proto.msg.SVector3 o = GameUtils.Convert(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			fogs.proto.msg.SVector3 o = GameUtils.Convert(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameUtils.Convert");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int String2Float(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		float o = GameUtils.String2Float(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int String2FloatList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		float[] o = GameUtils.String2FloatList(arg0,arg1);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int String2IntList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		int[] o = GameUtils.String2IntList(arg0,arg1);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int String2Vector2(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		Vector2 o = GameUtils.String2Vector2(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int String2Vector3(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		Vector3 o = GameUtils.String2Vector3(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Vector22String(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Vector2 arg0 = LuaScriptMgr.GetVector2(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		string o = GameUtils.Vector22String(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Vector32String(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		char arg1 = (char)LuaScriptMgr.GetNumber(L, 2);
		string o = GameUtils.Vector32String(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Vector2Cross(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Vector2 arg0 = LuaScriptMgr.GetVector2(L, 1);
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 2);
		float o = GameUtils.Vector2Cross(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StripV3Y(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		Vector2 o = GameUtils.StripV3Y(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DummyV2Y(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Vector2 arg0 = LuaScriptMgr.GetVector2(L, 1);
		Vector3 o = GameUtils.DummyV2Y(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindChildRecursive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = GameUtils.FindChildRecursive(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRenderQueue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		GameUtils.SetRenderQueue(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayerRecursive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		GameUtils.SetLayerRecursive(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetWidgetColorRecursive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		Color arg1 = LuaScriptMgr.GetColor(L, 2);
		GameUtils.SetWidgetColorRecursive(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateBounds(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
		Bounds o = GameUtils.CreateBounds(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HorizonalNormalized(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Vector3)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = GameUtils.HorizonalNormalized(arg0,arg1);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = GameUtils.HorizonalNormalized(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameUtils.HorizonalNormalized");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RotateTowards(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		bool arg3 = LuaScriptMgr.GetBoolean(L, 4);
		IM.Vector3 arg4 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 5, typeof(IM.Vector3));
		IM.Vector3 o = GameUtils.RotateTowards(arg0,arg1,arg2,arg3,arg4);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawSectors(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		List<float> arg1 = (List<float>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<float>));
		List<float> arg2 = (List<float>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<float>));
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		Color arg4 = LuaScriptMgr.GetColor(L, 5);
		GameUtils.DrawSectors(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawWireArc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
		Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 4);
		float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
		Color arg5 = LuaScriptMgr.GetColor(L, 6);
		GameUtils.DrawWireArc(arg0,arg1,arg2,arg3,arg4,arg5);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReSkinning(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		GameObject arg1 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		GameUtils.ReSkinning(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalcSolutionQuadraticFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		IM.Number arg0;
		IM.Number arg1;
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		IM.Number arg3 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Number arg4 = (IM.Number)LuaScriptMgr.GetNetObject(L, 5, typeof(IM.Number));
		bool o = GameUtils.CalcSolutionQuadraticFunc(out arg0,out arg1,arg2,arg3,arg4);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.PushValue(L, arg0);
		LuaScriptMgr.PushValue(L, arg1);
		return 3;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CombineSkin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		Transform o = GameUtils.CombineSkin(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AngleToDir(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(float), null, null))
		{
			float arg0 = (float)LuaDLL.lua_tonumber(L, 1);
			EDirection arg1;
			Vector3 arg2;
			GameUtils.AngleToDir(arg0,out arg1,out arg2);
			LuaScriptMgr.Push(L, arg1);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), null, null))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			int arg1;
			IM.Vector3 arg2;
			GameUtils.AngleToDir(arg0,out arg1,out arg2);
			LuaScriptMgr.Push(L, arg1);
			LuaScriptMgr.PushValue(L, arg2);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameUtils.AngleToDir");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DirToAngle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		EDirection arg0 = (EDirection)LuaScriptMgr.GetNetObject(L, 1, typeof(EDirection));
		Vector3 arg1;
		GameUtils.DirToAngle(arg0,out arg1);
		LuaScriptMgr.Push(L, arg1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStringLength(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int o = GameUtils.GetStringLength(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

