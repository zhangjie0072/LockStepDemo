using System;
using UnityEngine;

public class VDebugStatus
{
	GUIText gui;
	
	float updateInterval = 1.0f;
	float lastInterval;
	float frames = 0;
	
	public VDebugStatus()
	{
	    lastInterval = Time.realtimeSinceStartup;
	    frames = 0;
	}
	
	public void Disable ()
	{
		//TODO:fix states error
		if (gui)
			GameObject.DestroyImmediate (gui.gameObject);
	}
	
	public void SetDebugStatus( string field, string info ){
		if (gui){
			gui.text = info;
		}
	}
	
	public void Update()
	{
	#if !UNITY_FLASH
	    ++frames;
	    var timeNow = Time.realtimeSinceStartup;
	    if (timeNow > lastInterval + updateInterval)
	    {
			if (!gui)
			{
				GameObject go = new GameObject("FPS Display" );
				go.AddComponent<GUIText>();
				go.hideFlags = HideFlags.HideAndDontSave;
				go.transform.position = new Vector3(0,0,0);
				gui = go.GetComponent<GUIText>();
			//	gui.pixelOffset = new Vector2(5,55);
			}
	        float fps = frames / (timeNow - lastInterval);
			float ms = 1000.0f / Mathf.Max (fps, 0.00001f);
			gui.text = ms.ToString("f1") + "ms " + fps.ToString("f2") + "FPS";
	        frames = 0;
	        lastInterval = timeNow;
	    }
	#endif
	}
}