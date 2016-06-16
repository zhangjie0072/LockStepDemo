using System;
using System.Collections.Generic;
using LuaInterface;

public class VipDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateVipData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("consume", get_consume, set_consume),
			new LuaField("hp_times", get_hp_times, set_hp_times),
			new LuaField("gold_times", get_gold_times, set_gold_times),
			new LuaField("career_times", get_career_times, set_career_times),
			new LuaField("regular_times", get_regular_times, set_regular_times),
			new LuaField("reset_regular_times", get_reset_regular_times, set_reset_regular_times),
			new LuaField("reset_tour_times", get_reset_tour_times, set_reset_tour_times),
			new LuaField("add_training_count", get_add_training_count, set_add_training_count),
			new LuaField("skill_slot", get_skill_slot, set_skill_slot),
			new LuaField("add_vigor_limit", get_add_vigor_limit, set_add_vigor_limit),
			new LuaField("mail_id", get_mail_id, set_mail_id),
			new LuaField("gift", get_gift, set_gift),
			new LuaField("ori_gift_price", get_ori_gift_price, set_ori_gift_price),
			new LuaField("gift_price", get_gift_price, set_gift_price),
			new LuaField("append_sign", get_append_sign, set_append_sign),
			new LuaField("bullfight_buytimes", get_bullfight_buytimes, set_bullfight_buytimes),
			new LuaField("shoot_buytimes", get_shoot_buytimes, set_shoot_buytimes),
			new LuaField("qualifying_buytimes", get_qualifying_buytimes, set_qualifying_buytimes),
			new LuaField("exp_buytimes", get_exp_buytimes, set_exp_buytimes),
		};

		LuaScriptMgr.RegisterLib(L, "VipData", typeof(VipData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateVipData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			VipData obj = new VipData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: VipData.New");
		}

		return 0;
	}

	static Type classType = typeof(VipData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hp_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hp_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hp_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hp_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gold_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gold_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_career_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name career_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index career_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.career_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_regular_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name regular_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index regular_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.regular_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reset_regular_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_regular_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_regular_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.reset_regular_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reset_tour_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_tour_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_tour_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.reset_tour_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_add_training_count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_training_count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_training_count on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.add_training_count);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skill_slot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_slot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_slot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.skill_slot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_add_vigor_limit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_vigor_limit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_vigor_limit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.add_vigor_limit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mail_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mail_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mail_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mail_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gift(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gift);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ori_gift_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ori_gift_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ori_gift_price on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ori_gift_price);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gift_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift_price on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gift_price);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_append_sign(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name append_sign");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index append_sign on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.append_sign);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bullfight_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullfight_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullfight_buytimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bullfight_buytimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shoot_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shoot_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shoot_buytimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shoot_buytimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifying_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifying_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifying_buytimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.qualifying_buytimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_exp_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp_buytimes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.exp_buytimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume on a nil value");
			}
		}

		obj.consume = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hp_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hp_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hp_times on a nil value");
			}
		}

		obj.hp_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gold_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_times on a nil value");
			}
		}

		obj.gold_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_career_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name career_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index career_times on a nil value");
			}
		}

		obj.career_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_regular_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name regular_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index regular_times on a nil value");
			}
		}

		obj.regular_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reset_regular_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_regular_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_regular_times on a nil value");
			}
		}

		obj.reset_regular_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reset_tour_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_tour_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_tour_times on a nil value");
			}
		}

		obj.reset_tour_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_add_training_count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_training_count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_training_count on a nil value");
			}
		}

		obj.add_training_count = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skill_slot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_slot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_slot on a nil value");
			}
		}

		obj.skill_slot = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_add_vigor_limit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_vigor_limit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_vigor_limit on a nil value");
			}
		}

		obj.add_vigor_limit = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mail_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mail_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mail_id on a nil value");
			}
		}

		obj.mail_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gift(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift on a nil value");
			}
		}

		obj.gift = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ori_gift_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ori_gift_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ori_gift_price on a nil value");
			}
		}

		obj.ori_gift_price = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gift_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift_price on a nil value");
			}
		}

		obj.gift_price = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_append_sign(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name append_sign");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index append_sign on a nil value");
			}
		}

		obj.append_sign = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bullfight_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullfight_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullfight_buytimes on a nil value");
			}
		}

		obj.bullfight_buytimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shoot_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shoot_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shoot_buytimes on a nil value");
			}
		}

		obj.shoot_buytimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifying_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifying_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifying_buytimes on a nil value");
			}
		}

		obj.qualifying_buytimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_exp_buytimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipData obj = (VipData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp_buytimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp_buytimes on a nil value");
			}
		}

		obj.exp_buytimes = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

