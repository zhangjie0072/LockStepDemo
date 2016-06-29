using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using Object = UnityEngine.Object;

public static class DelegateFactory
{
	delegate Delegate DelegateValue(LuaFunction func);
	static Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();

	[NoToLuaAttribute]
	public static void Register(IntPtr L)
	{
		dict.Add(typeof(Action<GameObject>), new DelegateValue(Action_GameObject));
		dict.Add(typeof(Action), new DelegateValue(Action));
		dict.Add(typeof(UnityEngine.Events.UnityAction), new DelegateValue(UnityEngine_Events_UnityAction));
		dict.Add(typeof(MainPlayer.DataChangedDelegate), new DelegateValue(MainPlayer_DataChangedDelegate));
		dict.Add(typeof(System.Reflection.MemberFilter), new DelegateValue(System_Reflection_MemberFilter));
		dict.Add(typeof(System.Reflection.TypeFilter), new DelegateValue(System_Reflection_TypeFilter));
		dict.Add(typeof(Application.LogCallback), new DelegateValue(Application_LogCallback));
		dict.Add(typeof(Application.AdvertisingIdentifierCallback), new DelegateValue(Application_AdvertisingIdentifierCallback));
		dict.Add(typeof(AudioClip.PCMReaderCallback), new DelegateValue(AudioClip_PCMReaderCallback));
		dict.Add(typeof(AudioClip.PCMSetPositionCallback), new DelegateValue(AudioClip_PCMSetPositionCallback));
		dict.Add(typeof(Camera.CameraCallback), new DelegateValue(Camera_CameraCallback));
		dict.Add(typeof(EventDelegate.Callback), new DelegateValue(EventDelegate_Callback));
		dict.Add(typeof(UIWidget.OnDimensionsChanged), new DelegateValue(UIWidget_OnDimensionsChanged));
		dict.Add(typeof(UIWidget.HitCheck), new DelegateValue(UIWidget_HitCheck));
		dict.Add(typeof(UICamera.OnScreenResize), new DelegateValue(UICamera_OnScreenResize));
		dict.Add(typeof(UICamera.OnCustomInput), new DelegateValue(UICamera_OnCustomInput));
		dict.Add(typeof(SpringPanel.OnFinished), new DelegateValue(SpringPanel_OnFinished));
		dict.Add(typeof(UIEventListener.VoidDelegate), new DelegateValue(UIEventListener_VoidDelegate));
		dict.Add(typeof(UIEventListener.BoolDelegate), new DelegateValue(UIEventListener_BoolDelegate));
		dict.Add(typeof(UIEventListener.FloatDelegate), new DelegateValue(UIEventListener_FloatDelegate));
		dict.Add(typeof(UIEventListener.VectorDelegate), new DelegateValue(UIEventListener_VectorDelegate));
		dict.Add(typeof(UIEventListener.ObjectDelegate), new DelegateValue(UIEventListener_ObjectDelegate));
		dict.Add(typeof(UIEventListener.KeyCodeDelegate), new DelegateValue(UIEventListener_KeyCodeDelegate));
		dict.Add(typeof(UIGrid.OnReposition), new DelegateValue(UIGrid_OnReposition));
		dict.Add(typeof(BetterList<Transform>.CompareFunc), new DelegateValue(CompareFunc_Transform));
		dict.Add(typeof(UIInput.OnValidate), new DelegateValue(UIInput_OnValidate));
		dict.Add(typeof(UIPanel.OnGeometryUpdated), new DelegateValue(UIPanel_OnGeometryUpdated));
		dict.Add(typeof(UIPanel.OnClippingMoved), new DelegateValue(UIPanel_OnClippingMoved));
		dict.Add(typeof(UIProgressBar.OnDragFinished), new DelegateValue(UIProgressBar_OnDragFinished));
		dict.Add(typeof(UIScrollView.OnDragFinished), new DelegateValue(UIScrollView_OnDragFinished));
		dict.Add(typeof(UITable.OnReposition), new DelegateValue(UITable_OnReposition));
		dict.Add(typeof(UIWrapContent.OnInitializeItem), new DelegateValue(UIWrapContent_OnInitializeItem));
		dict.Add(typeof(AnimationResp.RespDel), new DelegateValue(AnimationResp_RespDel));
		dict.Add(typeof(Func<int,Transform,GameObject>), new DelegateValue(Func_int_Transform_GameObject));
		dict.Add(typeof(FriendData.OnListChanged), new DelegateValue(FriendData_OnListChanged));
		dict.Add(typeof(GameScene.DEBUG_DRAW_DELEGATE), new DelegateValue(GameScene_DEBUG_DRAW_DELEGATE));
		dict.Add(typeof(NetworkManager.OnServerConnected), new DelegateValue(NetworkManager_OnServerConnected));
		dict.Add(typeof(Action<Object>), new DelegateValue(Action_Object));
		dict.Add(typeof(Action<string>), new DelegateValue(Action_string));
		dict.Add(typeof(Action<Texture>), new DelegateValue(Action_Texture));
		dict.Add(typeof(DelegateLoadComplete), new DelegateValue(DelegateLoadComplete));
		dict.Add(typeof(Action<int,GameObject>), new DelegateValue(Action_int_GameObject));
		dict.Add(typeof(UBasket.BasketEventDelegate), new DelegateValue(UBasket_BasketEventDelegate));
		dict.Add(typeof(UBasket.BasketEventDunkDelegate), new DelegateValue(UBasket_BasketEventDunkDelegate));
		dict.Add(typeof(UBasketball.BallDelegate), new DelegateValue(UBasketball_BallDelegate));
		dict.Add(typeof(UBasketball.OnDunkDelegate), new DelegateValue(UBasketball_OnDunkDelegate));
		dict.Add(typeof(PlayerState.OnActionDone), new DelegateValue(PlayerState_OnActionDone));
		dict.Add(typeof(Action<PlayerState,PlayerState>), new DelegateValue(Action_PlayerState_PlayerState));
		dict.Add(typeof(Predicate<int>), new DelegateValue(Predicate_int));
		dict.Add(typeof(Action<int>), new DelegateValue(Action_int));
		dict.Add(typeof(Comparison<int>), new DelegateValue(Comparison_int));
		dict.Add(typeof(Predicate<uint>), new DelegateValue(Predicate_uint));
		dict.Add(typeof(Action<uint>), new DelegateValue(Action_uint));
		dict.Add(typeof(Comparison<uint>), new DelegateValue(Comparison_uint));
		dict.Add(typeof(Predicate<string>), new DelegateValue(Predicate_string));
		dict.Add(typeof(Comparison<string>), new DelegateValue(Comparison_string));
		dict.Add(typeof(Predicate<fogs.proto.msg.BadgeSlot>), new DelegateValue(Predicate_fogs_proto_msg_BadgeSlot));
		dict.Add(typeof(Action<fogs.proto.msg.BadgeSlot>), new DelegateValue(Action_fogs_proto_msg_BadgeSlot));
		dict.Add(typeof(Comparison<fogs.proto.msg.BadgeSlot>), new DelegateValue(Comparison_fogs_proto_msg_BadgeSlot));
		dict.Add(typeof(Predicate<fogs.proto.msg.BadgeBook>), new DelegateValue(Predicate_fogs_proto_msg_BadgeBook));
		dict.Add(typeof(Action<fogs.proto.msg.BadgeBook>), new DelegateValue(Action_fogs_proto_msg_BadgeBook));
		dict.Add(typeof(Comparison<fogs.proto.msg.BadgeBook>), new DelegateValue(Comparison_fogs_proto_msg_BadgeBook));
		dict.Add(typeof(Predicate<fogs.proto.msg.ChatBroadcast>), new DelegateValue(Predicate_fogs_proto_msg_ChatBroadcast));
		dict.Add(typeof(Action<fogs.proto.msg.ChatBroadcast>), new DelegateValue(Action_fogs_proto_msg_ChatBroadcast));
		dict.Add(typeof(Comparison<fogs.proto.msg.ChatBroadcast>), new DelegateValue(Comparison_fogs_proto_msg_ChatBroadcast));
		dict.Add(typeof(Predicate<fogs.proto.msg.EquipInfo>), new DelegateValue(Predicate_fogs_proto_msg_EquipInfo));
		dict.Add(typeof(Action<fogs.proto.msg.EquipInfo>), new DelegateValue(Action_fogs_proto_msg_EquipInfo));
		dict.Add(typeof(Comparison<fogs.proto.msg.EquipInfo>), new DelegateValue(Comparison_fogs_proto_msg_EquipInfo));
		dict.Add(typeof(Predicate<fogs.proto.msg.EquipmentSlot>), new DelegateValue(Predicate_fogs_proto_msg_EquipmentSlot));
		dict.Add(typeof(Action<fogs.proto.msg.EquipmentSlot>), new DelegateValue(Action_fogs_proto_msg_EquipmentSlot));
		dict.Add(typeof(Comparison<fogs.proto.msg.EquipmentSlot>), new DelegateValue(Comparison_fogs_proto_msg_EquipmentSlot));
		dict.Add(typeof(Predicate<fogs.proto.msg.ExerciseInfo>), new DelegateValue(Predicate_fogs_proto_msg_ExerciseInfo));
		dict.Add(typeof(Action<fogs.proto.msg.ExerciseInfo>), new DelegateValue(Action_fogs_proto_msg_ExerciseInfo));
		dict.Add(typeof(Comparison<fogs.proto.msg.ExerciseInfo>), new DelegateValue(Comparison_fogs_proto_msg_ExerciseInfo));
		dict.Add(typeof(Predicate<fogs.proto.msg.FashionSlotProto>), new DelegateValue(Predicate_fogs_proto_msg_FashionSlotProto));
		dict.Add(typeof(Action<fogs.proto.msg.FashionSlotProto>), new DelegateValue(Action_fogs_proto_msg_FashionSlotProto));
		dict.Add(typeof(Comparison<fogs.proto.msg.FashionSlotProto>), new DelegateValue(Comparison_fogs_proto_msg_FashionSlotProto));
		dict.Add(typeof(Predicate<fogs.proto.msg.FightRole>), new DelegateValue(Predicate_fogs_proto_msg_FightRole));
		dict.Add(typeof(Action<fogs.proto.msg.FightRole>), new DelegateValue(Action_fogs_proto_msg_FightRole));
		dict.Add(typeof(Comparison<fogs.proto.msg.FightRole>), new DelegateValue(Comparison_fogs_proto_msg_FightRole));
		dict.Add(typeof(Predicate<fogs.proto.msg.GameModeInfo>), new DelegateValue(Predicate_fogs_proto_msg_GameModeInfo));
		dict.Add(typeof(Action<fogs.proto.msg.GameModeInfo>), new DelegateValue(Action_fogs_proto_msg_GameModeInfo));
		dict.Add(typeof(Comparison<fogs.proto.msg.GameModeInfo>), new DelegateValue(Comparison_fogs_proto_msg_GameModeInfo));
		dict.Add(typeof(Predicate<fogs.proto.msg.MailInfo>), new DelegateValue(Predicate_fogs_proto_msg_MailInfo));
		dict.Add(typeof(Action<fogs.proto.msg.MailInfo>), new DelegateValue(Action_fogs_proto_msg_MailInfo));
		dict.Add(typeof(Comparison<fogs.proto.msg.MailInfo>), new DelegateValue(Comparison_fogs_proto_msg_MailInfo));
		dict.Add(typeof(Predicate<fogs.proto.msg.RoleInfo>), new DelegateValue(Predicate_fogs_proto_msg_RoleInfo));
		dict.Add(typeof(Action<fogs.proto.msg.RoleInfo>), new DelegateValue(Action_fogs_proto_msg_RoleInfo));
		dict.Add(typeof(Comparison<fogs.proto.msg.RoleInfo>), new DelegateValue(Comparison_fogs_proto_msg_RoleInfo));
		dict.Add(typeof(Predicate<fogs.proto.msg.SkillSlotProto>), new DelegateValue(Predicate_fogs_proto_msg_SkillSlotProto));
		dict.Add(typeof(Action<fogs.proto.msg.SkillSlotProto>), new DelegateValue(Action_fogs_proto_msg_SkillSlotProto));
		dict.Add(typeof(Comparison<fogs.proto.msg.SkillSlotProto>), new DelegateValue(Comparison_fogs_proto_msg_SkillSlotProto));
		dict.Add(typeof(Predicate<fogs.proto.msg.TaskData>), new DelegateValue(Predicate_fogs_proto_msg_TaskData));
		dict.Add(typeof(Action<fogs.proto.msg.TaskData>), new DelegateValue(Action_fogs_proto_msg_TaskData));
		dict.Add(typeof(Comparison<fogs.proto.msg.TaskData>), new DelegateValue(Comparison_fogs_proto_msg_TaskData));
		dict.Add(typeof(Predicate<fogs.proto.config.AwardConfig>), new DelegateValue(Predicate_fogs_proto_config_AwardConfig));
		dict.Add(typeof(Action<fogs.proto.config.AwardConfig>), new DelegateValue(Action_fogs_proto_config_AwardConfig));
		dict.Add(typeof(Comparison<fogs.proto.config.AwardConfig>), new DelegateValue(Comparison_fogs_proto_config_AwardConfig));
		dict.Add(typeof(Predicate<fogs.proto.config.GenerateNewGoodsArgConfig>), new DelegateValue(Predicate_fogs_proto_config_GenerateNewGoodsArgConfig));
		dict.Add(typeof(Action<fogs.proto.config.GenerateNewGoodsArgConfig>), new DelegateValue(Action_fogs_proto_config_GenerateNewGoodsArgConfig));
		dict.Add(typeof(Comparison<fogs.proto.config.GenerateNewGoodsArgConfig>), new DelegateValue(Comparison_fogs_proto_config_GenerateNewGoodsArgConfig));
		dict.Add(typeof(Predicate<BaseDataConfig2>), new DelegateValue(Predicate_BaseDataConfig2));
		dict.Add(typeof(Action<BaseDataConfig2>), new DelegateValue(Action_BaseDataConfig2));
		dict.Add(typeof(Comparison<BaseDataConfig2>), new DelegateValue(Comparison_BaseDataConfig2));
		dict.Add(typeof(Predicate<DataById>), new DelegateValue(Predicate_DataById));
		dict.Add(typeof(Action<DataById>), new DelegateValue(Action_DataById));
		dict.Add(typeof(Comparison<DataById>), new DelegateValue(Comparison_DataById));
		dict.Add(typeof(Predicate<EventDelegate>), new DelegateValue(Predicate_EventDelegate));
		dict.Add(typeof(Action<EventDelegate>), new DelegateValue(Action_EventDelegate));
		dict.Add(typeof(Comparison<EventDelegate>), new DelegateValue(Comparison_EventDelegate));
		dict.Add(typeof(Predicate<FashionShopConfigItem>), new DelegateValue(Predicate_FashionShopConfigItem));
		dict.Add(typeof(Action<FashionShopConfigItem>), new DelegateValue(Action_FashionShopConfigItem));
		dict.Add(typeof(Comparison<FashionShopConfigItem>), new DelegateValue(Comparison_FashionShopConfigItem));
		dict.Add(typeof(Predicate<Goods>), new DelegateValue(Predicate_Goods));
		dict.Add(typeof(Action<Goods>), new DelegateValue(Action_Goods));
		dict.Add(typeof(Comparison<Goods>), new DelegateValue(Comparison_Goods));
		dict.Add(typeof(Predicate<fogs.proto.msg.KeyValueData>), new DelegateValue(Predicate_fogs_proto_msg_KeyValueData));
		dict.Add(typeof(Action<fogs.proto.msg.KeyValueData>), new DelegateValue(Action_fogs_proto_msg_KeyValueData));
		dict.Add(typeof(Comparison<fogs.proto.msg.KeyValueData>), new DelegateValue(Comparison_fogs_proto_msg_KeyValueData));
		dict.Add(typeof(Predicate<KeyValuePair<uint,bool>>), new DelegateValue(Predicate_KeyValuePair_uint_bool));
		dict.Add(typeof(Action<KeyValuePair<uint,bool>>), new DelegateValue(Action_KeyValuePair_uint_bool));
		dict.Add(typeof(Comparison<KeyValuePair<uint,bool>>), new DelegateValue(Comparison_KeyValuePair_uint_bool));
		dict.Add(typeof(Predicate<Player>), new DelegateValue(Predicate_Player));
		dict.Add(typeof(Action<Player>), new DelegateValue(Action_Player));
		dict.Add(typeof(Comparison<Player>), new DelegateValue(Comparison_Player));
		dict.Add(typeof(Predicate<QualifyingAwardsData>), new DelegateValue(Predicate_QualifyingAwardsData));
		dict.Add(typeof(Action<QualifyingAwardsData>), new DelegateValue(Action_QualifyingAwardsData));
		dict.Add(typeof(Comparison<QualifyingAwardsData>), new DelegateValue(Comparison_QualifyingAwardsData));
		dict.Add(typeof(Predicate<RobotPlayer>), new DelegateValue(Predicate_RobotPlayer));
		dict.Add(typeof(Action<RobotPlayer>), new DelegateValue(Action_RobotPlayer));
		dict.Add(typeof(Comparison<RobotPlayer>), new DelegateValue(Comparison_RobotPlayer));
		dict.Add(typeof(Predicate<RoleBaseData2>), new DelegateValue(Predicate_RoleBaseData2));
		dict.Add(typeof(Action<RoleBaseData2>), new DelegateValue(Action_RoleBaseData2));
		dict.Add(typeof(Comparison<RoleBaseData2>), new DelegateValue(Comparison_RoleBaseData2));
		dict.Add(typeof(Predicate<StoreGoodsData>), new DelegateValue(Predicate_StoreGoodsData));
		dict.Add(typeof(Action<StoreGoodsData>), new DelegateValue(Action_StoreGoodsData));
		dict.Add(typeof(Comparison<StoreGoodsData>), new DelegateValue(Comparison_StoreGoodsData));
		dict.Add(typeof(Predicate<SkillConsumable>), new DelegateValue(Predicate_SkillConsumable));
		dict.Add(typeof(Action<SkillConsumable>), new DelegateValue(Action_SkillConsumable));
		dict.Add(typeof(Comparison<SkillConsumable>), new DelegateValue(Comparison_SkillConsumable));
		dict.Add(typeof(Predicate<TrialData>), new DelegateValue(Predicate_TrialData));
		dict.Add(typeof(Action<TrialData>), new DelegateValue(Action_TrialData));
		dict.Add(typeof(Comparison<TrialData>), new DelegateValue(Comparison_TrialData));
	}

	[NoToLuaAttribute]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateValue create = null;

		if (!dict.TryGetValue(t, out create))
		{
			Debugger.LogError("Delegate {0} not register", t.FullName);
			return null;
		}
		return create(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		Action<GameObject> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action(LuaFunction func)
	{
		Action d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		UnityEngine.Events.UnityAction d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate MainPlayer_DataChangedDelegate(LuaFunction func)
	{
		MainPlayer.DataChangedDelegate d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate System_Reflection_MemberFilter(LuaFunction func)
	{
		System.Reflection.MemberFilter d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate System_Reflection_TypeFilter(LuaFunction func)
	{
		System.Reflection.TypeFilter d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		Application.LogCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		Application.AdvertisingIdentifierCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate AudioClip_PCMReaderCallback(LuaFunction func)
	{
		AudioClip.PCMReaderCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushArray(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		AudioClip.PCMSetPositionCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Camera_CameraCallback(LuaFunction func)
	{
		Camera.CameraCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate EventDelegate_Callback(LuaFunction func)
	{
		EventDelegate.Callback d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIWidget_OnDimensionsChanged(LuaFunction func)
	{
		UIWidget.OnDimensionsChanged d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIWidget_HitCheck(LuaFunction func)
	{
		UIWidget.HitCheck d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate UICamera_OnScreenResize(LuaFunction func)
	{
		UICamera.OnScreenResize d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UICamera_OnCustomInput(LuaFunction func)
	{
		UICamera.OnCustomInput d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate SpringPanel_OnFinished(LuaFunction func)
	{
		SpringPanel.OnFinished d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIEventListener_VoidDelegate(LuaFunction func)
	{
		UIEventListener.VoidDelegate d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIEventListener_BoolDelegate(LuaFunction func)
	{
		UIEventListener.BoolDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIEventListener_FloatDelegate(LuaFunction func)
	{
		UIEventListener.FloatDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIEventListener_VectorDelegate(LuaFunction func)
	{
		UIEventListener.VectorDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIEventListener_ObjectDelegate(LuaFunction func)
	{
		UIEventListener.ObjectDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIEventListener_KeyCodeDelegate(LuaFunction func)
	{
		UIEventListener.KeyCodeDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIGrid_OnReposition(LuaFunction func)
	{
		UIGrid.OnReposition d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate CompareFunc_Transform(LuaFunction func)
	{
		BetterList<Transform>.CompareFunc d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate UIInput_OnValidate(LuaFunction func)
	{
		UIInput.OnValidate d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (char)objs[0];
		};
		return d;
	}

	public static Delegate UIPanel_OnGeometryUpdated(LuaFunction func)
	{
		UIPanel.OnGeometryUpdated d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIPanel_OnClippingMoved(LuaFunction func)
	{
		UIPanel.OnClippingMoved d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UIProgressBar_OnDragFinished(LuaFunction func)
	{
		UIProgressBar.OnDragFinished d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIScrollView_OnDragFinished(LuaFunction func)
	{
		UIScrollView.OnDragFinished d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UITable_OnReposition(LuaFunction func)
	{
		UITable.OnReposition d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UIWrapContent_OnInitializeItem(LuaFunction func)
	{
		UIWrapContent.OnInitializeItem d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate AnimationResp_RespDel(LuaFunction func)
	{
		AnimationResp.RespDel d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Func_int_Transform_GameObject(LuaFunction func)
	{
		Func<int,Transform,GameObject> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (GameObject)objs[0];
		};
		return d;
	}

	public static Delegate FriendData_OnListChanged(LuaFunction func)
	{
		FriendData.OnListChanged d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate GameScene_DEBUG_DRAW_DELEGATE(LuaFunction func)
	{
		GameScene.DEBUG_DRAW_DELEGATE d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate NetworkManager_OnServerConnected(LuaFunction func)
	{
		NetworkManager.OnServerConnected d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action_Object(LuaFunction func)
	{
		Action<Object> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action_string(LuaFunction func)
	{
		Action<string> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action_Texture(LuaFunction func)
	{
		Action<Texture> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DelegateLoadComplete(LuaFunction func)
	{
		DelegateLoadComplete d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action_int_GameObject(LuaFunction func)
	{
		Action<int,GameObject> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UBasket_BasketEventDelegate(LuaFunction func)
	{
		UBasket.BasketEventDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UBasket_BasketEventDunkDelegate(LuaFunction func)
	{
		UBasket.BasketEventDunkDelegate d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UBasketball_BallDelegate(LuaFunction func)
	{
		UBasketball.BallDelegate d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UBasketball_OnDunkDelegate(LuaFunction func)
	{
		UBasketball.OnDunkDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate PlayerState_OnActionDone(LuaFunction func)
	{
		PlayerState.OnActionDone d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate Action_PlayerState_PlayerState(LuaFunction func)
	{
		Action<PlayerState,PlayerState> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Predicate_int(LuaFunction func)
	{
		Predicate<int> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_int(LuaFunction func)
	{
		Action<int> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_int(LuaFunction func)
	{
		Comparison<int> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_uint(LuaFunction func)
	{
		Predicate<uint> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_uint(LuaFunction func)
	{
		Action<uint> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_uint(LuaFunction func)
	{
		Comparison<uint> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_string(LuaFunction func)
	{
		Predicate<string> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Comparison_string(LuaFunction func)
	{
		Comparison<string> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_BadgeSlot(LuaFunction func)
	{
		Predicate<fogs.proto.msg.BadgeSlot> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_BadgeSlot(LuaFunction func)
	{
		Action<fogs.proto.msg.BadgeSlot> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_BadgeSlot(LuaFunction func)
	{
		Comparison<fogs.proto.msg.BadgeSlot> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_BadgeBook(LuaFunction func)
	{
		Predicate<fogs.proto.msg.BadgeBook> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_BadgeBook(LuaFunction func)
	{
		Action<fogs.proto.msg.BadgeBook> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_BadgeBook(LuaFunction func)
	{
		Comparison<fogs.proto.msg.BadgeBook> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_ChatBroadcast(LuaFunction func)
	{
		Predicate<fogs.proto.msg.ChatBroadcast> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_ChatBroadcast(LuaFunction func)
	{
		Action<fogs.proto.msg.ChatBroadcast> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_ChatBroadcast(LuaFunction func)
	{
		Comparison<fogs.proto.msg.ChatBroadcast> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_EquipInfo(LuaFunction func)
	{
		Predicate<fogs.proto.msg.EquipInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_EquipInfo(LuaFunction func)
	{
		Action<fogs.proto.msg.EquipInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_EquipInfo(LuaFunction func)
	{
		Comparison<fogs.proto.msg.EquipInfo> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_EquipmentSlot(LuaFunction func)
	{
		Predicate<fogs.proto.msg.EquipmentSlot> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_EquipmentSlot(LuaFunction func)
	{
		Action<fogs.proto.msg.EquipmentSlot> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_EquipmentSlot(LuaFunction func)
	{
		Comparison<fogs.proto.msg.EquipmentSlot> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_ExerciseInfo(LuaFunction func)
	{
		Predicate<fogs.proto.msg.ExerciseInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_ExerciseInfo(LuaFunction func)
	{
		Action<fogs.proto.msg.ExerciseInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_ExerciseInfo(LuaFunction func)
	{
		Comparison<fogs.proto.msg.ExerciseInfo> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_FashionSlotProto(LuaFunction func)
	{
		Predicate<fogs.proto.msg.FashionSlotProto> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_FashionSlotProto(LuaFunction func)
	{
		Action<fogs.proto.msg.FashionSlotProto> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_FashionSlotProto(LuaFunction func)
	{
		Comparison<fogs.proto.msg.FashionSlotProto> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_FightRole(LuaFunction func)
	{
		Predicate<fogs.proto.msg.FightRole> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_FightRole(LuaFunction func)
	{
		Action<fogs.proto.msg.FightRole> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_FightRole(LuaFunction func)
	{
		Comparison<fogs.proto.msg.FightRole> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_GameModeInfo(LuaFunction func)
	{
		Predicate<fogs.proto.msg.GameModeInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_GameModeInfo(LuaFunction func)
	{
		Action<fogs.proto.msg.GameModeInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_GameModeInfo(LuaFunction func)
	{
		Comparison<fogs.proto.msg.GameModeInfo> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_MailInfo(LuaFunction func)
	{
		Predicate<fogs.proto.msg.MailInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_MailInfo(LuaFunction func)
	{
		Action<fogs.proto.msg.MailInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_MailInfo(LuaFunction func)
	{
		Comparison<fogs.proto.msg.MailInfo> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_RoleInfo(LuaFunction func)
	{
		Predicate<fogs.proto.msg.RoleInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_RoleInfo(LuaFunction func)
	{
		Action<fogs.proto.msg.RoleInfo> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_RoleInfo(LuaFunction func)
	{
		Comparison<fogs.proto.msg.RoleInfo> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_SkillSlotProto(LuaFunction func)
	{
		Predicate<fogs.proto.msg.SkillSlotProto> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_SkillSlotProto(LuaFunction func)
	{
		Action<fogs.proto.msg.SkillSlotProto> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_SkillSlotProto(LuaFunction func)
	{
		Comparison<fogs.proto.msg.SkillSlotProto> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_TaskData(LuaFunction func)
	{
		Predicate<fogs.proto.msg.TaskData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_TaskData(LuaFunction func)
	{
		Action<fogs.proto.msg.TaskData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_TaskData(LuaFunction func)
	{
		Comparison<fogs.proto.msg.TaskData> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_config_AwardConfig(LuaFunction func)
	{
		Predicate<fogs.proto.config.AwardConfig> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_config_AwardConfig(LuaFunction func)
	{
		Action<fogs.proto.config.AwardConfig> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_config_AwardConfig(LuaFunction func)
	{
		Comparison<fogs.proto.config.AwardConfig> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_config_GenerateNewGoodsArgConfig(LuaFunction func)
	{
		Predicate<fogs.proto.config.GenerateNewGoodsArgConfig> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_config_GenerateNewGoodsArgConfig(LuaFunction func)
	{
		Action<fogs.proto.config.GenerateNewGoodsArgConfig> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_config_GenerateNewGoodsArgConfig(LuaFunction func)
	{
		Comparison<fogs.proto.config.GenerateNewGoodsArgConfig> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_BaseDataConfig2(LuaFunction func)
	{
		Predicate<BaseDataConfig2> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_BaseDataConfig2(LuaFunction func)
	{
		Action<BaseDataConfig2> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_BaseDataConfig2(LuaFunction func)
	{
		Comparison<BaseDataConfig2> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_DataById(LuaFunction func)
	{
		Predicate<DataById> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_DataById(LuaFunction func)
	{
		Action<DataById> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_DataById(LuaFunction func)
	{
		Comparison<DataById> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_EventDelegate(LuaFunction func)
	{
		Predicate<EventDelegate> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_EventDelegate(LuaFunction func)
	{
		Action<EventDelegate> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_EventDelegate(LuaFunction func)
	{
		Comparison<EventDelegate> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_FashionShopConfigItem(LuaFunction func)
	{
		Predicate<FashionShopConfigItem> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_FashionShopConfigItem(LuaFunction func)
	{
		Action<FashionShopConfigItem> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_FashionShopConfigItem(LuaFunction func)
	{
		Comparison<FashionShopConfigItem> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_Goods(LuaFunction func)
	{
		Predicate<Goods> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_Goods(LuaFunction func)
	{
		Action<Goods> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_Goods(LuaFunction func)
	{
		Comparison<Goods> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_fogs_proto_msg_KeyValueData(LuaFunction func)
	{
		Predicate<fogs.proto.msg.KeyValueData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_fogs_proto_msg_KeyValueData(LuaFunction func)
	{
		Action<fogs.proto.msg.KeyValueData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_fogs_proto_msg_KeyValueData(LuaFunction func)
	{
		Comparison<fogs.proto.msg.KeyValueData> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_KeyValuePair_uint_bool(LuaFunction func)
	{
		Predicate<KeyValuePair<uint,bool>> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushValue(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_KeyValuePair_uint_bool(LuaFunction func)
	{
		Action<KeyValuePair<uint,bool>> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushValue(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_KeyValuePair_uint_bool(LuaFunction func)
	{
		Comparison<KeyValuePair<uint,bool>> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushValue(L, param0);
			LuaScriptMgr.PushValue(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_Player(LuaFunction func)
	{
		Predicate<Player> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_Player(LuaFunction func)
	{
		Action<Player> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_Player(LuaFunction func)
	{
		Comparison<Player> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_QualifyingAwardsData(LuaFunction func)
	{
		Predicate<QualifyingAwardsData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_QualifyingAwardsData(LuaFunction func)
	{
		Action<QualifyingAwardsData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_QualifyingAwardsData(LuaFunction func)
	{
		Comparison<QualifyingAwardsData> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_RobotPlayer(LuaFunction func)
	{
		Predicate<RobotPlayer> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_RobotPlayer(LuaFunction func)
	{
		Action<RobotPlayer> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_RobotPlayer(LuaFunction func)
	{
		Comparison<RobotPlayer> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_RoleBaseData2(LuaFunction func)
	{
		Predicate<RoleBaseData2> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_RoleBaseData2(LuaFunction func)
	{
		Action<RoleBaseData2> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_RoleBaseData2(LuaFunction func)
	{
		Comparison<RoleBaseData2> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_StoreGoodsData(LuaFunction func)
	{
		Predicate<StoreGoodsData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_StoreGoodsData(LuaFunction func)
	{
		Action<StoreGoodsData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_StoreGoodsData(LuaFunction func)
	{
		Comparison<StoreGoodsData> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_SkillConsumable(LuaFunction func)
	{
		Predicate<SkillConsumable> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_SkillConsumable(LuaFunction func)
	{
		Action<SkillConsumable> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_SkillConsumable(LuaFunction func)
	{
		Comparison<SkillConsumable> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate Predicate_TrialData(LuaFunction func)
	{
		Predicate<TrialData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate Action_TrialData(LuaFunction func)
	{
		Action<TrialData> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Comparison_TrialData(LuaFunction func)
	{
		Comparison<TrialData> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (int)objs[0];
		};
		return d;
	}

	public static void Clear()
	{
		dict.Clear();
	}

}
