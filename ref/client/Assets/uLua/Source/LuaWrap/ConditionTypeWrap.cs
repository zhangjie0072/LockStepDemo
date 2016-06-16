using System;
using LuaInterface;

public class ConditionTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("None", GetNone),
		new LuaMethod("CaptainLevel", GetCaptainLevel),
		new LuaMethod("CareerSectionComplete", GetCareerSectionComplete),
		new LuaMethod("CareerSectionUI", GetCareerSectionUI),
		new LuaMethod("GuideModuleComplete", GetGuideModuleComplete),
		new LuaMethod("TaskComplete", GetTaskComplete),
		new LuaMethod("CareerChapterUI", GetCareerChapterUI),
		new LuaMethod("StarAwardReceive", GetStarAwardReceive),
		new LuaMethod("SkillEquip", GetSkillEquip),
		new LuaMethod("TattooEquip", GetTattooEquip),
		new LuaMethod("TattooUpgrade", GetTattooUpgrade),
		new LuaMethod("TrainingComplete", GetTrainingComplete),
		new LuaMethod("CreateStep", GetCreateStep),
		new LuaMethod("RoleAllTrainingQuality", GetRoleAllTrainingQuality),
		new LuaMethod("CurCareerStar", GetCurCareerStar),
		new LuaMethod("SquadRoleHeadEquiped", GetSquadRoleHeadEquiped),
		new LuaMethod("AnyEquipmentUpgraded", GetAnyEquipmentUpgraded),
		new LuaMethod("TaskAwardReceived", GetTaskAwardReceived),
		new LuaMethod("AnyRoleAnyExercised", GetAnyRoleAnyExercised),
		new LuaMethod("RoleCannotReset", GetRoleCannotReset),
		new LuaMethod("NoExpGoods", GetNoExpGoods),
		new LuaMethod("RoleNotInSquad", GetRoleNotInSquad),
		new LuaMethod("AnyRoleImproved", GetAnyRoleImproved),
		new LuaMethod("AcquiringGoods", GetAcquiringGoods),
		new LuaMethod("FailedAtSection", GetFailedAtSection),
		new LuaMethod("AnyRoleUpgraded", GetAnyRoleUpgraded),
		new LuaMethod("SkillOwned", GetSkillOwned),
		new LuaMethod("SkillUpgraded", GetSkillUpgraded),
		new LuaMethod("BadgeEquiped", GetBadgeEquiped),
		new LuaMethod("LotteryFree", GetLotteryFree),
		new LuaMethod("GuideRunning", GetGuideRunning),
		new LuaMethod("BadgeSlotEquiped", GetBadgeSlotEquiped),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "ConditionType", typeof(ConditionType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCaptainLevel(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CaptainLevel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCareerSectionComplete(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CareerSectionComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCareerSectionUI(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CareerSectionUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGuideModuleComplete(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.GuideModuleComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskComplete(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.TaskComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCareerChapterUI(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CareerChapterUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStarAwardReceive(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.StarAwardReceive);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillEquip(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.SkillEquip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTattooEquip(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.TattooEquip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTattooUpgrade(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.TattooUpgrade);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTrainingComplete(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.TrainingComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCreateStep(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CreateStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleAllTrainingQuality(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.RoleAllTrainingQuality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCurCareerStar(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.CurCareerStar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSquadRoleHeadEquiped(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.SquadRoleHeadEquiped);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnyEquipmentUpgraded(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.AnyEquipmentUpgraded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskAwardReceived(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.TaskAwardReceived);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnyRoleAnyExercised(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.AnyRoleAnyExercised);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleCannotReset(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.RoleCannotReset);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNoExpGoods(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.NoExpGoods);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleNotInSquad(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.RoleNotInSquad);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnyRoleImproved(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.AnyRoleImproved);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAcquiringGoods(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.AcquiringGoods);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFailedAtSection(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.FailedAtSection);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnyRoleUpgraded(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.AnyRoleUpgraded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillOwned(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.SkillOwned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillUpgraded(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.SkillUpgraded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeEquiped(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.BadgeEquiped);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLotteryFree(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.LotteryFree);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGuideRunning(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.GuideRunning);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeSlotEquiped(IntPtr L)
	{
		LuaScriptMgr.Push(L, ConditionType.BadgeSlotEquiped);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		ConditionType o = (ConditionType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

