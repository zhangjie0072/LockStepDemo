using System;
using System.Collections.Generic;
using LuaInterface;

public class ConditionValidatorWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Validate", Validate),
			new LuaMethod("ValidateNone", ValidateNone),
			new LuaMethod("ValidateCaptainLevel", ValidateCaptainLevel),
			new LuaMethod("ValidateCareerSectionComplete", ValidateCareerSectionComplete),
			new LuaMethod("ValidateCareerSectionUI", ValidateCareerSectionUI),
			new LuaMethod("ValidateCareerChapterUI", ValidateCareerChapterUI),
			new LuaMethod("ValidateGuideModuleComplete", ValidateGuideModuleComplete),
			new LuaMethod("ValidateTaskComplete", ValidateTaskComplete),
			new LuaMethod("ValidateStarAwardReceive", ValidateStarAwardReceive),
			new LuaMethod("ValidateSkillEquip", ValidateSkillEquip),
			new LuaMethod("ValidateTattooEquip", ValidateTattooEquip),
			new LuaMethod("ValidateTrainingComplete", ValidateTrainingComplete),
			new LuaMethod("ValidateCreateStep", ValidateCreateStep),
			new LuaMethod("ValidateRoleAllTrainingQuality", ValidateRoleAllTrainingQuality),
			new LuaMethod("ValidateCurCareerStar", ValidateCurCareerStar),
			new LuaMethod("ValidateSquadRoleHeadEquiped", ValidateSquadRoleHeadEquiped),
			new LuaMethod("ValidateAnyEquipmentUpgraded", ValidateAnyEquipmentUpgraded),
			new LuaMethod("ValidateTaskAwardReceived", ValidateTaskAwardReceived),
			new LuaMethod("ValidateAnyRoleAnyExercised", ValidateAnyRoleAnyExercised),
			new LuaMethod("ValidateRoleCannotReset", ValidateRoleCannotReset),
			new LuaMethod("ValidateNoExpGoods", ValidateNoExpGoods),
			new LuaMethod("ValidateRoleNotInSquad", ValidateRoleNotInSquad),
			new LuaMethod("ValidateAnyRoleImproved", ValidateAnyRoleImproved),
			new LuaMethod("ValidateAcquiringGoods", ValidateAcquiringGoods),
			new LuaMethod("ValidateFailedAtSection", ValidateFailedAtSection),
			new LuaMethod("ValidateAnyRoleUpgraded", ValidateAnyRoleUpgraded),
			new LuaMethod("ValidateSkillOwned", ValidateSkillOwned),
			new LuaMethod("ValidateSkillUpgraded", ValidateSkillUpgraded),
			new LuaMethod("ValidateBadgeEquiped", ValidateBadgeEquiped),
			new LuaMethod("ValidateLotteryFree", ValidateLotteryFree),
			new LuaMethod("ValidateGuideRunning", ValidateGuideRunning),
			new LuaMethod("ValidateBadgeSlotEquiped", ValidateBadgeSlotEquiped),
			new LuaMethod("New", _CreateConditionValidator),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "ConditionValidator", typeof(ConditionValidator), regs, fields, typeof(Singleton<ConditionValidator>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateConditionValidator(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ConditionValidator obj = new ConditionValidator();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ConditionValidator.New");
		}

		return 0;
	}

	static Type classType = typeof(ConditionValidator);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Validate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
			ConditionType arg0 = (ConditionType)LuaScriptMgr.GetNetObject(L, 2, typeof(ConditionType));
			string arg1 = LuaScriptMgr.GetLuaString(L, 3);
			bool o = obj.Validate(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
			List<ConditionType> arg0 = (List<ConditionType>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<ConditionType>));
			List<string> arg1 = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
			bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
			bool o = obj.Validate(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ConditionValidator.Validate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateNone(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateNone(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCaptainLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCaptainLevel(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCareerSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCareerSectionComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCareerSectionUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCareerSectionUI(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCareerChapterUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCareerChapterUI(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateGuideModuleComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateGuideModuleComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateTaskComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateTaskComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateStarAwardReceive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateStarAwardReceive(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateSkillEquip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateSkillEquip(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateTattooEquip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateTattooEquip(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateTrainingComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateTrainingComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCreateStep(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCreateStep(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateRoleAllTrainingQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateRoleAllTrainingQuality(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateCurCareerStar(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateCurCareerStar(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateSquadRoleHeadEquiped(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateSquadRoleHeadEquiped(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateAnyEquipmentUpgraded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateAnyEquipmentUpgraded(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateTaskAwardReceived(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateTaskAwardReceived(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateAnyRoleAnyExercised(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateAnyRoleAnyExercised(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateRoleCannotReset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateRoleCannotReset(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateNoExpGoods(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateNoExpGoods(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateRoleNotInSquad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateRoleNotInSquad(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateAnyRoleImproved(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateAnyRoleImproved(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateAcquiringGoods(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateAcquiringGoods(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateFailedAtSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateFailedAtSection(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateAnyRoleUpgraded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateAnyRoleUpgraded(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateSkillOwned(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateSkillOwned(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateSkillUpgraded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateSkillUpgraded(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateBadgeEquiped(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateBadgeEquiped(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateLotteryFree(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateLotteryFree(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateGuideRunning(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateGuideRunning(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateBadgeSlotEquiped(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ConditionValidator obj = (ConditionValidator)LuaScriptMgr.GetNetObjectSelf(L, 1, "ConditionValidator");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ValidateBadgeSlotEquiped(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

