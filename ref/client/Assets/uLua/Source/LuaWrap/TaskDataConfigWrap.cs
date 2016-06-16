using System;
using System.Collections.Generic;
using LuaInterface;

public class TaskDataConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetTypeById", GetTypeById),
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadTaskMainData", ReadTaskMainData),
			new LuaMethod("ReadTaskDailyData", ReadTaskDailyData),
			new LuaMethod("ReadTaskLevelData", ReadTaskLevelData),
			new LuaMethod("ReadTaskLinkData", ReadTaskLinkData),
			new LuaMethod("GetTaskMainInfoByID", GetTaskMainInfoByID),
			new LuaMethod("GetTaskDailyInfoByID", GetTaskDailyInfoByID),
			new LuaMethod("GetTaskLevelInfoByID", GetTaskLevelInfoByID),
			new LuaMethod("GetTaskMainAwardID", GetTaskMainAwardID),
			new LuaMethod("GetTaskDailyAwardID", GetTaskDailyAwardID),
			new LuaMethod("GetTaskLevelAwardID", GetTaskLevelAwardID),
			new LuaMethod("GetTaskPreConditionValueById", GetTaskPreConditionValueById),
			new LuaMethod("GetLinkUIName", GetLinkUIName),
			new LuaMethod("GetTaskLinkUIName", GetTaskLinkUIName),
			new LuaMethod("GetTaskLinkSubID", GetTaskLinkSubID),
			new LuaMethod("New", _CreateTaskDataConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("taskMainConfig", get_taskMainConfig, set_taskMainConfig),
			new LuaField("taskLevelConfig", get_taskLevelConfig, set_taskLevelConfig),
			new LuaField("taskDailyConfig", get_taskDailyConfig, set_taskDailyConfig),
			new LuaField("taskLinkConfig", get_taskLinkConfig, set_taskLinkConfig),
		};

		LuaScriptMgr.RegisterLib(L, "TaskDataConfig", typeof(TaskDataConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTaskDataConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TaskDataConfig obj = new TaskDataConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TaskDataConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(TaskDataConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_taskMainConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, TaskDataConfig.taskMainConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_taskLevelConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, TaskDataConfig.taskLevelConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_taskDailyConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, TaskDataConfig.taskDailyConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_taskLinkConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, TaskDataConfig.taskLinkConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_taskMainConfig(IntPtr L)
	{
		TaskDataConfig.taskMainConfig = (Dictionary<uint,fogs.proto.config.TaskConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.TaskConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_taskLevelConfig(IntPtr L)
	{
		TaskDataConfig.taskLevelConfig = (Dictionary<uint,fogs.proto.config.TaskConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.TaskConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_taskDailyConfig(IntPtr L)
	{
		TaskDataConfig.taskDailyConfig = (Dictionary<uint,fogs.proto.config.TaskConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.TaskConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_taskLinkConfig(IntPtr L)
	{
		TaskDataConfig.taskLinkConfig = (Dictionary<uint,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTypeById(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTypeById(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTaskMainData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.ReadTaskMainData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTaskDailyData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.ReadTaskDailyData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTaskLevelData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.ReadTaskLevelData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTaskLinkData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		obj.ReadTaskLinkData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskMainInfoByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.TaskConfig o = obj.GetTaskMainInfoByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskDailyInfoByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.TaskConfig o = obj.GetTaskDailyInfoByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskLevelInfoByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.TaskConfig o = obj.GetTaskLevelInfoByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskMainAwardID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTaskMainAwardID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskDailyAwardID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTaskDailyAwardID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskLevelAwardID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTaskLevelAwardID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskPreConditionValueById(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTaskPreConditionValueById(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLinkUIName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetLinkUIName(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskLinkUIName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetTaskLinkUIName(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTaskLinkSubID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TaskDataConfig obj = (TaskDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TaskDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetTaskLinkSubID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

