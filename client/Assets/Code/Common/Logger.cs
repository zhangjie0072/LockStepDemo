using System.Collections;
using UnityEngine;

public class Logger
{

	static public bool EnableLog = true;
	static public void Log(object message)
	{
		Log(message, null);
	}
	static public void Log(object message, Object context)
	{
		if (EnableLog)
		{
			Debug.Log(message, context);
		}
	}
	static public void LogError(object message)
	{
		LogError(message, null);
	}
	static public void LogError(object message, Object context)
	{
		if (EnableLog)
		{
			Debug.LogError(message, context);
		}
	}
	static public void LogWarning(object message)
	{
		LogWarning(message, null);
	}
	static public void LogWarning(object message, Object context)
	{
		if (EnableLog)
		{
			Debug.LogWarning(message, context);
		}
	}
}