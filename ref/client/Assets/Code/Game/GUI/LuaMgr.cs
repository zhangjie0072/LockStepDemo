using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaMgr : Singleton<LuaMgr>
{
	LuaTable tblTime;
	LuaFunction funcSetFixedDelta;

    public LuaMgr()
    {
   
    }
    public void Init()
    {
        System.DateTime time = System.DateTime.Now;
        _luaScriptMgr = new LuaScriptMgr();
        Logger.Log("【Time】LuaMgr>>>new LuaScriptMgr=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        time = System.DateTime.Now;
        _luaScriptMgr.Start();
        Logger.Log("【Time】LuaMgr>>>_luaScriptMgr.Start=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        string init = "require 'common/functions'\n" +
						"require 'pbc/protobuf'\n" +
						"parser = require 'pbc/parser'\n" +
						"require 'Custom/Common/Common'\n";

        time = System.DateTime.Now;
        _luaScriptMgr.DoString(init);
        Logger.Log("【Time】LuaMgr>>>_luaScriptMgr.DoString=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        tblTime = LuaScriptMgr.Instance.GetLuaTable("Time");
		funcSetFixedDelta = tblTime["SetFixedDelta"] as LuaFunction;
    }

	public void FixedUpdate()
	{
		funcSetFixedDelta.Call(new object[] { tblTime, Time.fixedDeltaTime });
	}

    private LuaScriptMgr _luaScriptMgr;
}


public static class LuaTool
{
    static public LuaTable CreateLuaTable()
    {
        return (LuaTable)LuaScriptMgr.Instance.DoString("return {}")[0];
    }

    static public LuaTable CreateLuaTable(IEnumerable objs)
    {
        var table = CreateLuaTable();
        int index = 0;
        foreach (var obj in objs)
        {
            table[index.ToString()] = obj;
            index++;
        }
        return table;
    }

    static public LuaTable CreateLuaTable(IList objs)
    {
        var table = CreateLuaTable();
        int index = 1;
        foreach (var obj in objs)
        {
            table[index] = obj;
            index++;
        }
        return table;
    }

    static public LuaTable CreateLuaTable(IDictionary objs)
    {
        var table = CreateLuaTable();

        foreach (var key in objs.Keys)
        {
            table[key] = objs[key];
        }
        return table;
    }

    public static LuaTable toLuaTable(this IEnumerable objs)
    {
        return CreateLuaTable(objs);
    }

    public static LuaTable toLuaTable(this IList objs)
    {
        return CreateLuaTable(objs);
    }

    public static LuaTable toLuaTable(this IDictionary objs)
    {
        return CreateLuaTable(objs);
    }
}
