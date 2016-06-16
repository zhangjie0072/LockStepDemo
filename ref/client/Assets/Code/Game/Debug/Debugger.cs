using UnityEngine;
using System;
using System.Collections.Generic;

public class Debugger
	: Singleton<Debugger>
{
	public bool m_bEnableAI = true;

	public bool m_bEnableDebugMsg = false; 
	public bool m_bEnableDefenderAction = true;
	public bool m_bEnableTiming = true;

	public DebugStreamer m_steamer;

	private Dictionary<string, GameObject>	m_debugShapes = new Dictionary<string, GameObject>();

	public Debugger()
	{
		GameObject debug_streamer = new GameObject("Debug Streamer");
		m_steamer = debug_streamer.AddComponent<DebugStreamer>();
	}

	public void DrawSphere(string type, Vector3 pos, Color color, float range = 0.1f)
	{
		if( !m_bEnableDebugMsg )
		{
			Reset();
			return;
		}

		GameObject sphere;
		if( !m_debugShapes.TryGetValue(type, out sphere) )
		{
			sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.GetComponent<SphereCollider>().enabled = false;
			sphere.name = type;
			m_debugShapes.Add(type, sphere);
		}
		sphere.transform.position = pos;
		sphere.transform.localScale = Vector3.one * range;

		sphere.GetComponent<Renderer>().material.color = color;
	}

	public void Reset()
	{
		m_debugShapes.Clear();
		m_steamer.enabled = m_bEnableDebugMsg;
	}

    public static void Log(string str, params object[] args)
    {
        str = string.Format(str, args);
        Logger.Log(str);
    }

    public static void LogWarning(string str, params object[] args)
    {
        str = string.Format(str, args);
        Logger.LogWarning(str);
    }

    public static void LogError(string str, params object[] args)
    {
        str = string.Format(str, args);
        Logger.LogError(str);
    }
}
