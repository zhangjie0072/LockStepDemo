/*************************************************
author：ricky pu
data：2014.4.12
email:32145628@qq.com
**********************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;
using Junfine.Debuger;
using fogs.proto.msg;

public static class LuaHelper {

    /// <summary>
    /// getType
    /// </summary>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static System.Type GetType(string classname) {
        Assembly assb = Assembly.GetExecutingAssembly();  //.GetExecutingAssembly();
        System.Type t = null;
        t = assb.GetType(classname); ;
        if (t == null) {
            t = assb.GetType(classname);
        }
        return t;
    }

    /// <summary>
    /// GetComponentInChildren
    /// </summary>
    public static Component GetComponentInChildren(GameObject obj, string classname) {
        System.Type t = GetType(classname);
        Component comp = null;
        if (t != null && obj != null) comp = obj.GetComponentInChildren(t);
        return comp;
    }

    /// <summary>
    /// GetComponent
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static Component GetComponent(GameObject obj, string classname) {
        if (obj == null) return null; 
        return obj.GetComponent(classname);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static Component[] GetComponentsInChildren(GameObject obj, string classname) {
        System.Type t = GetType(classname);
        if (t != null && obj != null) return obj.transform.GetComponentsInChildren(t);
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Transform[] GetAllChild(GameObject obj) {
        Transform[] child = null;
        int count = obj.transform.childCount;
        child = new Transform[count];
        for (int i = 0; i < count; i++) {
            child[i] = obj.transform.GetChild(i);
        }
        return child;
    }


    public static Action Action(LuaFunction func) {
        Action action = () => {
            func.Call();
        };
        return action;
    }

    public static EventDelegate.Callback Callback(LuaFunction func)
    {
        EventDelegate.Callback callback = () =>
        {
            func.Call();
        };
        return callback;
    }

    /// <summary>
    /// pbc/pblua函数回调
    /// </summary>
    /// <param name="func"></param>
    public static void OnCallLuaFunc(LuaStringBuffer data, LuaFunction func) {
        byte[] buffer = data.buffer;
        Logger.LogWarning("OnCallLuaFunc buffer:>>" + buffer + " lenght:>>" + buffer.Length);
        if (func == null) return;
        /*
         * 调用此函数需要获取Luastate的指针(mgr.lua.L)，根据自身项目修改此处。
        int oldTop = func.BeginPCall();
        LuaDLL.lua_pushlstring(mgr.lua.L, buffer, buffer.Length);
        if (func.PCall(oldTop, 1)) func.EndPCall();
         * */
    }

	public static UIEventListener.BoolDelegate BoolDelegate(LuaFunction func)
	{
		UIEventListener.BoolDelegate action = (go ,isPressed) =>
		{
            func.Call(new object[] { go, isPressed });
		};
		return action;
	}
    public static UIEventListener.VoidDelegate VoidDelegate(LuaFunction func)
    {
        UIEventListener.VoidDelegate action = (go) =>
        {
            func.Call(go);
        };
        return action;
    }
    //test
    public static UIEventListener.VectorDelegate VectorDelegate(LuaFunction func)
    {
        UIEventListener.VectorDelegate action = (go ,delta) =>
        {
            func.Call(new object[] {go ,delta});
        };
        return action;
    }
    //test

    public static BetterList<Transform>.CompareFunc BetterListCB(LuaFunction func)
    {
        BetterList<Transform>.CompareFunc action = (a,b) =>
        {
            object[] r = func.Call(a, b);
            return (int)r[0];
        };
        return action;
    }

    public static void SendPlatMsgFromLuaNoWait(uint msgID, LuaStringBuffer data)
    {
        GameSystem.Instance.mNetworkManager.m_platConn.SendMsgFromLua(msgID, data);
    }

	//发包
    public static void SendPlatMsgFromLua(uint msgID, LuaStringBuffer data)
    {
		if ((MsgID)msgID == MsgID.EnterGameReqID)
			PlatNetwork.Instance.cachedEnterGameReq = data;
		else if ((MsgID)msgID == MsgID.ExitGameReqID)
			PlatNetwork.Instance.cachedEnterGameReq = null;
		CommonFunction.ShowWaitMask();
        GameSystem.Instance.mNetworkManager.m_platConn.SendMsgFromLua(msgID, data);
    }

	public static void SendGameMsgFromLua(uint msgID, LuaStringBuffer data)
	{
		GameSystem.Instance.mNetworkManager.m_gameConn.SendMsgFromLua(msgID, data);
	}

	private static Dictionary<string, Dictionary<uint, MsgHandler.Handle>> msgHandlers 
		= new Dictionary<string, Dictionary<uint, MsgHandler.Handle>>();

	public static void RegisterPlatMsgHandler(uint msgID, LuaFunction func, string uiName)
    {
        MsgHandler.Handle handler = (Pack pack) =>
        {
			CommonFunction.HideWaitMask();
            func.Call(new LuaStringBuffer(pack.buffer));
        };
        Dictionary<uint, MsgHandler.Handle> msgMap = null;
        if (!msgHandlers.TryGetValue(uiName, out msgMap))
        {
            msgMap = new Dictionary<uint, MsgHandler.Handle>();
            msgHandlers.Add(uiName, msgMap);
        }
        //if (msgMap.ContainsKey(msgID) == false)
        {
            msgMap[msgID] = handler;
		    GameSystem.Instance.mNetworkManager.m_platMsgHandler.RegisterHandler((fogs.proto.msg.MsgID)msgID, handler);
        }
	}

	public static void UnRegisterPlatMsgHandler(uint msgID, string uiName)
	{
		Dictionary<uint, MsgHandler.Handle> msgMap = null;
		if (!msgHandlers.TryGetValue(uiName, out msgMap))
		{
            Logger.LogError("No msg handlers for UI: " + uiName);
        }
		MsgHandler.Handle handler;
		if (msgMap.TryGetValue(msgID, out handler))
		{
			NetworkManager nmgr = GameSystem.Instance.mNetworkManager;
			if (nmgr != null )
				GameSystem.Instance.mNetworkManager.m_platMsgHandler.UnregisterHandler((fogs.proto.msg.MsgID)msgID, handler);
		}
	}

	public static void RegisterGameMsgHandler(uint msgID, LuaFunction func, string uiName)
	{
		MsgHandler.Handle handler = (Pack pack) =>
		{
			func.Call(new LuaStringBuffer(pack.buffer));
		};
		Dictionary<uint, MsgHandler.Handle> msgMap = null;
		if (!msgHandlers.TryGetValue(uiName, out msgMap))
		{
			msgMap = new Dictionary<uint, MsgHandler.Handle>();
			msgHandlers.Add(uiName, msgMap);
		}
		//if (msgMap.ContainsKey(msgID) == false)
		{
			msgMap[msgID] = handler;
			GameSystem.Instance.mNetworkManager.m_gameMsgHandler.RegisterHandler((fogs.proto.msg.MsgID)msgID, handler);
		}
	}
	
	public static void UnRegisterGameMsgHandler(uint msgID, string uiName)
	{
		Dictionary<uint, MsgHandler.Handle> msgMap = null;
		if (!msgHandlers.TryGetValue(uiName, out msgMap))
		{
			Logger.LogError("Need to pass the right uiName");
		}
		MsgHandler.Handle handler;
		if (msgMap.TryGetValue(msgID, out handler))
		{
			NetworkManager nmgr = GameSystem.Instance.mNetworkManager;
			if (nmgr != null)
				GameSystem.Instance.mNetworkManager.m_gameMsgHandler.UnregisterHandler((fogs.proto.msg.MsgID)msgID, handler);
		}
	}


    /// <summary>
    /// cjson函数回调
    /// </summary>
    /// <param name="data"></param>
    /// <param name="func"></param>
    public static void OnJsonCallFunc(string data, LuaFunction func) {
        Logger.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
        if (func != null) func.Call(data);
    }
}