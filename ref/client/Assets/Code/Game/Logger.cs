
#if ENABLE_LOG

#undef ENABLE_LOG

#endif

#define ENABLE_LOG

using System.Collections;
using UnityEngine;


public class Logger
{

	#if (ENABLE_LOG)
		static private bool _EnableLog = true;
	#else
		static private bool _EnableLog = false;
	#endif
	static public bool EnableLog{
		get{
			return _EnableLog;
		}
		set{
			_EnableLog = value;
			Debug.LogWarning("it's not suggested to set log switch!");
		}
	}
    static public bool EnableTimeTrace = false;
    
	static public void Log(object message)
	{
#if DEBUG_TIME
        if( !message.ToString().StartsWith("【Time】"))
            return;
#endif
	#if(ENABLE_LOG)
		if(_EnableLog)
			Log(message, null);
	#endif
	}
	static public void Log(object message, Object context)
	{
	#if(ENABLE_LOG)
		if(_EnableLog)
		{
            if (EnableTimeTrace)  
            {
                System.DateTime now = System.DateTime.Now;
                string log = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3} | {4}",
                    now.Hour, now.Minute, now.Second, now.Millisecond, message);
                Debug.Log(log, context);
            }
            else
                Debug.Log(message, context);
		}
	#endif
	}
	static public void LogError(object message)
	{
	#if(ENABLE_LOG)
		if(_EnableLog)
		{
			LogError(message, null);
		}
	#endif
	}
	static public void LogError(object message, Object context)
	{
	#if(ENABLE_LOG)
		if(_EnableLog)
		{
            if (EnableTimeTrace)
            {
                System.DateTime now = System.DateTime.Now;
                string log = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3} | {4}",
                    now.Hour, now.Minute, now.Second, now.Millisecond, message);
                Debug.LogError(log, context);
            }
            else
                Debug.LogError(message, context);
		}
	#endif
	}
	static public void LogWarning(object message)
	{
	#if(ENABLE_LOG)

		if(_EnableLog)
		{
			LogWarning(message, null);
		}
	#endif
	}
	static public void LogWarning(object message, Object context)
	{
	#if(ENABLE_LOG)
		if(_EnableLog)
		{
            if (EnableTimeTrace)
            {
                System.DateTime now = System.DateTime.Now;
                string log = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3} | {4}",
                    now.Hour, now.Minute, now.Second, now.Millisecond, message);
                Debug.LogWarning(log, context);
            }
            else
                Debug.LogWarning(message, context);
		}
	#endif
	}
	static public void ConfigBegin(string name)
	{
	#if(ENABLE_LOG)
		if(_EnableLog)
		{
			Debug.Log("Config reading "+name+" ...");
		}
	#endif
	}
	static public void ConfigEnd(string name)
	{
	#if(ENABLE_LOG)
//		if(_EnableLog)
//		{
//			Debug.Log("Config "+name+" end...");
//		}
	#endif
	}
}