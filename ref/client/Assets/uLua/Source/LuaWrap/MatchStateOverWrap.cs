using System;
using System.Collections.Generic;
using LuaInterface;

public class MatchStateOverWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("SendPractise1vs1Result", SendPractise1vs1Result),
			new LuaMethod("SendCareerResult", SendCareerResult),
			new LuaMethod("SendPractiseResult", SendPractiseResult),
			new LuaMethod("SendEnterGamePractise1vs1", SendEnterGamePractise1vs1),
			new LuaMethod("SendEnterGame", SendEnterGame),
			new LuaMethod("TourCompleteHandler", TourCompleteHandler),
			new LuaMethod("QualifyingCompleteHandler", QualifyingCompleteHandler),
			new LuaMethod("BullFightCompleteHandler", BullFightCompleteHandler),
			new LuaMethod("ShootCompleteHandler", ShootCompleteHandler),
			new LuaMethod("ChallengeCompleteHandler", ChallengeCompleteHandler),
			new LuaMethod("ChallengeExCompleteHandler", ChallengeExCompleteHandler),
			new LuaMethod("QualifyingNewerCompleteHandler", QualifyingNewerCompleteHandler),
			new LuaMethod("SectionCompleteHandler", SectionCompleteHandler),
			new LuaMethod("PracticePveCompleteHandler", PracticePveCompleteHandler),
			new LuaMethod("HandlePracticePveComplete", HandlePracticePveComplete),
			new LuaMethod("HandleQualifyingNewerAIComplete", HandleQualifyingNewerAIComplete),
			new LuaMethod("HandleSectionComplete", HandleSectionComplete),
			new LuaMethod("HandleQualifyingComplete", HandleQualifyingComplete),
			new LuaMethod("HandleBullFightComplete", HandleBullFightComplete),
			new LuaMethod("HandleShootComplete", HandleShootComplete),
			new LuaMethod("HandlePractiseComplete", HandlePractiseComplete),
			new LuaMethod("HandlePracticeLocalComplete", HandlePracticeLocalComplete),
			new LuaMethod("HandleChallengeComplete", HandleChallengeComplete),
			new LuaMethod("HandleQualifyingNewerComplete", HandleQualifyingNewerComplete),
			new LuaMethod("HandleChallengeExComplete", HandleChallengeExComplete),
			new LuaMethod("RegularCompleteHandler", RegularCompleteHandler),
			new LuaMethod("QualifyingNewCompleteHandler", QualifyingNewCompleteHandler),
			new LuaMethod("New", _CreateMatchStateOver),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("matchResultSent", get_matchResultSent, null),
		};

		LuaScriptMgr.RegisterLib(L, "MatchStateOver", typeof(MatchStateOver), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStateOver(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStateOver obj = new MatchStateOver(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateOver.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStateOver);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matchResultSent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateOver obj = (MatchStateOver)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchResultSent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchResultSent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.matchResultSent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendPractise1vs1Result(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		GameMatch arg0 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
		obj.SendPractise1vs1Result(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendCareerResult(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		GameMatch arg0 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
		obj.SendCareerResult(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendPractiseResult(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.SendPractiseResult();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendEnterGamePractise1vs1(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.EnterGameReq arg0 = (fogs.proto.msg.EnterGameReq)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EnterGameReq));
		obj.SendEnterGamePractise1vs1(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendEnterGame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.MatchType arg0 = (fogs.proto.msg.MatchType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.MatchType));
		obj.SendEnterGame(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TourCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.TourEndResp arg0 = (fogs.proto.msg.TourEndResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.TourEndResp));
		obj.TourCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int QualifyingCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.QualifyingEndResp arg0 = (fogs.proto.msg.QualifyingEndResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.QualifyingEndResp));
		obj.QualifyingCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BullFightCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.EndBullFightResp arg0 = (fogs.proto.msg.EndBullFightResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EndBullFightResp));
		obj.BullFightCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShootCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.EndShootResp arg0 = (fogs.proto.msg.EndShootResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EndShootResp));
		obj.ShootCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChallengeCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.PVPEndChallengePlusResp arg0 = (fogs.proto.msg.PVPEndChallengePlusResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PVPEndChallengePlusResp));
		obj.ChallengeCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChallengeExCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.PVPEndChallengeExResp arg0 = (fogs.proto.msg.PVPEndChallengeExResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PVPEndChallengeExResp));
		List<fogs.proto.msg.KeyValueData> arg1 = (List<fogs.proto.msg.KeyValueData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.KeyValueData>));
		obj.ChallengeExCompleteHandler(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int QualifyingNewerCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.PVPEndQualifyingNewerResp arg0 = (fogs.proto.msg.PVPEndQualifyingNewerResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PVPEndQualifyingNewerResp));
		List<fogs.proto.msg.KeyValueData> arg1 = (List<fogs.proto.msg.KeyValueData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.KeyValueData>));
		obj.QualifyingNewerCompleteHandler(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SectionCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.EndSectionMatchResp arg0 = (fogs.proto.msg.EndSectionMatchResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EndSectionMatchResp));
		obj.SectionCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PracticePveCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.EndPracticePveResp arg0 = (fogs.proto.msg.EndPracticePveResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EndPracticePveResp));
		obj.PracticePveCompleteHandler(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandlePracticePveComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandlePracticePveComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleQualifyingNewerAIComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleQualifyingNewerAIComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleSectionComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleQualifyingComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleQualifyingComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleBullFightComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleBullFightComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleShootComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleShootComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandlePractiseComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandlePractiseComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandlePracticeLocalComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandlePracticeLocalComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleChallengeComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleChallengeComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleQualifyingNewerComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleQualifyingNewerComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleChallengeExComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		obj.HandleChallengeExComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegularCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.PVPEndRegularResp arg0 = (fogs.proto.msg.PVPEndRegularResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PVPEndRegularResp));
		List<fogs.proto.msg.KeyValueData> arg1 = (List<fogs.proto.msg.KeyValueData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.KeyValueData>));
		obj.RegularCompleteHandler(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int QualifyingNewCompleteHandler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MatchStateOver obj = (MatchStateOver)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOver");
		fogs.proto.msg.PVPEndQualifyingResp arg0 = (fogs.proto.msg.PVPEndQualifyingResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PVPEndQualifyingResp));
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		List<fogs.proto.msg.KeyValueData> arg2 = (List<fogs.proto.msg.KeyValueData>)LuaScriptMgr.GetNetObject(L, 4, typeof(List<fogs.proto.msg.KeyValueData>));
		obj.QualifyingNewCompleteHandler(arg0,arg1,arg2);
		return 0;
	}
}

