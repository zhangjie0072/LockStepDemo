using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using LuaInterface;
public class AnimationResp : MonoBehaviour {

    public delegate void RespDel(string value);
    Dictionary<GameObject, RespDel> cache = new Dictionary<GameObject, RespDel>();


	// Use this for initialization
	void Start () {
	
	}

    public RespDel respDel;

    public void AddResp(LuaFunction func, GameObject go)
    {
        RespDel del = (value) =>
            {
                func.Call(value);
            };
        respDel += del;        
        cache[go] = del;
    }

    public void RemoveResp(GameObject go)
    {
        if (!cache.ContainsKey(go))
            return;
        RespDel del = cache[go];
        if (del != null )
        {
            respDel -= del;
        }
    }

	
    public void OnResp(string value)
    {
        if (cache.Count != 0)
        respDel(value);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
