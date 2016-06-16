using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using fogs.proto.msg;

public enum ConditionType
{
	None,
	CaptainLevel,
	CareerSectionComplete,
	CareerSectionUI,
	GuideModuleComplete,
	TaskComplete,
	CareerChapterUI,
	StarAwardReceive,
	SkillEquip,
	TattooEquip,
	TattooUpgrade,
	TrainingComplete,
	CreateStep,
	RoleAllTrainingQuality,
	CurCareerStar,			//当前显示的生涯章节星级数
	SquadRoleHeadEquiped,	//阵容中的第N个球员头部装备了装备
	AnyEquipmentUpgraded,	//任意装备已经进行过强化
	TaskAwardReceived,		//某个任务奖励已经领取
	AnyRoleAnyExercised,	//任意球员完成过任意训练
	RoleCannotReset,		//指定球员不能重置
	NoExpGoods,				//没有经验道具
	RoleNotInSquad,			//指定ID球员不在当前阵容中
	AnyRoleImproved,		//任意球员进阶过
	AcquiringGoods,			//获得指定物品界面
	FailedAtSection,		//某关卡失败界面
	AnyRoleUpgraded,		//任意球员升级过
	SkillOwned = 26,		//已拥有技能
	SkillUpgraded = 27,		//技能等级不为1
	BadgeEquiped  =28,		//已装备涂鸦
	LotteryFree = 29,		//有免费抽奖
	GuideRunning = 30,		//正在运行指引
	BadgeSlotEquiped = 31,	//涂鸦格已装备
}

public class ConditionValidator : Singleton<ConditionValidator>
{
	Transform basePanel
	{
		get { return GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform; }
	}
	
	public bool Validate(List<ConditionType> conditions, List<string> args, bool matchAnyOne = false)
	{
		for (int i = 0; i < conditions.Count; ++i)
		{
			if (i >= args.Count)
			{
				Logger.Log("ConditionValidator, wrong arg count.");
				foreach (ConditionType cond in conditions)
					Logger.Log("ConditionValidator, Condition: " + cond);
				foreach (string arg in args)
					Logger.Log("ConditionValidator, Argument: " + arg);
			}
            bool result = Validate(conditions[i], args[i]);
            if (!matchAnyOne && !result)
                return false;
            else if (matchAnyOne && result)
                return true;
		}
		return !matchAnyOne;
	}

	public bool Validate(ConditionType condition, string arg)
	{
		MethodInfo method = GetType().GetMethod("Validate" + condition.ToString());
		if (method == null)
			Logger.LogError("Error condition type: " + condition);
		return (bool)method.Invoke(this, new System.Object[] { arg });
	}

	public bool ValidateNone(string arg)
	{
		return true;
	}

	public bool ValidateCaptainLevel(string arg)
	{
		uint level = uint.Parse(arg);
		bool enabled = MainPlayer.Instance.Level >= level;
		Logger.Log("Validate captain level, " + enabled + " , Captain level: " + MainPlayer.Instance.Level + " Arg: " + level);
		return enabled;
	}

	public bool ValidateCareerSectionComplete(string arg)
	{
		string[] tokens = arg.Split(',');
		uint chapterID = uint.Parse(tokens[0]);
		uint sectionID = uint.Parse(tokens[1]);
		bool enabled = MainPlayer.Instance.CheckSectionComplete(chapterID, sectionID);
		Logger.Log("Validate career section complete, " + enabled + " , Chapter: " + chapterID + " Section: " + sectionID);
		return enabled;
	}

	public bool ValidateCareerSectionUI(string arg)
	{
		Transform tm = basePanel.FindChild("UICareer(Clone)/UICareerSection(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			string[] tokens = arg.Split(',');
			uint chapterID = uint.Parse(tokens[0]);
			uint sectionID = uint.Parse(tokens[1]);
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			uint curChapterID = (uint)(double)luaCom.table["chapterID"];
			uint curSectionID = (uint)(double)luaCom.table["sectionID"];
			if (curChapterID == chapterID && curSectionID == sectionID)
			{
				Logger.Log("Validate career section UI, true, Chapter: " + chapterID + " Section: " + sectionID);
				return true;
			}
			Logger.Log("Validate career section UI, false, Chapter: " + chapterID + " Section: " + sectionID);
			return false;
		}
		Logger.Log("Validate career section UI, false, UI not visible.");
		return false;
	}

	public bool ValidateCareerChapterUI(string arg)
	{
		Transform tm = basePanel.FindChild("UICareer(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			uint chapterID = uint.Parse(arg);
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			uint curChapterID = (uint)(double)luaCom.table["chapterID"];
			if (curChapterID == chapterID || chapterID == 0)
			{
				Logger.Log("Validate career chapter UI, true, Chapter: " + chapterID);
				return true;
			}
			Logger.Log("Validate career chapter UI, false, Chapter: " + chapterID);
			return false;
		}
		Logger.Log("Validate career chapter UI, false, UI not visible");
		return false;
	}

	public bool ValidateGuideModuleComplete(string arg)
	{
		uint guideModuleID = uint.Parse(arg);
		bool enabled =  MainPlayer.Instance.IsGuideCompleted(guideModuleID);
		Logger.Log("Validate guide module complete, " + enabled + ", ID: " + guideModuleID);
		return enabled;
	}

	public bool ValidateTaskComplete(string arg)
	{
		uint taskID = uint.Parse(arg);
		bool enabled = MainPlayer.Instance.IsTaskCompleted(taskID);
		Logger.Log("Validate task complete, " + enabled + ", ID: " + taskID);
		return enabled;
	}

	public bool ValidateStarAwardReceive(string arg)
	{
		string[] tokens = arg.Split(',');
		uint chapterID = uint.Parse(tokens[0]);
		uint awardType = uint.Parse(tokens[1]);
		Chapter chapter = MainPlayer.Instance.GetChapter(chapterID);
		if (chapter != null)
		{
			if (awardType == 1)
				return chapter.is_bronze_awarded;
			else if (awardType == 2)
				return chapter.is_silver_awarded;
			else if (awardType == 3)
				return chapter.is_gold_awarded;
		}
		return false;
	}

	public bool ValidateSkillEquip(string arg)
	{
		uint skillID = uint.Parse(arg);
		foreach (SkillSlotProto skillProto in MainPlayer.Instance.Captain.m_roleInfo.skill_slot_info)
		{
			if (skillProto.skill_id == skillID)
			{
				Logger.Log("ConditionValidator, SkillEquip succeed. " + skillID);
				return true;
			}
		}
		Logger.Log("ConditionValidator, SkillEquip failed. " + skillID);
		return false;
	}

	public bool ValidateTattooEquip(string arg)
	{
		//uint tattooID = uint.Parse(arg);
		//foreach (TattooSlotProto tattoo in MainPlayer.Instance.Captain.m_roleInfo.tattoo_slot_info)
		//{
		//	if (tattoo.tattoo_uuid != 0)
		//	{
		//		Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TATTOO, tattoo.tattoo_uuid);
		//		if (goods.GetID() == tattooID)
		//			return true;
		//	}
		//}
		return false;
	}

    /*
	public bool ValidateTattooUpgrade(string arg)
	{
		uint tattooID = uint.Parse(arg);
		List<Goods> goodsList = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TATTOO, tattooID);
		foreach (Goods goods in goodsList)
		{
			if (goods.GetLevel() > 1)
				return true;
		}
		return false;
	}
     * */

    public bool ValidateTrainingComplete(string arg)
    {
        uint traningID = uint.Parse(arg);
        foreach (RoleInfo captainInfo in MainPlayer.Instance.CaptainInfos)
        {
            if (captainInfo.id == MainPlayer.Instance.CaptainID)
            {
                foreach (ExerciseInfo info in captainInfo.exercise)
                {
                    // for pass compile.
                    if (info.id == traningID /*&& info.level > 0*/)
                        return true;
                }
            }
        }
        return false;
    }

	public bool ValidateCreateStep(string arg)
	{
		uint step = uint.Parse(arg);
		uint curStep = MainPlayer.Instance.CreateStep;
		if (curStep != step)
		{
			Logger.Log("Validate create step failed, Cur step: " + curStep + " Expected step: " + step);
			return false;
		}
		return true;
	}

	public bool ValidateRoleAllTrainingQuality(string arg)
	{
		Transform tm = basePanel.FindChild("RoleOpen(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			uint roleID = (uint)(double)luaCom.table["id"];
			RoleInfo roleInfo = MainPlayer.Instance.GetRole2(roleID);
			uint quality = uint.Parse(arg);
			foreach (ExerciseInfo info in roleInfo.exercise)
			{
				if (info.quality < quality)
				{
					Logger.Log("Validate RoleAllTrainingQuality failed, required quality: " + quality + " cur quality: " + info.quality + " exercise id: " + info.id + " roleID: " + roleID);
					return false;
				}
			}
			return true;
		}
		Logger.Log("Validate RoleAllTrainingQuality failed, UI not opened");
		return false;
	}

	public bool ValidateCurCareerStar(string arg)
	{
		Transform tm = basePanel.FindChild("UICareer(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			uint starNum = uint.Parse(arg);
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			uint curChapterID = (uint)(double)luaCom.table["chapterID"];
			Chapter chapter = MainPlayer.Instance.GetChapter(curChapterID);
			if (chapter.star_num >= starNum)
			{
				Logger.Log("Validate cur career star num, true, Chapter: " + curChapterID + " Star num:" + starNum);
				return true;
			}
			Logger.Log("Validate cur career star num, false, Chapter: " + curChapterID + " Star num:" + starNum);
			return false;
		}
		Logger.Log("Validate cur career star num, false, UI not visible");
		return false;
	}

	public bool ValidateSquadRoleHeadEquiped(string arg)
	{
		int roleIdx = int.Parse(arg);
		FightRole fightRole = MainPlayer.Instance.SquadInfo[roleIdx];
		foreach (EquipInfo info in MainPlayer.Instance.EquipInfo)
		{
			if (info.pos == fightRole.status)
			{
				foreach (EquipmentSlot slot in info.slot_info)
				{
					if (slot.id == EquipmentSlotID.ESID_HEAD)
						return slot.equipment_uuid != 0;
				}
			}
		}
		return false;
	}

	public bool ValidateAnyEquipmentUpgraded(string arg)
	{
		foreach (KeyValuePair<ulong, Goods> pair in MainPlayer.Instance.EquipmentGoodsList)
		{
			if (pair.Value.GetLevel() > 1)
				return true;
		}
		Logger.Log("ConditionValidator, AnyEquipmentUpgraded failed.");
		return false;
	}

	public bool ValidateTaskAwardReceived(string arg)
	{
		uint taskID = uint.Parse(arg);
		TaskData task = MainPlayer.Instance.taskInfo.task_list.Find(t => t.id == taskID);
		return task == null || task.state == 3;
	}

	public bool ValidateAnyRoleAnyExercised(string arg)
	{
		foreach (Player role in MainPlayer.Instance.PlayerList)
		{
			foreach (ExerciseInfo info in role.m_roleInfo.exercise)
			{
				if (info.quality > 1 || info.star > 0)
					return true;
			}
		}
		Logger.Log("ConditionValidator, AnyRoleAnyExercise failed.");
		return false;
	}

	public bool ValidateRoleCannotReset(string arg)
	{
		uint roleID = uint.Parse(arg);
		RoleInfo role = MainPlayer.Instance.GetRole2(roleID);
		if (role != null)
		{
			if (role.exp > 0)
				return false;
			if (role.level > 1)
				return false;
			if (role.quality > 1)
				return false;
			if (role.star > 0)
				return false;
			foreach (ExerciseInfo info in role.exercise)
			{
				if (info.quality > 1 || info.star > 0)
					return false;
			}
		}
		return true;
	}

	public bool ValidateNoExpGoods(string arg)
	{
		foreach (KeyValuePair<ulong, Goods> pair in MainPlayer.Instance.ConsumeGoodsList)
		{
			if ((int)(pair.Value.GetSubCategory()) == 1)
				return false;
		}
		return true;
	}

	public bool ValidateRoleNotInSquad(string arg)
	{
		uint roleID = uint.Parse(arg);
		return MainPlayer.Instance.SquadInfo.Find(role => role.role_id == roleID) == null;
	}

	public bool ValidateAnyRoleImproved(string arg)
	{
		foreach (Player role in MainPlayer.Instance.PlayerList)
		{
			if (role.m_roleInfo.star > 0)
				return true;
		}
		Logger.Log("ConditionValidator, AnyRoleImproved failed.");
		return false;
	}

	public bool ValidateAcquiringGoods(string arg)
	{
		Transform tm = basePanel.FindChild("GoodsAcquirePopup(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			uint goodsID = uint.Parse(arg);
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			LuaInterface.LuaFunction func = (LuaInterface.LuaFunction)luaCom.table["HasGoods"];
			bool hasGoods = (bool)(func.Call((double)goodsID)[0]);
			if (hasGoods)
			{
				Logger.Log("Validate goods acquire, true, GoodsID: " + goodsID);
				return true;
			}
			Logger.Log("Validate goods acquire, false, GoodsID: " + goodsID);
			return false;
		}
		Logger.Log("Validate goods acquire, false, UI not visible");
		return false;
	}

	public bool ValidateFailedAtSection(string arg)
	{
		Transform tm = basePanel.FindChild("UIMatchResult(Clone)");
		if (tm != null && NGUITools.GetActive(tm.gameObject))
		{
			LuaComponent luaCom = tm.GetComponent<LuaComponent>();
			if (!(bool)luaCom.table["isWin"])
			{
				GameMatch.LeagueType leagueType = (GameMatch.LeagueType)luaCom.table["leagueType"];
				if (leagueType == GameMatch.LeagueType.eCareer)
				{
					uint sectionID = (uint)(double)(LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionID"]);
					if (sectionID == uint.Parse(arg))
					{
						return true;
						Logger.Log("Validate failed at section, true, section ID:" + sectionID);
					}
					else
						Logger.Log("Validate failed at section, false, cur section ID:" + sectionID);
				}
				else
					Logger.Log("Validate failed at section, false, match is not career.");
			}
			else
				Logger.Log("Validate failed at section, false, match win.");
		}
		else
			Logger.Log("Validate failed at section, false, UI not visible");
		return false;
	}

	public bool ValidateAnyRoleUpgraded(string arg)
	{
		foreach (Player role in MainPlayer.Instance.PlayerList)
		{
			if (role.m_roleInfo.level > 1)
				return true;
		}
		Logger.Log("ConditionValidator, AnyRoleUpgraded failed.");
		return false;
	}

	public bool ValidateSkillOwned(string arg)
	{
		uint skillID = uint.Parse(arg);
		uint count = MainPlayer.Instance.GetGoodsCount(skillID);
		Logger.Log("ConditionValidator, SkillOwned " + (count > 0 ? "succeed" : "failed"));
		return count > 0; 
	}

	public bool ValidateSkillUpgraded(string arg)
	{
		uint skillID = uint.Parse(arg);
		List<Goods> goodsList = MainPlayer.Instance.GetGoodsList(GoodsCategory.GC_SKILL, skillID);
		foreach (Goods goods in goodsList)
		{
			if (goods.GetLevel() > 1)
			{
				Logger.Log("ConditionValidator, SkillUpgraded succeed. " + skillID);
				return true;
			}
		}
		Logger.Log("ConditionValidator, SkillUpgraded failed. " + skillID);
		return false;
	}

	public bool ValidateBadgeEquiped(string arg)
	{
		uint badgeID = uint.Parse(arg);
		List<BadgeBook> books = MainPlayer.Instance.badgeSystemInfo.GetAllBadgeBooks();
		foreach (BadgeBook book in books)
		{
			foreach (BadgeSlot slot in book.slot_list)
			{
				if (slot.status == BadgeSlotStatus.UNLOCK && slot.badge_id == badgeID)
				{
					Logger.Log("ConditionValidator, BadgeEquiped succeed. " + badgeID);
					return true;
				}
			}
		}
		Logger.Log("ConditionValidator, BadgeEquiped failed. " + badgeID);
		return false;
	}

	public bool ValidateLotteryFree(string arg)
	{
		uint ID = uint.Parse(arg);
		LotteryInfo lotteryInfo = MainPlayer.Instance.LotteryInfo;
		if (ID == GlobalConst.DIAMOND_ID)
		{
			uint totalFreeTimes = GameSystem.Instance.CommonConfig.GetUInt("gSpecialLotteryFreeTimes");
			if (lotteryInfo.free_times2 > 0 || totalFreeTimes == 0)
			{
				Logger.Log("ConditionValidator, LotteryFree failed. " + ID);
				return false;
			}
		}
		else if (ID == GlobalConst.GOLD_ID)
		{
			uint totalFreeTimes = GameSystem.Instance.CommonConfig.GetUInt("gNormalLotteryFreeTimes");
			if (lotteryInfo.free_times1 > 0 || totalFreeTimes == 0)
			{
				Logger.Log("ConditionValidator, LotteryFree failed. " + ID);
				return false;
			}
		}
		Logger.Log("ConditionValidator, LotteryFree succeed. " + ID);
		return true;
	}

	public bool ValidateGuideRunning(string arg)
	{
		GuideSystem sys = GuideSystem.Instance;
		return sys.curModule != null || sys.curStep != null || sys.requestingBeingGuide;
	}

	public bool ValidateBadgeSlotEquiped(string arg)
	{
		string[] tokens = arg.Split(',');
		uint bookID = uint.Parse(tokens[0]);
		uint slotID = uint.Parse(tokens[1]);
		BadgeBook books = MainPlayer.Instance.badgeSystemInfo.GetBadgeBookByBookId(bookID);
		if (books == null)
			Logger.LogError("ValidateBadgeSlotEquiped, error book ID:" + bookID);
		BadgeSlot slot = books.slot_list.Find((s) => s.id == slotID);
		if (slot == null)
			Logger.LogError("ValidateBadgeSlotEquiped, error slot ID:" + bookID + " " + slotID);
		return slot.status == BadgeSlotStatus.UNLOCK && slot.badge_id != 0;
	}
}
