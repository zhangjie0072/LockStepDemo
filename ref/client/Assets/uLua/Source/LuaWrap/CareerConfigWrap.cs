using System;
using System.Collections.Generic;
using LuaInterface;

public class CareerConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ParseChapterConfig", ParseChapterConfig),
			new LuaMethod("ParseSectionConfig", ParseSectionConfig),
			new LuaMethod("ParseCareerAwardLibConfig", ParseCareerAwardLibConfig),
			new LuaMethod("ParseAwardPackConfig", ParseAwardPackConfig),
			new LuaMethod("ParsePlotConfig", ParsePlotConfig),
			new LuaMethod("ParseStarConditionConfig", ParseStarConditionConfig),
			new LuaMethod("ParseSectionResetConfig", ParseSectionResetConfig),
			new LuaMethod("GetChapterData", GetChapterData),
			new LuaMethod("GetSectionData", GetSectionData),
			new LuaMethod("GetAwardPackConfig", GetAwardPackConfig),
			new LuaMethod("GetGoodsList", GetGoodsList),
			new LuaMethod("GetSectionAllGoodsList", GetSectionAllGoodsList),
			new LuaMethod("GetStarConditionString", GetStarConditionString),
			new LuaMethod("New", _CreateCareerConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("awardPackConfig", get_awardPackConfig, set_awardPackConfig),
			new LuaField("chapterConfig", get_chapterConfig, set_chapterConfig),
			new LuaField("sectionConfig", get_sectionConfig, set_sectionConfig),
			new LuaField("careerAwardLibConfig", get_careerAwardLibConfig, set_careerAwardLibConfig),
			new LuaField("plotConfig", get_plotConfig, set_plotConfig),
			new LuaField("dialogConfig", get_dialogConfig, set_dialogConfig),
			new LuaField("starConditionConfig", get_starConditionConfig, set_starConditionConfig),
			new LuaField("assistConfig", get_assistConfig, set_assistConfig),
			new LuaField("sectionResetConfig", get_sectionResetConfig, set_sectionResetConfig),
			new LuaField("sectionAwardsConfig", get_sectionAwardsConfig, set_sectionAwardsConfig),
		};

		LuaScriptMgr.RegisterLib(L, "CareerConfig", typeof(CareerConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCareerConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			CareerConfig obj = new CareerConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CareerConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(CareerConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardPackConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.awardPackConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_chapterConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.chapterConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sectionConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.sectionConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_careerAwardLibConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.careerAwardLibConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_plotConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.plotConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dialogConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.dialogConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_starConditionConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.starConditionConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assistConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.assistConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sectionResetConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.sectionResetConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sectionAwardsConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CareerConfig.sectionAwardsConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardPackConfig(IntPtr L)
	{
		CareerConfig.awardPackConfig = (Dictionary<uint,fogs.proto.config.AwardPackConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.AwardPackConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_chapterConfig(IntPtr L)
	{
		CareerConfig.chapterConfig = (Dictionary<uint,fogs.proto.config.ChapterConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.ChapterConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sectionConfig(IntPtr L)
	{
		CareerConfig.sectionConfig = (Dictionary<uint,fogs.proto.config.SectionConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.SectionConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_careerAwardLibConfig(IntPtr L)
	{
		CareerConfig.careerAwardLibConfig = (Dictionary<uint,fogs.proto.config.CareerAwardLibConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.CareerAwardLibConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_plotConfig(IntPtr L)
	{
		CareerConfig.plotConfig = (Dictionary<uint,List<fogs.proto.config.PlotConfig>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<fogs.proto.config.PlotConfig>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dialogConfig(IntPtr L)
	{
		CareerConfig.dialogConfig = (Dictionary<uint,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_starConditionConfig(IntPtr L)
	{
		CareerConfig.starConditionConfig = (Dictionary<uint,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assistConfig(IntPtr L)
	{
		CareerConfig.assistConfig = (Dictionary<uint,List<uint>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<uint>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sectionResetConfig(IntPtr L)
	{
		CareerConfig.sectionResetConfig = (Dictionary<uint,fogs.proto.config.BuyGameTimesConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.BuyGameTimesConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sectionAwardsConfig(IntPtr L)
	{
		CareerConfig.sectionAwardsConfig = (Dictionary<uint,List<uint>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<uint>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseChapterConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseChapterConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseSectionConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseSectionConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseCareerAwardLibConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseCareerAwardLibConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseAwardPackConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseAwardPackConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParsePlotConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParsePlotConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseStarConditionConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseStarConditionConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseSectionResetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		obj.ParseSectionResetConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetChapterData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.ChapterConfig o = obj.GetChapterData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSectionData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CareerConfig obj = (CareerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CareerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.SectionConfig o = obj.GetSectionData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAwardPackConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		fogs.proto.config.AwardPackConfig o = CareerConfig.GetAwardPackConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoodsList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		List<fogs.proto.config.AwardConfig> o = CareerConfig.GetGoodsList(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSectionAllGoodsList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		List<fogs.proto.config.AwardConfig> o = CareerConfig.GetSectionAllGoodsList(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStarConditionString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = CareerConfig.GetStarConditionString(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

